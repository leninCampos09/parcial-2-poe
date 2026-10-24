using Microsoft.Data.SqlClient;
using RepoDb;
using SistemaRegistroNotas.Clases;
using System.Configuration;

namespace SistemaRegistroNotas.Repositorios;

/// <summary>
/// Repositorio genérico con operaciones CRUD usando RepoDB.
/// </summary>
public class RepositorioGenerico<T> where T : class
{
    private readonly string _cadenaConexion;

    public RepositorioGenerico()
    {
        _cadenaConexion = ConfigurationManager.ConnectionStrings["conexionBD"]?.ConnectionString
            ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'conexionBD' en App.config.");
    }

    private SqlConnection CrearConexion() => new(_cadenaConexion);

    public IEnumerable<T> ObtenerTodos()
    {
        using var conexion = CrearConexion();
        conexion.Open();
        return conexion.QueryAll<T>().ToList();
    }

    public T? ObtenerPorId(int id)
    {
        using var conexion = CrearConexion();
        conexion.Open();
        return conexion.Query<T>(new { Id = id }).FirstOrDefault();
    }

    public int Insertar(T entidad)
    {
        using var conexion = CrearConexion();
        conexion.Open();
        return conexion.Insert<T, int>(entidad);
    }

    public int Actualizar(T entidad)
    {
        using var conexion = CrearConexion();
        conexion.Open();
        return conexion.Update<T>(entidad);
    }

    public int Eliminar(int id)
    {
        using var conexion = CrearConexion();
        conexion.Open();
        return conexion.Delete<T>(new { Id = id });
    }
}

public class EstudianteNotaRepositorio : RepositorioGenerico<EstudianteNota>
{
    // La clase base implementa las operaciones genéricas para EstudianteNota.
}
