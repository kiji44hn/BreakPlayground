using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource audioSource;

    // アニメーションイベントで呼び出される関数
    public void PlaySoundEffect()
    {
        audioSource.Play();
    }
}