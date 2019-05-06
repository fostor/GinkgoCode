using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fostor.Ginkgo.TaskSample
{
    public class LeaveApplication
    {
        public virtual int Id { get; set; }
        public virtual int? TenantId { get; set; }
        [MaxLength(30)]
        public virtual string LastModifier { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        [Required(AllowEmptyStrings = true)]
        [MaxLength(30)]
        public virtual string Creator { get; set; }
        public virtual DateTime? CreationTime { get; set; }
        [MaxLength(20)]
        [Required]
        public string AppliNumber { get; set; }
        [MaxLength(30)]
        [Required]
        public string Applicant { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [MaxLength(10)]
        public string LeaveType { get; set; }        
        public DateTime? FromTime { get; set; }        
        public DateTime? EndTime { get; set; }
        public decimal? TotalHours { get; set; }
        [MaxLength(10)]
        public string Status { get; set; }      

        public bool? NeedCheck { get; set; }

    }
}
