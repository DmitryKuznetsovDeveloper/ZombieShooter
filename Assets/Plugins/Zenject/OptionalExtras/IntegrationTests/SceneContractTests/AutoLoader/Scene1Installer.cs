using Plugins.Zenject.Source.Install;
namespace Plugins.Zenject.OptionalExtras.IntegrationTests.SceneContractTests.AutoLoader
{
    public class Scene1Installer : MonoInstaller<Scene1Installer>
    {
        public override void InstallBindings()
        {
            Container.Bind<Qux>().AsSingle();
        }
    }
}
