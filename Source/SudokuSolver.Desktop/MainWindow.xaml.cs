using System.Windows;
using Ninject;

namespace SudokuSolver.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var kernel = new StandardKernel();
            kernel.Load("*.dll");
            DataContext = kernel.Get<SudokuSolverViewModel>();
        }
    }
}