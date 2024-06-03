using System;

namespace MvcStartApp.Models.Db
{
    public class UserInfo
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public string UserAgent { get; set; }
    }
}
