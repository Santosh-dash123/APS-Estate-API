using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSEstate.CORE.Model
{
    public class LoginResponseModel
    {
        public int UserId { get; set; }

        public int UserTypeId { get; set; }

        public string? UserTypeName { get; set; }

        public string? UserName { get; set; }

        public int? ReferenceId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}
