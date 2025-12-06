using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.Collections;

public class EnemyAIController : MonoBehaviour
{

    string apiUrl = "https://karina-unpropertied-kati.ngrok-free.dev/predict"; // your API
    public float sendInterval = 0.2f; // every 0.2 seconds

    private float timer = 0f;

    public PlayerAttackLogger logger;

    public Animator enemyAnimator;

    private EnemyAI enemyAI;


    public Transform enemy;

    void Start()
    {
        InvokeRepeating("SendStateToAPI", 0f, 2f);
        enemyAI = enemy.GetComponent<EnemyAI>();
    }

    void SendStateToAPI()
    {
        float[] currentState = logger.GetCurrentPlayerState();
        StartCoroutine(SendState(currentState));
    }


    /*
        void Update()
        {
            float[] currentState = logger.GetCurrentPlayerState();
            StartCoroutine(SendState(currentState));
        }
        */





    IEnumerator SendState(float[] gameState)
    {
        string json = JsonUtility.ToJson(new GameStateWrapper(gameState));
        byte[] body = Encoding.UTF8.GetBytes(json);
        string boolblock;

        UnityWebRequest req = new UnityWebRequest(apiUrl, "POST");
        req.uploadHandler = new UploadHandlerRaw(body);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            //Debug.Log("API Response: " + req.downloadHandler.text);
            boolblock = int.Parse(req.downloadHandler.text) == 1 ? "1" : "0";
            //Debug.Log("Parsed Response: " + boolblock);
            if(boolblock == "1")
            {
                enemyAI.disableattack();
                Debug.Log("Enemy blocks!");
                enemyAnimator.SetTrigger("EnemyBlock");
            }
            else
            {
                Debug.Log("Enemy does not block.");
            }
        }
        else
        {
            Debug.Log("API Error: " + req.error);
        }
    }

    [System.Serializable]
    public class GameStateWrapper
    {
        public float[] state;
        public GameStateWrapper(float[] s) { state = s; }
    }
}
