﻿using IDI.Core.Common;

namespace IDI.Central.Models.Administration
{
    public class UserRegistrationInput : IInput
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Confirm { get; set; }
    }
}
