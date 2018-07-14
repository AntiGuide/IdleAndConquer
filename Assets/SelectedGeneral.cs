using UnityEngine;

public class SelectedGeneral : MonoBehaviour {
    private static General general;

    public static General General {
        get { return general; }
        set { general = value; }
    }
}
