public class AuthApi : IApi
{
    public void Register(WebApplication app)
    {
        app.MapGet("/login", [AllowAnonymousAttribute] async (HttpContext context,
            ITokenService tokenServie, IUserRepository userRepository) =>
            {
                UserModel userModel = new()
                {
                    UserName = context.Request.Query["username"],
                    Password = context.Request.Query["password"]
                };

                var UserDto = userRepository.GetUser(userModel);

                if (UserDto == null) return Results.Unauthorized();
                var token = tokenServie.BuildToken(app.Configuration["Jwt:Key"],
                    app.Configuration["Jwt:Issuer"], UserDto);
                return Results.Ok(token);
            });
    }
}