public class UserApi : IApi
{
    public void Register(WebApplication app)
    {

        app.MapGet("/Users", GetAllUser)
           .Produces<List<User>>(StatusCodes.Status200OK)
           .WithName("GetAllUsers")
           .WithTags(new[] { "Getters" });

        app.MapGet("/Users/ByUserName/{UserName}", GetByUserName)
           .Produces<User>(StatusCodes.Status200OK)
           .Produces<User>(StatusCodes.Status404NotFound)
           .WithName("GetUserByUserName")
           .WithTags("Getters");

        app.MapGet("/Users/ById/{Id}", GetById)
           .Produces<User>(StatusCodes.Status200OK)
           .Produces<User>(StatusCodes.Status404NotFound)
           .WithName("GetUserById")
           .WithTags("Getters")
           .ExcludeFromDescription();

        app.MapPost("/Users", InsertUser)
            .Accepts<User>("Application/Json")
            .Produces<User>(StatusCodes.Status201Created)
            .WithName("CreateUser")
            .WithTags("Creators");

        app.MapPut("/Users", UpdateUser)
            .Accepts<User>("Application/Json")
            .Produces<User>(StatusCodes.Status204NoContent)
            .WithName("UpdateUser")
            .WithTags("Updaters");

        app.MapDelete("/Users/{Id}", DeleteUser)
            .Produces<User>(StatusCodes.Status204NoContent)
            .Produces<User>(StatusCodes.Status404NotFound)
            .WithName("DeleteUser")
            .WithTags("Deletors");
    }

    [Authorize]
    private async Task<IResult> GetAllUser(IUserRepository IUser)
        => Results.Ok(await IUser.GetUserAsync());

    [Authorize]
    private async Task<IResult> GetByUserName(IUserRepository IUser, string userName)
    {
        var UserWithUsername = await IUser.GetUserAsync(userName);
        if (UserWithUsername != null)
            return Results.Ok(UserWithUsername);

        return Results.NotFound();
    }

    [Authorize]
    private async Task<IResult> GetById(IUserRepository IUser, ulong Id)
    {
        var UserWithId = await IUser.GetUserAsync(Id);
        if (UserWithId != null)
            return Results.Ok(UserWithId);

        return Results.NotFound();
    }

    [Authorize]
    private async Task<IResult> InsertUser(IUserRepository IUser, [FromBody] User User)
    {
        await IUser.InsertUserAsync(User);
        await IUser.SaveAsync();
        return Results.Created($"/Users/{User.Id}", User);
    }

    [Authorize]
    private async Task<IResult> UpdateUser(IUserRepository IUser, [FromBody] User User)
    {
        await IUser.UpdateUserAsync(User);
        await IUser.SaveAsync();

        return Results.NoContent();
    }

    [Authorize]
    private async Task<IResult> DeleteUser(IUserRepository IUser, ulong Id)
    {
        var GettedUser = await IUser.GetUserAsync(Id);
        if (GettedUser == null)
            return Results.NotFound();

        GettedUser.AccountDeleting = DateTime.Now.AddMonths(1);
        await IUser.SaveAsync();

        return Results.NoContent();
    }
}