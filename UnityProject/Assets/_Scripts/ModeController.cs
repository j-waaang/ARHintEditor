using UnityEngine;
using System.Collections;

public class ModeController : MonoBehaviour {

    public enum Mode
    {
        Edit,
        View
    };

    [SerializeField] private GameObject EditUI;
    [SerializeField] private GameObject ViewUI;

    private Mode mode = Mode.View;

    public void SwitchMode(Mode newMode)
    {

    }

}
