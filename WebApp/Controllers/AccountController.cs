using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly AddressService _addressService;
    private readonly AddressRepository _addressRepository;
    private readonly OptionalInfoRepository _optionalInfoRepository;




    public AccountController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, AddressService addressService, AddressRepository addressRepository, OptionalInfoRepository optionalInfoRepository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _addressService = addressService;
        _addressRepository = addressRepository;
        _optionalInfoRepository = optionalInfoRepository;
    }


    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var viewModel = new AccountDetailsViewModel();
        viewModel.AccountBasic = await PopulateBasic();

        if(viewModel.AccountBasic != null)
        {
            try
            {
                viewModel.AccountAddress = await PopulateAddress();
                viewModel.AccountOptionals = await PopulateOptionals();
            }
            catch
            {
                return View(viewModel);
            }
            
        }
       

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Basic(AccountDetailsViewModel viewmodel)
    {
     if (viewmodel.AccountBasic !=null)
        {
            var user = await _userManager.GetUserAsync(User);
            var optionalIfon = await Bio(viewmodel.AccountOptionals);
            if (user != null)
            {
                user.FirstName = viewmodel.AccountBasic.FirstName;
                user.LastName = viewmodel.AccountBasic.LastName;
                user.Email = viewmodel.AccountBasic.Email;
                user.UserName = viewmodel.AccountBasic.Email;
                user.PhoneNumber = viewmodel.AccountBasic.Phone;

                await _userManager.UpdateAsync(user);

                


               
                return RedirectToAction("Index", "Account");
            }
            

        }
        return View(viewmodel);
    }

    [HttpPost]
    public async Task<IActionResult> Address(AccountDetailsViewModel viewmodel)

    {
        if (viewmodel.AccountAddress != null)
        {
            var userEntity = await _userManager.GetUserAsync(User);
            var optionalInfo = await SecAddress(viewmodel.AccountOptionals);

            if (String.IsNullOrEmpty(viewmodel.AccountAddress.AddressLine1) && String.IsNullOrEmpty(viewmodel.AccountAddress.PostalCode) && String.IsNullOrEmpty(viewmodel.AccountAddress.City))
            {


                userEntity!.AddressId = null;
                var result = await _userManager.UpdateAsync(userEntity);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Account");
                }
            }
            else
            {

                var address = await _addressRepository.GetOneAsync(x => x.AddressLine == viewmodel.AccountAddress.AddressLine1 && x.PostalCode == viewmodel.AccountAddress.PostalCode && x.City == viewmodel.AccountAddress.City);

                if (address == null)
                {
                    var newAddress = await _addressRepository.CreateAsync(new AddressEntity
                    {
                        AddressLine = viewmodel.AccountAddress.AddressLine1!,
                        PostalCode = viewmodel.AccountAddress.PostalCode!,
                        City = viewmodel.AccountAddress.City!,
                    });

                    if (newAddress != null)
                    {
                        var user = await _userManager.GetUserAsync(User);
                        if (user != null)
                        {
                            user.AddressId = newAddress.Id;
                            var result = await _userManager.UpdateAsync(user);

                            if (result.Succeeded)
                            {
                                return RedirectToAction("Index", "Account");
                            }

                        }

                    }

                }
                else
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user != null)

                    {
                        user.AddressId = address.Id;
                        var result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Account");
                        }

                    }

                }
               
        }

          
        }
            return RedirectToAction("Index", "Account");
        

    }

    [HttpGet]
    public async Task<IActionResult> Security()
    {
        var viewModel = new AccountSecurityViewModel
        {
            AccountBasic = await PopulateBasic()
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Security(AccountSecurityViewModel viewmodel)
    {
       if(viewmodel.AccountSecurity != null)
        {
            var userEntity = await _userManager.GetUserAsync(User);
            if(userEntity != null)
            {
                if(!String.IsNullOrEmpty(viewmodel.AccountSecurity.CurrentPassword) || !String.IsNullOrEmpty(viewmodel.AccountSecurity.NewPassword))
                {
                    var passwordChange = await _userManager.ChangePasswordAsync(userEntity, viewmodel.AccountSecurity.CurrentPassword, viewmodel.AccountSecurity.NewPassword);
                    if (passwordChange.Succeeded)
                    {
                        var result = await _userManager.UpdateAsync(userEntity);
                        if (result.Succeeded)
                        {
                            TempData["PasswordSuccess"] = "Password was succesfully changed";
                            return RedirectToAction("Security", "Account");
                        }
                    }

                    TempData["PasswordError"] = "Something went wrong, please check your passwords";
                    return RedirectToAction("Security", "Account");
                }
                
                
                TempData["PasswordError"] = "Something went wrong, please check your passwords";
                return RedirectToAction("Security", "Account");
            }
            
        }
        return RedirectToAction("Security", "Account");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(AccountSecurityViewModel viewmodel)
    {
        if (viewmodel.AccountDelete != null)
        {
            var userEntity = await _userManager.GetUserAsync(User);
            if(userEntity != null)
            {
                if (viewmodel.AccountDelete.DeleteAccount== true)
                {

                    var deleteUser = await _userManager.DeleteAsync(userEntity);
                    if (deleteUser.Succeeded)
                    {
                        await _signInManager.SignOutAsync();
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    
                    TempData["ErrorMessage"] = "Checkbox must be confirmed";
                    return RedirectToAction("Security", "Account");
                }

            }
        }
        return RedirectToAction("Security", "Account");
    }

    [HttpGet]
    public async Task<IActionResult> SavedItems()
    {
        var viewmodel = new AccountSavedItemsViewModel
        {
            AccountBasic = await PopulateBasic()
        };
        return View(viewmodel);
    }

    [HttpPost]

    public IActionResult SavedItems(AccountSavedItemsViewModel viewmodel)
    {
        if (ModelState.IsValid)
        {
            return RedirectToAction("SavedItems", "Account");
        }
        return View(viewmodel);
    }


    [HttpGet]

    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }


    private async Task<AccountBasic> PopulateBasic()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            return new AccountBasic
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                Phone = user.PhoneNumber,
                Bio = user.OptionalInfo?.Bio,
                IsExternalAccount = user.IsExternalAccount,

            };
        }

        return null!;
    }



    private async Task<AccountAddress> PopulateAddress()
    {
        var user= await _userManager.GetUserAsync(User);
        if (user != null)
        {

            var address = await _addressRepository.GetOneAsync(x => x.Id == user.AddressId);
            if (address != null)
            {
                return new AccountAddress
                {
                    AddressLine1 = address.AddressLine,
                    PostalCode = address.PostalCode,
                    City = address.City,

                };
            }
            else
            {
                return new AccountAddress
                {
                    AddressLine1 = "",
                    PostalCode = "",
                    City = "",
                };
            }
           
           
        }
        return null!;
    }


    private async Task<AccountOptionals> PopulateOptionals()
    {
        var user = await _userManager.GetUserAsync(User);
        

        if (user != null)
        {
            var optional = await _optionalInfoRepository.GetOneAsync(x => x.Id == user.OptionalInfoId);
            if (optional != null)
            {

                return new AccountOptionals
                {
                    Bio = optional.Bio,
                    SecAddressLine = optional.SecAddressLine,
                    ProfilePictureUrl = optional.ProfilePictureUrl,
                
                };
            }

            else
            {
                return new AccountOptionals
                {
                    Bio = "",
                    SecAddressLine= "",
                    ProfilePictureUrl=""
                
                };
            }
        }
        return null!;
    }

    private async Task<OptionalInfoEntity> SecAddress(AccountOptionals optionals)
    {
        var userOptional = await _userManager.GetUserAsync(User);
        var secAddress = await _optionalInfoRepository.GetOneAsync(x => x.Id == userOptional!.OptionalInfoId);
        if (secAddress != null)
        {

            secAddress.SecAddressLine = optionals.SecAddressLine;
            await _optionalInfoRepository.UpdateAsync(x => x.Id == secAddress.Id, secAddress);
            return secAddress;

        }
        else
        {
            if (!String.IsNullOrEmpty(optionals.SecAddressLine))
            {

                var createdOptional = await _optionalInfoRepository.CreateAsync(new OptionalInfoEntity
                {
                    SecAddressLine = optionals.SecAddressLine,
                });
                await _optionalInfoRepository.UpdateAsync(x => x.Id == createdOptional.Id, createdOptional);

                if (createdOptional != null)
                {
                    userOptional!.OptionalInfoId = createdOptional.Id;

                    var result = await _userManager.UpdateAsync(userOptional);

                    if (result.Succeeded)
                    {

                        return createdOptional;
                    }
                }
            }

        }
        return null!;
    }

    private async Task<OptionalInfoEntity> Bio(AccountOptionals optionals)
    {
        var userOptional = await _userManager.GetUserAsync(User);
        var bio = await _optionalInfoRepository.GetOneAsync(x => x.Id == userOptional!.OptionalInfoId);
        if (bio != null)
        {

            bio.Bio  = optionals.Bio;
            await _optionalInfoRepository.UpdateAsync(x => x.Id == bio.Id, bio);
            return bio;

        }
        else
        {
            if (!String.IsNullOrEmpty(optionals.Bio))
            {

                var createdOptional = await _optionalInfoRepository.CreateAsync(new OptionalInfoEntity
                {
                    Bio = optionals.Bio,
                });
                await _optionalInfoRepository.UpdateAsync(x => x.Id == createdOptional.Id, createdOptional);

                if (createdOptional != null)
                {
                    userOptional!.OptionalInfoId = createdOptional.Id;

                    var result = await _userManager.UpdateAsync(userOptional);

                    if (result.Succeeded)
                    {

                        return createdOptional;
                    }
                }
            }

        }
        return null!;
    }


}