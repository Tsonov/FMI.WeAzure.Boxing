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
using FMI.WeAzure.Boxing.Contracts.Dto;

namespace FMI.WeAzure.Boxing.Business.Handlers.Matches
{
    public class CreateMatchHandler : BaseHandler, IRequestHandler<CreateMatchRequest, Match>
    {
        public CreateMatchHandler(BoxingDbContext context) : base(context)
        {

        }

        public async Task<Match> HandleAsync(CreateMatchRequest request)
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
                Time = request.Date,
                Active = true
            };
            Context.BoxingMatches.Add(entity);
            await Context.SaveChangesAsync();

            var match = new Match()
            {
                Id = entity.Id,
                Address = entity.Address,
                Date = entity.Time,
                FirstBoxer = entity.FirstBoxer.Id,
                SecondBoxer = entity.SecondBoxer.Id,
                Description = entity.Description,
                Predictions = Enumerable.Empty<Contracts.Dto.Prediction>().ToList()
            };

            return match;
        }
    }
}
