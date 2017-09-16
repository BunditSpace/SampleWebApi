using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SampleWebApi.Models
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext() : base("name=SampleDbEntities")  
        {

        }

        public System.Data.Entity.DbSet<SampleWebApi.Models.UserProfile> UserProfile { get; set; }
    }
}