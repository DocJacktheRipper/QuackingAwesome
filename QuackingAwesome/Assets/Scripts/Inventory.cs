using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int numberOfSticks;
    public int maxCapacityOfSticks = 1;    
    
    public bool enableDuckbillVisual;
    public GameObject branchVisual;

    public ProgressBar Pb;
    
    private void Start()
    {
        Pb.BarValue = 10f;
    }

    public void DeleteSticks(int number)
    {
        for (var i = 0; i < number; i++)
        {
            GameObject child = transform.GetChild(0).gameObject;
            if (child != null)
            {
                Destroy(child);
            }
        }
    }

    public void DeleteAllSticks()
    {
        DeleteSticks(numberOfSticks);
    }
    
    public void ShowSticksInDuckbill()
    {
        if (!enableDuckbillVisual)
        {
            return;
        }
        
        var stick = Instantiate(branchVisual, transform, true);
        stick.transform.localPosition = new Vector3(0.00037f, 0.00869f, 0.00588f);
        stick.transform.eulerAngles = new Vector3(0f, -90f, 0f);
    }
}
