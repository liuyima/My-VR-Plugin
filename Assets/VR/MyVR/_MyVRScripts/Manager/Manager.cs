using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
    public static Manager manager
    {
        get
        {
            if (_manager == null)
            {
                _manager = FindObjectOfType<Manager>();
            }
            return _manager;
        }
    }
    static Manager _manager;

    public TrackedObjectManager trackedObjectManager;
    public UIManager uiManager;
    public LayerManager layerManager;
    public LoadScene loadScene;
    public VRMove vrMove;
    public ViewController viewController;
    public AudioManager audioManager;
}
