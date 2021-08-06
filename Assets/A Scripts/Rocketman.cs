using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rocketman : MonoBehaviour
{
    [SerializeField] GameObject rocketmanCamera;
    [SerializeField] GameObject StickTopPos;
    [SerializeField] GameObject stickRot;
    Vector3 StickToRocketmanVector;

    [SerializeField] float MoveSpeedZ, MoveSpeedY,MoveSpeedX;
    public bool HasLaunch, gameOver;

    Quaternion BaseRotation;
    Rigidbody rg;
    Animator animator;
    Spawner spawner;
    Score score;

    [SerializeField] float mousePosInUnits;
    float MousePosAtStart;
    float rotateForwardSpeed=15;
    [SerializeField] float MultiplierOfFallSpeed;


    [SerializeField] GameObject GameOverPanel;
    [SerializeField] Text FinalScore;
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
        score = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HasLaunch==false)
        {
            LockToTheStick();
        }
        if (HasLaunch == true && gameOver == false)
        {
            MoveSpeedY -= (0.007f * Time.deltaTime * MultiplierOfFallSpeed);
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
        //rotateForwardSpeed=15;       
        if (Input.GetMouseButtonDown(0))
        {
            MousePosAtStart = (int)mousePosInUnits;
            //Debug.Log("MousePosAtStart:"+ MousePosAtStart);
        }
        if (Input.GetMouseButton(0))
        {
            animator.SetBool("Open", true);
            transform.rotation = BaseRotation;
            MultiplierOfFallSpeed = 0.5f;
            MoveSpeedZ = 1f;

            HorizontalMove();
            //rotateForwardSpeed = 0;
        }
       if (Input.GetMouseButtonUp(0))
        {
            MultiplierOfFallSpeed = 20;
            MoveSpeedZ = 1.3f;
            Debug.Log("GetMouseButtonUp worked");
            animator.speed = 1;// To make transition to close-wings animation
            animator.SetBool("Open", false);
            //rotateForwardSpeed = 0;
            //  MoveSpeedX = 0;

            transform.rotation = BaseRotation;

        }
        
        rotateForwardSpeed = 15;
        transform.Rotate(rotateForwardSpeed, 0, 0);



    }
    private void HorizontalMove()
    {
        //Debug.Log("open wings state");
        if (mousePosInUnits > 9)//MousePosAtStart
        {  
            transform.Translate(0.6f, 0, 0);            
            transform.Rotate(0, 20, 0);
            //Debug.Log("mousePosInUnits > 9)");
        }
        else
        {
            transform.Translate(-0.6f, 0, 0);
            transform.Rotate(0, -20, 0);
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
        MoveSpeedZ = moveSpeed/5;
        rocketmanCamera.SetActive(true);
    }
    private void OnCollisionEnter(Collision otherCollider)
    {
        if (otherCollider.gameObject.name == "Ground")
        {
            gameOver = true;
            GameOverPanel.SetActive(true);
            FinalScore.text = "YOUR SCORE IS "+score.GetScore();

        }
        if (otherCollider.gameObject.tag == "Jumper")
        {
            animator.speed = 1;
            animator.SetBool("Open", false);
            MultiplierOfFallSpeed = 9;

            //MoveSpeedY = 0.35f;
            //MoveSpeedZ = 1.3f;
            Debug.Log("zýplama çalýþtý");
        }
    }
    public void WingState(int state)//Open wind animation event
    {
        animator.speed = state;
    }

    public void SetMoveVektor(float upward, float forward)
    {
        MoveSpeedY = upward;
        MoveSpeedZ = forward;
    }
   
}
