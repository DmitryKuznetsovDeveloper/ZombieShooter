using System.Collections;
using Plugins.Zenject.OptionalExtras.IntegrationTests.Installers.TestScriptableObjectInstallers;
using Plugins.Zenject.OptionalExtras.IntegrationTests.Tests.Util;
using Plugins.Zenject.OptionalExtras.TestFramework;
using Plugins.Zenject.Source.Internal;
using UnityEngine.TestTools;
namespace Plugins.Zenject.OptionalExtras.IntegrationTests.Tests.Installers.TestScriptableObjectInstallers
{
    public class TestScriptableObjectInstallers : ZenjectIntegrationTestFixture
    {
        [UnityTest]
        public IEnumerator TestBadResourcePath()
        {
            PreInstall();
            Assert.Throws(() => FooInstaller.InstallFromResource("TestScriptableObjectInstallers/SDFSDFSDF", Container));
            PostInstall();
            yield break;
        }

        [UnityTest]
        public IEnumerator TestZeroArgs()
        {
            PreInstall();
            FooInstaller.InstallFromResource("TestScriptableObjectInstallers/FooInstaller", Container);

            PostInstall();

            FixtureUtil.AssertResolveCount<Foo>(Container, 1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestOneArg()
        {
            PreInstall();
            BarInstaller.InstallFromResource("TestScriptableObjectInstallers/BarInstaller", Container, "blurg");

            PostInstall();

            Assert.IsEqual(Container.Resolve<string>(), "blurg");
            yield break;
        }

        [UnityTest]
        public IEnumerator TestThreeArgs()
        {
            PreInstall();
            QuxInstaller.InstallFromResource("TestScriptableObjectInstallers/QuxInstaller", Container, "blurg", 2.0f, 1);

            PostInstall();

            Assert.IsEqual(Container.Resolve<string>(), "blurg");
            yield break;
        }
    }
}

