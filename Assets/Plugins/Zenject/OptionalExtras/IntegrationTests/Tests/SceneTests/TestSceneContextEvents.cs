using System.Collections;
using Plugins.Zenject.OptionalExtras.TestFramework;
using UnityEngine;
using UnityEngine.TestTools;
namespace Plugins.Zenject.OptionalExtras.IntegrationTests.Tests.SceneTests
{
    public class TestSceneContextEvents : SceneTestFixture
    {
        [UnityTest]
        public IEnumerator TestScene()
        {
            yield return LoadScene("TestSceneContextEvents");
            yield return new WaitForSeconds(2.0f);
        }
    }
}
