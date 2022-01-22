using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Model.Processes;
using Cvl.ApplicationServer.Core.Model.Temporary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Users.Model;

namespace Cvl.ApplicationServer.Core.Database.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
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

        //documents
        public DbSet<Documents.Model.File> Files => Set<Documents.Model.File>();
        public DbSet<Files.Model.Directory> Directories => Set<Files.Model.Directory>();

        //processes
        public DbSet<ProcessInstanceContainer> ProcessInstances => Set<ProcessInstanceContainer>();
        public DbSet<ProcessActivity> ProcessActivities => Set<ProcessActivity>();
        public DbSet<ProcessActivityData> ProcessActivityDaties => Set<ProcessActivityData>();
        public DbSet<ProcessStateData> ProcessStates => Set<ProcessStateData>();
        public DbSet<ProcessStepHistory> StepHistories => Set<ProcessStepHistory>();
        public DbSet<LogElement> LogElements => Set<LogElement>();

    }
}
