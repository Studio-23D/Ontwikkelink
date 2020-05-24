using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtensionButton : MonoBehaviour
{
    [SerializeField] private Animator extensionContainer;

    public Animator ExtensionContainerAnimator => extensionContainer;

    private void Awake()
    {
        this.GetComponent<Animator>().GetBehaviour<ExtendButtonState>().SetAnimator(extensionContainer);
    }
}
