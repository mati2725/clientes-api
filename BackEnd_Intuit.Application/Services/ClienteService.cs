using BackEnd_Intuit.Application.DTOs;
using BackEnd_Intuit.Application.Interfaces;
using BackEnd_Intuit.Domain.Entities;
using BackEnd_Intuit.Domain.Exceptions;

namespace BackEnd_Intuit.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ClienteDto>> GetAllAsync()
        {
            var clientes = await _repository.GetAllAsync();
            return clientes.Select(MapToDto);
        }

        public async Task<ClienteDto> GetByIdAsync(int id)
        {
            var cliente = await _repository.GetByIdAsync(id);

            if (cliente is null)
                throw new DomainException("Cliente no encontrado.");

            return MapToDto(cliente);
        }

        public async Task<IEnumerable<ClienteDto>> SearchAsync(string nombre)
        {
            var clientes = await _repository.SearchByNombreAsync(nombre);
            return clientes.Select(MapToDto);
        }

        public async Task<int> CreateAsync(ClienteCreateDto dto)
        {
            if (await _repository.ExistsByCuitAsync(dto.Cuit))
                throw new DomainException("El CUIT ya existe.");

            if (await _repository.ExistsByEmailAsync(dto.Email))
                throw new DomainException("El email ya existe.");

            var cliente = new Cliente(
                dto.Nombre,
                dto.Apellido,
                dto.RazonSocial,
                dto.Cuit,
                dto.FechaNacimiento,
                dto.TelefonoCelular,
                dto.Email
            );

            await _repository.AddAsync(cliente);
            return cliente.Id;
        }

        public async Task UpdateAsync(int id, ClienteUpdateDto dto)
        {
            var cliente = await _repository.GetByIdAsync(id);

            if (cliente is null)
                throw new DomainException("Cliente no encontrado.");

            if (cliente.Email != dto.Email &&
                await _repository.ExistsByEmailAsync(dto.Email))
                throw new DomainException("El email ya existe.");

            cliente.Actualizar(
                dto.Nombre,
                dto.Apellido,
                dto.TelefonoCelular,
                dto.Email
            );

            await _repository.UpdateAsync(cliente);
        }

        public async Task DeleteAsync(int id)
        {
            var cliente = await _repository.GetByIdAsync(id);

            if (cliente is null)
                throw new DomainException("Cliente no encontrado.");

            await _repository.DeleteAsync(cliente);
        }

        private static ClienteDto MapToDto(Cliente cliente)
        {
            return new ClienteDto
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                RazonSocial = cliente.RazonSocial,
                Cuit = cliente.Cuit,
                FechaNacimiento = cliente.FechaNacimiento,
                TelefonoCelular = cliente.TelefonoCelular,
                Email = cliente.Email
            };
        }
    }
}
