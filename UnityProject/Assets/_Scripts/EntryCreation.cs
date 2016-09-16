using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EntryCreation : MonoBehaviour, IPointerDownHandler{

    public delegate void SaveEntryEventHandler(string head, string body);
    public event SaveEntryEventHandler SaveEntry;

    [SerializeField] private Text head;
    [SerializeField] private Text body;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (head.text != "" && body.text != "")
        {
            SaveEntry(head.text, body.text);
            Destroy(transform.parent.gameObject);
        }
    }
}
