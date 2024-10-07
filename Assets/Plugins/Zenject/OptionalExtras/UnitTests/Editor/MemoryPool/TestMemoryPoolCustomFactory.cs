using NUnit.Framework;
using Plugins.Zenject.OptionalExtras.TestFramework;
using Plugins.Zenject.Source.Binding.Binders.Factory.FactoryFromBinder;
using Plugins.Zenject.Source.Binding.BindInfo;
using Plugins.Zenject.Source.Factories;
using Plugins.Zenject.Source.Factories.Pooling;
using Assert = Plugins.Zenject.Source.Internal.Assert;

#pragma warning disable 219

namespace Plugins.Zenject.OptionalExtras.UnitTests.Editor.MemoryPool
{
    [TestFixture]
    public class TestMemoryPoolCustomFactory : ZenjectUnitTestFixture
    {
        [Test]
        public void TestFromBinding()
        {
            Container.BindMemoryPool<Qux, Qux.Pool>().FromIFactory(b => b.To<CustomFactory>().AsCached());

            var pool = Container.Resolve<Qux.Pool>();

            var qux = pool.Spawn();

            Assert.IsEqual(pool.NumTotal, 1);
        }

        [Test]
        public void TestFromRuntime()
        {
            var settings = new MemoryPoolSettings(0, int.MaxValue, PoolExpandMethods.OneAtATime);

            var pool = Container.Instantiate<Qux.Pool>(new object[] { settings, new CustomFactory() });

            var qux = pool.Spawn();

            Assert.IsEqual(pool.NumTotal, 1);
        }

        class CustomFactory : IFactory<Qux>
        {
            public Qux Create()
            {
                return new Qux();
            }
        }

        class Qux
        {
            public class Pool : MemoryPool<Qux>
            {
            }
        }
    }
}


