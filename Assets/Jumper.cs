using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision otherCollider)
    {
        /*
        ///Debug.Log("jumper collision �al��t�");
        if (otherCollider.gameObject.tag == "Player")
        {
            otherCollider.transform.position +=  new Vector3(0, 15, 0);
            Debug.Log("z�plama �al��t�");
        }*/
    }
}
