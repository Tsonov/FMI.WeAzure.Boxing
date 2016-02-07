using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Database
{
    public class Prediction
    {
        [Key]
        public int Id { get; set; }
        
        public virtual User MadeBy { get; set; }

        public virtual BoxingMatch MadeFor { get; set; }

        public virtual Boxer PredictedWinner { get; set; }

        public virtual PredictionResult PredictionResult { get; set; }
    }
}
