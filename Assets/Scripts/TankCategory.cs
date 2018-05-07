public class TankCategory
{
    private string name;
    private int damage;
    private float buildTime;

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

    public TankCategory(string name, int damage, int cost, float buildTime) {
        this.name = name;
        this.damage = damage;
        this.Cost = cost;
        this.buildTime = buildTime;
    }

    public bool Build(ref int availableUnits, ref MoneyManagement moneyManager) {
        if (moneyManager.subMoney(Cost)) {
            return true;
        } else {
            return false;
        }

    }

}