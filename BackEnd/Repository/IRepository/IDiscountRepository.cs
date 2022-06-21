using BackEnd.Model;

namespace BackEnd.Repository.IRepository
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        void Update(Discount discount);
    }
}
