using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Breakable : MonoBehaviour
{
    const int k_PlayerLayer = 9;

    public abstract void OnBreak();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == k_PlayerLayer)
        {
            PlayerCharacter player = collision.gameObject.GetComponent<PlayerCharacter>();
            if (player.IsDashing)
            {
                player.OnDashEnd();
                player.m_DesiredMovement = Vector3.zero;
                OnBreak();
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        PlayerCharacter player = collision.gameObject.GetComponent<PlayerCharacter>();
        if (player.IsDashing)
        {
            player.OnDashEnd();
            player.m_DesiredMovement = Vector3.zero;
        }
    }
}