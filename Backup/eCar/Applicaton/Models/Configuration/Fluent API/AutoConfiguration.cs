using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using eCar.Applicaton.Models.Service.Entities;

namespace eCar.Applicaton.Models.Configuration.Fluent_API
{
    public class AutoConfiguration:EntityTypeConfiguration<Auto>
    {
        public AutoConfiguration()
        {
        }
    }
}