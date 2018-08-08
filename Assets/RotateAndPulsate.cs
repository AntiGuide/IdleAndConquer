using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateAndPulsate : MonoBehaviour {
    [Header("Values")]
    [SerializeField] private float rotationSpeed = 15.0f;
    [SerializeField] private float pulsationSpeed = 1.0f;
    [SerializeField] private LootBoxStackManager.LootboxType type;
    [SerializeField] private uint id;

    [Header("References")]
    [SerializeField] private Text countText;
    [SerializeField] private GameObject[] objectsToHide;
    [SerializeField] private Transform transformCanvas;
    [SerializeField] private GameObject[] lootboxRewardPrefabs;
    [SerializeField] private BlueprintManager blueprintMan;

    private int count = 0;

	// Update is called once per frame
	void Update () {
	    transform.Rotate(new Vector3(0f, 0f, rotationSpeed * Time.deltaTime));
	    var scaleVal = 0.4f + Mathf.PingPong(Time.time * pulsationSpeed, 0.1f);
        transform.localScale = new Vector3(scaleVal, scaleVal, scaleVal);
    }


    public void AddBox(int countAdd = 1) {
        count += countAdd;
        count = count < 0 ? 0 : count;
        PlayerPrefs.SetInt("LootboxButtons_" + id, count);
        Output();
    }

    private void Output() {
        if (count >= 10) {
            countText.text = "9+";
        } else {
            countText.text = count.ToString();
        }

        if (count <= 0) {
            foreach (var o in objectsToHide) {
                o.SetActive(false);
            }
        } else {
            foreach (var o in objectsToHide) {
                o.SetActive(true);
            }
        }
    }

    public void OnClick() {
        if (count <= 0) {
            return;
        }

        var go = Instantiate(this.lootboxRewardPrefabs[(int)type], this.transformCanvas);
        var comp = go.GetComponent<LootGenerator>();
        comp.InstantiateLootbox(type, blueprintMan);
        AddBox(-1);
    }


    public void StartUp() {
        count = PlayerPrefs.GetInt("LootboxButtons_" + id, 0);
        count = 5;
        Output();
    }
}
