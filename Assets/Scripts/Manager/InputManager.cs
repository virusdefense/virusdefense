using Tower;
using UnityEngine;
using Utils;

namespace Manager
{
    public class InputManager : MonoBehaviour
    {
        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            if (!Mouse.GetGameObjectPointed(out var hit)) return;
            if (!hit.CompareTag(Tag.BuildBlockTag)) return;
            
            hit.GetComponent<SpawnTower>().Spawn();    // TODO 
        }
    }
}
