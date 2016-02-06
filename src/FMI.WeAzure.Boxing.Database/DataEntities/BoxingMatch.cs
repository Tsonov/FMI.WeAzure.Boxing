﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Database
{
    public class BoxingMatch
    {
        public BoxingMatch()
        {
            this.Predictions = new HashSet<Prediction>();
        }

        [Key]
        public int Id { get; set; }

        public string Address { get; set; }

        public DateTime Time { get; set; }

        public string Description { get; set; }

        public virtual Boxer FirstBoxer { get; set; }

        public virtual Boxer SecondBoxer { get; set; }

        public virtual ICollection<Prediction> Predictions { get; set; }
    }
}