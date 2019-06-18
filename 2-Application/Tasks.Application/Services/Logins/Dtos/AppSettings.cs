using System;

namespace Tasks.Application.Services.Logins.Dtos
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public Guid PortalId { get; set; }
    }
}