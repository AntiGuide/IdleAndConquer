public class TankCategory {
    public MoneyManagement moneyManager;
    private int cost;
    private float buildTime;
    private int damage;
    private string name;

    public TankCategory(string name, int damage, int cost, float buildTime) {
        this.Name = name;
        this.Damage = damage;
        this.Cost = cost;
        this.BuildTime = buildTime;
    }

    public int Cost {
        get { return this.cost; }
        set { this.cost = value; }
    }

    public float BuildTime {
        get { return this.buildTime; }
        set { this.buildTime = value; }
    }

    public int Damage {
        get { return this.damage; }
        set { this.damage = value; }
    }

    public string Name {
        get { return this.name; }
        set { this.name = value; }
    }

    public bool Build(ref int availableUnits, ref MoneyManagement moneyManager) {
        if (this.moneyManager.SubMoney(this.Cost)) {
            return true;
        } else {
            return false;
        }
    }
}