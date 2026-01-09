using System;
using System.Text.RegularExpressions;
using BackEnd_Intuit.Domain.Exceptions;

namespace BackEnd_Intuit.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; }
        public string Nombre { get; private set; } = null!;
        public string Apellido { get; private set; } = null!;
        public string RazonSocial { get; private set; } = null!;
        public string Cuit { get; private set; } = null!;
        public DateTime FechaNacimiento { get; private set; }
        public string TelefonoCelular { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public DateTime FechaCreacion { get; private set; }
        public DateTime FechaModificacion { get; private set; }

        protected Cliente() { }

        public Cliente(
            string nombre,
            string apellido,
            string razonSocial,
            string cuit,
            DateTime fechaNacimiento,
            string telefonoCelular,
            string email)
        {
            Validar(nombre, apellido, razonSocial, cuit, fechaNacimiento, telefonoCelular, email);

            Nombre = nombre.Trim();
            Apellido = apellido.Trim();
            RazonSocial = razonSocial.Trim();
            Cuit = cuit.Trim();
            FechaNacimiento = fechaNacimiento;
            TelefonoCelular = telefonoCelular.Trim();
            Email = email.Trim().ToLower();

            FechaCreacion = DateTime.UtcNow;
            FechaModificacion = DateTime.UtcNow;
        }

        public void Actualizar(
            string nombre,
            string apellido,
            string telefonoCelular,
            string email)
        {
            ValidarCamposActualizables(nombre, apellido, telefonoCelular, email);

            Nombre = nombre.Trim();
            Apellido = apellido.Trim();
            TelefonoCelular = telefonoCelular.Trim();
            Email = email.Trim().ToLower();
            FechaModificacion = DateTime.UtcNow;
        }

        private static void Validar(
            string nombre,
            string apellido,
            string razonSocial,
            string cuit,
            DateTime fechaNacimiento,
            string telefonoCelular,
            string email)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new DomainException("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(apellido))
                throw new DomainException("El apellido es obligatorio.");

            if (string.IsNullOrWhiteSpace(razonSocial))
                throw new DomainException("La razón social es obligatoria.");

            if (!EsCuitValido(cuit))
                throw new DomainException("El CUIT no tiene un formato válido.");

            if (fechaNacimiento >= DateTime.Today)
                throw new DomainException("La fecha de nacimiento no es válida.");

            if (string.IsNullOrWhiteSpace(telefonoCelular))
                throw new DomainException("El teléfono celular es obligatorio.");

            if (!EsEmailValido(email))
                throw new DomainException("El email no tiene un formato válido.");
        }

        private static void ValidarCamposActualizables(
            string nombre,
            string apellido,
            string telefonoCelular,
            string email)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new DomainException("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(apellido))
                throw new DomainException("El apellido es obligatorio.");

            if (string.IsNullOrWhiteSpace(telefonoCelular))
                throw new DomainException("El teléfono celular es obligatorio.");

            if (!EsEmailValido(email))
                throw new DomainException("El email no tiene un formato válido.");
        }

        private static bool EsEmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            return Regex.IsMatch(
                email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase);
        }

        private static bool EsCuitValido(string cuit)
        {
            if (string.IsNullOrWhiteSpace(cuit)) return false;

            // Formato simple: 11 dígitos (ej: 20304050607)
            return Regex.IsMatch(cuit, @"^\d{11}$");
        }
    }
}
