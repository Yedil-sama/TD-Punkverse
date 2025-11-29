using System;
using UnityEngine;

namespace TD_Punkverse.Game.Towers
{
	[Serializable]
	public sealed class DataBazarTower : Tower
	{
		[SerializeField] private int _incomePerTick = 5;
		public int IncomePerTick => _incomePerTick;

		public DataBazarTower(string name, int cost, float workSpeed) : base(name, cost, workSpeed) { }
	}
}
