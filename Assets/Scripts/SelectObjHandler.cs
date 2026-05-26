using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UiHandler : MonoBehaviour
{
    public TextMeshProUGUI selectedObjText;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit)){
                // set ui text
                selectedObjText.SetText("Selected: " + hit.collider.name + ", Position: " + hit.collider.transform.position + ".");
            }
        }        
    }
}
