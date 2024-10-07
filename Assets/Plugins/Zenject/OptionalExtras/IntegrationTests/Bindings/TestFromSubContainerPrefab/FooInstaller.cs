using Plugins.Zenject.Source.Install;
using UnityEngine;
#pragma warning disable 649

namespace Plugins.Zenject.OptionalExtras.IntegrationTests.Bindings.TestFromSubContainerPrefab
{
    public class FooInstaller : MonoInstaller
    {
        [SerializeField]
        Bar _bar;

        public override void InstallBindings()
        {
            Container.BindInstance(_bar);
            Container.Bind<Gorp>().WithId("gorp").AsSingle();
        }
    }
}
