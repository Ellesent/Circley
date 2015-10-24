using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject target;
    Vector3 pos;
    Vector2 targetPos; 
   
	// Use this for initialization
	void Start () {

        targetPos = target.transform.position;

        //holds the camera's position in a variable
        pos = transform.position;

        //only set pillar boxes and check for ratio if on a mobile device - will most likely be taken out later
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            // set the desired aspect ratio - 9:16 or portrait mode on a standard phone
            //IMPORTANT - landscape mode will not be used
            float targetaspect = 9.0f / 16.0f;

            // determine the game window's current aspect ratio
            float windowaspect = (float)Screen.width / (float)Screen.height;

            // current viewport height should be scaled by this amount
            float scaleheight = windowaspect / targetaspect;

            // obtain camera component so we can modify its viewport
            Camera camera = GetComponent<Camera>();

            // if scaled height is less than current height, add letterbox
            if (scaleheight < 1.0f)
            {
                Rect rect = camera.rect;

                rect.width = 1.0f;
                rect.height = scaleheight;
                rect.x = 0;
                rect.y = (1.0f - scaleheight) / 2.0f;

                camera.rect = rect;
            }
            else // add pillarbox
            {
                float scalewidth = 1.0f / scaleheight;

                Rect rect = camera.rect;

                rect.width = scalewidth;
                rect.height = 1.0f;
                rect.x = (1.0f - scalewidth) / 2.0f;
                rect.y = 0;

                camera.rect = rect;
            }
        }
    

	}
	
	// Update is called once per frame
	void Update () {

        //transfer where the player is in relation to the camera into screen space/pixels so we can compare the screen dimensions to player's position
        Vector3 blah = GetComponent<Camera>().WorldToScreenPoint(target.transform.position);

        //if the player goes past the screen height
        if (blah.y > Screen.height)
        {
            //change the position of the camera to where the player is smoothly (not smooth yet haha)
            pos.y += (target.transform.position.y +500) * Time.deltaTime;
            //Vector2.Lerp(transform.position, new Vector2(0,target.transform.position.y + 500), 0.5f);
           
        }
            //if the player ball gets below a certain point (in the global coordinate space, not the screen space)
        else if (target.transform.position.y < -4)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
            //if the player ball gets below the screen move the camera
        else if (blah.y < 0)
        {
                pos.y += (target.transform.position.y - 500) * Time.deltaTime;
        }

        //put the variable back into the camera's position
        transform.position = pos;
        
	}

}
