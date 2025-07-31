using BookLibraryAPI.Data;
using BookLibraryAPI.DTOs;
using BookLibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BookLibrary.Tests
{
    public class GeneroControllerTests
    {
        private BibliotecaContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<BibliotecaContext>()
                .UseInMemoryDatabase(databaseName: "GeneroTestDb")
                .Options;

            return new BibliotecaContext(options);
        }

        [Fact]
        public async Task GetAll_ReturnsListOfGeneros()
        {
            var context = GetInMemoryContext();
            context.Generos.Add(new Genero { Nome = "Ficção" });
            context.Generos.Add(new Genero { Nome = "Romance" });
            await context.SaveChangesAsync();

            var controller = new GeneroController(context);
            var result = await controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var generos = Assert.IsType<List<GeneroDTO>>(okResult.Value);
            Assert.Equal(2, generos.Count);
        }

        [Fact]
        public async Task Create_ReturnsCreatedGenero()
        {
            var context = GetInMemoryContext();
            var controller = new GeneroController(context);
            var genero = new GeneroDTO { Nome = "Terror" };

            var result = await controller.Create(genero);

            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var generoCreated = Assert.IsType<GeneroDTO>(created.Value);
            Assert.True(generoCreated.Id > 0);
            Assert.Equal("Terror", generoCreated.Nome);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenGeneroDoesNotExist()
        {
            var controller = new GeneroController(GetInMemoryContext());
            var result = await controller.GetById(999);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdMismatch()
        {
            var controller = new GeneroController(GetInMemoryContext());
            var genero = new GeneroDTO { Id = 1, Nome = "Ação" };
            var result = await controller.Update(2, genero); // ids diferentes
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenGeneroDoesNotExist()
        {
            var controller = new GeneroController(GetInMemoryContext());
            var genero = new GeneroDTO { Id = 1, Nome = "Ação" };
            var result = await controller.Update(1, genero); // não existe no banco
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenGeneroDoesNotExist()
        {
            var controller = new GeneroController(GetInMemoryContext());
            var result = await controller.Delete(999);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
