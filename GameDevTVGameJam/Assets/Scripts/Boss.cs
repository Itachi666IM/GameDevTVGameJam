using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    private GameObject player;
    private GameObject youDiedPanel;
    public NavMeshAgent boss;
    public int health = 5;

    public LayerMask playerMask, groundMask;

    //Attack
    private bool isAlreadyAttacked = false;
    [SerializeField] float timeBetweenAttacks = 1.0f;

    //Hit

    //SideStep

    //Death

    //Walk
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //States
    public float sightRange, attackRange;
    private bool isSightRange, isAttackRange; 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        youDiedPanel = GameObject.Find("You Died Panel");
    }

    // Update is called once per frame
    void Update()
    {
        isSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        isAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

        if (!isSightRange && !isAttackRange) Walk();
        if (isSightRange && !isAttackRange) Chase();
        if (isSightRange && isAttackRange) Attack();
    }

    void Walk()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        { 
            boss.SetDestination(walkPoint);
            Animator anim = GetComponent<Animator>();
            anim.SetTrigger("Walk");
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask))
            walkPointSet = true;
    }

    void Chase()
    {
        boss.SetDestination(player.transform.position);
    }

    void Attack()
    {
        boss.SetDestination(transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position), 2f * Time.deltaTime);
        if(!isAlreadyAttacked)
        {
            Animator anim = GetComponent<Animator>();
            string[] attacks = { "Attack", "Attack2", "SAttack","SideStep"};
            int ind = Random.Range(0, 4);
            anim.SetTrigger(attacks[ind]);
            isAlreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        isAlreadyAttacked = false;
    }

    void GetHit()
    {
        if(health==0)
        {
            Animator anim = GetComponent<Animator>();
            anim.SetTrigger("Death");
            Destroy(gameObject);
        }
        else
        {
            health -= 1;
            Animator anim = GetComponent<Animator>();
            anim.SetTrigger("Hit");
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Weapon"))
        {
            GetHit();
        }
        if (other.gameObject.CompareTag("Player"))
        {
            youDiedPanel.SetActive(true);
        }
    }

}
