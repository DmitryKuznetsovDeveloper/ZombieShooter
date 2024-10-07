using Plugins.Zenject.Source.Install;
namespace Plugins.Zenject.OptionalExtras.IntegrationTests.SceneContractTests.AutoLoader
{
    public class Scene2Installer : MonoInstaller<Scene2Installer>
    {
        public override void InstallBindings()
        {
            Container.Bind<Bar>().AsSingle();
        }
    }
}
