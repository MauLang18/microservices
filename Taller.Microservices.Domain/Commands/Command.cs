using Taller.Microservices.Domain.Events;

namespace Taller.Microservices.Domain.Commands;

public abstract class Command : Message
{
    public DateTime Timestamp { get; protected set; }

    protected Command()
    {
        Timestamp = DateTime.Now;
    }
}