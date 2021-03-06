﻿using IDI.Central.Common;
using IDI.Central.Core;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Administration.Commands;
using IDI.Central.Domain.Modules.Administration.Queries;
using IDI.Central.Models.Administration;
using IDI.Central.Models.Administration.Inputs;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Messaging;
using IDI.Core.Localization;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/oauth"), ApplicationAuthorize]
    [Module(Configuration.Modules.OAuth)]
    public class OAuthController : Controller, IAuthorizable
    {
        private readonly ICommandBus bus;
        private readonly IQuerier querier;
        private readonly ILocalization localization;

        public OAuthController(ICommandBus bus, IQuerier querier, ILocalization localization)
        {
            this.bus = bus;
            this.querier = querier;
            this.localization = localization;
        }

        [HttpPost("login")]
        [Permission("login", PermissionType.Read)]
        public Result Get([FromBody]OAuthInput input)
        {
            var tokenResult = querier.Execute<QueryOAuthTokenCondition, OAuthTokenModel>(new QueryOAuthTokenCondition { Code = input.Code, RedirectUri = input.RedirectUri, State = input.State, Type = input.Type });

            if (tokenResult.Status != ResultStatus.Success)
                return Result.Fail(tokenResult.Message);

            var userResult = querier.Execute<QueryOAuthUserCondition, IOAuthUserModel>(new QueryOAuthUserCondition { Type = input.Type, AccessToken = tokenResult.Data.AccessToken });

            if (userResult.Status != ResultStatus.Success)
                return Result.Fail(localization.Get(Resources.Key.Command.RetrieveUserInfoFail));

            var cmd = new OAuthUserCreationCommand
            {
                Email = userResult.Data.Email,
                Login = userResult.Data.Login,
                Name = userResult.Data.Name,
                Group = ValidationGroup.Create,
                Mode = CommandMode.Create,
                Type = userResult.Data.Type
            };

            return bus.Send(cmd);
        }
    }
}
