using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EnsureThat;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SudokuSolver.Core.Contracts;
using SudokuSolver.Core.Contracts.Models;
using SudokuSolver.Desktop.Extensions;
using SudokuSolver.Desktop.ViewModels;

namespace SudokuSolver.Desktop
{
    public class SudokuSolverViewModel : ReactiveObject
    {
        private readonly ISudokuSolverService _sudokuSolverService;

        [Reactive] public bool IsBusy { get; set; }
        public List<List<CellViewModel>> Field { get; set; }

        public ICommand SolveCommand { get; }
        public ICommand SetValuesCommand { get; }
        public ICommand ClearCellsCommand { get; }
        public ICommand ClearFieldCommand { get; }

        public SudokuSolverViewModel(ISudokuSolverService sudokuSolverService)
        {
            Ensure.That(sudokuSolverService, nameof(sudokuSolverService)).IsNotNull();

            _sudokuSolverService = sudokuSolverService;

            SolveCommand = ReactiveCommand.Create(Solve);
            SetValuesCommand = ReactiveCommand.Create<Key>(key => SetValues(key.ToInt()));
            ClearCellsCommand = ReactiveCommand.Create(ClearCells);
            ClearFieldCommand = ReactiveCommand.Create(ClearField);

            InitializeField();
        }

        private void InitializeField()
        {
            Field = new List<List<CellViewModel>>();

            for (int i = 0; i < 9; ++i)
            {
                Field.Add(new List<CellViewModel>());

                for (int j = 0; j < 9; ++j)
                {
                    Field[i].Add(new CellViewModel(boxDimensionality: 3, row: i, column: j, value: 0));
                }
            }
        }

        private async Task Solve() // TODO Refactor
        {
            IsBusy = true; // TODO Remake

            var filledCells = 
                Field
                    .SelectMany(x => x)
                    .Where(x => x.Value > 0)
                    .Select(x => new Cell(3, x.Row, x.Column, x.Value))
                    .ToList();

            var result =
                await Task.Run(() => _sudokuSolverService.Solve(dimensionality: 3, new HashSet<Cell>(filledCells)));

            foreach (var cell in result)
            {
                Field[cell.Row][cell.Column].Value = cell.Value;
            }

            IsBusy = false;
        }

        private void SetValues(int value)
        {
            foreach (var cell in Field.SelectMany(x => x).Where(x => x.IsSelected))
            {
                cell.Value = value;
                cell.Deselect();
            }
        }

        private void ClearCells()
        {
            foreach (var cell in Field.SelectMany(x => x).Where(x => x.IsSelected))
            {
                cell.Clear();
                cell.Deselect();
            }
        }

        private void ClearField()
        {
            foreach (var cell in Field.SelectMany(x => x))
            {
                cell.Clear();
                cell.Deselect();
            }
        }
    }
}