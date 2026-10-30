using SistemaRegistroNotas.Clases;
using SistemaRegistroNotas.Repositorios;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SistemaRegistroNotas.Formularios;

public partial class frmRegistrarNota : Form
{
    private readonly EstudianteNotaRepositorio _repositorio = new();
    private readonly Color azul = Color.FromArgb(31, 78, 121);
    private readonly Color fondo = Color.FromArgb(245, 247, 250);
    private TextBox txtApellido = null!;
    public TextBox txtNombre = null!;
    public TextBox txtCodigo = null!;
    public TextBox txtLaboratorio = null!;
    public TextBox txtParcial = null!;
    public TextBox txtAsistencia = null!;
    public Button btnAgregar = null!;
    public DataGridView dgvRegistro = null!;
    private Label lblEstado = null!;

    public frmRegistrarNota()
    {
        InitializeComponent();
        Load += FrmRegistrarNota_Load;
    }

    private void FrmRegistrarNota_Load(object? sender, EventArgs e)
    {
        LimpiarControles();
        CargarGrid();
    }

    private void BtnLimpiar_Click(object? sender, EventArgs e)
    {
        LimpiarControles();
    }

    private void GridPanel_Paint(object? sender, PaintEventArgs e)
    {
        if (sender is not Panel gridPanel) return;

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        var rect = new Rectangle(0, 0, gridPanel.Width - 1, gridPanel.Height - 1);
        var shadowRect = Rectangle.Inflate(rect, 4, 4);
        using var sombra = new SolidBrush(Color.FromArgb(20, 0, 0, 0));
        using var fondoCard = new SolidBrush(Color.White);
        using var borde = new Pen(Color.FromArgb(214, 224, 235), 1.2f);
        e.Graphics.FillPath(sombra, CrearRutaRedondeada(shadowRect, 18));
        e.Graphics.FillPath(fondoCard, CrearRutaRedondeada(rect, 18));
        e.Graphics.DrawPath(borde, CrearRutaRedondeada(rect, 18));
    }

    private void GridPanel_Resize(object? sender, EventArgs e)
    {
        if (sender is Control gridPanel)
            AplicarEsquinasRedondeadas(gridPanel, 18);
    }

    private void BtnAgregar_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text) ||
            string.IsNullOrWhiteSpace(txtCodigo.Text) || string.IsNullOrWhiteSpace(txtLaboratorio.Text) ||
            string.IsNullOrWhiteSpace(txtParcial.Text) || string.IsNullOrWhiteSpace(txtAsistencia.Text))
        {
            MessageBox.Show("Complete todos los campos antes de guardar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!decimal.TryParse(txtLaboratorio.Text, out var lab) || !decimal.TryParse(txtParcial.Text, out var parcial) ||
            !decimal.TryParse(txtAsistencia.Text, out var asistencia) ||
            lab < 0 || lab > 10 || parcial < 0 || parcial > 10 || asistencia < 0 || asistencia > 10)
        {
            MessageBox.Show("Las notas deben ser números entre 0.0 y 10.0.", "Nota inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            // Bonus: convertir los campos de texto a mayúsculas antes de insertar.
            txtNombre.Text = txtNombre.Text.Trim().ToUpperInvariant();
            txtApellido.Text = txtApellido.Text.Trim().ToUpperInvariant();
            txtCodigo.Text = txtCodigo.Text.Trim().ToUpperInvariant();

            foreach (var registro in _repositorio.ObtenerTodos().Cast<EstudianteNota>())
            {
                if (registro.Codigo.Equals(txtCodigo.Text, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Ya existe un estudiante con ese código.", "Código duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            var estudiante = new EstudianteNota
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Codigo = txtCodigo.Text,
                Laboratorio = lab,
                Parcial = parcial,
                Asistencia = asistencia,
                NotaFinal = Math.Round(lab * 0.5m + parcial * 0.4m + asistencia * 0.1m, 2)
            };
            _repositorio.Insertar(estudiante);
            MessageBox.Show("Estudiante registrado correctamente.", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarGrid();
            LimpiarControles();
            lblEstado.Text = "Estudiante guardado correctamente.";
        }
        catch (Exception ex)
        {
            MessageBox.Show("No fue posible guardar el registro.\n\n" + ex.Message, "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void DgvRegistro_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || dgvRegistro.Rows[e.RowIndex].DataBoundItem is not EstudianteNota seleccionado) return;
        using var dialogo = new frmRegistrarNotaCmd(seleccionado);
        if (dialogo.ShowDialog(this) == DialogResult.OK) CargarGrid();
    }

    private void CargarGrid()
    {
        try
        {
            var registros = _repositorio.ObtenerTodos().Cast<EstudianteNota>().ToList();
            registros.Sort(CompararPorId);
            dgvRegistro.DataSource = registros;
            if (dgvRegistro.Columns["Id"] != null) dgvRegistro.Columns["Id"].HeaderText = "ID";
            if (dgvRegistro.Columns["Nombre"] != null) dgvRegistro.Columns["Nombre"].HeaderText = "NOMBRE";
            if (dgvRegistro.Columns["Apellido"] != null) dgvRegistro.Columns["Apellido"].HeaderText = "APELLIDO";
            if (dgvRegistro.Columns["Codigo"] != null) dgvRegistro.Columns["Codigo"].HeaderText = "CÓDIGO";
            if (dgvRegistro.Columns["Laboratorio"] != null) dgvRegistro.Columns["Laboratorio"].HeaderText = "LABORATORIO";
            if (dgvRegistro.Columns["Parcial"] != null) dgvRegistro.Columns["Parcial"].HeaderText = "PARCIAL";
            if (dgvRegistro.Columns["Asistencia"] != null) dgvRegistro.Columns["Asistencia"].HeaderText = "ASISTENCIA";
            if (dgvRegistro.Columns["NotaFinal"] != null) dgvRegistro.Columns["NotaFinal"].HeaderText = "NOTA FINAL";
            foreach (var columna in new[] { "Laboratorio", "Parcial", "Asistencia", "NotaFinal" })
                if (dgvRegistro.Columns[columna] != null) dgvRegistro.Columns[columna]!.DefaultCellStyle.Format = "0.00";
            lblEstado.Text = $"Registros: {dgvRegistro.Rows.Count} · Doble clic en un estudiante para editar.";
        }
        catch (Exception ex)
        {
            MessageBox.Show("No se pudo cargar la información. Verifique SQL Server y App.config.\n\n" + ex.Message, "Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static int CompararPorId(EstudianteNota primero, EstudianteNota segundo)
    {
        return primero.Id.CompareTo(segundo.Id);
    }

    private void LimpiarControles()
    {
        foreach (var tb in new[] { txtNombre, txtApellido, txtCodigo, txtLaboratorio, txtParcial, txtAsistencia })
            if (tb != null) tb.Clear();
        txtNombre?.Focus();
    }

    private void Nota_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (char.IsControl(e.KeyChar)) return;
        var tb = (TextBox)sender!;
        if (char.IsDigit(e.KeyChar)) return;
        if ((e.KeyChar == '.' || e.KeyChar == ',') && !tb.Text.Contains('.') && !tb.Text.Contains(',')) return;
        e.Handled = true;
    }

    private void Nota_TextChanged(object? sender, EventArgs e)
    {
        var tb = (TextBox)sender!;
        if (decimal.TryParse(tb.Text.Replace(',', '.'), System.Globalization.NumberStyles.Number,
            System.Globalization.CultureInfo.InvariantCulture, out var valor) && valor > 10)
        {
            tb.BackColor = Color.MistyRose;
        }
        else tb.BackColor = Color.White;
    }

    private TextBox Campo(Control parent, string etiqueta, string nombre, int x, int y, int ancho)
    {
        var lbl = new Label { Text = etiqueta, Location = new Point(x, y), AutoSize = true, ForeColor = Color.FromArgb(65, 75, 90), Font = new Font("Segoe UI", 9, FontStyle.Bold) };
        var tb = new TextBox { Name = nombre, Location = new Point(x, y + 23), Width = ancho, Height = 30, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 10) };
        parent.Controls.Add(lbl);
        parent.Controls.Add(tb);
        return tb;
    }

    private static GraphicsPath CrearRutaRedondeada(Rectangle rect, int radius)
    {
        var path = new GraphicsPath();
        var r = Math.Max(radius, 0);

        path.AddArc(rect.X, rect.Y, r * 2, r * 2, 180, 90);
        path.AddArc(rect.Right - (r * 2), rect.Y, r * 2, r * 2, 270, 90);
        path.AddArc(rect.Right - (r * 2), rect.Bottom - (r * 2), r * 2, r * 2, 0, 90);
        path.AddArc(rect.X, rect.Bottom - (r * 2), r * 2, r * 2, 90, 90);
        path.CloseFigure();
        return path;
    }

    private static void AplicarEsquinasRedondeadas(Control control, int radio)
    {
        if (control.Width <= 0 || control.Height <= 0) return;
        var rect = new Rectangle(0, 0, control.Width, control.Height);
        control.Region = new Region(CrearRutaRedondeada(rect, radio));
    }
}
