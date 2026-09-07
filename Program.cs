interface IPrestable
{
    void Prestar();
    void Devolver();
}
class Libro : IPrestable
{
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string Categoria { get; set; }
    public string Codigo { get; set; }
    public bool Disponibilidad { get; set; }

    public Libro(string titulo, string autor, string categoria, string codigo, bool disponibilidad)
    {
        Titulo = titulo;
        Autor = autor;
        Categoria = categoria;
        Codigo = codigo;
        Disponibilidad = disponibilidad;
    }
    public void Prestar()
    {
        if (!Disponibilidad)
        {
            throw new Exception("El libro ya fue prestado.");
        }

        Disponibilidad = false;
    }
    public void Devolver()
    {
        Disponibilidad = true;
    }
    public void MostrarInformacion()
    {
        Console.WriteLine($"{Titulo} - Escrito por: {Autor} ({Categoria},{Codigo}), Estado: ");
        if (Disponibilidad)
            {
            Console.WriteLine("Disponible");
            }
        else
            {
            Console.WriteLine("No disponible");
            }
    }
}

class Usuario
{
    public string Identificador { get; set; }
    public string Nombre { get; set; }
    public string Correo { get; set; }

    public Usuario(string identificador, string nombre, string correo)
    {
        Identificador = identificador;
        Nombre = nombre;
        Correo = correo;
    }
    public void MostrarInformacion()
    {
        Console.WriteLine($"ID: {Identificador} | Nombre: {Nombre} | Correo: {Correo}");
    }
}

record Prestamo(
    string CodigoLibro,
    string IdentificadorUsuario,
    DateTime FechaPrestamo,
    DateTime? FechaDevolucion
);

class BibliotecaService
{
    private List<Libro> libros = new List<Libro>();
    private List<Usuario> usuarios = new List<Usuario>();
    private List<Prestamo> prestamos = new List<Prestamo>();

    // REGISTRAR LIBRO
    public void RegistrarLibro()
    {
        Console.WriteLine("\n--- REGISTRAR LIBRO ---");

        Console.Write("Título: ");
        string titulo = Console.ReadLine()!;

        Console.Write("Autor: ");
        string autor = Console.ReadLine()!;

        Console.Write("Categoría: ");
        string categoria = Console.ReadLine()!;

        Console.Write("Código: ");
        string codigo = Console.ReadLine()!;

        // Verificar que el código sea único
        if (libros.Any(l => l.Codigo == codigo))
        {
            Console.WriteLine("Error: ya existe un libro con ese código.");
            return;
        }

        Libro libro = new Libro(
            titulo,
            autor,
            categoria,
            codigo,
            true
        );

        libros.Add(libro);

        Console.WriteLine("Libro registrado correctamente.");
    }

    // REGISTRAR USUARIO

    public void RegistrarUsuario()
    {
        Console.WriteLine("\n--- REGISTRAR USUARIO ---");

        Console.Write("Identificador: ");
        string identificador = Console.ReadLine()!;

        // Verificar identificador único
        if (usuarios.Any(u => u.Identificador == identificador))
        {
            Console.WriteLine("Error: ya existe un usuario con ese identificador.");
            return;
        }

        Console.Write("Nombre: ");
        string nombre = Console.ReadLine()!;

        Console.Write("Correo: ");
        string correo = Console.ReadLine()!;

        Usuario usuario = new Usuario(
            identificador,
            nombre,
            correo
        );

        usuarios.Add(usuario);

        Console.WriteLine("Usuario registrado correctamente.");
    }


    // LISTAR LIBROS

    public void ListarLibros()
    {
        Console.WriteLine("\n--- LISTA DE LIBROS ---");

        if (libros.Count == 0)
        {
            Console.WriteLine("No hay libros registrados.");
            return;
        }

        foreach (Libro libro in libros)
        {
            libro.MostrarInformacion();
        }
    }


    // BUSCAR LIBRO POR CÓDIGO

    public void BuscarLibro()
    {
        Console.WriteLine("\n--- BUSCAR LIBRO ---");

        Console.Write("Ingrese el código: ");
        string codigo = Console.ReadLine()!;

        Libro? libro = libros.FirstOrDefault(l => l.Codigo == codigo);

        if (libro == null)
        {
            Console.WriteLine("No se encontró ningún libro con ese código.");
            return;
        }

        libro.MostrarInformacion();
    }

    // ELIMINAR LIBRO
    public void EliminarLibro()
    {
        Console.WriteLine("\n--- ELIMINAR LIBRO ---");

        Console.Write("Ingrese el código del libro: ");
        string codigo = Console.ReadLine()!;

        Libro? libro = libros.FirstOrDefault(l => l.Codigo == codigo);

        if (libro == null)
        {
            Console.WriteLine("No se encontró el libro.");
            return;
        }

        if (!libro.Disponibilidad)
        {
            Console.WriteLine("No se puede eliminar un libro que está prestado.");
            return;
        }

        libros.Remove(libro);

        Console.WriteLine("Libro eliminado correctamente.");
    }
}