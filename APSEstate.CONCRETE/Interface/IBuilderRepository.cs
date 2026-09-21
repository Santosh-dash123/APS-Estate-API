using APSEstate.CORE.APIResponse;
using APSEstate.CORE.Enum;
using APSEstate.CORE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSEstate.CONCRETE.Interface
{
    public interface IBuilderRepository
    {
        Task<ApiResponse<BuilderGetModel>> GetBuilderAsync(int? builderId);

        Task<ApiSingleResponse<BuilderGetModel>> SaveBuilderAsync(BuilderSaveModel data,EnumAction action);
    }
}
