using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
    public AudioSource AudioSource1;
    public AudioSource AudioSourceBGM;
    public AudioClip[] Clips;
    public AudioClip[] BGMClips;
    public float volumeBGM = 0.2f;

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
        UPGRADING,
        RESEARCH_COMPLETE,// TODO
        FUNDS_REQUIRED,
        CANNOT_BUILT_HERE,
        LOW_POWER,
        MISSION_COMPLETE
    }

    public void StartSound(Sounds sound, float volume = 1f) {
        this.AudioSource1.PlayOneShot(this.Clips[(int)sound], volume);
    }

    public AudioSource StartLoopingSound(Sounds sound, float volume) {
        AudioSource retAudioSource = this.gameObject.AddComponent<AudioSource>();
        retAudioSource.loop = true;
        retAudioSource.clip = this.Clips[(int)sound];
        retAudioSource.volume = volume;
        retAudioSource.Play();
        StartCoroutine(StopLoopingSoundDelayed(retAudioSource, 1f));
        return retAudioSource;
    }

    private IEnumerator StopLoopingSoundDelayed(AudioSource retAudioSource, float delay) {
        yield return new WaitForSeconds(delay);
        StopLoopingSound(ref retAudioSource);
    }

    public void StopLoopingSound(ref AudioSource inpAudioSource) {
        Destroy(inpAudioSource);
    }

    private void Start() {
        StartCoroutine(PlayBGM());
    }

    private System.Collections.IEnumerator PlayBGM() {
        this.AudioSourceBGM.volume = volumeBGM;
        int aktTrackNumber = -1;
        int nextTrackNumber = Mathf.RoundToInt(UnityEngine.Random.Range(0f, BGMClips.Length - 1));
        this.AudioSourceBGM.clip = this.BGMClips[nextTrackNumber];
        this.AudioSourceBGM.clip.LoadAudioData();
        while (true) {
            this.AudioSourceBGM.Play();
            if (aktTrackNumber >= 0) {
                this.BGMClips[aktTrackNumber].UnloadAudioData();
            }
            yield return new WaitForSeconds(this.AudioSourceBGM.clip.length / 2f);
            aktTrackNumber = nextTrackNumber;
            nextTrackNumber = Mathf.RoundToInt(UnityEngine.Random.Range(0f, BGMClips.Length - 1));
            this.BGMClips[nextTrackNumber].LoadAudioData();
            yield return new WaitForSeconds(this.AudioSourceBGM.clip.length / 2f);
            this.AudioSourceBGM.Stop();
            this.AudioSourceBGM.clip = this.BGMClips[nextTrackNumber];
        }
    }
}
