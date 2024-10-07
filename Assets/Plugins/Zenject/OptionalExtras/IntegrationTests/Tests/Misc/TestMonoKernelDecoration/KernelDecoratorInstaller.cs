using Plugins.Zenject.Source.Install;
using Plugins.Zenject.Source.Runtime.Kernels;
namespace Plugins.Zenject.OptionalExtras.IntegrationTests.Tests.Misc.TestMonoKernelDecoration
{
    public class KernelDecoratorInstaller : Installer<KernelDecoratorInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<DecoratableMonoKernel>().AsCached();
            Container.Decorate<IDecoratableMonoKernel>().With<DelayedInitializeKernel>();
        }
    }
}