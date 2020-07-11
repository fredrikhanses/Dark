using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStartDialogue : MonoBehaviour
{
    public bool m_Ending;

    Dialogue m_Current;
    [SerializeField] TextMeshProUGUI m_Text;
    Animator m_Animator;

    Color defaultTextColor = Color.white;
    Color defaultBackgroundColor = Color.white;

    private const string k_Rise = "Rise";

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    internal void StartDialogue(Dialogue dialogue, bool fadeBackground)
    {
        StartDialogue(dialogue, fadeBackground, defaultBackgroundColor, defaultTextColor);
    }

    internal void StartDialogue(Dialogue dialogue, bool fadeBackground, Color Background, Color Text)
    {
        m_Text.color = Text;
        GetComponent<Image>().color = Background;

        m_Current = dialogue;
        gameObject.SetActive(true);
        UIController.Instance.UIInput = true;
        if (fadeBackground)
        {
            m_Animator.SetTrigger("Start");
        }
        StartCoroutine(Dialogue(fadeBackground));
    }

    IEnumerator Dialogue(bool fade)
    {
        if (fade)
        {
            yield return new WaitUntil(BGFaded);
        }

        foreach (DialogueNode d in m_Current.dialogue)
        {
            foreach (string s in d.text)
            {
                m_Animator.SetBool("Text Visibility", false);
                yield return new WaitUntil(AnimationDone);
                SetText(s);
                m_Animator.SetBool("Text Visibility", true);
                UIController.Instance.InputConformation = false;
                yield return new WaitUntil(InputConformation);
            }
        }

        m_Animator.SetTrigger("End");
        GameManager.Instance.PlayerAnimator.SetBool(k_Rise, true);
        GameManager.Instance.PlayerInput.enabled = false;
        yield return new WaitUntil(Faded);
        EndDialogue();
    }

    void EndDialogue()
    {
        StopCoroutine(Dialogue(false));
        m_Animator.SetTrigger("Faded");
        UIController.Instance.UIInput = false;
        gameObject.SetActive(false);
        if(m_Ending == true)
        {
            GameManager.Instance.ResetToMainMenu();
        }
    }

    private void SetText(string s)
    {
        m_Text.text = s;
    }

    bool InputConformation()
    {
        return UIController.Instance.InputConformation;
    }

    bool AnimationDone()
    {
        bool time = m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
        bool param1 = !m_Animator.GetCurrentAnimatorStateInfo(0).IsTag("Text") && time;
        return param1;
    }

    bool Faded()
    {
        bool time = m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
        bool param1 = m_Animator.GetCurrentAnimatorStateInfo(0).IsTag("Fade") && time;
        return param1;
    }

    bool BGFaded()
    {
        bool time = m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
        bool param1 = m_Animator.GetCurrentAnimatorStateInfo(0).IsTag("BG") && time;
        return param1;
    }
}
