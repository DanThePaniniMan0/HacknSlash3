using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string playerEquippedWeapon;

    public Canvas startGUI;
    public Canvas selectionGUI;
    public GameObject fighterText;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchGUI()
    {
        startGUI.GetComponent<Canvas>().enabled = false;
        selectionGUI.GetComponent<Canvas>().enabled = true;
    }

    public void setFighter(string fighter)
    {
        fighterText.GetComponent<Text>().text = "Current Fighter: " + fighter;
        playerEquippedWeapon = fighter;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
