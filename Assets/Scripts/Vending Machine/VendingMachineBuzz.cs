using UnityEngine;

public class VendingMachineBuzz : MonoBehaviour
{
    private AudioSource buzzAudio;

    private void Start()
    {
        buzzAudio =
            GetComponent<AudioSource>();

        if (buzzAudio != null)
        {
            buzzAudio.Stop();
        }
    }

    private void OnTriggerEnter2D(
        Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (buzzAudio != null &&
                !buzzAudio.isPlaying)
            {
                buzzAudio.Play();
            }
        }
    }

    private void OnTriggerExit2D(
        Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (buzzAudio != null)
            {
                buzzAudio.Stop();
            }
        }
    }
}
