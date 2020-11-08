﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Managers
{
    public interface IUserManager: IBaseManager<User>
    {
        public string Login()

    }
}
