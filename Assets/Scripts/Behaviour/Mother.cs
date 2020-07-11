using UnityEngine;
using UnityEngine.VFX;

public class Mother : MonoBehaviour
{
    [Header("Un Corrupted")]
    [SerializeField] private Material m_UnCorruptedMaterial;
    [SerializeField] private Gradient m_UnCorruptedGradient;
    [SerializeField] private Dialogue m_UnCorruptedDialogue;
    public Vector3 m_UnCorruptCameraShiftPosition;
    public Quaternion m_UnCorruptCameraShiftRotation = new Quaternion(0, 0, 0, 1);

    [Header("Corrupted")]
    [SerializeField] private Material m_CorruptedMaterial;
    [SerializeField] private Gradient m_CorruptedGradient;
    [SerializeField] private Dialogue m_CorruptedDialogue;
    public Vector3 m_CorruptCameraShiftPosition;
    public Quaternion m_CorruptCameraShiftRotation = new Quaternion(0, 0, 0, 1);

    [Header("Full Corrupted")]
    [SerializeField] private Material m_FullCorruptedMaterial;
    [SerializeField] private Gradient m_FullCorruptedGradient;
    [SerializeField] private Dialogue m_FullCorruptedDialogue;
    public Vector3 m_FullCorruptCameraShiftPosition;
    public Quaternion m_FullCorruptCameraShiftRotation = new Quaternion(0, 0, 0, 1);

    [Space]
    [SerializeField] private bool m_FirstMother;
    [SerializeField] private MeshRenderer m_MeshRenderer;
    [SerializeField] private VisualEffect m_VisualEffect;
    [SerializeField] private DialougeInteractable m_DialougeInteractable;
    [SerializeField] private CameraShiftInteractable m_CameraShiftInteractable;

    private const string k_ColorGradient = "Color Gradient";

    public bool FirstMother { get => m_FirstMother; }

    private void Start()
    {
        if (m_MeshRenderer == null)
        {
            m_MeshRenderer = GetComponent<MeshRenderer>();
        }
        if (m_VisualEffect == null)
        {
            m_VisualEffect = GetComponent<VisualEffect>();
        }
        if (m_DialougeInteractable == null)
        {
            m_DialougeInteractable = GetComponent<DialougeInteractable>();
        }
        if (m_CameraShiftInteractable == null)
        {
            m_CameraShiftInteractable = GetComponent<CameraShiftInteractable>();
        }
    }

    public void SetUnCorrupted()
    {
        if (m_MeshRenderer != null && m_UnCorruptedMaterial != null)
        {
            m_MeshRenderer.material = m_UnCorruptedMaterial;
        }
        if (m_VisualEffect != null && m_UnCorruptedGradient != null)
        {
            m_VisualEffect.SetGradient(k_ColorGradient, m_UnCorruptedGradient);
        }
        if (m_DialougeInteractable != null && m_UnCorruptedDialogue != null)
        {
            m_DialougeInteractable.m_Dialogue[0] = m_UnCorruptedDialogue;
        }
        if (m_CameraShiftInteractable != null)
        {
            m_CameraShiftInteractable.m_TargetPosition = m_UnCorruptCameraShiftPosition;
            m_CameraShiftInteractable.m_Direction = m_UnCorruptCameraShiftRotation;
        }
    }

    public void SetCorrupted()
    {
        if (m_MeshRenderer != null && m_CorruptedMaterial != null)
        {
            m_MeshRenderer.material = m_CorruptedMaterial;
        }
        if (m_VisualEffect != null && m_CorruptedGradient != null)
        {
            m_VisualEffect.SetGradient(k_ColorGradient, m_CorruptedGradient);
        }
        if (m_DialougeInteractable != null && m_CorruptedDialogue != null)
        {
            m_DialougeInteractable.m_Dialogue[0] = m_CorruptedDialogue;
        }
        if (m_CameraShiftInteractable != null)
        {
            m_CameraShiftInteractable.m_TargetPosition = m_CorruptCameraShiftPosition;
            m_CameraShiftInteractable.m_Direction = m_CorruptCameraShiftRotation;
        }
    }

    public void SetFullCorrupted()
    {
        if (m_MeshRenderer != null && m_FullCorruptedMaterial != null)
        {
            m_MeshRenderer.material = m_FullCorruptedMaterial;
        }
        if (m_VisualEffect != null && m_FullCorruptedGradient != null)
        {
            m_VisualEffect.SetGradient(k_ColorGradient, m_FullCorruptedGradient);
        }
        if (m_DialougeInteractable != null && m_FullCorruptedDialogue != null)
        {
            m_DialougeInteractable.m_Dialogue[0] = m_FullCorruptedDialogue;
        }
        if (m_CameraShiftInteractable != null)
        {
            m_CameraShiftInteractable.m_TargetPosition = m_FullCorruptCameraShiftPosition;
            m_CameraShiftInteractable.m_Direction = m_FullCorruptCameraShiftRotation;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(m_UnCorruptCameraShiftPosition, 0.25f);
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(m_CorruptCameraShiftPosition, 0.25f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(m_FullCorruptCameraShiftPosition, 0.25f);
    }
}
