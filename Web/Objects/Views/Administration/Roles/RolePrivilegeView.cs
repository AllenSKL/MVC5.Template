﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Template.Objects
{
    public class RolePrivilegeView : BaseView
    {
        [Required]
        public String RoleId { get; set; }

        [Required]
        public String PrivilegeId { get; set; }

        public RoleView Role { get; set; }
        public PrivilegeView Privilege { get; set; }
    }
}
