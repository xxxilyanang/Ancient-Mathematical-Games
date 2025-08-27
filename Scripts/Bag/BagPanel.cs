using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPanel : MonoBehaviour
{
    public GameObject mybag;
    public Button btnbook;
    private bool isShow = true;
    public Button btntool;
    public GameObject bookbag;
    public GameObject toolbag;

    private bool IsOpen;
    // Start is called before the first frame update
    void Start()
    {

        btnbook.onClick.AddListener(BookOpen);


        btntool.onClick.AddListener(ToolOpen);


    }
    void BookOpen()
    {
        bookbag.SetActive(isShow);
        toolbag.SetActive(!isShow);
    }
    void ToolOpen()
    {
        toolbag.SetActive(isShow);
        bookbag.SetActive(!isShow);
    }
    // Update is called once per frame
    void Update()
    {
        OPenmybag();
    }
    private void OPenmybag()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsOpen = !IsOpen;
            mybag.SetActive(IsOpen);
        }
    }
}
