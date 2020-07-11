//©©©©©©©Samuel Gustafsson©©©©©©©2020©©©©©©©©

using System;
using UnityEngine;
using UnityEngine.Assertions;
using Object = System.Object;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCharacter : MonoBehaviour
{
    //Private
    private Vector3 m_DesiredDirection;
    private Vector3 m_DirectionSmoothing;
    private Vector3 m_SmoothDir;
    private float m_OldDrag;
    private float m_JumpForce;
    private float m_AngleDifference;
    private float m_Acceleration;
    private float m_LastFallingFrame;
    private float m_FallingFrame;
    private Vector3 m_DashTargetLocation;
    private Vector3 m_DashDirection;

    //Public
    [System.NonSerialized] public Vector3 m_DesiredMovement;
    [System.NonSerialized] public Vector3 m_MovementDirection;
    [System.NonSerialized] public bool m_Jumping;
    [System.NonSerialized] public bool m_HoldingJump;
    [System.NonSerialized] public bool m_CanJump = true;
    [System.NonSerialized] public bool m_Airborne;
    [System.NonSerialized] public bool m_Falling;
    [System.NonSerialized] public bool m_FirstJumpFrame;
    [System.NonSerialized] public bool m_OnLedge = false;
    [System.NonSerialized] public bool m_PressingDashing = false;
    [System.NonSerialized] public bool m_Dashing = false;
    [System.NonSerialized] public int m_DashCharges;
    [System.NonSerialized] public bool m_Mounted = false;
    [System.NonSerialized] public bool m_FloatingObjectCheck = false;

    //Settings
    [Header("Character")]
    [SerializeField] private float m_CharacterAirialAcceleration;
    [Range(0.01f, 0.5f)]
    [SerializeField] private float m_AcclerationPercentage;
    [SerializeField] private float m_CharacterMovementSpeed;
    [Range(0, 90)]
    [Tooltip("Determines how steep an angle the player is allowed to take, 0 being flat, 90 being straight up.")]
    [SerializeField] private float m_MaximumSlopeIncline;
    [Tooltip("Time it takes for character to smooth desired direction.")]
    [SerializeField] private float m_CharacterRotationSmoothing;
    [Tooltip("Highest possible jump.")]
    [SerializeField] private float m_CharacterMaximumJumpForce;
    [Tooltip("How fast you reduce the jump force when you release the jump button")]
    [SerializeField] private float m_CharacterJumpGravity;
    [Tooltip("Enabled: The player can strafe and stop movement in the air, Disabled: The player cannot control their movement until they land")]
    [SerializeField] private bool m_AirControl;
    [Tooltip("Center of the blue box bellow the player, shows where the player connects with the floor")]
    [SerializeField] private Vector3 m_FloorColliderOffset;
    [Tooltip("Size of the blue box bellow the player, shows where the player connects with the floor")]
    [SerializeField] private Vector3 m_FloorColliderSize;
    [Tooltip("Which layers can the floor collider interact with.")]
    [SerializeField] private LayerMask m_FloorChecks;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private SkinnedMeshRenderer m_MeshRenderer;
    [SerializeField] private SoundManager m_SoundManager;

    #region Dash
    //Dash Settings
    [Header("Dash Settings")]
    [Tooltip("Amount of Dash Charges the player has.")]
    public int m_MaxDashCharges;
    [Tooltip("Dash movement speed as a multiplier of force")] [Range(0.0f, 5.0f)]
    public float m_DashSpeed;
    [Tooltip("Dash range")]
    public float m_DashDistance;
    #endregion

    //Components
    private Rigidbody m_PlayerRigidBody;
    private Collider m_PlayerCollider;
    internal CameraMovement m_CameraMovement;
    [NonSerialized] public Dash_Trail m_DashTrail;

    //Used when controlling companion
    public Rigidbody PlayerRigidBody { get => m_PlayerRigidBody; set => m_PlayerRigidBody = value; }
    public float CharacterMaximumJumpForce { get => m_CharacterMaximumJumpForce; }

    public bool IsDashing { get => m_Dashing; }
    public Vector3 DashDirection { get => m_DashDirection; }

    //Used to change materials on player
    public SkinnedMeshRenderer MeshRenderer { get => m_MeshRenderer; set => m_MeshRenderer = value; }

    private const string k_AnimationSpeed = "Speed";
    private const string k_AnimationGrounded = "Grounded";
    private const string k_AnimationJumping = "Jumping";
    private const string k_AnimationFalling = "Falling";
    private const string k_AnimationCharging = "Charging";
    private const string k_AnimationDashing = "Dashing";
    private const string k_AnimationHanging = "Hanging";

    private void Awake()
    {
        m_PlayerRigidBody = GetComponent<Rigidbody>();
        m_PlayerCollider = GetComponent<Collider>();
        m_DashTrail = GetComponent<Dash_Trail>();
        m_DashCharges = m_MaxDashCharges;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GameManager.Instance.DontDestroyOnLoadList.Add(gameObject);
        Assert.IsNotNull(m_MeshRenderer);
        Assert.IsNotNull(m_Animator);
        Assert.IsNotNull(m_SoundManager);
    }

    private void FixedUpdate()
    {
        m_Animator.SetFloat(k_AnimationSpeed, Mathf.Abs(m_PlayerRigidBody.velocity.magnitude));
        m_Animator.SetBool(k_AnimationGrounded, !m_Airborne);
        m_Animator.SetBool(k_AnimationFalling, m_Falling);
        m_Animator.SetBool(k_AnimationJumping, m_Jumping);
        m_Animator.SetBool(k_AnimationCharging, m_PressingDashing);
        m_Animator.SetBool(k_AnimationDashing, m_Dashing);
        m_Animator.SetBool(k_AnimationHanging, m_OnLedge);

        //Acceleration in air, instant movement while on the ground.

        Vector2 cameraForward = m_CameraMovement.DirectionForward;
        Vector2 cameraRight = m_CameraMovement.DirectionRight;

        if (Mathf.Abs(m_DesiredMovement.magnitude) > 0f)
        {
            //Friction Change
            m_PlayerCollider.material.staticFriction = 0;
            m_PlayerCollider.material.dynamicFriction = 0;
            m_PlayerRigidBody.drag = m_OldDrag;

            //Direction Calculation
            Vector3 desiredForward = new Vector3(cameraForward.x, 0, cameraForward.y);
            Vector3 desiredRight = new Vector3(cameraRight.x, 0, cameraRight.y);
            m_MovementDirection = (m_DesiredMovement.z * desiredForward + m_DesiredMovement.x * desiredRight).normalized;

            m_DesiredDirection = m_MovementDirection;

            //Rotation Smoothing Calculation
            if (!m_Airborne)
            {
                m_AngleDifference = Vector2Extentions.V2Atan2(m_DesiredDirection.CreateVector2(), transform.forward.CreateVector2()) / 90;
            }

            if (m_AngleDifference > 1.9f)
            {
                m_DesiredDirection += (transform.forward + transform.right).normalized;
            }

            float rotationSpeedDamping = Mathf.Clamp01(1.2f - m_AngleDifference);

            //Movement Ground
            if (!m_Airborne)
            {
                Vector3 slope = new Vector3();
                if (Physics.Raycast(transform.position + m_FloorColliderOffset, -transform.up, out RaycastHit hit, m_FloorChecks))
                {
                    float angle = Vector3.Angle(hit.normal, Vector3.up);
                    if (angle <= 90 + m_MaximumSlopeIncline)
                    {
                        slope = Vector3.Cross(transform.right, hit.normal);
                    }
                }
                else
                {
                    slope = Vector3.Cross(transform.right, Vector3.up);
                }

                OnDashEnd();

                if (m_Acceleration > 1f)
                    m_Acceleration = 1f;
                else
                    m_Acceleration += m_AcclerationPercentage;

                m_PlayerRigidBody.velocity = slope * m_CharacterMovementSpeed * rotationSpeedDamping * m_Acceleration;
                CapPlayerSpeed();
                Debug.DrawLine(transform.position, transform.position + slope);
            }

            //Movement Airborne
            if (m_Airborne && m_AirControl)
            {
                m_PlayerRigidBody.AddForce(m_MovementDirection * m_CharacterAirialAcceleration, ForceMode.Acceleration);
                CapPlayerSpeed();
            }
        }
        else
        {
            //No input
            if (!m_Airborne)
            {
                m_Acceleration = 0;
                m_PlayerRigidBody.drag = 10;
                m_PlayerCollider.material.staticFriction = 1;
                m_PlayerCollider.material.dynamicFriction = 1;
            }
            else
            {
                m_PlayerRigidBody.drag = m_OldDrag;
            }
        }

        //Stops rotation if not moving
        if (m_PlayerRigidBody.velocity.CreateVector2().magnitude < 2f && !(Mathf.Abs(m_DesiredMovement.magnitude) > 0f))
            m_DesiredDirection = Vector3.Lerp(m_DesiredDirection, transform.forward, 0.10f);

        if (m_PlayerRigidBody.velocity.magnitude < 1f && !(Mathf.Abs(m_DesiredMovement.magnitude) > 0f))
            m_DesiredDirection = transform.forward;

        //Character Rotation
        if (!m_Airborne)
        {
            m_SmoothDir = Vector3.SmoothDamp(m_SmoothDir, m_DesiredDirection, ref m_DirectionSmoothing, m_CharacterRotationSmoothing);
            transform.LookAt(transform.position + m_SmoothDir, transform.up);
        }
        else if (m_PressingDashing && !m_OnLedge)
        {
            Vector3 m_targetDir = m_CameraMovement.transform.forward;
            m_targetDir.y = transform.forward.y;
            transform.LookAt(transform.position + m_targetDir);
        }

        Jump();

        if (Time.timeScale != 1 && !m_Airborne)
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
        }

        if (m_Dashing)
        {
            Vector3 offset = m_DashTargetLocation - transform.position;
            float sqrLen = offset.sqrMagnitude;

            if (sqrLen < 150)
            {
                OnDashEnd();
            }
            else
            {
                m_PlayerRigidBody.AddForce(m_DashDirection * m_DashSpeed, ForceMode.Impulse);   
            }
        }
    }

    private void CapPlayerSpeed()
    {
        if (!m_Dashing)
        {
            if (m_PlayerRigidBody.velocity.CreateVector2().magnitude > m_CharacterMovementSpeed)
            {
                Vector2 newVelocity = m_PlayerRigidBody.velocity.CreateVector2().normalized * m_CharacterMovementSpeed;
                m_PlayerRigidBody.velocity = m_PlayerRigidBody.velocity.SetX(newVelocity.x);
                m_PlayerRigidBody.velocity = m_PlayerRigidBody.velocity.SetZ(newVelocity.y);
            }
        }
    }

    private void Jump()
    {
        bool onGround = Physics.CheckBox(m_FloorColliderOffset + transform.position, m_FloorColliderSize / 2, transform.rotation, m_FloorChecks);

        if (!onGround)
        {
            m_Airborne = true;
            m_CanJump = m_OnLedge;
        }
        else if (m_OnLedge)
        {
            m_PlayerRigidBody.constraints = RigidbodyConstraints.None;
            m_PlayerRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
            m_OnLedge = false;
        }

        if (m_Jumping && m_CanJump && !m_HoldingJump || m_OnLedge && m_Jumping)
        {
            if (m_OnLedge)
            {
                m_PlayerRigidBody.constraints = RigidbodyConstraints.None;
                m_PlayerRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
                m_OnLedge = false;
            }

            m_PlayerRigidBody.drag = 0;
            m_JumpForce = m_CharacterMaximumJumpForce;
            m_PlayerRigidBody.AddForce(transform.up * m_JumpForce, ForceMode.Impulse);
            m_FirstJumpFrame = true;
            m_CanJump = false;
            m_Airborne = true;
        }
        else if (!m_Jumping && !m_CanJump && !m_FloatingObjectCheck)
        {
            if (m_PlayerRigidBody.velocity.y > 0.01f)
            {
                m_PlayerRigidBody.AddForce(-transform.up * m_CharacterJumpGravity, ForceMode.Force);
            }
        }

        if (m_Airborne)
        {
            m_LastFallingFrame = m_FallingFrame;
            m_FallingFrame = m_PlayerRigidBody.transform.position.y;
            m_Falling = (m_FallingFrame - m_LastFallingFrame) < 0.01f && !m_FirstJumpFrame;

            m_FirstJumpFrame = false;

            if (onGround && m_Falling)
            {
                m_Airborne = false;
                if (m_DashCharges < m_MaxDashCharges)
                {
                    m_DashCharges += 1;   
                }
                m_CanJump = true;
                if (!m_AirControl)
                    m_PlayerRigidBody.drag = m_OldDrag;
            }
        }
    }

    public void Dash()
    {
        if (m_DashCharges > 0)
        {
            m_PlayerRigidBody.useGravity = false;
            m_DashCharges -= 1;
            m_Dashing = true;
            if (m_OnLedge)
            {
                Vector3 m_targetDir = m_CameraMovement.transform.forward;
                m_targetDir.y = transform.forward.y;
                transform.LookAt(transform.position + m_targetDir);
                m_PlayerRigidBody.constraints = RigidbodyConstraints.None;
                m_PlayerRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
            }
            m_DashTargetLocation = transform.position + transform.forward * m_DashDistance;
            m_DashTargetLocation.y = transform.position.y;
            m_DashDirection = transform.forward * m_DashDistance;
            m_DashDirection.y = transform.forward.y;
        }
    }

    public void OnDashEnd()
    {
        m_Dashing = false;
        m_PlayerRigidBody.useGravity = true;
        m_PlayerRigidBody.velocity = Vector3.zero;
        m_OnLedge = false;
    }

    public RaycastHit FloorCheck()
    {
        Physics.Raycast(transform.position + m_FloorColliderOffset, -transform.up, out RaycastHit floorHit, m_FloorChecks);
        return floorHit;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(m_FloorColliderOffset + transform.position, m_FloorColliderSize);
        Gizmos.color = Color.white;
    }
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.layer != 11)
        {
            if (m_Dashing)
            {
                OnDashEnd();
            }
        }
    }
}