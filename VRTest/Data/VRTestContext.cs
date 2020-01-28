using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VRTest.Models
{
    public class VRTestContext : DbContext
    {
        public VRTestContext (DbContextOptions<VRTestContext> options)
            : base(options)
        {
        }

        public DbSet<VRTestWeb.Razor.Models.NPVDetailModel> NPVDetailModel { get; set; }
    }
}
