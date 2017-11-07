using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using KHBPA.Models;
using KHBPA.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DeveloperUniversity.Controllers
{
    public class UserManagementController : Controller
    {
        readonly ApplicationDbContext _db = new ApplicationDbContext();

        [Authorize(Roles = "Admin, Member")]
        public ActionResult Index()
        {
            var userName = User.Identity.Name;
            var users = _db.Users.Where(u => u.Email == userName);

            if (User.IsInRole("Admin"))
            {
                users = _db.Users;
            }

            
            var model = new List<SelectUserRolesViewModel>();
            foreach (var user in users)
            {
                var u = new SelectUserRolesViewModel(user);
                u.Id = user.Id;
                model.Add(u);
            }
            return View(model);
        }


        [Authorize(Roles = "Admin, Member")]
        public ActionResult Edit(string id)
        {
            var Db = new ApplicationDbContext();
            var user = Db.Users.FirstOrDefault(u => u.Id == id);
            var roleList = new List<SelectRoleEditorViewModel>();

            var allRoles = _db.Roles;

            foreach (var role in allRoles)
            {
                var rvm = new SelectRoleEditorViewModel()
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                roleList.Add(rvm);
            }
            ViewBag.Name = new SelectList(_db.Roles.ToList(), "Name", "Name");
            var model = new EditUserViewModel(user);
            var member = _db.Members.FirstOrDefault(m => m.Email == model.UserName);
            model.Roles = roleList;
            model.OriginalUserName = user.UserName;
            model.FirstName = member.FirstName;
            model.LastName = member.LastName;
            model.Affiliation = member.Affiliation;
            model.ManagingPartner = member.ManagingPartner;
            model.Address = member.Address;
            model.City = member.City;
            model.State = member.State;
            model.ZipCode = member.ZipCode;
            model.PhoneNumber = member.PhoneNumber;
            model.IsOwner = member.IsOwner;
            model.IsTrainer = member.IsTrainer;
            model.IsOwnerAndTrainer = member.IsOwnerAndTrainer;
            model.LicenseNumber = member.LicenseNumber; return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "Admin, Member")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var OriginalUserName = model.OriginalUserName;
                var user = _db.Users.FirstOrDefault(u => u.UserName == model.OriginalUserName);
                var member = _db.Members.FirstOrDefault(m => m.Email == model.OriginalUserName);
                var dbRole = _db.Roles?.FirstOrDefault(r => r.Name == model.RoleName);

                if (dbRole != null && user != null && member != null)
                {
                    var _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    if(User.IsInRole("Admin"))
                    { 
                    if (dbRole.Name == "Member")
                    {
                        _userManager.RemoveFromRole(user.Id, "Admin");
                    }
                    else if (dbRole.Name == "Admin")
                    {
                        _userManager.RemoveFromRole(user.Id, "Member");
                    }

                        // _userManager.RemoveFromRole(user.Id, user.Roles.ToString());
                        
                    _userManager.AddToRole(user.Id, dbRole.Name);
                    }

                    user.UserName = model.UserName;
                    
                    //Update the userName
                    member.Email = model.UserName;
                    member.FirstName = model.FirstName;
                    member.LastName = model.LastName;
                    member.Affiliation = model.Affiliation;
                    member.ManagingPartner = model.ManagingPartner;
                    member.Address = model.Address;
                    member.City = model.City;
                    member.State = model.State;
                    member.ZipCode = model.ZipCode;
                    member.PhoneNumber = model.PhoneNumber;
                    member.IsOwner = model.IsOwner;
                    member.IsTrainer = model.IsTrainer;
                    member.IsOwnerAndTrainer = model.IsOwnerAndTrainer;
                    member.LicenseNumber = model.LicenseNumber;
                }
               
                //user.Email = model.Email;

                _db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        //[Authorize(Roles = "Admin")]
        //public ActionResult Delete(string id = null)
        //{
        //    var Db = new ApplicationDbContext();
        //    var user = Db.Users.First(u => u.UserName == id);
        //    var model = new EditUserViewModel(user);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return System.Web.UI.WebControls.View(model);
        //}


        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    var Db = new ApplicationDbContext();
        //    var user = Db.Users.First(u => u.UserName == id);
        //    Db.Users.Remove(user);
        //    Db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


        //[Authorize(Roles = "Admin")]
        //public ActionResult UserRoles(string id)
        //{
        //    var Db = new ApplicationDbContext();
        //    var user = Db.Users.First(u => u.UserName == id);
        //    var model = new SelectUserRolesViewModel(user);
        //    return System.Web.UI.WebControls.View(model);
        //}


        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        //[ValidateAntiForgeryToken]
        //public ActionResult UserRoles(SelectUserRolesViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var idManager = new IdentityManager();
        //        var Db = new ApplicationDbContext();
        //        var user = Db.Users.First(u => u.UserName == model.UserName);
        //        idManager.ClearUserRoles(user.Id);
        //        foreach (var role in model.Roles)
        //        {
        //            if (role.Selected)
        //            {
        //                idManager.AddUserToRole(user.Id, role.RoleName);
        //            }
        //        }
        //        return RedirectToAction("index");
        //    }
        //    return View();
        //}
    }
}