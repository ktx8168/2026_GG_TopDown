using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class DialogueBox : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text nameText;
    public TMP_Text bodyText;
    public GameObject choicePanel;
    public TMP_Text choiceText1;
    public TMP_Text choiceText2;
    public GameObject documentPanel;
    public UnityEngine.UI.Image documentImage;
    public bool isOpen = false;
    public Talk nearTalk;
    public Door nearDoor;
    private Talk current;
    private int index = 0;
    private bool choosing = false;
    void Update()
    {
        Keyboard kb = Keyboard.current;
        if (choosing)
        {
            if (kb.digit1Key.wasPressedThisFrame) { Choose(1); }
            else if (kb.digit2Key.wasPressedThisFrame) { Choose(2); }
        }
        else if (kb.eKey.wasPressedThisFrame)
        {
            if (isOpen) { Next(); }
            else if (nearTalk != null && nearTalk.isActiveAndEnabled) { nearTalk.Say(); }
            else if (nearDoor != null) { nearDoor.Use(); }
        }
    }
    public void Open(Talk talk)
    {
        current = talk;
        index = 0;
        choosing = false;
        isOpen = true;
        choicePanel.SetActive(false);
        if (talk.documentImage != null)
        {
            panel.SetActive(false);
            documentImage.sprite = talk.documentImage;
            documentImage.preserveAspect = true;
            documentPanel.SetActive(true);
        }
        else
        {
            if (documentPanel != null) documentPanel.SetActive(false);
            panel.SetActive(true);
            ShowLine();
        }
    }
    void ShowLine()
    {
        nameText.text = current.lines[index].speaker;
        bodyText.text = current.lines[index].text;
        bool last = index == current.lines.Length - 1;
        if (last && current.choice1 != "") { ShowChoices(); }
    }
    void Next()
    {
        if (current.documentImage != null)
        {
            CloseDocument();
            return;
        }
        index += 1;
        if (index < current.lines.Length) { ShowLine(); }
        else
        {
            Talk finished = current;
            Close();
            finished.Finish();
        }
    }
    public void CloseDocument()
    {
        if (current == null || current.documentImage == null) return;
        Talk finished = current;
        Close();
        finished.Finish();
    }
    void ShowChoices()
    {
        choosing = true;
        choiceText1.text = "1. " + current.choice1;
        choiceText2.text = "2. " + current.choice2;
        choicePanel.SetActive(true);
    }
    void Choose(int number)
    {
        Talk chosen = current;
        Close();
        if (number == 1) { chosen.onChoice1.Invoke(); }
        else { chosen.onChoice2.Invoke(); }
    }
    void Close()
    {
        choosing = false;
        isOpen = false;
        panel.SetActive(false);
        choicePanel.SetActive(false);
        if (documentPanel != null) documentPanel.SetActive(false);
        current = null;
    }
}
