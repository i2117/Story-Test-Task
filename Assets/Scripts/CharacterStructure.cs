using UnityEngine;

public class CharacterStructure : MonoBehaviour
{
    public string characterName;

    public SpriteRenderer face;
    public SpriteRenderer body;
    public SpriteRenderer hair;
    public SpriteRenderer emotion;
    public FaceSprites faceSprites;

    [System.Serializable]
    public struct FaceSprites
    {
        public Sprite neutral;
        public Sprite happy;
        public Sprite angry;
        public Sprite sad;
        public Sprite shocked;
        public Sprite horny;
        public Sprite suspicious;
    }
}


