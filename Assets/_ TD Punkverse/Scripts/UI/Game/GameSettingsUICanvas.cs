using TD_Punkverse.Core;

namespace TD_Punkverse.UI.Game
{
	public sealed class GameSettingsUICanvas : SettingsUICanvas
	{
		public void OnRetryButtonPress()
		{
			ServiceLocator.Instance.Get<SceneLoaderService>().LoadScene("Game Scene");
		}

		public void OnMenuButtonPress()
		{
			ServiceLocator.Instance.Get<SceneLoaderService>().LoadScene("Menu Scene");
		}
	}
}