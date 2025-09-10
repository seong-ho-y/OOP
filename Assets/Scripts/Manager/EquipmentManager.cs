using Manager;
using UnityEngine;


public class EquipmentManager : MonoBehaviour
{
    public BaseEquipmentData[] currentEquipment;
    
    #region Singleton
    public static EquipmentManager instance {get; private set;}
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new BaseEquipmentData[numSlots];
    }
    #endregion
    

    private StatManager statManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EquipItem(BaseEquipmentData equipment)
    {
        int slotIndex = (int)equipment.equipSlot;
        currentEquipment[slotIndex] = equipment;
    }

    public void UnEquipItem(IEquipable equipment)
    {
        
    }

    public BaseEquipmentData GetEquipItem(EquipmentSlot slot)
    {
        return currentEquipment[(int)slot];
    }
}
