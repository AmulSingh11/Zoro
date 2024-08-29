using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource rotationSound;
    public AudioSource levelCompleteSound;

    public void PlayRotationSound()
    {
        rotationSound.Play();
    }

    public void PlayLevelCompleteSound()
    {
        levelCompleteSound.Play();
    }
}

