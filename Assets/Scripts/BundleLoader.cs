using UnityEngine;
using System.IO;

public class BundleLoader : MonoBehaviour
{
    private static AssetBundle LoadBundleWithName(string name)
    {
        var result = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, name));
        if (result == null)
            Debug.LogError("Failed to load AssetBundle with name " + name);

        return result;
    }

    public static GameObject LoadCharacter(string name)
    {
        var bundle = LoadBundleWithName(Path.Combine("characters", name));
        bundle.LoadAllAssets();

        return bundle.LoadAsset<GameObject>(Path.Combine("assets/characters/", name, "_character.prefab"));
    }

    public static TextAsset[] LoadAllStories()
    {
        var bundle = LoadBundleWithName("stories");
        bundle.LoadAllAssets();
        return bundle.LoadAllAssets<TextAsset>();
    }
}
