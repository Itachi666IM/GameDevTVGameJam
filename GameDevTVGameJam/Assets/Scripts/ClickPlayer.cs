using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ClickPlayer : MonoBehaviour
{
    public AudioSource clickSound;

    public void ClickSound()
    {
        clickSound.Play();
    }    
}
