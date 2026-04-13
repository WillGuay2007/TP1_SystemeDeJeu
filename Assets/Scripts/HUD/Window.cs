using UnityEngine;

public class Window : MonoBehaviour
{
    [SerializeField] private int m_layer;
    [SerializeField] private Canvas m_canvas;

    private void Awake()
    {
        m_canvas.sortingOrder = m_layer;
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
