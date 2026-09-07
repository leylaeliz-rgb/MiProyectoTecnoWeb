# Sistema de Gestión de Biblioteca

Aplicación de consola desarrollada en C# y .NET 10 para la materia Tec Web 1.
Permite administrar el catálogo de libros, los usuarios registrados y los préstamos
de una biblioteca, mediante un menú interactivo.

## Funcionalidades

- Registrar libros (título, autor, categoría, código, disponibilidad)
- Registrar usuarios (identificador, nombre, correo)
- Listar, buscar y eliminar libros
- Registrar préstamos y devoluciones
- Consultar libros disponibles y préstamos activos
- Búsqueda por autor y listado ordenado por título
- Validación de datos con manejo de excepciones (no cierra el programa ante errores)

## Ambiente en el cual se trabaja

- Visual Studio con el paquete "Desarrollo de escritorio de .NET" instalado

## Requisitos
- [.NET 10 SDK](https://dotnet.microsoft.com/download) instalado

## Cómo ejecutarlo

1. Cloná el repositorio:

git clone https://github.com/leylaeliz-rgb/MiProyectoTecnoWeb.git

2. Entrá a la carpeta del proyecto:

cd MiProyectoTecnoWeb

3. Ejecutá el programa:

dotnet run

4. Navegá el menú ingresando el número de la opción deseada. Elegí `0` para salir.

## Estructura del proyecto

- **`Libro`**: representa un libro del catálogo. Implementa `IPrestable` para manejar su propio ciclo de préstamo/devolución.
- **`Usuario`**: representa a un usuario registrado en la biblioteca.
- **`Prestamo`** (record): representa un préstamo realizado, con fecha de préstamo y devolución (nula si sigue activo).
- **`IPrestable`**: interfaz que define el contrato de préstamo/devolución.
- **`BibliotecaService`**: contiene la lógica de negocio (registrar, buscar, prestar, devolver, consultas LINQ).
- **`LibroNoDisponibleException`, `LibroPrestadoException`, `ElementoDuplicadoException`**: excepciones propias para validar reglas de negocio.
- **`Program`**: punto de entrada con el menú de consola.

## Autor

Leyla (leylaeliz-rgb)
