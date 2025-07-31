using BookLibraryAPI.Data;
using BookLibraryAPI.DTOs;
using BookLibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BookLibrary.Tests
{
    public class LivroControllerTests
    {
        private BibliotecaContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<BibliotecaContext>()
                .UseInMemoryDatabase(databaseName: "LivroTestDb")
                .Options;

            return new BibliotecaContext(options);
        }

        [Fact]
        public async Task GetAll_ReturnsListOfLivros()
        {
            var context = GetInMemoryContext();

            context.Autores.Add(new Autor { Id = 1, Nome = "Autor 1" });
            context.Generos.Add(new Genero { Id = 1, Nome = "Genero 1" });
            context.Livros.Add(new Livro { Titulo = "Livro A", AutorId = 1, GeneroId = 1 });
            context.Livros.Add(new Livro { Titulo = "Livro B", AutorId = 1, GeneroId = 1 });
            await context.SaveChangesAsync();

            var controller = new LivroController(context);
            var result = await controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var livros = Assert.IsType<List<LivroDTO>>(okResult.Value);
            Assert.Equal(2, livros.Count);
        }

        [Fact]
        public async Task Create_ReturnsCreatedLivro()
        {
            var context = GetInMemoryContext();
            context.Autores.Add(new Autor { Id = 1, Nome = "Autor Teste" });
            context.Generos.Add(new Genero { Id = 1, Nome = "Genero Teste" });
            await context.SaveChangesAsync();

            var controller = new LivroController(context);
            var dto = new LivroDTO
            {
                Titulo = "Novo Livro",
                AutorId = 1,
                GeneroId = 1
            };

            var result = await controller.Create(dto);

            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var livro = Assert.IsType<LivroDTO>(created.Value);
            Assert.True(livro.Id > 0);
            Assert.Equal("Novo Livro", livro.Titulo);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenLivroDoesNotExist()
        {
            var controller = new LivroController(GetInMemoryContext());
            var result = await controller.GetById(999);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdMismatch()
        {
            var controller = new LivroController(GetInMemoryContext());
            var dto = new LivroDTO { Id = 1, Titulo = "Livro", AutorId = 1, GeneroId = 1 };
            var result = await controller.Update(2, dto); // ids diferentes
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenLivroDoesNotExist()
        {
            var controller = new LivroController(GetInMemoryContext());
            var dto = new LivroDTO { Id = 1, Titulo = "Livro", AutorId = 1, GeneroId = 1 };
            var result = await controller.Update(1, dto); // livro nao existe no db
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenLivroDoesNotExist()
        {
            var controller = new LivroController(GetInMemoryContext());
            var result = await controller.Delete(999);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
