﻿using IDI.Central.Core;
using IDI.Central.Domain.Modules.Administration.Queries;
using IDI.Central.Models.Administration;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/user/profile"), ApplicationAuthorize]
    public class UserProfileController : Controller
    {
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public UserProfileController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
        }

        // GET api/user/profile/{username}
        [HttpGet("{username}")]
        public Result<MyProfile> Get(string username)
        {
            var condition = new QueryMyProfileCondition { UserName = username };

            return queryProcessor.Execute<QueryMyProfileCondition, MyProfile>(condition);
        }
    }
}
