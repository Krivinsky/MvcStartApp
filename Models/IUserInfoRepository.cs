using MvcStartApp.Models.Db;
using System.Threading.Tasks;

namespace MvcStartApp.Models
{
    public interface IUserInfoRepository
    {
        Task Add(UserInfo userInfo);
    }
}
