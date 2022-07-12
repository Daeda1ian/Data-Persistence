using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;

public class InputHandler : MonoBehaviour {
    
    List<Person> persons = new List<Person>();

    private string filename = "savedata.json";

    private void Start() {
        persons = ReadListFromJSON();
        GetRecordPerson();
        /*for (int i = 0; i < persons.Count; i++) {
            Debug.Log("name : " + persons[i].Name + ", count : " + persons[i].points);
        } */
    }

    public void UpdatePoints (string name, int points) {
        Debug.Log("name : " + name + ", points : " + points);
        for (int i = 0; i < persons.Count; i++) {
            if (persons[i].Name == name) {
                if (persons[i].points < points) {
                    persons[i].points = points;
                }
                SaveToJSON();
                return;
            }
        }
    }

    public void GetRecordPerson () {
        if (persons == null || persons.Count == 0) 
            return;
        Person person = persons[0];
        for (int i = 1; i < persons.Count; i++) {
            if (persons[i].points > persons[i - 1].points) {
                person = persons[i];
            }
        }
        GameManager.instance.SetPersonRecord(person);
    }
    public void AddEntry (string name) {
        persons.Add(new Person(name, 0));
        SaveToJSON();
    }

    public void SaveToJSON() {
        Debug.Log(GetPath());
        string content = JsonHelper.ToJson(persons.ToArray());
        WriteFile(GetPath(), content);
    }

    public List<Person> ReadListFromJSON () {
        string content = ReadFile(GetPath());

        if (string.IsNullOrEmpty(content) || content == "{}") {
            return new List<Person>();
        }

        List<Person> res = JsonHelper.FromJson<Person>(content).ToList();
        return res;
    }

    private string ReadFile(string path) {
        if (File.Exists(path)) {
            using (StreamReader reader = new StreamReader(path)) {
                string content = reader.ReadToEnd();
                return content;
            }
        }
        return "";
    }

    private void WriteFile(string path, string content) {
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream)) {
            writer.Write(content);
        }
    }

    private string GetPath() {
        return Application.persistentDataPath + "/" + filename;
    }

}

[System.Serializable]
public class Person {
    public string Name;
    public int points;

    public Person(string name, int points) {
        Name = name;
        this.points = points;
    }
}

public static class JsonHelper {
    public static T[] FromJson<T>(string json) {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array) {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint) {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T> {
        public T[] Items;
    }
}