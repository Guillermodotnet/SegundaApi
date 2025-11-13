using Microsoft.AspNetCore.Mvc; // Importo el espacio de nombres para crear controladores y manejar peticiones HTTP
using Microsoft.EntityFrameworkCore; // Uso Entity Framework Core para acceder a la base de datos de forma asincrona
using App_Usuario_.Data; // Importo mi contexto de base de datos DbUsuarioContext
using App_Usuario_.Models; // Importo mi modelo Usuario

namespace App_Usuario_.Controllers
{
    // Defino la ruta base del controlador. "api/[controller]" usa el nombre del controlador automaticamente
    [Route("api/[controller]")]
    [ApiController] // Indico que esta clase es un controlador de tipo API
    public class UsuariosController : ControllerBase
    {
        private readonly DbUsuarioContext _context; // Creo una variable privada para acceder al contexto de la base de datos

        // En el constructor inyecto mi contexto para poder usarlo en los metodos del controlador
        public UsuariosController(DbUsuarioContext context)
        {
            _context = context;
        }

        // --- Obtener todos los usuarios ---
        // GET: api/Usuarios/listar
        [HttpGet("listar")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            // Obtengo la lista completa de usuarios desde la base de datos
            var usuarios = await _context.Usuarios.ToListAsync();

            // Si la lista esta vacia, devuelvo un codigo 204 No Content
            if (usuarios == null || !usuarios.Any())
            {
                return NoContent();
            }

            // Devuelvo la lista con codigo 200 OK
            return Ok(usuarios);
        }

        // --- Obtener un usuario por su id ---
        // GET: api/Usuarios/buscar/5
        [HttpGet("buscar/{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            // Busco un usuario por su id
            var usuario = await _context.Usuarios.FindAsync(id);

            // Si no existe, devuelvo un 404 Not Found
            if (usuario == null)
            {
                return NotFound();
            }

            // Si lo encuentra, lo devuelvo con codigo 200 OK
            return Ok(usuario);
        }

        // --- Actualizar un usuario existente ---
        // PUT: api/Usuarios/actualizar/5
        [HttpPut("actualizar{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            // Busco el usuario a actualizar
            var usuarioActualizado = await _context.Usuarios.FindAsync(id);

            // Si no existe, devuelvo un 404 Not Found
            if (usuarioActualizado == null)
            {
                return NotFound();
            }

            // Actualizo las propiedades del usuario con los nuevos valores recibidos
            usuarioActualizado.Nombres = usuario.Nombres;
            usuarioActualizado.Apellidos = usuario.Apellidos;
            usuarioActualizado.Correo = usuario.Correo;
            usuarioActualizado.Username = usuario.Username;

            // Guardo los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Devuelvo el usuario actualizado con codigo 200 OK
            return Ok(usuarioActualizado);
        }

        // --- Crear un nuevo usuario ---
        // POST: api/Usuarios/guardar
        [HttpPost("guardar")]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            // Asigno la fecha de creacion al momento actual
            usuario.FechaCreacion = DateTime.Now;

            // Agrego el usuario al contexto para que se guarde en la base de datos
            _context.Usuarios.Add(usuario);

            // Guardo los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Devuelvo el usuario creado con codigo 201 Created
            return StatusCode(StatusCodes.Status201Created, usuario);
        }

        // --- Eliminar un usuario por su id ---
        // DELETE: api/Usuarios/eliminar/5
        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            // Busco el usuario que quiero eliminar
            var usuario = await _context.Usuarios.FindAsync(id);

            // Si no existe, devuelvo 404 Not Found
            if (usuario == null)
            {
                return NotFound();
            }

            // Elimino el usuario del contexto
            _context.Usuarios.Remove(usuario);

            // Guardo los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Devuelvo un 204 No Content para indicar que se elimino correctamente
            return NoContent();
        }
    }
}

