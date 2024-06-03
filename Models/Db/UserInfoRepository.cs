using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MvcStartApp.Models.Db
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly BlogContext _blogContext;

        public UserInfoRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }
        
        public async Task Add(UserInfo userInfo)
        {
            var entry = _blogContext.Entry(userInfo);

            if (entry.State == EntityState.Detached)
            {
                await _blogContext.UserInfos.AddAsync(userInfo);

                // Сохранение изенений
                await _blogContext.SaveChangesAsync();
            }
        }
    }
}
