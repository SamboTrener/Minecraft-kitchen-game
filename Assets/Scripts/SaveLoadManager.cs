using System;
using YG;

public static class SaveLoadManager
{
    public static Action<int> OnMoneyChangedVisual;

    public static void SaveCurrentSkinName(string skinSOName)
    {
        YandexGame.savesData.CurrentSkinSOName = skinSOName;
        YandexGame.SaveProgress();
    }

    public static string GetCurrentSkinName() => YandexGame.savesData.CurrentSkinSOName;

    public static void SaveMoneyData(int moneyCount)
    {
        ChangeMoneyCount(moneyCount);
        YandexGame.SaveProgress();
    }

    public static int GetMoneyData() => YandexGame.savesData.MoneyCount;

    public static bool TryPurchaseSkin(SkinSO skin)
    {
        if (YandexGame.savesData.MoneyCount >= skin.Cost)
        {
            if (IsSkinPurchased(skin))
            {
                return false;
            }
            ChangeMoneyCount(-skin.Cost);
            YandexGame.savesData.AvailableSkinNames.Add(skin.Name);
            YandexGame.SaveProgress();
            return true;
        }
        return false;
    }

    public static bool IsSkinPurchased(SkinSO skin)
    {
        foreach (var skinName in YandexGame.savesData.AvailableSkinNames)
        {
            if (skinName == skin.Name)
            {
                return true;
            }
        }
        return false;
    }

    static void ChangeMoneyCount(int money)
    {
        YandexGame.savesData.MoneyCount += money;
        OnMoneyChangedVisual?.Invoke(YandexGame.savesData.MoneyCount);
    }

    public static void SaveSoundVolume(float volume)
    {
        YandexGame.savesData.SoundVolume = volume;
        YandexGame.SaveProgress();
    }

    public static void SaveMusicVolume(float volume)
    {
        YandexGame.savesData.MusicVolume = volume;
        YandexGame.SaveProgress();
    }

    public static float GetMusicVolume() => YandexGame.savesData.MusicVolume;
    public static float GetSoundVolume() => YandexGame.savesData.SoundVolume;
    public static void FirstTimePlayChange() => YandexGame.savesData.IsPlayingFirstTime = false;
    public static bool GetIsFirstTimePlayInfo() => YandexGame.savesData.IsPlayingFirstTime;
}
