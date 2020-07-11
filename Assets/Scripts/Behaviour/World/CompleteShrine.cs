using UnityEngine;

public class CompleteShrine : MonoBehaviour, IEvent
{
    public void Event()
    {
        GameManager.Instance.ShrineCompleted();
    }
}
