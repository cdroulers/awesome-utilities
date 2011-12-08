using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Reflection;

namespace System.Web.Mvc
{
    /// <summary>
    ///     A class to verify access to a specific controller and action
    /// </summary>
    public class Access
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Access"/> class.
        ///     protected so we can't initialize the class
        /// </summary>
        private Access()
        {
        }

        static Access()
        {
            IsAuthenticatedDelegate = () => HttpContext.Current.User.Identity.IsAuthenticated;
            GetRolesForUserDelegate = () => Roles.GetRolesForUser();
        }

        /// <summary>
        ///     Instance.
        /// </summary>
        public static readonly Access Is = new Access();

        /// <summary>
        /// Gets or sets the application assembly to use to get controllers and areas and stuff.
        /// </summary>
        /// <value>The application assembly.</value>
        public static Assembly ApplicationAssembly { get; set; }

        /// <summary>
        ///     The delegate to get if the request is authenticated. Mostly used for testing.
        /// </summary>
        public static Func<bool> IsAuthenticatedDelegate { get; set; }

        /// <summary>
        ///     The delegate to get the roles for the current user
        /// </summary>
        public static Func<string[]> GetRolesForUserDelegate { get; set; }

        /// <summary>
        ///     Gets or sets the MVC controllers namespace, to override using the assembly.
        /// </summary>
        /// <value>The MVC controllers namespace.</value>
        public static string MvcControllersNamespace { get; set; }

        /// <summary>
        ///     Gets or sets the MVC areas namespace, to override using the assembly.
        /// </summary>
        /// <value>The MVC areas namespace.</value>
        public static string MvcAreasNamespace { get; set; }

        /// <summary>
        /// Returns true if the current user has the rights to this action
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <returns>
        /// 	<c>true</c> if the specified controller is authorized; otherwise, <c>false</c>.
        /// </returns>
        public bool Authorized(string controller, string actionName)
        {
            return this.Authorized(controller, actionName, (string)null);
        }

        /// <summary>
        /// Returns true if the current user has the rights to this action
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="area">The area.</param>
        /// <returns>
        /// 	<c>true</c> if the specified controller is authorized; otherwise, <c>false</c>.
        /// </returns>
        public bool Authorized(string controller, string actionName, string area)
        {
            bool isAuthenticated;
            try
            {
                isAuthenticated = IsAuthenticatedDelegate();
            }
            catch
            {
                isAuthenticated = false;
            }
            return this.Authorized(controller, actionName, area, isAuthenticated);
        }

        /// <summary>
        /// Returns true if the roles specified are in the action specified
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="isAuthenticated">if set to <c>true</c> [is authenticated].</param>
        /// <returns>
        /// 	<c>true</c> if the specified controller is authorized; otherwise, <c>false</c>.
        /// </returns>
        public bool Authorized(string controller, string actionName, bool isAuthenticated)
        {
            return this.Authorized(controller, actionName, (string)null, isAuthenticated);
        }

        /// <summary>
        /// Returns true if the roles specified are in the action specified
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="area">The area.</param>
        /// <param name="isAuthenticated">if set to <c>true</c> [is authenticated].</param>
        /// <returns>
        /// 	<c>true</c> if the specified controller is authorized; otherwise, <c>false</c>.
        /// </returns>
        public bool Authorized(string controller, string actionName, string area, bool isAuthenticated)
        {
            return this.Authorized(controller, actionName, area, isAuthenticated, GetRolesForUserDelegate());
        }

        /// <summary>
        /// Returns true if the specified roles have access to the specified controller action in the specified assembly
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="isAuthenticated">if set to <c>true</c> [is authenticated].</param>
        /// <param name="roles">The roles.</param>
        /// <returns>
        /// 	<c>true</c> if the specified controller is authorized; otherwise, <c>false</c>.
        /// </returns>
        public bool Authorized(string controller, string actionName, bool isAuthenticated, string[] roles)
        {
            return this.Authorized(controller, actionName, (string)null, isAuthenticated, roles);
        }

        /// <summary>
        /// Returns true if the specified roles have access to the specified controller action in the specified assembly
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="area">The area.</param>
        /// <param name="isAuthenticated">if set to <c>true</c> [is authenticated].</param>
        /// <param name="roles">The roles.</param>
        /// <returns>
        /// 	<c>true</c> if the specified controller is authorized; otherwise, <c>false</c>.
        /// </returns>
        public bool Authorized(string controller, string actionName, string area, bool isAuthenticated, string[] roles)
        {
            string namespaceName = null;
            if (string.IsNullOrEmpty(area))
            {
                namespaceName = string.IsNullOrEmpty(Access.MvcControllersNamespace) ? Access.ApplicationAssembly.GetName().Name + ".Controllers" : Access.MvcControllersNamespace;
            }
            else
            {
                namespaceName = string.IsNullOrEmpty(Access.MvcAreasNamespace) ? Access.ApplicationAssembly.GetName().Name + ".Areas" : Access.MvcAreasNamespace;
                namespaceName += string.Format(".{0}.Controllers", area);
            }

            string controllerName = string.Format("{0}.{1}Controller", namespaceName, controller);

            Type controllerClass = Access.ApplicationAssembly.GetType(controllerName);
            MethodInfo action = null;
            try
            {
                action = controllerClass.GetMethod(actionName);
            }
            catch (AmbiguousMatchException)
            {
            }

            if (action == null) // try to find according to ActionName attribute
            {
                var memberFilter = new MemberFilter((mi, c) =>
                {
                    var nonActionAttributes = (NonActionAttribute[])mi.GetCustomAttributes(typeof(NonActionAttribute), false);
                    if (nonActionAttributes.Length != 0)
                    {
                        return false;
                    }
                    if (mi.Name == actionName)
                    {
                        return true;
                    }
                    var attributes = (ActionNameAttribute[])mi.GetCustomAttributes(typeof(ActionNameAttribute), false);
                    return attributes.Length > 0 && attributes[0].Name == actionName;
                });
                var members = controllerClass.FindMembers(MemberTypes.All, BindingFlags.Instance | BindingFlags.Public, memberFilter, null);
                action = members.FirstOrDefault() as MethodInfo;
            }

            if (action == null)
            {
                throw new MissingMethodException(string.Format("The action {0} was not found", actionName));
            }

            AuthorizeAttribute[] controllerAttributes = (AuthorizeAttribute[])controllerClass.GetCustomAttributes(typeof(AuthorizeAttribute), true);
            List<string> controllerRoles = GetRoles(controllerAttributes);

            AuthorizeAttribute[] actionAttributes = (AuthorizeAttribute[])action.GetCustomAttributes(typeof(AuthorizeAttribute), false);
            List<string> actionRoles = GetRoles(actionAttributes);

            IEnumerable<string> authorizedRoles = controllerRoles;
            if (controllerRoles.Count() > 0 && actionRoles.Count > 0)
            {
                authorizedRoles = controllerRoles.Intersect(actionRoles);
            }
            else if (actionRoles.Count > 0)
            {
                authorizedRoles = actionRoles;
            }

            return (controllerAttributes.Length == 0 && actionAttributes.Length == 0) ||
                   (authorizedRoles.Count() == 0 && isAuthenticated) ||
                   authorizedRoles.Any(r => roles.Contains(r, StringComparer.InvariantCultureIgnoreCase));
        }

        private static List<string> GetRoles(AuthorizeAttribute[] attributes)
        {
            List<string> roles = new List<string>();
            foreach (AuthorizeAttribute attribute in attributes)
            {
                string[] roleParts = attribute.Roles.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                roles.AddRange(roleParts.Select(r => r.Trim()).ToArray());
            }
            return roles;
        }
    }
}
