using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public string fileLoc = "";
    private StreamReader file;

    [SerializeField]
    private Sprite[] spriteList;
    [SerializeField]
    private SpriteRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        if (!File.Exists(fileLoc))
        {
            Debug.LogError("INVALID FILE LOCATION");
            Destroy(gameObject);
            Application.Quit();
        }
        file = new StreamReader(fileLoc);
        StartCoroutine(MainLogic());
    }


    private IEnumerator MainLogic()
    {

        yield break;
    }

    private void SwapImage(int img)
    {
        _renderer.sprite = spriteList[img];
    }
}
