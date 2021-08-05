using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocketman : MonoBehaviour
{
    [SerializeField] GameObject rocketmanCamera;
    [SerializeField] GameObject StickTopPos;
    [SerializeField] GameObject stickRot;
    Vector3 StickToRocketmanVector;

    [SerializeField] float MoveSpeedZ, MoveSpeedY,MoveSpeedX;
    bool HasLaunch, gameOver;

    Quaternion BaseRotation;
    Rigidbody rg;
    Animator animator;
    Spawner spawner;

    [SerializeField] float mousePosInUnits;
    float MousePosAtStart;
    // Start is called before the first frame update
    void Start()
    {
        MoveSpeedX = 0;
        BaseRotation = transform.rotation;
        HasLaunch = false;
        gameOver = false;
        StickToRocketmanVector = transform.position - StickTopPos.transform.position;

        animator = GetComponent<Animator>();
        spawner = GetComponent<Spawner>();
        rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HasLaunch==false)
        {
            LockToTheStick();
        }
        
    }
    private void FixedUpdate()
    {
        if (HasLaunch == true && gameOver ==false)
        {
            MoveSpeedY -= (0.01f * Time.deltaTime * 6);
            MoveSpeedZ -= (0.01f * Time.deltaTime * 3);

            Vector3 MoveVek = new Vector3(MoveSpeedX, MoveSpeedY, MoveSpeedZ);
            transform.Translate(MoveVek, Space.World);

            //rg.AddForce(MoveSpeedX*100, MoveSpeedY*100, MoveSpeedZ*20);
            //rg.velocity = new Vector3(MoveSpeedX * 100, MoveSpeedY * 150, MoveSpeedZ * 50);

            mousePosInUnits = Input.mousePosition.x / Screen.width * 18;

            //spawner.ZPositionAmountOfRise = 1; 
            
            ManageWingStates();
        }
    }

    void ManageWingStates()
    {
        //TODO: increase drag while open wings
        if (Input.GetMouseButtonDown(0))
        {
            MousePosAtStart = (int)mousePosInUnits;
            Debug.Log("MousePosAtStart:"+ MousePosAtStart);
        }
        if (Input.GetMouseButton(0))
        {
            animator.SetBool("Open", true);
            transform.rotation = BaseRotation;

            //Debug.Log("open wings state");
            if(  mousePosInUnits>9)//MousePosAtStart
            {
                //MoveSpeedX = 0.5f;
                //Debug.Log(" MoveSpeedX = 0.5f;");
                transform.Translate(0.5f, 0, 0);
            }
            else
            {
                //MoveSpeedX = -0.5f;
                transform.Translate(-0.5f, 0, 0);
                //Debug.Log(" MoveSpeedX = minus;");
            }
            //
            //Invoke("SlowTime", 0.2f);
        }
        if (Input.GetMouseButtonUp(0))
        {
           
            Debug.Log("GetMouseButtonUp worked");
            animator.speed = 1;// To make transition to close-wings animation
            animator.SetBool("Open", false);
            animator.SetTrigger("CloseWing");
            MoveSpeedX = 0;
            
        }
        else
        {
            transform.Rotate(15, 0, 0);
            MoveSpeedX = 0;
        }
        
    }
    void LockToTheStick()
    {
        Vector3 stickPos = new Vector3(StickTopPos.transform.position.x, StickTopPos.transform.position.y, StickTopPos.transform.position.z);
        transform.position = stickPos + StickToRocketmanVector;

        Quaternion stickRotation = stickRot.transform.rotation;
        transform.rotation = stickRotation;
    }
    public void SetHasLaunch(float moveSpeed)
    {
        transform.rotation = BaseRotation;
        HasLaunch = true;
        //rg.AddForce()
        MoveSpeedZ = moveSpeed/7;

        rocketmanCamera.SetActive(true);
    }
    private void OnCollisionEnter(Collision otherCollider)
    {
        if (otherCollider.gameObject.name == "Ground")
        {
            gameOver = true;
            Debug.Log("Game Over");
            
        }
        if (otherCollider.gameObject.tag == "Jumper")
        {

            MoveSpeedY = 0.35f;
            MoveSpeedZ = 1.3f;
            Debug.Log("zýplama çalýþtý");
        }
    }
    public void OpenWingState()//Open wind animation event
    {
        animator.speed = 0;
    }
   
}
