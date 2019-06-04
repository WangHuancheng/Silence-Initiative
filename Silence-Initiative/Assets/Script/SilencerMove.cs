using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilencerMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed;//max speed in unit per second
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = Vector3.zero;
        position.x = Input.GetAxis("Horizontal");
        position.y = Input.GetAxis("Vertical");
        position.Normalize();
        float Y_CorrectValue = 1.414f;//due to our stupid fucking Coordinate,we need a correct value
        position.x *= Speed*Time.deltaTime;
        position.y *= Speed*Time.deltaTime*Y_CorrectValue;
        //Debug.Log(position);
        transform.position += position;
    }
}
