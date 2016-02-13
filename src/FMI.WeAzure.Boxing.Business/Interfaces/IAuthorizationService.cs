﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Business.Interfaces
{
    public interface IAuthorizationService
    {
        Task<bool> ValidateAdminToken(string token);

        Task<bool> ValidateLoginToken(string token);
    }
}
