using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public AudioSource sound;
    public AudioClip hoversound;
    public AudioClip clicksound;

    public void HoverSound(){
        sound.PlayOneShot(hoversound);
    }

    public void ClickSound(){
        sound.PlayOneShot(clicksound);
    }
}
