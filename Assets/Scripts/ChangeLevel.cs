using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    public static void GotoNextLevel(int dex)
    {
        SceneManager.LoadScene(dex);
    }
}
