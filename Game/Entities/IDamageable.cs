public interface IDamageable
{
    public DamageableKind DamageableKind { get; }

    public void TakeDamage(int damage);
}
