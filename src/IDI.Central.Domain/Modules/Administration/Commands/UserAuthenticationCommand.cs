﻿using System;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Localization;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class UserAuthenticationCommand : Command
    {
        [RequiredField]
        public string UserName { get; private set; }

        [RequiredField]
        public string Password { get; private set; }

        public UserAuthenticationCommand(string username, string password)
        {
            this.UserName = username;
            this.Password = password;
        }
    }

    public class UserAuthenticationCommandHandler : ICommandHandler<UserAuthenticationCommand>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        [Injection]
        public IRepository<User> Users { get; set; }

        public Result Execute(UserAuthenticationCommand command)
        {
            var user = this.Users.Find(u => u.UserName == command.UserName);

            if (user == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.InvalidUsernameOrPassword));

            if (!user.Active)
                return Result.Fail(Localization.Get(Resources.Key.Command.AccountInactive));

            if (user.IsLocked)
                return Result.Fail(Localization.Get(Resources.Key.Command.AccountLocked));

            if (!user.SecretKey().Verify(command.Password))
                return Result.Fail(Localization.Get(Resources.Key.Command.InvalidUsernameOrPassword));

            user.LatestLoginTime = DateTime.Now;

            this.Users.Update(user);
            this.Users.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.AuthSuccess));
        }
    }
}
