using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilencerMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed;//max speed in unit per second
    public Vector3 SilencerPositionChange;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = Vector3.zero;
        Vector3 rigidbodyPosition = GetComponent<Rigidbody2D>().position;
        position.x = Input.GetAxis("Horizontal");
        position.z = Input.GetAxis("Vertical");
        position.Normalize();
        float z_CorrectValue = 1.414f;//due to our stupid fucking Coordinate,we need a correct value
        position.x *= Speed*Time.deltaTime;
        position.z *= Speed*Time.deltaTime*z_CorrectValue;
        //Debug.Log(position);
        rigidbodyPosition += position;
        SilencerPositionChange = position;
    }
}
