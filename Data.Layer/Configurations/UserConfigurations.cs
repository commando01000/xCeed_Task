using Data.Layer.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Layer.Configurations
{
    public class UsersConfigurations : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            // add the address to the user table instead of being on a separate table
            builder.OwnsOne(x => x.Address, a => a.WithOwner());

            builder.HasMany(u => u.AssignedTasks)
             .WithOne(t => t.AssignedUser)
             .HasForeignKey(t => t.AssignedUserId)
             .OnDelete(DeleteBehavior.SetNull); // set the foreign key to null when the user is deleted
        }
    }
}
