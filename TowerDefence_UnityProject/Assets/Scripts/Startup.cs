using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startup : MonoBehaviour
{
    private async void Start()
    {
        await new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }
}

