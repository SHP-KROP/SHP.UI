using DAL;
using System.Threading.Tasks;

namespace OnlineShopAPI.Services.Interfaces
{
    public interface ISeeder
    {
        Task Seed();
    }
}
