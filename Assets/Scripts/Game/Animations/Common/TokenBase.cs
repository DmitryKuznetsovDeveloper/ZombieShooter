using System.Threading;
using UnityEngine;
namespace Game.Animations.Common
{
    public class TokenBase : MonoBehaviour
    {
        public CancellationToken Token => _cancellationTokenSource.Token;
        private CancellationTokenSource _cancellationTokenSource;

        private void OnEnable() => _cancellationTokenSource = new CancellationTokenSource();
        private void OnDisable() => _cancellationTokenSource.Cancel();
        private void OnDestroy() => _cancellationTokenSource.Cancel();
    }
}