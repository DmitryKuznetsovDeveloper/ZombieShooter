using Plugins.Zenject.Source.Install;
using Zenject;
namespace Plugins.Zenject.OptionalExtras.IntegrationTests.Installers.TestInstallers
{
    public class BarInstaller : Installer<string, BarInstaller>
    {
        string _value;

        [Inject]
        public void Construct(string value)
        {
            _value = value;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(_value);
        }
    }
}
