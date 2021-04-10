using AlgorithmX.Contracts;
using Ninject;
using Ninject.Modules;

namespace AlgorithmX
{
    public sealed class AlgorithmXModule : NinjectModule
    {
        public override void Load() => RegisterServices(Kernel);

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IAlgorithmX>().To<AlgorithmX>();
        }
    }
}