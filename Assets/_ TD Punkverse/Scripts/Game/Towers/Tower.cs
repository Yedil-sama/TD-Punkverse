namespace TD_Punkverse.Game.Towers
{
	public class Tower
	{
		private string _name;
		private int _cost;
		private float _workSpeed;

		public string Name => _name;
		public int Cost => _cost;
		public float WorkSpeed => _workSpeed;

		public Tower(string name, int cost, float workSpeed)
		{
			_name = name;
			_cost = cost;
			_workSpeed = workSpeed;
		}
	}
}