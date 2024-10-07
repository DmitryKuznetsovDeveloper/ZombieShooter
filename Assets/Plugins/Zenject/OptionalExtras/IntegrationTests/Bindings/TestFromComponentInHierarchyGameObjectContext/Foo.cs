using System;
using UnityEngine;
using Zenject;
namespace Plugins.Zenject.OptionalExtras.IntegrationTests.Bindings.TestFromComponentInHierarchyGameObjectContext
{
    public class Foo : MonoBehaviour
    {
        [NonSerialized]
        [Inject]
        public Gorp Gorp;
    }
}
