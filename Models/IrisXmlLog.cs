using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IRIS.Models
{
    [Table("IrisXmlLog", Schema = "dbo")]
    public partial class IrisXmlLog
    {
        
        [Key]
        public int SequenceNumber { get; set; }
        public string MSGID { get; set; }
        public string OrderID { get; set; }
        public string  XmlPayload { get; set; }
        public DateTime? DateTime { get; set; }
        public string newItem;
    }
}
