
class Libro
{
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string Categoria { get; set; }
    public string Codigo{get; set;}
    public bool Disponibilidad { get; set; }

    public Libro (string titulo, string autor, string categoria, string codigo, bool disponibilidad)
    {
        Titulo = titulo;
        Autor = autor;
        Categoria = categoria;
        Codigo = codigo;
        disponibilidad = true;
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
