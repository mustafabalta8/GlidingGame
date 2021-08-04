using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocketman : MonoBehaviour
{
    [SerializeField] GameObject rocketmanCamera;
    [SerializeField] GameObject StickTopPos;
    [SerializeField] GameObject stickRot;
    Vector3 StickToRocatmanVector;

    [SerializeField] float MoveSpeedZ, MoveSpeedY;
    bool HasLaunch;

    Quaternion BaseRotation;
    // Start is called before the first frame update
    void Start()
    {
        BaseRotation = transform.rotation;
        HasLaunch = false;
        StickToRocatmanVector = transform.position - StickTopPos.transform.position;

        
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
            transform.Translate(0, MoveSpeedY, MoveSpeedZ);
        }

        
    }
    private void LockToTheStick()
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
}
