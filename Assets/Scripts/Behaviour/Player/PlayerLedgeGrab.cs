using UnityEngine;

[RequireComponent(typeof(PlayerCharacter))]
public class PlayerLedgeGrab : MonoBehaviour
{
    PlayerCharacter m_Player;
    [Tooltip("Height offset for the ledge grab represented by the purple line.")]
    [SerializeField] float m_LedgeGrabHeight;
    [Tooltip("How far the player can reach for a ledge grab represented by the purple line.")]
    [SerializeField] float m_LedgeGrabLength;
    [Tooltip("How much room between the check for weather or not you are on a ledge or not represented by the blue line.")]
    [SerializeField] float m_LedgeGrabMagin;
    [SerializeField] LayerMask m_LedgeMask;


    // Start is called before the first frame update
    void Awake()
    {
        m_Player = GetComponent<PlayerCharacter>();
    }

    void FixedUpdate()
    {
        if (m_Player.m_Falling && m_Player.m_Airborne && !m_Player.m_OnLedge)
        {
            LedgeCheck();
        }
    }

    private void LedgeCheck()
    {
        Vector3 start = transform.position + new Vector3(0, m_LedgeGrabHeight);
        Vector3 margin = new Vector3(0, m_LedgeGrabMagin) + start;

        Debug.DrawLine(start, start + m_Player.m_MovementDirection * m_LedgeGrabLength, Color.red, Time.fixedDeltaTime);

        if (Physics.Raycast(start, m_Player.m_MovementDirection, out RaycastHit hit, m_LedgeGrabLength, m_LedgeMask))
        {
            if (!Physics.Raycast(margin, m_Player.m_MovementDirection, m_LedgeGrabLength, m_LedgeMask))
            {
                transform.forward = -new Vector3(hit.normal.x, 0, hit.normal.z);
                m_Player.m_OnLedge = true;
                m_Player.PlayerRigidBody.constraints = RigidbodyConstraints.FreezePosition;
            }
        }

        m_Player.PlayerRigidBody.freezeRotation = true;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 start = transform.position + new Vector3(0, m_LedgeGrabHeight);
        Vector3 end = start + transform.forward * m_LedgeGrabLength;
        Vector3 margin = new Vector3(0, m_LedgeGrabMagin);
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(start, end);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(start + margin, end + margin);
    }
}
