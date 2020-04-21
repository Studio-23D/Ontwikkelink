using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialPropertyApplier : MonoBehaviour
{
	#region PUBLIC_STRUCTS

	[System.Serializable]
	public struct Pattern
	{
		public Texture newTexture;
		public Texture defaultTexture;
		public Color newColor1;
		public Color newColor2;
		public Color newColor3;
		public Color defaultColor1;
		public Color defaultColor2;
		public Color defaultColor3;
	}

	[System.Serializable]
	public struct Textile
	{
		public Texture newTexture;
		public Texture defaultTexture;
		public Color newColor;
		public Color defaultColor;
	}

	#endregion



	#region PUBLIC_MEMBERS

	public Transform ClothingPiece { get { return clothingPiece; } }
	public Pattern pattern;
	public Textile textile;

	#endregion



	#region PRIVATE_MEMBERS
	
	[Tooltip("The material of the mesh renderer of this object will be edited and outputed")]
	[SerializeField] private Transform clothingPiece;

	[Header("Shader property keywords")]
	[SerializeField] private string textileColorPropKey = "Color_C67D4219";
    [SerializeField] private string textileAlbedoPropKey = "Texture2D_23DA1713";
	[SerializeField] private string patternAlbedoPropKey = "";
	[SerializeField] private string patternColor1PropKey = "";
	[SerializeField] private string patternColor2PropKey = "";
	[SerializeField] private string patternColor3PropKey = "";
    [SerializeField] private string maskMapPropKey = "Texture2D_58606ADF";
	[SerializeField] private string normalMapPropKey = "Texture2D_F734C7A";

	private Material customMaterial;

	private MeshRenderer meshRenderer;
	private Material defaultMaterial;

	#endregion



	#region MONOBEHAVIOUR_METHODS

	private void Start()
	{
		// INIT WHEN CLOTHINGPIECE SELECTED IN NODE

		Init(clothingPiece);
	}

	private void OnDisable()
	{
		RestoreDefaults();
	}

	// REMOVE AFTER MERGE INTO DEVELOP
	private void OnGUI()
    {
        if (GUILayout.Button("Connect material node to clothing node")) { SetTexture(customMaterial, textileAlbedoPropKey, textile.newTexture); }
        if (GUILayout.Button("Connect color node to clothing node")) { SetColor(customMaterial, textileColorPropKey, textile.newColor); }

        if (GUILayout.Button("Set to default texture")) { SetTextileTextureDefault(); }
        if (GUILayout.Button("Set to default color")) { SetTextileColorDefault(); }
    }
	// --- //

	#endregion



	#region PUBLIC_METHODS

	public void Init(Transform clothingPiece)
	{

		if (!clothingPiece.GetComponent<MeshRenderer>())
		{
			Debug.LogError("CLOTHING IS MISSING MESH RENDERER");
		}
		else
		{
			this.clothingPiece = clothingPiece;
			meshRenderer = clothingPiece.GetComponent<MeshRenderer>();


			defaultMaterial = meshRenderer.sharedMaterial;

			defaultMaterial.EnableKeyword(textileAlbedoPropKey);
			defaultMaterial.EnableKeyword(textileColorPropKey);

			textile.defaultTexture = defaultMaterial.GetTexture(textileAlbedoPropKey);
			textile.defaultColor = defaultMaterial.GetColor(textileColorPropKey);

			customMaterial = new Material(defaultMaterial.shader);
			customMaterial.name = "CustomMaterial";

			// Sets default properties to custom material
			customMaterial.SetTexture(textileAlbedoPropKey, defaultMaterial.GetTexture(textileAlbedoPropKey));
			customMaterial.SetTexture(maskMapPropKey, defaultMaterial.GetTexture(maskMapPropKey));
			customMaterial.SetTexture(normalMapPropKey, defaultMaterial.GetTexture(normalMapPropKey));
			customMaterial.SetColor(textileColorPropKey, defaultMaterial.GetColor(textileColorPropKey));

			clothingPiece.GetComponent<MeshRenderer>().sharedMaterial = customMaterial;

			customMaterial.EnableKeyword(textileAlbedoPropKey);
			customMaterial.EnableKeyword(textileColorPropKey);
		}
	}

	#endregion



	#region PRIVATE_METHODS

	private void SetTexture(Material material, string propertyKeyword, Texture texture) 
	{
		material.SetTexture(propertyKeyword, texture);

	}

    private void SetColor(Material material, string propertyKeyword, Color color) 
	{
		material.SetColor(propertyKeyword, color); 
	}

	private void SetTextileTextureDefault() 
	{ 
		SetTexture(customMaterial, textileAlbedoPropKey, textile.defaultTexture); 
	}

	private void SetTextileColorDefault()
	{
		SetColor(customMaterial, textileColorPropKey, textile.defaultColor);
	}

	private void SetPatternTextureDefault()
	{
		SetTexture(customMaterial, patternAlbedoPropKey, pattern.defaultTexture);
	}

	private void SetPatterColorDefault(int i)
	{
		switch(i)
		{
			case 1:
				SetColor(customMaterial, patternColor1PropKey, pattern.defaultColor1);
				break;

			case 2:
				SetColor(customMaterial, patternColor2PropKey, pattern.defaultColor2);
				break;

			case 3:
				SetColor(customMaterial, patternColor3PropKey, pattern.defaultColor3);
				break;

			default:
				Debug.LogWarning("GIVEN INDEX '" + i + "' IS NOT AVAILABLE AS COLOR INDEX OF THE PATTERN");
				break;
		}
	}

	private void RestoreDefaults()
	{
		SetTextileTextureDefault();
		SetTextileColorDefault();
		meshRenderer.sharedMaterial = defaultMaterial;
	}

	#endregion
}
