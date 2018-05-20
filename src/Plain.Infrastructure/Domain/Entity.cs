using Plain.Infrastructure.Interfaces.Domain;
using System;

namespace Plain.Infrastructure.Domain
{
    [Serializable]
    public abstract class Entity<TId> : IEntityKey<TId>
    {
        public virtual TId ID { get; set; }
    }


    [Serializable]
    public abstract class Entity : Entity<int>
    {

    }
}
