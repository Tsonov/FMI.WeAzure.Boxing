﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Contracts.Dto
{
    public sealed class Login
    {
        public int Id { get; set; }

        public string AuthToken { get; set; }
    }
}
