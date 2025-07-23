using TMPro;
using UnityEngine;

public class GameButtonController : MonoBehaviour
{
    public MinMax mm;
    public TextMeshProUGUI objRef;

    void Start()
    {
        objRef.text = "Restart Random";
        objRef.color = new Color32(255, 255, 255, 255);
    }
    public void ActivateAction()
    {
        mm.Restart();
    }
}
