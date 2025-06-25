using LMSWebAppMinimal.API.DTO;
using LMSWebAppMinimal.Application.Interface;
using Microsoft.AspNetCore.Builder;

namespace LMSWebAppMinimal.API.Endpoint
{
    public static class UserEndpoint
    {
        public static WebApplication MapUserEndpoints(this WebApplication app)
        {
            var users = app.MapGroup("/api/users")
                .WithTags("Users")
                .WithOpenApi();

            //get all users
            users.MapGet("/", (IUserService userService) => {
                return Results.Ok(userService.GetAllUsers());
            });

            //get User with Id
            users.MapGet("/{id}", (int id, IUserService userService) =>
            {
                try
                {
                    var user = userService.GetUser(id);
                    return Results.Ok(user);
                }
                catch (Exception ex)
                {
                    return Results.NotFound("User with Id not found.");
                }
            });

            //Add user with CreateUserDTO
            users.MapPost("/", (CreateUserDTO createUserDTO, IUserService userService) =>
            {
                try
                {
                    var user = userService.AddUser(createUserDTO.Name, createUserDTO.Type);
                    return Results.Created($"/api/users/{user.Id}", user);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest("User could not be created.");
                }
            });

            //Update user with Id to User
            users.MapPut("/{id}", (int id, UserDTO userDTO, IUserService userService) =>
            {
                try
                {
                    var updatedUser = userService.UpdateUser(id, userDTO.Name, userDTO.Type);
                    return Results.Ok(updatedUser);
                }
                catch (Exception ex)
                {
                    return Results.NotFound("User with Id could not be updated");
                }
            });

            //Delete user with Id
            users.MapDelete("/{id}", (int id, IUserService userService) =>
            {
                try
                {
                    var user = userService.RemoveUser(id);
                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    return Results.NotFound("User with Id could not be deleted.");
                }
            });


            return app;
        }
    }
}
