using System;
using System.Collections;
using System.Collections.Generic;
using Plugins.Zenject.OptionalExtras.TestFramework;
using Plugins.Zenject.Source.Internal;
using Plugins.Zenject.Source.Runtime;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;
using Assert = NUnit.Framework.Assert;
namespace Plugins.Zenject.OptionalExtras.IntegrationTests.Tests.Misc.TestMonoKernelDecoration
{
    public class TestMonoKernelDecoration : ZenjectIntegrationTestFixture
    {
        
        [UnityTest]
        public IEnumerator TestDelayedMonoKernelDecorator()
        {
            PreInstall();

            Container.Rebind<InitializableManager>().To<InitializableManagerSpy>().AsCached();
            KernelDecoratorInstaller.Install(Container);
            PostInstall();
            
            yield return new WaitForSeconds(1.0f);

            InitializableManagerSpy initializableManager = SceneContext.Container.Resolve<InitializableManager>() as InitializableManagerSpy;
            var initializedBeforeDelay = initializableManager.IsInitialized;
            
            yield return new WaitForSeconds(6.0f);
            var initializedAfterDelay = initializableManager.IsInitialized;

            Assert.IsFalse(initializedBeforeDelay);
            Assert.IsTrue(initializedAfterDelay);
        }
        
        private class InitializableManagerSpy : InitializableManager
        {
            
            public InitializableManagerSpy(List<IInitializable> initializables, List<ValuePair<Type, int>> priorities) : base(initializables, priorities){}

            public bool IsInitialized => _hasInitialized;
        }
        
        
    }
}