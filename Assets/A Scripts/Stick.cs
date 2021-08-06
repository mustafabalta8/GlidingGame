using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    [SerializeField] float mousePosInUnits;
    [SerializeField] int mousePosInUnitsINT;
    [SerializeField]int MousePosAtBendAnimStart;

    bool isPause;

    Animator animator;
    Rocketman rocketman;

    int FrameRate;
    int FrameRateWhereRelease;

    void Start()
    {
        animator = GetComponent<Animator>();
        rocketman = FindObjectOfType<Rocketman>();
    }

    public void Update()
    {
        mousePosInUnits = Input.mousePosition.x / Screen.width * 18;
        //mousePosInUnitsINT = (int)mousePosInUnits;

        if (Input.GetMouseButtonDown(0))
        {
            MousePosAtBendAnimStart = (int)mousePosInUnits;
        }
        if (Input.GetMouseButton(0))
        {
            animator.SetTrigger("BendStick");
        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetTrigger("ReleaseStick");
            FrameRateWhereRelease = FrameRate;
        }

        /*  
        if (FrameRate == MousePosAtBendAnimStart - (int)mousePosInUnits)
        {
            animator.speed = 0;
        }
        if (FrameRate != MousePosAtBendAnimStart - (int)mousePosInUnits)
        {
            isPause = false;
            animator.speed = 1;
        }*/
            

    }
    public void LaunchRocketman()
    {
        Debug.Log("FrameRateWhereDrop:" + FrameRateWhereRelease);
        rocketman.SetHasLaunch(FrameRateWhereRelease);
       // rocketman.rg.velocity = new Vector3(0, 30, 0);
    }

    public void GetBendStateFrame(int frameRate)
    {
        FrameRate = frameRate;
        //StartCoroutine(CheckFrameRate(frameRate));
        

    }
    IEnumerator CheckFrameRate(int frameRate)
    {
        FrameRate = frameRate;
        Debug.Log("frame rate "+frameRate);
        isPause = true;
        /*
        Debug.Log("MousePosAtBendAnimStart: " + MousePosAtBendAnimStart);
        Debug.Log("mousePosInUnits: " + mousePosInUnits);
        Debug.Log("MousePosAtBendAnimStart - (int)mousePosInUnits: " + (MousePosAtBendAnimStart - (int)mousePosInUnits));

        if (frameRate == MousePosAtBendAnimStart - (int)mousePosInUnits)
        {
            animator.speed = 0;
            Debug.Log("animator speed 000");
        }
        else //(frameRate != MousePosAtBendAnimStart - (int)mousePosInUnits)
        {
            isPause = false;
            Debug.Log("isPause false");
        }*/
        yield return new WaitUntil(NoPause);       
    } 
    bool NoPause()
    {
        if (isPause == false)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
   
   
}
