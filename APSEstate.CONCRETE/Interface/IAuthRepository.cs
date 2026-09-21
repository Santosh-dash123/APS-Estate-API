using APSEstate.CORE.APIResponse;
using APSEstate.CORE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSEstate.CONCRETE.Interface
{
    public interface IAuthRepository
    {
        Task<ApiSingleResponse<LoginResponseModel>> LoginAsync(LoginModel data);
    }
}
