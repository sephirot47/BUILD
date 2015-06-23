using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class CanvasUtils
{

    public static void SetAlpha(GameObject go, float alpha)
    {
        if (go == null) return;
        if (go.GetComponent<CanvasGroup>() != null)
        {
            go.GetComponent<CanvasGroup>().alpha = alpha;
        }
        else if (go.GetComponent<CanvasRenderer>() != null)
        {
            go.GetComponent<CanvasRenderer>().SetAlpha(alpha);
        }
    }

    public static void Show(GameObject go)
    {
        SetAlpha(go, 1.0f);
    }

    public static void Hide(GameObject go)
    {
        SetAlpha(go, 0.0f);
    }

    public static bool MouseOver(GameObject go)
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, results);

        foreach (RaycastResult rr in results)
        {
            if (ParentContains(go, rr.gameObject)) return true;
            if (rr.gameObject == go) return true;
        }

        return false;
    }

    public static bool ParentContains(GameObject parent, GameObject child)
    {
        foreach(Transform t in parent.transform)
            if (ParentContains(t.gameObject, child)) return true;
        return false;
    }

    public static void ShowCursor()
    {
        Cursor.visible = true;
    }

    public static void HideCursor()
    {
        Cursor.visible = false;
    }
}