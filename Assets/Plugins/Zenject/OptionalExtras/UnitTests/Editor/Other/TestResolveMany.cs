using System.Collections.Generic;
using NUnit.Framework;
using Plugins.Zenject.OptionalExtras.TestFramework;
using Assert = Plugins.Zenject.Source.Internal.Assert;

namespace Plugins.Zenject.OptionalExtras.UnitTests.Editor.Other
{
    [TestFixture]
    public class TestResolveMany : ZenjectUnitTestFixture
    {
        class Test0
        {
        }

        class Test1 : Test0
        {
        }

        class Test2 : Test0
        {
        }

        [Test]
        public void TestCase1()
        {
            Container.Bind<Test0>().To<Test1>().AsSingle();
            Container.Bind<Test0>().To<Test2>().AsSingle();

            List<Test0> many = Container.ResolveAll<Test0>();

            Assert.That(many.Count == 2);
        }

        [Test]
        public void TestOptional()
        {
            List<Test0> many = Container.ResolveAll<Test0>();
            Assert.That(many.Count == 0);
        }
    }
}


