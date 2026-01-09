using BackEnd_Intuit.Domain.Entities;

namespace BackEnd_Intuit.Application.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task<Cliente?> GetByIdAsync(int id);
        Task<IEnumerable<Cliente>> SearchByNombreAsync(string nombre);
        Task AddAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
        Task DeleteAsync(Cliente cliente);
        Task<bool> ExistsByIdAsync(int id);
        Task<bool> ExistsByCuitAsync(string cuit);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
