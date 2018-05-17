using UnityEngine;

public class Passives : MonoBehaviour
{

    private static float[,] skillValues = new float[4, 6] { {1.0f,1.0f,1.0f,1.0f,1.0f,1.0f},
                                                            {1.0f,1.0f,1.0f,1.0f,1.0f,1.0f},
                                                            {1.0f,1.0f,1.0f,1.0f,1.0f,1.0f},
                                                            {1.0f,1.0f,1.0f,1.0f,1.0f,1.0f}};

    private static int[,] skillValuesAbsolut = new int[4, 6] {  {0,0,0,0,0,0},
                                                                {0,0,0,0,0,0},
                                                                {0,0,0,0,0,0},
                                                                {0,0,0,0,0,0}};

    private static float[,] skillValuesArmortype = new float[4, 6] { {1.0f,1.0f,1.0f,1.0f,1.0f,1.0f},
                                                            {1.0f,1.0f,1.0f,1.0f,1.0f,1.0f},
                                                            {1.0f,1.0f,1.0f,1.0f,1.0f,1.0f},
                                                            {1.0f,1.0f,1.0f,1.0f,1.0f,1.0f}};

    private static int[,] skillValuesAbsolutArmortype = new int[4, 6] {  {0,0,0,0,0,0},
                                                                {0,0,0,0,0,0},
                                                                {0,0,0,0,0,0},
                                                                {0,0,0,0,0,0}};

    private static float[,] skillValuesVersusType = new float[4, 6] {   {1.0f,1.0f,1.0f,1.0f,1.0f,1.0f},
                                                                    {1.0f,1.0f,1.0f,1.0f,1.0f,1.0f},
                                                                    {1.0f,1.0f,1.0f,1.0f,1.0f,1.0f},
                                                                    {1.0f,1.0f,1.0f,1.0f,1.0f,1.0f}};

    private static int[,] skillValuesVersusTypeAbsolut = new int[4, 6] {{0,0,0,0,0,0},
                                                                    {0,0,0,0,0,0},
                                                                    {0,0,0,0,0,0},
                                                                    {0,0,0,0,0,0}};

    private static float[,] skillValuesVersusArmortype = new float[4, 6] {   {1.0f,1.0f,1.0f,1.0f,1.0f,1.0f},
                                                                    {1.0f,1.0f,1.0f,1.0f,1.0f,1.0f},
                                                                    {1.0f,1.0f,1.0f,1.0f,1.0f,1.0f},
                                                                    {1.0f,1.0f,1.0f,1.0f,1.0f,1.0f}};

    private static int[,] skillValuesVersusArmortypeAbsolut = new int[4, 6] {{0,0,0,0,0,0},
                                                                    {0,0,0,0,0,0},
                                                                    {0,0,0,0,0,0},
                                                                    {0,0,0,0,0,0}};

    private static float generalSurvivability = 0.0f;
    public static float GeneralSurvivability {
        get {
            return generalSurvivability;
        }

        set {
            generalSurvivability = value;
        }
    }

    public enum Value
    {
        HP = 0,
        ATTACK,
        CRITICAL,
        DEFENSE,
        COST,
        BUILDTIME
    }

    //Passiv Buffs & Nerfs
    public static void SetPassive(Unit.Type type, Value whichValue, float valueToSetTo) {
        skillValues[(int)type, (int)whichValue] = valueToSetTo;
    }

    public static void SetAbsolutPassive(Unit.Type type, Value whichValue, int valueToSetTo) {
        skillValuesAbsolut[(int)type, (int)whichValue] = valueToSetTo;
    }

    public static float GetPassive(Unit.Type type, Value whichValue) {
        return skillValues[(int)type, (int)whichValue];
    }

    public static int GetAbsolutPassive(Unit.Type type, Value whichValue) {
        return skillValuesAbsolut[(int)type, (int)whichValue];
    }

    //Passiv Buffs & Nerfs for specific ArmorTypes
    public static void SetPassiveArmortype(Unit.ArmorType type, Value whichValue, float valueToSetTo) {
        skillValuesArmortype[(int)type, (int)whichValue] = valueToSetTo;
    }

    public static void SetAbsolutPassiveArmortype(Unit.ArmorType type, Value whichValue, int valueToSetTo) {
        skillValuesAbsolutArmortype[(int)type, (int)whichValue] = valueToSetTo;
    }

    public static float GetPassiveArmortype(Unit.ArmorType type, Value whichValue) {
        return skillValuesArmortype[(int)type, (int)whichValue];
    }

    public static int GetAbsolutPassiveArmortype(Unit.ArmorType type, Value whichValue) {
        return skillValuesAbsolutArmortype[(int)type, (int)whichValue];
    }

    //Passiv Buffs & Nerfs against specific types of enemys
    public static void SetPassiveAgainstType(Unit.Type type, Value whichValue, float valueToSetTo) {
        skillValuesVersusType[(int)type, (int)whichValue] = valueToSetTo;
    }

    public static void SetAbsolutPassiveAgainstType(Unit.Type type, Value whichValue, int valueToSetTo) {
        skillValuesVersusTypeAbsolut[(int)type, (int)whichValue] = valueToSetTo;
    }

    public static float GetPassiveAgainstType(Unit.Type type, Value whichValue) {
        return skillValuesVersusType[(int)type, (int)whichValue];
    }

    public static int GetAbsolutPassiveAgainstType(Unit.Type type, Value whichValue) {
        return skillValuesVersusTypeAbsolut[(int)type, (int)whichValue];
    }

    //Passiv Buffs & Nerfs against enemys with specific armor

    public static void SetPassiveAgainstArmortype(Unit.ArmorType type, Value whichValue, float valueToSetTo) {
        skillValuesVersusArmortype[(int)type, (int)whichValue] = valueToSetTo;
    }

    public static void SetAbsolutPassiveAgainstArmortype(Unit.ArmorType type, Value whichValue, int valueToSetTo) {
        skillValuesVersusArmortypeAbsolut[(int)type, (int)whichValue] = valueToSetTo;
    }

    public static float GetPassiveAgainstArmortype(Unit.ArmorType type, Value whichValue) {
        return skillValuesVersusArmortype[(int)type, (int)whichValue];
    }

    public static int GetAbsolutPassiveAgainstArmortype(Unit.ArmorType type, Value whichValue) {
        return skillValuesVersusArmortypeAbsolut[(int)type, (int)whichValue];
    }

}