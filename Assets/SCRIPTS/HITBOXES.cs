using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject hitbox;

    public int damage;
    private bool canDamage;

    public void EnableDamage() => canDamage = true;
    public void DisableDamage() => canDamage = false;
    public bool CanDamage() => canDamage;

    public void EnableHitbox()
    {
        hitbox.SetActive(true);
    }

    public void DisableHitbox()
    {
        hitbox.SetActive(false);

        
    }

}
