using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace MvcStartApp.Models.Db
{
    public class IRequestRepoImpl : IRequestRepository
    {
        private readonly BlogContext _blogContext;

        public IRequestRepoImpl(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task AddRequest(Request request)
        {
            request.Date = DateTime.Now;
            request.Id = Guid.NewGuid();

            // Добавление пользователя
            var entry = _blogContext.Entry(request);
            if (entry.State == EntityState.Detached)
            {
                await _blogContext.Requests.AddAsync(request);

                // Сохранение изенений
                await _blogContext.SaveChangesAsync();
            }
        }
    }
}
