namespace OfficeReview.Domain.Events.Enum;
public class EventType : Enumeration
{
    public static EventType UserAdded = new(1, nameof(UserAdded));
    public static EventType UserUpdated = new(2, nameof(UserUpdated));
    public EventType(int id, string name) : base(id, name)
    {
    }
}

