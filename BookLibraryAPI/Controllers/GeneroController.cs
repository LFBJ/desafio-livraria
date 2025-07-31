using BookLibraryAPI.Data;
using BookLibraryAPI.DTOs;
using BookLibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Controlador para gerenciar gêneros.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class GeneroController : ControllerBase
{
    private readonly BibliotecaContext _context;

    public GeneroController(BibliotecaContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retorna todos os gêneros cadastrados.
    /// </summary>
    /// <returns>Lista de gêneros.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GeneroDTO>>> GetAll()
    {
        var generos = await _context.Generos
            .Select(g => new GeneroDTO { Id = g.Id, Nome = g.Nome })
            .ToListAsync();

        return Ok(generos);
    }

    /// <summary>
    /// Retorna um gênero pelo ID.
    /// </summary>
    /// <param name="id">ID do gênero.</param>
    /// <returns>Gênero encontrado ou NotFound.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GeneroDTO>> GetById(int id)
    {
        var genero = await _context.Generos.FindAsync(id);
        if (genero == null) return NotFound();

        return Ok(new GeneroDTO { Id = genero.Id, Nome = genero.Nome });
    }

    /// <summary>
    /// Cria um novo gênero.
    /// </summary>
    /// <param name="dto">Dados do gênero.</param>
    /// <returns>Gênero criado.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<GeneroDTO>> Create(GeneroDTO dto)
    {
        var genero = new Genero { Nome = dto.Nome };
        _context.Generos.Add(genero);
        await _context.SaveChangesAsync();

        dto.Id = genero.Id;
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    /// <summary>
    /// Atualiza um gênero existente.
    /// </summary>
    /// <param name="id">ID do gênero.</param>
    /// <param name="dto">Novos dados do gênero.</param>
    /// <returns>NoContent ou erro.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, GeneroDTO dto)
    {
        if (id != dto.Id) return BadRequest();

        var genero = await _context.Generos.FindAsync(id);
        if (genero == null) return NotFound();

        genero.Nome = dto.Nome;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Remove um gênero pelo ID.
    /// </summary>
    /// <param name="id">ID do gênero.</param>
    /// <returns>NoContent ou NotFound.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var genero = await _context.Generos.FindAsync(id);
        if (genero == null) return NotFound();

        _context.Generos.Remove(genero);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
