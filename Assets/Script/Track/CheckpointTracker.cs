using Unity.VisualScripting;
using UnityEngine;

public class CheckpointTracker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AiLearnso") || collision.gameObject.CompareTag("Aimilton"))
        {
            gameObject.SetActive(false);
        }

    }

    public void Restart()
    {
      gameObject.SetActive(true);
    }
}
