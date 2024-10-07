using System;
using UnityEngine;
using Zenject;
namespace Plugins.Zenject.OptionalExtras.IntegrationTests.Bindings.TestFromPrefab
{
    public class Jim : MonoBehaviour
    {
        [NonSerialized]
        [Inject]
        public Bob Bob;
    }
}
