using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    //how fast the player ball goes when space is pressed - this will be changed depending on how fast the circle it is rotating is going
    float speed = 11;
    float rotParent;

    //used to increase bounce
    float bounce = 9;
    
    //should the player rotate
    bool rotate;

    //can the player press space and shoot
    bool canShoot;

    //used to count how many collectables have been collected
   public static int collectableNumber;

   static bool touchForGUI = true;
   static bool spikeForGUI = false;
   public static bool winMenu;

   Vector2 pos; 

 

	// Use this for initialization
	void Start () {
        winMenu = false;
        //player can start off shooting
        canShoot = true;
        collectableNumber = 0;
        rotate = false;
        //used for trail rendering
        GetComponent<TrailRenderer>().sortingLayerName = "background";
        GetComponent<TrailRenderer>().sortingOrder = -2;
        pos = transform.position;
        Time.timeScale = 1;
        

	}
	
	// Update is called once per frame
	void Update () {

        
            //if space is pressed and the player can shoot
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount == 1 && canShoot == true)
            {
                //set parent to null (not attached to anything), Add a force to the rigidbody, and turn on gravity
                GetComponent<Transform>().parent = null;
                GetComponent<Rigidbody2D>().gravityScale = 1;
                GetComponent<Rigidbody2D>().AddForce(this.transform.up * speed, ForceMode2D.Impulse);
                

                //rotate is true and canshoot is false while the ball is in the air
               // rotate = true;
                canShoot = false;
                if (touchForGUI == true)
                {
                    touchForGUI = false;
                    spikeForGUI = true;
                }

            }
        }
            //ONLY FOR TESTING PURPOSES - WILL BE DELETED IN FINAL VERSION
        else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            if (Input.GetKeyDown("space") && canShoot == true)
            {
                

                //set parent to null (not attached to anything), Add a force to the rigidbody, and turn on gravity
                GetComponent<Transform>().parent = null;
                GetComponent<Rigidbody2D>().AddForce(this.transform.up * speed, ForceMode2D.Impulse);
                GetComponent<Rigidbody2D>().gravityScale = 1;

                //rotate is true and canshoot is false while the ball is in the air
                //rotate = true;
                canShoot = false;
                if (touchForGUI == true)
                {
                    touchForGUI = false;
                    spikeForGUI = true;
                }
                

            }
        }
        
        

        //if rotate is true, rotate the z axis
        //if (rotate == true)
        //{
        //    this.transform.Rotate(0, 0, 5f);
        //}
        //else
        //{
        //    this.transform.Rotate(0, 0, 0);
        //}

        if (spikeForGUI == true && transform.parent != null)
        {
            if (transform.parent.name == "Circle1Spike")
            {
                spikeForGUI = false; 
            }
        }
	}


    //This method is used for colliders on objects that are set to onTrigger (without onTrigger, colliders would hit and bounce off of each other like a normal collision)
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Speed")
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        }
        if (col.gameObject.tag == "Win")
        {
            transform.Rotate(new Vector3(0, 0, 0));
            Time.timeScale = 0;
            winMenu = true;
            rotate = false;
        }
        if (col.gameObject.tag == "Collectable")
        {
            collectableNumber += 1;
            Destroy(col.gameObject);
            //Debug.Log("Hit");
        }
        //if the player ball collides with a circle
        if (col.gameObject.tag == "CircleClockwise" || col.gameObject.tag == "CircleCounterClockwise")
        {
            
            
            //turn off gravity and set the parent of the player ball to the circle they collided with
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Transform>().SetParent(col.transform);
            //rotParent = Mathf.Abs(GetComponentInParent<CircleRotation>().rotSpeed);

            //the player can shoot again, but should stop rotating
            canShoot = true;
            rotate = false;

            //set the velocity of the player to be 0 so the player doesn't move through the collision
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            //math that will be used to face the player ball in the right direction while rotating around a circle
            Vector3 dir = col.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
             
            //set the player ball to look left or right depending on if the circle is clockwise or counterclockwise. The variable "angle" is most important here
            if (col.gameObject.tag == "CircleCounterClockwise")
            {
                transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
            }
            else if (col.gameObject.tag == "CircleClockwise")
            {
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            }
        }
             
        //if the player collides with a spike, reset the level
        if (col.gameObject.tag == "Spike")
        {
            Application.LoadLevel(Application.loadedLevel);
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "BouncyWall")
        {
            rotate = false;
            if (GetComponent<Rigidbody2D>().velocity.y > 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-bounce, bounce);
            }
            else if(GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-bounce, -bounce);
            }
        }
        if (col.gameObject.tag == "Wall")
        {
            rotate = false;
        }
    }

    void OnGUI()
    {
        //style variable used to change font, color, etc of labels. 
        GUIStyle style = new GUIStyle();
        style.fontSize = 50;


       // float time = Time.timeSinceLevelLoad; 
        
        //GUI.Label(new Rect(Screen.width * 0.1f, Screen.height - 200, Screen.width / 8.0f, Screen.height / 8.0f), time.ToString());

        //if this variable is true, show this label (when this variable is false the label will automatically go away) - these labels are used for instructions
        if (touchForGUI == true && Application.loadedLevel == 2)
        {
            GUI.Label(new Rect((Screen.width - 500) / 2.0f, Screen.height * 0.9f, Screen.width / 8.0f, Screen.height / 8.0f), "Tap the screen to shoot the ball.", style);
        }

        if (spikeForGUI == true && Application.loadedLevel == 2)
        {
            GUI.Label(new Rect(Screen.width * 0.7f, Screen.height / 2.0f, Screen.width / 8.0f, Screen.height / 8.0f), "Don't hit spikes!", style);
        }
    }

}
