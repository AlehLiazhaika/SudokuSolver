using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using EnsureThat;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SudokuSolver.Core;
using SudokuSolver.Core.Models;
using SudokuSolver.Desktop.Extensions;
using SudokuSolver.Desktop.ViewModels;

namespace SudokuSolver.Desktop
{
    public class SudokuSolverViewModel : ReactiveObject
    {
        private readonly ISudokuSolverService _sudokuSolverService;

        [Reactive] public bool IsBusy { get; set; }
        public List<List<CellViewModel>> Field { get; set; }

        public ReactiveCommand<Unit, Unit> SolveCommand { get; }
        public ReactiveCommand<Key, Unit> SetValuesCommand { get; }
        public ReactiveCommand<Unit, Unit> ClearCellsCommand { get; }
        public ReactiveCommand<Unit, Unit> ClearFieldCommand { get; }

        public SudokuSolverViewModel(ISudokuSolverService sudokuSolverService)
        {
            Ensure.That(sudokuSolverService, nameof(sudokuSolverService)).IsNotNull();

            _sudokuSolverService = sudokuSolverService;

            SolveCommand = ReactiveCommand.CreateFromTask(async () => await Task.Run(Solve));
            SetValuesCommand = ReactiveCommand.Create<Key>(key => SetValues(key.ToInt()));
            ClearCellsCommand = ReactiveCommand.Create(ClearCells);
            ClearFieldCommand = ReactiveCommand.Create(ClearField);

            SolveCommand.IsExecuting.Subscribe(x => IsBusy = x);

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

        private void Solve()
        {
            var filledCells = 
                Field
                    .SelectMany(x => x)
                    .Where(x => x.Value > 0)
                    .Select(x => new Cell(3, x.Row, x.Column, x.Value))
                    .ToHashSet();

            var result = _sudokuSolverService.Solve(dimensionality: 3, filledCells);

            foreach (var cell in result)
            {
                Field[cell.Row][cell.Column].Value = cell.Value;
            }
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