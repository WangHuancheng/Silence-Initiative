using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilencerShot : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject gunBone;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float RotationAngle = 0f;
        Vector3 currentGunVector = transform.position;
        Vector3 desiredGunVector = gameCursor.GetCursorWorldPosition(Camera.main);
        RotationAngle = FromToRotationAngle(currentGunVector,desiredGunVector);
        transform.rotation = Quaternion.Euler(new Vector3(0,0,RotationAngle));
    }

    float FromToRotationAngle(Vector3 v1, Vector3 v2)
    {
        float angle = Vector3.Angle(v1,v2);
        angle*=Mathf.Sign(v1.x*v2.y - v2.x*v1.y);
        return angle;
    }
}
