using UnityEngine;

public class CompanionInput : MonoBehaviour
{
    [SerializeField] private CompanionCharacter m_CompanionCharacter;

    private const string k_MouseX = "Mouse X";
    private const string k_MouseY = "Mouse Y";
    private const string k_MovementZ = "Vertical";
    private const string k_MovementX = "Horizontal";
    private const string k_MouseWheel = "Mouse ScrollWheel";
    private const string k_Jump = "Jump";
    private const string k_Interact = "Interact";
    private const string k_Dash = "Dash";
    private const string k_ToggleWalk = "ToggleWalk";
    private const string k_CancelJump = "CancelJump";
    private const string k_Restart = "Restart";

    private bool m_AutoForward;
    private float m_CancelJumpDelay = 0.1f;

    void Start()
    {
        if (m_CompanionCharacter == null)
        {
            m_CompanionCharacter = GetComponent<CompanionCharacter>();
        }
    }

    private void Update()
    {
        m_CompanionCharacter.CameraMovement.UpdateCameraCoordinates(new Vector3(Input.GetAxis(k_MouseX), Input.GetAxis(k_MouseY), Input.GetAxis(k_MouseWheel)));
        m_CompanionCharacter.DesiredMovement = m_CompanionCharacter.DesiredMovement.SetZ(Input.GetAxisRaw(k_MovementZ));
        m_CompanionCharacter.DesiredMovement = m_CompanionCharacter.DesiredMovement.SetX(Input.GetAxisRaw(k_MovementX));

        if(m_CompanionCharacter.DesiredMovement != Vector3.zero)
        {
            m_AutoForward = false;
        }

        if (Input.GetButtonDown(k_Jump))
        {
            m_CompanionCharacter.Jumping = true;
            Invoke(k_CancelJump, m_CancelJumpDelay);
        }

        if (Input.GetButtonDown(k_ToggleWalk))
        {
            m_CompanionCharacter.Walking = !m_CompanionCharacter.Walking;
        }

        if (Input.GetButtonDown(k_Dash))
        {
            m_AutoForward = !m_AutoForward;
        }

        if(m_AutoForward)
        {
            m_CompanionCharacter.DesiredMovement = Vector3.forward;
        }

        if (Input.GetButtonDown(k_Interact))
        {
            m_CompanionCharacter.Dismounting = true;
        }

        if(Input.GetButtonDown(k_Restart))
        {
            GameManager.Instance.RespawnPlayer();
        }
    }

    private void CancelJump()
    {
        m_CompanionCharacter.Jumping = false;
    }
}
