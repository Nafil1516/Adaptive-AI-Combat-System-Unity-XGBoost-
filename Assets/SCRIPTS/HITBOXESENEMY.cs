using UnityEngine;

public class HITBOXESENEMY : MonoBehaviour
{
    public GameObject hitboxleft; 
    public GameObject hitboxright; 

    public void EnableHitboxLeft()
    {
        hitboxleft.SetActive(true);
    }

    public void DisableHitboxLeft()
    {
        hitboxleft.SetActive(false);
    }

    public void EnableHitboxRight()
    {
        hitboxright.SetActive(true);
    }

    public void DisableHitboxRight()
    {
        hitboxright.SetActive(false);
    }
}
