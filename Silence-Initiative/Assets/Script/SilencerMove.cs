using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilencerMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed;//max speed in unit per second
    public Vector2 SilencerPositionChange;
    Rigidbody2D m_Rigidbody;
    Animator m_Animator;
    bool isSilencerCurrentLeft = false;
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 MoveInput = Vector2.zero;
        
        MoveInput.x = Input.GetAxis("Horizontal");
        MoveInput.y = Input.GetAxis("Vertical");//获取轴输入 范围：-1——1
        //MoveAnima(MoveInput);
        
        if(MoveInput!=Vector2.zero)//如果有输入
        {
            MovePhysic(MoveInput);
            MoveAnima(MoveInput);
        }
    }
    void MovePhysic(Vector2 MoveInput)
    {
        Vector2 deltaPosition = Vector2.zero;
        deltaPosition.x =  MoveInput.x * Speed * Time.deltaTime;
        m_Rigidbody.MovePosition(deltaPosition+m_Rigidbody.position);
        //transform.position = m_Rigidbody.position;
        SilencerPositionChange = deltaPosition;//  editor中显示位置变化量
    }
    void MoveAnima(Vector2 MoveInput)
    {
        if(MoveInput.x > 0 && isSilencerCurrentLeft==true)
        {
            transform.eulerAngles = new Vector3 (0,0,0);
            isSilencerCurrentLeft = false;
        }
        else if(MoveInput.x < 0 && isSilencerCurrentLeft==false)
        {
            transform.eulerAngles = new Vector3 (0,180,0);
            isSilencerCurrentLeft = true;
        }
        m_Animator.SetFloat("WalkX",MoveInput.x);
    }

    
}
