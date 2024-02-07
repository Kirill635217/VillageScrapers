using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Reset : MonoBehaviour
{
    private Button resetButton;
    private const string SceneName = "SampleScene";

    private void Awake()
    {
        TryGetComponent(out resetButton);
        resetButton.onClick.AddListener(ResetButtonPressed);
    }

    private void ResetButtonPressed()
    {
        SceneManager.LoadScene(SceneName);
    }
}
