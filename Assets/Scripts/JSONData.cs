using System.Collections.Generic;
using UnityEngine;

public class JSONData : MonoBehaviour
{
    public static List<Story> stories;

    void Awake()
    {
        stories = new List<Story>();
        var allStoriesTexts = BundleLoader.LoadAllStories();

        foreach (var storyText in allStoriesTexts)
        {
            Node[] nodes = JsonHelper.FromJson<Node>(storyText.ToString());
            Story story = new Story();
            story.Nodes = nodes;

            stories.Add(story);
        }
    }

    private string StringFromFile(string filename)
    {
        TextAsset file = Resources.Load<TextAsset>(filename) as TextAsset;
        return file.ToString();
    }
}

[System.Serializable]
public struct Story
{
    public Node[] Nodes;
}

[System.Serializable]
public struct Node
{
    public int id;
    public string character;
    public string text;
    public string emotion;
    public Option[] options;
    public int next;
}

[System.Serializable]
public struct Option
{
    public string text;
    public int next;
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Nodes;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Nodes = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Nodes = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Nodes;
    }
}
