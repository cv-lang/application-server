using Cvl.ApplicationServer.Core.Model.Temporary;
using Cvl.ApplicationServer.Core.Processes.Model;
using Cvl.ApplicationServer.Core.Users.Model;
using Microsoft.EntityFrameworkCore;

namespace Cvl.ApplicationServer.Core.Model.Contexts
{
    public class ApplicationServerDbContext : DbContext
    {
        public ApplicationServerDbContext(DbContextOptions<ApplicationServerDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        //core
        public DbSet<User> Users => Set<User>();

        //processes
        public DbSet<ProcessInstanceContainer> ProcessInstances => Set<ProcessInstanceContainer>();
        public DbSet<ProcessActivity> ProcessActivities => Set<ProcessActivity>();
        public DbSet<ProcessActivityData> ProcessActivityDaties => Set<ProcessActivityData>();
        public DbSet<ProcessStateData> ProcessStates => Set<ProcessStateData>();
        public DbSet<ProcessStepHistory> StepHistories => Set<ProcessStepHistory>();
        public DbSet<LogElement> LogElements => Set<LogElement>();

    }
}
