using TD_Punkverse.Core.GameStates;

namespace TD_Punkverse.Core
{
	public sealed class GameStateMachine : Service
	{
		private GameState _current;

		public GameState Current => _current;

		public override void Initialize()
		{
			Subscribe();

			Switch(new PlayingGameState(this));
		}

		private void OnDisable() => Unsubscribe();

		private void Subscribe()
		{
			ServiceLocator.Instance.Get<PlayerService>().Observer.OnLose += OnLose;
		}

		private void Unsubscribe()
		{
			ServiceLocator.Instance.Get<PlayerService>().Observer.OnLose -= OnLose;

		}

		public void Switch(GameState next)
		{
			if (_current != null)
			{
				_current.Exit();
			}

			_current = next;
			_current.Enter();
		}

		private void OnLose() => Switch(new LoseGameState(this));
	}
}
