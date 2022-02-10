using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager// : MonoBehaviour
{
    public static void ChangeLevel(string level)
    {
        SceneManager.LoadScene(level);
    }
}
