using Plugins.Zenject.Source.Internal;
using UnityEngine;
using Zenject;
#pragma warning disable 649

namespace Plugins.Zenject.OptionalExtras.IntegrationTests.Bindings.TestDiContainerMethods
{
    public class Gorp : MonoBehaviour
    {
        [Inject]
        string _arg;

        public string Arg
        {
            get { return _arg; }
        }

        [Inject]
        public void Initialize()
        {
            Log.Trace("Received arg '{0}' in Gorp", _arg);
        }
    }
}
