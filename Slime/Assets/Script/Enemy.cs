 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //ü��
    public int hp = 33;
    //����(��)
    int dex;
    
    float speed;

    // 1 , -1 , 0 = �߰�
    int nextMove;

    float moveTime, curMoveTime;
    float attackTime, curattackTime;

    // �¾Ҵ�! = isHit
    bool isknockback;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamage(int damage)
    {
        hp = hp - damage;

    }
}
