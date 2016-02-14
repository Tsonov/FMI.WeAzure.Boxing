using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Database
{
    public class User
    {
        public User()
        {
            Predictions = new HashSet<Prediction>();
            Logins = new HashSet<Login>();
        }

        [Key]
        public string Username { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public virtual ICollection<Prediction> Predictions { get; set; }

        public virtual ICollection<Login> Logins { get; set; }

        public decimal CalculateRating()
        {
            int successId = (int)PredictionResultEnum.Correct;
            var correct = Predictions.Where(p => p.MadeFor.IsClosed && p.PredictionResult.Id == successId).Count();
            var all = Predictions.Where(p => p.MadeFor.IsClosed).Count();
            System.Diagnostics.Debug.Assert(all >= correct);

            return all == 0 ? 0 : correct / all;
        }
    }
}
