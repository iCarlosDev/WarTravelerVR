using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class XR_MyDirectInteractor : XRDirectInteractor
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.TryGetComponent(out XR_TriggerGrabbable xrTriggerGrabbable))
        {
            XR_InputDetector xrInputDetector = args.interactorObject.transform.GetComponent<XR_InputDetector>();
            if (!xrInputDetector.IsTriggering)return;
        }

        base.OnSelectEntered(args);
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.TryGetComponent(out XR_TriggerGrabbable xrTriggerGrabbable))
        {
            XR_InputDetector xrInputDetector = args.interactorObject.transform.GetComponent<XR_InputDetector>();
            if (!xrInputDetector.IsTriggering) return;
        }

        base.OnSelectEntering(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        XR_InputDetector xrInputDetector = args.interactorObject.transform.GetComponent<XR_InputDetector>();
        Magazine magazine = args.interactableObject.transform.GetComponent<Magazine>();
        XR_Slider xrSlider = args.interactableObject.transform.GetComponent<XR_Slider>();

        if (magazine != null)
        {
            if (xrInputDetector.IsTriggering && !magazine.IsBeingInserted) return;
        }

        if (xrSlider != null)
        {
            if (xrSlider.MHandle.TryGetComponent(out Magazine sliderMagazine))
            {
                if (xrInputDetector.IsTriggering && sliderMagazine.IsBeingInserted) return;
            }

            if (xrInputDetector.IsTriggering && xrSlider.CompareTag("WeaponSlide")) return;
        }
        
        base.OnSelectExited(args);
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        XR_InputDetector xrInputDetector = args.interactorObject.transform.GetComponent<XR_InputDetector>();
        Magazine magazine = args.interactableObject.transform.GetComponent<Magazine>();
        XR_Slider xrSlider = args.interactableObject.transform.GetComponent<XR_Slider>();

        if (magazine != null)
        {
            if (xrInputDetector.IsTriggering && !magazine.IsBeingInserted) return;
        }

        if (xrSlider != null)
        {
            if (xrSlider.MHandle.TryGetComponent(out Magazine sliderMagazine))
            {
                if (xrInputDetector.IsTriggering && sliderMagazine.IsBeingInserted) return;
            }

            if (xrInputDetector.IsTriggering && xrSlider.CompareTag("WeaponSlide")) return;
        }
        
        base.OnSelectExiting(args);
    }
}
