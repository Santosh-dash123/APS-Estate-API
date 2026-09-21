using APSEstate.CONCRETE.Interface;
using APSEstate.CORE.Common;
using APSEstate.CORE.Enum;
using APSEstate.CORE.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APSEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOnly")]
    public class BuilderController : ControllerBase
    {
        private readonly IBuilderRepository _builderRepository;

        public BuilderController(IBuilderRepository builderRepository)
        {
            _builderRepository = builderRepository;
        }

        [HttpPost("GetAllBuilder")]
        public async Task<IActionResult> GetAllBuilder()
        {
            try
            {
                var result = await _builderRepository.GetBuilderAsync(null);

                if (result.success)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost("GetBuilderById")]
        public async Task<IActionResult> GetBuilderById([FromBody] IdModel data)
        {
            try
            {
                var result = await _builderRepository.GetBuilderAsync(data.Id);

                if (result.success)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost("InsertBuilder")]
        public async Task<IActionResult> InsertBuilder([FromBody] BuilderSaveModel data)
        {
            try
            {
                var result = await _builderRepository.SaveBuilderAsync(
                    data,
                    EnumAction.INSERT);

                if (result.success)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost("UpdateBuilder")]
        public async Task<IActionResult> UpdateBuilder([FromBody] BuilderSaveModel data)
        {
            try
            {
                var result = await _builderRepository.SaveBuilderAsync(
                    data,
                    EnumAction.UPDATE);

                if (result.success)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost("DeleteBuilder")]
        public async Task<IActionResult> DeleteBuilder([FromBody] IdModel data)
        {
            try
            {
                var builder = new BuilderSaveModel
                {
                    BuilderId = data.Id
                };

                var result = await _builderRepository.SaveBuilderAsync(
                    builder,
                    EnumAction.DELETE);

                if (result.success)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}