using System.Collections.Generic;
using YG;

public static class MetricaSender 
{
    public static void SendPurchasedSkinName(string name)
    {
        var eventParams = new Dictionary<string, string>
        {
            {"SkinName", name }
        };

        YandexMetrica.Send("SkinPurchased", eventParams);
    }
}
