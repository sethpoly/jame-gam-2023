using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExpositionController : MonoBehaviour
{
    public void Continue()
    {
        SceneManager.LoadScene("Game");
    }
}
