using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class AttackSelectorUI : MonoBehaviour
{
    public Image[] noteHighlights;  // Drag highlight panels in order: Z, X, C, V, B, N, M

    private bool[] isNoteSelected = new bool[7];  // true = selected

    void Update()
    {
        // Collect all keys pressed this frame
        List<int> newlyPressed = new();

        if (Input.GetKeyDown(KeyCode.Z)) newlyPressed.Add(0);
        if (Input.GetKeyDown(KeyCode.X)) newlyPressed.Add(1);
        if (Input.GetKeyDown(KeyCode.C)) newlyPressed.Add(2);
        if (Input.GetKeyDown(KeyCode.V)) newlyPressed.Add(3);
        if (Input.GetKeyDown(KeyCode.B)) newlyPressed.Add(4);
        if (Input.GetKeyDown(KeyCode.N)) newlyPressed.Add(5);
        if (Input.GetKeyDown(KeyCode.M)) newlyPressed.Add(6);

        // Only update if at least one key was pressed this frame
        if (newlyPressed.Count > 0)
        {
            for (int i = 0; i < isNoteSelected.Length; i++)
            {
                bool selected = newlyPressed.Contains(i);
                isNoteSelected[i] = selected;
                noteHighlights[i].gameObject.SetActive(selected);
            }
        }
    }

    // void ToggleNote(int index)
    // {
    //     isNoteSelected[index] = !isNoteSelected[index]; // toggle on/off
    //     noteHighlights[index].gameObject.SetActive(isNoteSelected[index]);
    // }

    public bool[] GetSelectedNotes()
    {
        return isNoteSelected;
    }
}
