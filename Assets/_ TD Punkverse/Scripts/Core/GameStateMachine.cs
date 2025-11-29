using TD_Punkverse.Core.GameStates;
using TD_Punkverse.Game.Enemies;

namespace TD_Punkverse.Core
{
	public sealed class GameStateMachine : Service
	{
		private GameState _current;

		private PlayerService _playerService;
		private EnemyWaveService _enemyWaveService;

		public GameState Current => _current;

		public override void Initialize()
		{
			_playerService = ServiceLocator.Instance.Get<PlayerService>();
			_enemyWaveService = ServiceLocator.Instance.Get<EnemyWaveService>();

			Subscribe();
			Switch(new PlayingGameState(this));
		}

		private void OnDisable() => Unsubscribe();

		private void Subscribe()
		{
			_playerService.Observer.OnLose += OnLose;
			_enemyWaveService.OnWin += OnWin;
		}

		private void Unsubscribe()
		{
			_playerService.Observer.OnLose -= OnLose;
			_enemyWaveService.OnWin -= OnWin;
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

		private void OnLose()
		{
			Switch(new LoseGameState(this));
		}

		private void OnWin()
		{
			Switch(new WinGameState(this));
			GameEventBus.RaiseWin();
		}
	}
}
