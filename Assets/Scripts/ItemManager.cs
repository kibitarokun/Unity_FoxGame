using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private GameObject K;

    // Start is called before the first frame update
    void Start()
    {
        K.SetActive(false);       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //SwitchAction‚ÅOn‚É‚È‚é‚Æ”­“®‚·‚éƒƒ\ƒbƒh
    public void ActiveK()
    {
        K.SetActive(true);
    }
}
