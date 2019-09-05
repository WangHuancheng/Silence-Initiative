using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_STANDALONE && !UNITY_EDITOR
        if (Input.GetButton("Quit"))
        {
            Application.Quit();
        }
        #endif
    }
}
