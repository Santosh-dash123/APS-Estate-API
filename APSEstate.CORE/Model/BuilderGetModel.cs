using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSEstate.CORE.Model
{
    public class BuilderGetModel
    {
        public int BuilderId { get; set; }
        public int? AdminId { get; set; }
        public string? BuilderName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? PIN { get; set; }
        public string? State { get; set; }
        public string? PhoneNo { get; set; }
        public string? EmailId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdationDate { get; set; }
        public int? CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
