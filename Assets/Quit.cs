using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quit : MonoBehaviour
{
    private Button quitButton;

    private void Awake()
    {
        TryGetComponent(out quitButton);
        quitButton.onClick.AddListener(QuitButtonPressed);
    }

    private void QuitButtonPressed()
    {
        Application.Quit();
    }
}
