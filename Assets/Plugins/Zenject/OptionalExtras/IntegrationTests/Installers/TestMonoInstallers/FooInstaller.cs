using Plugins.Zenject.Source.Install;
namespace Plugins.Zenject.OptionalExtras.IntegrationTests.Installers.TestMonoInstallers
{
    public class Foo
    {
    }

    public class FooInstaller : MonoInstaller<FooInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Foo>().AsSingle().NonLazy();
        }
    }
}
