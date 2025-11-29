using System.Collections;
using TD_Punkverse.Core;
using UnityEngine;

namespace TD_Punkverse.Game.Towers
{
	public sealed class YurtTowerView : TowerView
	{
		[SerializeField] private YurtTower _yurt;
		private PlayerService _player;

		private void Start()
		{
			_player = ServiceLocator.Instance.Get<PlayerService>();
			StartCoroutine(IncomeRoutine());
		}

		private IEnumerator IncomeRoutine()
		{
			while (true)
			{
				yield return new WaitForSeconds(_yurt.IncomeInterval);
				_player.AddMoney(_yurt.IncomePerTick);
			}
		}
	}
}
