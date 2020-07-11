using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Tooltip("The prefab/object you want to pool")]
    [SerializeField] private GameObject m_ObjectToPool;
    [Tooltip("The amount you want to pool")]
    [SerializeField] private int m_AmountToPool;
    [Tooltip("All the pooled items")]
    [SerializeField] private List<GameObject> m_PooledObjects = new List<GameObject>();

    private GameObject m_PoolHolder;

    public static ObjectPooler s_SharedInstance;

    private void Awake()
    {
        s_SharedInstance = this;
        gameObject.name = m_ObjectToPool.name + " pooler";
        StartPooler();
    }


    private void StartPooler()
    {
        for(int i = 0; i < m_AmountToPool; i++)
        {
            GameObject tempObject = Instantiate(m_ObjectToPool, transform) as GameObject;
            tempObject.SetActive(false);
            tempObject.transform.position = new Vector3(0, -1000, 0);
            m_PooledObjects.Add(tempObject);
        }
    }

    // GameObject yourVariable = ObjectPooler.SharedInstance.GetPooledObject(); 
    // if yourVariable != null then you can use it
    // This is the way to access it from different scripts
    public GameObject GetPooledObject()
    {
        for (int i = 0; i <m_PooledObjects.Count; i++)
        {
            if (!m_PooledObjects[i].activeInHierarchy)
            {
                return m_PooledObjects[i];
            }
        }
        return null;
    }
}
