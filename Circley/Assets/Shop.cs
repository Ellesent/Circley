using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour
{
    public Texture2D skip;
    public Texture2D redo;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        GUI.skin.box.fontSize = 50;
        GUIStyle style1 = new GUIStyle();
        style1.fontSize = 80;
        style1.fontStyle = FontStyle.Bold;
        GUI.depth = -2;
        GUI.skin.button.fontSize = 80;

        if (Score.drawShop == true)
        {
            GUI.Box(new Rect(Screen.width / 13, Screen.height / 4, Screen.width / 1.2f, Screen.height / 2), "Welcome to the Shop!");
            GUI.DrawTexture(new Rect(Screen.width / 10, Screen.height / 3, skip.width, skip.height), skip);
            GUI.DrawTexture(new Rect(Screen.width / 2, Screen.height / 3, redo.width, redo.height), redo);
            if (GUI.Button(new Rect(Screen.width / 6, Screen.height / 2, Screen.width / 4, Screen.width / 4), "Back"))
            {
                Score.drawShop = false;
                Time.timeScale = 1;
            }
        }

    }
}

