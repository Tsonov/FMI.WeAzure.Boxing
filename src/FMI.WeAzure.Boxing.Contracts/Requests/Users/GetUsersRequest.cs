using FMI.WeAzure.Boxing.Contracts.Dto;
using FMI.WeAzure.Boxing.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Contracts.Requests.Users
{
    public class GetUsersRequest : IRequest<IEnumerable<User>>, IPaged
    {
        public int Skip { get; set; }

        public int Take { get; set; }

        public SortOrder SortOrder { get; set; }

        public string SortColumn { get; set; }
    }
}
