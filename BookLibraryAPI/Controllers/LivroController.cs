using BookLibraryAPI.Data;
using BookLibraryAPI.DTOs;
using BookLibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Controlador para gerenciar livros.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class LivroController : ControllerBase
{
    private readonly BibliotecaContext _context;

    public LivroController(BibliotecaContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retorna todos os livros cadastrados.
    /// </summary>
    /// <returns>Lista de livros.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LivroDTO>>> GetAll()
    {
        var livros = await _context.Livros
            .Select(l => new LivroDTO
            {
                Id = l.Id,
                Titulo = l.Titulo,
                GeneroId = l.GeneroId,
                AutorId = l.AutorId
            })
            .ToListAsync();

        return Ok(livros);
    }

    /// <summary>
    /// Retorna um livro pelo ID.
    /// </summary>
    /// <param name="id">ID do livro.</param>
    /// <returns>Livro encontrado ou NotFound.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LivroDTO>> GetById(int id)
    {
        var livro = await _context.Livros.FindAsync(id);
        if (livro == null) return NotFound();

        return Ok(new LivroDTO
        {
            Id = livro.Id,
            Titulo = livro.Titulo,
            GeneroId = livro.GeneroId,
            AutorId = livro.AutorId
        });
    }

    /// <summary>
    /// Cria um novo livro.
    /// </summary>
    /// <param name="dto">Dados do livro.</param>
    /// <returns>Livro criado.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<LivroDTO>> Create(LivroDTO dto)
    {
        var livro = new Livro
        {
            Titulo = dto.Titulo,
            GeneroId = dto.GeneroId,
            AutorId = dto.AutorId
        };

        _context.Livros.Add(livro);
        await _context.SaveChangesAsync();

        dto.Id = livro.Id;
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    /// <summary>
    /// Atualiza um livro existente.
    /// </summary>
    /// <param name="id">ID do livro.</param>
    /// <param name="dto">Novos dados do livro.</param>
    /// <returns>NoContent ou erro.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, LivroDTO dto)
    {
        if (id != dto.Id) return BadRequest();

        var livro = await _context.Livros.FindAsync(id);
        if (livro == null) return NotFound();

        livro.Titulo = dto.Titulo;
        livro.GeneroId = dto.GeneroId;
        livro.AutorId = dto.AutorId;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Remove um livro pelo ID.
    /// </summary>
    /// <param name="id">ID do livro.</param>
    /// <returns>NoContent ou NotFound.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var livro = await _context.Livros.FindAsync(id);
        if (livro == null) return NotFound();

        _context.Livros.Remove(livro);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
