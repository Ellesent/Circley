using UnityEngine;
using System.Collections;

public class EndMenu : MonoBehaviour {
    public int amountToCollect; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.skin.box.fontSize = 50;
        GUIStyle style1 = new GUIStyle();
        style1.fontSize = 80;
        style1.fontStyle = FontStyle.Bold; 
        GUI.depth = -2;
        GUI.skin.button.fontSize = 80;
        
       if (Time.timeScale == 0 && Player.winMenu == true)
        {
            //Debug.Log("yes");
            GUI.Box(new Rect(Screen.width/13,Screen.height / 4, Screen.width/ 1.2f ,Screen.height / 2), "Level Completed");
            GUI.Label(new Rect(Screen.width / 6, Screen.height / 3, 50, 50), "You collected " + Player.collectableNumber + " out of " + amountToCollect, style1);
            if (GUI.Button(new Rect(Screen.width / 3, Screen.height / 2.5f, 500,500), "Next Level?"))
            {
                Application.LoadLevel(Application.loadedLevel + 1);
            }
       }
    }

    
}
