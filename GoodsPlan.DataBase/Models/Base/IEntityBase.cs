namespace GoodsPlan.DataBase.Models.Base
{
    public interface IEntityBase<T>
    {
        T Id { get; set; }
    }
}
