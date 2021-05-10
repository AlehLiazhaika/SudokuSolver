namespace SudokuSolver.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow(SudokuSolverViewModel sudokuSolverViewModel)
        {
            InitializeComponent();
            DataContext = sudokuSolverViewModel;
        }
    }
}