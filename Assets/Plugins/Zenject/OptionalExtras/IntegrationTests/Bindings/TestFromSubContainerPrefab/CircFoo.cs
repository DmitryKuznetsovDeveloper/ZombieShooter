using UnityEngine;
using Zenject;
namespace Plugins.Zenject.OptionalExtras.IntegrationTests.Bindings.TestFromSubContainerPrefab
{
    public class CircFoo : MonoBehaviour
    {
        [Inject]
        public CircBar Bar;
    }
}
