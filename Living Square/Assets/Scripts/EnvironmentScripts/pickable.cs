using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickable : MonoBehaviour
{
	private Vector3 spawn;
	private Rigidbody rb; 
	public int num;
    // Start is called before the first frame update
    void Start()
    {
        spawn = gameObject.transform.position;
		rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void respawn()
	{
		rb.velocity = new Vector3(0,0,0);
		rb.position = spawn;
	}
}
