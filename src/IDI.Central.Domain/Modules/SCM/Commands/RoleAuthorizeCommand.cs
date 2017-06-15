﻿using System;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Central.Domain.Modules.SCM.Commands
{
    public class RoleAuthorizeCommand : Command
    {
        [RequiredField("角色")]
        public string RoleName { get; private set; }

        public Guid[] Privileges { get; private set; }

        public RoleAuthorizeCommand(string rolename, Guid[] privileges)
        {
            this.RoleName = rolename;
            this.Privileges = privileges;
        }
    }
}
