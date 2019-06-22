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
        Vector3 cursorWorldPosition = gameCursor.GetCursorWorldPosition(Camera.main);
        cursorWorldPosition.z= cursorWorldPosition.y;
        Vector3 desiredGunVector = cursorWorldPosition - currentGunVector;
        desiredGunVector.z = -desiredGunVector.y;
        RotationAngle = FromToRotationAngle(currentGunVector,desiredGunVector);
        //transform.rotation = Quaternion.Euler(new Vector3(0,0,RotationAngle));
        Vector3 rotationVector =  new Vector3(0,0,RotationAngle-55);//55 is a fucking magic number, MUSTN'T CHANGE IT
        transform.localEulerAngles=rotationVector;
        //Debug.Log("currentGunVector"+currentGunVector);
        //Debug.Log("derierd"+desiredGunVector);
    }

    float FromToRotationAngle(Vector3 v1, Vector3 v2)
    {
        float angle = Vector3.Angle(v1,v2);
        angle*=Mathf.Sign(v1.x*v2.y - v2.x*v1.y);
        return angle;
    }
}
