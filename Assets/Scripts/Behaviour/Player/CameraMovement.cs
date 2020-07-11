//©©©©©©©Samuel Gustafsson©©©©©©©2020©©©©©©©©

using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.PlayerLoop;

public class CameraMovement : MonoBehaviour
{
    //Public
    public Vector2 DirectionForward
    {
        get
        { return new Vector2(Mathf.Cos(m_DesiredCameraX * Mathf.Deg2Rad), Mathf.Sin(m_DesiredCameraX * Mathf.Deg2Rad)); }
    }

    public Vector2 DirectionRight
    {
        get
        { return new Vector2(Mathf.Cos((m_DesiredCameraX - 90) * Mathf.Deg2Rad), Mathf.Sin((m_DesiredCameraX - 90) * Mathf.Deg2Rad)); }
    }

    [NonSerialized] public bool m_Static = false;
    [NonSerialized] public Vector3 m_StaticPosition;
    [NonSerialized] public Quaternion m_StaticRotation;
    [NonSerialized] public float m_StaticSmoothSpeed;

    //Settings
    [Header("Camera Movement")]
    [SerializeField] private float m_CameraSensitivity;
    [Tooltip("Time it takes for camera to smooth desired position.")]
    [SerializeField] private float m_CameraSmoothness;
    [SerializeField] private float m_MinimumCameraYClamp;
    [SerializeField] private float m_MaximumCameraYClamp;
    private float m_CameraSensitivityBase;

    [Header("Camera Offset")]
    [SerializeField] private float m_MinimumAdditiveZoomClamp;
    [SerializeField] private float m_MaximumAdditiveZoomClamp;
    [SerializeField] private float m_ZoomSenstivity;
    [Tooltip("Time it takes for camera to smooth desired zoom.")]
    [SerializeField] private float m_ZoomSmootness;
    [SerializeField] private Vector3 m_CameraOffset;
    [SerializeField] private LayerMask m_CollisionLayers = default;

    //Script Attachments
    [Header("Attachments")]
    [SerializeField] private PlayerCharacter m_Target;

    //Private
    private float m_DesiredCameraX = 270;
    private float m_DesiredCameraY = 0;
    private float m_DesiredCameraZoom = 0;
    private float m_CameraX = 360;
    private float m_CameraY = 0;
    private float m_CameraZoom;
    private float m_CameraXSmoothVelocity;
    private float m_CameraYSmoothVelocity;
    private float m_CameraZoomVelocity;
    private Vector3 m_CameraLocation;
    private Vector3 m_StaticSmoothingPosition;
    private Quaternion m_StartRotation;
    private float m_Inter;
    private float m_InterVelocity;

    private void Awake()
    {
        m_CameraSensitivityBase = m_CameraSensitivity;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GameManager.Instance.DontDestroyOnLoadList.Add(gameObject);
        if (m_Target == null)
        {
            m_Target = FindObjectOfType<PlayerCharacter>();
        }
        Assert.IsNotNull(m_Target, "Camera attachment missing.");
        if (!m_Static)
        {
            SetCameraLocation();
        }
        m_Target.m_CameraMovement = this;
    }

    internal void UpdateCameraCoordinates(Vector3 mouseScrollDelta)
    {
        if (!m_Static)
        {

            if (Input.GetButton("Dash") && m_Target.m_Airborne && !m_Target.m_Mounted &&
                (m_CameraSensitivity < m_CameraSensitivityBase * 9f) && !m_Target.m_OnLedge)
            {
                m_CameraSensitivity *= 4f;
            }
            else
            {
                m_CameraSensitivity = m_CameraSensitivityBase;
            }
            m_DesiredCameraX -= mouseScrollDelta.x * m_CameraSensitivity * Time.deltaTime;
            m_DesiredCameraY -= mouseScrollDelta.y * m_CameraSensitivity * Time.deltaTime;
            m_DesiredCameraZoom -= mouseScrollDelta.z * m_ZoomSenstivity * Time.deltaTime;

            if (m_DesiredCameraX > 360)
            {
                m_DesiredCameraX -= 360;
                m_CameraX -= 360;
            }

            if (m_DesiredCameraX < 0)
            {
                m_DesiredCameraX += 360;
                m_CameraX += 360;
            }

            m_CameraX = Mathf.SmoothDamp(m_CameraX, m_DesiredCameraX, ref m_CameraXSmoothVelocity, m_CameraSmoothness);

            m_DesiredCameraY = Mathf.Clamp(m_DesiredCameraY, m_MinimumCameraYClamp, m_MaximumCameraYClamp);

            m_CameraY = Mathf.SmoothDamp(m_CameraY, m_DesiredCameraY, ref m_CameraYSmoothVelocity, m_CameraSmoothness);

            m_DesiredCameraZoom = Mathf.Clamp(m_DesiredCameraZoom, m_MinimumAdditiveZoomClamp, m_MaximumAdditiveZoomClamp);

            m_CameraZoom = Mathf.SmoothDamp(m_CameraZoom, m_DesiredCameraZoom, ref m_CameraZoomVelocity, m_ZoomSmootness);

            SetCameraLocation();
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, m_StaticPosition, ref m_StaticSmoothingPosition, m_StaticSmoothSpeed);
            m_Inter = Mathf.SmoothDamp(m_Inter, 1, ref m_InterVelocity, m_StaticSmoothSpeed);
            transform.rotation = Quaternion.Slerp(m_StartRotation, m_StaticRotation, m_Inter);
        }
    }

    private void SetCameraLocation()
    {
        float zoom = m_CameraOffset.z - m_CameraZoom;
        Vector3 targetPosition = m_Target.transform.position;
        Vector2 rotationXNormal = new Vector2(Mathf.Cos(m_CameraX * Mathf.Deg2Rad), Mathf.Sin(m_CameraX * Mathf.Deg2Rad));
        Vector2 rotationYNormal = new Vector2(Mathf.Cos(m_CameraY * Mathf.Deg2Rad), -Mathf.Sin(m_CameraY * Mathf.Deg2Rad));
        Vector3 newDirection = new Vector3(rotationXNormal.x * rotationYNormal.x, rotationYNormal.y, rotationXNormal.y * rotationYNormal.x).normalized;

        float collisionOffset = 0;

        if (Physics.Linecast(m_Target.transform.position, m_Target.transform.position + newDirection * zoom, out RaycastHit hit, m_CollisionLayers))
        {
            collisionOffset = zoom + hit.distance;
        }

        newDirection *= zoom - collisionOffset;

        m_CameraLocation = new Vector3(newDirection.x + m_CameraOffset.x, newDirection.y + m_CameraOffset.y, newDirection.z) + targetPosition;

        transform.position = m_CameraLocation;
        transform.LookAt(new Vector3(targetPosition.x + m_CameraOffset.x, targetPosition.y + m_CameraOffset.y, targetPosition.z), Vector3.up);
    }

    public void ResetInterpolation()
    {
        m_Inter = 0;
        m_InterVelocity = 0;
        m_StartRotation = transform.rotation;
    }
}