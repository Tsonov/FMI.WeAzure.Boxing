using FMI.WeAzure.Boxing.Contracts;
using FMI.WeAzure.Boxing.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Business.Interfaces
{
    interface ICommandHandler<TRequest> : IRequestHandler<TRequest, Unit>
        where TRequest : IRequest, new()
    {
    }
}
