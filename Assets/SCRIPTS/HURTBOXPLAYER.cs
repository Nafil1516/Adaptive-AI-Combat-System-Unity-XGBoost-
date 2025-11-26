using UnityEngine;

public class PlayerHurtBox : MonoBehaviour
{
    private Movement player;

    void Awake()
    {
        player = GetComponentInParent<Movement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENEMY HIT THE PLAYER");
        EnemyHitBox hit = other.GetComponent<EnemyHitBox>();
        if (hit == null) return; 

        // if (hit.CanDamage())
        // {
            player.TakeDamage(hit.damage);
       // }
    }
}
