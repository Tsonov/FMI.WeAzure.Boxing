using FMI.WeAzure.Boxing.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMI.WeAzure.Boxing.Contracts;
using FMI.WeAzure.Boxing.Contracts.Requests.Matches;
using FMI.WeAzure.Boxing.Business.Exceptions;
using FMI.WeAzure.Boxing.Database;

namespace FMI.WeAzure.Boxing.Business.Handlers.Matches
{
    public class SetMatchResultHandler : BaseHandler, ICommandHandler<SetMatchResultRequest>
    {
        public SetMatchResultHandler(BoxingDbContext context) : base(context)
        {

        }

        public async Task<Unit> HandleAsync(SetMatchResultRequest request)
        {
            var match = await Context.BoxingMatches.FindAsync(request.MatchId);
            if (match == null)
            {
                throw new EntityDoesNotExistException("No such match");
            }
            if (match.FirstBoxer.Id != request.Winner && match.SecondBoxer.Id != request.Winner)
            {
                // TODO: exception types
                throw new Exception("Invalid winner id, did not participate");
            }
            match.Winner = match.FirstBoxer.Id == request.Winner ? match.FirstBoxer : match.SecondBoxer;
            await Context.SaveChangesAsync();

            return Unit.Instance;
        }
    }
}
