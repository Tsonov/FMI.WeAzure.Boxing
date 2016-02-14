using FMI.WeAzure.Boxing.Contracts.Dto;
using FMI.WeAzure.Boxing.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Contracts.Requests.Matches
{
    public class CreateMatchRequest : IRequest<Match>
    {
        public int FirstBoxer { get; set; }

        public int SecondBoxer { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

    }
}
