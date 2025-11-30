using System;
using System.Collections;
using TD_Punkverse.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TD_Punkverse.Core
{
	public sealed class SceneLoaderService : Service
	{
		private const float ReadyThreshold = 0.9f;

		private UIService _uiService;
		private LoadingUICanvas _loadingUICanvas;
		private bool _isLoading;

		public override void Initialize()
		{
			_uiService = ServiceLocator.Instance.Get<UIService>();
			_loadingUICanvas = _uiService.Get<LoadingUICanvas>();
		}

		public void LoadScene(string sceneName, Action onLoaded = null)
		{
			if (_isLoading)
				return;

			_isLoading = true;
			ServiceLocator.Instance.StartCoroutine(LoadRoutine(sceneName, onLoaded));
		}

		private IEnumerator LoadRoutine(string sceneName, Action onLoaded)
		{
			OpenLoadingUICanvas();
			UpdateProgress(0f);

			AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
			operation.allowSceneActivation = false;

			while (!operation.isDone)
			{
				float progress = Mathf.Clamp01(operation.progress / ReadyThreshold);
				UpdateProgress(progress);

				if (operation.progress >= ReadyThreshold)
					break;

				yield return null;
			}

			UpdateProgress(1f);
			yield return new WaitForSeconds(0.1f);

			operation.allowSceneActivation = true;
			yield return null;

			_isLoading = false;
			CloseLoadingUICanvas();
			onLoaded?.Invoke();
		}

		private void UpdateProgress(float value)
		{
			if (_loadingUICanvas != null)
				_loadingUICanvas.SetProgress(value);
		}

		private void OpenLoadingUICanvas()
		{
			if (_loadingUICanvas != null)
				_loadingUICanvas.Open();
		}

		private void CloseLoadingUICanvas()
		{
			if (_loadingUICanvas != null)
				_loadingUICanvas.Close();
		}
	}
}
