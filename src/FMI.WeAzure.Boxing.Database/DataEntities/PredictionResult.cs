﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Database
{
    public class PredictionResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Description { get; set; }
    }
        
    public enum PredictionResultEnum
    {
        MatchNotEnded = 1,
        Correct = 2,
        Incorrect = 3,
        MatchCanceled = 4,
        UserCanceled = 5
    }
}
