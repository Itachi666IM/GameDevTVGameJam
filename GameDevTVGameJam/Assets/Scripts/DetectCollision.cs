using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    private GameObject youDiedPanel;
    // Start is called before the first frame update
    void Start()
    {
        youDiedPanel = GameObject.Find("You Died Panel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Weapon"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            youDiedPanel.SetActive(true);
        }

    }

}
