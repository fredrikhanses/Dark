using System.Collections;
using UnityEngine;

public class CharacterVFX : MonoBehaviour
{
    [Header("Foot Steps")]
    [SerializeField] private ObjectPooler m_DustPooler;
    [SerializeField] private Transform m_LeftFoot;
    [SerializeField] private Transform m_RightFoot;
    [SerializeField, Range(0f, 5f)] private float m_DustLifetime = 1f;
    [Header("Jump")]
    [SerializeField] private ObjectPooler m_JumpPooler;
    [SerializeField, Range(0f, 5f)] private float m_JumpLifetime = 1f;
    [Header("Charge")]
    [SerializeField] private Transform m_Chest;
    [SerializeField] private ObjectPooler m_ChargePooler;
    [SerializeField, Range(0f, 5f)] private float m_ChargeLifetime = 1f;
    [Header("Dash")]
    [SerializeField] private Transform m_Pelvis;
    [SerializeField] private ObjectPooler m_DashPooler;
    [SerializeField, Range(0f, 5f)] private float m_DashLifetime = 1f;

    public void VFXFootStepEvent(int rightFoot)
    {
        GameObject dustEffect = m_DustPooler.GetPooledObject();
        if (dustEffect != null)
        {
            if (rightFoot > 0)
            {
                dustEffect.transform.position = m_RightFoot.position;
            }
            else
            {
                dustEffect.transform.position = m_LeftFoot.position;
            }
            dustEffect.transform.SetParent(null);
            dustEffect.SetActive(true);
            StartCoroutine(SetInactive(dustEffect, m_DustPooler, m_DustLifetime));
        }
    }

    public void VFXJumpEvent()
    {
        GameObject jumpEffect = m_JumpPooler.GetPooledObject();
        if (jumpEffect != null)
        {
            jumpEffect.transform.position = transform.position;
            jumpEffect.SetActive(true);
            StartCoroutine(SetInactive(jumpEffect, m_JumpPooler, m_JumpLifetime));
        }
    }

    public void VFXDashEvent()
    {
        GameObject dashEffect = m_DashPooler.GetPooledObject();
        if (dashEffect != null)
        {
            dashEffect.transform.position = m_Pelvis.position;
            dashEffect.SetActive(true);
            StartCoroutine(SetInactive(dashEffect, m_DashPooler, m_DashLifetime));
        }
    }

    public void VFXChargeEvent()
    {
        GameObject chargeEffect = m_ChargePooler.GetPooledObject();
        if (chargeEffect != null)
        {
            chargeEffect.transform.position = m_Chest.position;
            chargeEffect.SetActive(true);
            StartCoroutine(SetInactive(chargeEffect, m_ChargePooler, m_ChargeLifetime));
        }
    }

    private IEnumerator SetInactive(GameObject visualEffect, ObjectPooler objectPooler, float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        visualEffect.SetActive(false);
        visualEffect.transform.SetParent(objectPooler.transform);
    }
}
