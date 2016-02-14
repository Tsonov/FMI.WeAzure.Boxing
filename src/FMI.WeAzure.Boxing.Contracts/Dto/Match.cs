using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Contracts.Dto
{
    public sealed class Match
    {
        public Match()
        {
            this.Predictions = new List<Prediction>();
        }

        public int Id { get; set; }

        public int FirstBoxer { get; set; }

        public int SecondBoxer { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public IEnumerable<Prediction> Predictions { get; set; }
    }
}
