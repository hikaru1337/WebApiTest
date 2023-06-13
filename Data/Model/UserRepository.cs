public class UserRepository : IUserRepository
{
    private readonly TusaDb _context;
    public UserRepository(TusaDb context)
    {
        _context = context;
    }

    public Task<List<User>> GetUserAsync()
    {
        return _context.User.ToListAsync();
    }
    public async Task<User> GetUserAsync(ulong Id) 
        => await _context.User.FirstOrDefaultAsync(x => x.Id == Id);

    public async Task<User> GetUserAsync(string UserName) 
        => await _context.User.FirstOrDefaultAsync(x=>x.UserName == UserName);

    public async Task InsertUserAsync(User user)
        => await _context.User.AddAsync(user);

    public async Task UpdateUserAsync(User user)
    {
        var UserFromDb = await _context.User.FindAsync(new object[] { user.Id });
        if (UserFromDb is null)
            return;

        _context.Entry(UserFromDb).CurrentValues.SetValues(user);
    }

    public async Task DeleteUserAsync(User user)
    {
        var UserFromDb = await _context.User.FindAsync(new object[] { user.Id });
        if (UserFromDb is null) 
            return;

        _context.User.Remove(UserFromDb);
    }

    public async Task SaveAsync() => await _context.SaveChangesAsync();

    private bool _disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
            _context.Dispose();

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
