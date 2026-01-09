using Microsoft.EntityFrameworkCore;
using BackEnd_Intuit.Application.Interfaces;
using BackEnd_Intuit.Domain.Entities;
using BackEnd_Intuit.Infrastructure.Data;
using MySqlConnector;

namespace BackEnd_Intuit.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _context.Clientes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<IEnumerable<Cliente>> SearchByNombreAsync(string nombre)
        {
            var parametro = new MySqlParameter("p_nombre", nombre);

            return await _context.Clientes
                .FromSqlRaw("CALL sp_search_clientes(@p_nombre)", parametro)
                .AsNoTracking()
                .ToListAsync();
        }


        public async Task AddAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Cliente cliente)
        {
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await _context.Clientes.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> ExistsByCuitAsync(string cuit)
        {
            return await _context.Clientes.AnyAsync(c => c.Cuit == cuit);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Clientes.AnyAsync(c => c.Email == email);
        }
    }
}
