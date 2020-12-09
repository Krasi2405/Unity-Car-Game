using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickerSystem : MonoBehaviour
{
    [SerializeField]
    private int playerIndex;

    [SerializeField]
    private Selector carSelector;

    [SerializeField]
    private Selector gunSelector;

    [SerializeField]
    private RectTransform readyPanel;

    [SerializeField]
    private Button selectButton;

    [SerializeField]
    private Button backButton;

    [SerializeField]
    private Button readyCancelButton;

    [SerializeField]
    private CarList carList;

    [SerializeField]
    private GunList gunList;

    private enum Stage
    {
        PickCar,
        PickGun,
        Ready
    }

    private Stage currentStage = Stage.PickCar;

    private int selectedCarIndex;
    private int selectedGunIndex;

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

            PlayerPrefs.SetInt("SelectedCar" + playerIndex, selectedCarIndex);
            PlayerPrefs.SetInt("SelectedGun" + playerIndex, selectedGunIndex);
        }
    }

}
