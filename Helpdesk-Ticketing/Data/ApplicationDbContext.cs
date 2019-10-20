using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Helpdesk_Ticketing.Models;


namespace Helpdesk_Ticketing.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        /// <summary>
        /// Seeding data with dummy users.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountUsers>().HasData(
                new AccountUsers
                {
                    ID = 1,
                    UserName = "test1@test.com",
                    PassWord = "password1"
                },
                new AccountUsers
                {
                    ID = 2,
                    UserName = "test2@test.com",
                    PassWord = "password2"
                },
                new AccountUsers
                {
                    ID = 3,
                    UserName = "admin@helpdeskteammember.com",
                    PassWord = "password3"
                }
                );
        }

        public DbSet<AccountUsers> AccountUsers { get; set; }

        //public object Roles { get; internal set; }
    }
}
