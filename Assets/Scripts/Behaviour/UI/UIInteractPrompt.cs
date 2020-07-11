using System;
using UnityEngine;
using TMPro;

public class UIInteractPrompt : MonoBehaviour
{
    [NonSerialized] public bool m_Highlighting;
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (m_Highlighting && !UIController.Instance.IsUIInput())
        {
            text.color = Color.white;
        }
        else
        {
            text.color = Color.clear;
        }
    }
}
