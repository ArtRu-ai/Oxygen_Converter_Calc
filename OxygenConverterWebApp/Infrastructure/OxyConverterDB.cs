using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OxygenConverterWebApp.Domain;
using System.Data.Entity;

namespace OxygenConverterWebApp.Infrastructure
{
    public class OxyConverterDB : DbContext
    {
        public OxyConverterDB() : base ("OxyConverterDBConnection") { }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<Variants> Variants { get; set; }

        public DbSet<InputDataVariants> InputDataVariants { get; set; }
    }
}