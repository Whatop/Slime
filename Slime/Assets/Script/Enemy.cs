 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //체력
    public int hp = 33;
    //방어력(방어구)
    int dex;
    
    float speed;

    // 1 , -1 , 0 = 발견
    int nextMove;

    float moveTime, curMoveTime;
    float attackTime, curattackTime;

    // 맞았다! = isHit
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
