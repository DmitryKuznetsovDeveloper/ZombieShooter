using Plugins.Zenject.Source.Main;
using UnityEngine;
using Zenject;
namespace Plugins.Zenject.OptionalExtras.IntegrationTests.Misc.TestZenAutoInjecter
{
    public class Gorp : MonoBehaviour
    {
        [Inject]
        public DiContainer Container;
    }
}

