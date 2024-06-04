using System.Threading.Tasks;

namespace MvcStartApp.Models.Db
{
    public interface IRequestRepository
    {
        Task AddRequest(Request request);
    }
}
