using UnityEngine;

public class Respawn : MonoBehaviour, IEvent
{
    public void Event()
    {
        if (GameManager.Instance.WorldStateData.CurrentLevel != GameManager.Instance.WorldStateData.Transition)
        {
            GameManager.Instance.RespawnPlayer();
        }
    }
}
