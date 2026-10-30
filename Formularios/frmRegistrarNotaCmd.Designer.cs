namespace SistemaRegistroNotas.Formularios;

public partial class frmRegistrarNotaCmd
{
    private Panel header = null!;
    private Label lblHeader = null!;
    private Label lblNombre = null!;
    private Label lblApellido = null!;
    private Label lblCodigo = null!;
    private Label lblLaboratorio = null!;
    private Label lblParcial = null!;
    private Label lblAsistencia = null!;
    private Button btnGuardar = null!;
    private Button btnEliminar = null!;
    private Button btnCancelar = null!;

    private void InitializeComponent()
    {
        header = new Panel();
        lblHeader = new Label();
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
        lblFinal = new Label();
        btnGuardar = new Button();
        btnEliminar = new Button();
        btnCancelar = new Button();

        SuspendLayout();

        Name = "frmRegistrarNotaCmd";
        Text = "Editar Estudiante";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ClientSize = new Size(510, 520);
        BackColor = Color.FromArgb(245, 247, 250);
        Font = new Font("Segoe UI", 10F);

        header.Name = "header";
        header.Location = new Point(0, 0);
        header.Size = new Size(510, 76);
        header.BackColor = Color.FromArgb(31, 78, 121);

        lblHeader.Name = "lblHeader";
        lblHeader.Text = "EDITAR ESTUDIANTE";
        lblHeader.ForeColor = Color.White;
        lblHeader.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblHeader.AutoSize = true;
        lblHeader.Location = new Point(22, 22);
        header.Controls.Add(lblHeader);

        lblNombre.Name = "lblNombre";
        lblNombre.Text = "Nombre";
        lblNombre.Location = new Point(28, 95);
        lblNombre.AutoSize = true;
        lblNombre.ForeColor = Color.FromArgb(65, 75, 90);

        lblApellido.Name = "lblApellido";
        lblApellido.Text = "Apellido";
        lblApellido.Location = new Point(28, 143);
        lblApellido.AutoSize = true;
        lblApellido.ForeColor = Color.FromArgb(65, 75, 90);

        lblCodigo.Name = "lblCodigo";
        lblCodigo.Text = "Código";
        lblCodigo.Location = new Point(28, 191);
        lblCodigo.AutoSize = true;
        lblCodigo.ForeColor = Color.FromArgb(65, 75, 90);

        lblLaboratorio.Name = "lblLaboratorio";
        lblLaboratorio.Text = "Laboratorio (50%)";
        lblLaboratorio.Location = new Point(28, 239);
        lblLaboratorio.AutoSize = true;
        lblLaboratorio.ForeColor = Color.FromArgb(65, 75, 90);

        lblParcial.Name = "lblParcial";
        lblParcial.Text = "Parcial (40%)";
        lblParcial.Location = new Point(28, 287);
        lblParcial.AutoSize = true;
        lblParcial.ForeColor = Color.FromArgb(65, 75, 90);

        lblAsistencia.Name = "lblAsistencia";
        lblAsistencia.Text = "Asistencia (10%)";
        lblAsistencia.Location = new Point(28, 335);
        lblAsistencia.AutoSize = true;
        lblAsistencia.ForeColor = Color.FromArgb(65, 75, 90);

        txtNombre.Name = "txtNombre";
        txtNombre.Location = new Point(205, 92);
        txtNombre.Size = new Size(265, 27);
        txtNombre.BorderStyle = BorderStyle.FixedSingle;

        txtApellido.Name = "txtApellido";
        txtApellido.Location = new Point(205, 140);
        txtApellido.Size = new Size(265, 27);
        txtApellido.BorderStyle = BorderStyle.FixedSingle;

        txtCodigo.Name = "txtCodigo";
        txtCodigo.Location = new Point(205, 188);
        txtCodigo.Size = new Size(265, 27);
        txtCodigo.BorderStyle = BorderStyle.FixedSingle;

        txtLaboratorio.Name = "txtLaboratorio";
        txtLaboratorio.Location = new Point(205, 236);
        txtLaboratorio.Size = new Size(265, 27);
        txtLaboratorio.BorderStyle = BorderStyle.FixedSingle;

        txtParcial.Name = "txtParcial";
        txtParcial.Location = new Point(205, 284);
        txtParcial.Size = new Size(265, 27);
        txtParcial.BorderStyle = BorderStyle.FixedSingle;

        txtAsistencia.Name = "txtAsistencia";
        txtAsistencia.Location = new Point(205, 332);
        txtAsistencia.Size = new Size(265, 27);
        txtAsistencia.BorderStyle = BorderStyle.FixedSingle;

        lblFinal.Name = "lblFinal";
        lblFinal.Text = "Nota final: 0.00";
        lblFinal.Location = new Point(28, 420);
        lblFinal.AutoSize = true;
        lblFinal.ForeColor = Color.FromArgb(31, 78, 121);
        lblFinal.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

        btnGuardar.Name = "btnGuardar";
        btnGuardar.Text = "Guardar cambios";
        btnGuardar.Location = new Point(28, 458);
        btnGuardar.Size = new Size(180, 38);
        btnGuardar.BackColor = Color.FromArgb(31, 78, 121);
        btnGuardar.ForeColor = Color.White;
        btnGuardar.FlatStyle = FlatStyle.Flat;
        btnGuardar.FlatAppearance.BorderSize = 0;

        btnEliminar.Name = "btnEliminar";
        btnEliminar.Text = "Eliminar";
        btnEliminar.Location = new Point(218, 458);
        btnEliminar.Size = new Size(100, 38);
        btnEliminar.BackColor = Color.FromArgb(180, 55, 55);
        btnEliminar.ForeColor = Color.White;
        btnEliminar.FlatStyle = FlatStyle.Flat;
        btnEliminar.FlatAppearance.BorderSize = 0;

        btnCancelar.Name = "btnCancelar";
        btnCancelar.Text = "Cancelar";
        btnCancelar.Location = new Point(328, 458);
        btnCancelar.Size = new Size(145, 38);
        btnCancelar.BackColor = Color.FromArgb(225, 230, 236);
        btnCancelar.ForeColor = Color.FromArgb(40, 50, 60);
        btnCancelar.FlatStyle = FlatStyle.Flat;
        btnCancelar.FlatAppearance.BorderSize = 0;
        btnCancelar.DialogResult = DialogResult.Cancel;

        Controls.Add(header);
        Controls.Add(lblNombre);
        Controls.Add(lblApellido);
        Controls.Add(lblCodigo);
        Controls.Add(lblLaboratorio);
        Controls.Add(lblParcial);
        Controls.Add(lblAsistencia);
        Controls.Add(txtNombre);
        Controls.Add(txtApellido);
        Controls.Add(txtCodigo);
        Controls.Add(txtLaboratorio);
        Controls.Add(txtParcial);
        Controls.Add(txtAsistencia);
        Controls.Add(lblFinal);
        Controls.Add(btnGuardar);
        Controls.Add(btnEliminar);
        Controls.Add(btnCancelar);

        btnGuardar.Click += Guardar_Click;
        btnEliminar.Click += Eliminar_Click;
        txtLaboratorio.KeyPress += Nota_KeyPress;
        txtLaboratorio.TextChanged += CalcularFinalDesdeTexto;
        txtParcial.KeyPress += Nota_KeyPress;
        txtParcial.TextChanged += CalcularFinalDesdeTexto;
        txtAsistencia.KeyPress += Nota_KeyPress;
        txtAsistencia.TextChanged += CalcularFinalDesdeTexto;

        AcceptButton = btnGuardar;
        CancelButton = btnCancelar;
        ResumeLayout(false);
        PerformLayout();
    }

}
