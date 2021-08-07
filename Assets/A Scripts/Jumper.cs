using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] float upwardMove, forwardMove;
    private void OnCollisionEnter(Collision otherCollider)
    {
              
        if (otherCollider.gameObject.tag == "Player")
        {
           Rocketman rocketman= otherCollider.gameObject.GetComponent<Rocketman>();
            rocketman.SetMoveVektor(upwardMove, forwardMove);

        }
        
    }
    
}
