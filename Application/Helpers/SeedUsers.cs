using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Application.Services;
using Domain.Exceptions;
using Bogus;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Helpers
{
    public class SeedUsers
    {
        // public async static Task Seed(IApplicationDataContext context, 
        //     IAuthenticationService authenticationService)
        // {
        //     for (int i = 0; i < 1000; i++)
        //     {
        //         var faker = new Faker<AppUser>()
        //             .RuleFor(x => x.Id, Guid.NewGuid())
        //             .RuleFor(x => x.Email, x => x.Person.Email.ToLower())
        //             .RuleFor(x => x.UserName, x => x.Person.UserName.ToLower())
        //             .RuleFor(x => x.FirstName, x => x.Person.FirstName.ToLower())
        //             .RuleFor(x => x.LastName, x => x.Person.LastName.ToLower())
        //             .RuleFor(x => x.City, x => x.Address.City().ToLower())
        //             .RuleFor(x => x.BirthDate, x => x.Person.DateOfBirth)
        //             .RuleFor(x => x.MainPhotoUrl, x => x.Image.PicsumUrl());

        //         var user = faker.Generate();

        //         byte[] hash, salt;

        //         authenticationService.CreatePasswordHash("123456", out hash, out salt);

        //         user.PasswordHash = hash;
        //         user.PasswordSalt = salt;

        //         if (user.BirthDate.Year - 13 > DateTime.UtcNow.Year )
        //         {
        //             user.BirthDate.AddYears(-14);
        //         }

        //         await context.Users.AddAsync(user);

        //         if (await context.SaveChangesAsync() <= 0)
        //         {
        //             throw new DatabaseException("Error while generating data");
        //         }
        //     }
        //}

        // public async static Task AddUrls(ApplicationDataContext context) 
        // {
        //     var usersFromRepo = await context.Users.ToListAsync();

        //     foreach(var user in usersFromRepo) {

        //         var faker = new Faker<AppUser>()
        //             //.RuleFor(x => x.MainPhotoUrl, x => x.Image.PicsumUrl())
        //             .RuleFor(x => x.Description, x => x.Lorem.Text());

        //         var url = faker.Generate();

        //         var newUser = user;
        //         newUser.Description = url.Description;

        //         context.Users.Update(newUser);

        //         context.SaveChanges();
        //     }
        // }
    }
}