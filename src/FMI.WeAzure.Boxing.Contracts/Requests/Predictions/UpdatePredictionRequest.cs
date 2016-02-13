using FMI.WeAzure.Boxing.Contracts.Dto;
using FMI.WeAzure.Boxing.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Contracts.Requests.Predictions
{
    public class UpdatePredictionRequest : IRequest
    {
        public int PredictionId { get; set; }

        public PredictionKind UserPrediction { get; set; }
    }


    public enum PredictionKind
    {
        FirstBoxerWins,
        SecondBoxerWins
    }
}
