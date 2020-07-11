using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GameManager.Instance.DontDestroyOnLoadList.Add(gameObject);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public int GetSceneIndex()
    {
        return gameObject.scene.buildIndex;
    }

    public Quaternion GetRotation()
    {
        return transform.rotation;
    }
}
