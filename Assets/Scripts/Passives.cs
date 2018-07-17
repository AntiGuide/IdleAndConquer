using UnityEngine;

/// <summary>
/// This class handles the passives (of generals etc.)
/// </summary>
public class Passives : MonoBehaviour {
    /// <summary>Bonus on single stat values (percentage)</summary>
    private static readonly float[,] skillValues = { { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f },
                                                            { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f },
                                                            { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f },
                                                            { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f } };

    /// <summary>Bonus on single stat values (absolut)</summary>
    private static readonly int[,] skillValuesAbsolut = { { 0, 0, 0, 0, 0, 0 },
                                                                { 0, 0, 0, 0, 0, 0 },
                                                                { 0, 0, 0, 0, 0, 0 },
                                                                { 0, 0, 0, 0, 0, 0 } };

    /// <summary>Bonus on stat values for specific armor type (percentage)</summary>
    private static readonly float[,] skillValuesArmortype = { { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f },
                                                            { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f },
                                                            { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f },
                                                            { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f } };

    /// <summary>Bonus on stat values for specific armor type (absolut)</summary>
    private static readonly int[,] skillValuesAbsolutArmortype = { { 0, 0, 0, 0, 0, 0 },
                                                                { 0, 0, 0, 0, 0, 0 },
                                                                { 0, 0, 0, 0, 0, 0 },
                                                                { 0, 0, 0, 0, 0, 0 } };

    /// <summary>Bonus on stat values versus specific unit type (percentage)</summary>
    private static readonly float[,] skillValuesVersusType = { { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f },
                                                                    { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f },
                                                                    { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f },
                                                                    { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f } };

    /// <summary>Bonus on stat values versus specific unit type (absolut)</summary>
    private static readonly int[,] skillValuesVersusTypeAbsolut = { { 0, 0, 0, 0, 0, 0 },
                                                                    { 0, 0, 0, 0, 0, 0 },
                                                                    { 0, 0, 0, 0, 0, 0 },
                                                                    { 0, 0, 0, 0, 0, 0 } };

    /// <summary>Bonus on stat values versus specific armor type (percentage)</summary>
    private static readonly float[,] skillValuesVersusArmortype = { { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f },
                                                                    { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f },
                                                                    { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f },
                                                                    { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f } };

    /// <summary>Bonus on stat values versus specific armor type (absolut)</summary>
    private static readonly int[,] skillValuesVersusArmortypeAbsolut = { { 0, 0, 0, 0, 0, 0 },
                                                                    { 0, 0, 0, 0, 0, 0 },
                                                                    { 0, 0, 0, 0, 0, 0 },
                                                                    { 0, 0, 0, 0, 0, 0 } };

    static Passives() {
        GeneralSurvivability = 0.0f;
    }

    /// <summary>The different value categories</summary>
    public enum Value {
        HP = 0,
        ATTACK,
        CRITICAL,
        DEFENSE,
        COST,
        BUILDTIME
    }

    /// <summary>Getter/setter for generalSurvivability</summary>
    public static float GeneralSurvivability { get; private set; }

    // Passiv Buffs & Nerfs

    /// <summary>
    /// Sets Bonus stat for single unit (percentage)
    /// </summary>
    /// <param name="type">Type of the unit to grant the bonus to</param>
    /// <param name="whichValue">Which value to apply the buff to</param>
    /// <param name="valueToSetTo">Value to set the bonus to (1.0f means no buff or nerf)</param>
    public static void SetPassive(Unit.Type type, Value whichValue, float valueToSetTo) {
        skillValues[(int)type, (int)whichValue] = valueToSetTo;
    }

    /// <summary>
    /// Sets Bonus stat for single unit (absolut)
    /// </summary>
    /// <param name="type">Type of the unit to grant the bonus to</param>
    /// <param name="whichValue">Which value to apply the buff to</param>
    /// <param name="valueToSetTo">Value to set the bonus to (0 means no buff or nerf)</param>
    public static void SetAbsolutPassive(Unit.Type type, Value whichValue, int valueToSetTo) {
        skillValuesAbsolut[(int)type, (int)whichValue] = valueToSetTo;
    }

    /// <summary>
    /// Gets Bonus stat for single unit (percentage)
    /// </summary>
    /// <param name="type">The type of the unit to get the effect for</param>
    /// <param name="whichValue">Which value to get the effect for</param>
    /// <returns>Value of the bonus (1.0f means no buff or nerf)</returns>
    public static float GetPassive(Unit.Type type, Value whichValue) {
        return skillValues[(int)type, (int)whichValue];
    }

    /// <summary>
    /// Gets Bonus stat for single unit (absolut)
    /// </summary>
    /// <param name="type">The type of the unit to get the effect for</param>
    /// <param name="whichValue">Which value to get the effect for</param>
    /// <returns>Value of the bonus (0 means no buff or nerf)</returns>
    public static int GetAbsolutPassive(Unit.Type type, Value whichValue) {
        return skillValuesAbsolut[(int)type, (int)whichValue];
    }

    // Passiv Buffs & Nerfs for specific ArmorTypes

    /// <summary>
    /// Sets Bonus stat for specific armor type (percentage)
    /// </summary>
    /// <param name="type">Type of the armor to grant the bonus to</param>
    /// <param name="whichValue">Which value to apply the buff to</param>
    /// <param name="valueToSetTo">Value to set the bonus to (1.0f means no buff or nerf)</param>
    public static void SetPassiveArmortype(Unit.ArmorType type, Value whichValue, float valueToSetTo) {
        skillValuesArmortype[(int)type, (int)whichValue] = valueToSetTo;
    }

    /// <summary>
    /// Sets Bonus stat for specific armor type (absolut)
    /// </summary>
    /// <param name="type">Type of the armor to grant the bonus to</param>
    /// <param name="whichValue">Which value to apply the buff to</param>
    /// <param name="valueToSetTo">Value to set the bonus to (0 means no buff or nerf)</param>
    public static void SetAbsolutPassiveArmortype(Unit.ArmorType type, Value whichValue, int valueToSetTo) {
        skillValuesAbsolutArmortype[(int)type, (int)whichValue] = valueToSetTo;
    }

    /// <summary>
    /// Gets Bonus stat for specific armor type (percentage)
    /// </summary>
    /// <param name="type">The type of the armor to get the effect for</param>
    /// <param name="whichValue">Which value to get the effect for</param>
    /// <returns>Value of the bonus (1.0f means no buff or nerf)</returns>
    public static float GetPassiveArmortype(Unit.ArmorType type, Value whichValue) {
        return skillValuesArmortype[(int)type, (int)whichValue];
    }

    /// <summary>
    /// Gets Bonus stat for specific armor type (absolut)
    /// </summary>
    /// <param name="type">The type of the armor to get the effect for</param>
    /// <param name="whichValue">Which value to get the effect for</param>
    /// <returns>Value of the bonus (0 means no buff or nerf)</returns>
    public static int GetAbsolutPassiveArmortype(Unit.ArmorType type, Value whichValue) {
        return skillValuesAbsolutArmortype[(int)type, (int)whichValue];
    }

    // Passiv Buffs & Nerfs against specific types of enemys

    /// <summary>
    /// Sets Bonus stat against a specific unit type (percentage)
    /// </summary>
    /// <param name="type">Type of the unit against which the bonus will be granted</param>
    /// <param name="whichValue">Which value to apply the buff to</param>
    /// <param name="valueToSetTo">Value to set the bonus to (1.0f means no buff or nerf)</param>
    public static void SetPassiveAgainstType(Unit.Type type, Value whichValue, float valueToSetTo) {
        skillValuesVersusType[(int)type, (int)whichValue] = valueToSetTo;
    }

    /// <summary>
    /// Sets Bonus stat against a specific unit type (absolut)
    /// </summary>
    /// <param name="type">Type of the unit against which the bonus will be granted</param>
    /// <param name="whichValue">Which value to apply the buff to</param>
    /// <param name="valueToSetTo">Value to set the bonus to (0 means no buff or nerf)</param>
    public static void SetAbsolutPassiveAgainstType(Unit.Type type, Value whichValue, int valueToSetTo) {
        skillValuesVersusTypeAbsolut[(int)type, (int)whichValue] = valueToSetTo;
    }

    /// <summary>
    /// Gets Bonus stat against a specific unit type (percentage)
    /// </summary>
    /// <param name="type">Type of the unit against which the bonus will be granted</param>
    /// <param name="whichValue">Which value to apply the buff to</param>
    /// <returns>Value of the bonus (1.0f means no buff or nerf)</returns>
    public static float GetPassiveAgainstType(Unit.Type type, Value whichValue) {
        return skillValuesVersusType[(int)type, (int)whichValue];
    }

    /// <summary>
    /// Gets Bonus stat against a specific unit type (absolut)
    /// </summary>
    /// <param name="type">Type of the unit against which the bonus will be granted</param>
    /// <param name="whichValue">Which value to apply the buff to</param>
    /// <returns>Value of the bonus (0 means no buff or nerf)</returns>
    public static int GetAbsolutPassiveAgainstType(Unit.Type type, Value whichValue) {
        return skillValuesVersusTypeAbsolut[(int)type, (int)whichValue];
    }

    // Passiv Buffs & Nerfs against enemys with specific armor

    /// <summary>
    /// Sets Bonus stat against a specific armor type (percentage)
    /// </summary>
    /// <param name="type">Armortype of the unit against which the bonus will be granted</param>
    /// <param name="whichValue">Which value to apply the buff to</param>
    /// <param name="valueToSetTo">Value to set the bonus to (1.0f means no buff or nerf)</param>
    public static void SetPassiveAgainstArmortype(Unit.ArmorType type, Value whichValue, float valueToSetTo) {
        skillValuesVersusArmortype[(int)type, (int)whichValue] = valueToSetTo;
    }

    /// <summary>
    /// Sets Bonus stat against a specific armor type (absolut)
    /// </summary>
    /// <param name="type">Armortype of the unit against which the bonus will be granted</param>
    /// <param name="whichValue">Which value to apply the buff to</param>
    /// <param name="valueToSetTo">Value to set the bonus to (0 means no buff or nerf)</param>
    public static void SetAbsolutPassiveAgainstArmortype(Unit.ArmorType type, Value whichValue, int valueToSetTo) {
        skillValuesVersusArmortypeAbsolut[(int)type, (int)whichValue] = valueToSetTo;
    }

    /// <summary>
    /// Gets Bonus stat against a specific armor type (percentage)
    /// </summary>
    /// <param name="type">Armortype of the unit against which the bonus will be granted</param>
    /// <param name="whichValue">Which value to apply the buff to</param>
    /// <returns>Value of the bonus (1.0f means no buff or nerf)</returns>
    public static float GetPassiveAgainstArmortype(Unit.ArmorType type, Value whichValue) {
        return skillValuesVersusArmortype[(int)type, (int)whichValue];
    }

    /// <summary>
    /// Gets Bonus stat against a specific armor type (absolut)
    /// </summary>
    /// <param name="type">Armortype of the unit against which the bonus will be granted</param>
    /// <param name="whichValue">Which value to apply the buff to</param>
    /// <returns>Value of the bonus (0 means no buff or nerf)</returns>
    public static int GetAbsolutPassiveAgainstArmortype(Unit.ArmorType type, Value whichValue) {
        return skillValuesVersusArmortypeAbsolut[(int)type, (int)whichValue];
    }
}