using SistemaRegistroNotas.Clases;
using SistemaRegistroNotas.Repositorios;
using System.Drawing;

namespace SistemaRegistroNotas.Formularios;

public partial class frmRegistrarNotaCmd : Form
{
    private readonly EstudianteNotaRepositorio _repositorio = new();
    private readonly EstudianteNota _estudiante;
    private TextBox txtNombre = null!, txtApellido = null!, txtCodigo = null!;
    private TextBox txtLaboratorio = null!, txtParcial = null!, txtAsistencia = null!;
    private Label lblFinal = null!;

    public frmRegistrarNotaCmd(EstudianteNota estudiante)
    {
        _estudiante = estudiante;
        InitializeComponent();
        ConstruirInterfaz();
        CargarDatos();
    }

    private void ConstruirInterfaz()
    {
        var header = new Panel { Dock = DockStyle.Top, Height = 76, BackColor = Color.FromArgb(31, 78, 121) };
        header.Controls.Add(new Label { Text = "EDITAR ESTUDIANTE", ForeColor = Color.White, Font = new Font("Segoe UI", 16, FontStyle.Bold), AutoSize = true, Location = new Point(22, 22) });
        Controls.Add(header);
        int y = 95;
        txtNombre = AgregarCampo("Nombre", "txtNombre", ref y);
        txtApellido = AgregarCampo("Apellido", "txtApellido", ref y);
        txtCodigo = AgregarCampo("Código", "txtCodigo", ref y);
        txtLaboratorio = AgregarCampo("Laboratorio (50%)", "txtLaboratorio", ref y);
        txtParcial = AgregarCampo("Parcial (40%)", "txtParcial", ref y);
        txtAsistencia = AgregarCampo("Asistencia (10%)", "txtAsistencia", ref y);
        foreach (var tb in new[] { txtLaboratorio, txtParcial, txtAsistencia })
        {
            tb.KeyPress += Nota_KeyPress;
            tb.TextChanged += (_, _) => CalcularFinal();
        }
        lblFinal = new Label { Text = "Nota final: 0.00", Location = new Point(28, 420), AutoSize = true, ForeColor = Color.FromArgb(31, 78, 121), Font = new Font("Segoe UI", 11, FontStyle.Bold) };
        Controls.Add(lblFinal);
        var guardar = new Button { Text = "Guardar cambios", Location = new Point(28, 458), Size = new Size(180, 38), BackColor = Color.FromArgb(31, 78, 121), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        guardar.FlatAppearance.BorderSize = 0;
        guardar.Click += Guardar_Click;
        Controls.Add(guardar);
        var eliminar = new Button { Text = "Eliminar", Location = new Point(218, 458), Size = new Size(100, 38), BackColor = Color.FromArgb(180, 55, 55), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        eliminar.FlatAppearance.BorderSize = 0;
        eliminar.Click += Eliminar_Click;
        Controls.Add(eliminar);
        var cancelar = new Button { Text = "Cancelar", Location = new Point(328, 458), Size = new Size(145, 38), BackColor = Color.FromArgb(225, 230, 236), ForeColor = Color.FromArgb(40, 50, 60), FlatStyle = FlatStyle.Flat, DialogResult = DialogResult.Cancel };
        cancelar.FlatAppearance.BorderSize = 0;
        Controls.Add(cancelar);
        AcceptButton = guardar;
        CancelButton = cancelar;
    }

    private TextBox AgregarCampo(string etiqueta, string nombre, ref int y)
    {
        Controls.Add(new Label { Text = etiqueta, Location = new Point(28, y), AutoSize = true, ForeColor = Color.FromArgb(65, 75, 90) });
        var tb = new TextBox { Name = nombre, Location = new Point(205, y - 3), Width = 265, Height = 27, BorderStyle = BorderStyle.FixedSingle };
        Controls.Add(tb);
        y += 48;
        return tb;
    }

    private void CargarDatos()
    {
        txtNombre.Text = _estudiante.Nombre;
        txtApellido.Text = _estudiante.Apellido;
        txtCodigo.Text = _estudiante.Codigo;
        txtLaboratorio.Text = _estudiante.Laboratorio.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture);
        txtParcial.Text = _estudiante.Parcial.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture);
        txtAsistencia.Text = _estudiante.Asistencia.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture);
        CalcularFinal();
    }

    private bool LeerNotas(out decimal lab, out decimal parcial, out decimal asistencia)
    {
        lab = 0m;
        parcial = 0m;
        asistencia = 0m;

        if (!decimal.TryParse(txtLaboratorio.Text.Replace(',', '.'), System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out lab))
            return false;
        if (!decimal.TryParse(txtParcial.Text.Replace(',', '.'), System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out parcial))
            return false;
        if (!decimal.TryParse(txtAsistencia.Text.Replace(',', '.'), System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out asistencia))
            return false;

        return new[] { lab, parcial, asistencia }.All(n => n >= 0 && n <= 10);
    }

    private void CalcularFinal()
    {
        if (lblFinal == null) return;
        if (LeerNotas(out var lab, out var parcial, out var asistencia))
            lblFinal.Text = $"Nota final: {Math.Round(lab * .5m + parcial * .4m + asistencia * .1m, 2):0.00}";
        else lblFinal.Text = "Nota final: —";
    }

    private void Nota_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar)) return;
        var tb = (TextBox)sender!;
        if ((e.KeyChar == '.' || e.KeyChar == ',') && !tb.Text.Contains('.') && !tb.Text.Contains(',')) return;
        e.Handled = true;
    }

    private void Guardar_Click(object? sender, EventArgs e)
    {
        if (new[] { txtNombre.Text, txtApellido.Text, txtCodigo.Text, txtLaboratorio.Text, txtParcial.Text, txtAsistencia.Text }.Any(string.IsNullOrWhiteSpace))
        {
            MessageBox.Show("Todos los campos son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (!LeerNotas(out var lab, out var parcial, out var asistencia))
        {
            MessageBox.Show("Las notas deben ser números entre 0.0 y 10.0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            _estudiante.Nombre = txtNombre.Text.Trim().ToUpperInvariant();
            _estudiante.Apellido = txtApellido.Text.Trim().ToUpperInvariant();
            _estudiante.Codigo = txtCodigo.Text.Trim().ToUpperInvariant();
            _estudiante.Laboratorio = lab;
            _estudiante.Parcial = parcial;
            _estudiante.Asistencia = asistencia;
            _estudiante.NotaFinal = Math.Round(lab * .5m + parcial * .4m + asistencia * .1m, 2);
            _repositorio.Actualizar(_estudiante);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("No se pudieron guardar los cambios.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void Eliminar_Click(object? sender, EventArgs e)
    {
        if (MessageBox.Show($"¿Desea eliminar el registro de {_estudiante.Nombre} {_estudiante.Apellido}?", "Confirmar eliminación",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
        try
        {
            _repositorio.Eliminar(_estudiante.Id);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("No se pudo eliminar el registro.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
