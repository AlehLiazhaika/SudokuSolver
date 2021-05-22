using System.Windows;
using Ninject;
using NLog;

namespace SudokuSolver.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger(); 

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            WriteStartupLogMessage();

            var kernel = new StandardKernel();
            kernel.Load("*.dll");
            kernel.Bind<ILogger>().ToConstant(_logger);
            kernel.Bind<SudokuSolverViewModel>().ToSelf();
            kernel.Bind<MainWindow>().ToSelf();

            Current.MainWindow = kernel.Get<MainWindow>();
            Current.MainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            WriteExitLogMessage();
        }

        private void WriteStartupLogMessage() => _logger.Info("Sudoku Solver - Application starting up…");

        private void WriteExitLogMessage() => _logger.Info("Sudoku Solver - Application shutting down…");
    }
}