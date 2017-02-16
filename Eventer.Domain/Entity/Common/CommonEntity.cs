namespace Eventer.Domain.Entity.Common
{
    public abstract class CommonEntity<T> : EntityBase
    {
        public T Id { get; set; }
    }
}