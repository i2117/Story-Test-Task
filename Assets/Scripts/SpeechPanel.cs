using System;
using UnityEngine;
using UnityEngine.UI;

public class SpeechPanel : MonoBehaviour
{
    public static Action<int> OnAnswerClicked;

    public CharacterStructure currentCharacter;
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text speechText;
    [SerializeField]
    private Button[] answerButtons;

    private Node currentNode;

    public void SetNode(Node node)
    {
        currentNode = node;

        if (StoryLogic.characterDict.ContainsKey(currentNode.character))
            SetCharacter();

        SetTexts();
    }

    private void SetCharacter()
    {
        if (currentCharacter != null)
            Destroy(currentCharacter.gameObject);

        var newCharGO = Instantiate(StoryLogic.characterDict[currentNode.character], transform);

        currentCharacter = GetComponentInChildren<CharacterStructure>();
        var currentCharacterLogic = currentCharacter.gameObject.AddComponent<CharacterLogic>();

        currentCharacterLogic.Init(currentCharacter);

        currentCharacterLogic.SetEmotion(currentNode.emotion);
    }

    private void SetTexts()
    {
        nameText.text = currentNode.character;
        speechText.text = currentNode.text;

        var hasOptions = currentNode.options != null;

        nextButton.onClick.RemoveAllListeners();

        if (!hasOptions)
        {
            nextButton.onClick.AddListener(() =>
            {
                OnAnswerClicked?.Invoke(currentNode.next);
            });
        }

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].onClick.RemoveAllListeners();
            var shouldActivate = hasOptions && i < currentNode.options.Length;
            answerButtons[i].gameObject.SetActive(shouldActivate);
            if (shouldActivate)
            {
                answerButtons[i].GetComponentInChildren<Text>().text = currentNode.options[i].text;
                var next = currentNode.options[i].next;
                answerButtons[i].onClick.AddListener(() =>
                {
                    OnAnswerClicked?.Invoke(next);
                });
            }
        }
    }

    private void OnDisable()
    {
        if (currentCharacter != null)
            Destroy(currentCharacter.gameObject);
    }
}
