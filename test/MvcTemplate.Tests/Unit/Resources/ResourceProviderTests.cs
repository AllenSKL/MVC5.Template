﻿using MvcTemplate.Objects;
using MvcTemplate.Resources;
using MvcTemplate.Resources.Shared;
using MvcTemplate.Tests.Objects;
using System;
using System.Web.Routing;
using Xunit;
using Xunit.Extensions;

namespace MvcTemplate.Tests.Unit.Resources
{
    public class ResourceProviderTests
    {
        #region Static method: GetDatalistTitle(String datalist)

        [Fact]
        public void GetDatalistTitle_IsCaseInsensitive()
        {
            String expected = MvcTemplate.Resources.Datalist.Titles.Role;
            String actual = ResourceProvider.GetDatalistTitle("role");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDatalistTitle_NotFound_ReturnsNull()
        {
            Assert.Null(ResourceProvider.GetDatalistTitle("Test"));
        }

        [Fact]
        public void GetDatalistTitle_NullDatalist_ReturnsNull()
        {
            Assert.Null(ResourceProvider.GetDatalistTitle(null));
        }

        #endregion

        #region Static method: GetContentTitle(RouteValueDictionary values)

        [Fact]
        public void GetContentTitle_IsCaseInsensitive()
        {
            RouteValueDictionary values = new RouteValueDictionary();
            values["area"] = "administration";
            values["controller"] = "roles";
            values["action"] = "details";

            String expected = ContentTitles.AdministrationRolesDetails;
            String actual = ResourceProvider.GetContentTitle(values);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GetContentTitle_WithoutArea(String area)
        {
            RouteValueDictionary values = new RouteValueDictionary();
            values["controller"] = "profile";
            values["action"] = "edit";
            values["area"] = area;

            String actual = ResourceProvider.GetContentTitle(values);
            String expected = ContentTitles.ProfileEdit;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetContentTitle_NotFound_ReturnsNull()
        {
            Assert.Null(ResourceProvider.GetContentTitle(new RouteValueDictionary()));
        }

        #endregion

        #region Static method: GetSiteMapTitle(String area, String controller, String action)

        [Fact]
        public void GetSiteMapTitle_IsCaseInsensitive()
        {
            String actual = ResourceProvider.GetSiteMapTitle("administration", "roles", "index");
            String expected = MvcTemplate.Resources.SiteMap.Titles.AdministrationRolesIndex;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetSiteMapTitle_WithoutControllerAndAction()
        {
            String actual = ResourceProvider.GetSiteMapTitle("administration", null, null);
            String expected = MvcTemplate.Resources.SiteMap.Titles.Administration;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetSiteMapTitle_NotFound_ReturnsNull()
        {
            Assert.Null(ResourceProvider.GetSiteMapTitle("Test", "Test", "Test"));
        }

        #endregion

        #region Static method: GetPermissionAreaTitle(String area)

        [Fact]
        public void GetPermissionAreaTitle_IsCaseInsensitive()
        {
            String expected = MvcTemplate.Resources.Permission.Area.Titles.Administration;
            String actual = ResourceProvider.GetPermissionAreaTitle("administration");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetPermissionAreaTitle_NotFound_ReturnsNull()
        {
            Assert.Null(ResourceProvider.GetPermissionAreaTitle("Test"));
        }

        [Fact]
        public void GetPermissionAreaTitle_NullArea_ReturnsNull()
        {
            Assert.Null(ResourceProvider.GetPermissionAreaTitle(null));
        }

        #endregion

        #region Static method: GetPermissionControllerTitle(String area, String controller)

        [Fact]
        public void GetPermissionControllerTitle_ReturnsTitle()
        {
            String expected = MvcTemplate.Resources.Permission.Controller.Titles.AdministrationRoles;
            String actual = ResourceProvider.GetPermissionControllerTitle("Administration", "Roles");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetPermissionControllerTitle_NotFound_ReturnsNull()
        {
            Assert.Null(ResourceProvider.GetPermissionControllerTitle("", ""));
        }

        #endregion

        #region Static method: GetPermissionActionTitle(String area, String controller, String action)

        [Fact]
        public void GetPermissionActionTitle_ReturnsTitle()
        {
            String actual = ResourceProvider.GetPermissionActionTitle("administration", "accounts", "index");
            String expected = MvcTemplate.Resources.Permission.Action.Titles.AdministrationAccountsIndex;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetPermissionActionTitle_NotFound_ReturnsNull()
        {
            Assert.Null(ResourceProvider.GetPermissionActionTitle("", "", ""));
        }

        #endregion

        #region Static method: GetPropertyTitle<TModel, TProperty>(Expression<Func<TModel, TProperty>> property)

        [Fact]
        public void GetPropertyTitle_NotMemberExpression_ReturnNull()
        {
            Assert.Null(ResourceProvider.GetPropertyTitle<TestView, String>(view => view.ToString()));
        }

        [Fact]
        public void GetPropertyTitle_FromExpression()
        {
            String actual = ResourceProvider.GetPropertyTitle<AccountView, String>(account => account.Username);
            String expected = MvcTemplate.Resources.Views.Administration.Accounts.AccountView.Titles.Username;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetPropertyTitle_FromExpressionRelation()
        {
            String actual = ResourceProvider.GetPropertyTitle<AccountEditView, Int32?>(account => account.RoleId);
            String expected = MvcTemplate.Resources.Views.Administration.Roles.RoleView.Titles.Id;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetPropertyTitle_NotFoundExpression_ReturnsNull()
        {
            Assert.Null(ResourceProvider.GetPropertyTitle<AccountView, Int32>(account => account.Id));
        }

        [Fact]
        public void GetPropertyTitle_NotFoundType_ReturnsNull()
        {
            Assert.Null(ResourceProvider.GetPropertyTitle<TestView, String>(test => test.Text));
        }

        #endregion

        #region Static method: GetPropertyTitle(Type view, String property)

        [Fact]
        public void GetPropertyTitle_IsCaseInsensitive()
        {
            String expected = MvcTemplate.Resources.Views.Administration.Accounts.AccountView.Titles.Username;
            String actual = ResourceProvider.GetPropertyTitle(typeof(AccountView), "username");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetPropertyTitle_FromRelation()
        {
            String expected = MvcTemplate.Resources.Views.Administration.Accounts.AccountView.Titles.Username;
            String actual = ResourceProvider.GetPropertyTitle(typeof(RoleView), "AccountUsername");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetPropertyTitle_FromMultipleRelations()
        {
            String expected = MvcTemplate.Resources.Views.Administration.Accounts.AccountView.Titles.Username;
            String actual = ResourceProvider.GetPropertyTitle(typeof(RoleView), "AccountRoleAccountUsername");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetPropertyTitle_NotFoundProperty_ReturnsNull()
        {
            Assert.Null(ResourceProvider.GetPropertyTitle(typeof(AccountView), "Id"));
        }

        [Fact]
        public void GetPropertyTitle_NotFoundTypeProperty_ReturnsNull()
        {
            Assert.Null(ResourceProvider.GetPropertyTitle(typeof(TestView), "Title"));
        }

        [Fact]
        public void GetPropertyTitle_NullKey_ReturnsNull()
        {
            Assert.Null(ResourceProvider.GetPropertyTitle(typeof(RoleView), null));
        }

        #endregion
    }
}
