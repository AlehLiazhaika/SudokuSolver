using System;
using System.IO;
using System.Windows;
using Ninject;
using Serilog;

namespace SudokuSolver.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly string LogFilePath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "SudokuSolver",
                "Logs",
                $"{DateTime.Now:yyyy-MM-dd hh-mm-ss}_log.txt");

        private readonly ILogger _logger =
            new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(LogFilePath)
                .CreateLogger();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var kernel = new StandardKernel();
            kernel.Load("*.dll");
            kernel.Bind<ILogger>().ToConstant(_logger);
            kernel.Bind<SudokuSolverViewModel>().ToSelf();
            kernel.Bind<MainWindow>().ToSelf();

            Current.MainWindow = kernel.Get<MainWindow>();
            Current.MainWindow.Show();

            WriteStartupLogMessage();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            WriteExitLogMessage();
        }

        private void WriteStartupLogMessage() => _logger.Information("Sudoku Solver - Application starting up…");

        private void WriteExitLogMessage() => _logger.Information("Sudoku Solver - Application shutting down…");
    }
}