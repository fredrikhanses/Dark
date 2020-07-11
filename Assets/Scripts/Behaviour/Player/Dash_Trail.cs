using UnityEngine;
using UnityEngine.VFX;

public class Dash_Trail : MonoBehaviour
{
    private PlayerCharacter m_Player;
    [System.NonSerialized] public VisualEffect m_VisualEffect;
    public VisualEffectAsset m_dashChargeup;
    public VisualEffectAsset m_Dashing;
    private void Awake()
    {
        m_Player = GetComponent<PlayerCharacter>();
        m_VisualEffect = GetComponent<VisualEffect>();
        m_VisualEffect.Stop();
    }
}
