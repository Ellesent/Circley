using UnityEngine;
using System.Collections;

public class CircleRotation : MonoBehaviour {

    //rotation variable for circles - can be changed in the inspector
    public float rotSpeed = -2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //rotate the z axis of the circle
        this.transform.Rotate(0, 0, rotSpeed); 
	}
}
