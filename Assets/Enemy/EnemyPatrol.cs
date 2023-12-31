using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [Header("Enemy")]
    [SerializeField] private Transform enemy;
    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;
    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;
    [Header("Enemy Animator")]
    [SerializeField] private Animator ani;

    private void Awake()
    {
        initScale = enemy.localScale;
    }
    private void OnDisable()
    {
        ani.SetBool("moving", false);
    }
    private void Update()
    {
        if (movingLeft)
        {
            if(enemy.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                //Thay doi huong
                DirectionChage();
            }
           
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                //Thay doi huong
                DirectionChage();
            }
        }
    }
    private void DirectionChage()
    {
        ani.SetBool("moving", false);

        idleTimer += Time.deltaTime;
        if(idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
        }
        
    }
    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        ani.SetBool("moving", true);
        //Huong quay mat cua enemy
        enemy.localScale = new Vector3 (Mathf.Abs(initScale.x) * _direction,
            initScale.y, initScale.z);
        //Di chuyen toi huong do
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }
}
