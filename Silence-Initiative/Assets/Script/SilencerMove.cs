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
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = Vector2.zero;
        
        position.x = Input.GetAxis("Horizontal");
        position.y = Input.GetAxis("Vertical");//获取轴输入 范围：-1——1
        if(position!=Vector2.zero)//如果有输入
        {
            m_Animator.SetBool("isWalking",true);
            m_Animator.SetBool("isBackward",false);
            m_Animator.SetFloat("X",position.x);
            m_Animator.SetFloat("Y",position.y);
            position.Normalize();
            float y_CorrectValue = 1.414f;//due to our stupid fucking Coordinate,we need a correct value
            position.x *= Speed*Time.deltaTime;
            position.y *= Speed*Time.deltaTime*y_CorrectValue;
            m_Rigidbody.MovePosition(position+m_Rigidbody.position);
            transform.position = m_Rigidbody.position;
            SilencerPositionChange = position;
        }
        else
        {
            m_Animator.SetBool("isWalking",false);
        }
        //Debug.Log(m_Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        
    }
}
