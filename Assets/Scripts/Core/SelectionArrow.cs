using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    private RectTransform rect;
    private int currentPos;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {   
        // position of arrow
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangePosition(-1);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangePosition(1);
        }

        // interact with options
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }
    }

    private void ChangePosition(int _change)
    {
        currentPos += _change;

        if (currentPos < 0)
        {
            currentPos = options.Length - 1;
        }
        else if (currentPos > options.Length - 1)
        {
            currentPos = 0;
        }

        // Y position of arrow when selecting options
        rect.position = new Vector3(rect.position.x, (float)((options[currentPos].position.y) + 15), 0);
    }

    private void Interact()
    {
        options[currentPos].GetComponent<Button>().onClick.Invoke();
    }
}
