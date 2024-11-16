using AutoMapper;
using DynamicBooking.Doomain;
using DynamicBooking.Infrastructure.Abstractions;
using MediatR;

namespace DynamicBooking.UseCases.EditForm;

public class EditFormCommandHandler : IRequestHandler<EditFormCommand, Unit>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    public EditFormCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }


    public async Task<Unit> Handle(EditFormCommand request, CancellationToken cancellationToken)
    {
        //var eventDto = request.eventDto;

        //var e = mapper.Map<Event>(eventDto);

        //await appDbContext.Events.AddAsync(e);

        //await appDbContext.SaveChangesAsync();

        return Unit.Value;
    }
}
