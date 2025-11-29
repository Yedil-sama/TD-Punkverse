using System;

namespace TD_Punkverse.Game.Towers
{
	[Serializable]
	public class Tower
	{
		public string Name;
		public int Cost;
		public float WorkSpeed;

		public Tower(string name, int cost, float workSpeed)
		{
			Name = name;
			Cost = cost;
			WorkSpeed = workSpeed;
		}
	}
}