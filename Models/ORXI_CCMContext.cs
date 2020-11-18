using System;
//using DataContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IRIS.Models
{
    public partial class ORXI_CCMContext : DbContext
    {
        public ORXI_CCMContext()
        {
        }

        public ORXI_CCMContext(DbContextOptions<ORXI_CCMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<IrisProcessedLog> IrisProcessedLog { get; set; }

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IrisProcessedLog>(entity =>
            {
                entity.HasKey(e => e.SequenceId);

                entity.ToTable("IRIS_ProcessedLog", "dbo");

                entity.Property(e => e.SequenceId).HasColumnName("SequenceID");

                entity.Property(e => e.AccountNumber)
                    .HasColumnName("accountNumber")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Age)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.AltAccountNumber)
                    .HasColumnName("altAccountNumber")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountGiveCode).HasColumnName("countGiveCode");

                entity.Property(e => e.Detailedjobstate)
                    .HasColumnName("detailedjobstate")
                    .IsUnicode(false);

                entity.Property(e => e.DocsStatusUrl).HasColumnName("DocsStatusURL");

                entity.Property(e => e.DoctorOrderFlag)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentType)
                    .HasColumnName("documentType")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Durationtime).HasColumnName("durationtime");

                entity.Property(e => e.Edrop)
                    .HasColumnName("EDrop")
                    .IsUnicode(false);

                entity.Property(e => e.Estorage)
                    .HasColumnName("EStorage")
                    .IsUnicode(false);

                entity.Property(e => e.FillerOrderNum)
                    .HasColumnName("fillerOrderNum")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.HealthPlanName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.JobSource)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Jobidentifier)
                    .HasColumnName("jobidentifier")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Jobpagecount).HasColumnName("jobpagecount");

                entity.Property(e => e.Jobspoolslot)
                    .HasColumnName("jobspoolslot")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LanguageId)
                    .HasColumnName("languageId")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LogoFile).HasColumnName("logoFile");

                entity.Property(e => e.MsgControlId)
                    .HasColumnName("msgControlID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Msid)
                    .HasColumnName("MSID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Numberofdocuments).HasColumnName("numberofdocuments");

                entity.Property(e => e.Odtype)
                    .HasColumnName("ODType")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.OrderControl)
                    .HasColumnName("orderControl")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrgCode)
                    .HasColumnName("orgCode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PatientId)
                    .HasColumnName("PatientID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PatientMohistoryFirstOrderFlag)
                    .HasColumnName("patientMOHistoryFirstOrderFlag")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.PatientPreferenceHffEnrollmentFlag)
                    .HasColumnName("patientPreferenceHffEnrollmentFlag")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.PctPdf).HasColumnName("PCt_PDF");

                entity.Property(e => e.PctPrintApsDown).HasColumnName("PCt_Print_APS_Down");

                entity.Property(e => e.PctPrintApsDuplex).HasColumnName("PCt_Print_APS_Duplex");

                entity.Property(e => e.PctPrintApsUp).HasColumnName("PCt_Print_APS_UP");

                entity.Property(e => e.PctPrintHvs).HasColumnName("PCt_Print_HVS");

                entity.Property(e => e.PctTno).HasColumnName("PCt_TNO");

                entity.Property(e => e.PrefDate).HasColumnType("datetime");

                entity.Property(e => e.PrefEmail).HasMaxLength(50);

                entity.Property(e => e.PrintAnywherePrinter)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrintDrop).IsUnicode(false);

                entity.Property(e => e.Queuesapplied)
                    .HasColumnName("queuesapplied")
                    .IsUnicode(false);

                entity.Property(e => e.RxpharmCode)
                    .HasColumnName("RXPharmCode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .IsUnicode(false);

                entity.Property(e => e.StrDocTypes)
                    .HasColumnName("strDocTypes")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Submissiontime)
                    .HasColumnName("submissiontime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Tfn)
                    .HasColumnName("TFN")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
