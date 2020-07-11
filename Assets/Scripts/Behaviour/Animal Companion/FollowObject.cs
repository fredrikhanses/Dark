using UnityEngine;

public class FollowObject : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject m_Player;
    [SerializeField] private CompanionFollow m_AnimalCompanion;
    [SerializeField, Range(0f, 10f)] private float m_StoppingDistance = 0;

    private float m_InitialStoppingDistance;

    private void Start()
    {
        if(m_AnimalCompanion == null)
        {
            m_AnimalCompanion = FindObjectOfType<CompanionFollow>();
        }
        if(m_Player == null)
        {
            m_Player = FindObjectOfType<PlayerCharacter>().gameObject;
        }
        if (m_AnimalCompanion != null)
        {
            m_InitialStoppingDistance = m_AnimalCompanion.Agent.stoppingDistance;
        }
    }

    public void ImDoingMyThing()
    {
        if (m_AnimalCompanion != null)
        {
            if (m_AnimalCompanion.Target == this.gameObject)
            {
                m_AnimalCompanion.Target = m_Player;
                m_AnimalCompanion.Agent.stoppingDistance = m_InitialStoppingDistance;
            }
            else
            {
                m_AnimalCompanion.Target = this.gameObject;
                m_AnimalCompanion.Agent.stoppingDistance = m_StoppingDistance;
            }
        }
    }
}
