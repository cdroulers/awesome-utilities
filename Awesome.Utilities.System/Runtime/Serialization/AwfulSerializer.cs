using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Reflection;
using System.Collections;

namespace System.Runtime.Serialization
{
    /// <summary>
    ///     An awful serializer that simply iterates public properties and renders them as string however possible.
    ///     Takes into account a few different things, such as lists and dictionaries.
    /// </summary>
    public class AwfulSerializer
    {
        /// <summary>
        ///     Indentation separator
        /// </summary>
        public readonly string Indentation;

        /// <summary>
        /// Gets the types that should be serialized as strings.
        /// </summary>
        public List<Type> StringableTypes { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwfulSerializer"/> class.
        /// </summary>
        /// <param name="indentation">The indentation.</param>
        public AwfulSerializer(string indentation)
        {
            this.Indentation = indentation;
            this.StringableTypes = new List<Type>()
            {
                typeof(string),
                typeof(DateTime),
                typeof(TimeSpan),
                typeof(IPAddress),
                typeof(Uri)
            };
        }

        /// <summary>
        /// Adds the specified stringable type to the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual AwfulSerializer AddStringableType<T>()
        {
            return this.AddStringableTypes(typeof(T));
        }

        /// <summary>
        /// Adds the specified stringable types to the list.
        /// </summary>
        /// <param name="types">The types.</param>
        /// <returns></returns>
        public virtual AwfulSerializer AddStringableTypes(params Type[] types)
        {
            this.StringableTypes.AddRange(types);
            return this;
        }

        /// <summary>
        /// Serializes the specified graph.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <returns>The graph, serialized</returns>
        public virtual string Serialize<T>(T graph)
        {
            var builder = new StringBuilder();
            this.Serialize(graph, builder);
            return builder.ToString().Trim();
        }

        /// <summary>
        /// Serializes the specified graph.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="builder">The builder.</param>
        public virtual void Serialize<T>(T graph, StringBuilder builder)
        {
            builder.AppendLine(GetTypeName(graph != null ? graph.GetType() : typeof(T)));
            this.Serialize(graph, builder, AwfulSerializer.DefaultLevel);
        }

        private const int DefaultLevel = 1;

        private void Serialize(object graph, StringBuilder builder, int level)
        {
            if (graph == null)
            {
                this.AppendLine(builder, level, "NULL");
                return;
            }
            var type = graph.GetType();
            if (this.IsOneLine(type))
            {
                this.AppendLine(builder, 0, graph.ToString());
            }
            else if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                if (level > AwfulSerializer.DefaultLevel)
                {
                    builder.AppendLine();
                }
                int index = 0;
                foreach (var obj in graph as IEnumerable)
                {
                    Type objType = null;
                    if (obj != null)
                    {
                        objType = obj.GetType();
                    }
                    else
                    {
                        objType = graph.GetType().IsGenericType ? graph.GetType().GetGenericArguments().First() : typeof(void);
                    }
                    if (objType.Name.StartsWith("KeyValuePair`"))
                    {
                        var keyProp = obj.GetType().GetProperty("Key");
                        var valueProp = obj.GetType().GetProperty("Value");
                        var key = keyProp.GetValue(obj, null);
                        var value = valueProp.GetValue(obj, null);

                        if (this.IsOneLine(keyProp.PropertyType) && this.IsOneLine(valueProp.PropertyType))
                        {
                            this.SerializeOne(builder, level, key.ToString(), value, valueProp.PropertyType);
                        }
                        else
                        {
                            this.SerializeOne(builder, level, keyProp.Name, key, keyProp.PropertyType);
                            this.SerializeOne(builder, level, valueProp.Name, value, valueProp.PropertyType);
                        }
                    }
                    else
                    {
                        this.SerializeOne(builder, level, index.ToString(), obj, objType);
                    }
                    index++;
                }
            }
            else
            {
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);
                foreach (var property in properties)
                {
                    this.Append(builder, level, AwfulSerializer.GetPropertyName(property));
                    this.Serialize(property.GetValue(graph, null), builder, level + 1);
                }
            }
        }

        private bool IsOneLine(Type type)
        {
            return type.IsPrimitive || type.IsEnum || this.StringableTypes.Contains(type);
        }

        private static string GetPropertyName(PropertyInfo property)
        {
            return property.Name + " (" + GetTypeName(property.PropertyType) + ") => ";
        }

        private static string GetTypeName(Type type)
        {
            string propertyType = type.Name;
            if (!type.IsArray && type.IsGenericType)
            {
                propertyType = propertyType.Substring(0, propertyType.IndexOf("`")) + "<" + string.Join(", ", type.GetGenericArguments().Select(a => a.Name)) + ">";
            }
            return propertyType;
        }

        private void SerializeOne(StringBuilder builder, int level, string name, object obj, Type objType)
        {
            if (this.IsOneLine(objType))
            {
                this.Append(builder, level, name.PadRight(4) + " => ");
                this.Serialize(obj, builder, 0);
            }
            else
            {
                this.AppendLine(builder, level, name.PadRight(4));
                this.Serialize(obj, builder, level + 1);
            }
        }

        private void Append(StringBuilder builder, int level, string value)
        {
            for (int i = 0; i < level; i++)
            {
                builder.Append(this.Indentation);
            }
            builder.Append(value);
        }

        private void AppendLine(StringBuilder builder, int level, string value)
        {
            for (int i = 0; i < level; i++)
            {
                builder.Append(this.Indentation);
            }
            builder.AppendLine(value);
        }
    }
}
