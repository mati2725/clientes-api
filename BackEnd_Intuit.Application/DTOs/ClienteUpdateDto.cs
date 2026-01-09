namespace BackEnd_Intuit.Application.DTOs
{
    public class ClienteUpdateDto
    {
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string TelefonoCelular { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
