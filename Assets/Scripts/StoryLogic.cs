using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StoryLogic : MonoBehaviour
{
    [SerializeField]
    private Image blackScreen;
    [SerializeField]
    private SpeechPanel playerSpeechPanel;
    [SerializeField]
    private SpeechPanel neutralSpeechPanel;
    [SerializeField]
    private SpeechPanel tutorialSpeechPanel;

    public Story story;
    private Node currentNode;
    private SpeechPanel currentSpeechPanel;

    public static Dictionary<string, GameObject> characterDict = new Dictionary<string, GameObject>();

    private void OnEnable()
    {
        SpeechPanel.OnAnswerClicked += ToNode;
    }

    private void OnDisable()
    {
        SpeechPanel.OnAnswerClicked -= ToNode;
    }

    public void StartNewStory(Story story)
    {
        this.story = story;

        ToNode(0);
    }

    private void ToNode(int n)
    {
        if (n == 0 && currentNode.id != 0)
        {
            Finish();
            return;
        }
            
        if (currentSpeechPanel)
        {
            currentSpeechPanel.gameObject.SetActive(false);
        }
            
        currentNode = story.Nodes[n];
        var charName = currentNode.character;

        if (charName == "...")
            currentSpeechPanel = tutorialSpeechPanel;
        else
        {
            currentSpeechPanel = charName == "player" ? playerSpeechPanel : neutralSpeechPanel;
            if (!characterDict.ContainsKey(charName))
                characterDict.Add(charName, BundleLoader.LoadCharacter(charName));
        }

        currentSpeechPanel.SetNode(currentNode);
        currentSpeechPanel.gameObject.SetActive(true);
    }

    private void Finish()
    {
        blackScreen.raycastTarget = true;
        blackScreen.DOFade(1, 2);
    }
}
