using System.Collections.Generic;
using UnityEngine;

public class CharacterLogic : MonoBehaviour
{
    private CharacterStructure cs;

    Dictionary<string, Sprite> emotionDict = new Dictionary<string, Sprite>();

    public void Init(CharacterStructure characterStructure)
    {
        cs = characterStructure;

        emotionDict.Add("neutral", cs.faceSprites.neutral);
        emotionDict.Add("happy", cs.faceSprites.happy);
        emotionDict.Add("angry", cs.faceSprites.angry);
        emotionDict.Add("sad", cs.faceSprites.sad);
        emotionDict.Add("shocked", cs.faceSprites.shocked);
        emotionDict.Add("horny", cs.faceSprites.horny);
        emotionDict.Add("suspicious", cs.faceSprites.suspicious);
    }

    public void SetEmotion(string emotion)
    {
        if (!emotionDict.ContainsKey(emotion))
        {
            Debug.Log("No such emotion key: " + emotion);
            return;
        }
            

        var newSprite = emotionDict[emotion];
        if (newSprite == null)
        {
            Debug.Log("No sprite for emotion: " + emotion);
            return;
        }
            
        cs.face.sprite = newSprite;
    }
}
