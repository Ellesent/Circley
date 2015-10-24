using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
    public int level; 

	// Use this for initialization
	public void OnClickLoad()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }

    public void OnClickLoadLevel()
    {
        Application.LoadLevel(level);
    }
}
