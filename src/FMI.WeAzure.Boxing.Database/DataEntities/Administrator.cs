using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Database
{
    public class Administrator
    {
        [Key]
        public string Key { get; set; }

        public bool Active { get; set; }
    }
}
