using UnityEngine;
using UnityEngine.UI;

public class AttackSelectorUI : MonoBehaviour
{
    public Image[] noteHighlights;

    private int selectedIndex = 0;

    void Update()
    {
    if (Input.GetKeyDown(KeyCode.Z)) { SelectNote(0); }
    if (Input.GetKeyDown(KeyCode.X)) { SelectNote(1); }
    if (Input.GetKeyDown(KeyCode.C)) { SelectNote(2); }
    if (Input.GetKeyDown(KeyCode.V)) { SelectNote(3); }
    if (Input.GetKeyDown(KeyCode.B)) { SelectNote(4); }
    if (Input.GetKeyDown(KeyCode.N)) { SelectNote(5); }
    if (Input.GetKeyDown(KeyCode.M)) { SelectNote(6); }
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
