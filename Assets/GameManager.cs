using NodeSystem;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private NodeManager nodeManager;
    [SerializeField] private ColorFader colorFader;
    [SerializeField] private CharacterAppearence characterAppearence;
    [SerializeField] private GenderHandler genderHandler;

    [SerializeField] private Button buttonFemale;
    [SerializeField] private Button buttonMale;

    [SerializeField] private GameObject GMFemale;
    [SerializeField] private GameObject GMMale;

    private GameObject character;

    private void Start()
    {
        buttonFemale.onClick.AddListener(InitFemale);
        buttonMale.onClick.AddListener(InitMale);
    }

    private void InitFemale()
    {
        genderHandler.SetGender(GMFemale);
        Init();
    }

    private void InitMale()
    {
        genderHandler.SetGender(GMMale);
        Init();
    }

    private void Init()
    {
        nodeManager.Init();
        characterAppearence.Character = genderHandler.character.GetComponent<Character>();

        colorFader.StartFadeOut();
    }
}
