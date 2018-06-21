using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to handle base switching on player input
/// </summary>
public class BaseSwitcher : MonoBehaviour {
    /// <summary>An array of the base containers to enable/disable a base</summary>
    public GameObject[] Bases;

    /// <summary>An array of bools that indicate wether a base is enable/disable (indicator for array Bases)</summary>
    public bool[] EnablesBases;

    /// <summary>The index of the current enabled base</summary>
    public int CurrentBase;

    /// <summary>The position where the camera was when the app started</summary>
    private Vector3 startPosCamera;

    /// <summary>
    /// Switches the base after a button click
    /// </summary>
    /// <param name="isLeft">If true the base should be switched to the one on the left. If false its to the right</param>
    public void OnClickBaseSwitch(bool isLeft) {
        if (isLeft && this.CurrentBase > 0) {
            ////this.Bases[this.CurrentBase].SetActive(false);
            this.Bases[this.CurrentBase].transform.localPosition -= new Vector3(10000, 10000, 10000);
            this.CurrentBase--;
            this.Bases[this.CurrentBase].transform.localPosition += new Vector3(10000, 10000, 10000);
            ////this.Bases[this.CurrentBase].SetActive(true);
        } else if (this.CurrentBase < this.Bases.Length - 1) {
            ////this.Bases[this.CurrentBase].SetActive(false);
            this.Bases[this.CurrentBase].transform.localPosition -= new Vector3(10000, 10000, 10000);
            this.CurrentBase++;
            this.Bases[this.CurrentBase].transform.localPosition += new Vector3(10000, 10000, 10000);
            ////this.Bases[this.CurrentBase].SetActive(true);
        }

        this.Bases[this.CurrentBase].GetComponent<EnergyPool>().SetActive();
        this.transform.position = this.startPosCamera;
    }

    /// <summary>
    /// Checks if it is possible to move to a base in both directions
    /// </summary>
    /// <param name="leftPossible">Is left possible</param>
    /// <param name="rightPossible">Is right possible</param>
    public void CheckPossibilities(out bool leftPossible, out bool rightPossible) {
        // Check if switches in the directions are possible
        leftPossible = this.CurrentBase > 0;
        rightPossible = this.CurrentBase < this.Bases.Length - 1;
    }

    /// <summary>
    /// Gets the BuildBuilding object of the current base
    /// </summary>
    /// <returns>Returns the BuildBuilding object of the current base</returns>
    public BuildBuilding GetBuilder() {
        return this.Bases[this.CurrentBase].GetComponentInChildren<BuildBuilding>();
    }

    /// <summary>
    /// Gets the EnergyPool object of the current base
    /// </summary>
    /// <returns>Returns the EnergyPool object of the current base</returns>
    public EnergyPool GetEnergyPool() {
        return this.Bases[this.CurrentBase].GetComponent<EnergyPool>();
    }

    /// <summary>
    /// Saves camera position and sets the current bases energy to active
    /// </summary>
    private void Start() {
        this.startPosCamera = this.transform.position;
        this.Bases[this.CurrentBase].GetComponent<EnergyPool>().SetActive();
    }
}
