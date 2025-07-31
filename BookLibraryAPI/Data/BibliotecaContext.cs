using BookLibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPI.Data
{
    public class BibliotecaContext : DbContext
    {
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }

        public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options) { }
    }
}
