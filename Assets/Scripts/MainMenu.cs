using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour {

    public InputField inputField;
    public string recordPerson;
    public int record;
    public void LoadGame() {
        SceneManager.LoadScene("main");
    }

    public void QuitGame() {
    #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
    #else
        Application.Quit();
    #endif
    }

    public void ChangeName() {
        GameManager.instance.AddEntry(inputField.text);
    }

}
