using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip changeSound;
    [SerializeField] private AudioClip interactSound;
    private RectTransform rect;
    private int currentPosition;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    private void Update()
    {
        //thay doi vi tri lua chon cua mui ten
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangePositon(-1);
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) 
            ChangePositon(1);
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
            Interact();
        //

    }
    private void ChangePositon(int _change)
    {
        currentPosition += _change;
        if(currentPosition < 0)
            currentPosition = options.Length - 1;
        else if(currentPosition > options.Length - 1)
            currentPosition = 0;

        // gan vi tri cua Y vao mui ten
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
    }
    private void Interact()
    {
        //sound
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
