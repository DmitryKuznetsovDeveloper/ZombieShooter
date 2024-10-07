using Plugins.Zenject.OptionalExtras.SampleGame2__Advanced_.Scripts.Enemy;
using Plugins.Zenject.OptionalExtras.SampleGame2__Advanced_.Scripts.Enemy.States;
using Plugins.Zenject.Source.Install;
namespace Plugins.Zenject.OptionalExtras.SampleGame2__Advanced_.Scripts.Installers
{
    public class EnemyInstaller : Installer<EnemyInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemyTunables>().AsSingle();

            Container.BindInterfacesAndSelfTo<EnemyStateManager>().AsSingle();

            Container.Bind<EnemyStateIdle>().AsSingle();
            Container.Bind<EnemyStateAttack>().AsSingle();
            Container.Bind<EnemyStateFollow>().AsSingle();

            Container.BindInterfacesAndSelfTo<EnemyDeathHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyRotationHandler>().AsSingle();
        }
    }
}
