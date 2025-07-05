using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrayerEffect : MonoBehaviour
{
    public ParticleSystem lightEffect;
    public float maxHeight = 10f;
    void Update()
    {
        if (transform.position.y > maxHeight)
        {
            if (!lightEffect.isPlaying) lightEffect.Play();
        }
        else
        {
            if (lightEffect.isPlaying) lightEffect.Stop();
        }
    }
}
