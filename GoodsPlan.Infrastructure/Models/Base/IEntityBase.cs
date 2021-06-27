namespace GoodsPlan.Infrastructure.Models.Base
{
    public interface IEntityBase<T>
    {
        T Id { get; set; }
    }
}
