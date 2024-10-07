using System.Collections;
using System.Linq;
using NUnit.Framework;
using Plugins.Zenject.OptionalExtras.IntegrationTests.Bindings.TestFromComponentInHierarchyGameObjectContext;
using Plugins.Zenject.OptionalExtras.IntegrationTests.Tests.Util;
using Plugins.Zenject.OptionalExtras.TestFramework;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = Plugins.Zenject.Source.Internal.Assert;

namespace Plugins.Zenject.OptionalExtras.IntegrationTests.Tests.Bindings.TestFromComponentInHierarchyGameObjectContext
{
    public class TestFromComponentInHierarchyGameObjectContext : ZenjectIntegrationTestFixture
    {
        GameObject FooPrefab
        {
            get
            {
                return FixtureUtil.GetPrefab("TestFromComponentInHierarchyGameObjectContext/Foo");
            }
        }

        [SetUp]
        public void SetUp()
        {
            new GameObject().AddComponent<Gorp>();
            new GameObject().AddComponent<Gorp>();
        }

        [UnityTest]
        public IEnumerator TestCorrectHierarchy()
        {
            PreInstall();

            Container.Bind<Foo>().FromSubContainerResolve()
                .ByNewContextPrefab(FooPrefab).AsSingle().NonLazy();

            PostInstall();

            var foo = Container.Resolve<Foo>();

            Assert.IsNotNull(foo.Gorp);
            Assert.IsEqual(foo.gameObject.GetComponentsInChildren<Gorp>().Single(), foo.Gorp);
            yield break;
        }
    }
}

