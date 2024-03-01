﻿using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class AccountAddress


{
    public string Title { get; set; } = "Address";

    [Display(Name = "Address Line 1", Prompt = "Enter your address", Order =0)]
    public string AddressLine1 { get; set; } = null!;

    [Display(Name = "Address Line 2", Prompt = "Enter your second address", Order = 1)]
    public string? AddressLine2 { get; set; }

    [Display(Name = "Postal Code", Prompt = "Enter your postal code", Order = 2)]
    public string PostalCode { get; set; } = null!;

    [Display(Name = "City", Prompt = "Enter your city", Order = 3)]
    public string City { get; set; } = null!;
}
