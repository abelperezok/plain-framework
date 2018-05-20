namespace Plain.Infrastructure.Interfaces.Domain
{
    public interface IEntityKey<TKey>
    {
        TKey ID { set; get; }
    }
}
