using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilencerAction : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed;//max speed in unit per second
    public float JumpInitialSpeed;
    public Vector2 SilencerVelocity;
    Rigidbody2D m_Rigidbody;
    Animator m_Animator;
    public bool isSilencerCurrentOnGround = true;
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate() 
    {
        float MoveInput = 0;
        MoveInput= Input.GetAxis("Horizontal");//获取轴输入 范围：-1——1

        if(MoveInput!=0)//如果水平轴有输入
        {
            MovePhysic(MoveInput);
        }
        if(Input.GetButtonDown("Jump")&&isSilencerCurrentOnGround)
        {
            JumpPhysic();
        }
        SilencerVelocity = m_Rigidbody.velocity;
    }
    void MovePhysic(float MoveInput) => 
        m_Rigidbody.velocity = new Vector2(MoveInput * Speed, m_Rigidbody.velocity.y); //MovePosition(deltaPosition+m_Rigidbody.position);//transform.position = m_Rigidbody.position;
    void JumpPhysic() => 
        m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, JumpInitialSpeed);

    void OnCollisionEnter2D(Collision2D other)//主角与另一个碰撞体刚发生碰撞时
    {
        if(other.gameObject.tag=="ground")//与地面发生碰撞
        {
            isSilencerCurrentOnGround = true;//设置在地面标记为真
        }
    }

    void OnCollisionExit2D(Collision2D other)//主角与另一个碰撞体刚结束碰撞时
    {
        if(other.gameObject.tag=="ground")//与地面发生碰撞
        {
            isSilencerCurrentOnGround = false;//设置在地面标记为假
        }
    }

    
}
