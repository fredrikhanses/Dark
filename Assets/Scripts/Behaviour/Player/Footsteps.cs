using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody))]
public class Footsteps : MonoBehaviour
{
    [SerializeField] private GameObject m_SpawnPoint;
    [SerializeField] private PlayerCharacter m_Player;
    [SerializeField] private float m_CurrentTime = 0f;
    [SerializeField] private float m_MaxTime = 0.05f;
    private Rigidbody m_Rigidbody;
    private Vector3 m_Speed = new Vector3(0, 0, 0);
    private bool m_StartTimer;
    private bool m_CanSpawn = true;

    public ObjectPooler objectPooler;



    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    private void SpawnFootsteps()
    {
        GameObject footstep = objectPooler.GetPooledObject();
        if (footstep != null && !m_Player.m_Airborne && m_Rigidbody.velocity.magnitude > 2f && m_CanSpawn)
        {
            if (!m_StartTimer)
            {
                print("Spawning");
                SetParticlePlace(footstep);
                footstep.SetActive(true);
                m_StartTimer = true;
        }
    }
        else if(footstep != null && !m_Player.m_Airborne && m_Rigidbody.velocity.magnitude > 2f && !m_CanSpawn)
        {
            print("Cant spawn particles");
        }
    }
    private void Update()
    {

        SpawnFootsteps();

        if (m_StartTimer)
        {
            if (m_CurrentTime < m_MaxTime)
            {
                m_CurrentTime += Time.time * Time.deltaTime;
                print(m_CurrentTime);
            }
            else
            {
                m_StartTimer = false;
                m_CurrentTime = 0f;
                SpawnFootsteps();
            }
        }
        else
        {
            SpawnFootsteps();
        }
    }

    private void FindDoneParticles()
    {

    }

    private void SetParticlePlace(GameObject particle)
    {
        particle.transform.position = m_SpawnPoint.transform.position;
        particle.GetComponent<VisualEffect>().transform.position = m_SpawnPoint.transform.position;
    }
}
