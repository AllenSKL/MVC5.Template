﻿using MvcTemplate.Objects;
using System;
using System.Collections.Generic;

namespace MvcTemplate.Tests
{
    public static class ObjectFactory
    {
        #region Administration

        public static Account CreateAccount(Int32 id = 0)
        {
            return new Account
            {
                Username = "Username" + id,
                Passhash = "Passhash" + id,

                Email = id + "@tests.com",

                IsLocked = true,

                RecoveryToken = "Token" + id,
                RecoveryTokenExpirationDate = DateTime.Now.AddMinutes(5),

                RoleId = id,
                Role = CreateRole(id)
            };
        }
        public static AccountView CreateAccountView(Int32 id = 0)
        {
            return new AccountView
            {
                Username = "Username" + id,
                Email = id + "@tests.com",

                IsLocked = true,

                RoleTitle = "Title" + id
            };
        }
        public static AccountEditView CreateAccountEditView(Int32 id = 0)
        {
            return new AccountEditView
            {
                Username = "Username" + id,
                Email = id + "@tests.com",

                IsLocked = true,

                RoleId = id
            };
        }
        public static AccountCreateView CreateAccountCreateView(Int32 id = 0)
        {
            return new AccountCreateView
            {
                Username = "Username" + id,
                Password = "Password" + id,

                Email = id + "@tests.com",

                RoleId = id
            };
        }

        public static AccountLoginView CreateAccountLoginView(Int32 id = 0)
        {
            return new AccountLoginView
            {
                Username = "Username" + id,
                Password = "Password" + id
            };
        }
        public static AccountResetView CreateAccountResetView(Int32 id = 0)
        {
            return new AccountResetView
            {
                Token = "Token" + id,
                NewPassword = "NewPassword" + id
            };
        }
        public static AccountRegisterView CreateAccountRegisterView(Int32 id = 0)
        {
            return new AccountRegisterView
            {
                Username = "Username" + id,
                Password = "Password" + id,

                Email = id + "@tests.com"
            };
        }
        public static AccountRecoveryView CreateAccountRecoveryView(Int32 id = 0)
        {
            return new AccountRecoveryView
            {
                Email = id + "@tests.com"
            };
        }

        public static ProfileEditView CreateProfileEditView(Int32 id = 0)
        {
            return new ProfileEditView
            {
                Email = id + "@tests.com",
                Username = "Username" + id,

                Password = "Password" + id,
                NewPassword = "NewPassword" + id

            };
        }
        public static ProfileDeleteView CreateProfileDeleteView(Int32 id = 0)
        {
            return new ProfileDeleteView
            {
                Password = "Password" + id
            };
        }

        public static Role CreateRole(Int32 id = 0)
        {
            return new Role
            {
                Title = "Title" + id,

                Permissions = new List<RolePermission>()
            };
        }
        public static RoleView CreateRoleView(Int32 id = 0)
        {
            return new RoleView
            {
                Title = "Title" + id
            };
        }

        public static Permission CreatePermission(Int32 id = 0)
        {
            return new Permission
            {
                Area = "Area" + id,
                Action = "Action" + id,
                Controller = "Controller" + id
            };
        }
        public static RolePermission CreateRolePermission(Int32 id = 0)
        {
            return new RolePermission
            {
                RoleId = id,
                Role = CreateRole(id),

                PermissionId = id,
                Permission = CreatePermission(id)
            };
        }

        #endregion
    }
}
