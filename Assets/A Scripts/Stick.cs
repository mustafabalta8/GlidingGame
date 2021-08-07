using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stick : MonoBehaviour
{
    [SerializeField] float mousePosInUnits;
    [SerializeField] int mousePosInUnitsINT;
   // [SerializeField]int MousePosAtBendAnimStart;

    Animator animator;
    Rocketman rocketman;

    int FrameRate;
    int FrameRateWhereRelease;

    [SerializeField] Text ShowPower;

    void Start()
    {
        animator = GetComponent<Animator>();
        rocketman = FindObjectOfType<Rocketman>();
    }

    public void Update()
    {

        if (Input.GetMouseButton(0))
        {
            animator.SetTrigger("BendStick");
        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetTrigger("ReleaseStick");
            FrameRateWhereRelease = FrameRate;
        }           
    }
    public void LaunchRocketman()//release animation event
    {
     
        rocketman.SetHasLaunch(FrameRateWhereRelease);

        Destroy(ShowPower.gameObject);
        Destroy(gameObject, 1f);
    }

    public void GetBendStateFrame(int frameRate)
    {
        FrameRate = frameRate;
        //StartCoroutine(CheckFrameRate(frameRate));
        ShowPower.text = "Power: " + frameRate;

    }
   
   
}
