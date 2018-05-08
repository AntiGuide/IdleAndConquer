using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFactory {
    //To build tanks
    //+1 Tank Tier each
    private List<TankCategory> tankTypes = new List<TankCategory>();
    private MoneyManagement moneyManagement;

    public TankFactory(List<TankCategory> tankTypes, MoneyManagement moneyManagement) {
        this.tankTypes = tankTypes;
        this.moneyManagement = moneyManagement;
    }
    
    public bool Build(TankCategory category) {
        if (moneyManagement.subMoney(category.Cost)) {
            //TODO: Que for tanks being built
            return true;
        } else {
            return false;
        }
    }

    public void BuildTankButton(int place) {
        int i = 0;
        foreach (TankCategory category in tankTypes) {
            if (++i == place) {
                if (Build(category)) {
                    //Start que Animation
                }
            }
        }
    }
}
