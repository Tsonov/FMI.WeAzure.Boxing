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

        public int UserId { get; set; }

        public PredictionKind UserPrediction { get; set; }

        public enum PredictionKind
        {
            FirstBoxerWins,
            SecondBoxerWins
        }
    }

}
