﻿using IDI.Central.Common;
using IDI.Central.Domain.Modules.Administration.Commands;
using IDI.Central.Models.Administration;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.APIs
{
    [Route("api/account")]
    public class AccountController : ApiController
    {
        [HttpPost, AllowAnonymous]
        public Result Post([FromBody]UserRegistrationInput input)
        {
            return ServiceLocator.CommandBus.Send(new UserRegistrationCommand(input.UserName, input.Password, input.Confirm));
        }
    }
}
