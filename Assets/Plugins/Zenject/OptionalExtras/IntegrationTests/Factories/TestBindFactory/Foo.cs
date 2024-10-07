using Plugins.Zenject.Source.Factories;
using UnityEngine;
namespace Plugins.Zenject.OptionalExtras.IntegrationTests.Factories.TestBindFactory
{
    public interface IFoo
    {
    }

    public class IFooFactory : PlaceholderFactory<IFoo>
    {
    }

    public class Foo : MonoBehaviour, IFoo
    {
        public class Factory : PlaceholderFactory<Foo>
        {
        }
    }
}
