using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XR_MyDirectInteractor : XRDirectInteractor
{
    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        if (args.interactableObject.transform.CompareTag("AmmoPouch"))
        {
            Debug.Log("HOVEEEER");
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        
        Debug.Log("olaaaaaaa");
        if (args.interactableObject.transform.CompareTag("AmmoPouch"))
        {
            Debug.Log("OYUYEYYEY");
        }
    }
}
