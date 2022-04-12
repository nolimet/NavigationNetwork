using Cysharp.Threading.Tasks;
using TowerDefence.Entities.Towers;
using UnityEngine;
using Zenject;

namespace Examples.Towers
{
    public class ExampleTowerPlacer : MonoBehaviour
    {
        [Inject] private TowerService towerService;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                var cam = Camera.main;
                var pos = cam.ScreenToWorldPoint(Input.mousePosition);

                towerService.PlaceTower("Example", pos, default).Preserve().Forget();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                towerService.DestroyAllTowers();
            }
        }
    }
}