using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Contracts.Dto
{
    public sealed class Match
    {
        public string FirstBoxer { get; set; }

        public string SecondBoxer { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }
    }
}
