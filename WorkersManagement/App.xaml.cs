using System.Configuration;
using System.Data;
using System.Windows;

namespace WorkersManagement
{ 
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using var db = new AppDbContext();
            db.Database.EnsureCreated();

            if (!db.Users.Any())
            {
                db.Users.Add(new Models.User
                {
                    Username = "admin",
                    PasswordHash = SecurityHelper.HashPassword("admin123")
                });
                db.SaveChanges();
            }
        }
    }

}
