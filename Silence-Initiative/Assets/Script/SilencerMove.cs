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
        Vector2 MoveInput = Vector2.zero;
        
        MoveInput.x = Input.GetAxis("Horizontal");
        MoveInput.y = Input.GetAxis("Vertical");//获取轴输入 范围：-1——1
        MoveAnima(MoveInput);
        
        if(MoveInput!=Vector2.zero)//如果有输入
        {
            MovePhysic(MoveInput);
            //MoveAnima(MoveInput);
            m_Animator.SetFloat("WalkX",MoveInput.x);
        }
    }
    void MovePhysic(Vector2 position)
    {
        position.Normalize();
        float y_CorrectValue = 1.414f;//due to our stupid fucking Coordinate,we need a correct value
        position.x *= Speed*Time.deltaTime;
        position.y *= Speed*Time.deltaTime*y_CorrectValue;
        m_Rigidbody.MovePosition(position+m_Rigidbody.position);
        transform.position = m_Rigidbody.position;
        SilencerPositionChange = position;
    }
    void MoveAnima(Vector2 position)
    {
        
    }

    
}
