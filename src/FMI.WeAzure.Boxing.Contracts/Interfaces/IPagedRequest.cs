using FMI.WeAzure.Boxing.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Contracts.Interfaces
{
    public interface IPaged
    {
        int Skip { get; }

        int Take { get; }

        SortOrder SortOrder { get; }
    }
}
