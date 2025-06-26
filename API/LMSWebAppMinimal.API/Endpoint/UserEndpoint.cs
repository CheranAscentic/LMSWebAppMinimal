using LMSWebAppMinimal.API.DTO;
using LMSWebAppMinimal.API.Interface;
using LMSWebAppMinimal.Application.Interface;
using LMSWebAppMinimal.Domain.Base;
using Microsoft.AspNetCore.Http;

namespace LMSWebAppMinimal.API.Endpoint
{
    public class UserEndpoint : IEndpointGroup
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var users = app.MapGroup("/api/users")
                .WithTags("Users")
                .WithOpenApi();

            // Get all users
            users.MapGet("/", (IUserService userService) => {
                return Results.Ok(userService.GetAllUsers());
            })
            .WithName("GetAllUsers")
            .WithSummary("Get all users")
            .WithDescription("Returns a list of all users in the system")
            .Produces<List<BaseUser>>(StatusCodes.Status200OK);

            // Get User with Id
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
            })
            .WithName("GetUserById")
            .WithSummary("Get a user by ID")
            .WithDescription("Retrieves a specific user by their ID")
            .Produces<BaseUser>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound);

            // Add user with CreateUserDTO
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
            })
            .WithName("CreateUser")
            .WithSummary("Create a new user")
            .WithDescription("Creates a new user with the specified name and type")
            .Produces<BaseUser>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);

            // Update user with Id to User
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
            })
            .WithName("UpdateUser")
            .WithSummary("Update a user")
            .WithDescription("Updates a user's information by their ID")
            .Produces<BaseUser>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound);

            // Delete user with Id
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
            })
            .WithName("DeleteUser")
            .WithSummary("Delete a user")
            .WithDescription("Deletes a user by their ID")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound);
        }
    }
}
