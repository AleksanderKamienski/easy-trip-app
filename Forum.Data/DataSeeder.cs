using Forum.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data
{
    public class DataSeeder
    {
        private readonly ApplicationDbContext _context;

        public DataSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SeedAll()
        {
            SeedUsers();
            SeedGroup();
            SeedForums();
        }

        public void SeedGroup()
        {
            if (!_context.Groups.Any())
            {
                var groups = new List<Group>
                {
                    new Group 
                    {
                        Id = "1",
                        AdminId = "7098e959-fd77-4cc1-b916-61d0d6af2ce7",
                        Description = "Bułgaria trip",
                        ImageUrl = "/images/users/default.png",
                        Name = "Bułgaria"

                    },
                    new Group 
                    {
                        Id = "2",
                        AdminId = "7098e959-fd77-4cc1-b916-61d0d6af2ce7",
                        Description = "dla tych, którzy lubią tatry",
                        ImageUrl = "/images/users/default.png",
                        Name = "Tatry"
                    },
                    new Group 
                    {
                        Id = "3",
                        AdminId = "6098e959-fd77-4cc1-b916-61d0d6af2ce7",
                        Description = "lubimy mazury",
                        ImageUrl = "/images/users/default.png",
                        Name = "Mazury"
                    },
                    new Group 
                    {
                        Id = "4",
                        AdminId = "6098e959-fd77-4cc1-b916-61d0d6af2ce7",
                        Description = "dla tych którzy lubią sporty zimowe",
                        ImageUrl = "/images/users/default.png",
                        Name = "Sporty zimowe"
                    }
                };

                _context.AddRange(groups);

                var usersGroups = new List<ApplicationUserGroup>
                {
                    new ApplicationUserGroup
                    {
                        Id = "1",
                        ApplicationUserId = "5098e959-fd77-4cc1-b916-61d0d6af2ce7",
                        GroupId = "1"
                    },
                    new ApplicationUserGroup
                    {
                        Id = "2",
                        ApplicationUserId = "6098e959-fd77-4cc1-b916-61d0d6af2ce7",
                        GroupId = "1"
                    },

                    new ApplicationUserGroup
                    {
                        Id = "3",
                        ApplicationUserId = "4098e959-fd77-4cc1-b916-61d0d6af2ce7",
                        GroupId = "2"

                    },
                    new ApplicationUserGroup
                    {
                        Id = "4",
                        ApplicationUserId = "3098e959-fd77-4cc1-b916-61d0d6af2ce7",
                        GroupId = "2"
                    },
                    new ApplicationUserGroup
                    {
                        Id = "5",
                        ApplicationUserId = "3098e959-fd77-4cc1-b916-61d0d6af2ce7",
                        GroupId = "3"
                    },

                    new ApplicationUserGroup
                    {
                        Id = "6",
                        ApplicationUserId = "7098e959-fd77-4cc1-b916-61d0d6af2ce7",
                        GroupId = "4"
                    },
                    new ApplicationUserGroup
                    {
                        Id = "7",
                        ApplicationUserId = "3098e959-fd77-4cc1-b916-61d0d6af2ce7",
                        GroupId = "4"
                    },
                    new ApplicationUserGroup
                    {
                        Id = "8",
                        ApplicationUserId = "4098e959-fd77-4cc1-b916-61d0d6af2ce7",
                        GroupId = "4"
                    },
                };

                _context.AddRange(usersGroups);
                _context.SaveChanges();
            }

        }

        public void SeedUsers()
        {
            if (!_context.Users.Any())
            {
                var users = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        Id = "7098e959-fd77-4cc1-b916-61d0d6af2ce7",
                        ConcurrencyStamp = "54de09db-b176-4b40-9ba8-104b4f763fe1",
                        Email = "alek@example.com",
                        IsActive = true,
                        NormalizedEmail = "ALEK@EXAMPLE.COM",
                        NormalizedUserName = "ALEK",
                        PasswordHash = "AQAAAAEAACcQAAAAEEMb6gicmjyfGhMiRQ5IfL1U/DsAYR2XshO+zubWcuqufYYi/KO3Bul4WOOwlXeRLw==",
                        UserName = "Alek",
                        LockoutEnabled = true,
                        SecurityStamp = "2f3fe6a6-c97d-475f-9a14-00cc80cba90f",
                        MemberSince = DateTime.Now
                    },
                    new ApplicationUser
                    {
                        Id = "6098e959-fd77-4cc1-b916-61d0d6af2ce7",
                        ConcurrencyStamp = "54de09db-b176-4b40-9ba8-104b4f763fe1",
                        Email = "jarek@example.com",
                        IsActive = true,
                        NormalizedEmail = "JAREK@EXAMPLE.COM",
                        NormalizedUserName = "JAREK",
                        PasswordHash = "AQAAAAEAACcQAAAAEEMb6gicmjyfGhMiRQ5IfL1U/DsAYR2XshO+zubWcuqufYYi/KO3Bul4WOOwlXeRLw==",
                        UserName = "Jarek",
                        LockoutEnabled = true,
                        SecurityStamp = "2f3fe6a6-c97d-475f-9a14-00cc80cba90f",
                        MemberSince = DateTime.Now
                    },
                    new ApplicationUser
                    {
                        Id = "5098e959-fd77-4cc1-b916-61d0d6af2ce7",
                        ConcurrencyStamp = "54de09db-b176-4b40-9ba8-104b4f763fe1",
                        Email = "krzysiek@example.com",
                        IsActive = true,
                        NormalizedEmail = "KRZYSIEK@EXAMPLE.COM",
                        NormalizedUserName = "KRZYSIEK",
                        PasswordHash = "AQAAAAEAACcQAAAAEEMb6gicmjyfGhMiRQ5IfL1U/DsAYR2XshO+zubWcuqufYYi/KO3Bul4WOOwlXeRLw==",
                        UserName = "Krzysiek",
                        LockoutEnabled = true,
                        SecurityStamp = "2f3fe6a6-c97d-475f-9a14-00cc80cba90f",
                        MemberSince = DateTime.Now
                    },
                    new ApplicationUser
                    {
                        Id = "4098e959-fd77-4cc1-b916-61d0d6af2ce7",
                        ConcurrencyStamp = "54de09db-b176-4b40-9ba8-104b4f763fe1",
                        Email = "jan@example.com",
                        IsActive = true,
                        NormalizedEmail = "JAN@EXAMPLE.COM",
                        NormalizedUserName = "JAN",
                        PasswordHash = "AQAAAAEAACcQAAAAEEMb6gicmjyfGhMiRQ5IfL1U/DsAYR2XshO+zubWcuqufYYi/KO3Bul4WOOwlXeRLw==",
                        UserName = "Jan",
                        LockoutEnabled = true,
                        SecurityStamp = "2f3fe6a6-c97d-475f-9a14-00cc80cba90f",
                        MemberSince = DateTime.Now
                    },
                    new ApplicationUser
                    {
                        Id = "3098e959-fd77-4cc1-b916-61d0d6af2ce7",
                        ConcurrencyStamp = "54de09db-b176-4b40-9ba8-104b4f763fe1",
                        Email = "ewa@example.com",
                        IsActive = true,
                        NormalizedEmail = "EWA@EXAMPLE.COM",
                        NormalizedUserName = "EWA",
                        PasswordHash = "AQAAAAEAACcQAAAAEEMb6gicmjyfGhMiRQ5IfL1U/DsAYR2XshO+zubWcuqufYYi/KO3Bul4WOOwlXeRLw==",
                        UserName = "Ewa",
                        LockoutEnabled = true,
                        SecurityStamp = "2f3fe6a6-c97d-475f-9a14-00cc80cba90f",
                        MemberSince = DateTime.Now
                    }
                };

                _context.AddRange(users);
                _context.SaveChanges();
            }

        }

        public void SeedForums()
        {
            if (!_context.Forums.Any())
            {
                var forums = new List<Forum.Data.Models.Forum>
                {
                    new Forum.Data.Models.Forum
                    {
                        Created = DateTime.Now,
                        Description = "Zachęcamy nowych użytkowników do przywitania się" ,
                        ImageUrl = "/images/forum/cs.png",
                        Title = "Powitanie",
                        GroupId = "1",

                    },
                    new Forum.Data.Models.Forum
                    {
                        Created = DateTime.Now,
                        Description = "Zachęcamy nowych użytkowników do przywitania się" ,
                        ImageUrl = "/images/forum/cpp.png",
                        Title = "Powitanie",
                        GroupId = "2"
                    },
                    new Forum.Data.Models.Forum
                    {
                        Created = DateTime.Now,
                        Description = "Zachęcamy nowych użytkowników do przywitania się" ,
                        ImageUrl = "/images/forum/js.png",
                        Title ="Powitanie",
                        GroupId = "3"
                    },
                    new Forum.Data.Models.Forum
                    {
                        Created = DateTime.Now,
                        Description = "Zachęcamy nowych użytkowników do przywitania się" ,
                        ImageUrl = "/images/forum/go.png",
                        Title ="Powitanie",
                        GroupId = "4"
                    },
                };

                _context.AddRange(forums);
                _context.SaveChanges();
            }

        }
    }
}
