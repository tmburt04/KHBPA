using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace KHBPA.Models.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel() { }

        // Allow Initialization with an instance of ApplicationUser:
        public EditUserViewModel(ApplicationUser user)
        {
            UserName = user.UserName;
            if (string.IsNullOrWhiteSpace(UserName))
            {
                UserName = user.Email;
            }
        }

        [Required]
        [EmailAddress]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Stable, Corpoation, Syndicate, or Farm")]
        public string Affiliation { get; set; }

        [Display(Name = "Managing Partner")]
        public string ManagingPartner { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Zip")]
        public string ZipCode { get; set; }

        [Required]
        [Phone]
        [Display (Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Owner")]
        public bool IsOwner { get; set; }

        [Display(Name = "Trainer")]
        public bool IsTrainer { get; set; }

        [Display(Name = "Owner/Trainer")]
        public bool IsOwnerAndTrainer { get; set; }

        [Required]
        [Display(Name = "KRC License #")]
        public string LicenseNumber { get; set; }


        //[Required]
        //public string Email { get; set; }

        public string RoleName { get; set; }
        public List<SelectRoleEditorViewModel> Roles { get; set; }
        public ICollection<SelectListItem> RolesList { get; set; }

        public string OriginalUserName { get; set; }
    }
}