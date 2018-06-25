﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
    public AudioSource AudioSource1;
    public AudioClip[] Clips;

    public enum Sounds {
        BUTTON_CLICK = 0
    }

    public void StartSound(Sounds sound) {
        this.AudioSource1.PlayOneShot(this.Clips[(int)Sounds.BUTTON_CLICK]);
    }
}
