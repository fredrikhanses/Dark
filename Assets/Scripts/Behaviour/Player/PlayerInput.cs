//©©©©©©©Samuel Gustafsson©©©©©©©2020©©©©©©©©

using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(PlayerCharacter))]
[RequireComponent(typeof(PlayerInteract))]
public class PlayerInput : MonoBehaviour
{
    //Input Names
    private const string k_MouseX = "Mouse X";
    private const string k_MouseY = "Mouse Y";
    private const string k_MovementZ = "Vertical";
    private const string k_MovementX = "Horizontal";
    private const string k_MouseWheel = "Mouse ScrollWheel";
    private const string k_Jump = "Jump";
    private const string k_Interact = "Interact";
    private const string k_Dash = "Dash";
    private const string k_Pause = "Pause";

    [Tooltip("Minimum timescale the Dash can lower to. Do not set to 0.")]
    [SerializeField] private float m_maximumSlow = 0.2f;

    //Components
    private PlayerCharacter m_Player;
    private PlayerInteract m_Interact;

    private GameObject m_PauseMenu;
    private GameObject m_Settings;

    private bool m_IsPaused;
    public bool IsPaused
    {
        get => m_IsPaused;
        set => m_IsPaused = value;
    }

    private void Awake()
    {
        m_Player = GetComponent<PlayerCharacter>();
        m_Interact = GetComponent<PlayerInteract>();
    }

    private void Start()
    {
        m_PauseMenu = UIController.Instance.m_PauseMenu;
        m_Settings = UIController.Instance.m_Settings;
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (UIController.Instance.UIInput)
        {
            UIInput();
            m_Player.m_CameraMovement.UpdateCameraCoordinates(Vector3.zero);
            return;
        }
        Assert.IsNotNull(m_Player.m_CameraMovement, "Camera attachment missing.");
        m_Player.m_CameraMovement.UpdateCameraCoordinates(new Vector3(Input.GetAxis(k_MouseX), Input.GetAxis(k_MouseY), Input.GetAxis(k_MouseWheel)));


        if (!m_Player.m_Jumping && m_Player.m_CanJump)
        {
            m_Player.m_Jumping = Input.GetButtonDown(k_Jump);
        }

        if (Input.GetButton(k_Jump))
        {
            if (m_Player.m_Airborne && !m_Player.m_OnLedge)
                m_Player.m_HoldingJump = true;
        }
        else
        {
            m_Player.m_HoldingJump = false;
        }

        if (m_Player.m_Jumping && Input.GetButtonUp(k_Jump))
        {
            m_Player.m_Jumping = false;
        }


        if (!m_Player.m_Dashing)
        {
            m_Player.m_DesiredMovement = m_Player.m_DesiredMovement.SetZ(Input.GetAxisRaw(k_MovementZ));
            m_Player.m_DesiredMovement = m_Player.m_DesiredMovement.SetX(Input.GetAxisRaw(k_MovementX));

            m_Player.m_PressingDashing = Input.GetButton(k_Dash);

            if (!m_Interact.m_IsInteracting)
            {
                m_Interact.m_IsInteracting = Input.GetButtonDown(k_Interact);
            }

            //Resets Interaction
            if (Input.GetButtonUp(k_Interact))
            {
                m_Interact.ResetInteract();
            }



            if (!m_Player.m_Dashing && !IsPaused)
            {
                if (m_Player.m_DashCharges > 0 && m_Player.m_Airborne)
                {
                    if (Input.GetButtonDown("Dash"))
                    {
                        //m_Player.m_DashTrail.m_VisualEffect.visualEffectAsset = m_Player.m_DashTrail.m_dashChargeup;
                        //m_Player.m_DashTrail.m_VisualEffect.Play();
                    }
                    if (Input.GetButtonUp("Dash"))
                    {
                        Time.timeScale = 1;
                        Time.fixedDeltaTime = 0.02f;
                        if (m_Player.m_Airborne)
                        {
                            //m_Player.m_DashTrail.m_VisualEffect.visualEffectAsset = m_Player.m_DashTrail.m_Dashing;
                            //m_Player.m_DashTrail.m_VisualEffect.Play();
                            m_Player.Dash();
                        }
                    }
                    else if (Input.GetButton("Dash"))
                    {
                        if (Time.timeScale > m_maximumSlow)
                        {
                            Time.timeScale *= 0.9f;
                            Time.fixedDeltaTime = 0.02f * Time.timeScale;
                        }
                    }
                }
            }
        }
    }

    private void UIInput()
    {
        if (Input.GetButtonDown(k_Interact))
        {
            UIController.Instance.InputConformation = true;
        }
    }
}