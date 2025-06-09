using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class AttackSelectorUI : MonoBehaviour
{
    public Image[] noteHighlights;  // Drag highlight panels in order: 1, 2, 3, 4, 5, 6, 7

    private bool[] isNoteSelected = new bool[7];  // true = selected

    void Update()
    {
        // Collect all keys pressed this frame
        List<int> newlyPressed = new();

        if (Input.GetKeyDown(KeyCode.Alpha1)) newlyPressed.Add(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) newlyPressed.Add(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) newlyPressed.Add(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) newlyPressed.Add(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) newlyPressed.Add(4);
        if (Input.GetKeyDown(KeyCode.Alpha6)) newlyPressed.Add(5);
        if (Input.GetKeyDown(KeyCode.Alpha7)) newlyPressed.Add(6);

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
