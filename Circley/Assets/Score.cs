using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    public GameObject textGameObject;
    public static bool drawShop;
    Text component;

	// Use this for initialization
	void Start () {
        component = textGameObject.GetComponent<Text>();
        drawShop = false;
	}
	
	// Update is called once per frame
	void Update () {


        component.text = "Collected: " + Player.collectableNumber;
	}

    public void OnLoadClick()
    {
        Time.timeScale = 0;
        drawShop = true; 
    }
}
