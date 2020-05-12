using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSpectrum : MonoBehaviour
{
    private Rect previewRect   = new Rect(150.0f,  50.0f, 256.0f,  50.0f);
    private Rect selectionRect = new Rect(150.0f, 100.0f, 256.0f, 256.0f);
    private Rect draggableRect = new Rect(155.0f, 105.0f,  25.0f,  25.0f);
    private Rect draggableContainRect;

    private Color previewColor;
    private Color selectionColor;

    private string hexCode;

    private Texture2D previewTexture;
    [SerializeField] private Texture2D selectionTexture;
    private Texture2D draggableTexture;

    Vector2 draggableCenter;

    private bool isSelectingColor; 

    private void Awake()
    {
        InitializeTextures();
        InitializeRect();
    }

    private void InitializeTextures()
    {
        previewTexture = new Texture2D(1, 1);
        previewTexture.SetPixel(0, 0, previewColor);
        previewTexture.Apply();

        GetTextureFromAssets();
    }

    private void InitializeRect()
    {
        draggableContainRect = new Rect(selectionRect.position.x + (draggableRect.width / 2), selectionRect.position.y + (draggableRect.height / 2), selectionRect.width - draggableRect.width, selectionRect.height - draggableRect.height);
    }

    private void GetTextureFromAssets()
    {
        // THIS WONT WORK, BUT ISN'T NECESSARY FOR FUNCTIONALITY - FIX LATER
        //selectionTexture = Resources.Load<Texture2D>("Assets/Art/Development/colors");
    }

    private void SetTexture()
    {
        previewColor = selectionColor;
        previewTexture.SetPixel(0, 0, previewColor);
        previewTexture.Apply();
    }

    private void DrawLayouts()
    {
        SetTexture();

        GUI.DrawTextureWithTexCoords(previewRect, previewTexture, new Rect (0, 0, 1, -1));
        GUI.DrawTextureWithTexCoords(selectionRect, selectionTexture, new Rect(0, 0, 1, -1));
    }

    private void DrawDraggableElement()
    {
        draggableTexture = new Texture2D(1, 1);
        draggableTexture.SetPixel(0, 0, Color.black);
        draggableTexture.Apply();

        if(GUI.Button(draggableRect, draggableTexture))
        {
            if (isSelectingColor)
            {
                isSelectingColor = false;
                SetHexColor();
            }
            else
            {
                isSelectingColor = true;
            }
        }

        if(!draggableContainRect.Contains(Event.current.mousePosition))
            return;

        if(isSelectingColor)
        {
            draggableCenter.x = Event.current.mousePosition.x - (draggableRect.width / 2);
            draggableCenter.y = Event.current.mousePosition.y - (draggableRect.height / 2);

            draggableRect.x = draggableCenter.x;
            draggableRect.y = draggableCenter.y;
        }
    }

    private void SetPreviewColor()
    {
        selectionColor = selectionTexture.GetPixel((int)draggableCenter.x - (int)selectionRect.x, ((int)draggableCenter.y - (int)selectionRect.y));
    }

    private void SetHexColor()
    {
        hexCode = ColorUtility.ToHtmlStringRGB(previewColor);
    }

    private void OnGUI()
    {
        DrawLayouts();
        DrawDraggableElement();
        SetPreviewColor();
    }

    public Color RGBA
    {
        get { return previewColor; }
        set { previewColor = value; }
    }

    public string Hex
    {
        get { return hexCode; }
        set { hexCode = value; }
    }
}
