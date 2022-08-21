using Core.Application.Mediator.Icons;
using Core.Application.Models;
using HotChocolate.AspNetCore.Authorization;
using MediatR;

namespace Web.WebApi.Queries
{
    [ExtendObjectType("Query")]
    [Authorize]
    public class IconQuery
    {   
        public async Task<IconDto> GetIcon([Service] IMediator mediator, Guid id)
        {
            return await mediator.Send(new GetIconRequest(id));
        }

        public async Task<IQueryable<IconDto>> GetIcons([Service] IMediator mediator)
        {
            return (await mediator.Send(new GetIconsQuery())).AsQueryable();
        }


    }
}
