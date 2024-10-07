#if !(UNITY_WSA && ENABLE_DOTNET)

using System.Linq;
using NUnit.Framework;
using Plugins.Zenject.OptionalExtras.TestFramework;
using Plugins.Zenject.Source.Internal;
using UnityEngine;
using Assert = Plugins.Zenject.Source.Internal.Assert;

namespace Plugins.Zenject.OptionalExtras.UnitTests.Editor.Conventions
{
    [TestFixture]
    public class TestConvention : ZenjectUnitTestFixture
    {
        [Test]
        public void TestDerivingFrom()
        {
            Container.Bind<IFoo>()
                .To(x => x.AllTypes().DerivingFrom<IFoo>().FromThisAssembly()).AsTransient();

            Assert.IsEqual(Container.ResolveAll<IFoo>().Count(), 4);
        }

        [Test]
        public void TestDerivingFrom2()
        {
            Container.Bind<IFoo>()
                .To(x => x.AllTypes().DerivingFrom<IFoo>()).AsTransient();

            Assert.IsEqual(Container.ResolveAll<IFoo>().Count(), 4);
        }

        [Test]
        public void TestMatchAll()
        {
            // Should automatically filter by contract types
            Container.Bind<IFoo>().To(x => x.AllNonAbstractClasses()).AsTransient();

            Assert.IsEqual(Container.ResolveAll<IFoo>().Count(), 4);
        }

#if !NOT_UNITY3D
        [Test]
        public void TestDerivingFromFail()
        {
            Container.Bind<IFoo>()
                .To(x => x.AllTypes().DerivingFrom<IFoo>().FromAssemblyContaining<Vector3>()).AsTransient();

            Assert.That(Container.ResolveAll<IFoo>().IsEmpty());
        }
#endif

        [Test]
        public void TestAttributeFilter()
        {
            Container.Bind<IFoo>()
                .To(x => x.AllTypes().WithAttribute<ConventionTestAttribute>()).AsTransient();

            Assert.IsEqual(Container.ResolveAll<IFoo>().Count(), 2);
        }

        [Test]
        public void TestAttributeWhereFilter()
        {
            Container.Bind<IFoo>()
                .To(x => x.AllTypes().WithAttributeWhere<ConventionTestAttribute>(a => a.Num == 1)).AsTransient();

            Assert.IsEqual(Container.ResolveAll<IFoo>().Count(), 1);
        }

        [Test]
        public void TestInNamespace()
        {
            Container.Bind<IFoo>()
                .To(x => x.AllTypes().DerivingFrom<IFoo>().InNamespace("Zenject.Tests.Convention.NamespaceTest")).AsTransient();

            Assert.IsEqual(Container.ResolveAll<IFoo>().Count(), 1);
        }
    }
}

#endif
