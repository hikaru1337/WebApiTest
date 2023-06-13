public interface IUserRepository : IDisposable
{
    Task<List<User>> GetUserAsync();
    Task<User> GetUserAsync(ulong Id);
    Task<User> GetUserAsync(string Username);
    Task InsertUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);
    Task SaveAsync();
}
