using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
   public void RePlayGame()
   {
        SceneManager.LoadSceneAsync(1);
   }

   public void QuitGame()
   {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
   }
}
