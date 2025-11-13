namespace App_Usuario_.Models
{
    // Esta clase representa la entidad Usuario que se guarda en la base de datos
    // Cada propiedad se corresponde con una columna en la tabla "Usuario"
    public partial class Usuario
    {
        // Identificador unico del usuario (clave primaria)
        public int Id { get; set; }

        // Nombres del usuario (campo obligatorio)
        public string Nombres { get; set; } = null!;

        // Apellidos del usuario (campo obligatorio)
        public string Apellidos { get; set; } = null!;

        // Correo electronico del usuario
        public string Correo { get; set; } = null!;

        // Nombre de usuario (puede ser usado para login o identificacion)
        public string Username { get; set; } = null!;

        // Fecha en la que el usuario fue creado (puede ser nula)
        public DateTime? FechaCreacion { get; set; }
    }
}

