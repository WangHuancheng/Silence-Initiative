using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testObjectDestroy : MonoBehaviour
{
    public GameObject PlayerCharacter;//玩家控制的角色 （除了silencer应该没了。。。吧）
    SilencerInteract PlayerInteract;//贴在玩家控制觉得上的互动脚本实例
    Animator m_testObjecetAnimator;//测试物品的动画器
    const int testObejectDestroying = 1;//损毁中状态
    const int testObejectDestroyed = 2;//损毁后状态
    int testObjectCondition;//初始完好状态
    // Start is called before the first frame update
    void Start()
    {
        PlayerInteract = PlayerCharacter.GetComponent<SilencerInteract>();//初始化对象
        m_testObjecetAnimator = GetComponent<Animator>();
        testObjectCondition = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("test Obeject "+testObjectCondition);
        //当物品在损毁状态中且动画播放完毕，进入完全损坏状态
        if(testObjectCondition==testObejectDestroying&&
            m_testObjecetAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f) 
            {
                testObjectCondition = testObejectDestroyed;
                m_testObjecetAnimator.SetBool("destroyed",true);
                //Debug.Log("test Obeject Destroyed");
            }
            
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        //Debug.Log("stay");
        if (other.gameObject==PlayerCharacter && PlayerInteract.GetSilencerInteraction())//当玩家角色触发互动范围 且 进行了互动
        {
            testObjectCondition = testObejectDestroying;
            m_testObjecetAnimator.SetBool("destroying",true);//物品开始损毁
        }
    }
}
