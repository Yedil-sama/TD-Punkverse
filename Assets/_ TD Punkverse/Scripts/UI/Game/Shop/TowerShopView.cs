using TD_Punkverse.Core;
using TD_Punkverse.Game;
using TD_Punkverse.Game.Towers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TD_Punkverse.UI.Game.Shop
{
	public sealed class TowerShopView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		[SerializeField] private Image _icon;
		[SerializeField] private TMP_Text _cost;
		[SerializeField] private TowerView _towerPrefab;

		private TowerPlacerService _placer;

		private void Awake()
		{
			_placer = ServiceLocator.Instance.Get<TowerPlacerService>();
		}

		public void Set(TowerView towerView)
		{
			_towerPrefab = towerView;
			_icon.sprite = towerView.GetComponent<SpriteRenderer>().sprite;
			Tower data = towerView.Tower;
			_cost.text = data.Cost.ToString();
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			_placer.StartDrag(_towerPrefab);
		}

		public void OnDrag(PointerEventData eventData)
		{
			_placer.UpdateDrag();
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			_placer.EndDrag();
		}
	}
}
