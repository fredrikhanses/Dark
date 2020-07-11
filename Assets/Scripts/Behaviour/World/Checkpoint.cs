using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private PlayerCharacter m_Player;

    private bool m_Triggered = false;
    private const string k_LevelLoaded = "LevelLoaded";

    private void Start()
    {
        m_Player = GameManager.Instance.PlayerCharacter;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject == m_Player.gameObject && m_Triggered == false)
        {
            m_Triggered = true;
            GameManager.Instance.SoundManager.PlaySound(k_LevelLoaded);
            GameManager.Instance.CurrentPlayerSpawnPoint.gameObject.transform.position = transform.position + transform.forward;
        }
    }
}
