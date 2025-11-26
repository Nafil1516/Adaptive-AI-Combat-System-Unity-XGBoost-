using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    public int damage = 10;

    private bool canDamage = false;

    public void EnableDamage() => canDamage = true;
    public void DisableDamage() => canDamage = false;

    public bool CanDamage() => canDamage;
}
