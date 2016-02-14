using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Contracts.Requests.Boxers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMI.WeAzure.Boxing.Contracts;
using FMI.WeAzure.Boxing.Database;
using Dto = FMI.WeAzure.Boxing.Contracts.Dto;

namespace FMI.WeAzure.Boxing.Business.Handlers.Boxers
{
    public class CreateBoxerHandler : BaseHandler, IRequestHandler<CreateBoxerRequest, Dto.Boxer>
    {
        public CreateBoxerHandler(BoxingDbContext context) : base(context)
        {

        }

        public async Task<Dto.Boxer> HandleAsync(CreateBoxerRequest request)
        {
            var entity = new Boxer()
            {
                Name = request.Name,
                Biography = request.Biography
            };

            Context.Boxers.Add(entity);
            await Context.SaveChangesAsync();

            return new Dto.Boxer()
            {
                Id = entity.Id,
                Name = entity.Name,
                Biography = entity.Biography
            };
        }
    }
}
