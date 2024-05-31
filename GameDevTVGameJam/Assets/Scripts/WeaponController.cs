using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class WeaponController : MonoBehaviour
{
    public GameObject Shield, Spear;
    [SerializeField] bool CanBlock = true;
    [SerializeField] bool CanAttack = true;
    [SerializeField] float BlockCooldown = 1.2f;
    [SerializeField] float AttackCooldown = 1.0f;
    public AudioClip attackAudio;
    public AudioClip blockAudio;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GameObject.Find("SpawnManager").GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if (CanBlock)
            {
                ShieldBlock();
                audioSource.PlayOneShot(blockAudio, 1.0f);
            }

        }

        if(Input.GetMouseButtonDown(0))
        {
            if(CanAttack)
            {
                SpearAttack();
                audioSource.PlayOneShot(attackAudio, 1.0f);
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
