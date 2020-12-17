using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
[RequireComponent(typeof(CarPhysics))]
[RequireComponent(typeof(CarBoostManager))]
public class Car : MonoBehaviour {

    [SerializeField]
    private CarSO carInfo = null;

    [SerializeField]
    private AudioClip destructionSound = null;
    
    [SerializeField]
    private ParticleSystem smokeParticleSystem = null;

    [SerializeField]
    private ParticleSystem engineBurnParticleSystem = null;

    [SerializeField]
    private Transform gunMountTransform = null;

    private int playerIndex;
    private HealthSystem healthSystem;
    private CarPhysics physics;
    private GunBase gun;
    private CarBoostManager boostManager;


	void Awake () {
        healthSystem = GetComponent<HealthSystem>();
        physics = GetComponent<CarPhysics>();
        boostManager = GetComponent<CarBoostManager>();

        smokeParticleSystem.Stop();
        engineBurnParticleSystem.Stop();

        healthSystem.SetMaxHealth(carInfo.maxHealth, true);
        healthSystem.OnDeath += Car_OnDeath;
        healthSystem.OnHeal += Car_OnHeal;
        healthSystem.OnDamage += Car_OnDamage;
    }

    private void Start()
    {
        physics.Setup(carInfo.physics);
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal" + playerIndex);
        float verticalInput = Input.GetAxis("Vertical" + playerIndex);

        physics.HandleMovement(horizontalInput, verticalInput);

        string fireButton = "Fire" + playerIndex;
        if (Input.GetButtonDown(fireButton))
        {
            gun.StartFiring();
        }

        if(Input.GetButtonUp(fireButton))
        {
            gun.StopFiring();
        }

        if(Input.GetButtonDown("Boost" + playerIndex))
        {
            boostManager.UseBoost();
        }
    }

    public void Setup(GunSO gunSO, int playerIndex)
    {
        gun = Instantiate(gunSO.prefab, gunMountTransform);
        this.playerIndex = playerIndex;
    }

    private void Car_OnDamage(object sender, System.EventArgs e)
    {
        UpdateSmokeState();
    }

    private void Car_OnHeal(object sender, System.EventArgs e)
    {
        UpdateSmokeState();
    }

    private void Car_OnDeath(object sender, System.EventArgs e)
    {
        gun.StopFiring();

        MakeCarImmovable();
        AudioSource.PlayClipAtPoint(destructionSound, transform.position);

        Destroy(this);
    }

    private void UpdateSmokeState()
    {
        float maxHealth = healthSystem.GetMaxHealth();
        float currentHealth = healthSystem.GetCurrentHealth();

        if (currentHealth <= maxHealth / 4)
        {
            smokeParticleSystem.Play();
            engineBurnParticleSystem.Play();
        }
        else if (currentHealth <= maxHealth / 2)
        {
            smokeParticleSystem.Play();
            engineBurnParticleSystem.Stop();
        }
        else
        {
            smokeParticleSystem.Stop();
            engineBurnParticleSystem.Stop();
        }
    }

    private void MakeCarImmovable()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.mass *= 100;
        rigidbody.drag *= 10;
        rigidbody.angularDrag *= 10;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gunMountTransform.position, 0.1f);
    }

    public CarSO GetCarSO()
    {
        return carInfo;
    }

    public GunBase GetGun()
    {
        return gun;
    }

    public CarBoostManager GetBoostManager()
    {
        return boostManager;
    }
}
