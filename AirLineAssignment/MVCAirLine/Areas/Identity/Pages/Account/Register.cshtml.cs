﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using MVCAirLine.Data;
using MVCAirLine.Models;

namespace MVCAirLine.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Field> _signInManager;
        private readonly UserManager<Field> _userManager;
        private readonly IUserStore<Field> _userStore;
        private readonly IUserEmailStore<Field> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RegisterModel(
            UserManager<Field> userManager,
            IUserStore<Field> userStore,
            SignInManager<Field> signInManager,
            ILogger<RegisterModel> logger,
            RoleManager<IdentityRole> roleManager,
            IEmailSender emailSender, ApplicationDbContext ApplicationDbContext) 

          
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _userStore = userStore;
           // _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
           _emailSender = emailSender;
            _applicationDbContext = ApplicationDbContext;
           
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            
            [Display(Name = "PAN Card:")]

            [Required(ErrorMessage = "PAN Number is required")]

            public string PanNo { get; set; }

           
           // public string RoleName { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

           

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public IActionResult OnPostAsync(string returnUrl = null)
        {

            if (ModelState.IsValid)
            {
                var user = CreateUser();
                var result = new AdminPage()
                {
                    Email = Input.Email,
                    PanNo = Input.PanNo,
                    Password = Input.Password,
                    ConfirmPassword = Input.ConfirmPassword,
                    RoleName = "Operator",
                    // Status = "Pending"
                };

                _applicationDbContext.AdminPage.Add(result);
                _applicationDbContext.SaveChanges();
                SendEmail();
                return RedirectToAction("RegisteredPage", "Register");
            }

            return Page();
        }

       

        //        await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
        //        await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
        //        var result = await _userManager.CreateAsync(user, Input.Password);

        //        if (result.Succeeded)
        //        {

        //            _logger.LogInformation("User created a new account with password.");
        //            await _userManager.AddToRoleAsync(user, "RoleManager");

        //var userId = await _userManager.GetUserIdAsync(user);
        //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        //var callbackUrl = Url.Page(
        //    "/Account/ConfirmEmail",
        //    pageHandler: null,
        //    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
        //    protocol: Request.Scheme);

        //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
        //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

        //        if (_userManager.Options.SignIn.RequireConfirmedAccount)
        //        {
        //            return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "Home");
        //            //await _signInManager.SignInAsync(user, isPersistent: false);
        //            //return LocalRedirect(returnUrl);
        //        }
        //    }
        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.AddModelError(string.Empty, error.Description);
        //    }
        //}

        // If we got this far, something failed, redisplay form
        //return Page();
        //}

        public void SendEmail()
        {

            SmtpClient email = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential("renukadevi36918@gmail.com", "mywtlgvxxhrkvrhm")
            };
            string subject = "Welcome,Your Registration is Successful";
            string body = $"Dear {Input.Email} , Thanks for registering with us ." +
                $"https://localhost:7174/Identity/Account/Login";

            try
            {

                email.EnableSsl = true;
                email.Send("renukadevi36918@gmail.com", $"{Input.Email}", subject, body);

            }
            catch (SmtpException e)
            {
                Console.WriteLine(e);
            }

        }


        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}