using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region PUBLIC_CLASS
public class MaterialPropertyApplier : MonoBehaviour
{
    [SerializeField] private Texture[] availableAlbinos;
    private Texture selectedTexture;
    private Texture defaultTexture;


    [SerializeField] private Color[] availableColors;
    private Color selectedColor;
    private Color defaultColor;

    [SerializeField] private GameObject hoodie;

    private MeshRenderer meshRenderer;

    private string albedoPropertyKeyword = "Texture2D_23DA1713";
    private string colorPropertyKeyword = "Color_C67D4219";

    private void Start()
    {
        selectedTexture = availableAlbinos[0];

        meshRenderer = hoodie.GetComponent<MeshRenderer>();
        meshRenderer.material.EnableKeyword(albedoPropertyKeyword);

        defaultTexture = meshRenderer.material.GetTexture(albedoPropertyKeyword);
        defaultColor = meshRenderer.material.GetColor(colorPropertyKeyword);
    }

    // REMOVE AFTER MERGE INTO DEVELOP
    private void OnGUI()
    {
        if (GUILayout.Button("Connect material node to clothing node")) { SetTexture(selectedTexture); }
        if (GUILayout.Button("Connect color node to clothing node")) { SetColor(selectedColor); }

        if (GUILayout.Button("Choose carrot texture")) { selectedTexture = availableAlbinos[0]; }
        if (GUILayout.Button("Choose whale texture")) { selectedTexture = availableAlbinos[1]; }

        if (GUILayout.Button("Choose custom color 1")) { selectedColor = availableColors[0]; }
        if (GUILayout.Button("Choose custom color 2")) { selectedColor = availableColors[1]; }

        if (GUILayout.Button("Set to default texture")) { SetTextureDefault(); }
        if (GUILayout.Button("Set to default color")) { SetColorDefault(); }
    }
    // --- //

    private void SetTexture(Texture texture) { meshRenderer.material.SetTexture(albedoPropertyKeyword, texture); }
    private void SetColor(Color color) { meshRenderer.material.SetColor(colorPropertyKeyword, color); }

    private void SetTextureDefault() { SetTexture(defaultTexture); }
    private void SetColorDefault() { SetColor(defaultColor); }
}
#endregion