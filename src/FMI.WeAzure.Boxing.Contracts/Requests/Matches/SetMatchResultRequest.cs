using FMI.WeAzure.Boxing.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Contracts.Requests.Matches
{
    public class SetMatchResultRequest : IRequest
    {
        public int MatchId { get; set; }

        public int Winner { get; set; }
    }
}
