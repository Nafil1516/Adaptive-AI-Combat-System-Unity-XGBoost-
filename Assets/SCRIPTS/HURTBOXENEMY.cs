using UnityEngine;

public class EnemyHurtBox : MonoBehaviour
{
    private EnemyAI enemy;

    public GameObject sword;


    private void Awake()
    {
        enemy = GetComponentInParent<EnemyAI>();

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision with: " + other.name);

        PlayerCombat hit = other.GetComponent<PlayerCombat>();
        if (hit == null) hit = other.GetComponentInParent<PlayerCombat>();

        if (hit != null)
        {
            Debug.Log("PlayerCombat found on collider: " + other.name);
        }
        if (hit == null)
        {
            Debug.Log("No PlayerHitBox found on collider: " + other.name);
            return;
        }

        enemy.playerblockedtheattack = true;
        Debug.Log("ENEMY GOT HIT BY THE PLAYER");
        enemy.TakeDamage(hit.damage);

    }


}
