using UnityEngine;
using UnityEngine.UI;

public class UIDash : MonoBehaviour
{
    private Text text;
    private PlayerCharacter m_Player;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        m_Player = GameManager.Instance.PlayerCharacter;
    }

    private void Update()
    {
        if(m_Player != null)
        {
            if (m_Player.m_DashCharges > 0)
            {
                text.color = Color.green;
                text.text = "Dash Ready!";
            }
            else
            {
                text.color = Color.red;
                text.text = "Dash on Cooldown!";
            }
        }
    }
}
