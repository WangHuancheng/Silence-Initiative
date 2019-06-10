using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silencer_Interact : MonoBehaviour
{
    // Start is called before the first frame update
    Animator m_SilencerAnimator;
    void Start()
    {
        m_SilencerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        m_SilencerAnimator.SetBool("Lighting",Input.GetButton("interact"));//如果按interact键，就把Lighitng设定成true，反之则为false
    }
}
