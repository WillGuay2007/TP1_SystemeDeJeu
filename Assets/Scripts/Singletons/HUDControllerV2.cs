using UnityEngine;

//V2 parce que je garde le premier (vu qu'on a pas besoin de refactor) (mais j'ai refactor d'autre trucs pareil.)
//Je l'ai changé pour mieux me retrouver mais l'idée reste la meme (Héritage + windows + open close principle)
public class HUDControllerV2 : MonoBehaviour
{
    public static HUDControllerV2 Instance { get; private set; }

    public void OpenWindow(Window window) => window.Show();

    public void CloseWindow(Window window) => window.Hide();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
}

