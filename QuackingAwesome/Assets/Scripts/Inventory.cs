using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int numberOfSticks;
    public int maxCapacityOfSticks = 1;    
    
    public ProgressBar Pb;
    
    private void Start()
    {
        Pb.BarValue = 10f;
    }
}
