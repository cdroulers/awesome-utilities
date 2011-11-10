using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.Mvc;

namespace System.Web.Mvc.UI
{
    /// <summary>
    ///     Represents a basic control that is to be subclassed
    /// </summary>
    public abstract class Control
    {
        /// <summary>
        ///     If set to false, will not create new lines before and after the tags
        /// </summary>
        public bool IsIndented { get; protected set; }

        /// <summary>
        ///     The html Id of this control.
        /// </summary>
        public virtual string ID
        {
            get { return this.NullableAttribute("id"); }
            set { this.attributes["id"] = CreateSafeID(value); }
        }

        /// <summary>
        ///     The CSS class to apply to this Control.
        /// </summary>
        public virtual string CssClass
        {
            get { return this.NullableAttribute("class"); }
            set { this.attributes["class"] = value; }
        }

        /// <summary>
        /// Gets or sets the CSS class that this control absolutely requires.
        /// </summary>
        /// <value>The control CSS class.</value>
        protected string ControlCssClass { get; set; }

        /// <summary>
        ///     The style attributes of this control
        /// </summary>
        protected string Style
        {
            get { return this.NullableAttribute("style"); }
            set { this.attributes["style"] = value; }
        }

        /// <summary>
        ///     The width of the control.
        /// </summary>
        public virtual string Width { get; set; }

        /// <summary>
        ///     The height of the control.
        /// </summary>
        public virtual string Height { get; set; }

        /// <summary>
        ///     Gets or sets the element's title
        /// </summary>
        /// <value>The title.</value>
        public virtual string Title
        {
            get { return this.NullableAttribute("title"); }
            set { this.attributes["title"] = value; }
        }

        /// <summary>
        ///     The regex of valid characters for an ID. Reference: http://www.w3.org/TR/html4/types.html#type-name
        /// </summary>
        private static readonly Regex IDRegex = new Regex(@"[^a-z0-9_\-:\.]", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        ///     Creates an ID that is safe to use for any element
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static string CreateSafeID(string id)
        {
            if (id == null) return null;

            return IDRegex.Replace(id, "_");
        }

        /// <summary>
        ///     Javascript event for "Loading of an image is interrupted"
        /// </summary>
        public string OnAbort
        {
            get { return this.NullableAttribute("onabort"); }
            set { this.attributes.Add("onabort", value); }
        }
        /// <summary>
        ///     Javascript event for "An element loses focus"
        /// </summary>
        public string OnBlur
        {
            get { return this.NullableAttribute("onblur"); }
            set { this.attributes.Add("onblur", value); }
        }
        /// <summary>
        ///     Javascript event for "The user changes the content of a field"
        /// </summary>
        public string OnChange
        {
            get { return this.NullableAttribute("onchange"); }
            set { this.attributes.Add("onchange", value); }
        }
        /// <summary>
        ///     Javascript event for "Mouse clicks an object"
        /// </summary>
        public string OnClick
        {
            get { return this.NullableAttribute("onclick"); }
            set { this.attributes.Add("onclick", value); }
        }
        /// <summary>
        ///     Javascript event for "Mouse double-clicks an object"
        /// </summary>
        public string OnDoubleClick
        {
            get { return this.NullableAttribute("ondblclick"); }
            set { this.attributes.Add("ondblclick", value); }
        }
        /// <summary>
        ///     Javascript event for "An error occurs when loading a document or and image"
        /// </summary>
        public string OnError
        {
            get { return this.NullableAttribute("onerror"); }
            set { this.attributes.Add("onerror", value); }
        }
        /// <summary>
        ///     Javascript event for "An element gets focus"
        /// </summary>
        public string OnFocus
        {
            get { return this.NullableAttribute("onfocus"); }
            set { this.attributes.Add("onfocus", value); }
        }
        /// <summary>
        ///     Javascript event for "A keyboard key is pressed"
        /// </summary>
        public string OnKeyDown
        {
            get { return this.NullableAttribute("onkeydown"); }
            set { this.attributes.Add("onkeydown", value); }
        }
        /// <summary>
        ///     Javascript event for "A keyboard key is pressed or held down"
        /// </summary>
        public string OnKeyPress
        {
            get { return this.NullableAttribute("onkeypress"); }
            set { this.attributes.Add("onkeypress", value); }
        }
        /// <summary>
        ///     Javascript event for "A keyboard key is released"
        /// </summary>
        public string OnKeyUp
        {
            get { return this.NullableAttribute("onkeyup"); }
            set { this.attributes.Add("onkeyup", value); }
        }
        /// <summary>
        ///     Javascript event for "A page or an image is finished loading"
        /// </summary>
        public string OnLoad
        {
            get { return this.NullableAttribute("onload"); }
            set { this.attributes.Add("onload", value); }
        }
        /// <summary>
        ///     Javascript event for "A mouse button is pressed"
        /// </summary>
        public string OnMouseDown
        {
            get { return this.NullableAttribute("onmousedown"); }
            set { this.attributes.Add("onmousedown", value); }
        }
        /// <summary>
        ///     Javascript event for "The mouse is moved"
        /// </summary>
        public string OnMouseMove
        {
            get { return this.NullableAttribute("onmousemove"); }
            set { this.attributes.Add("onmousemove", value); }
        }
        /// <summary>
        ///     Javascript event for "The mouse is moved off an element"
        /// </summary>
        public string OnMouseOut
        {
            get { return this.NullableAttribute("onmouseout"); }
            set { this.attributes.Add("onmouseout", value); }
        }
        /// <summary>
        ///     Javascript event for "The mouse is moved over an element"
        /// </summary>
        public string OnMouseOver
        {
            get { return this.NullableAttribute("onmouseover"); }
            set { this.attributes.Add("onmouseover", value); }
        }
        /// <summary>
        ///     Javascript event for "A mouse button is released"
        /// </summary>
        public string OnMouseUp
        {
            get { return this.NullableAttribute("onmouseup"); }
            set { this.attributes.Add("onmouseup", value); }
        }
        /// <summary>
        ///     Javascript event for "The reset button is clicked"
        /// </summary>
        public string OnReset
        {
            get { return this.NullableAttribute("onreset"); }
            set { this.attributes.Add("onreset", value); }
        }
        /// <summary>
        ///     Javascript event for "A window or frame is resized"
        /// </summary>
        public string OnResize
        {
            get { return this.NullableAttribute("onresize"); }
            set { this.attributes.Add("onresize", value); }
        }
        /// <summary>
        ///     Javascript event for "Text is selected"
        /// </summary>
        public string OnSelect
        {
            get { return this.NullableAttribute("onselect"); }
            set { this.attributes.Add("onselect", value); }
        }
        /// <summary>
        ///     Javascript event for "The submit button is clicked"
        /// </summary>
        public string OnSubmit
        {
            get { return this.NullableAttribute("onsubmit"); }
            set { this.attributes.Add("onsubmit", value); }
        }
        /// <summary>
        ///     Javascript event for "The user exits the page"
        /// </summary>
        public string OnUnload
        {
            get { return this.NullableAttribute("onunload"); }
            set { this.attributes.Add("onunload", value); }
        }

        /// <summary>
        ///     The html attributes of this control
        /// </summary>
        protected readonly IDictionary<string, string> attributes = new Dictionary<string, string>();
        /// <summary>
        ///     The attributes to ignore for this control
        /// </summary>
        protected readonly IList<string> ignoredAttributes = new List<string>();

        /// <summary>
        ///     The tag name of this control
        /// </summary>
        public HtmlTextWriterTag Tag { get; protected set; }

        /// <summary>
        ///     Returns the tag as a valid html tag name
        /// </summary>
        protected string TagName { get { return this.Tag.ToString().ToLowerInvariant(); } }

        /// <summary>
        ///     The way we will close the tag
        /// </summary>
        public bool IsSelfClosing { get; set; }

        /// <summary>
        ///     The default for IsSelfClosing
        /// </summary>
        protected const bool IsSelfClosingDefault = false;

        /// <summary>
        ///     Creates a control with the specified tag name
        /// </summary>
        /// <param name="tagName">The tag name</param>
        protected Control(HtmlTextWriterTag tagName)
            : this(tagName, Control.IsSelfClosingDefault) { }

        /// <summary>
        ///     Creates a control with the specified tag name
        /// </summary>
        /// <param name="tagName">The tag name</param>
        /// <param name="isSelfClosing">The closing option for this tag</param>
        protected Control(HtmlTextWriterTag tagName, bool isSelfClosing)
        {
            this.Tag = tagName;
            this.IsSelfClosing = isSelfClosing;
            this.IsIndented = true;
        }

        /// <summary>
        ///     Gets a nullable attribute's value
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        protected string NullableAttribute(string name)
        {
            return this.attributes.ContainsKey(name) ? this.attributes[name] : null;
        }

        /// <summary>
        /// Get an enum attribute 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        protected T EnumAttribute<T>(string name)
        {
            return (T)Enum.Parse(typeof(T), (this.NullableAttribute(name)), true);
        }

        /// <summary>
        /// Get an enum attribute with default value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        protected T EnumAttributeWithDefault<T>(string name, T defaultValue)
        {
            return (T)Enum.Parse(typeof(T), (this.NullableAttribute(name) ?? defaultValue.ToString()), true);
        }

        /// <summary>
        /// Sets an enum attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        protected void EnumAttribute<T>(string name, T value)
        {
            this.NullableAttribute(name, value.ToString().ToLowerInvariant());
        }

        /// <summary>
        ///     Sets a nullable value. If the value is null or empty, removes the attribute altogether
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        protected void NullableAttribute(string name, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                this.attributes.Remove(name);
            }
            else
            {
                this.attributes[name] = value;
            }
        }

        /// <summary>
        ///     Gets a boolean attribute value (Boolean attributes whose value is the attribute name (e.g. readonly, disabled))
        /// </summary>
        /// <param name="name">The name of the attribute</param>
        /// <returns>The current value of the attribute</returns>
        protected bool BooleanAttribute(string name)
        {
            return this.NullableAttribute(name) == name;
        }

        /// <summary>
        ///     Sets a boolean attribute value (Boolean attributes whose value is the attribute name (e.g. readonly, disabled))
        /// </summary>
        /// <param name="name">The name of the attribute</param>
        /// <param name="value">The value of the boolean attribute</param>
        protected void BooleanAttribute(string name, bool value)
        {
            if (value)
            {
                this.attributes.Add(name, name);
            }
            else
            {
                this.attributes.Remove(name);
            }
        }

        /// <summary>
        ///     Updates the control's attributes according to a dictionary of attributes
        /// </summary>
        /// <param name="attributes"></param>
        public void UpdateAttributes(IDictionary<string, string> attributes)
        {
            foreach (KeyValuePair<string, string> attribute in attributes)
            {
                this.attributes[attribute.Key] = attribute.Value.ToString();
            }
        }

        /// <summary>
        ///     Is automatically called when using &lt;%= new ControlName() %&gt;
        /// </summary>
        /// <returns>The html representation of the control</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
            {
                using (HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter, "    "))
                {
                    this.ToString(htmlTextWriter);
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Creates and HTML string from the control
        /// </summary>
        /// <returns></returns>
        public MvcHtmlString ToHtmlString()
        {
            return MvcHtmlString.Create(this.ToString());
        }

        /// <summary>
        ///     Write the control to the htmlTextWriter. Used to keep indentation with controls that render subcontrols.
        /// </summary>
        /// <param name="htmlTextWriter">The writer to output to</param>
        private void ToString(HtmlTextWriter htmlTextWriter)
        {
            this.OnPreRender(htmlTextWriter);

            this.Render(htmlTextWriter);
        }

        /// <summary>
        ///     If anything needs to be done before rendering (including css, javascript, etc.)
        /// </summary>
        /// <param name="htmlTextWriter">The writer to write to.</param>
        protected virtual void OnPreRender(HtmlTextWriter htmlTextWriter)
        {
        }

        /// <summary>
        ///     Clean stuff to make sure the html is valid and clean
        /// </summary>
        private void CleanAttributes()
        {
            if (!string.IsNullOrEmpty(this.CssClass))
            {
                this.CssClass = this.CssClass.Trim();
            }
            if (string.IsNullOrEmpty(this.ID) && this.attributes.ContainsKey("id"))
            {
                this.attributes.Remove("id");
            }

            string style = string.Empty;
            if (!string.IsNullOrEmpty(this.Width))
            {
                style += "width:" + this.Width + ";";
            }
            if (!string.IsNullOrEmpty(this.Height))
            {
                style += "height:" + this.Height + ";";
            }
            if (!string.IsNullOrEmpty(style))
            {
                if (string.IsNullOrWhiteSpace(this.Style))
                {
                    this.Style = style;
                }
                else
                {
                    if (!this.Style.Trim().EndsWith(";"))
                    {
                        this.Style += ";";
                    }
                    this.Style += style;
                }
            }
        }

        /// <summary>
        ///     The basic rendering of the Control (i.e. the opening tag, call render content, close tag)
        /// </summary>
        /// <param name="htmlTextWriter">The writer to write to.</param>
        protected virtual void Render(HtmlTextWriter htmlTextWriter)
        {
            this.CleanAttributes();

            // Add the control CSS class
            if (!string.IsNullOrEmpty(this.ControlCssClass))
            {
                this.CssClass += (string.IsNullOrEmpty(this.CssClass) ? string.Empty : " ") + this.ControlCssClass;
            }

            htmlTextWriter.WriteBeginTag(this.TagName);

            foreach (var attribute in this.attributes)
            {
                if (!this.ignoredAttributes.Contains(attribute.Key))
                {
                    htmlTextWriter.WriteAttribute(attribute.Key, attribute.Value);
                }
            }

            if (this.IsSelfClosing)
            {
                htmlTextWriter.Write(HtmlTextWriter.SelfClosingTagEnd);
            }
            else
            {
                if (this.IsIndented)
                {
                    htmlTextWriter.WriteLine(HtmlTextWriter.TagRightChar);
                    htmlTextWriter.Indent++;
                }
                else
                {
                    htmlTextWriter.Write(HtmlTextWriter.TagRightChar);
                }

                this.RenderContents(htmlTextWriter);

                if (this.IsIndented)
                {
                    htmlTextWriter.Indent--;
                    htmlTextWriter.WriteEndTag(this.TagName);
                    htmlTextWriter.WriteLine();
                }
                else
                {
                    htmlTextWriter.WriteEndTag(this.TagName);
                }
            }

            // Remove the control CSS class
            if (!string.IsNullOrEmpty(this.ControlCssClass) && !string.IsNullOrEmpty(this.CssClass))
            {
                this.CssClass = this.CssClass.Replace(this.ControlCssClass, string.Empty).Trim();
            }
        }

        /// <summary>
        ///     Render the contents of the control
        /// </summary>
        /// <param name="htmlTextWriter">The writer to write to</param>
        protected virtual void RenderContents(HtmlTextWriter htmlTextWriter)
        {
        }

        /// <summary>
        /// Adds the CSS class.
        /// </summary>
        /// <param name="cssClass">The CSS class.</param>
        /// <returns></returns>
        public virtual Control AddCssClass(string cssClass)
        {
            string temp = this.CssClass ?? string.Empty;
            if (!temp.Contains(cssClass))
            {
                temp += " " + cssClass;
            }
            this.CssClass = temp.Trim();

            return this;
        }

        /// <summary>
        /// Removes the CSS class.
        /// </summary>
        /// <param name="cssClass">The CSS class.</param>
        /// <returns></returns>
        public virtual Control RemoveCssClass(string cssClass)
        {
            string temp = this.CssClass ?? string.Empty;
            if (temp.Contains(cssClass))
            {
                temp = temp.Replace(this.ControlCssClass, string.Empty);
            }
            this.CssClass = temp.Trim();

            return this;
        }

        /// <summary>
        /// Adds the attribute
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual Control AddAttribute(string name, string value)
        {
            this.attributes[name] = value;

            return this;
        }

        /// <summary>
        /// Removes the attribute
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public virtual Control RemoveAttribute(string name)
        {
            if (this.attributes.ContainsKey(name))
            {
                this.attributes.Remove(name);
            }

            return this;
        }

        /// <summary>
        /// Gets or sets the translation delegate that will be used in any control requiring translation
        /// </summary>
        /// <value>The translation delegate.</value>
        public static Func<string, string> TranslationDelegate { get; set; }

        static Control()
        {
            TranslationDelegate = (s) => s;
        }
    }
}
