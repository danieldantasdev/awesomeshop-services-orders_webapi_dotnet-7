using MediatR;

namespace AwesomeShop.Services.Orders.Application.Queries.Orders;

public class GetOrderByIdQueryInputModel : IRequest<GetOrderByIdQueryViewModel>
{
    public GetOrderByIdQueryInputModel(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; private set; }
}