using Plugins.Zenject.Source.Internal;
using UnityEngine;
using Zenject;
#pragma warning disable 649

namespace Plugins.Zenject.OptionalExtras.IntegrationTests.Bindings.TestFromPrefab
{
    public class Qux : MonoBehaviour
    {
        [Inject]
        int _arg;

        [Inject]
        public void Initialize()
        {
            Log.Trace("Received arg '{0}' in Qux", _arg);
        }
    }
}
