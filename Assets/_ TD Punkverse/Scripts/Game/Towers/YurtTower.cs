using System;

namespace TD_Punkverse.Game.Towers
{
	[Serializable]
	public sealed class YurtTower : Tower
	{
		public int IncomePerTick = 1;
		public float IncomeInterval = 3f;

		public YurtTower(string name, int cost, float workSpeed) : base(name, cost, workSpeed) { }
	}
}
