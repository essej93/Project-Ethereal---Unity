using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateWall : MonoBehaviour
{
    /*==============================================================================
                                       VARIABLES
   ==============================================================================*/
    // Public variables
    public float speed = 10f;
    [HideInInspector] public bool isFullDown = false, isFullUp = true;


    // Flags
    private bool isDownDirection = true, isTransitioning = false;

    // internal variables
    private Vector3 originalPos;
    private float targetPosActivated;
    public GameObject anInteractObj;
    public int PlateID;

    /*==============================================================================
                                        START
    ==============================================================================*/
    // Start is called before the first frame update
    void Start()
    {
        originalPos = anInteractObj.transform.position;
        targetPosActivated = originalPos.y - 6;
    }


    /*==============================================================================
                                        UPDATE
    ==============================================================================*/
    // Update is called once per frame
    void Update()
    {
        if (isTransitioning)
        {
            Vector3 pos = anInteractObj.transform.position;

            if (isDownDirection)
            {
                if (anInteractObj.transform.position.y > targetPosActivated)
                {
                    pos.y -= speed * Time.deltaTime;
                    anInteractObj.transform.position = pos;
                }
                else
                {
                    isFullDown = true;
                    isTransitioning = false;
                }
            }
            else
            {
                // go up until its orginal position
                if (anInteractObj.transform.position.y < originalPos.y)
                {
                    pos.y += speed * Time.deltaTime;
                    anInteractObj.transform.position = pos;
                }
                else
                {
                    // Clamp Y pos to not be higher than original
                    if (pos.y > originalPos.y)
                    {
                        pos.y = originalPos.y;
                        anInteractObj.transform.position = pos;
                    }

                    isFullUp = true;
                    isTransitioning = false;
                }
            }
        }
    }

    /*==============================================================================
                                    ON_TRIGGER_ENTER
    ==============================================================================*/
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Pickable")
        {
            isTransitioning = true;
            isDownDirection = true;
            isFullUp = false;
        }
    }
    /*==============================================================================
                                    ON_TRIGGER_EXIT
    ==============================================================================*/
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Pickable")
        {
            isTransitioning = true;
            isDownDirection = false;
            isFullDown = false;
        }
    }
}
