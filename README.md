# Sistema de Registro de Notas POE

Proyecto Windows Forms en C# (.NET 8) con SQL Server Management Studio (SSMS), RepoDB y Microsoft.Data.SqlClient. Interfaz de escritorio con estilo profesional.

## Estructura solicitada

- `Clases/EstudianteNota.cs`: entidad. Su nombre coincide exactamente con la tabla `dbo.EstudianteNota`.
- `Formularios/frmRegistrarNota.cs`: formulario principal `frmRegistrarNota` (Text: Crear Estudiante).
- `Formularios/frmRegistrarNotaCmd.cs`: formulario modal de edición (Text: Editar Estudiante), con edición y eliminación.
- `Repositorios/RepositorioGenerico.cs`: operaciones CRUD genéricas con RepoDB.
- `SQL/CrearBaseDatos.sql`: script de creación de base de datos y tabla.
- `App.config`: cadena de conexión `conexionBD`.

## Requisitos

- Windows 10/11
- Visual Studio 2022 con la carga de trabajo **Desarrollo de escritorio de .NET**
- SQL Server Express/Developer y SQL Server Management Studio

## Instalación paso a paso

1. Abra SQL Server Management Studio y conéctese a su instancia local.
2. Abra y ejecute `SQL/CrearBaseDatos.sql` (puede abrirlo desde SSMS con File > Open > File).
3. Abra `App.config` y ajuste `Server=.\SQLEXPRESS` a su instancia. Ejemplos:
   - `.\SQLEXPRESS` para SQL Server Express con instancia SQLEXPRESS.
   - `localhost` para una instancia predeterminada local.
   - `NOMBREPC\SQLEXPRESS` si la instancia tiene otro nombre.
4. Abra `SistemaRegistroNotas.csproj` en Visual Studio 2022. Acepte la restauración de paquetes NuGet.
5. Compile y ejecute con F5.

## Funciones

- Agregar estudiante: nombre, apellido, código y las tres notas.
- Cálculo ponderado automático: `Laboratorio * 0.5 + Parcial * 0.4 + Asistencia * 0.1`.
- Validación de campos obligatorios y notas entre 0.0 y 10.0.
- La base de datos también restringe las notas mediante `CHECK`.
- Grid con registros guardados en SQL Server.
- Doble clic sobre una fila abre un formulario hijo modal para editar o eliminar.
- Conversión de nombre, apellido y código a mayúsculas antes de guardar.
- El código del estudiante no se puede repetir.

## Fórmula

`NotaFinal = (Laboratorio * 0.5) + (Parcial * 0.4) + (Asistencia * 0.1)`

## Notas técnicas

- Los controles de notas aceptan dígitos y un separador decimal (`.` o `,`). La validación definitiva se ejecuta antes de guardar.
- El formulario principal tiene los controles `txtNombre`, `txtCodigo`, `txtLaboratorio`, `txtParcial`, `txtAsistencia`, `btnAgregar` y `dgvRegistro`. Incluye además `txtApellido`, requerido por el enunciado.
- El repositorio genérico utiliza `QueryAll<T>`, `Query<T>`, `Insert<int,T>`, `Update<T>` y `Delete<T>` de RepoDB.
- Si NuGet indica que no encuentra una versión de RepoDB, actualice la versión de `RepoDb.SqlServer.Microsoft.Data.SqlClient` desde el Administrador de paquetes NuGet y restaure de nuevo.
