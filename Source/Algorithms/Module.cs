using Ninject.Modules;

namespace Algorithms
{
    public sealed class Module : NinjectModule
    {
        public override void Load() => Bind<IAlgorithmX>().To<AlgorithmX>().InSingletonScope();
    }
}