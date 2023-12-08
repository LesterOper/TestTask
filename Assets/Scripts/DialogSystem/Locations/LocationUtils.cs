using DefaultNamespace.ShopDialog;
using UnityEngine;

namespace DefaultNamespace.Locations
{
    public class LocationUtils
    {
        private string LVL_PASSED = "LVL_PASSED";
        private string CURRENT_LEVEL = "CURRENT_LEVEL";
        private string LOCATION_CLAIMED = "LOCATION_CLAIMED_";

        public void SetLvlPassed(int level) => PlayerPrefs.SetInt(LVL_PASSED, level);
        public int GetLvlPassed() => PlayerPrefs.GetInt(LVL_PASSED);
        public void SetCurrentLevel(int current) => PlayerPrefs.SetInt(CURRENT_LEVEL, current);
        public int GetCurrentLevel() => PlayerPrefs.GetInt(CURRENT_LEVEL);
        public void SetLocationClaimed(LocationType locationType, bool isClaimed) => PlayerPrefs.SetInt(LOCATION_CLAIMED + locationType, isClaimed ? 0 : 1);
        public bool GetLocationClaimed(LocationType locationType) => PlayerPrefs.GetInt(LOCATION_CLAIMED + locationType) == 0;
    }
}