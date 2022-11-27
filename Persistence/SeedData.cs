using Doiman;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace Persistence;
public class SeedData

{
    public static void SeedAsync(UserManager<User> userManager)
    {
        try
        {
            if(!userManager.Users.Any())
            {

                //Seed directlly the user 
                var result = userManager.CreateAsync(new User
                {
                    Email = "admin@gmail.com",
                    UserName = "Admin",
                    FullName = "Admin",
                    PhoneNumber = ""
                }, "Senhaboa-1").GetAwaiter().GetResult();

                var userFile = File.ReadAllText("../Persistence/Seeds/UserSeed.json");
                var user = JsonSerializer.Deserialize<User>(userFile);

                var result2 = userManager.CreateAsync(new User
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber
                }, "Senhaboa-1").GetAwaiter().GetResult();


                if(result.Errors is not null && result2.Errors is not null)
                {
                    throw new Exception(result.Errors + "\t"  + result2.Errors);
                }
            }
        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}