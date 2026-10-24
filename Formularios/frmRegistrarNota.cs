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
        ConstruirInterfaz();
        Load += FrmRegistrarNota_Load;
    }

    private void ConstruirInterfaz()
    {
        var encabezado = new Panel { Dock = DockStyle.Top, Height = 94, BackColor = azul };
        var titulo = new Label { Text = "REGISTRO DE NOTAS · POE", ForeColor = Color.White, Font = new Font("Segoe UI", 20, FontStyle.Bold), AutoSize = true, Location = new Point(28, 17) };
        var subtitulo = new Label { Text = "Administración de estudiantes y evaluación ponderada", ForeColor = Color.FromArgb(220, 232, 244), AutoSize = true, Location = new Point(31, 57) };
        encabezado.Controls.AddRange(new Control[] { titulo, subtitulo });
        Controls.Add(encabezado);

        var formulario = new Panel { Dock = DockStyle.Top, Height = 235, Padding = new Padding(24), BackColor = Color.White };
        var lblForm = new Label { Text = "Datos del estudiante", Font = new Font("Segoe UI", 13, FontStyle.Bold), ForeColor = azul, AutoSize = true, Location = new Point(24, 15) };
        formulario.Controls.Add(lblForm);

        txtNombre = Campo(formulario, "Nombre", "txtNombre", 25, 58, 230);
        txtApellido = Campo(formulario, "Apellido", "txtApellido", 285, 58, 230);
        txtCodigo = Campo(formulario, "Código", "txtCodigo", 545, 58, 190);
        txtLaboratorio = Campo(formulario, "Laboratorio (50%)", "txtLaboratorio", 25, 132, 150);
        txtParcial = Campo(formulario, "Parcial (40%)", "txtParcial", 205, 132, 150);
        txtAsistencia = Campo(formulario, "Asistencia (10%)", "txtAsistencia", 385, 132, 150);
        foreach (var tb in new[] { txtLaboratorio, txtParcial, txtAsistencia })
        {
            tb.KeyPress += Nota_KeyPress;
            tb.TextChanged += Nota_TextChanged;
        }

        btnAgregar = new Button { Name = "btnAgregar", Text = "Agregar estudiante", Location = new Point(565, 153), Size = new Size(180, 40), BackColor = azul, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold), Cursor = Cursors.Hand };
        btnAgregar.FlatAppearance.BorderSize = 0;
        btnAgregar.Click += BtnAgregar_Click;
        formulario.Controls.Add(btnAgregar);

        var btnLimpiar = new Button { Text = "Limpiar", Location = new Point(755, 153), Size = new Size(105, 40), BackColor = Color.FromArgb(230, 235, 241), ForeColor = Color.FromArgb(45, 55, 72), FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
        btnLimpiar.FlatAppearance.BorderSize = 0;
        btnLimpiar.Click += (_, _) => LimpiarControles();
        formulario.Controls.Add(btnLimpiar);
        var info = new Label { Text = "Notas permitidas: de 0.0 a 10.0. La nota final se calcula automáticamente.", AutoSize = true, ForeColor = Color.DimGray, Location = new Point(25, 202), Font = new Font("Segoe UI", 9) };
        formulario.Controls.Add(info);
        Controls.Add(formulario);

        var gridPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(18, 8, 18, 12), BackColor = fondo, Margin = new Padding(0) };
        gridPanel.Paint += (_, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, gridPanel.Width - 1, gridPanel.Height - 1);
            var shadowRect = Rectangle.Inflate(rect, 4, 4);

            using var sombra = new SolidBrush(Color.FromArgb(20, 0, 0, 0));
            using var fondoCard = new SolidBrush(Color.White);
            using var borde = new Pen(Color.FromArgb(214, 224, 235), 1.2f);

            e.Graphics.FillPath(sombra, CrearRutaRedondeada(shadowRect, 18));
            e.Graphics.FillPath(fondoCard, CrearRutaRedondeada(rect, 18));
            e.Graphics.DrawPath(borde, CrearRutaRedondeada(rect, 18));
        };
        gridPanel.Resize += (_, _) => AplicarEsquinasRedondeadas(gridPanel, 18);

        var gridTitulo = new Label { Text = "Estudiantes registrados", Font = new Font("Segoe UI", 13, FontStyle.Bold), ForeColor = azul, Dock = DockStyle.Top, Height = 36, Padding = new Padding(8, 0, 0, 0) };
        var gridBody = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(0), Margin = new Padding(0) };

        dgvRegistro = new DataGridView
        {
            Name = "dgvRegistro", Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false,
            AllowUserToDeleteRows = false, AutoGenerateColumns = true, MultiSelect = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect, RowHeadersVisible = false,
            BackgroundColor = Color.White, BorderStyle = BorderStyle.None, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            EnableHeadersVisualStyles = false, ColumnHeadersHeight = 42, RowTemplate = { Height = 34 },
            Margin = new Padding(0), CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
            GridColor = Color.FromArgb(220, 228, 236)
        };
        dgvRegistro.ColumnHeadersDefaultCellStyle.BackColor = azul;
        dgvRegistro.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        dgvRegistro.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        dgvRegistro.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dgvRegistro.DefaultCellStyle.SelectionBackColor = Color.FromArgb(214, 231, 248);
        dgvRegistro.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 45, 60);
        dgvRegistro.DefaultCellStyle.Font = new Font("Segoe UI", 9);
        dgvRegistro.DefaultCellStyle.Padding = new Padding(4, 2, 4, 2);
        dgvRegistro.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(247, 249, 252);
        dgvRegistro.CellDoubleClick += DgvRegistro_CellDoubleClick;

        gridBody.Controls.Add(dgvRegistro);
        gridPanel.Controls.Add(gridBody);
        gridPanel.Controls.Add(gridTitulo);
        Controls.Add(gridPanel);
        gridPanel.BringToFront();
        AplicarEsquinasRedondeadas(gridPanel, 18);

        lblEstado = new Label { Dock = DockStyle.Bottom, Height = 28, TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(24, 0, 0, 0), BackColor = Color.White, ForeColor = Color.DimGray, Text = "Doble clic en un registro para editarlo o eliminarlo." };
        Controls.Add(lblEstado);
        lblEstado.BringToFront();
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

    private void FrmRegistrarNota_Load(object? sender, EventArgs e)
    {
        LimpiarControles();
        CargarGrid();
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
            new[] { lab, parcial, asistencia }.Any(n => n < 0 || n > 10))
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

            if (_repositorio.ObtenerTodos().Cast<EstudianteNota>().Any(x => x.Codigo.Equals(txtCodigo.Text, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Ya existe un estudiante con ese código.", "Código duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
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
            dgvRegistro.DataSource = _repositorio.ObtenerTodos().Cast<EstudianteNota>()
                .OrderBy(x => x.Id).ToList();
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
}
