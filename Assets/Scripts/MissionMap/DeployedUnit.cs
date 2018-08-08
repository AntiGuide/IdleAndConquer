using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeployedUnit : MonoBehaviour {

    public Image UnitImage;

    public Text CountText;

    public Unit AttachedUnit;

    private int count = 1;

    private OnClickDeploy ocd;

    private ShowChosenGeneral showChosenGeneral;

    public void Initialize(Unit unit,  OnClickDeploy ocd,  ShowChosenGeneral showChosenGeneral) {
        UnitImage.sprite = unit.CreateAndOrderButton.UnitIconSprite;
        this.showChosenGeneral = showChosenGeneral;
        AttachedUnit = unit;
        this.ocd = ocd;
    }

    public void IncreaseCount() {
        if (CountText == null) {
            return;
        }
        count++;
        CountText.text = count > 1 ? count.ToString() : "";
    }

    public void DecreaseCount() {
        count--;
        CountText.text = count > 1 ? count.ToString() : "";
        ocd.GiveUnitBack();
        if (count <= 0) {
            CountText.text = "";
            this.showChosenGeneral.unitStacks.Remove(this);
            Destroy(gameObject);
        }
    }
}
