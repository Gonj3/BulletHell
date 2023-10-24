using Godot;

public interface IDamageable
{
	public DamageableKind DamageableKind { get; }

	public void TakeDamage(int damage, float angle, int force);
	public Vector2 Position { get; }
}
