using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IRIS.Models
{
    [Table("IrisProcessedLog", Schema = "dbo")]
    public partial class IrisProcessedLog
    {
        
        [Key]
        public int SequenceId { get; set; }
        public string FillerOrderNum { get; set; }
        public string MsgControlId { get; set; }
        public DateTime? Submissiontime { get; set; }
        public int? Durationtime { get; set; }
        public string AccountNumber { get; set; }
        public string AltAccountNumber { get; set; }
        public string PatientId { get; set; }
        public string HealthPlanName { get; set; }
        public int? LogoFile { get; set; }
        public string RxpharmCode { get; set; }
        public int? HttpReturnCode { get; set; }
        public string Status { get; set; }
        public string Detailedjobstate { get; set; }
        public string OrderControl { get; set; }
        public string OrgCode { get; set; }
        public string DocumentType { get; set; }
        public string Jobidentifier { get; set; }
        public string Jobspoolslot { get; set; }
        public string Queuesapplied { get; set; }
        public int? Numberofdocuments { get; set; }
        public int? Jobpagecount { get; set; }
        public int? PctTno { get; set; }
        public int? PctPdf { get; set; }
        public int? PctPrintHvs { get; set; }
        public int? PctPrintApsUp { get; set; }
        public int? PctPrintApsDown { get; set; }
        public int? CountGiveCode { get; set; }
        public int? CountNewRx { get; set; }
        public int? ReorderLineCt { get; set; }
        public int? OrderLineCt { get; set; }
        public string PrintAnywherePrinter { get; set; }
        public string Msid { get; set; }
        public string PrefEmail { get; set; }
        public long? ActiveJobs { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StrDocTypes { get; set; }
        public DateTime? PrefDate { get; set; }
        public string JobSource { get; set; }
        public string Odtype { get; set; }
        public string PrintDrop { get; set; }
        public string Edrop { get; set; }
        public string DocsStatusUrl { get; set; }
        public string State { get; set; }
        public string Tfn { get; set; }
        public string DoctorOrderFlag { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Estorage { get; set; }
        public int? PctPrintApsDuplex { get; set; }
        public string PatientPreferenceHffEnrollmentFlag { get; set; }
        public string PatientMohistoryFirstOrderFlag { get; set; }
        public string LanguageId { get; set; }
        public int? CounthffStatus { get; set; }
    }
}
