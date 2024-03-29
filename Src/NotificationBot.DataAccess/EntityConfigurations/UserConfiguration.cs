﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotifiicationBot.Domain.Entities;

namespace NotificationBot.DataAccess.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(user => user.Id)
                .IsRequired()
                .UseIdentityColumn();

            builder
                .Property(user => user.Email)
                .IsRequired();
        }
    }
}