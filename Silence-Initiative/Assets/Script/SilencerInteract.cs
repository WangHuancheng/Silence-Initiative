using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilencerInteract : MonoBehaviour
{
    // Start is called before the first frame update
    Animator m_SilencerAnimator;
    private bool m_IsSilencerInteract;
    void Start()
    {
        m_SilencerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        m_IsSilencerInteract = Input.GetButton("interact");
        m_SilencerAnimator.SetBool("Lighting",m_IsSilencerInteract);//如果按interact键，就把Lighitng设定成true，
        //Debug.Log("m_IsSilencerInteract="+m_IsSilencerInteract);
    }

    public bool GetSilencerInteraction() => m_IsSilencerInteract;//返回当前主角的互动状态
}
