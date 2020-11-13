using UnityEngine;
using UnityEngine.UI;

public class UpdateTutorialAdvicesText : MonoBehaviour
{
    private Text _text;

    void Awake()
    {
        _text = GetComponent<Text>();
    }

    public void UpdateText(string newText)
    {
        _text.text = newText;
    }
}
