using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "Main Installer", menuName = "Installers/Main Installer")]
public sealed class MainInstaller : ScriptableObjectInstaller
{
    public override void InstallBindings()
    {
    }
}