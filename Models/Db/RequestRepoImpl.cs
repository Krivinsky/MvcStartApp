using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace MvcStartApp.Models.Db
{
    public class RequestRepoImpl : IRequestRepository
    {
        private readonly BlogContext _blogContext;

        public RequestRepoImpl(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task AddRequest(Request request)
        {
            
            // Добавление запроса в БД
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
