using System.Collections;
using TD_Punkverse.Core;
using UnityEngine;

namespace TD_Punkverse.Game.Towers
{
	public sealed class DataBazarTowerView : TowerView
	{
		[SerializeField] private DataBazarTower _tower;

		private PlayerService _playerService;
		private Coroutine _incomeRoutine;

		private void Start()
		{
			_playerService = ServiceLocator.Instance.Get<PlayerService>();
			_incomeRoutine = StartCoroutine(IncomeRoutine());
		}

		private IEnumerator IncomeRoutine()
		{
			while (true)
			{
				yield return new WaitForSeconds(_tower.WorkSpeed);
				_playerService.AddMoney(_tower.IncomePerTick);
			}
		}

		public new void FinalizePlacement()
		{
			base.FinalizePlacement();
		}

		private void OnDestroy()
		{
			if (_incomeRoutine != null)
				StopCoroutine(_incomeRoutine);
		}
	}
}
