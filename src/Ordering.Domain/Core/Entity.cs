namespace Ordering.Domain.Core
{
    public abstract class Entity: Validation
    {
        public long Id { get; protected set; }
    }
}
