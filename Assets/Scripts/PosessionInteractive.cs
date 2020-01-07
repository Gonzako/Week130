using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy;
using Doozy.Engine.UI;
using UnityEngine.UI;

public class PosessionInteractive : MonoBehaviour
{
    [SerializeField] private RectTransform _interact;
    public float verticalAdder = 0.1f;
    public float sizeMultiplier = 2;
    public string ViewCategory;
    public string ViewName;

    private Camera cam;

    private void OnEnable()
    {
       
        PossessController.onAnyPosessionPrompt += InteractionUIPrompt;
        PossessController.onAnyPosessionPromptEnd += InteractionUIPromptEnd;
        cam = Camera.main;
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
        _interact.GetComponent<Image>().enabled = true;
        _interact.GetComponentInChildren<TMPro.TextMeshProUGUI>().enabled = true;
        _interact.position = worldToUISpace(_interact.GetComponentInParent<Canvas>(), 
            character.transform.position + new Vector3(0, character.GetComponent<Collider2D>().bounds.extents.y + verticalAdder));
        character.GetComponentInChildren<SpriteRenderer>().material.SetFloat("_OutlineEnabled", 1.0F);
    }

    private void InteractionUIPromptEnd(possesableMovement character)
    {

        _interact.GetComponent<Image>().enabled = false;
        _interact.GetComponentInChildren<TMPro.TextMeshProUGUI>().enabled = false;
        character.GetComponentInChildren<SpriteRenderer>().material.SetFloat("_OutlineEnabled", 0.0F);
    }

    public Vector3 worldToUISpace(Canvas parentCanvas, Vector3 worldPos)
    {
        //Convert the world for screen point so that it can be used with ScreenPointToLocalPointInRectangle function
        Vector3 screenPos = cam.WorldToScreenPoint(worldPos);
        Vector2 movePos;

        //Convert the screenpoint to ui rectangle local point
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out movePos);
        //Convert the local point to world point
        return parentCanvas.transform.TransformPoint(movePos);
    }
}
