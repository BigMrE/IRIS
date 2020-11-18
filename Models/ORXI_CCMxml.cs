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
    public partial class ORXI_CCMxml : DbContext
    {
        public ORXI_CCMxml()
        {
        }

        public ORXI_CCMxml(DbContextOptions<ORXI_CCMxml> options)
            : base(options)
        {
        }

        public virtual DbSet<IrisXmlLog> IrisXmlLog { get; set; }

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IrisXmlLog>(entity =>
            {
                entity.HasKey(e => e.SequenceNumber);

                entity.ToTable("IRIS_xmlLog", "dbo");

                entity.Property(e => e.SequenceNumber)
                    .HasColumnName("sequenceNumber").IsUnicode(false);

                entity.Property(e => e.MSGID)
                    .HasColumnName("MSGID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

               entity.Property(e => e.OrderID)
                    .HasColumnName("orderID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.XmlPayload)
                     .HasColumnName("xmlPayload")
                     .IsUnicode(false);

                entity.Property(e => e.DateTime)
                    .HasColumnName("dateTime")
                    .IsUnicode(false);

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
