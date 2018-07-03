using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
    public AudioSource AudioSource1;
    public AudioClip[] Clips;

    public enum Sounds {
        CANCEL_SELL = 0,
        FLOATUP,
        MENUE_TAPS,
        QUEUE_TAPS,
        REPORT_TAPS,
        SOFTCURRENCY_COUNTUP,
        SWITCHBASE_TO_MISSION,
        BUILDING,
        UNIT_READY,
        UPGRADING
    }

    public void StartSound(Sounds sound) {
        this.AudioSource1.PlayOneShot(this.Clips[(int)sound]);
    }

    public void StartLoopingSound(Sounds sound, float volume) {
        this.AudioSource1.loop = true;
        this.AudioSource1.clip = this.Clips[(int)sound];
        this.AudioSource1.volume = volume;
        this.AudioSource1.Play();
    }

    public void StopLoopingSound() {
        this.AudioSource1.loop = false;
    }
}
