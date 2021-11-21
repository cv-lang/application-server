using Cvl.ApplicationServer.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Database.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ProcessInstance> ProcessInstances => Set<ProcessInstance>();
        public DbSet<ProcessActivity> ProcessActivities => Set<ProcessActivity>();
        public DbSet<ProcessActivityData> ProcessActivityDaties => Set<ProcessActivityData>();
        public DbSet<ProcessInstanceStateData> ProcessStates => Set<ProcessInstanceStateData>();
        public DbSet<StepHistory> StepHistories => Set<StepHistory>();
    }
}
