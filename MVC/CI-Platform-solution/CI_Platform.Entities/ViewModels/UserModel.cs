using CI_Platform.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Entities.ViewModels;

public  class UserModel
{
 
    [Required(ErrorMessage ="FirstName is Required")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "LastName is Required")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Email is Required")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Please Enter valid EmailId")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Phone Number is Required")]
    //[MaxLength(10, ErrorMessage = "Please Enter valid short PhoneNumber")]
    //[MinLength(10, ErrorMessage = "Please Enter valid long PhoneNumber")]

    //[MinLength(10, ErrorMessage = "Please Enter valid PhoneNumber")]
    //[DataType( ErrorMessage = "Please Enter valid PhoneNumber")]
    public long PhoneNumber { get; set; }

    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one uppercase letter and one digit")]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters long")]
    [Required(ErrorMessage = "Password is Required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage ="Password must match")]
    [Required(ErrorMessage = "Confirm PassWord is Required")]
    public string ConfirmPassword { get; set; } = null!;
    

}
public  class Login
{
    [Required(ErrorMessage = "Email is Required")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Please Enter valid EmailId")]
    public string Email { get; set; } = null!;

    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one uppercase letter and one digit")]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters long")]
    [Required(ErrorMessage = "Password is Required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public string? returnUrl { get; set; }

  
}

public class ForgotPwd
{
    [Required(ErrorMessage = "Email is Required")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Please Enter valid EmailId")]
    public string Email { get; set; } = null!;

}


public class ResetPwd
{

    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one uppercase letter and one digit")]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters long")]
    [Required(ErrorMessage = "Password is Required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    //public string token { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password must match")]
    [Required(ErrorMessage = "Confirm PassWord is Required")]
    public string ConfirmPassword { get; set; } = null!;


}