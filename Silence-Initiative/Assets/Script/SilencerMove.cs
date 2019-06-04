using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilencerMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed;
    void Start()
    {
        Speed = 1f;//max speed in unit per second
    }

    // Update is called once per frame
    void Update()
    {
        float deltaXFromInput = Input.GetAxis("Horizontal");
        float deltaYFromInput = Input.GetAxis("Horizontal");
        Vector3 position = Vector3.zero;
        const float Y_CorrectValue = 1.4142f;
        position.x = deltaXFromInput * Speed * Time.deltaTime;
        position.y = deltaXFromInput * Speed * Time.deltaTime * Y_CorrectValue;
        transform.position += position;
    }
}
