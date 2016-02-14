using FMI.WeAzure.Boxing.Contracts.Dto;
using FMI.WeAzure.Boxing.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Contracts.Requests.Users
{
    public class GetAllUsersRequest : IRequest<IEnumerable<User>>, IPaged
    {
        public GetAllUsersRequest()
        {
            this.Skip = 0;
            this.Take = 15;
            this.SortOrder = SortOrder.Descending;
            this.OrderByColumn = UserOrderColumn.FullName;
        }

        public int Skip { get; set; }

        public int Take { get; set; }

        public SortOrder SortOrder { get; set; }

        public UserOrderColumn OrderByColumn { get; set; }
    }
}
