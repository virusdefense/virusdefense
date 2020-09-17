using Tower;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class TowerButton : MonoBehaviour
    {
        [SerializeField] private TowerType.Type type;
        private string _towerName;
        private int _towerPrice;

        private void Awake()
        {
            ResourcesHelper.SetFeaturesFromTextFile(
                string.Format(TowerFeatureFile, type),
                SetFeature
            );
            
            GetComponentInChildren<Text>().text = $"<b>{_towerName}</b>\n\n{_towerPrice}";
        }

        private void SetFeature(string featureName, string featureValue)
        {
            switch (featureName)
            {
                case "price":
                    _towerPrice = int.Parse(featureValue);
                    break;
                case "name":
                    _towerName = featureValue;
                    break;
            }
        }

        private const string TowerFeatureFile = "Plain/tower/tower_{0}";
    }
}