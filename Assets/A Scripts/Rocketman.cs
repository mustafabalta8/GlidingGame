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
    Score score;

    [SerializeField] float mousePosInUnits;
    //float MousePosAtStart;
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
        // rg = GetComponent<Rigidbody>();
        //rg.AddForce(MoveSpeedX*100, MoveSpeedY*100, MoveSpeedZ*20);
        //rg.velocity = new Vector3(MoveSpeedX * 100, MoveSpeedY * 150, MoveSpeedZ * 50);
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
            MoveSpeedY -= (MultiplierOfFallSpeed * Time.deltaTime);
            MoveSpeedZ -= (0.01f * Time.deltaTime * 3);

            Vector3 MoveVek = new Vector3(MoveSpeedX, MoveSpeedY, MoveSpeedZ);
            transform.Translate(MoveVek, Space.World);        

            mousePosInUnits = Input.mousePosition.x / Screen.width * 18;
            ManageWingStates();           
        }
    }
    void ManageWingStates()
    {        
        if (Input.GetMouseButton(0))
        {
            animator.SetBool("Open", true);
            transform.rotation = BaseRotation;
            MultiplierOfFallSpeed = 0.000003f;
            //MoveSpeedY = -0.05f;

            HorizontalMove();
        }

        if (Input.GetMouseButtonUp(0))
        {
            MultiplierOfFallSpeed = 0.15f;
           // Debug.Log("GetMouseButtonUp worked");
            animator.speed = 1;// To make transition to close-wings animation
            animator.SetBool("Open", false);
            transform.rotation = BaseRotation;

        }      
        rotateForwardSpeed = 15;
        transform.Rotate(rotateForwardSpeed, 0, 0);
    }
    private void HorizontalMove()
    {
        if (mousePosInUnits > 9)//MousePosAtStart
        {  
            transform.Translate(0.6f, 0, 0);            
            transform.Rotate(0, 20, 0);
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
        MoveSpeedZ = moveSpeed/4;
        rocketmanCamera.SetActive(true);
    }
    private void OnCollisionEnter(Collision otherCollider)
    {
        if (otherCollider.gameObject.name == "Ground")
        {
            gameOver = true;
            GameOverPanel.SetActive(true);
            FinalScore.text = ""+score.GetScore();

        }
        if (otherCollider.gameObject.tag == "Jumper")
        {
            animator.speed = 1;
            animator.SetBool("Open", false);
            MultiplierOfFallSpeed = 0.12f;

            //MoveSpeedY = 0.35f;
            //MoveSpeedZ = 1.3f;
            Debug.Log("zýplama çalýþtý");
        }
    }
    public void SetMoveVektor(float upward, float forward)
    {
        MoveSpeedY = upward;
        MoveSpeedZ = forward;
    }
   
}
