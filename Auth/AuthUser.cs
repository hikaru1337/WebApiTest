public record class UserDto(string Username, string Password);


public record UserModel
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}
