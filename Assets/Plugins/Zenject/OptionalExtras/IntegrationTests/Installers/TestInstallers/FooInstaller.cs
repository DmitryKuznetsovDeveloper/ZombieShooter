using Plugins.Zenject.Source.Install;
namespace Plugins.Zenject.OptionalExtras.IntegrationTests.Installers.TestInstallers
{
    public class Foo
    {
    }

    public class FooInstaller : Installer<FooInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Foo>().AsSingle().NonLazy();
        }
    }
}
