using UnityEngine;

public interface IDamagable{
    public int _maxHealth {get;set;}
    public int _health {get; set;}
    public void TakeDamage(int damage);
    public void DamageVisualied();
}