using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSEstate.CORE.APIResponse
{
    public class ApiResponse<T>
    {
        public bool success { get; set; }
        public string? message { get; set; }
        public List<T>? data { get; set; } = default(List<T>);
    }

    public class ApiSingleResponse<T>
    {
        public bool success { get; set; }
        public string? message { get; set; }
        public T? data { get; set; }
    }
}
