using UnityEngine;

//V2 parce que je garde le premier (vu qu'on a pas besoin de refactor) (mais j'ai refactor d'autre trucs pareil.)
public class HUDControllerV2 : MonoBehaviour
{
    private static HUDControllerV2 _instance;

    public static HUDControllerV2 Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("HUDController is null");
            return _instance;
        }
    }

    public void OpenWindow(Window window)
    {
        window.Show();
    }

    public void CloseWindow(Window window)
    {
        window.Hide();
    }

    private void Awake()
    {
        _instance = this;
    }
}

