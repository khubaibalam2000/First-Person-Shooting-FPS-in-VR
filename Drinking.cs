using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drinking : MonoBehaviour
{
    private Animator animCanteen;
    public GameObject canteen;
    private Inventory invent;
    private PauseMenu pm;
    private bool canteenActive;
    private int count1;

    // Start is called before the first frame update
    void Start()
    {
        count1 = 0;
        canteenActive = false;
        pm = GameObject.Find("Canvas").GetComponent<PauseMenu>();
        animCanteen = canteen.GetComponent<Animator>();
        invent = GetComponent<Inventory>();
        canteen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5) && invent.enable[4])
        {
            canteenActive = true;
            canteen.SetActive(true);
            count1++;
        }
        if (count1 % 2 == 0)
        {
            canteenActive = false;
            canteen.SetActive(false);
        }
        if (canteenActive && !pm.gameIsPause)
        {
            animCanteen.Play("drinking2");
            StartCoroutine(Drinking());
        }
        IEnumerator Drinking()
        {
            yield return new WaitForSeconds(1f);
            canteenActive = false;
            canteen.SetActive(false);
        }
    }
}