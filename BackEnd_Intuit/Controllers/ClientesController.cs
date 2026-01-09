using Microsoft.AspNetCore.Mvc;
using BackEnd_Intuit.Application.Interfaces;
using BackEnd_Intuit.Application.DTOs;
using BackEnd_Intuit.Domain.Entities;

namespace BackEnd_Intuit.Api.Controllers
{
    /// <summary>
    /// Controlador para la gestión de clientes.
    /// </summary>
    [ApiController]
    [Route("api/clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClientesController(IClienteService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtiene todos los clientes.
        /// </summary>
        /// <response code="200">Listado de clientes obtenido correctamente</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _service.GetAllAsync();
            return Ok(clientes);
        }

        /// <summary>
        /// Obtiene un cliente por su identificador.
        /// </summary>
        /// <param name="id">Id del cliente</param>
        /// <response code="200">Cliente encontrado</response>
        /// <response code="404">Cliente no encontrado</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _service.GetByIdAsync(id);
            return Ok(cliente);
        }

        /// <summary>
        /// Busca clientes por nombre o apellido.
        /// </summary>
        /// <param name="nombre">Texto a buscar</param>
        /// <response code="200">Clientes encontrados</response>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Search([FromQuery] string nombre)
        {
            var clientes = await _service.SearchAsync(nombre);
            return Ok(clientes);
        }

        /// <summary>
        /// Crea un nuevo cliente.
        /// </summary>
        /// <response code="201">Cliente creado correctamente</response>
        /// <response code="400">Datos inválidos</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ClienteCreateDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }

        /// <summary>
        /// Actualiza un cliente existente.
        /// </summary>
        /// <param name="id">Id del cliente</param>
        /// <response code="204">Cliente actualizado correctamente</response>
        /// <response code="404">Cliente no encontrado</response>
        /// <response code="400">Datos inválidos</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] ClienteUpdateDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return Ok(dto);
        }

        /// <summary>
        /// Elimina un cliente.
        /// </summary>
        /// <param name="id">Id del cliente</param>
        /// <response code="204">Cliente eliminado correctamente</response>
        /// <response code="404">Cliente no encontrado</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
