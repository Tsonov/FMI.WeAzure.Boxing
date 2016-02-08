using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Business.Interfaces
{
    public interface IPasswordService
    {
        string CreateHash(string password);

        bool ValidatePassword(string password, string correctHash);
    }
}
