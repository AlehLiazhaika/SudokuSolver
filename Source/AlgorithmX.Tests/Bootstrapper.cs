using Ninject;
using Prism.Ninject;

namespace AlgorithmX.Tests
{
    internal sealed class Bootstrapper : NinjectBootstrapper
    {
        protected override void InitializeModules() => LoadModules();

        private void LoadModules() => Kernel.Load<AlgorithmXModule>();
    }
}