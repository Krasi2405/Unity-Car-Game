using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickerSystem : MonoBehaviour
{
    public event System.EventHandler OnReady;
    public event System.EventHandler OnCancelReady;


    [SerializeField] 
    private int playerIndex = 0;


    [SerializeField] 
    private Selector carSelector = null;
    
    [SerializeField] 
    private Selector gunSelector = null;

    [SerializeField] 
    private Button selectButton = null;
    
    [SerializeField] 
    private Button backButton = null;
    
    [SerializeField] 
    private Button readyCancelButton = null;
    
    [SerializeField] 
    private RectTransform readyPanel = null;


    [SerializeField] 
    private CarList carList = null;

    [SerializeField] 
    private GunList gunList = null;


    private enum Stage
    {
        PickCar,
        PickGun,
        Ready
    }

    private Stage currentStage = Stage.PickCar;

    private int selectedCarIndex;
    private int selectedGunIndex;
    private bool isReady = false;

    private void Awake()
    {
        carSelector.gameObject.SetActive(true);
        gunSelector.gameObject.SetActive(false);
        readyPanel.gameObject.SetActive(false);

        selectButton.onClick.AddListener(NextStage);
        backButton.onClick.AddListener(PreviousStage);
        readyCancelButton.onClick.AddListener(PreviousStage);

        Dictionary<Sprite, Object> carDictionary = new Dictionary<Sprite, Object>();
        foreach(CarSO car in carList.cars)
        {
            carDictionary.Add(car.iconRepresentation, car);
        }
        carSelector.SetSelectorDictionary(carDictionary);

        Dictionary<Sprite, Object> gunDictionary = new Dictionary<Sprite, Object>();
        foreach (GunSO gun in gunList.guns)
        {
            gunDictionary.Add(gun.iconRepresentation, gun);
        }
        gunSelector.SetSelectorDictionary(gunDictionary);
    }

    private void NextStage()
    {
        if (currentStage == Stage.PickCar)
        {
            selectedCarIndex = carList.cars.IndexOf((CarSO) carSelector.GetCurrentSelection());
            backButton.gameObject.SetActive(true);
            carSelector.gameObject.SetActive(false);
            gunSelector.gameObject.SetActive(true);
            currentStage = Stage.PickGun;
        }
        else if(currentStage == Stage.PickGun)
        {
            selectedGunIndex = gunList.guns.IndexOf((GunSO)gunSelector.GetCurrentSelection());
            gunSelector.gameObject.SetActive(false);
            readyPanel.gameObject.SetActive(true);
            selectButton.gameObject.SetActive(false);
            
            currentStage = Stage.Ready;
            isReady = true;
            OnReady?.Invoke(this, System.EventArgs.Empty);

            PlayerPrefs.SetInt("SelectedCar" + playerIndex, selectedCarIndex);
            PlayerPrefs.SetInt("SelectedGun" + playerIndex, selectedGunIndex);
        }
    }

    private void PreviousStage()
    {
        if(currentStage == Stage.PickCar) { return; }

        if(currentStage == Stage.PickGun)
        {
            backButton.gameObject.SetActive(false);
            carSelector.gameObject.SetActive(true);
            gunSelector.gameObject.SetActive(false);
            currentStage = Stage.PickCar;
        }
        else if(currentStage == Stage.Ready)
        {
            gunSelector.gameObject.SetActive(true);
            readyPanel.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);
            currentStage = Stage.PickGun;

            isReady = false;
            OnCancelReady?.Invoke(this, System.EventArgs.Empty);
        }
    }

    public bool IsReady()
    {
        return isReady;
    }

}
