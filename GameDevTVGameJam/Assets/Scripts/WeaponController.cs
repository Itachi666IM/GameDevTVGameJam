using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Shield, Spear;
    [SerializeField] bool CanBlock = true;
    [SerializeField] bool CanAttack = true;
    [SerializeField] float BlockCooldown = 1.2f;
    [SerializeField] float AttackCooldown = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if (CanBlock)
            {
                ShieldBlock();
            }

        }

        if(Input.GetMouseButtonDown(0))
        {
            if(CanAttack)
            {
                SpearAttack();
            }
        }
    }

    void ShieldBlock()
    {
        CanBlock = false;
        Animator anim = Shield.GetComponent<Animator>();
        anim.SetTrigger("Block");
        StartCoroutine(ResetShieldBlock());
    }

    void SpearAttack()
    {
        CanAttack = false;
        Animator spearAnim = Spear.GetComponent<Animator>();
        spearAnim.SetTrigger("Attack");
        StartCoroutine(ResetSpearAttack());
    }

    IEnumerator ResetShieldBlock()
    {
        yield return new WaitForSeconds(BlockCooldown);
        CanBlock = true;
    }

    IEnumerator ResetSpearAttack()
    {
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }

}
