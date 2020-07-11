//©©©©©©©Samuel Gustafsson©©©©©©©2020©©©©©©©©

using System.Collections;
using UnityEngine;

public class EventMove : MonoBehaviour, IEvent
{
    public Vector3 m_TargetPosition;
    public PlayerCharacter m_TargetPlayer;
    [Tooltip("When Smooth is set to True this dictates how many seconds is takes to reach that position, When set to False it dictates the speed of the object.")] 
    public float m_Value;
    public float m_Delay;

    [SerializeField] bool m_Smooth;

    private Vector3 m_Velocity;
    private Vector3 m_StartPosition;
    private bool m_Moved = false;

    private void Start()
    {
        m_StartPosition = transform.position;
    }

    public void Event()
    {
        StartCoroutine(MoveTo());
    }

    IEnumerator MoveTo()
    {
        Vector3 target = m_TargetPosition;
        if (m_Moved)
            target = m_StartPosition;

        yield return new WaitForSeconds(m_Delay);

        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            Vector3 oldPos = transform.position;
            Vector3 newPos;

            if (m_Smooth)
                newPos = transform.position = Vector3.SmoothDamp(transform.position, target, ref m_Velocity, m_Value);
            else
                newPos = transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * m_Value);

            if (m_TargetPlayer != null)
            {
                if (!m_TargetPlayer.m_Airborne || m_TargetPlayer.m_OnLedge)
                {
                    m_TargetPlayer.transform.position += newPos - oldPos;
                }
            }

            yield return new WaitForFixedUpdate();
        }

        m_Moved = !m_Moved;
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerCharacter player = collision.gameObject.GetComponent<PlayerCharacter>();

        if (player != null)
        {
            m_TargetPlayer = player;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        PlayerCharacter player = collision.gameObject.GetComponent<PlayerCharacter>();

        if (player == m_TargetPlayer)
        {
            m_TargetPlayer = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(m_TargetPosition, 0.25f);
    }
}
