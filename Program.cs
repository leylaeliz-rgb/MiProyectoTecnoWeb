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