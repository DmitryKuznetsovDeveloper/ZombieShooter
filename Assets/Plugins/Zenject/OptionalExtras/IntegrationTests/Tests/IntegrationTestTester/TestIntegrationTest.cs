using System;
using System.Collections;
using Plugins.Zenject.OptionalExtras.TestFramework;
using Plugins.Zenject.Source.Install.Contexts;
using Plugins.Zenject.Source.Internal;
using UnityEngine.TestTools;
using Zenject;
namespace Plugins.Zenject.OptionalExtras.IntegrationTests.Tests.IntegrationTestTester
{
    public class TestIntegrationTest : ZenjectIntegrationTestFixture
    {
        public class Foo : IInitializable, IDisposable
        {
            public static bool WasDisposed
            {
                get; set;
            }

            public static bool WasInitialized
            {
                get; set;
            }

            public void Initialize()
            {
                WasInitialized = true;
            }

            public void Dispose()
            {
                WasDisposed = true;
            }
        }

        [UnityTest]
        public IEnumerator TestRun()
        {
            PreInstall();

            Foo.WasDisposed = false;
            Foo.WasInitialized = false;

            Container.BindInterfacesTo<Foo>().AsSingle();

            Assert.That(!Foo.WasInitialized);

            PostInstall();

            yield return null;

            Assert.That(Foo.WasInitialized);

            yield return DestroyEverything();

            Assert.That(Foo.WasDisposed);
        }

        [UnityTest]
        public IEnumerator TestSkipInstall()
        {
            SkipInstall();
            yield break;
        }

        [UnityTest]
        public IEnumerator TestProjectContextDestroyed()
        {
            Assert.That(!ProjectContext.HasInstance);
            SkipInstall();
            yield return null;
            Assert.That(ProjectContext.HasInstance);
            yield return DestroyEverything();
            Assert.That(!ProjectContext.HasInstance);
        }
    }
}
