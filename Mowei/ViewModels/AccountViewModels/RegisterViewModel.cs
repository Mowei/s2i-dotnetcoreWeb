using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Mowei.Validation;

namespace Mowei.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {/*
        [Display(Name = "Email")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Mowei.Validation.validation))]
        [MaxLength(250, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Mowei.Validation.validation))]
        [EmailAddress(ErrorMessage = null, ErrorMessageResourceName = "EmailAddress", ErrorMessageResourceType = typeof(Mowei.Validation.validation))]
        public string Email { get; set; }
        */
        
        [Required]
        [EmailAddress]
        [RegularExpression(@"^.*@.*", ErrorMessageResourceType = typeof(Mowei.Resources.RegularExpression),ErrorMessageResourceName = "Email")]
        [Display(Name = "Email",ResourceType =typeof(Mowei.Resources.DisplayName))]
        public string Email { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(Mowei.Resources.RegularExpression),ErrorMessageResourceName = "StringLength")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Mowei.Resources.DisplayName))]
        public string Password { get; set; }

       
        [DataType(DataType.Password)]
        [Display(Name = "Confirmpassword", ResourceType = typeof(Mowei.Resources.DisplayName))]
        [Compare("Password", ErrorMessageResourceType = typeof(Mowei.Resources.RegularExpression), ErrorMessageResourceName = "Compare")]
        public string ConfirmPassword { get; set; }
    }
}
