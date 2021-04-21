using Ninject.Modules;

namespace SudokuSolver.Core
{
    public sealed class Module : NinjectModule
    {
        public override void Load() => Bind<ISudokuSolverService>().To<SudokuSolverService>().InSingletonScope();
    }
}