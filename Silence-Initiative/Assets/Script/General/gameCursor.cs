using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class gameCursor : MonoBehaviour
{
    public Texture2D myCursor;
    // Start is called before the first frame update
    void Start()
    {
        //初始化鼠标
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(myCursor,Vector2.zero,CursorMode.Auto);
    }
    void Update() 
    {
        //Debug.Log("world position"+ GetCursorWorldPosition(Camera.main));
    }
    //获取鼠标位置的世界坐标
    public static Vector3 GetCursorWorldPosition(Camera currentCamera)
    {
        //Camera currentCamera = Camera.main;
        Vector3 screenPoint = Input.mousePosition;
        Vector3 worldPosition = Vector3.zero;
        screenPoint.z = 10f;

        worldPosition = currentCamera.ScreenToWorldPoint(screenPoint);
        worldPosition.z = 0f;
        return worldPosition;
    }
}
