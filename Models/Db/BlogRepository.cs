using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MvcStartApp.Models.Db
{
    public class BlogRepository : IBlogRepository
    {
        // ссылка на контекст
        private readonly BlogContext _blogContext;

        // Метод-конструктор для инициализации
        public BlogRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task AddUser(User user)
        {
            // Добавление пользователя
            var entry = _blogContext.Entry(user);
            if (entry.State == EntityState.Detached)    
            {
                await _blogContext.Users.AddAsync(user);

                // Сохранение изенений
                await _blogContext.SaveChangesAsync();
            }
        }
    }
}
