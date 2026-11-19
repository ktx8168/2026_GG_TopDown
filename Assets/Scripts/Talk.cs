using UnityEngine;
using UnityEngine.Events;
public class Talk : MonoBehaviour
{
    public DialogueLine[] lines;
    // 이미지가 있으면 글 대신 이미지만 보여줍니다.
    public Sprite documentImage;
    public string choice1 = "";
    public string choice2 = "";
    public UnityEvent onChoice1;
    public UnityEvent onChoice2;
    // 마지막 대사를 읽고 창을 닫으면 연결한 행동을 실행합니다.
    public UnityEvent onFinished = new UnityEvent();
    public GameObject prompt;
    public DialogueBox dialogueBox;
    public bool finishOnlyOnce = false;
    public int needClues = 0;
    public Inventory inventory;
    public Talk lockedTalk;
    private bool finishedOnce = false;
    public void Say()
    {
        if (needClues > 0 && inventory.clues < needClues)
        {
            lockedTalk.Say();
        }
        else
        {
            dialogueBox.Open(this);
        }
    }
    public void Finish()
    {
        // 서신이나 단서 대화의 완료 이벤트는 처음 한 번만 실행합니다.
        if (documentImage != null || finishOnlyOnce)
        {
            if (finishedOnce) return;
            finishedOnce = true;
        }
        onFinished.Invoke();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            prompt.SetActive(true);
            dialogueBox.nearTalk = this;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            prompt.SetActive(false);
            if (dialogueBox.nearTalk == this) { dialogueBox.nearTalk = null; }
        }
    }
    void OnDisable()
    {
        if (prompt != null) { prompt.SetActive(false); }
        if (dialogueBox != null && dialogueBox.nearTalk == this)
        {
            dialogueBox.nearTalk = null;
        }
    }
}
