using UnityEngine;

/// <summary>
/// Class for genarating FloatUp feedback elements
/// </summary>
public class FloatUpSpawner : MonoBehaviour {
    /// <summary>Prefab for new FloatUp elements</summary>
    public GameObject FloatUpPrefab;

    /// <summary>The time that is needed for newly generated FloatUp elements to completely fade</summary>
    public float FadeTime;

    /// <summary>The distance that the newly generated FloatUp elements travel until disappearing</summary>
    public float TravelDistance;

    public SoundController soundController;

    /// <summary>Method creates a new FloatUp element of the given parameters</summary>
    /// <param name="value">The value to display in the generated FloatUp element</param>
    /// <param name="type">The type of the generated FloatUp element. For example this could decide between Powerlevel and Dollar.</param>
    /// <param name="pos">The position at which the FloatUp will spawn and begin traveling</param>
    public void GenerateFloatUp(long value, FloatUp.ResourceType type, Vector2 pos) {
        GameObject go = Instantiate(this.FloatUpPrefab, transform);
        if (value > 0) {
            pos += new Vector2(0, 25);
        } else {
            pos += new Vector2(0, -25);
        }

        go.transform.position = pos;
        go.GetComponent<FloatUp>().Initialize(type, value, this.FadeTime, this.TravelDistance);
        soundController.StartSound(SoundController.Sounds.FLOATUP);
    }
}
