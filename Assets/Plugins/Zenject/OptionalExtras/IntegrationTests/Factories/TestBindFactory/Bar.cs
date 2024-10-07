using Plugins.Zenject.Source.Factories;
using UnityEngine;
namespace Plugins.Zenject.OptionalExtras.IntegrationTests.Factories.TestBindFactory
{
    //[CreateAssetMenu(fileName = "Bar", menuName = "Installers/Bar")]
    public class Bar : ScriptableObject
    {
        public class Factory : PlaceholderFactory<Bar>
        {
        }
    }
}

