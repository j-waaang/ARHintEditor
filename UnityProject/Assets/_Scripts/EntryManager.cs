using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class EntryManager : MonoBehaviour {

    public struct Entry
    {
        public string head;
        public string body;
        public GameObject gizmo;
    }

    [SerializeField] private Material gizmoMaterial;
    [SerializeField] private Material closestGizmoMaterial;
    [SerializeField] private GameObject entryUI;

    private GameObject currentlyActiveUI = null;
    private GameObject currentlyActiveGizmo = null;
    private int currentlyActiveIndex;
    private List<Entry> entryList;

    void Awake()
    {
        entryList = new List<Entry>();
    }

    public void AddEntry(string head, string body, GameObject gizmo)
    {
        Entry newEntry = new Entry();
        newEntry.head = head;
        newEntry.body = body;
        newEntry.gizmo = gizmo;

        entryList.Add(newEntry);
        Debug.Log("Added " + head + " " + body);

    }

    public void ShowNearestEntry()
    {
        Entry nearestEntry = GetNearestEntry();
        if (nearestEntry.gizmo != currentlyActiveGizmo)
        {
            if(currentlyActiveGizmo != null)
            {
                currentlyActiveGizmo.GetComponent<MeshRenderer>().material = gizmoMaterial;
            }

            currentlyActiveGizmo = nearestEntry.gizmo;
            currentlyActiveGizmo.GetComponent<MeshRenderer>().material = closestGizmoMaterial;
            Destroy(currentlyActiveUI);
            SpawnEntryUI();
        }
        UpdateUIPosition();
    }

    private Entry GetNearestEntry()
    {

        float smallestDistance = float.PositiveInfinity;
        int indexWithSmallestDistance = -1;
        Plane cameraPlane = GetCameraPlane();

        for(int i = 0; i<entryList.Count; ++i)
        {
            float distance = cameraPlane.GetDistanceToPoint(entryList[i].gizmo.transform.position);
            if (distance < smallestDistance)
            {
                smallestDistance = distance;
                indexWithSmallestDistance = i;
                currentlyActiveIndex = i;
            }
        }

        return entryList[indexWithSmallestDistance];
    }

    private Plane GetCameraPlane()
    {
        Transform camTransform = Camera.main.transform;

        Plane cameraPlane = new Plane(
            camTransform.position + camTransform.up,
            camTransform.position - camTransform.right,
            camTransform.position - camTransform.up);

        return cameraPlane;
    }

    private void SpawnEntryUI()
    {
        currentlyActiveUI = GameController.gameController.InstantiateUiOnCanvas(entryUI, currentlyActiveGizmo.transform.position);
        Text[] texts = currentlyActiveUI.GetComponentsInChildren<Text>();
        if(texts[0].text == "Body")
        {
            Text tmp = texts[1];
            texts[1] = texts[0];
            texts[0] = tmp;
        }
        texts[0].text = entryList[currentlyActiveIndex].head;
        texts[1].text = entryList[currentlyActiveIndex].body;
    }

    private void UpdateUIPosition()
    {
        currentlyActiveUI.transform.position = Camera.main.WorldToScreenPoint(currentlyActiveGizmo.transform.position);
    }
}
