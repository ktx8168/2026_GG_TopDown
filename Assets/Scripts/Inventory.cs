using UnityEngine;
using TMPro;
public class Inventory : MonoBehaviour
{
    public int keys = 0;
    public TMP_Text keyText;
    public int clues = 0;
    public TMP_Text clueText;
    public GameObject cluePanel;
    public bool cluesCompleted = false;
    public void AddClue()
    {
        clues += 1;
        if (clueText != null) clueText.text = "단서 " + clues + "/4";
        ShowClues();
    }
    public void ShowClues()
    {
        // 1스테이지를 끝낸 뒤에는 다시 들어와도 표시하지 않습니다.
        if (cluesCompleted) return;
        if (cluePanel != null) cluePanel.SetActive(true);
    }
    public void HideClues()
    {
        if (cluePanel != null) cluePanel.SetActive(false);
    }
    public void CompleteClues()
    {
        cluesCompleted = true;
        HideClues();
    }
    public void AddKey()
    {
        keys += 1;
        keyText.text = "열쇠 " + keys;
    }
}
