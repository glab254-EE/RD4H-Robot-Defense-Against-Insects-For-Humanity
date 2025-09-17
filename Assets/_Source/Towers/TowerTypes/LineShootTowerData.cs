using UnityEngine;

[CreateAssetMenu(fileName = "LineShootTowerData", menuName = "Scriptable Objects/Towers/New Line Shooting Tower Data"), System.Serializable]
public class LineShootTowerData : TowerDataSO
{
    [Header("Tower type settings")]
    [field: SerializeField]
    public int BaseCooldown { get; protected set; }
    [Header("Tower type Shooting settings")]
    [field: SerializeField]
    public GameObject ProjectilePrefab { get; protected set; }
    [field: SerializeField]
    public float ProjectileDeathTime { get; protected set; }
    [field: SerializeField]
    public float ExplosionSize { get; protected set; }
    [Header("Tower type Visual movement settings")]
    [field: SerializeField]
    public bool MoveFullTowerWithEnemy { get; protected set; }  
    [field: SerializeField]
    public bool MoveOriginWithEnemy { get; protected set; }  
    [field: SerializeField]
    public string BarrelName { get; protected set; } = "shootOrigin"; 
    [field: SerializeField]
    public string ShootingPartName { get; protected set; } = "shootPart"; 
    [field: SerializeField]
    public Vector3 BarrelDifferencePosition { get; protected set; } 
     [field: SerializeField]
    public Vector3 ShootingPartDifferencePosition { get; protected set; } 
    private int currentCooldown; // using tick time not float time.
    private Transform barrelOrigin;
    private Transform shootOrigin;
    public override void OnPlace(GameObject tower)
    {
        if (barrelOrigin == null)
        {
            barrelOrigin = tower.transform.Find(BarrelName);
        }
        if (shootOrigin == null && barrelOrigin != null)
        {
            shootOrigin = tower.transform.Find(BarrelName);
        }
        if (barrelOrigin != null && shootOrigin != null)
        {
            barrelOrigin.Translate(BarrelDifferencePosition);
            shootOrigin.Translate(ShootingPartDifferencePosition);
        }
    }
    public override void OnTick(GameObject tower, System.Collections.Generic.List<GameObject> enemies, GameObject currenttarget)
    {
        if (barrelOrigin == null)
        {
            barrelOrigin = tower.transform.Find(BarrelName);
        }
        if (shootOrigin == null && barrelOrigin != null)
        {
            shootOrigin = barrelOrigin.Find(ShootingPartName);
        }
        if (currenttarget != null)
        {
            if (MoveFullTowerWithEnemy)
            {
                Vector3 targetPosition = currenttarget.transform.position;
                targetPosition.y = tower.transform.position.y;
                tower.transform.LookAt(targetPosition);
            }
            if (MoveOriginWithEnemy)
            {
                if (shootOrigin != null)
                {
                Vector3 targetPosition = currenttarget.transform.position;
                targetPosition.y = tower.transform.position.y;
                shootOrigin.LookAt(targetPosition);
                }
            }            
        }
        currentCooldown--;
        if (currenttarget != null && currentCooldown <= 0)
        {
            currentCooldown = BaseCooldown;
            OnShoot(tower,currenttarget);
        } 
    }
    private void OnShoot(GameObject tower,GameObject target)
    {
        if (target.transform == null) return;
        Animator animator = tower.GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("fire");
        }
        barrelOrigin = tower.transform.Find(BarrelName);
        if (barrelOrigin != null) shootOrigin = barrelOrigin.Find(ShootingPartName);
        if (target.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            damagable.Damage(BaseDamage);
        }
        if (ProjectilePrefab != null&&shootOrigin != null)
        {
            GameObject projectile = Instantiate(ProjectilePrefab, null, true);
            float deathtime = ProjectileDeathTime > 0 ? ProjectileDeathTime : 0.2f;
            Vector3 difference = target.transform.position - shootOrigin.position;
            projectile.transform.position = difference / 2 + shootOrigin.position;
            projectile.transform.LookAt(shootOrigin.position+difference);
            projectile.transform.localScale = new Vector3(1, 1,Vector3.Distance(shootOrigin.position,target.transform.position));
            Destroy(projectile, deathtime);
        }  
        if (ExplosionSize > 0)
        {
            RaycastHit[] hits = Physics.SphereCastAll(new Ray(target.transform.position,(tower.transform.position-target.transform.position).normalized), ExplosionSize);
            if (hits != null && hits.Length >= 1)
            {
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider != null && hit.collider.gameObject != null && hit.collider.gameObject != target && hit.collider.gameObject.TryGetComponent<IDamagable>(out IDamagable component))
                    {
                        component.Damage(BaseDamage);
                    }
                }
            }
        }  
    }

    public override void OnUpgrade(TowerBaseHandler parent,GameObject oldTower)
    {
        if (NextUpgradeTower != null && parent != null&& NextUpgradeTower.BaseCost > 0 && ResourceManager.instance.CanBuy(NextUpgradeTower.BaseCost))
        {
            Destroy(oldTower);
            parent.PlaceTower(NextUpgradeTower);
        }
    }
}