using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilencerAnima : MonoBehaviour
{
    bool isSilencerCurrentLeft = false;
    Animator m_Animator;
    SilencerAction silencerAction;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        silencerAction = gameObject.GetComponent<SilencerAction>();
    }

    // Update is called once per frame
    void Update()
    {
        float MoveInput = 0;
        MoveInput= Input.GetAxis("Horizontal");//获取轴输入 范围：-1——1
         if(MoveInput!=0)//如果水平轴有输入
        {
            Turn(MoveInput);
            ActionAnima(MoveInput);
        }
    }
    void Turn(float MoveInput)
    {
        if(MoveInput > 0 && isSilencerCurrentLeft==true)// 有向右输入 且 现在面向左
        {
            transform.eulerAngles = new Vector3 (0,0,0);
            isSilencerCurrentLeft = false;
        }
        else if(MoveInput< 0 && isSilencerCurrentLeft==false)//有向左输入 且 现在面向右
        {
            transform.eulerAngles = new Vector3 (0,180,0);
            isSilencerCurrentLeft = true;
        }
    }
        void ActionAnima(float MoveInput)
    {
        m_Animator.SetFloat("WalkX",MoveInput);
    }
}
