using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

using Tvitr.Models;



namespace Tvitr.Data;



public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>

{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)

        : base(options)

    {



    }





    public DbSet<Tweet> Tweet { get; set; }



    protected override void OnModelCreating(ModelBuilder builder)

    {

        base.OnModelCreating(builder);

        string schema = "Identity";



        builder.Entity<ApplicationUser>(entity =>

        {

            entity.ToTable(name: "User", schema: schema);

        });



        builder.Entity<IdentityRole<int>>(entity =>

        {

            entity.ToTable(name: "Role", schema: schema);

        });



        builder.Entity<IdentityUserClaim<int>>(entity =>

        {

            entity.ToTable("UserClaim", schema);

        });



        builder.Entity<IdentityUserLogin<int>>(entity =>

        {

            entity.ToTable("UserLogin", schema);

        });



        builder.Entity<IdentityRoleClaim<int>>(entity =>

        {

            entity.ToTable("RoleClaim", schema);

        });



        builder.Entity<IdentityUserRole<int>>(entity =>

        {

            entity.ToTable("UserRole", schema);

        });



        builder.Entity<IdentityUserToken<int>>(entity =>

        {

            entity.ToTable("UserToken", schema);

        });

    }

}



