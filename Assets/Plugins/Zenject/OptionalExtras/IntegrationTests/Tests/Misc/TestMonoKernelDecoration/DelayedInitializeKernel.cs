using System.Threading.Tasks;
using Plugins.Zenject.Source.Runtime.Kernels;
namespace Plugins.Zenject.OptionalExtras.IntegrationTests.Tests.Misc.TestMonoKernelDecoration
{
    public class DelayedInitializeKernel : BaseMonoKernelDecorator
    {
        public async override void Initialize()
        {
            await Task.Delay(5000);
            DecoratedMonoKernel.Initialize();
        }
    }
}