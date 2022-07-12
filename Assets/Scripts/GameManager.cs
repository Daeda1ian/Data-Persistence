using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour{
    
    public static GameManager instance;

    private string nameOfPlayer;

    private Person bestPerson;
    private InputHandler inputHandler;

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        inputHandler = GetComponent<InputHandler>();
        bestPerson = new Person("Nobody", 0);
        Debug.Log("Name of current player: " + nameOfPlayer);
        DontDestroyOnLoad(gameObject);
    }

    public void AddEntry (string name) {
        nameOfPlayer = name;
        inputHandler.AddEntry(name);
    }

    public Person GetBestPerson () {
        inputHandler.GetRecordPerson();
        return bestPerson;
    }

    public void SetPersonRecord (Person person) {
        bestPerson = person;
    }

    public void UpdatePoints (int points) {
        Debug.Log("Name of current player: " + nameOfPlayer);
        inputHandler.UpdatePoints(nameOfPlayer, points);
    }
}








