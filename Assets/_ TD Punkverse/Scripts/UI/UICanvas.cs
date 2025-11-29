using UnityEngine;

namespace TD_Punkverse.UI
{
	[RequireComponent(typeof(Canvas))]
	public abstract class UICanvas : MonoBehaviour
	{
		private Canvas _canvas;

		protected virtual void Awake()
		{
			_canvas = GetComponent<Canvas>();
		}

		public virtual void Open()
		{
			gameObject.SetActive(true);
		}

		public virtual void Close()
		{
			gameObject.SetActive(false);
		}
	}
}
