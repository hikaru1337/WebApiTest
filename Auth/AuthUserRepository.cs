public class AuthUserRepository : IAuthUserRepository
{
    private List<UserDto> _users = new() { new UserDto("Test","Test")};
    public UserDto GetUser(UserModel userModel)
    {
        var user = _users.FirstOrDefault(x => x.Username == userModel.UserName && x.Password == userModel.Password);
        if(user == null)
            throw new Exception();

        return user;
    }
}
