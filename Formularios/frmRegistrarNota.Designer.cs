using System.Drawing.Drawing2D;

namespace SistemaRegistroNotas.Formularios;

public partial class frmRegistrarNota
{
    private Panel encabezado = null!;
    private Panel formulario = null!;
    private Panel gridPanel = null!;
    private Panel gridBody = null!;
    private Label titulo = null!;
    private Label subtitulo = null!;
    private Label lblForm = null!;
    private Label lblNombre = null!;
    private Label lblApellido = null!;
    private Label lblCodigo = null!;
    private Label lblLaboratorio = null!;
    private Label lblParcial = null!;
    private Label lblAsistencia = null!;
    private Label info = null!;
    private Label gridTitulo = null!;
    private Button btnLimpiar = null!;

    private void InitializeComponent()
    {
        encabezado = new Panel();
        titulo = new Label();
        subtitulo = new Label();
        formulario = new Panel();
        lblForm = new Label();
        lblNombre = new Label();
        lblApellido = new Label();
        lblCodigo = new Label();
        lblLaboratorio = new Label();
        lblParcial = new Label();
        lblAsistencia = new Label();
        txtNombre = new TextBox();
        txtApellido = new TextBox();
        txtCodigo = new TextBox();
        txtLaboratorio = new TextBox();
        txtParcial = new TextBox();
        txtAsistencia = new TextBox();
        btnAgregar = new Button();
        btnLimpiar = new Button();
        info = new Label();
        gridPanel = new Panel();
        gridTitulo = new Label();
        gridBody = new Panel();
        dgvRegistro = new DataGridView();
        lblEstado = new Label();

        SuspendLayout();

        Name = "frmRegistrarNota";
        Text = "Crear Estudiante";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(1050, 720);
        ClientSize = new Size(1180, 780);
        BackColor = Color.FromArgb(245, 247, 250);
        Font = new Font("Segoe UI", 10F);

        encabezado.Name = "encabezado";
        encabezado.Location = new Point(0, 235);
        encabezado.Size = new Size(1180, 94);
        encabezado.BackColor = Color.FromArgb(31, 78, 121);

        titulo.Name = "titulo";
        titulo.Text = "REGISTRO DE NOTAS · POE";
        titulo.ForeColor = Color.White;
        titulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        titulo.AutoSize = true;
        titulo.Location = new Point(28, 17);

        subtitulo.Name = "subtitulo";
        subtitulo.Text = "Administración de estudiantes y evaluación ponderada";
        subtitulo.ForeColor = Color.FromArgb(220, 232, 244);
        subtitulo.AutoSize = true;
        subtitulo.Location = new Point(31, 57);

        encabezado.Controls.Add(titulo);
        encabezado.Controls.Add(subtitulo);

        formulario.Name = "formulario";
        formulario.Location = new Point(0, 0);
        formulario.Size = new Size(1180, 235);
        formulario.Padding = new Padding(24);
        formulario.BackColor = Color.White;

        lblForm.Name = "lblForm";
        lblForm.Text = "Datos del estudiante";
        lblForm.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblForm.ForeColor = Color.FromArgb(31, 78, 121);
        lblForm.AutoSize = true;
        lblForm.Location = new Point(24, 15);

        lblNombre.Name = "lblNombre";
        lblNombre.Text = "Nombre";
        lblNombre.AutoSize = true;
        lblNombre.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblNombre.ForeColor = Color.FromArgb(65, 75, 90);
        lblNombre.Location = new Point(25, 58);

        lblApellido.Name = "lblApellido";
        lblApellido.Text = "Apellido";
        lblApellido.AutoSize = true;
        lblApellido.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblApellido.ForeColor = Color.FromArgb(65, 75, 90);
        lblApellido.Location = new Point(285, 58);

        lblCodigo.Name = "lblCodigo";
        lblCodigo.Text = "Código";
        lblCodigo.AutoSize = true;
        lblCodigo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblCodigo.ForeColor = Color.FromArgb(65, 75, 90);
        lblCodigo.Location = new Point(545, 58);

        lblLaboratorio.Name = "lblLaboratorio";
        lblLaboratorio.Text = "Laboratorio (50%)";
        lblLaboratorio.AutoSize = true;
        lblLaboratorio.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblLaboratorio.ForeColor = Color.FromArgb(65, 75, 90);
        lblLaboratorio.Location = new Point(25, 132);

        lblParcial.Name = "lblParcial";
        lblParcial.Text = "Parcial (40%)";
        lblParcial.AutoSize = true;
        lblParcial.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblParcial.ForeColor = Color.FromArgb(65, 75, 90);
        lblParcial.Location = new Point(205, 132);

        lblAsistencia.Name = "lblAsistencia";
        lblAsistencia.Text = "Asistencia (10%)";
        lblAsistencia.AutoSize = true;
        lblAsistencia.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblAsistencia.ForeColor = Color.FromArgb(65, 75, 90);
        lblAsistencia.Location = new Point(385, 132);

        txtNombre.Name = "txtNombre";
        txtNombre.Location = new Point(25, 81);
        txtNombre.Size = new Size(230, 30);
        txtNombre.BorderStyle = BorderStyle.FixedSingle;
        txtNombre.Font = new Font("Segoe UI", 10F);

        txtApellido.Name = "txtApellido";
        txtApellido.Location = new Point(285, 81);
        txtApellido.Size = new Size(230, 30);
        txtApellido.BorderStyle = BorderStyle.FixedSingle;
        txtApellido.Font = new Font("Segoe UI", 10F);

        txtCodigo.Name = "txtCodigo";
        txtCodigo.Location = new Point(545, 81);
        txtCodigo.Size = new Size(190, 30);
        txtCodigo.BorderStyle = BorderStyle.FixedSingle;
        txtCodigo.Font = new Font("Segoe UI", 10F);

        txtLaboratorio.Name = "txtLaboratorio";
        txtLaboratorio.Location = new Point(25, 155);
        txtLaboratorio.Size = new Size(150, 30);
        txtLaboratorio.BorderStyle = BorderStyle.FixedSingle;
        txtLaboratorio.Font = new Font("Segoe UI", 10F);

        txtParcial.Name = "txtParcial";
        txtParcial.Location = new Point(205, 155);
        txtParcial.Size = new Size(150, 30);
        txtParcial.BorderStyle = BorderStyle.FixedSingle;
        txtParcial.Font = new Font("Segoe UI", 10F);

        txtAsistencia.Name = "txtAsistencia";
        txtAsistencia.Location = new Point(385, 155);
        txtAsistencia.Size = new Size(150, 30);
        txtAsistencia.BorderStyle = BorderStyle.FixedSingle;
        txtAsistencia.Font = new Font("Segoe UI", 10F);

        btnAgregar.Name = "btnAgregar";
        btnAgregar.Text = "Agregar estudiante";
        btnAgregar.Location = new Point(565, 153);
        btnAgregar.Size = new Size(180, 40);
        btnAgregar.BackColor = Color.FromArgb(31, 78, 121);
        btnAgregar.ForeColor = Color.White;
        btnAgregar.FlatStyle = FlatStyle.Flat;
        btnAgregar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnAgregar.Cursor = Cursors.Hand;
        btnAgregar.FlatAppearance.BorderSize = 0;

        btnLimpiar.Name = "btnLimpiar";
        btnLimpiar.Text = "Limpiar";
        btnLimpiar.Location = new Point(755, 153);
        btnLimpiar.Size = new Size(105, 40);
        btnLimpiar.BackColor = Color.FromArgb(230, 235, 241);
        btnLimpiar.ForeColor = Color.FromArgb(45, 55, 72);
        btnLimpiar.FlatStyle = FlatStyle.Flat;
        btnLimpiar.Cursor = Cursors.Hand;
        btnLimpiar.FlatAppearance.BorderSize = 0;

        info.Name = "info";
        info.Text = "Notas permitidas: de 0.0 a 10.0. La nota final se calcula automáticamente.";
        info.AutoSize = true;
        info.ForeColor = Color.DimGray;
        info.Location = new Point(25, 202);
        info.Font = new Font("Segoe UI", 9F);

        formulario.Controls.Add(lblForm);
        formulario.Controls.Add(lblNombre);
        formulario.Controls.Add(lblApellido);
        formulario.Controls.Add(lblCodigo);
        formulario.Controls.Add(lblLaboratorio);
        formulario.Controls.Add(lblParcial);
        formulario.Controls.Add(lblAsistencia);
        formulario.Controls.Add(txtNombre);
        formulario.Controls.Add(txtApellido);
        formulario.Controls.Add(txtCodigo);
        formulario.Controls.Add(txtLaboratorio);
        formulario.Controls.Add(txtParcial);
        formulario.Controls.Add(txtAsistencia);
        formulario.Controls.Add(btnAgregar);
        formulario.Controls.Add(btnLimpiar);
        formulario.Controls.Add(info);

        gridPanel.Name = "gridPanel";
        gridPanel.Location = new Point(0, 329);
        gridPanel.Size = new Size(1180, 423);
        gridPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
        gridPanel.Padding = new Padding(18, 8, 18, 12);
        gridPanel.BackColor = Color.FromArgb(245, 247, 250);
        gridPanel.Margin = new Padding(0);

        gridTitulo.Name = "gridTitulo";
        gridTitulo.Text = "Estudiantes registrados";
        gridTitulo.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        gridTitulo.ForeColor = Color.FromArgb(31, 78, 121);
        gridTitulo.Dock = DockStyle.Top;
        gridTitulo.Height = 36;
        gridTitulo.Padding = new Padding(8, 0, 0, 0);

        gridBody.Name = "gridBody";
        gridBody.Dock = DockStyle.Fill;
        gridBody.BackColor = Color.White;
        gridBody.Padding = new Padding(0);
        gridBody.Margin = new Padding(0);

        dgvRegistro.Name = "dgvRegistro";
        dgvRegistro.Dock = DockStyle.Fill;
        dgvRegistro.ReadOnly = true;
        dgvRegistro.AllowUserToAddRows = false;
        dgvRegistro.AllowUserToDeleteRows = false;
        dgvRegistro.AutoGenerateColumns = true;
        dgvRegistro.MultiSelect = false;
        dgvRegistro.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvRegistro.RowHeadersVisible = false;
        dgvRegistro.BackgroundColor = Color.White;
        dgvRegistro.BorderStyle = BorderStyle.None;
        dgvRegistro.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvRegistro.EnableHeadersVisualStyles = false;
        dgvRegistro.ColumnHeadersHeight = 42;
        dgvRegistro.RowTemplate.Height = 34;
        dgvRegistro.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dgvRegistro.GridColor = Color.FromArgb(220, 228, 236);
        dgvRegistro.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 78, 121);
        dgvRegistro.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        dgvRegistro.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dgvRegistro.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dgvRegistro.DefaultCellStyle.SelectionBackColor = Color.FromArgb(214, 231, 248);
        dgvRegistro.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 45, 60);
        dgvRegistro.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
        dgvRegistro.DefaultCellStyle.Padding = new Padding(4, 2, 4, 2);
        dgvRegistro.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(247, 249, 252);

        gridBody.Controls.Add(dgvRegistro);
        gridPanel.Controls.Add(gridBody);
        gridPanel.Controls.Add(gridTitulo);

        lblEstado.Name = "lblEstado";
        lblEstado.Dock = DockStyle.Bottom;
        lblEstado.Height = 28;
        lblEstado.TextAlign = ContentAlignment.MiddleLeft;
        lblEstado.Padding = new Padding(24, 0, 0, 0);
        lblEstado.BackColor = Color.White;
        lblEstado.ForeColor = Color.DimGray;
        lblEstado.Text = "Doble clic en un registro para editarlo o eliminarlo.";

        Controls.Add(encabezado);
        Controls.Add(formulario);
        Controls.Add(gridPanel);
        Controls.Add(lblEstado);

        btnAgregar.Click += BtnAgregar_Click;
        btnLimpiar.Click += BtnLimpiar_Click;
        txtLaboratorio.KeyPress += Nota_KeyPress;
        txtLaboratorio.TextChanged += Nota_TextChanged;
        txtParcial.KeyPress += Nota_KeyPress;
        txtParcial.TextChanged += Nota_TextChanged;
        txtAsistencia.KeyPress += Nota_KeyPress;
        txtAsistencia.TextChanged += Nota_TextChanged;
        dgvRegistro.CellDoubleClick += DgvRegistro_CellDoubleClick;
        gridPanel.Paint += GridPanel_Paint;
        gridPanel.Resize += GridPanel_Resize;

        ResumeLayout(false);
        PerformLayout();
    }
}
