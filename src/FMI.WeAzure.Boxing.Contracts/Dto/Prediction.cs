using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Contracts.Dto
{
    public sealed class Prediction
    {
        public int Id { get; set; }

        public string User { get; set; }

        public PredictionKind Predicted { get; set; }

        public enum PredictionKind
        {
            FirstBoxerWins = 1,
            SecondBoxerWins = 2
        }
    }

}
