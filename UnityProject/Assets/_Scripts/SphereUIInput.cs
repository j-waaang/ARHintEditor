using UnityEngine;

public class SphereUIInput : MonoBehaviour {

    private bool trackClicks = true;
    private bool creatingEntry = false;

    void Start()
    {
        GameController.gameController.entryCreated += EntryCreated;
    }

	void OnMouseDown()
    {
        if (trackClicks == true && creatingEntry == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo) == true && hitInfo.collider.tag == "SphereUI")
            {
                creatingEntry = true;
                GameController.gameController.ShowEntryCreationMenu(hitInfo.point);
            }
        }
    }

    public void StopTracking()
    {
        trackClicks = false;
        GetComponent<Collider>().enabled = false;
    }

    public void EntryCreated()
    {
        creatingEntry = false;
    }
}
