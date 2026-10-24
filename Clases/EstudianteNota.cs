namespace SistemaRegistroNotas.Clases;

/// <summary>
/// El nombre de esta clase coincide exactamente con el nombre de la tabla SQL: EstudianteNota.
/// </summary>
public class EstudianteNota
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;
    public decimal Laboratorio { get; set; }
    public decimal Parcial { get; set; }
    public decimal Asistencia { get; set; }
    public decimal NotaFinal { get; set; }
}
