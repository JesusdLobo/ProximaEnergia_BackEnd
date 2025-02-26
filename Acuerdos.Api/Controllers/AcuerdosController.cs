using Acuerdos.Application.Interfaces;
using Acuerdos.Domain.Models;
using Microsoft.AspNetCore.Mvc;
namespace Acuerdos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AcuerdosController : ControllerBase
    {
        private readonly IAcuerdosService _acuerdosService;

        public AcuerdosController(IAcuerdosService acuerdosService)
        {
            _acuerdosService = acuerdosService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<AcuerdoComercial>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 200)
        {
            var pagedResult = await _acuerdosService.GetAllAsync(page, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AcuerdoComercial>> GetById(int id)
        {
            var acuerdo = await _acuerdosService.GetByIdAsync(id);
            if (acuerdo == null) return NotFound();
            return Ok(acuerdo);
        }

        [HttpPost]
        public async Task<ActionResult<AcuerdoComercial>> Create(AcuerdoComercial acuerdo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _acuerdosService.CreateAsync(acuerdo);
            return CreatedAtAction(nameof(GetById), new { id = created.IdAcuerdo }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, AcuerdoComercial acuerdo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _acuerdosService.GetByIdAsync(id);
            if (existing == null)
                return NotFound("El acuerdo no existe.");

            try
            {
                await _acuerdosService.UpdateAsync(id, acuerdo);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _acuerdosService.GetByIdAsync(id);
            if (existing == null)
                return NotFound("El acuerdo no existe.");

            var success = await _acuerdosService.DeleteAsync(id);
            if (!success)
                return StatusCode(500, "No se pudo eliminar el acuerdo.");
            return NoContent();
        }

        [HttpGet("tarifas")]
        public async Task<ActionResult<IEnumerable<TarifaConsumo>>> GetTarifas()
        {
            var tarifas = await _acuerdosService.GetTarifasAsync();
            return Ok(tarifas);
        }

        [HttpGet("{id:int}/tarifas")]
        public async Task<ActionResult<List<TarifaDetalleDto>>> GetTarifasByAcuerdo(int id)
        {
            var tarifas = await _acuerdosService.GetTarifasByAcuerdoAsync(id);
            if (tarifas == null || tarifas.Count == 0)
                return NotFound("No se encontraron tarifas para el acuerdo especificado.");
            return Ok(tarifas);
        }

        [HttpPut("{id:int}/tarifas")]
        public async Task<IActionResult> UpdateTarifas(int id, [FromBody] List<TarifaAcuerdoUpdateDto> dtoList)
        {
            if (dtoList == null)
                return BadRequest("No se enviaron tarifas.");

            try
            {
                var tarifas = new List<TarifaAcuerdo>();
                foreach (var dto in dtoList)
                {
                    var entity = new TarifaAcuerdo
                    {                       
                        IdAcuerdo = dto.IdAcuerdo,
                        IdTarifa = dto.IdTarifa,
                        PorcRenovacion = dto.PorcRenovacion,
                        FechaVigor = dto.FechaVigor
                    };
                    tarifas.Add(entity);
                }

                await _acuerdosService.UpdateTarifasAsync(id, tarifas);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("AgenteComercial")]
        public async Task<ActionResult<IEnumerable<AgenteComercial>>> GetAllAsyncAgenteComercial()
        {
            var agentes = await _acuerdosService.GetAllAsyncAgenteComercial();
            return Ok(agentes);
        }
    }
}
