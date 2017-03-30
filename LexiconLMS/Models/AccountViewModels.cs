using LexiconLMS.Models.Data_Annotation;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LexiconLMS.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]

        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Du måste fylla i ett förnamn.")]
        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ]{1}[a-zA-ZåäöÅÄÖ\-\s*]*$", ErrorMessage = "Mata in endast bokstäver!")]
        [StringLength(30, ErrorMessage = "Förnamnet får max vara 30 tecken långt.")]
        [DisplayName("Förnamn")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Du måste fylla i ett efternamn.")]
        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ]{1}[a-zA-ZåäöÅÄÖ\-\s*]*$", ErrorMessage = "Mata in endast bokstäver!")]
        [StringLength(30, ErrorMessage = "Efternamnet får max vara 30 tecken långt.")]
        [DisplayName("Efternamn")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Du måste fylla i ett personnummer.")]
        [RegularExpression(@"^(?:19|[2-9][0-9]){0,1}(?:[0-9]{2})(?!0229|0230|0231|0431|0631|0931|1131)(?:(?:0[1-9])|(?:1[0-2]))(?:(?:0[1-9])|(?:1[0-9])|(?:2[0-9])|(?:3[01]))[-+](?!0000)(?:[0-9]{4})$",
        ErrorMessage = "Felaktigt format på personnummer! (yyyymmdd-xxxx)")]
        [DisplayName("Personnummer")]
        public string SocialSecurityNumber { get; set; }

        [Required(ErrorMessage = "Du måste fylla i en e-postadress.")]
        [EmailAddress(ErrorMessage = "Inte en giltig e-postadress.")]
        [Display(Name = "E-post")]
        public string Email { get; set; }

        [RequiredIfNot("IsEditing", ErrorMessage = "Du måste fylla i ett lösenord.")]
        [StringLength(100, ErrorMessage = "{0} måste vara minst {2} tecken långt.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }

        [RequiredIfNot("IsEditing", ErrorMessage = "Du måste fylla i ett lösenord.")]
        [DataType(DataType.Password)]
        [Display(Name = "Bekräfta lösenord")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Lösenordet och bekräftade lösenordet är inte lika.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Du måste fylla i en kurs.")]
        public int CourseId { get; set; }

        [Display(Name = "Kurs")]
        public SelectList Courses { get; set; }

        public string Msg { get; set; }

        public bool IsEditing { get; set; }

        public string Title { get; set; }

        public string UserId { get; set; }
    }

    public class RegisterTeacherViewModel
    {
        [Required(ErrorMessage = "Du måste fylla i ett förnamn.")]
        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ]{1}[a-zA-ZåäöÅÄÖ\-\s*]*$", ErrorMessage = "Mata in endast bokstäver!")]
        [StringLength(30, ErrorMessage = "Förnamnet får max vara 30 tecken långt.")]
        [DisplayName("Förnamn")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Du måste fylla i ett efternamn.")]
        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ]{1}[a-zA-ZåäöÅÄÖ\-\s*]*$", ErrorMessage = "Mata in endast bokstäver!")]
        [StringLength(30, ErrorMessage = "Efternamnet får max vara 30 tecken långt.")]
        [DisplayName("Efternamn")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Du måste fylla i ett personnummer.")]
        [RegularExpression(@"^(?:19|[2-9][0-9]){0,1}(?:[0-9]{2})(?!0229|0230|0231|0431|0631|0931|1131)(?:(?:0[1-9])|(?:1[0-2]))(?:(?:0[1-9])|(?:1[0-9])|(?:2[0-9])|(?:3[01]))[-+](?!0000)(?:[0-9]{4})$",
        ErrorMessage = "Felaktigt format på personnummer! (yyyymmdd-xxxx)")]
        [DisplayName("Personnummer")]
        public string SocialSecurityNumber { get; set; }

        [Required(ErrorMessage = "Du måste fylla i en e-postadress.")]
        [EmailAddress(ErrorMessage = "Inte en giltig e-postadress.")]
        [Display(Name = "E-post")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Du måste fylla i ett lösenord.")]
        [StringLength(100, ErrorMessage = "{0} måste vara minst {2} tecken långt.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Du måste fylla i ett lösenord.")]
        [DataType(DataType.Password)]
        [Display(Name = "Bekräfta lösenord")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Lösenordet och bekräftade lösenordet är inte lika.")]
        public string ConfirmPassword { get; set; }

        //[Required(ErrorMessage = "Du måste fylla i en kurs.")]
        public int? CourseId { get; set; }

        [Display(Name = "Kurser")]
        public MultiSelectList Courses { get; set; }

        public string Msg { get; set; }
        public int[] CourseIds { get; set; }
    }

    public class ListUserViewModel
    {
        public ListUserViewModel() { }

        // Allow Initialization with an instance of ApplicationUser:
        public ListUserViewModel(ApplicationUser user)
        {
            this.UserName = user.UserName;
            this.FullName = user.FirstName + " " + user.LastName;
            this.SocialSecurityNumber = user.SocialSecurityNumber;
            if (user.Course != null)
            {
                this.CourseId = user.Course.Id;
                this.CourseName = user.Course.Name;
            }
            else
            {
                this.CourseName = "Ingen kurs";
            }
        }

        public int CourseId { get; set; }

        [Display(Name = "Användarnamn")]
        public string UserName { get; set; }

        [Display(Name = "Namn")]
        public string FullName { get; set; }

        [Display(Name = "Kurs")]
        public string CourseName { get; set; }

        [Display(Name = "Personnummer")]
        public string SocialSecurityNumber { get; set; }


    }


    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
