using System;
using NUnit.Framework;
using Plugins.Zenject.OptionalExtras.TestFramework;
using Plugins.Zenject.Source.Factories.Pooling.Static;
using Zenject;
using Assert = Plugins.Zenject.Source.Internal.Assert;

namespace Plugins.Zenject.OptionalExtras.UnitTests.Editor.MemoryPool
{
    [TestFixture]
    public class TestPoolableStaticMemoryPool : ZenjectUnitTestFixture
    {
        [Test]
        public void RunTest()
        {
            var pool = Foo.Pool;

            pool.Clear();

            Assert.IsEqual(pool.NumActive, 0);
            Assert.IsEqual(pool.NumInactive, 0);
            Assert.IsEqual(pool.NumTotal, 0);

            var foo = pool.Spawn("asdf");

            Assert.IsEqual(pool.NumActive, 1);
            Assert.IsEqual(pool.NumInactive, 0);
            Assert.IsEqual(pool.NumTotal, 1);
            Assert.IsEqual(foo.Data, "asdf");

            foo.Dispose();

            Assert.IsEqual(pool.NumActive, 0);
            Assert.IsEqual(pool.NumInactive, 1);
            Assert.IsEqual(pool.NumTotal, 1);
            Assert.IsEqual(foo.Data, null);
        }

        public class Foo : IPoolable<string>, IDisposable
        {
            public static readonly PoolableStaticMemoryPool<string, Foo> Pool =
                new PoolableStaticMemoryPool<string, Foo>();

            public string Data
            {
                get; private set;
            }

            public void Dispose()
            {
                Pool.Despawn(this);
            }

            public void OnSpawned(string data)
            {
                Data = data;
            }

            public void OnDespawned()
            {
                Data = null;
            }
        }
    }
}

