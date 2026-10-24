using RepoDb;
using SistemaRegistroNotas.Formularios;

namespace SistemaRegistroNotas;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        RepoDb.SqlServerGlobalConfiguration.UseSqlServer(GlobalConfiguration.Setup());
        Application.Run(new frmRegistrarNota());
    }
}
