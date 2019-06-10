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
        Vector2 position = Vector2.zero;
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        position.x = Input.GetAxis("Horizontal");
        position.y = Input.GetAxis("Vertical");
        position.Normalize();
        float y_CorrectValue = 1.414f;//due to our stupid fucking Coordinate,we need a correct value
        position.x *= Speed*Time.deltaTime;
        position.y *= Speed*Time.deltaTime*y_CorrectValue;
        //Debug.Log(position);
        rigidbody.MovePosition(position+rigidbody.position);
        transform.position = rigidbody.position;
        SilencerPositionChange = position;
    }
}
