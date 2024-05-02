using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string playerEquippedWeapon;

    public Canvas startGUI;
    public Canvas selectionGUI;


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
        startGUI.enabled = false;
        selectionGUI.enabled = true;
    }
}
