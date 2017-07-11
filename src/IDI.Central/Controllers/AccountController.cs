﻿using IDI.Central.Domain.Modules.Identity.Commands;
using IDI.Central.Models.Identity;
using IDI.Central.Common;
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
        public Result Post([FromBody]RegisterForm form)
        {
            return ServiceLocator.CommandBus.Send(new RegisterCommand(form.UserName, form.Password, form.Confirm));
        }
    }
}
