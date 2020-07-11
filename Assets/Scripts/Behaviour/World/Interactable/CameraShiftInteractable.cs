using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShiftInteractable : MonoBehaviour, IInteract, IEvent
{
    public Vector3 m_TargetPosition;
    [HideInInspector] public float m_WaitTime;
    public Quaternion m_Direction = new Quaternion(0, 0, 0, 1);
    [SerializeField] float m_TransitionSpeed;
    [SerializeField] bool m_SingleTrigger;
    bool m_IsTriggered;

    public EndCondition m_EndCondition;

    public enum EndCondition
    {
        UIExit,
        Timer,
        InteractInput
    }

    public void ImDoingMyThing()
    {
        StartCoroutine(WaitForEndCondition());
    }

    public void Event()
    {
        StartCoroutine(WaitForEndCondition());
    }

    IEnumerator WaitForEndCondition()
    {
        if (!m_IsTriggered)
        {
            var cam = GameManager.Instance.CameraMovement;
            if (cam != null)
            {
                m_IsTriggered = true;
                cam.m_Static = true;
                cam.m_StaticPosition = m_TargetPosition;
                cam.m_StaticRotation = m_Direction;
                cam.m_StaticSmoothSpeed = m_TransitionSpeed;
                cam.ResetInterpolation();
            }

            switch (m_EndCondition)
            {
                case EndCondition.UIExit:
                    yield return new WaitWhile(UIController.Instance.IsUIInput);
                    break;
                case EndCondition.Timer:
                    GameManager.Instance.PlayerCharacter.m_MovementDirection = Vector3.zero;
                    GameManager.Instance.PlayerCharacter.m_Jumping = false;
                    UIController.Instance.UIInput = true;
                    yield return new WaitForSeconds(m_WaitTime);
                    UIController.Instance.UIInput = false;
                    break;
                case EndCondition.InteractInput:
                    GameManager.Instance.PlayerCharacter.m_MovementDirection = Vector3.zero;
                    GameManager.Instance.PlayerCharacter.m_Jumping = false;
                    UIController.Instance.UIInput = true;
                    UIController.Instance.InputConformation = false;
                    yield return new WaitUntil(UIController.Instance.UIInputConformation);
                    UIController.Instance.UIInput = false;
                    break;
                default:
                    break;
            }
            cam.m_Static = false;
        }

        if (!m_SingleTrigger)
        {
            m_IsTriggered = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(m_TargetPosition, 0.25f);
    }


}

