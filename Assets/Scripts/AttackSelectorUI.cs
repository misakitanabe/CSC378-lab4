using UnityEngine;
using UnityEngine.UI;

public class AttackSelectorUI : MonoBehaviour
{
    public Image[] noteHighlights;

    private int selectedIndex = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { SelectNote(0); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { SelectNote(1); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { SelectNote(2); }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { SelectNote(3); }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { SelectNote(4); }
        if (Input.GetKeyDown(KeyCode.Alpha6)) { SelectNote(5); }
        if (Input.GetKeyDown(KeyCode.Alpha7)) { SelectNote(6); }
    }

    void SelectNote(int index)
    {
        selectedIndex = index;
        Debug.Log("Selected note index: " + index);

        for (int i = 0; i < noteHighlights.Length; i++)
        {
            noteHighlights[i].gameObject.SetActive(i == selectedIndex);
        }
    }
}
