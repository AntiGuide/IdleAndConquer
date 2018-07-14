using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Display a general as a button in the general menue
/// </summary>
public class GeneralButton : MonoBehaviour {
    /// <summary>The country that is displayed</summary>
    public Text country;

    /// <summary>The name that is displayed</summary>
    public Text generalName;

    /// <summary>The win lose history that is displayed</summary>
    public Text winLoseHistory;

    /// <summary>
    /// Sets the texts of a GeneralButton
    /// </summary>
    /// <param name="country">The generals country</param>
    /// <param name="generalName">The generals name</param>
    /// <param name="winLoseHistory">The generals win lose history</param>
    public void SetTexts(string country, string generalName, string winLoseHistory) {
        this.country.text = country;
        this.generalName.text = generalName;
        this.winLoseHistory.text = winLoseHistory;
    }
}
