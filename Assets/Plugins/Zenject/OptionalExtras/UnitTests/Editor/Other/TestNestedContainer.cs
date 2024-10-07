using NUnit.Framework;
using Plugins.Zenject.OptionalExtras.TestFramework;
using Plugins.Zenject.Source.Main;
using Assert = Plugins.Zenject.Source.Internal.Assert;

namespace Plugins.Zenject.OptionalExtras.UnitTests.Editor.Other
{
    [TestFixture]
    public class TestNestedContainer : ZenjectUnitTestFixture
    {
        public interface IFoo
        {
            int GetBar();
        }

        public class Foo : IFoo
        {
            public int GetBar()
            {
                return 0;
            }
        }

        public class Foo2 : IFoo
        {
            public int GetBar()
            {
                return 1;
            }
        }

        [Test]
        public void TestCase1()
        {
            var container1 = new DiContainer();

            Assert.Throws(() => container1.Resolve<IFoo>());
            Assert.Throws(() => Container.Resolve<IFoo>());

            Container.Bind<IFoo>().To<Foo>().AsSingle();

            Assert.Throws(() => container1.Resolve<IFoo>());
            Assert.IsEqual(Container.Resolve<IFoo>().GetBar(), 0);

            var container2 = Container.CreateSubContainer();

            Assert.IsEqual(container2.Resolve<IFoo>().GetBar(), 0);
            Assert.IsEqual(Container.Resolve<IFoo>().GetBar(), 0);

            container2.Bind<IFoo>().To<Foo2>().AsSingle();

            Assert.IsEqual(container2.Resolve<IFoo>().GetBar(), 1);
            Assert.IsEqual(Container.Resolve<IFoo>().GetBar(), 0);
        }
    }
}
