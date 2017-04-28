using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Util;
using TowerDefence.Enemies;
using System.Linq;

namespace TowerDefence
{
    public class BaseTower : Util.Update.BaseClass
    {
        /// <summary>
        /// Offset For the context menu on the tower
        /// </summary>
        [HideInInspector]
        public Vector2 ContextMenuOffset = Vector2.zero;

        /// <summary>
        /// Location the bullet is firedfrom
        /// </summary>
        [HideInInspector]
        public Vector2 fireLocation;
        public Vector2 fireWorldPosition { get { return fireLocation.Rotate(transform.rotation) + (Vector2)transform.position; } }

        /// <summary>
        /// projectiles shot per minut
        /// </summary>
        [SerializeField]
        protected float _fireRate = 60;
        [SerializeField]
        protected int _damage = 5;
        [SerializeField]
        protected float _range = 5;
        protected string _type = "base";

        public float fireRate { get { return _fireRate; } }
        public int damage { get { return _damage; } }
        public float range { get { return _range; } }
        public string type { get { return _type; } }

        [SerializeField]
        protected float rotationOffset;
        [SerializeField]
        protected float shootingOffsetDirection;

        public Target_Priority TargetMode = Target_Priority.ClosestToEnd;

        protected List<BaseEnemy> Enemies;

        int length;
        int mask;

        protected BaseEnemy Target;

        protected virtual void Start()
        {
            mask = LayerMask.GetMask(new string[] { Managers.LayerTagManager.Enemy });
            Enemies = new List<BaseEnemy>();

            //GetComponent<CircleCollider2D>().radius = range;
            GetComponent<CircleCollider2D>().isTrigger = true;
        }

        /// <summary>
        /// Don't callfunc dicrectly let update manager call it
        /// </summary>
        public override void IUpdate()
        {
            CircleCast(); 
            length = Enemies.Count;


            if (FindTarget())
            {
                RotateToTarget();
                HasTarget();
            }
        }

        int hitsLength;
        RaycastHit2D[] hits;
        List<BaseEnemy> tmpList;
        BaseEnemy e;
        void CircleCast()
        {
            tmpList = new List<BaseEnemy>();
            hits = Physics2D.CircleCastAll(transform.position, _range, Vector2.up, _range, mask);
            hitsLength = hits.Length;

            for (int i = 0; i < hitsLength; i++)
            {
                if(hits[i].collider.tag == Managers.TagManager.Enemy)
                {
                    e = hits[i].collider.GetComponent<BaseEnemy>();
                    tmpList.Add(e);
                    if (!Enemies.Contains(e))
                    {
                        Enemies.Add(e);
                        
                    }
                }
            }

            Enemies = Enemies.Intersect(tmpList).ToList();
        }

        /// <summary>
        /// Runs each update if there is a target
        /// </summary>
        protected virtual void HasTarget() { }

        #region targeting
        /// <summary>
        /// Handels the finding of the current target
        /// </summary>
        /// <returns></returns>
        protected virtual bool FindTarget()
        {
            if (length > 0)
            {
                CheckTargets();
                if (length > 0)
                {
                    switch (TargetMode)
                    {
                        case Target_Priority.Closest:
                            FindClosestTarget();
                            break;

                        case Target_Priority.First:
                            GetFirstEntered();
                            break;

                        case Target_Priority.Last:
                            GetLastEntered();
                            break;

                        case Target_Priority.Strongest:
                            FindStrongest();
                            break;

                        case Target_Priority.Weakest:
                            FindWeakest();
                            break;
                        case Target_Priority.ClosestToEnd:
                            FindClosestToEnd();
                                break;

                    }
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// Checks if Enemies still exist and are alive
        /// </summary>
        protected virtual void CheckTargets()
        {
            for (int i = length - 1; i >= 0; i--)
            {
                if (!Enemies[i] || Enemies[i] == null || !Enemies[i].isActiveAndEnabled || Enemies[i].VirtualHealth <= 0) 
                {
                    Enemies.RemoveAt(i);
                }
            }
                length = Enemies.Count;
        }
        /// <summary>
        /// Takes target closest to the tower
        /// </summary>
        protected virtual void FindClosestTarget()
        {
            BaseEnemy closest = Enemies[0];
            float dist = Vector2.Distance(transform.position, closest.transform.position);
            foreach (BaseEnemy hit in Enemies)
            {
                if (dist > Vector2.Distance(transform.position, hit.transform.position))
                {
                    closest = hit;
                }
            }
            Target = closest;
        }
        /// <summary>
        /// Takes the first target it finds in the list
        /// </summary>
        protected virtual void GetFirstEntered()
        {
            Target = Enemies[0];
        }
        /// <summary>
        /// Take last target it find in the list
        /// </summary>
        protected virtual void GetLastEntered()
        {
            if (Enemies.Count > 0)
            {
                Target = Enemies[Enemies.Count - 1];
            }
        }
        /// <summary>
        /// Finds the strongest or first encounterd in the list
        /// </summary>
        protected virtual void FindStrongest()
        {
            BaseEnemy closest = Enemies[0];
            foreach (BaseEnemy hit in Enemies)
            {
                if (closest.Health > hit.Health)
                {
                    closest = hit;
                }
            }
            Target = closest;
        }
        /// <summary>
        /// Finds the weakest or first encounterd in the list
        /// </summary>
        protected virtual void FindWeakest()
        {
            BaseEnemy closest = Enemies[0];
            foreach (BaseEnemy hit in Enemies)
            {
                if (closest.Health < hit.Health)
                {
                    closest = hit;
                }
            }
            Target = closest;
        }

        /// <summary>
        /// Finds the enemy clostest to the endpoint
        /// </summary>
        protected virtual void FindClosestToEnd()
        {
            BaseEnemy closest = Enemies[0];
            float shortestDist = Enemies[0].GetPathLengthLeft();
            foreach (BaseEnemy hit in Enemies)
            {
                if (shortestDist > hit.GetPathLengthLeft())
                {
                    closest = hit;
                    shortestDist = hit.GetPathLengthLeft();
                }
            }
            Target = closest;
        }
        #endregion
        #region ContextMenu
        public virtual void AddContextItems(GameObject MenuContainer,GameObject templateButton)
        {
            GameObject g = CloneGameobject(templateButton, MenuContainer.transform);
            setButtonText(g, "upgrade Range");
            setButtonEvents(g, new UnityEngine.Events.UnityAction(delegate 
            {
                TowerDefence.Managers.ContextMenus.TowerContextMenu.instance.currentTower.UpgradeRange(5);
            }));

            g = CloneGameobject(templateButton, MenuContainer.transform);
            setButtonText(g, "upgrade Damage");
            setButtonEvents(g, new UnityEngine.Events.UnityAction(delegate
            {
                TowerDefence.Managers.ContextMenus.TowerContextMenu.instance.currentTower.UpgradeDamage(2);
            }));

            g = CloneGameobject(templateButton, MenuContainer.transform);
            setButtonText(g, "upgrade FireRate");
            setButtonEvents(g, new UnityEngine.Events.UnityAction(delegate
            {
                TowerDefence.Managers.ContextMenus.TowerContextMenu.instance.currentTower.UpgradeFireRate(6);
            }));
        }

        /// <summary>
        /// Add Objects to DropDown Context Menu
        /// </summary>
        /// <param name="ToggleContainer">Containter that contains the toggles</param>
        /// <param name="templateToggle"></param>
        /// <param name="animationStart"></param>
        public void AddDropDownMenuItems(GameObject ToggleContainer, GameObject templateToggle, Vector3 animationStart)
        {
            GameObject g;
            Toggle t; 

            g = CloneGameobject(templateToggle, ToggleContainer.transform);
            setButtonText(g, "Closest");
            SetActiveToggle(g, Target_Priority.Closest);
            t = setToggleEvents(g, new UnityEngine.Events.UnityAction<bool>(delegate
            {
                TowerDefence.Managers.ContextMenus.TowerContextMenu.instance.currentTower.TargetMode = Target_Priority.Closest;
            }));
            g.transform.position = animationStart;

            g = CloneGameobject(templateToggle, ToggleContainer.transform);
            setButtonText(g, "ClosestToEnd");
            SetActiveToggle(g, Target_Priority.ClosestToEnd);
            t = setToggleEvents(g, new UnityEngine.Events.UnityAction<bool>(delegate
            {
                TowerDefence.Managers.ContextMenus.TowerContextMenu.instance.currentTower.TargetMode = Target_Priority.ClosestToEnd;
            }));            
            g.transform.position = animationStart;

            g = CloneGameobject(templateToggle, ToggleContainer.transform);
            setButtonText(g, "First");
            SetActiveToggle(g, Target_Priority.First);
            t = setToggleEvents(g, new UnityEngine.Events.UnityAction<bool>(delegate
            {
                TowerDefence.Managers.ContextMenus.TowerContextMenu.instance.currentTower.TargetMode = Target_Priority.First;
            }));
            g.transform.position = animationStart;

            g = CloneGameobject(templateToggle, ToggleContainer.transform);
            setButtonText(g, "Last");
            SetActiveToggle(g, Target_Priority.Last);
            setToggleEvents(g, new UnityEngine.Events.UnityAction<bool>(delegate
            {
                TowerDefence.Managers.ContextMenus.TowerContextMenu.instance.currentTower.TargetMode = Target_Priority.Last;
            }));
            
            g.transform.position = animationStart;

            g = CloneGameobject(templateToggle, ToggleContainer.transform);
            setButtonText(g, "Strongest");
            SetActiveToggle(g, Target_Priority.Strongest);
            setToggleEvents(g, new UnityEngine.Events.UnityAction<bool>(delegate
            {
                TowerDefence.Managers.ContextMenus.TowerContextMenu.instance.currentTower.TargetMode = Target_Priority.Strongest;
            }));
            
            g.transform.position = animationStart;

            g = CloneGameobject(templateToggle, ToggleContainer.transform);
            setButtonText(g, "Weakest");
            SetActiveToggle(g, Target_Priority.Weakest);
            setToggleEvents(g, new UnityEngine.Events.UnityAction<bool>(delegate
            {
                TowerDefence.Managers.ContextMenus.TowerContextMenu.instance.currentTower.TargetMode = Target_Priority.Weakest;
            }));
            
            g.transform.position = animationStart;
        }

        public virtual GameObject CloneGameobject(GameObject template, Transform Parent)
        {
            GameObject g = Instantiate(template) as GameObject;
            g.SetActive(true);
            g.transform.SetParent(Parent, false);
            return g;
        }

        public virtual Text setButtonText(GameObject obj, string newText)
        {
             obj.GetComponentInChildren<Text>().text = newText;
            return obj.GetComponentInChildren<Text>();
        }

        public virtual Button setButtonEvents(GameObject obj, UnityEngine.Events.UnityAction newEvent)
        {
            Button b = obj.GetComponent<Button>();
            b.onClick.AddListener(newEvent);

            return b;
        }

        public virtual Toggle setToggleEvents(GameObject obj, UnityEngine.Events.UnityAction<bool> newEvent)
        {
            Toggle t = obj.GetComponent<Toggle>();
            t.onValueChanged.AddListener(newEvent);

            return t;
        }

        public virtual void SetActiveToggle(GameObject g, Target_Priority type)
        {
            Toggle t = g.GetComponent<Toggle>();
            if (TargetMode == type)
                t.isOn = true;
        }
        #endregion
        #region TowerUpgrades
        public virtual void UpgradeRange(float newRange)
        {
            Debug.Log(newRange);
        }

        public virtual void UpgradeDamage(float newDamage)
        {
            Debug.Log(newDamage);
        }

        public virtual void UpgradeFireRate(float newFireRate)
        {
            Debug.Log(newFireRate);
        }

        #endregion

        /// <summary>
        /// Function rotates front of tower to enemy. Using a off set to correct for any rotation issues
        /// </summary>
        protected virtual void RotateToTarget()
        {
            transform.rotation = Quaternion.Euler(0, 0, Util.Common.VectorToAngle(transform.position - Target.transform.position) + rotationOffset);
        }

#if UNITY_EDITOR
        public void OnDrawGizmosSelected()
        {
            Vector2 fireLocationRotated = fireLocation.Rotate(transform.rotation);

            Gizmos.color = Color.red;
            Vector2 SphereLocation = fireLocationRotated + (Vector2)transform.position;
            Gizmos.DrawSphere(SphereLocation, 0.05f);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(((Vector2)transform.position) + fireLocationRotated, ((Vector2)transform.position) + fireLocationRotated + (Util.Common.AngleToVector(shootingOffsetDirection).Rotate(transform.rotation) * 0.5f));

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(ContextMenuOffset + (Vector2)transform.position, 0.05f);
        }
#endif
    }
}