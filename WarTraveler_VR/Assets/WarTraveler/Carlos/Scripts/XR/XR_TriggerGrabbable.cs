using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class XR_TriggerGrabbable : XRGrabInteractable
{

   protected override void OnSelectEntered(SelectEnterEventArgs args)
   {
      XR_InputDetector xrInputDetector = args.interactorObject.transform.GetComponent<XR_InputDetector>();
      if (!xrInputDetector.IsTriggering)return;

      base.OnSelectEntered(args);
   }

   protected override void OnSelectEntering(SelectEnterEventArgs args)
   {
      XR_InputDetector xrInputDetector = args.interactorObject.transform.GetComponent<XR_InputDetector>();
      if (!xrInputDetector.IsTriggering) return;
      
      base.OnSelectEntering(args);
   }

   protected override void OnSelectExited(SelectExitEventArgs args)
   {
      XR_InputDetector xrInputDetector = args.interactorObject.transform.GetComponent<XR_InputDetector>();
      Magazine magazine = args.interactableObject.transform.GetComponent<Magazine>();

      if (magazine != null)
      {
         if (xrInputDetector.IsTriggering && !magazine.IsBeingInserted) return;
      }

      Debug.Log($"MAGAZINE EXITED");
      base.OnSelectExited(args);
   }

   protected override void OnSelectExiting(SelectExitEventArgs args)
   {
      XR_InputDetector xrInputDetector = args.interactorObject.transform.GetComponent<XR_InputDetector>();
      Magazine magazine = args.interactableObject.transform.GetComponent<Magazine>();

      if (magazine != null)
      {
         if (xrInputDetector.IsTriggering && !magazine.IsBeingInserted) return;
      }

      Debug.Log($"MAGAZINE EXITING");
      base.OnSelectExiting(args);
   }
}
