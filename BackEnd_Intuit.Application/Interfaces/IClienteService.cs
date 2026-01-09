using BackEnd_Intuit.Application.DTOs;

namespace BackEnd_Intuit.Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDto>> GetAllAsync();
        Task<ClienteDto> GetByIdAsync(int id);
        Task<IEnumerable<ClienteDto>> SearchAsync(string nombre);
        Task<int> CreateAsync(ClienteCreateDto dto);
        Task UpdateAsync(int id, ClienteUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
