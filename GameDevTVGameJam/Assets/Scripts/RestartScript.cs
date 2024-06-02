using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour
{
    private GameObject gamePanel;

    private void Start()
    {
        gamePanel = GameObject.Find("Game Over panel");
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.R))
        {
            gamePanel.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if(Input.GetKey(KeyCode.B))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
    // Start is called before the first frame update

}
