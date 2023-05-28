using UnityEngine;

public class GrabAndPose : MonoBehaviour
{
    [SerializeField] private HandData _leftHandPose;

    private Vector3 _startingHandPosition;
    private Vector3 _finalHandPosition;
    private Quaternion _startingHandRotation;
    private Quaternion _finalHandRotation;

    private Quaternion[] _startingFingerRotations;
    private Quaternion[] _finalFingerRotations;

    private void Start()
    {
        _leftHandPose.gameObject.SetActive(false);
    }

    public void SetupPose(HandData handData)
    {
        SetHandDataValues(handData, _leftHandPose);
        SetHandData(handData, _finalHandPosition, _finalHandRotation, _finalFingerRotations);
    }

    private void SetHandDataValues(HandData mainHand, HandData handPose)
    {
        _startingHandPosition = mainHand.Root.localPosition;
        _finalHandPosition = handPose.Root.localPosition;

        _startingHandRotation = mainHand.Root.localRotation;
        _finalHandRotation = handPose.Root.localRotation;

        _startingFingerRotations = new Quaternion[mainHand.FingerBones.Length];
        _finalFingerRotations = new Quaternion[mainHand.FingerBones.Length];

        for (int i = 0; i < mainHand.FingerBones.Length; i++)
        {
            _startingFingerRotations[i] = mainHand.FingerBones[i].localRotation;
            _finalFingerRotations[i] = handPose.FingerBones[i].localRotation;
        }
    }

    private void SetHandData(HandData mainHand, Vector3 newPosition, Quaternion newRotation, Quaternion[] newBonesRotation)
    {
        mainHand.Root.localPosition = newPosition;
        mainHand.Root.localRotation = newRotation;

        for (int i = 0; i < newBonesRotation.Length; i++)
        {
            mainHand.FingerBones[i].localRotation = newBonesRotation[i];
        }
    }
}
