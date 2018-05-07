using System.Collections.Generic;

public class Base {
    
    public string baseName;
    public bool isMainBase;

    public CommandCenter commandCenter; //ROME
    public OreRefinery oreRefinery;     //ROME
    public TankFactory tankFactory;     //ROME
    public ResearchLab researchLab;     //FOCUS
    public PowerPlant powerPlant;       //FOCUS
    public Barracks barracks;           //OMAHA BEACH
    public AirField airField;           //OMAHA BEACH

    public MoneyManagement moneyManagement;

    public Base(string baseName, bool isMainBase, CommandCenter commandCenter, OreRefinery oreRefinery, ResearchLab researchLab, PowerPlant powerPlant, Barracks barracks, AirField airField) {
        this.baseName = baseName;
        this.isMainBase = isMainBase;
        
        this.commandCenter = new CommandCenter();
        
        this.oreRefinery = new OreRefinery();

        List<TankCategory> tankTypes = new List<TankCategory>();
        tankTypes.Add(new TankCategory("French Light Tank", 5, 100, 5.0f));
        tankTypes.Add(new TankCategory("Italian Rapidfire Tank", 5, 100, 5.0f));
        tankTypes.Add(new TankCategory("Spanish Rocket Tank", 5, 100, 5.0f));
        tankTypes.Add(new TankCategory("German Medium Tank", 5, 100, 5.0f));
        tankTypes.Add(new TankCategory("British Artillery Tank", 5, 100, 5.0f));
        tankTypes.Add(new TankCategory("Swedish Stealth Tank", 5, 100, 5.0f));
        tankTypes.Add(new TankCategory("European Heavy Tank", 5, 100, 5.0f));
        this.tankFactory = new TankFactory(tankTypes, moneyManagement);

        this.researchLab = new ResearchLab();

        this.powerPlant = new PowerPlant();

        this.barracks = new Barracks();

        this.airField = new AirField();

    }
}
