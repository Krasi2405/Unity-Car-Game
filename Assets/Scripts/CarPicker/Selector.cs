using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private Button nextItemButton;

    [SerializeField]
    private Button previousItemButton;

    private Dictionary<Sprite, Object> selectorDictionary;
    private List<Sprite> optionsList;
    int index = 0;

    public void SetSelectorDictionary(Dictionary<Sprite, Object> selectorDictionary, int currentSelectionIndex = 0)
    {
        this.selectorDictionary = selectorDictionary;

        optionsList = new List<Sprite>();
        foreach(Sprite sprite in selectorDictionary.Keys)
        {
            optionsList.Add(sprite);
        }
        UpdateCurrentSelection();
    }


    public Object GetCurrentSelection()
    {
        return selectorDictionary[optionsList[index]];
    }


    private void Awake()
    {
        nextItemButton.onClick.AddListener(SelectNext);
        previousItemButton.onClick.AddListener(SelectPrevious);
    }

    private void SelectNext()
    {
        index++;
        if (index >= optionsList.Count)
        {
            index = 0;
        }
        UpdateCurrentSelection();
    }


    private void SelectPrevious()
    {
        index--;
        if (index < 0)
        {
            index = optionsList.Count - 1;
        }
        UpdateCurrentSelection();
    }


    private void UpdateCurrentSelection()
    {
        image.sprite = optionsList[index];
    }
}
