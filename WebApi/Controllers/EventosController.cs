using Application.Interface.IService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class EventosController : ControllerBase
{
    private readonly IEventoService _eventoService;

    public EventosController(IEventoService eventoService)
    {
        _eventoService = eventoService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var eventos = await _eventoService.ObtenerEventosAsync();
        return Ok(eventos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var evento = await _eventoService.ObtenerEventoPorIdAsync(id);
        if (evento == null) return NotFound();
        return Ok(evento);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] EventoDto dto)
    {
        var creado = await _eventoService.CrearEventoAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = creado.Id }, creado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] EventoDto dto)
    {
        var actualizado = await _eventoService.EditarEventoAsync(id, dto);
        if (actualizado == null) return NotFound();
        return Ok(actualizado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminado = await _eventoService.EliminarEventoAsync(id);
        if (!eliminado) return NotFound();
        return NoContent();
    }
}