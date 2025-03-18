using OfficeReview.Domain.Events.Enum;

namespace OfficeReview.Domain.Events.Root;

public class EventLogs : Entity, IAggregateRoot
{
    protected EventLogs()
    {
        
    }
    public EventLogs(string description, EventType eventType)
    {
        EventId = Guid.NewGuid();
        this.Description = description;
        this.EventType = eventType;
        CreationTime = DateTime.Now;

    }
    public Guid EventId { get; private set; }
    public string Description { get; private set; }
    public EventType EventType { get; private set; }
    public DateTime CreationTime { get; private set; }
}

