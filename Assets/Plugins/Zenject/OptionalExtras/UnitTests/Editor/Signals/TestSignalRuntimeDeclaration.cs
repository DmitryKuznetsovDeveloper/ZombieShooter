using NUnit.Framework;
using Plugins.Zenject.OptionalExtras.Signals.Main;
using Plugins.Zenject.OptionalExtras.TestFramework;
using Zenject;
using Assert = Plugins.Zenject.Source.Internal.Assert;

namespace Plugins.Zenject.OptionalExtras.UnitTests.Editor.Signals
{
    public class FooSignal
    {
    }

    [TestFixture]
    public class TestSignalRuntimeDeclaration : ZenjectUnitTestFixture
    {
        [SetUp]
        public void InstallCommon()
        {
            SignalBusInstaller.Install(Container);
            Container.Inject(this);
        }

        [Inject]
        SignalBus _signalBus = null;

        [Test]
        public void TestMissingDeclaration()
        {
            Assert.Throws(() => _signalBus.Fire(new FooSignal()));
        }

        [Test]
        public void TestFireSuccess()
        {
            _signalBus.DeclareSignal<FooSignal>();
            _signalBus.Fire(new FooSignal());
        }

        [Test]
        public void TestIdentifierMissing()
        {
            _signalBus.DeclareSignal<FooSignal>();
            Assert.Throws(() => _signalBus.FireId("asdf", new FooSignal()));
        }

        [Test]
        public void TestIdentifier()
        {
            _signalBus.DeclareSignal<FooSignal>("asdf");
            _signalBus.FireId("asdf", new FooSignal());
        }
    }
}
