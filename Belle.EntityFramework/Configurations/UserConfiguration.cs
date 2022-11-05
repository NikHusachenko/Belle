using Belle.Database.Entities;
using Belle.Database.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Belle.EntityFramework.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users").HasKey(user => user.Id);

            builder.Property(user => user.Login).HasMaxLength(63);
            builder.Property(user => user.Email).HasMaxLength(63);
            builder.Property(user => user.Password).HasMaxLength(63);

            builder.HasMany<ProductEntity>(user => user.Products)
                .WithOne(prod => prod.UserEntity)
                .HasForeignKey(prod => prod.UserFK);

            builder.HasData(
                new UserEntity()
                {
                    Id = 1,
                    Login = "Vadim",
                    Email = "vadimEmail@gmail.com",
                    Password = "1111",
                    Type = UserType.Admin,
                    WalletBalance = 1000,
                    CreatedOn = DateTime.Now,
                },
                new UserEntity()
                {
                    Id = 2,
                    Login = "Vasia123",
                    Email = "vasiaEmail@gmail.com",
                    Password = "1111",
                    Type = UserType.Client,
                    WalletBalance = 5831,
                    CreatedOn = DateTime.Now,
                },
                new UserEntity()
                {
                    Id = 3,
                    Login = "Petya13",
                    Email = "petya.super.email@gmail.com",
                    Password = "1111",
                    Type = UserType.Client,
                    WalletBalance = 421,
                    CreatedOn = DateTime.Now,
                });
        }
    }
}