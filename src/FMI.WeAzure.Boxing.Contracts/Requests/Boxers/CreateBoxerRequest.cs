using FMI.WeAzure.Boxing.Contracts.Dto;
using FMI.WeAzure.Boxing.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Contracts.Requests.Boxers
{
    public class CreateBoxerRequest : IRequest<Boxer>
    {
        public string Name { get; set; }

        public string Biography { get; set; }
    }
}
