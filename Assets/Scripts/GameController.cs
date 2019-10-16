using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject controlPanel;

    public void LoadStory(int n)
    {
        GetComponent<StoryLogic>().StartNewStory(JSONData.stories[n]);
        controlPanel.SetActive(false);
    }

    public void Restart()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        SceneManager.LoadScene(0);
    }
}
