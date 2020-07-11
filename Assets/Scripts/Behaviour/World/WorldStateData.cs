using UnityEngine;

[CreateAssetMenu(fileName = "WorldStateData", menuName = "WorldStateData", order = 0)]
public class WorldStateData : ScriptableObject
{
    public int ShrinesCompleted = 0;
    public int CurrentLevel = 0;
    public int NextLevel = 1;
    public int LastLevel = -1;

    [SerializeField] private int m_StartMenu = 0;
    [SerializeField] private int m_OverWorld = 1;
    [SerializeField] private int m_SpiritWorldFirst = 2;
    [SerializeField] private int m_Transition = 3;
    [SerializeField] private int m_SpiritWorldSecond = 4;
    [SerializeField] private int m_SpiritWorldThird = 5;

    public int StartMenu { get => m_StartMenu; }
    public int OverWorld { get => m_OverWorld; }
    public int SpiritWorldFirst { get => m_SpiritWorldFirst; }
    public int Transition { get => m_Transition; }
    public int SpiritWorldSecond { get => m_SpiritWorldSecond; }
    public int SpiritWorldThird { get => m_SpiritWorldThird; }

    private void OnEnable()
    {
        ResetState();
    }

    public void ResetState()
    {
        ShrinesCompleted = 0;
        CurrentLevel = 0;
        NextLevel = 1;
    }
}
