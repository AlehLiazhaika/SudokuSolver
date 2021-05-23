using System;
using System.Threading.Tasks;
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
            SetupExceptionHandling();
            WriteStartupLogMessage();

            var kernel = new StandardKernel();
            kernel.Load(new Algorithms.Module(), new Core.Module());
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

        private void SetupExceptionHandling()
        {
            AppDomain.CurrentDomain.UnhandledException += (_, e) =>
                LogUnhandledException((Exception)e.ExceptionObject, nameof(AppDomain.CurrentDomain.UnhandledException));

            DispatcherUnhandledException += (_, e) =>
            {
                LogUnhandledException(e.Exception, nameof(Application.Current.DispatcherUnhandledException));
                e.Handled = true;
            };

            TaskScheduler.UnobservedTaskException += (_, e) =>
            {
                LogUnhandledException(e.Exception, nameof(TaskScheduler.UnobservedTaskException));
                e.SetObserved();
            };
        }

        private void LogUnhandledException(Exception exception, string source)
        {
            string message = $"Unhandled exception ({source})";
            try
            {
                System.Reflection.AssemblyName assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName();
                message = $"Unhandled exception in {assemblyName.Name} v{assemblyName.Version}";
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Exception while {nameof(LogUnhandledException)}");
            }
            finally
            {
                _logger.Error(exception, message);
                _logger.Error(exception.StackTrace);

                MessageBox.Show(
                    Desktop.Properties.Resources.CriticalErrorMessage,
                    Desktop.Properties.Resources.CriticalErrorMessageCaption,
                    MessageBoxButton.OK);

                Shutdown();
            }
        }

        private void WriteStartupLogMessage() => _logger.Info(Desktop.Properties.Resources.StartupMessage);

        private void WriteExitLogMessage() => _logger.Info(Desktop.Properties.Resources.ExitMessage);
    }
}