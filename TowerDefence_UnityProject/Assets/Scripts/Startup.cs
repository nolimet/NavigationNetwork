using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefence.Project
{
    public class Startup : MonoBehaviour
    {
        private async void Start()
        {
            await new WaitForSeconds(0.5f);
            SceneManager.LoadScene(1);
        }
    }
}