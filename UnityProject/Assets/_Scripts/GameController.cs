using UnityEngine;

public class GameController : MonoBehaviour {

    public enum GameMode { edit, showNearest };

    public delegate void EntryCreatedEventHandler();
    public event EntryCreatedEventHandler entryCreated;

    public static GameController gameController;

    [SerializeField] private GameObject sphereUI;
    [SerializeField] private GameObject entryCreaterPrefab;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject entryGizmoPrefab;
    [SerializeField] private GameObject objectTarget;

    private GameMode gameMode = GameMode.edit;
    private GameObject lastGizmo;
    private EntryManager entryManager;

    void Awake()
    {
        gameController = this;
        entryManager = GetComponent<EntryManager>();
    }

    void Update()
    {
        if(gameMode == GameMode.showNearest)
        {
            entryManager.ShowNearestEntry();
        }
    }

    public void ShowEntryCreationMenu(Vector3 pos)
    {
        lastGizmo = Instantiate(entryGizmoPrefab, pos, Quaternion.identity, objectTarget.transform) as GameObject;
        float scaleFix = 1.0f / 142.5f;
        lastGizmo.transform.localScale *= scaleFix;
        Vector3 spawnPos = Camera.main.WorldToScreenPoint(pos);
        GameObject entryCreator = InstantiateUiOnCanvas(entryCreaterPrefab, spawnPos);
        entryCreator.GetComponentInChildren<EntryCreation>().SaveEntry += AddEntry;
    }

    //Helper method for classes which doesn't have access to canvas.
    public GameObject InstantiateUiOnCanvas(GameObject prefab, Vector3 pos)
    {
        return Instantiate(prefab, pos, Quaternion.identity, canvas.transform) as GameObject;
    }

    public void AddEntry(string head, string body)
    {
        entryManager.AddEntry(head, body, lastGizmo);
        entryCreated();
    }

    public void FinishedEditing()
    {
        sphereUI.GetComponent<SphereUIInput>().StopTracking();
        gameMode = GameMode.showNearest;
    }
}
