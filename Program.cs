using System.Net;
int PriceVaiationCount;
List<KeyValuePair<string, int>> dctKeyValuePairs = new List<KeyValuePair<string, int>>();
void get_max_profit(List<int> liPrices)
{
    for (int i = 0; i < liPrices.Count - 1; i++)
    {
        List<int> liOutputs = new List<int>();
        for (int j = i + 1; j < liPrices.Count; j++)
        {
            liOutputs.Add(liPrices[j] - liPrices[i]);
        }
        liOutputs.Sort((Compare1, Compare2) => Compare2.CompareTo(Compare1));
        int Max = liOutputs.First();
        int MinuteNeedToAdd = liPrices.FindIndex(i, Data => Data == (Max + liPrices[i]));
        KeyValuePair<string, int> kvpPrice = new KeyValuePair<string, int>(MinuteNeedToAdd.ToString(), Max);
        dctKeyValuePairs.Add(kvpPrice);
    }
}
string url = "https://raw.githubusercontent.com/himesh-suthar/raocodefestv1.0/main/paytm_stock_input.txt";
WebClient webClient = new WebClient();
using (Stream stream = webClient.OpenRead(url))
{
    using (StreamReader streamReader = new StreamReader(stream))
    {
        PriceVaiationCount = Convert.ToInt32(streamReader.ReadLine());
        while (PriceVaiationCount-- > 0)
        {
            List<int> liPrices = new List<int>();
            string Data = streamReader.ReadLine().Replace("[", "").Replace("]", "");
            liPrices.AddRange(Data.Split(",").Select(int.Parse).ToArray());
            string ShareTime = "9.30";
            dctKeyValuePairs.Clear();
            get_max_profit(liPrices);
            KeyValuePair<string, int> keyValuePair = dctKeyValuePairs.OrderByDescending(Item => Item.Value).First();
            int Hour = Convert.ToInt32(ShareTime.Split(".")[0]);
            int Minute = Convert.ToInt32(ShareTime.Split(".")[1]);
            Minute += Convert.ToInt32(keyValuePair.Key) * 10;
            int ExtraHour = (int)Minute / 60;
            Hour += ExtraHour;
            Minute -= ExtraHour * 60;
            if (Hour > 12)
            {
                Hour -= 12;
            }
            ShareTime = Hour + "." + ((Minute == 0) ? "00" : Minute);
            Console.WriteLine("[" + keyValuePair.Value + "," + ShareTime + "]");
        }
    }
}