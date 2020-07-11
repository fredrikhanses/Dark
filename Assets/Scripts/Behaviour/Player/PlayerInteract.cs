using System.Linq;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    //Public
    [System.NonSerialized] public bool m_IsInteracting;

    //Private
    [Header("RayCast")]
    [Tooltip("This is the current hit gameobject")]
    [SerializeField] private GameObject m_InteractObject;
    [Tooltip("The max distance of interaction")]
    [SerializeField] private float m_InteractionDist = 300.0f;
    [Tooltip("The layers the trace is able to hit")]
    [SerializeField] private LayerMask m_LayerMask;
    [Tooltip("If this is true then a line will be drawn. This represents your interaction distance")]
    [SerializeField] private bool m_DrawRayCast;
    [SerializeField] private float m_RayCastOffsetY = 50f;
    private Camera m_Cam;

    private bool m_CanInteract = true;

    private void Awake()
    {
        m_Cam = Camera.main;
    }

    private void FixedUpdate()
    {
        InteractCheck();
    }
    
    //Perform Interaction
    private void InteractCheck()
    {
        //Find out a way to make the trace better
        RaycastHit hit;
        Ray ray = m_Cam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y + m_RayCastOffsetY, Input.mousePosition.z));
        if (Physics.Raycast(ray, out hit, m_InteractionDist, m_LayerMask))
        {
            m_InteractObject = hit.collider.gameObject;
            var Interacts = m_InteractObject.GetComponents<MonoBehaviour>().OfType<IInteract>();
            if (Interacts.Count() > 0)
                UIController.Instance.m_Interact.m_Highlighting = true;
            else
                UIController.Instance.m_Interact.m_Highlighting = false;
            if (m_CanInteract && m_IsInteracting)
            {
                m_CanInteract = false;
                m_IsInteracting = true;
                if (Interacts != null)
                {
                    foreach (var i in Interacts)
                    {
                        i.ImDoingMyThing();
                    }
                    Debug.Log("Hit object: " + m_InteractObject.name);
                }
                else
                {
                    Debug.Log("Tested Item does not implement correct interface");
                }
                if (m_DrawRayCast)
                {
                    //draws green line if hit is true
                    Debug.DrawRay(ray.origin, ray.direction * m_InteractionDist, Color.green, 2, false);
                }
            }
        }
        else
        {
            if (m_DrawRayCast)
            {
                //draws red line if nothing is hit
                Debug.DrawRay(ray.origin, ray.direction * m_InteractionDist, Color.red, 2, false);
            }
            UIController.Instance.m_Interact.m_Highlighting = false;
        }
    }

    public void ResetInteract()
    {
        m_CanInteract = true;
        m_IsInteracting = false;
    }

}
