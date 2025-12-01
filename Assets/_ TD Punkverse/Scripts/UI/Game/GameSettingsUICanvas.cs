using TD_Punkverse.Core;

namespace TD_Punkverse.UI.Game
{
	public sealed class GameSettingsUICanvas : SettingsUICanvas
	{

		private GameStateMachine _gameStateMachine;
		public void OnRetryButtonPress()
		{
			ServiceLocator.Instance.Get<SceneLoaderService>().LoadScene("Game Scene");
			//_gameStateMachine = ServiceLocator.Instance.Get<GameStateMachine>();

			//_gameStateMachine.Switch(new PlayingGameState(_gameStateMachine));
		}

		public void OnMenuButtonPress()
		{
			ServiceLocator.Instance.Get<SceneLoaderService>().LoadScene("Menu Scene");
		}
	}
}