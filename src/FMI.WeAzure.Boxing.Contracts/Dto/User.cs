﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Contracts.Dto
{
    public sealed class User
    {
        public string UserName { get; set; }

        public string FullName { get; set; }

        public decimal Rating { get; set; }
    }
}
