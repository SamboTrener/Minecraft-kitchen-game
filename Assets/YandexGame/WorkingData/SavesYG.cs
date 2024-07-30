
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public string CurrentSkinSOName = "";
        public int MoneyCount = 0;
        public List<string> AvailableSkinNames = new List<string>();
        public float SoundVolume = -1;
        public float MusicVolume = -1;
        public bool IsPlayingFirstTime =  true;
    }
}
