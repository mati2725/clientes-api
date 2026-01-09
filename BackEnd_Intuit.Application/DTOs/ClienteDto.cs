namespace BackEnd_Intuit.Application.DTOs
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string RazonSocial { get; set; } = null!;
        public string Cuit { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string TelefonoCelular { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
