using UnityEngine.XR.Interaction.Toolkit;

public class Single_XR_GrabInteractable : XRGrabInteractable
{
    /*protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.GetComponent<XR_InputDetector>().GripInput.action.ReadValue<float>() == 0) return;
        base.OnSelectEntered(args);
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.GetComponent<XR_InputDetector>().GripInput.action.ReadValue<float>() == 0) return;
        base.OnSelectEntering(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        if (args.interactorObject.transform.GetComponent<XR_InputDetector>().GripInput.action.ReadValue<float>() <= 1) return;
        base.OnSelectExited(args);
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        if (args.interactorObject.transform.GetComponent<XR_InputDetector>().GripInput.action.ReadValue<float>() <= 1) return;
        base.OnSelectExiting(args);
    }*/
}
