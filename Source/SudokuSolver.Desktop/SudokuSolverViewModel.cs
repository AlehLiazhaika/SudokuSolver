using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EnsureThat;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SudokuSolver.Core.Contracts;
using SudokuSolver.Core.Contracts.Models;
using SudokuSolver.Desktop.ViewModels;

namespace SudokuSolver.Desktop
{
    public class SudokuSolverViewModel : ReactiveObject
    {
        private readonly ISudokuSolverService _sudokuSolverService;

        [Reactive] public bool IsBusy { get; set; }
        public List<List<CellViewModel>> Items { get; set; }

        public ICommand SolveCommand { get; }

        public ICommand Set1 { get; }
        public ICommand Set2 { get; }
        public ICommand Set3 { get; }
        public ICommand Set4 { get; }
        public ICommand Set5 { get; }
        public ICommand Set6 { get; }
        public ICommand Set7 { get; }
        public ICommand Set8 { get; }
        public ICommand Set9 { get; }
        public ICommand ClearCell { get; }
        public ICommand ClearAll { get; }

        public SudokuSolverViewModel(ISudokuSolverService sudokuSolverService)
        {
            Ensure.That(sudokuSolverService, nameof(sudokuSolverService)).IsNotNull();

            _sudokuSolverService = sudokuSolverService;

            InitializeField();

            SolveCommand = ReactiveCommand.Create(Solve);

            Set1 = ReactiveCommand.Create(() =>
            {
                foreach (var cell in Items.SelectMany(x => x).Where(x => x.IsSelected))
                {
                    cell.Value = 1;
                    cell.IsSelected = false;
                }
            });
            Set2 = ReactiveCommand.Create(() =>
            {
                foreach (var cell in Items.SelectMany(x => x).Where(x => x.IsSelected))
                {
                    cell.Value = 2;
                    cell.IsSelected = false;
                }
            });
            Set3 = ReactiveCommand.Create(() =>
            {
                foreach (var cell in Items.SelectMany(x => x).Where(x => x.IsSelected))
                {
                    cell.Value = 3;
                    cell.IsSelected = false;
                }
            });
            Set4 = ReactiveCommand.Create(() =>
            {
                foreach (var cell in Items.SelectMany(x => x).Where(x => x.IsSelected))
                {
                    cell.Value = 4;
                    cell.IsSelected = false;
                }
            });
            Set5 = ReactiveCommand.Create(() =>
            {
                foreach (var cell in Items.SelectMany(x => x).Where(x => x.IsSelected))
                {
                    cell.Value = 5;
                    cell.IsSelected = false;
                }
            });
            Set6 = ReactiveCommand.Create(() =>
            {
                foreach (var cell in Items.SelectMany(x => x).Where(x => x.IsSelected))
                {
                    cell.Value = 6;
                    cell.IsSelected = false;
                }
            });
            Set7 = ReactiveCommand.Create(() =>
            {
                foreach (var cell in Items.SelectMany(x => x).Where(x => x.IsSelected))
                {
                    cell.Value = 7;
                    cell.IsSelected = false;
                }
            });
            Set8 = ReactiveCommand.Create(() =>
            {
                foreach (var cell in Items.SelectMany(x => x).Where(x => x.IsSelected))
                {
                    cell.Value = 8;
                    cell.IsSelected = false;
                }
            });
            Set9 = ReactiveCommand.Create(() =>
            {
                foreach (var cell in Items.SelectMany(x => x).Where(x => x.IsSelected))
                {
                    cell.Value = 9;
                    cell.IsSelected = false;
                }
            });
            ClearCell = ReactiveCommand.Create(() =>
            {
                foreach (var cell in Items.SelectMany(x => x).Where(x => x.IsSelected))
                {
                    cell.Value = 0;
                    cell.IsSelected = false;
                }
            });
            ClearAll = ReactiveCommand.Create(() =>
            {
                foreach (var cell in Items.SelectMany(x => x))
                {
                    cell.Value = 0;
                    cell.IsSelected = false;
                }
            });

            void InitializeField()
            {
                Items = new List<List<CellViewModel>>();

                for (int i = 0; i < 9; ++i)
                {
                    Items.Add(new List<CellViewModel>());

                    for (int j = 0; j < 9; ++j)
                    {
                        Items[i].Add(new CellViewModel(3, i, j, 0));
                    }
                }
            }
        }

        public async Task Solve()
        {
            IsBusy = true; // TODO Remake, non-editable cells

            var filledCells = Items
                .SelectMany(x => x)
                .Where(x => x.Value > 0)
                .Select(x => new Cell(3, x.Row, x.Column, x.Value))
                .ToList();

            var result = await Task.Run(() => _sudokuSolverService.Solve(3, new HashSet<Cell>(filledCells)));

            foreach (var cell in result)
            {
                Items[cell.Row][cell.Column].Value = cell.Value;
            }

            IsBusy = false;
        }
    }
}