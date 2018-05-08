public class TankCategory
{

    public MoneyManagement moneyManager;

    private int cost;
    public int Cost {
        get {
            return cost;
        }

        set {
            cost = value;
        }
    }

    private float buildTime;
    public float BuildTime {
        get {
            return buildTime;
        }

        set {
            buildTime = value;
        }
    }

    private int damage;
    public int Damage {
        get {
            return damage;
        }

        set {
            damage = value;
        }
    }

    private string name;
    public string Name {
        get {
            return name;
        }

        set {
            name = value;
        }
    }

    public TankCategory(string name, int damage, int cost, float buildTime) {
        this.Name = name;
        this.Damage = damage;
        this.Cost = cost;
        this.BuildTime = buildTime;
    }

    public bool Build(ref int availableUnits, ref MoneyManagement moneyManager) {
        if (moneyManager.subMoney(Cost)) {
            return true;
        } else {
            return false;
        }

    }

}