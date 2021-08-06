using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deletePassedObjs : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider otherCollider)
    {
        //Debug.Log("collision with DeleteObjectsBehind");
        if (otherCollider.gameObject.tag == "Jumper")
        {
            //Debug.Log("collision with DeleteObjectsBehind");
            Destroy(otherCollider.gameObject,0.5f);
        }
    }
    
}
