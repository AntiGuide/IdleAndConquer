using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Temporary fast forward solution
/// </summary>
public class __TimeScale : MonoBehaviour {
    /// <summary>
    /// Sets the time scale (standard = 1)
    /// </summary>
    /// <param name="scale">The scale to set the timescale to</param>
    public void SetTimeScale(float scale) {
        Time.timeScale = scale;
    }
}
