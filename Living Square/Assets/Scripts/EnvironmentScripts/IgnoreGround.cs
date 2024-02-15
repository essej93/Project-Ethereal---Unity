using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreGround : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GameObject ground;
        ground = GameObject.Find("Ground");
        MeshCollider groundCollider = ground.GetComponent<MeshCollider>();
        Physics.IgnoreCollision(groundCollider, GetComponent<BoxCollider>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
