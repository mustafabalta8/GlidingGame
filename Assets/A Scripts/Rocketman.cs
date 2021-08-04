using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocketman : MonoBehaviour
{
    [SerializeField] GameObject rocketmanCamera;
    [SerializeField] GameObject StickTopPos;
    [SerializeField] GameObject stickRot;
    Vector3 StickToRocatmanVector;

    [SerializeField] float MoveSpeedZ, MoveSpeedY,MoveSpeedX;
    bool HasLaunch;

    Quaternion BaseRotation;
    //Rigidbody rg;
    Animator animator;

    [SerializeField] float mousePosInUnits;
    float MousePosAtStart;
    // Start is called before the first frame update
    void Start()
    {
        MoveSpeedX = 0;
        BaseRotation = transform.rotation;
        HasLaunch = false;
        StickToRocatmanVector = transform.position - StickTopPos.transform.position;

        animator = GetComponent<Animator>();
        //rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (HasLaunch==false)
        {
            LockToTheStick();
        }
        else
        {
            MoveSpeedZ -= (0.004f* Time.deltaTime*5);
            transform.Translate(MoveSpeedX, MoveSpeedY, MoveSpeedZ, Space.World);
            //rg.AddForce(MoveSpeedX, MoveSpeedY, MoveSpeedZ);
            mousePosInUnits = Input.mousePosition.x / Screen.width * 18;
            ManageWingStates();

            
            
        }

        
    }

    void ManageWingStates()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MousePosAtStart = (int)mousePosInUnits;
        }
        if (Input.GetMouseButton(0))
        {
            animator.SetBool("Open", true);
            transform.rotation = BaseRotation;
            if(MousePosAtStart < mousePosInUnits)
            {
                MoveSpeedX = 0.05f;
            }
            else
            {
                MoveSpeedX = -0.05f;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            animator.speed = 1;
            animator.SetBool("Open", false);
            animator.SetTrigger("CloseWing");
            MoveSpeedX = 0;

        }
        else
        {
            transform.Rotate(5, 0, 0);
            MoveSpeedX = 0;
        }
        
    }
    void LockToTheStick()
    {
        Vector3 stickPos = new Vector3(StickTopPos.transform.position.x, StickTopPos.transform.position.y, StickTopPos.transform.position.z);
        transform.position = stickPos + StickToRocatmanVector;

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
            MoveSpeedZ = 0;
            MoveSpeedY = 0;
        }
    }
    public void OpenWingState()
    {
        animator.speed = 0;
    }
}
