using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.CrossPlatformInput;

public abstract class GunBase : MonoBehaviour {

    public UnityEvent OnAmmoChange;
    public enum SpawnLocationMethod
    {
        FirstOnly,
        All,
        RandomOne,
        RoundRobin,
    }

    public enum FireSoundMethod
    {
        Continous,
        PerShot
    }

    [SerializeField]
    protected GunSO gunInfo;

    [SerializeField]
    protected List<Transform> projectileSpawnTransforms;

    private AudioSource gunfireAudioSource;
    [SerializeField] // TODO: Remove
    protected float currentAmmo;
    protected bool isFiring = false;
    protected bool canFireNext = false;
    private int gunfireRoundRobinIndex = 0;
    
    protected Car owner;

    private void Awake()
    {
        owner = GetComponentInParent<Car>();
        currentAmmo = gunInfo.maxAmmo;

        SetupAudioSource();
        if(projectileSpawnTransforms.Count == 0)
        {
            Debug.LogError(name + " has no projectile spawn locations set!");
        }
    }


    void Update() {
        if (CanShoot() == false)
        {
            StopFiring();
            return;
        }

        if (isFiring)
        {
            HandleGunActivity();
        }
    }

    public void StartFiring()
    {
        if (isFiring == false && CanShoot())
        {
            isFiring = true;
            ActivateGun();
        }
    }

    public void StopFiring()
    {
        if (isFiring)
        {
            DeactivateGun();
            isFiring = false;
        }
    }

    protected virtual void ActivateGun() { 
        PlaySound();
    }
    protected virtual void HandleGunActivity() { }
    protected virtual void DeactivateGun() {
        StopSound();
    }


    protected virtual float GetAmmoCostForFire()
    {
        return gunInfo.ammoCostPerAction * projectileSpawnTransforms.Count;
    }

    public virtual bool CanShoot()
    {
        return GetAmmoCostForFire() <= currentAmmo;
    }

    public void DecreaseAmmo(float modifier = 1.0f)
    {
        currentAmmo -= gunInfo.ammoCostPerAction * modifier;
        OnAmmoChange?.Invoke();
        if (currentAmmo < 0) currentAmmo = 0;
    }

    public void AddAmmo(float ammo)
    {
        currentAmmo += ammo;
        if (currentAmmo > gunInfo.maxAmmo)
        {
            currentAmmo = gunInfo.maxAmmo;
        }
        OnAmmoChange?.Invoke();
    }

    protected void PlaySound()
    {
        if (gunInfo.fireSoundMethod == FireSoundMethod.Continous)
        {
            gunfireAudioSource.Play();
        }
        else if(gunInfo.fireSoundMethod == FireSoundMethod.PerShot)
        {
            AudioSource.PlayClipAtPoint(gunInfo.gunfireSoundClip, transform.position);
        }
    }


    protected void StopSound()
    {
        if (gunInfo.fireSoundMethod == FireSoundMethod.Continous)
        {
            gunfireAudioSource.Stop();
        }
    }


    private List<Transform> GetSpawnTransformsBySpawnMethod()
    {
        switch(gunInfo.spawnLocationMethod) {
            case SpawnLocationMethod.All:
                return projectileSpawnTransforms;
            
            case SpawnLocationMethod.FirstOnly:
                List<Transform> firstTransformList = new List<Transform>();
                firstTransformList.Add(projectileSpawnTransforms[0]);
                return firstTransformList;

            case SpawnLocationMethod.RandomOne:
                List<Transform> randomTransformList = new List<Transform>();
                randomTransformList.Add(projectileSpawnTransforms[Random.Range(0, projectileSpawnTransforms.Count)]); ;
                return randomTransformList;
            
            case SpawnLocationMethod.RoundRobin:
                List<Transform> singleTransformList = new List<Transform>();
                singleTransformList.Add(projectileSpawnTransforms[gunfireRoundRobinIndex]);
                gunfireRoundRobinIndex++;
                gunfireRoundRobinIndex %= projectileSpawnTransforms.Count;
                return singleTransformList;
        }

        Debug.LogError("Chosen SpawnLocationMethod has no implementation!");
        return null;
    }


    protected List<ProjectileBase> InstantiateProjectiles(ProjectileBase projectilePrefab)
    {
        List<ProjectileBase> projectiles = new List<ProjectileBase>();
        List<Transform> validSpawnTransforms = GetSpawnTransformsBySpawnMethod();
        foreach(Transform spawnTransform in validSpawnTransforms)
        {
            ProjectileBase projectile = Instantiate(projectilePrefab, spawnTransform.position, spawnTransform.rotation);
            projectile.SetOwner(owner);
            projectiles.Add(projectile);
            DecreaseAmmo();
        }
        return projectiles;
    }


    private void SetupAudioSource()
    {
        if (gunInfo.fireSoundMethod == FireSoundMethod.Continous)
        {
            gunfireAudioSource = GetComponent<AudioSource>();
            if (!gunfireAudioSource)
            {
                gunfireAudioSource = gameObject.AddComponent<AudioSource>();
            }
            gunfireAudioSource.loop = true;
            gunfireAudioSource.clip = gunInfo.gunfireSoundClip;
        }
    }

    private void OnDrawGizmos()
    {
        foreach (Transform projectileSpawnTransform in projectileSpawnTransforms) {
            Gizmos.DrawWireSphere(projectileSpawnTransform.position, 0.1f);
        }
    }

    public GunSO GetGunInfo()
    {
        return gunInfo;
    }
    public float GetAmmo()
    {
        return currentAmmo;
    }
}
