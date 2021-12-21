using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{

    [SerializeField] GameObject creditsGameObject;

    private bool credits;
    // Start is called before the first frame update
    void Start()
    {
        credits = false;
        creditsGameObject.SetActive(credits);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("Game");
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            onCreditsClick();
        }
    }

    public void onStartClick()
    {

    }

    public void onCreditsClick()
    {
        credits = !credits;
        creditsGameObject.SetActive(credits);
    }
}
