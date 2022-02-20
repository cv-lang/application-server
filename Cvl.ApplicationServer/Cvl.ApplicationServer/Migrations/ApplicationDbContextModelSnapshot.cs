﻿// <auto-generated />
using System;
using Cvl.ApplicationServer.Core.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cvl.ApplicationServer.Migrations
{
    [DbContext(typeof(ApplicationServerDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Cvl.ApplicationServer.Core.Model.Processes.ProcessActivity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<int>("ActivityState")
                        .HasColumnType("int");

                    b.Property<bool>("Archival")
                        .HasColumnType("bit");

                    b.Property<string>("ClientConnectionData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientIpAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientIpPort")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("MemberName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PreviewRequestJson")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("PreviewResponseJson")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<long>("ProcessActivityDataId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProcessInstanceId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ResponseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProcessActivityDataId");

                    b.HasIndex("ProcessInstanceId");

                    b.ToTable("ProcessActivity", "Processes");
                });

            modelBuilder.Entity("Cvl.ApplicationServer.Core.Model.Processes.ProcessActivityData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<bool>("Archival")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RequestFullSerialization")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestJson")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResponseFullSerialization")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResponseJson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResponseType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProcessActivityData", "Processes");
                });

            modelBuilder.Entity("Cvl.ApplicationServer.Core.Model.Processes.ProcessDiagnosticData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<bool>("Archival")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastError")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastErrorPreview")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastRequestPreview")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastResponsePreview")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("NumberOfActivities")
                        .HasColumnType("bigint");

                    b.Property<long>("NumberOfErrors")
                        .HasColumnType("bigint");

                    b.Property<long>("NumberOfSteps")
                        .HasColumnType("bigint");

                    b.Property<long>("ProcessInstanceId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ProcessInstanceId")
                        .IsUnique();

                    b.ToTable("ProcessDiagnosticData", "Processes");
                });

            modelBuilder.Entity("Cvl.ApplicationServer.Core.Model.Processes.ProcessInstanceContainer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<bool>("Archival")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProcessNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProcessNumber");

                    b.ToTable("ProcessInstanceContainer", "Processes");
                });

            modelBuilder.Entity("Cvl.ApplicationServer.Core.Model.Processes.ProcessStateData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<bool>("Archival")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("ProcessInstanceId")
                        .HasColumnType("bigint");

                    b.Property<string>("ProcessStateFullSerialization")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProcessInstanceId")
                        .IsUnique();

                    b.ToTable("ProcessStateData", "Processes");
                });

            modelBuilder.Entity("Cvl.ApplicationServer.Core.Model.ProcessStepHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<bool>("Archival")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("ProcessInstanceContainerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ProcessInstanceContainerId");

                    b.ToTable("ProcessStepHistory", "Processes");
                });

            modelBuilder.Entity("Cvl.ApplicationServer.Core.Model.Temporary.LogElement", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<bool>("Archival")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExecutionNumber")
                        .HasColumnType("int");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Logger")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ParentNumber")
                        .HasColumnType("int");

                    b.Property<long>("ProcessId")
                        .HasColumnType("bigint");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("LogElement", "Temporary");
                });

            modelBuilder.Entity("Cvl.ApplicationServer.Core.Users.Model.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<bool>("Archival")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Roles")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Cvl.ApplicationServer.Documents.Model.File", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<bool>("Archival")
                        .HasColumnType("bit");

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExternalGroupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhisicalPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UpdatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("VirtualPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Cvl.ApplicationServer.Files.Model.Directory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<bool>("Archival")
                        .HasColumnType("bit");

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExternalGroupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.Property<long>("UpdatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Directories");
                });

            modelBuilder.Entity("Cvl.ApplicationServer.Processes.Model.ProcessExternalData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<bool>("Archival")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExternalInputDataFullSerialization")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("ProcessInstanceId")
                        .HasColumnType("bigint");

                    b.Property<string>("ProcessOutputDataFullSerialization")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProcessInstanceId")
                        .IsUnique();

                    b.ToTable("ProcessExternalData", "Processes");
                });

            modelBuilder.Entity("Cvl.ApplicationServer.Core.Model.Processes.ProcessActivity", b =>
                {
                    b.HasOne("Cvl.ApplicationServer.Core.Model.Processes.ProcessActivityData", "ProcessActivityData")
                        .WithMany()
                        .HasForeignKey("ProcessActivityDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cvl.ApplicationServer.Core.Model.Processes.ProcessInstanceContainer", "ProcessInstance")
                        .WithMany()
                        .HasForeignKey("ProcessInstanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProcessActivityData");

                    b.Navigation("ProcessInstance");
                });

            modelBuilder.Entity("Cvl.ApplicationServer.Core.Model.Processes.ProcessDiagnosticData", b =>
                {
                    b.HasOne("Cvl.ApplicationServer.Core.Model.Processes.ProcessInstanceContainer", "ProcessInstance")
                        .WithOne("ProcessDiagnosticData")
                        .HasForeignKey("Cvl.ApplicationServer.Core.Model.Processes.ProcessDiagnosticData", "ProcessInstanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProcessInstance");
                });

            modelBuilder.Entity("Cvl.ApplicationServer.Core.Model.Processes.ProcessInstanceContainer", b =>
                {
                    b.OwnsOne("Cvl.ApplicationServer.Processes.Model.OwnedClasses.ProcessStepData", "Step", b1 =>
                        {
                            b1.Property<long>("ProcessInstanceContainerId")
                                .HasColumnType("bigint");

                            b1.Property<int>("Step")
                                .HasColumnType("int");

                            b1.Property<string>("StepDescription")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("StepName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ProcessInstanceContainerId");

                            b1.ToTable("ProcessInstanceContainer", "Processes");

                            b1.WithOwner()
                                .HasForeignKey("ProcessInstanceContainerId");
                        });

                    b.OwnsOne("Cvl.ApplicationServer.Processes.Model.OwnedClasses.ExternalIdentifiers", "ExternalIds", b1 =>
                        {
                            b1.Property<long>("ProcessInstanceContainerId")
                                .HasColumnType("bigint");

                            b1.Property<string>("ExternalId1")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("ExternalId2")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("ExternalId3")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("ExternalId4")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.HasKey("ProcessInstanceContainerId");

                            b1.ToTable("ProcessInstanceContainer", "Processes");

                            b1.WithOwner()
                                .HasForeignKey("ProcessInstanceContainerId");
                        });

                    b.OwnsOne("Cvl.ApplicationServer.Processes.Model.OwnedClasses.ProcessBusinessData", "BusinessData", b1 =>
                        {
                            b1.Property<long>("ProcessInstanceContainerId")
                                .HasColumnType("bigint");

                            b1.Property<string>("ClientName")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Email")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Phone")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("VendorName")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ProcessInstanceContainerId");

                            b1.ToTable("ProcessInstanceContainer", "Processes");

                            b1.WithOwner()
                                .HasForeignKey("ProcessInstanceContainerId");
                        });

                    b.OwnsOne("Cvl.ApplicationServer.Processes.Model.OwnedClasses.ProcessSpecificData", "ProcessSpecificData", b1 =>
                        {
                            b1.Property<long>("ProcessInstanceContainerId")
                                .HasColumnType("bigint");

                            b1.Property<string>("ProcessSpecificData1")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ProcessSpecificData2")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ProcessInstanceContainerId");

                            b1.ToTable("ProcessInstanceContainer", "Processes");

                            b1.WithOwner()
                                .HasForeignKey("ProcessInstanceContainerId");
                        });

                    b.OwnsOne("Cvl.ApplicationServer.Processes.Model.OwnedClasses.ProcessThreadData", "ThreadData", b1 =>
                        {
                            b1.Property<long>("ProcessInstanceContainerId")
                                .HasColumnType("bigint");

                            b1.Property<int>("MainThreadState")
                                .HasColumnType("int");

                            b1.Property<DateTime?>("NextExecutionDate")
                                .HasColumnType("datetime2");

                            b1.HasKey("ProcessInstanceContainerId");

                            b1.ToTable("ProcessInstanceContainer", "Processes");

                            b1.WithOwner()
                                .HasForeignKey("ProcessInstanceContainerId");
                        });

                    b.OwnsOne("Cvl.ApplicationServer.Processes.Model.OwnedClasses.ProcessTypeData", "ProcessTypeData", b1 =>
                        {
                            b1.Property<long>("ProcessInstanceContainerId")
                                .HasColumnType("bigint");

                            b1.Property<int>("ProcessType")
                                .HasColumnType("int");

                            b1.Property<string>("ProcessTypeFullName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ProcessInstanceContainerId");

                            b1.ToTable("ProcessInstanceContainer", "Processes");

                            b1.WithOwner()
                                .HasForeignKey("ProcessInstanceContainerId");
                        });

                    b.Navigation("BusinessData")
                        .IsRequired();

                    b.Navigation("ExternalIds")
                        .IsRequired();

                    b.Navigation("ProcessSpecificData")
                        .IsRequired();

                    b.Navigation("ProcessTypeData")
                        .IsRequired();

                    b.Navigation("Step")
                        .IsRequired();

                    b.Navigation("ThreadData")
                        .IsRequired();
                });

            modelBuilder.Entity("Cvl.ApplicationServer.Core.Model.Processes.ProcessStateData", b =>
                {
                    b.HasOne("Cvl.ApplicationServer.Core.Model.Processes.ProcessInstanceContainer", "ProcessInstance")
                        .WithOne("ProcessInstanceStateData")
                        .HasForeignKey("Cvl.ApplicationServer.Core.Model.Processes.ProcessStateData", "ProcessInstanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProcessInstance");
                });

            modelBuilder.Entity("Cvl.ApplicationServer.Core.Model.ProcessStepHistory", b =>
                {
                    b.HasOne("Cvl.ApplicationServer.Core.Model.Processes.ProcessInstanceContainer", "ProcessInstanceContainer")
                        .WithMany("ProcessStepHistories")
                        .HasForeignKey("ProcessInstanceContainerId");

                    b.OwnsOne("Cvl.ApplicationServer.Processes.Model.OwnedClasses.ProcessStepData", "Step", b1 =>
                        {
                            b1.Property<long>("ProcessStepHistoryId")
                                .HasColumnType("bigint");

                            b1.Property<int>("Step")
                                .HasColumnType("int");

                            b1.Property<string>("StepDescription")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("StepName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ProcessStepHistoryId");

                            b1.ToTable("ProcessStepHistory", "Processes");

                            b1.WithOwner()
                                .HasForeignKey("ProcessStepHistoryId");
                        });

                    b.Navigation("ProcessInstanceContainer");

                    b.Navigation("Step")
                        .IsRequired();
                });

            modelBuilder.Entity("Cvl.ApplicationServer.Files.Model.Directory", b =>
                {
                    b.HasOne("Cvl.ApplicationServer.Files.Model.Directory", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Cvl.ApplicationServer.Processes.Model.ProcessExternalData", b =>
                {
                    b.HasOne("Cvl.ApplicationServer.Core.Model.Processes.ProcessInstanceContainer", "ProcessInstance")
                        .WithOne("ProcessExternalData")
                        .HasForeignKey("Cvl.ApplicationServer.Processes.Model.ProcessExternalData", "ProcessInstanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProcessInstance");
                });

            modelBuilder.Entity("Cvl.ApplicationServer.Core.Model.Processes.ProcessInstanceContainer", b =>
                {
                    b.Navigation("ProcessDiagnosticData")
                        .IsRequired();

                    b.Navigation("ProcessExternalData")
                        .IsRequired();

                    b.Navigation("ProcessInstanceStateData")
                        .IsRequired();

                    b.Navigation("ProcessStepHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
