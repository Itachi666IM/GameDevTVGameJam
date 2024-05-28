using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Shield;
    [SerializeField] bool CanBlock = true;
    [SerializeField] float BlockCooldown = 1.2f;

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
    }

    void ShieldBlock()
    {
        CanBlock = false;
        Animator anim = Shield.GetComponent<Animator>();
        anim.SetTrigger("Block");
        StartCoroutine(ResetShieldBlock());
    }

    IEnumerator ResetShieldBlock()
    {
        yield return new WaitForSeconds(BlockCooldown);
        CanBlock = true;
    }

}
