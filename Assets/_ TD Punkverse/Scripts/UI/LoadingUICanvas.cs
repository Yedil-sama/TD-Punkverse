using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TD_Punkverse.UI
{
	public sealed class LoadingUICanvas : UICanvas
	{
		[SerializeField] private Slider _progressBar;
		[SerializeField] private TMP_Text _progressLabel;

		public void SetProgress(float value)
		{
			if (_progressBar != null)
				_progressBar.value = value;

			if (_progressLabel != null)
				_progressLabel.text = $"{Mathf.RoundToInt(value * 100f)}%";
		}
	}
}
