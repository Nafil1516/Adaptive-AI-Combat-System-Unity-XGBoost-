using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject hitbox;  // assign in inspector

    public void EnableHitbox()
    {
        hitbox.SetActive(true);
    }

    public void DisableHitbox()
    {
        hitbox.SetActive(false);
    }
}
