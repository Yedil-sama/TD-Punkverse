using TD_Punkverse.Game.Enemies;

namespace TD_Punkverse.Game.Projectiles
{
	public sealed class TargetProjectile : Projectile
	{
		public readonly Enemy Target;

		public TargetProjectile(int damage, float speed, Enemy target) : base(damage, speed)
		{
			Target = target;
		}
	}
}
