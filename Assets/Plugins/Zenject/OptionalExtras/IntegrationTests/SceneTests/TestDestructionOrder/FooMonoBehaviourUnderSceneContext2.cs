using UnityEngine;
namespace Plugins.Zenject.OptionalExtras.IntegrationTests.SceneTests.TestDestructionOrder
{
    public class FooMonoBehaviourUnderSceneContext2 : MonoBehaviour
    {
        public void OnDestroy()
        {
            Debug.Log("Destroyed FooMonoBehaviourUnderSceneContext2");
        }
    }
}
