using NodeSystem;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private NodeManager nodeManager;
    [SerializeField] private ColorFader colorFader;
    [SerializeField] private CharacterAppearance characterAppearance;
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
        character = genderHandler.SetGender(GMFemale);
        Init();
    }

    private void InitMale()
    {
        character = genderHandler.SetGender(GMMale);
        Init();
    }

    private void Init()
    {
        nodeManager.Init();
        characterAppearance.Character = character.GetComponent<Character>();

        colorFader.StartFadeOut();
    }
}
