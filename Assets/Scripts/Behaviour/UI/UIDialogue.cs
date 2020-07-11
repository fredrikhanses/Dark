using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIDialogue : MonoBehaviour
{
    Dialogue current;
    [SerializeField] TextMeshProUGUI name;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image character;

    internal void StartDialogue(Dialogue dialogue)
    {
        current = dialogue;
        gameObject.SetActive(true);
        UIController.Instance.UIInput = true;
        StartCoroutine(Dialogue());
    }

    IEnumerator Dialogue()
    {
        foreach (DialogueNode d in current.dialogue)
        {
            character.sprite = d.image;
            name.text = d.name;
            foreach (string s in d.text)
            {
                ClearText();
                UIController.Instance.InputConformation = false;
                foreach (char c in s)
                {
                    if (c == ' ')
                    {
                        text.text += c;
                        continue;
                    }
                    text.text += c;

                    if (!UIController.Instance.InputConformation)
                    {
                        yield return new WaitForSeconds(d.textSpeed);
                    }
                }
                UIController.Instance.InputConformation = false;

                yield return new WaitUntil(InputConformation);
            }
        }
        EndDialogue();
    }

    private void EndDialogue()
    {
        StopCoroutine(Dialogue());
        UIController.Instance.UIInput = false;
        gameObject.SetActive(false);
    }

    private void ClearText()
    {
        text.text = "";
    }

    bool InputConformation()
    {
        return UIController.Instance.InputConformation;
    }
}
