using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Tactoro.QuantDev.Models;

namespace Tactoro.QuantDev.Test.ControllersTests
{
    public class ControllersTestsSetup
    {
        public QuantDevDBContext mockDbContext { get; set; }

        public ControllersTestsSetup()
        {
            //Seed data
            User user1 = new User()
            {
                Id = 1,
                UserName = "vaqqas",
                Email = "vaqqas@gmail.com",
                Alias = "vqs",
                FirstName = "mohammad",
                LastName = "Vaqqas"
            };

            User user2 = new User()
            {
                Id = 2,
                UserName = "user2",
                Email = "user2@gmail.com",
                Alias = "user2",
                FirstName = "user2FName",
                LastName = "user2LName"
            };

            User user3 = new User()
            {
                Id = 3,
                UserName = "user3",
                Email = "user3@gmail.com",
                Alias = "user3",
                FirstName = "user3FName",
                LastName = "user3LName"
            };

            User user4 = new User()
            {
                Id = 4,
                UserName = "user4",
                Email = "user4@gmail.com",
                Alias = "user4",
                FirstName = "user4FName",
                LastName = "user4LName"
            };

            var manager1 = new Manager()
            {
                Id = 1,
                Position = "Senior",
                UserId = user1.Id,
                User = user1
            };

            var manager2 = new Manager()
            {
                Id = 2,
                Position = "Junior",
                UserId = user2.Id,
                User = user2
            };

            var cust1 = new Customer()
            {
                Id = 1,
                Level = "Regional",
                ManagerId = 1,
                UserId = user3.Id,
                User = user3,
                Manager = manager1,
            };

            var cust2 = new Customer()
            {
                Id = 2,
                Level = "National",
                ManagerId = 2,
                UserId = user4.Id,
                User = user4,
                Manager = manager2,
            };

            manager1.Customers.Add(cust1);

            manager1.Customers.Add(cust2);

            //create mock db context
            mockDbContext = new QuantDevDBContext(new DbContextOptionsBuilder<QuantDevDBContext>().UseInMemoryDatabase(databaseName: "QuantDevDB").Options);

            //seed the db context
            mockDbContext.Users.Add(user1);
            mockDbContext.Users.Add(user2);
            mockDbContext.Users.Add(user3);
            mockDbContext.Users.Add(user4);
            mockDbContext.Managers.Add(manager1);
            mockDbContext.Managers.Add(manager2);
            mockDbContext.Customers.Add(cust1);
            mockDbContext.Customers.Add(cust2);

            mockDbContext.SaveChanges();
        }
    }
}
