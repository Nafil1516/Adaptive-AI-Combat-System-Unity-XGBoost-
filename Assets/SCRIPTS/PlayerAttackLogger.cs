// using System.IO;
// using UnityEngine;

// public class PlayerAttackLogger : MonoBehaviour
// {
//     string filePath;
//     string lastAttack = "None";
//     bool firstAttackLogged = false;
//     public Transform player;
    
//     float lastAttackTime;
//     public EnemyAI enemyAI;

//     void Start()
//     {
//         filePath = Application.dataPath + "/attackData.csv";

//         // Create file with header if not exists
//         if (!File.Exists(filePath))
//         {
//             File.WriteAllText(filePath, "lastAttack,nextAttack\n");
//         }
//         lastAttackTime = Time.time;
//     }

//     public void LogAction(string actionType, string moveState)
//     {
//         float distance = 0f;
//         if (player != null && enemyAI != null)
//             distance = Vector3.Distance(player.position, enemyAI.transform.position);

//         float gap = Time.time - lastAttackTime;

//         string row = $"{lastAttack},{actionType},{distance:F2},{gap:F2},{moveState}\n";
//         File.AppendAllText(filePath, row);

//         lastAttack = actionType;
//         lastAttackTime = Time.time;

//         if (enemyAI != null)
//             enemyAI.OnPlayerAttack(actionType); // optional for attack-specific logic
//     }

// }

using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class PlayerAttackLogger : MonoBehaviour
{
    string filePath;
    string lastAttack = "None";
    float lastAttackTime;

    public Transform player;
    public EnemyAI enemyAI;

    // Mapping for actions and player states
    Dictionary<string, int> actionMapping = new Dictionary<string, int>()
    {
        {"None", 0},
        {"roll", 1},
        {"attack", 2},
        {"idle", 0}, // Example mapping, adjust if needed
        {"block", 3} // add more actions if you have them
    };

    Dictionary<string, int> stateMapping = new Dictionary<string, int>()
    {
        {"idle", 0},
        {"moving", 1},
        {"attacking", 2} // add more states if you have them
    };

    void Start()
    {
        filePath = Application.dataPath + "/attackData.csv";

        // Create file with header if not exists
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "prev_action,current_action,distance,time,player_state\n");
        }

        lastAttackTime = Time.time;
    }

    public void LogAction(string actionType, string moveState)
    {
        float distance = 0f;
        if (player != null && enemyAI != null)
            distance = Vector3.Distance(player.position, enemyAI.transform.position);

        float gap = Time.time - lastAttackTime;

        // Convert actions and states to integers
        int prevActionNum = actionMapping.ContainsKey(lastAttack) ? actionMapping[lastAttack] : -1;
        int currentActionNum = actionMapping.ContainsKey(actionType) ? actionMapping[actionType] : -1;
        int stateNum = stateMapping.ContainsKey(moveState) ? stateMapping[moveState] : -1;

        // Write numeric row
        string row = $"{prevActionNum},{currentActionNum},{distance:F2},{gap:F2},{stateNum}\n";
        File.AppendAllText(filePath, row);

        lastAttack = actionType;
        lastAttackTime = Time.time;

        if (enemyAI != null)
            enemyAI.OnPlayerAttack(actionType); // optional
    }
}

