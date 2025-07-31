using BookLibraryAPI.Data;
using BookLibraryAPI.DTOs;
using BookLibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Controlador para gerenciar autores.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AutorController : ControllerBase
{
    private readonly BibliotecaContext _context;

    public AutorController(BibliotecaContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retorna todos os autores cadastrados.
    /// </summary>
    /// <returns>Lista de autores.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AutorDTO>>> GetAll()
    {
        var autores = await _context.Autores
            .Select(a => new AutorDTO { Id = a.Id, Nome = a.Nome })
            .ToListAsync();

        return Ok(autores);
    }

    /// <summary>
    /// Retorna um autor pelo ID.
    /// </summary>
    /// <param name="id">ID do autor.</param>
    /// <returns>Autor encontrado ou NotFound.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AutorDTO>> GetById(int id)
    {
        var autor = await _context.Autores.FindAsync(id);
        if (autor == null) return NotFound();

        return Ok(new AutorDTO { Id = autor.Id, Nome = autor.Nome });
    }

    /// <summary>
    /// Cria um novo autor.
    /// </summary>
    /// <param name="dto">Dados do autor.</param>
    /// <returns>Autor criado.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<AutorDTO>> Create(AutorDTO dto)
    {
        var autor = new Autor { Nome = dto.Nome };
        _context.Autores.Add(autor);
        await _context.SaveChangesAsync();

        dto.Id = autor.Id;
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    /// <summary>
    /// Atualiza um autor existente.
    /// </summary>
    /// <param name="id">ID do autor.</param>
    /// <param name="dto">Novos dados do autor.</param>
    /// <returns>NoContent ou erro.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, AutorDTO dto)
    {
        if (id != dto.Id) return BadRequest();

        var autor = await _context.Autores.FindAsync(id);
        if (autor == null) return NotFound();

        autor.Nome = dto.Nome;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Remove um autor pelo ID.
    /// </summary>
    /// <param name="id">ID do autor.</param>
    /// <returns>NoContent ou NotFound.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var autor = await _context.Autores.FindAsync(id);
        if (autor == null) return NotFound();

        _context.Autores.Remove(autor);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
