using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

    public Text scoreText;
    public Image backgroundImg; //black sprite

    public bool isShown = false;

    private float transition = 0.0f;


	// Use this for initialization
	void Start () {
        gameObject.SetActive(false); //on boot the manu wont be on
	}
	
	// Update is called once per frame
	void Update () {
        if (!isShown)
            return;
        transition += Time.deltaTime;
        backgroundImg.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);
	}

    public void ToggleEndMenu(float score)
    {
        gameObject.SetActive(true);
        //point at end score
        scoreText.text = ((int)score).ToString();
        isShown = true;
    }

    public void Restart()
    {
        //loading a scene, string parameter, reboot scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    
    }
}
