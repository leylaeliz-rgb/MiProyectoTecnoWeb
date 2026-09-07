interface IPrestable
{
    void Prestar();
    void Devolver();
}
class LibroNoDisponibleException : Exception
{
    public LibroNoDisponibleException(string mensaje) : base(mensaje) { }
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
            throw new LibroNoDisponibleException($"El libro '{Titulo}' ya está prestado.");
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
    private readonly string[] categoriasValidas =
{
    "Ficción", "No Ficción", "Ciencia", "Historia", "Tecnología", "Infantil"
};

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

    // LIBROS DISPONIBLES

    public void MostrarLibrosDisponibles()
    {
        Console.WriteLine("\n--- LIBROS DISPONIBLES ---");

        var disponibles = libros
            .Where(l => l.Disponibilidad)
            .ToList();

        if (disponibles.Count == 0)
        {
            Console.WriteLine("No hay libros disponibles.");
            return;
        }

        foreach (Libro libro in disponibles)
        {
            libro.MostrarInformacion();
        }
    }


    // BUSCAR POR AUTOR

    public void BuscarPorAutor()
    {
        Console.WriteLine("\n--- BUSCAR POR AUTOR ---");

        Console.Write("Ingrese el autor: ");
        string autor = Console.ReadLine()!;

        var resultados = libros
            .Where(l => l.Autor.ToLower().Contains(autor.ToLower()))
            .ToList();

        if (resultados.Count == 0)
        {
            Console.WriteLine("No se encontraron libros de ese autor.");
            return;
        }

        foreach (Libro libro in resultados)
        {
            libro.MostrarInformacion();
        }
    }


    // LIBROS ORDENADOS

    public void ListarOrdenados()
    {
        Console.WriteLine("\n--- LIBROS ORDENADOS POR TÍTULO ---");

        var resultados = libros
            .OrderBy(l => l.Titulo)
            .ToList();

        foreach (Libro libro in resultados)
        {
            libro.MostrarInformacion();
        }
    }



    // PRESTAR LIBRO
    public void PrestarLibro()
    {
        Console.WriteLine("\n--- PRESTAR LIBRO ---");

        Console.Write("Código del libro: ");
        string codigoLibro = Console.ReadLine()!;

        Console.Write("Identificador del usuario: ");
        string identificadorUsuario = Console.ReadLine()!;

        Libro? libro = libros.FirstOrDefault(l => l.Codigo == codigoLibro);

        if (libro == null)
        {
            Console.WriteLine("Error: el libro no existe.");
            return;
        }

        Usuario? usuario = usuarios.FirstOrDefault(
            u => u.Identificador == identificadorUsuario
        );

        if (usuario == null)
        {
            Console.WriteLine("Error: el usuario no existe.");
            return;
        }

        try
        {
            libro.Prestar();
        }
        catch (LibroNoDisponibleException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return;
        }

        Prestamo nuevoPrestamo = new Prestamo(
            codigoLibro,
            identificadorUsuario,
            DateTime.Now,
            null
        );

        prestamos.Add(nuevoPrestamo);

        Console.WriteLine("Préstamo registrado correctamente.");
    }

    // DEVOLVER LIBRO

    public void DevolverLibro()
    {
        Console.WriteLine("\n--- DEVOLVER LIBRO ---");

        Console.Write("Código del libro: ");
        string codigoLibro = Console.ReadLine()!;

        Prestamo? prestamo = prestamos
            .FirstOrDefault(p =>
                p.CodigoLibro == codigoLibro &&
                p.FechaDevolucion == null
            );

        if (prestamo == null)
        {
            Console.WriteLine("No existe un préstamo activo para ese libro.");
            return;
        }

        Libro? libro = libros.FirstOrDefault(
            l => l.Codigo == codigoLibro
        );

        if (libro == null)
        {
            Console.WriteLine("El libro no existe.");
            return;
        }

        libro.Devolver();

        // Crear un nuevo record con la fecha de devolución
        Prestamo prestamoActualizado = prestamo with
        {
            FechaDevolucion = DateTime.Now
        };

        prestamos.Remove(prestamo);
        prestamos.Add(prestamoActualizado);

        Console.WriteLine("Libro devuelto correctamente.");
    }

    // PRÉSTAMOS ACTIVOS

    public void MostrarPrestamosActivos()
    {
        Console.WriteLine("\n--- PRÉSTAMOS ACTIVOS ---");

        var activos = prestamos
            .Where(p => p.FechaDevolucion == null)
            .Select(p => new
            {
                CodigoLibro = p.CodigoLibro,
                Usuario = p.IdentificadorUsuario,
                Fecha = p.FechaPrestamo
            })
            .ToList();

        if (activos.Count == 0)
        {
            Console.WriteLine("No existen préstamos activos.");
            return;
        }

        foreach (var prestamo in activos)
        {
            Console.WriteLine(
                $"Libro: {prestamo.CodigoLibro} | " +
                $"Usuario: {prestamo.Usuario} | " +
                $"Fecha: {prestamo.Fecha}"
            );
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        BibliotecaService bibliotecaPepito = new BibliotecaService();

        bool continuar = true;

        while (continuar)
        {
            Console.WriteLine("\n==============================");
            Console.WriteLine("      BIBLIOTECA PEPITO");
            Console.WriteLine("==============================");
            Console.WriteLine("1. Registrar libro");
            Console.WriteLine("2. Registrar usuario");
            Console.WriteLine("3. Listar libros");
            Console.WriteLine("4. Buscar libro por código");
            Console.WriteLine("5. Eliminar libro");
            Console.WriteLine("6. Mostrar libros disponibles");
            Console.WriteLine("7. Buscar libros por autor");
            Console.WriteLine("8. Listar libros ordenados");
            Console.WriteLine("9. Prestar libro");
            Console.WriteLine("10. Devolver libro");
            Console.WriteLine("11. Mostrar préstamos activos");
            Console.WriteLine("0. Salir");
            Console.WriteLine("==============================");

            Console.Write("Seleccione una opción: ");

            try
            {
                int opcion = int.Parse(Console.ReadLine()!);

                switch (opcion)
                {
                    case 1:
                        bibliotecaPepito.RegistrarLibro();
                        break;

                    case 2:
                        bibliotecaPepito.RegistrarUsuario();
                        break;

                    case 3:
                        bibliotecaPepito.ListarLibros();
                        break;

                    case 4:
                        bibliotecaPepito.BuscarLibro();
                        break;

                    case 5:
                        bibliotecaPepito.EliminarLibro();
                        break;

                    case 6:
                        bibliotecaPepito.MostrarLibrosDisponibles();
                        break;

                    case 7:
                        bibliotecaPepito.BuscarPorAutor();
                        break;

                    case 8:
                        bibliotecaPepito.ListarOrdenados();
                        break;

                    case 9:
                        bibliotecaPepito.PrestarLibro();
                        break;

                    case 10:
                        bibliotecaPepito.DevolverLibro();
                        break;

                    case 11:
                        bibliotecaPepito.MostrarPrestamosActivos();
                        break;

                    case 0:
                        continuar = false;
                        Console.WriteLine("Programa finalizado.");
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: debe ingresar un número válido.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}