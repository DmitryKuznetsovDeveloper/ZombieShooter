using Plugins.GameCycle;
namespace Game.Components
{
    public sealed class GameTimer : IGameTickable
    {
        private float _currentTime;

        void IGameTickable.Tick(float deltaTime)
        {
            _currentTime += deltaTime;
        }
    }
}