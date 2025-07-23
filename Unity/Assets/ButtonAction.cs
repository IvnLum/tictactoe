using Unity.Mathematics.Geometry;
using UnityEngine;

public class ButtonAction : MonoBehaviour
{
    public MinMax mm;
    public int idX = 0;
    public int idY = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     /*   mm = GetComponent<MinMax>();
        if (mm == null)
            Debug.LogError("MinMax not found in parent.");*/
    }

    // Update is called once per frame
    public void ActivateAction()
    {
        mm.ActivateButton(idX, idY);
    }
}
