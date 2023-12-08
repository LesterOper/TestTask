using DefaultNamespace.ShopDialog;
using UnityEngine;

namespace DefaultNamespace.Locations
{
    public class SkinUtils
    {
        private string SKIN_CLAIMED = "SKIN_CLAIMED_";

        public void SetSkinClaimed(SkinType skinType, bool isClaimed) =>
            PlayerPrefs.SetInt(SKIN_CLAIMED + skinType, isClaimed ? 0 : 1);

        public bool IsSkinClaimed(SkinType skinType) => PlayerPrefs.GetInt(SKIN_CLAIMED + skinType) == 0;
    }
}