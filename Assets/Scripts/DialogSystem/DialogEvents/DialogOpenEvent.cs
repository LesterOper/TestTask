using UnityEngine;

namespace DefaultNamespace.DialogEvents
{
    public class DialogOpenEvent
    {
        public DialogType DialogType { get; set; }
        public Transform Parent { get; set; }
    }

    public enum DialogType
    {
        NONE = 0,
        SETTINGS = 1,
        DAILY_PRIZE = 2,
        SHOP = 3,
        LEVEL = 4,
    }
}