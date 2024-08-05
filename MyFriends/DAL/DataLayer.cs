using Microsoft.EntityFrameworkCore;
using MyFriends.Models;

namespace MyFriends.DAL
{
    public class DataLayer : DbContext
    {
        public DataLayer(string connectionString) : base(GetOptions(connectionString))
        {
            Database.EnsureCreated();
            seed();
        }
        private void seed()
        {
            if(Friends.Count() > 0)
            {
                return;
            }

            Friend firstFriend = new Friend();

            firstFriend.FirstName = "meni";
            firstFriend.lastName = "levi";
            firstFriend.emailAddress = " meni@gmail.com";
            firstFriend.phoneNumber = "0533190476";

            Friends.Add(firstFriend);
            SaveChanges();
        }
        
        public DbSet<Friend> Friends { get; set; }

        public DbSet<Image> images { get; set; }



        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.
                UseSqlServer(new DbContextOptionsBuilder(),
                connectionString).Options;

        }
    }
}
