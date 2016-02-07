﻿using FMI.WeAzure.Boxing.Contracts.Dto;
using FMI.WeAzure.Boxing.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Contracts.Requests.Matches
{
    public class GetAllMatchesRequest : IRequest<IEnumerable<Match>>, IPaged
    {
        public int Skip { get; set; }

        public int Take { get; set; }

        public SortOrder SortOrder { get; set; }
    }
}