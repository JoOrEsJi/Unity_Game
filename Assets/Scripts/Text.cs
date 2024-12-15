using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text : MonoBehaviour
{
    public bool active;
    public GameObject go;
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;
    internal int fontSize;
    internal string text;
    internal Color color;
    internal TextAnchor alignment;
    internal Font font;

    public void Show()
    {
        active = true;
        lastShown = Time.time;
        go.SetActive(true);
    }

    public void Hide()
    {
        active = false;
        go.SetActive(active);
    }

    public void UpdateFloatingText()
    {
        if (!active)
            return;
        if (Time.time - lastShown > duration)
            Hide();
        go.transform.position += motion * Time.deltaTime;
    }
}
