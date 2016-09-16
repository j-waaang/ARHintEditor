using UnityEngine;
using UnityEngine.EventSystems;

public class DestroySelfHelper : MonoBehaviour {

   public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
