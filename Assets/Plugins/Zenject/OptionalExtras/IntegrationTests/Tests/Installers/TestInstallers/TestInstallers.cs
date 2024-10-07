using System.Collections;
using Plugins.Zenject.OptionalExtras.IntegrationTests.Installers.TestInstallers;
using Plugins.Zenject.OptionalExtras.IntegrationTests.Tests.Util;
using Plugins.Zenject.OptionalExtras.TestFramework;
using Plugins.Zenject.Source.Internal;
using UnityEngine.TestTools;
namespace Plugins.Zenject.OptionalExtras.IntegrationTests.Tests.Installers.TestInstallers
{
    public class TestInstallers : ZenjectIntegrationTestFixture
    {
        [UnityTest]
        public IEnumerator TestZeroArgs()
        {
            PreInstall();
            FooInstaller.Install(Container);

            PostInstall();

            FixtureUtil.AssertResolveCount<Foo>(Container, 1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestOneArg()
        {
            PreInstall();
            BarInstaller.Install(Container, "blurg");

            PostInstall();

            Assert.IsEqual(Container.Resolve<string>(), "blurg");
            yield break;
        }

        [UnityTest]
        public IEnumerator TestThreeArgs()
        {
            PreInstall();
            QuxInstaller.Install(Container, "blurg", 2.0f, 1);

            PostInstall();

            Assert.IsEqual(Container.Resolve<string>(), "blurg");
            yield break;
        }
    }
}

