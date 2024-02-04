using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;

    public void Options()
    {
        optionsMenu.SetActive(true);
    }

    public void mainMenu()
    {
        optionsMenu.SetActive(false);
    }
}
