using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public abstract class GunBase : MonoBehaviour {

    [SerializeField]
    private float maxAmmo = 100;
    [SerializeField]
    private float ammoCostPerActivation = 2;
    [SerializeField]
    private float fireCooldown = 1;
    [SerializeField]
    protected Position[] shotPositions;
    [SerializeField]
    private string activationKey = "Fire0";
    [SerializeField]
    private AudioClip gunSoundClip;
    
    private AudioSource audioSource;

    [SerializeField] // TODO: Remove
    private float currentAmmo;

    [SerializeField] // TODO: Remove 
    private float currentCooldown;

    [SerializeField] // TODO: Remove
    private bool gunIsFiring = false;
    
    protected Car owner;

    private void Awake()
    {
        owner = GetComponentInParent<Car>();
        currentAmmo = maxAmmo;

        SetupAudioSource();
        if(shotPositions.Length == 0)
        {
            Debug.LogError(name + " has no projectile spawn locations set!");
        }
    }


    void Update() {

        if (!gunIsFiring && currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
            return;
        }


        if (currentAmmo < ammoCostPerActivation)
        {
            if (gunIsFiring)
            {
                gunIsFiring = false;
                DeactivateGun();
            }
            return;
        }


        if (CrossPlatformInputManager.GetButtonDown(activationKey))
        {
            KeyDownEvents();
        }
        else if (CrossPlatformInputManager.GetButtonUp(activationKey))
        {
            KeyUpEvents();
        }
        else if (CrossPlatformInputManager.GetButton(activationKey))
        {
            KeyHeldEvents();
        }
    }

    

    protected abstract void GunActive();
    protected abstract void ActivateGun();
    protected abstract void DeactivateGun();


    public bool CanShoot()
    {
        return maxAmmo > currentAmmo + (ammoCostPerActivation * shotPositions.Length);
    }

    public void DecreaseAmmo()
    {
        currentAmmo -= ammoCostPerActivation;
        if (currentAmmo < 0) currentAmmo = 0;
    }

    public void SetActivationKey(string key)
    {
        activationKey = key;
    }

    public void SetMaxAmmo(float ammo)
    {
        maxAmmo = ammo;
        currentAmmo = ammo;
    }

    public void AddAmmo(float ammo)
    {
        currentAmmo += ammo;
        if (currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
    }
    

    protected void KeyUpEvents()
    {
        gunIsFiring = false;
        DeactivateGun();
        currentCooldown = fireCooldown;
    }

    protected void KeyDownEvents()
    {
        gunIsFiring = true;
        ActivateGun();
    }

    protected void KeyHeldEvents()
    {
        GunActive();
    }


    protected void PlaySound()
    {
        if (!audioSource || !gunSoundClip) return;
        audioSource.Play();
    }


    protected void StopSound()
    {
        if (!audioSource || !gunSoundClip) return;
        audioSource.Stop();
    }


    protected List<ProjectileBase> InstantiateProjectiles(ProjectileBase projectilePrefab)
    {
        List<ProjectileBase> projectiles = new List<ProjectileBase>();
        for(int i = 0; i < shotPositions.Length; i++)
        {
            ProjectileBase projectile = Instantiate(
                projectilePrefab,
                shotPositions[i].transform.position,
                transform.rotation
            );

            projectiles.Add(projectile);
        }
        return projectiles;
    }


    private void SetupAudioSource()
    {
        if (gunSoundClip)
        {
            audioSource = GetComponent<AudioSource>();
            if (!audioSource)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
            audioSource.loop = true;
            audioSource.clip = gunSoundClip;
        }
    }
}
