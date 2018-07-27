using System.Collections;
using UnityEngine;

public class SoundController : MonoBehaviour {
    public AudioSource AudioSource1;
    public AudioSource AudioSourceBGM;
    public AudioClip[] Clips;
    public AudioClip[] BGMClips;
    public float volumeBGM = 0.2f;

    private int aktTrackNumber;

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
        RESEARCH_COMPLETE, // TODO
        FUNDS_REQUIRED,
        CANNOT_BUILT_HERE,
        LOW_POWER,
        MISSION_COMPLETE
    }

    public static void StopLoopingSound(ref AudioSource inpAudioSource) {
        UnityEngine.Object.Destroy(inpAudioSource);
    }

    public void StartSound(Sounds sound, float volume = 1f) {
        this.AudioSource1.PlayOneShot(this.Clips[(int)sound], volume);
    }

    public AudioSource StartLoopingSound(Sounds sound, float volume) {
        var retAudioSource = this.gameObject.AddComponent<AudioSource>();
        retAudioSource.loop = true;
        retAudioSource.clip = this.Clips[(int)sound];
        retAudioSource.volume = volume;
        retAudioSource.Play();
        this.StartCoroutine(StopLoopingSoundDelayed(retAudioSource, 1f));
        return retAudioSource;
    }

    private static IEnumerator StopLoopingSoundDelayed(AudioSource retAudioSource, float delay) {
        yield return new WaitForSeconds(delay);
        StopLoopingSound(ref retAudioSource);
    }

    private void Start() {
        this.AudioSourceBGM.volume = this.volumeBGM;
        this.AudioSourceBGM.loop = false;
        aktTrackNumber = -1;
    }

    private void Update() {
        if (!AudioSourceBGM.isPlaying) {
            aktTrackNumber = ++aktTrackNumber > this.BGMClips.Length - 1 ? 0 : aktTrackNumber;
            this.AudioSourceBGM.clip = this.BGMClips[aktTrackNumber];
            this.AudioSourceBGM.Play();
        }
    }
}
