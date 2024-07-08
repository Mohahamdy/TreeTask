using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreeTask.DTOs;
using TreeTask.Models;
using TreeTask.Service;

namespace TreeTask.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly AssetsTreeService _service;
        public AssetsController(AssetsTreeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<AssetsTreeDTO>>> GetAll() {
            try
            {
                var All = await _service.GetAll();

                if (!All.Any())
                    return NotFound();

                return Ok(All);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }

        [HttpGet("AllNodes")]
        public async Task<ActionResult<List<AssetsTreeDTO>>> GetAllNodes()
        {
            try
            {
                var allNodes = await _service.GetAllNodes();

                if (!allNodes.Any())
                    return NotFound();

                return Ok(allNodes);
            }catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<AssetsTreeCreateDTO>> Add(AssetsTreeCreateDTO dto)
        {
            try
            {
                var asset = await _service.Add(dto);
                return Created("Added", dto);
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
