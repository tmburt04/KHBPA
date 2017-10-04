using KHBPA.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KHBPA.Startup))]
namespace KHBPA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        public void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //If Admin role doesn't exist, create first Admin Role and a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {
                //First we create Admin role   
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Then we create a Admin user                
                var user = new ApplicationUser();
                user.UserName = "admin@email.com"; //Use same UserName and Email for simplicity. 
                user.Email = "admin@email.com";    //Else you will need to modify the login action in the AccountController
                string userPWD = "admin";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            //For adding more roles on startup
            //// creating Creating Manager role    
            if (!roleManager.RoleExists("Member"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Member";
                roleManager.Create(role);

            }

        }
    }
}