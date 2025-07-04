using UnityEngine;

public class PlaySoundOnInteraction : MonoBehaviour
{
    public AudioSource interactionSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionSound.Play();
        }
    }
}
