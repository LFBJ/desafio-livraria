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
    public class AutorControllerTests
    {
        private BibliotecaContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<BibliotecaContext>()
                .UseInMemoryDatabase(databaseName: "AutorTestDb")
                .Options;

            return new BibliotecaContext(options);
        }

        [Fact]
        public async Task GetAll_ReturnsListOfAutores()
        {
            var context = GetInMemoryContext();
            context.Autores.Add(new Autor { Nome = "Machado de Assis" });
            context.Autores.Add(new Autor { Nome = "Clarice Lispector" });
            await context.SaveChangesAsync();

            var controller = new AutorController(context);
            var result = await controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var autores = Assert.IsType<List<AutorDTO>>(okResult.Value);
            Assert.Equal(2, autores.Count);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAutor()
        {
            var context = GetInMemoryContext();
            var controller = new AutorController(context);
            var dto = new AutorDTO { Nome = "José Saramago" };

            var result = await controller.Create(dto);

            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var autor = Assert.IsType<AutorDTO>(created.Value);
            Assert.True(autor.Id > 0);
            Assert.Equal("José Saramago", autor.Nome);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenAutorDoesNotExist()
        {
            var controller = new AutorController(GetInMemoryContext());
            var result = await controller.GetById(999);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdMismatch()
        {
            var controller = new AutorController(GetInMemoryContext());
            var dto = new AutorDTO { Id = 1, Nome = "Autor" };
            var result = await controller.Update(2, dto); // ids diferentes
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenAutorDoesNotExist()
        {
            var controller = new AutorController(GetInMemoryContext());
            var dto = new AutorDTO { Id = 1, Nome = "Autor" };
            var result = await controller.Update(1, dto); // autor nao existe
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenAutorDoesNotExist()
        {
            var controller = new AutorController(GetInMemoryContext());
            var result = await controller.Delete(999);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
