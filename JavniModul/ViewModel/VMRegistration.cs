﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdministrativniModul.ViewModeli
{
    public class VMRegistration
    {
        [DisplayName("User name")]
        public string UserName { get; set; }

        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [EmailAddress]
        [DisplayName("Confirm e-mail")]
        [Compare("Email")]
        public string EmailConfirm { get; set; }

        [DisplayName("First name")]
        public string FirstName { get; set; }

        [DisplayName("Last name")]
        public string LastName { get; set; }

        [Phone]
        public string Telephone { get; set; }

        public string Password { get; set; }

        [DisplayName("Repeat password")]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }

        [DisplayName("Country")]
        public int CountryId { get; set; }
    }
}
