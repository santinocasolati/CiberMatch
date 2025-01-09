using UnityEngine;

public class PhraseController : MonoBehaviour
{
    private int _id;
    private bool _isDraggable = false;

    public void SetValues(int id, string text, bool isDraggable)
    {
        _id = id;
        _isDraggable = isDraggable;
    }
}
