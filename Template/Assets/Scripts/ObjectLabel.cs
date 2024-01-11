using UnityEngine;

public class ObjectLabel : MonoBehaviour
{
    public string objectName = "Object";
    public string description = "Description of the object";
    public Vector3 textOffset = new Vector3(-1, 0, 0); // Offset from the object's position

    void OnGUI()
    {
        Transform objectTransform = transform;

        // Calculate the target position considering object scale and text offset
        Vector3 targetPosition = objectTransform.position + Vector3.Scale(objectTransform.localScale, textOffset);

        Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(targetPosition);
        targetScreenPos.y = Screen.height - targetScreenPos.y;

        GUIStyle styleNameText = new GUIStyle();
        styleNameText.normal.textColor = Color.white;
        styleNameText.font = Resources.Load<Font>("Fonts/Roboto-Regular"); // Assuming the font is in "Assets/Resources/Fonts" folder
        styleNameText.fontSize = 36;
        styleNameText.alignment = TextAnchor.UpperLeft;

        GUI.Label(new Rect(targetScreenPos.x - 75, targetScreenPos.y - 50, 200, 50), objectName, styleNameText);

        GUIStyle styleDescriptionText = new GUIStyle();
        styleDescriptionText.normal.textColor = Color.white;
        styleDescriptionText.font = Resources.Load<Font>("Fonts/Roboto-Regular"); // Assuming the font is in "Assets/Resources/Fonts" folder
        styleDescriptionText.fontSize = 18;
        styleDescriptionText.alignment = TextAnchor.UpperLeft;

        GUI.Label(new Rect(targetScreenPos.x - 75, targetScreenPos.y, 300, 50), description, styleDescriptionText);
    }
}
