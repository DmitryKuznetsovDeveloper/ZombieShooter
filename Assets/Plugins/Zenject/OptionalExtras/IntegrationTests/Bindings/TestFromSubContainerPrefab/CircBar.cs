using UnityEngine;
using Zenject;
namespace Plugins.Zenject.OptionalExtras.IntegrationTests.Bindings.TestFromSubContainerPrefab
{
    public class CircBar : MonoBehaviour
    {
        [Inject]
        public CircFoo Foo;
    }
}
