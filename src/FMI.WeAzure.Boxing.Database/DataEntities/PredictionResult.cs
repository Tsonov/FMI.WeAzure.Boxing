using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Database
{
    public class PredictionResultEntity
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }
    }
        
    public enum PredictionResult
    {
        Correct,
        Incorrect,
        Canceled
    }
}
