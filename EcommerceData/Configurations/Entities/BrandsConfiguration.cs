using EcommerceData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceData.Configurations.Entities
{
    class BrandsConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasData(
                new Brand
                {
                    Id = 1,
                    Title = "Adidas"
                },
                new Brand
                {
                    Id = 2,
                    Title = "Armani"
                },
                new Brand
                {
                    Id = 3,
                    Title = "Asics"
                },
                new Brand
                {
                    Id = 4,
                    Title = "Cabba"
                },
                new Brand
                {
                    Id = 5,
                    Title = "Calvin Klein"
                },
                new Brand
                {
                    Id = 6,
                    Title = "Columbia"
                },
                new Brand
                {
                    Id = 7,
                    Title = "Diesel"
                },
                new Brand
                {
                    Id = 8,
                    Title = "H&M"
                },
                new Brand
                {
                    Id = 9,
                    Title = "Zara"
                }
            );
        }
    }
}
