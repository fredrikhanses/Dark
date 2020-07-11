using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class HeartLight : MonoBehaviour
{
    private List<HDAdditionalLightData> m_Lights = new List<HDAdditionalLightData>();
    public float m_Strength;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Light>() != null)
            {
                var temp = child.GetComponent<HDAdditionalLightData>();
                m_Lights.Add(temp);
            }
        }
    }

    private void FixedUpdate()
    {
        foreach (var child in m_Lights)
        {
            child.intensity = Mathf.PingPong(Time.time * m_Strength, m_Strength * 10);
            child.intensity *= 100;
        }
    }
}