using System.Collections;
using Plugins.Zenject.OptionalExtras.IntegrationTests.Misc.TestAnimationStateBehaviourInject;
using Plugins.Zenject.OptionalExtras.IntegrationTests.Tests.Util;
using Plugins.Zenject.OptionalExtras.TestFramework;
using Plugins.Zenject.Source.Internal;
using UnityEngine.TestTools;
using Zenject;
namespace Plugins.Zenject.OptionalExtras.IntegrationTests.Tests.Misc.TestAnimationStateBehaviourInject
{
    public class TestAnimationStateBehaviourInject : ZenjectIntegrationTestFixture
    {
        const string ResourcePrefix = "TestAnimationStateBehaviourInject/";

        [UnityTest]
        public IEnumerator Test1()
        {
            PreInstall();
            var prefab = FixtureUtil.GetPrefab(ResourcePrefix + "Foo");

            StateBehaviour1.OnStateEnterCalls = 0;

            Container.InstantiatePrefab(prefab);
            Container.BindInterfacesAndSelfTo<Foo>().AsSingle();
            PostInstall();

            yield return null;

            Assert.IsEqual(StateBehaviour1.OnStateEnterCalls, 1);
        }

        public class Foo : IInitializable
        {
            public bool HasInitialized
            {
                get; private set;
            }

            public void Initialize()
            {
                HasInitialized = true;
            }
        }
    }
}

