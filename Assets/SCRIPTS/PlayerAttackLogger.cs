using System.IO;
using UnityEngine;

public class PlayerAttackLogger : MonoBehaviour
{
    string filePath;
    string lastAttack = "None";
    bool firstAttackLogged = false;
    public Transform player;
    
    float lastAttackTime;
    public EnemyAI enemyAI;

    void Start()
    {
        filePath = Application.dataPath + "/attackData.csv";

        // Create file with header if not exists
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "lastAttack,nextAttack\n");
        }
        lastAttackTime = Time.time;
    }

    public void LogAttack(string attackType, string moveState)
    {
        float distance = 0f;
        if (player != null && enemyAI != null)
            distance = Vector3.Distance(player.position, enemyAI.transform.position);

        float gap = Time.time - lastAttackTime;

        string row = $"{lastAttack},{attackType},{distance:F2},{gap:F2},{moveState}\n";
        File.AppendAllText(filePath, row);

        lastAttack = attackType;
        lastAttackTime = Time.time;
        firstAttackLogged = true;

        if (enemyAI != null)
        enemyAI.OnPlayerAttack(attackType);
    }
}
