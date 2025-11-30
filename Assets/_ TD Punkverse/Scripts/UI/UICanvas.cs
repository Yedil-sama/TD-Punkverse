using UnityEngine;

namespace TD_Punkverse.UI
{
	[RequireComponent(typeof(Canvas))]
	public abstract class UICanvas : MonoBehaviour
	{
		[Header("UICanvas")]
		[SerializeField] private bool _openOnStart = false;
		private Canvas _canvas;

		protected virtual void Initialize()
		{
			_canvas = GetComponent<Canvas>();
		}

		private void Start()
		{
			Initialize();

			if (_openOnStart)
			{
				Open();
			}
			else
			{
				Close();
			}
		}

		public virtual void Open()
		{
			gameObject.SetActive(true);
			_canvas.enabled = true;
		}

		public virtual void Close()
		{
			gameObject.SetActive(false);
			_canvas.enabled = false;
		}
	}
}
