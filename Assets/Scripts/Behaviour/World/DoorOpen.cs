using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class DoorOpen : MonoBehaviour, IEvent
{
    [SerializeField] private List<GameObject> m_RequiredActivators = new List<GameObject>();
    private int m_ActivatesLeft;


    private void Awake()
    {
        m_ActivatesLeft = m_RequiredActivators.Count;
        print(m_ActivatesLeft);
    }

    public void DeductActivater(bool active)
    {
        if (active) 
        { 
            m_ActivatesLeft -= 1;
            if(m_ActivatesLeft <= 0 && gameObject.activeSelf)
            {
                //Open door
                Debug.Log("Door Opening");
                gameObject.SetActive(false);
            }
        }
        else if (!active)
        {
            m_ActivatesLeft += 1;
            if (m_ActivatesLeft > 0 && !gameObject.activeSelf)
            {
                //Close Door
                Debug.Log("Door Closing");
                gameObject.SetActive(true);
            }
        }
    }

    public void Event()
    {
        OpenDoor();
    }

    private void OpenDoor()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        OpenEffect();
    }

    private void OpenEffect()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}
