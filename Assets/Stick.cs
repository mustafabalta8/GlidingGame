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
    int FrameRateWhereDrop;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rocketman = FindObjectOfType<Rocketman>();
    }

    // Update is called once per frame
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
            FrameRateWhereDrop = FrameRate;


        }


        if (FrameRate == MousePosAtBendAnimStart - (int)mousePosInUnits)
        {
            animator.speed = 0;
            //Debug.Log("animator speed 000");
        }
        if (FrameRate != MousePosAtBendAnimStart - (int)mousePosInUnits)
        {
            isPause = false;
            animator.speed = 1;
            //Debug.Log("animator speed 111");
        }
            

    }
    public void LaunchRocketman()
    {
        Debug.Log("frame:" + FrameRateWhereDrop);
        rocketman.SetHasLaunch(FrameRateWhereDrop);

    }

    public void GetBendStateFrame(int frameRate)
    {
        StartCoroutine(CheckFrameRate(frameRate));
        /*
        isPause = true;
        Debug.Log("frame rate " + frameRate);
        while (isPause==true)
        {
            Debug.Log("MousePosAtBendAnimStart: " + MousePosAtBendAnimStart);
            Debug.Log("mousePosInUnits: " + mousePosInUnits);
            Debug.Log("MousePosAtBendAnimStart - (int)mousePosInUnits: " + (MousePosAtBendAnimStart - (int)mousePosInUnits));
            if (frameRate == MousePosAtBendAnimStart - (int)mousePosInUnits)
            {
                animator.speed = 0;
                Debug.Log("animator speed 000");
            }
            else
            {
                Debug.Log("animator speed 111");
                animator.speed = 1;
                isPause = false;
                Debug.Log("isPause false");
            }
        }*/

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
