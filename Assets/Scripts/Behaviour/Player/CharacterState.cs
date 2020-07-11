using UnityEngine;

public class CharacterState : MonoBehaviour
{
    private const string k_Rise = "Rise";

    public void SetRise(int rise)
    {
        if (rise <= 0)
        {
            GameManager.Instance.PlayerAnimator.SetBool(k_Rise, false);
            GameManager.Instance.PlayerInput.enabled = true;
        }
        else
        {
            GameManager.Instance.PlayerAnimator.SetBool(k_Rise, true);
        }
    }
}
