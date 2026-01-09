using BackEnd_Intuit.Application.Services;
using BackEnd_Intuit.Application.Interfaces;
using BackEnd_Intuit.Application.DTOs;
using BackEnd_Intuit.Domain.Entities;
using BackEnd_Intuit.Domain.Exceptions;

namespace BackEnd_Intuit.Tests.Services
{
    public class ClienteServiceTests
    {
        private readonly Mock<IClienteRepository> _repositoryMock;
        private readonly ClienteService _service;

        public ClienteServiceTests()
        {
            _repositoryMock = new Mock<IClienteRepository>();
            _service = new ClienteService(_repositoryMock.Object);
        }

        private static Cliente CrearCliente()
        {
            return new Cliente(
                "Juan",
                "Perez",
                "Juan Perez SA",
                "20304050607",
                new DateTime(1990, 1, 1),
                "1123456789",
                "juan@test.com"
            );
        }

        [Fact]
        public async Task GetAllAsync_ReturnsClientes()
        {
            // Arrange
            var clientes = new List<Cliente>
            {
                CrearCliente(),
                CrearCliente()
            };

            _repositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(clientes);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetByIdAsync_WhenExists_ReturnsCliente()
        {
            var cliente = CrearCliente();

            _repositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(cliente);

            var result = await _service.GetByIdAsync(1);

            result.Email.Should().Be(cliente.Email);
        }

        [Fact]
        public async Task GetByIdAsync_WhenNotExists_ThrowsException()
        {
            _repositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync((Cliente?)null);

            Func<Task> act = async () => await _service.GetByIdAsync(1);

            await act.Should().ThrowAsync<DomainException>();
        }

        [Fact]
        public async Task CreateAsync_WhenValid_ReturnsId()
        {
            var dto = new ClienteCreateDto
            {
                Nombre = "Juan",
                Apellido = "Perez",
                RazonSocial = "JP SA",
                Cuit = "20304050607",
                FechaNacimiento = new DateTime(1990, 1, 1),
                TelefonoCelular = "1123456789",
                Email = "nuevo@test.com"
            };

            _repositoryMock.Setup(r => r.ExistsByCuitAsync(dto.Cuit)).ReturnsAsync(false);
            _repositoryMock.Setup(r => r.ExistsByEmailAsync(dto.Email)).ReturnsAsync(false);

            int id = 1;

            _repositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Cliente>()))
                .Callback<Cliente>(c =>
                {
                    typeof(Cliente)
                        .GetProperty("Id")!
                        .SetValue(c, id);
                })
                .Returns(Task.CompletedTask);

            var result = await _service.CreateAsync(dto);

            result.Should().Be(id);
        }

        [Fact]
        public async Task CreateAsync_WhenCuitExists_ThrowsException()
        {
            var dto = new ClienteCreateDto { Cuit = "20304050607", Email = "test@test.com" };

            _repositoryMock.Setup(r => r.ExistsByCuitAsync(dto.Cuit)).ReturnsAsync(true);

            Func<Task> act = async () => await _service.CreateAsync(dto);

            await act.Should().ThrowAsync<DomainException>();
        }

        [Fact]
        public async Task UpdateAsync_WhenValid_UpdatesCliente()
        {
            var cliente = CrearCliente();

            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(cliente);
            _repositoryMock.Setup(r => r.ExistsByEmailAsync(It.IsAny<string>())).ReturnsAsync(false);

            var dto = new ClienteUpdateDto
            {
                Nombre = "Nuevo",
                Apellido = "Nombre",
                TelefonoCelular = "119999999",
                Email = "nuevo@mail.com"
            };

            await _service.UpdateAsync(1, dto);

            cliente.Nombre.Should().Be("Nuevo");
        }

        [Fact]
        public async Task DeleteAsync_WhenExists_DeletesCliente()
        {
            var cliente = CrearCliente();

            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(cliente);

            await _service.DeleteAsync(1);

            _repositoryMock.Verify(r => r.DeleteAsync(cliente), Times.Once);
        }


    }
}
