using UnityEngine;

[CreateAssetMenu(fileName = "TextileData", menuName = "NodeSystem/TextileData", order = 1)]
public class TextileData : ScriptableObject
{
    public Texture2D albedoMap;
    public Texture2D normalMap;
    public Texture2D maskMap;

}
