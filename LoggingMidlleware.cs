using Microsoft.AspNetCore.Http;
using MvcStartApp.Models;
using MvcStartApp.Models.Db;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CoreStartApp
{
    public class LoggingMidlleware
    {
        private readonly RequestDelegate _requestDelegate;
        private IUserInfoRepository _repo; 

        /// <summary>
        ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
        /// </summary>
        public LoggingMidlleware(RequestDelegate requestDelegate, IUserInfoRepository repo)
        {
            _requestDelegate = requestDelegate;
            _repo = repo; 
        }

        /// <summary>
        ///  Необходимо реализовать метод Invoke  или InvokeAsync
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            string userAgent = context.Request.Headers["User-Agent"][0];
            var newUserInfo = new UserInfo()
            {
                Id = Guid.NewGuid(),
                DateTime = DateTime.Now,
                UserAgent = userAgent
            };

            await _repo.Add(newUserInfo);
            
            LogConsole(context);
            await LogFile(context);

            // Передача запроса далее по конвейеру
            await _requestDelegate.Invoke(context);
        }

        public void LogConsole(HttpContext context)
        {
            // Для логирования данных о запросе используем свойства объекта HttpContext
            Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
        }

        public async Task LogFile(HttpContext context)
        {
            // Строка для публикации в лог
            string logMessage = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}";

            // Путь до лога (опять-таки, используем свойства IWebHostEnvironment)
            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "Requests.txt");

            await File.AppendAllTextAsync(logFilePath, logMessage);
        }
    }
}
