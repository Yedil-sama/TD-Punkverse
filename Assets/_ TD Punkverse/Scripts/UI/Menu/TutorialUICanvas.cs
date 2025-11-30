using TD_Punkverse.Core;
using UnityEngine;

namespace TD_Punkverse.UI.Menu
{
	public sealed class TutorialUICanvas : UICanvas
	{
		private UIService _uiService;
		private const string IS_FIRST_SESSION = "IsFirstSession";

		protected override void Initialize()
		{
			base.Initialize();

			_uiService = ServiceLocator.Instance.Get<UIService>();

			OpenOnStart = PlayerPrefs.GetInt(IS_FIRST_SESSION, 1) == 1;
			PlayerPrefs.SetInt(IS_FIRST_SESSION, 0);
			PlayerPrefs.Save();
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
