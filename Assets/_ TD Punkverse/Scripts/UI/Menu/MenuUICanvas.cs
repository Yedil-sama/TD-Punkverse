using TD_Punkverse.Core;

namespace TD_Punkverse.UI.Menu
{
	public sealed class MenuUICanvas : UICanvas
	{
		private UIService _uiService;
		private SceneLoaderService _sceneLoaderService;


		protected override void Initialize()
		{
			base.Initialize();

			_uiService = ServiceLocator.Instance.Get<UIService>();
			_sceneLoaderService = ServiceLocator.Instance.Get<SceneLoaderService>();
		}

		public void OnPlayButtonPress()
		{
			_sceneLoaderService.LoadScene("Game Scene");
		}

		public void OnCreditsButtonPress()
		{
			Close();
			_uiService.Get<CreditsUICanvas>().Open();
		}

		public void OnSettingsButtonPress()
		{
			_uiService.Get<SettingsUICanvas>().Open();
		}
	}
}
