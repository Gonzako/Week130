using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosessionInteractive : MonoBehaviour
{
    [SerializeField] private GameObject _interact;

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
        _interact.transform.position = character.transform.position + new Vector3(1F, 1.5F);
        _interact.gameObject.SetActive(true);
    }

    private void InteractionUIPromptEnd(possesableMovement character)
    {
        _interact.gameObject.SetActive(false);
    }

}
