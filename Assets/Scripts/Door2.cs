using UnityEngine;
using UnityEngine.InputSystem;
public class Door2 : MonoBehaviour
{
    public Transform target;
    public GameObject prompt;
    private Transform player;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            prompt.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
            prompt.SetActive(false);
        }
    }
    void Update()
    {
        if (player != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            player.position = target.position;
        }
    }
}
