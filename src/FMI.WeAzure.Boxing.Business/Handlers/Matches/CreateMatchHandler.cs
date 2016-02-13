using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Contracts.Requests.Matches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMI.WeAzure.Boxing.Contracts;
using FMI.WeAzure.Boxing.Database;
using FMI.WeAzure.Boxing.Business.Exceptions;

namespace FMI.WeAzure.Boxing.Business.Handlers.Matches
{
    public class CreateMatchHandler : BaseHandler, ICommandHandler<CreateMatchRequest>
    {
        public CreateMatchHandler(BoxingDbContext context) : base(context)
        {

        }

        public async Task<Unit> HandleAsync(CreateMatchRequest request)
        {
            var firstBoxer = await Context.Boxers.FindAsync(request.FirstBoxer);
            if (firstBoxer == null)
            {
                throw new EntityDoesNotExistException("Provided boxer does not exist");
            }
            var secondBoxer = await Context.Boxers.FindAsync(request.SecondBoxer);
            if (secondBoxer == null)
            {
                throw new EntityDoesNotExistException("Provided boxer does not exist");
            }

            var entity = new BoxingMatch()
            {
                Address = request.Address,
                FirstBoxer = firstBoxer,
                SecondBoxer = secondBoxer,
                Description = request.Description,
                Time = request.Date
            };
            Context.BoxingMatches.Add(entity);
            await Context.SaveChangesAsync();

            return Unit.Instance;
        }
    }
}
