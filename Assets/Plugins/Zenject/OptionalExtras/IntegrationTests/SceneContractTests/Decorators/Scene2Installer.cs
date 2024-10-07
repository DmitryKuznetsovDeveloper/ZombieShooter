using Plugins.Zenject.Source.Install;
using Plugins.Zenject.Source.Internal;
namespace Plugins.Zenject.OptionalExtras.IntegrationTests.SceneContractTests.Decorators
{
    public class Bar
    {
    }

    public class Foo
    {
        public Foo(Bar bar)
        {
            Log.Trace("Created Foo");
        }
    }

    public class Scene2Installer : MonoInstaller<Scene2Installer>
    {
        public override void InstallBindings()
        {
            Container.Bind<Foo>().AsSingle().NonLazy();
        }
    }
}
