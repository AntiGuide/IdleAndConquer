using UnityEngine;
using UnityEngine.UI;

public class ShowFPS : MonoBehaviour {
    // Update is called once per frame
    private void Update() {
        GetComponent<Text>().text = Mathf.RoundToInt(1.0f / Time.deltaTime) + " FPS";
    }
}