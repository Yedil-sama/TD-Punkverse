using TD_Punkverse.Core;
using UnityEngine;

namespace TD_Punkverse.UI.Menu
{
	public sealed class TutorialUICanvas : UICanvas
	{
		private UIService _uiService;

		protected override void Initialize()
		{
			base.Initialize();

			_uiService = ServiceLocator.Instance.Get<UIService>();

			OpenOnStart = PlayerPrefs.GetInt("IsFirstSession", 1) == 1;
		}

		public void OnExitButtonPress()
		{
			Close();
			_uiService.Get<MenuUICanvas>().Open();
		}

		public void OnNextButtonPress()
		{

		}
	}
}
