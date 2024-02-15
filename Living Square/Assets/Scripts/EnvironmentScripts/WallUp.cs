using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallUp : MonoBehaviour
{
    public int WallID;
    private Vector3 originalPos;
    private float targetPosActivated;
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        targetPosActivated = originalPos.y + 6;
    }

    public void OnPlateTrigger(Component invoker, object arg)
    {
        if (invoker is PressurePlate && arg is bool)
        {
            //check if ids match
            if (((PressurePlate)invoker).PlateID == WallID)
            {
                up();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void up()
    {
        Vector3 pos = transform.position;
        pos.y += speed * Time.deltaTime;
        transform.position = pos;
        if (transform.position.y <= targetPosActivated)
        {
            up();
        }
    }

    void down()
    {
        Vector3 pos = transform.position;
        pos.y -= speed * Time.deltaTime;
        transform.position = pos;
        if (transform.position.y >= targetPosActivated)
        {
            down();
        }
    }
}
