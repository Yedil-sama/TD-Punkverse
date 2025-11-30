using TD_Punkverse.Core;

namespace TD_Punkverse.UI.Menu
{
	public sealed class CreditsUICanvas : UICanvas
	{
		private UIService _uiService;

		protected override void Initialize()
		{
			base.Initialize();

			_uiService = ServiceLocator.Instance.Get<UIService>();
		}

		public void OnExitButtonPress()
		{
			Close();
			_uiService.Get<MenuUICanvas>().Open();
		}
	}
}