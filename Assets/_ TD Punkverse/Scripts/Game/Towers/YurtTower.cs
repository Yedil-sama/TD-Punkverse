using System;
using UnityEngine;

namespace TD_Punkverse.Game.Towers
{
	[Serializable]
	public sealed class YurtTower : Tower
	{
		[SerializeField] private float _workSpeedBuff = 0.2f;
		[SerializeField] private float _range = 8f;

		public float WorkSpeedBuff => _workSpeedBuff;
		public float Range => _range;

		public YurtTower(string name, int cost, float workSpeed)
			: base(name, cost, workSpeed) { }
	}
}
