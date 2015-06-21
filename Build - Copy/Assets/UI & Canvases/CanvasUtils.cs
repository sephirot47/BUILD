using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class CanvasUtils
{
    public static void Show(GameObject go)
    {
        if (go == null) return;
        if (go.GetComponent<CanvasGroup>() != null)
        {
            go.GetComponent<CanvasGroup>().alpha = 1;
        }
        else if (go.GetComponent<CanvasRenderer>() != null)
        {
            go.GetComponent<CanvasRenderer>().SetAlpha(1);
        }
    }

    public static void Show(GameObject go, float alpha)
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

    public static void Hide(GameObject go)
    {
        if (go == null) return;
        if (go.GetComponent<CanvasGroup>() != null)
        {
            go.GetComponent<CanvasGroup>().alpha = 0;
        }
        else if (go.GetComponent<CanvasRenderer>() != null)
        {
            go.GetComponent<CanvasRenderer>().SetAlpha(0);
        }
    }

    public static bool MouseOver(GameObject go)
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, results);

        foreach (RaycastResult rr in results)
        {
            if (rr.gameObject == go) return true;
        }

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