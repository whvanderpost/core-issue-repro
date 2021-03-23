using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;

namespace WebApplication1.Areas.Identity.Pages.Account.Manage
{
  public partial class IndexModel : PageModel
  {
    private readonly CustomUserManager _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IndexModel(
        CustomUserManager userManager,
        SignInManager<ApplicationUser> signInManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
    }

    public string Username { get; set; }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
      [Phone]
      [Display(Name = "Phone number")]
      public string PhoneNumber { get; set; }

      [Display(Name = "Language")]
      public string Culture { get; set; }
    }

    private async Task LoadAsync(ApplicationUser user)
    {
      var userName = await _userManager.GetUserNameAsync(user);
      var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
      var culture = _userManager.GetCulture(user);

      Username = userName;

      Input = new InputModel
      {
        PhoneNumber = phoneNumber,
        Culture = culture
      };
    }

    public async Task<IActionResult> OnGetAsync()
    {
      var user = await _userManager.GetUserAsync(User);
      if (user == null)
      {
        return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
      }

      await LoadAsync(user);
      return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
      var user = await _userManager.GetUserAsync(User);
      if (user == null)
      {
        return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
      }

      if (!ModelState.IsValid)
      {
        await LoadAsync(user);
        return Page();
      }

      var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
      if (Input.PhoneNumber != phoneNumber)
      {
        var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
        if (!setPhoneResult.Succeeded)
        {
          StatusMessage = "Unexpected error when trying to set phone number.";
          return RedirectToPage();
        }
      }

      var culture = _userManager.GetCulture(user);
      if (Input.Culture != culture)
      {
        var setCultureResult = await _userManager.SetCultureAsync(user, Input.Culture);
        if (!setCultureResult.Succeeded)
        {
          var userId = await _userManager.GetUserIdAsync(user);
          throw new InvalidOperationException($"Unexpected error occurred setting culture for user with ID '{userId}'.");
        }
      }
      var newCulture = new CultureInfo(Input.Culture);

      Response.Cookies.Append(
        CookieRequestCultureProvider.DefaultCookieName,
        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(newCulture)),
        new CookieOptions { Path = Url.Content("~/"), IsEssential = true }
      );

      CultureInfo.DefaultThreadCurrentCulture = newCulture;
      CultureInfo.DefaultThreadCurrentUICulture = newCulture;

      await _signInManager.RefreshSignInAsync(user);
      StatusMessage = "Your profile has been updated";
      return RedirectToPage();
    }
  }
}
