using AwesomeShop.Services.Orders.Core.Events;

namespace AwesomeShop.Services.Orders.Core.Entities;

public class AggregateRoot : IEntityBase
{
    public Guid Id { get; protected set; }
    private List<IDomainEvents> _domainEventsList = new List<IDomainEvents>();
    public IEnumerable<IDomainEvents> Events => _domainEventsList;

    protected void AddEvent(IDomainEvents domainEvents)
    {
        if (_domainEventsList == null)
        {
            _domainEventsList = new List<IDomainEvents>();
        }

        _domainEventsList.Add(domainEvents);
    }
}