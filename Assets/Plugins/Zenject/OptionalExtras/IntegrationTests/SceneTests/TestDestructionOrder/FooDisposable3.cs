using System;
using UnityEngine;
namespace Plugins.Zenject.OptionalExtras.IntegrationTests.SceneTests.TestDestructionOrder
{
    public class FooDisposable3 : IDisposable
    {
        public void Dispose()
        {
            Debug.Log("Destroyed FooDisposable3");
        }
    }
}
