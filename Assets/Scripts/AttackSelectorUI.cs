using UnityEngine;
using UnityEngine.UI;

public class AttackSelectorUI : MonoBehaviour
{
    public Image[] noteHighlights;  // Drag highlight panels in order: Z, X, C, V, B, N, M

    private bool[] isNoteSelected = new bool[7];  // true = selected

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) { ToggleNote(0); }
        if (Input.GetKeyDown(KeyCode.X)) { ToggleNote(1); }
        if (Input.GetKeyDown(KeyCode.C)) { ToggleNote(2); }
        if (Input.GetKeyDown(KeyCode.V)) { ToggleNote(3); }
        if (Input.GetKeyDown(KeyCode.B)) { ToggleNote(4); }
        if (Input.GetKeyDown(KeyCode.N)) { ToggleNote(5); }
        if (Input.GetKeyDown(KeyCode.M)) { ToggleNote(6); }
    }

    void ToggleNote(int index)
    {
        isNoteSelected[index] = !isNoteSelected[index]; // toggle on/off
        noteHighlights[index].gameObject.SetActive(isNoteSelected[index]);
    }

    // Optional: Expose selected note states to other scripts (like PlayerAttack)
    public bool[] GetSelectedNotes()
    {
        return isNoteSelected;
    }
}
