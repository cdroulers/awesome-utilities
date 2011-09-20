using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Awesome.Utilities.Test.WebTestApp.Controllers;
using NUnit.Framework;

namespace Awesome.Utilities.Test.Web.Mvc
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenAccess
    {
        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            Access.ApplicationAssembly = typeof(TestController).Assembly;
            Access.MvcControllersNamespace = typeof(TestController).Namespace;
        }

        [SetUp]
        public void SetUp()
        {
            Access.IsAuthenticatedDelegate = () => false;
            Access.GetRolesForUserDelegate = () => new string[0];
        }

        [Test]
        public void When_No_Attributes_Then_Allowed()
        {
            Assert.IsTrue(Access.Is.Authorized("TestEmpty", "Index"));
        }

        [Test]
        public void When_Empty_Attribute_And_Not_Authenticated_Then_Not_Allowed()
        {
            Assert.IsFalse(Access.Is.Authorized("Test", "IndexNoRoles"));
        }

        [Test]
        public void When_Empty_Attribute_And_Authenticated_Then_Allowed()
        {
            Access.IsAuthenticatedDelegate = () => true;
            Assert.IsTrue(Access.Is.Authorized("Test", "IndexNoRoles"));
        }

        [Test]
        public void When_Attribute_With_Roles_And_Not_Authenticated_Then_Not_Allowed()
        {
            Assert.IsFalse(Access.Is.Authorized("Test", "IndexRole1"));
        }

        [Test]
        public void When_Attribute_With_Roles_And_Authenticated_Then_Not_Allowed()
        {
            Access.IsAuthenticatedDelegate = () => true;
            Assert.IsFalse(Access.Is.Authorized("Test", "IndexRole1"));
        }

        [Test]
        public void When_Attribute_With_Roles_And_Authenticated_With_Proper_Roles_Then_Allowed()
        {
            Access.IsAuthenticatedDelegate = () => true;
            Access.GetRolesForUserDelegate = () => new string[] { "Role1" };
            Assert.IsTrue(Access.Is.Authorized("Test", "IndexRole1"));
        }

        [Test]
        public void When_ActionOnPost_With_Attribute_With_Roles_And_Authenticated_With_Proper_Roles_Then_Allowed()
        {
            Access.IsAuthenticatedDelegate = () => true;
            Access.GetRolesForUserDelegate = () => new string[] { "Role1" };
            Assert.IsTrue(Access.Is.Authorized("Test", "IndexRole1"));
        }
    }
}
