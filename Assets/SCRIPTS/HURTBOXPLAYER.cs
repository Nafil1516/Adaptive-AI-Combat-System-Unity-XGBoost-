using UnityEngine;

public class PlayerHurtBox : MonoBehaviour
{
    private Movement player;

    private Animator enemyAnimator;

    public GameObject enemy;

    private EnemyAI enemycontroll;

    void Awake()
    {
        player = GetComponentInParent<Movement>();
        enemyAnimator = enemy.GetComponent<Animator>();
        enemycontroll = enemy.GetComponent<EnemyAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENEMY HIT THE PLAYER");
        EnemyHitBox hit = other.GetComponent<EnemyHitBox>();
        if (hit == null) return;

        if (player.isBlocking == false)
        {
            player.TakeDamage(hit.damage);
        }
        else if (player.isBlocking == true)
        {
            Debug.Log("PLAYER BLOCKED THE HIT");
            enemycontroll.playerblockedtheattack = true;
            enemycontroll.TakeDamage(10, true);
            //enemyAnimator.SetTrigger("Blocked");
        }
    }
}
