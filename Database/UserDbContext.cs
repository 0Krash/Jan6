using Jan6.Models;
using Microsoft.EntityFrameworkCore;

namespace Jan6.Database;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<UserModel> Users { get; set; }
}