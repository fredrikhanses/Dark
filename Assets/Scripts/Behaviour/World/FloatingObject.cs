using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using Random = System.Random;

public class FloatingObject : MonoBehaviour
{
    [SerializeField] public float m_Amplitude;
    [SerializeField] public float m_Speed;
    private PlayerCharacter m_TargetPlayer;
    private float m_Adjustment;
    private Vector3 m_Position;
    private Vector3 m_StartPosition;
    private Vector3 m_TargetPosition;

    private void Start()
    {
        m_Position = transform.position;
        m_StartPosition = m_Position;
        m_TargetPosition = m_Position;
        m_TargetPosition.y += m_Amplitude;
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, m_TargetPosition) > 0.1f)
        {
            Vector3 oldPos = transform.position;
            Vector3 newPos;
            newPos = transform.position = Vector3.MoveTowards(transform.position, m_TargetPosition, Time.deltaTime * m_Speed);

            if (m_TargetPlayer != null)
            {
                m_TargetPlayer.transform.position += newPos - oldPos;
            }
        }
        else
        {
            if (m_TargetPosition.y > m_StartPosition.y)
            {
                m_TargetPosition.y -= m_Amplitude * 2;
            }
            else
            {
                m_TargetPosition.y += m_Amplitude * 2;
            }
        }
   }

    private void OnCollisionEnter(Collision other)
    {
        PlayerCharacter player = other.gameObject.GetComponent<PlayerCharacter>();

        if (player != null)
        {
            m_TargetPlayer = player;
            m_TargetPlayer.m_FloatingObjectCheck = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        PlayerCharacter player = collision.gameObject.GetComponent<PlayerCharacter>();

        if (player == m_TargetPlayer)
        {
            m_TargetPlayer.m_FloatingObjectCheck = false;
            m_TargetPlayer = null;
        }
    }

}
