using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Doozy;
using Doozy.Engine.UI;

public class PosessionInteractive : MonoBehaviour
{
    [SerializeField] private RectTransform _interact;

    public string ViewCategory;
    public string ViewName;

    private void OnEnable()
    {
       
        PossessController.onAnyPosessionPrompt += InteractionUIPrompt;
        PossessController.onAnyPosessionPromptEnd += InteractionUIPromptEnd;
    }

    private void OnDisable()
    {
        PossessController.onAnyPosessionPrompt -= InteractionUIPrompt;
        PossessController.onAnyPosessionPromptEnd -= InteractionUIPromptEnd;
    }

    private void InteractionUIPrompt(possesableMovement character)
    {
        /*
        _interact.RectTransform.position = Camera.main.WorldToScreenPoint
            (character.transform.position + new Vector3(0F, 1.5F));
        _interact.Show();
       */
        _interact.anchoredPosition = Camera.main.WorldToScreenPoint
            (character.transform.position + new Vector3(0F, 1.5F));
    }

    private void InteractionUIPromptEnd(possesableMovement character)
    {
        //_interact.Hide(true);
    }

}
