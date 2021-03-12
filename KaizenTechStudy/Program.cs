using System;
using System.Collections.Generic;
using System.Linq;

namespace KaizenTechStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            var orjText = @"İzmir Büyükşehir Belediyesi, 15 Mayıs Cuma günü açık havada sinema gösterimi yapacak. Altı noktada eş zamanlı olarak 'Bayi Toplantısı' filminin gösterileceği etkinlikte vatandaşlara maske dağıtılacak ve gösterimin yapılacağı alanda yurttaşların araçlarından inmelerine izin verilmeyecek.
Koronavirüs önlemleri nedeniyle sinemaya gidemeyen İzmirliler, İzmir Büyükşehir Belediyesinin düzenlediği nostaljik arabalı sinema etkinliğinde buluşacak.Film gösterimi 15 Mayıs Cuma günü altı noktada eş zamanlı yapılacak.Bostanlı, Üçkuyular, Fuar İzmir, Bornova Aşık Veysel Rekreasyon Alanı Buz Pisti yanı, Buca ve Çiğli’de
kurulacak dev perdede yönetmenliğini Bedran Güzel'in yaptığı 'Bayi Toplantısı' filmi ücretsiz gösterilecek. Sinemaseverler arabalarından inmeden güvenli bir şekilde sinema keyfi yaşayacak.Başrollerini İbrahim Büyükak, 
Onur Buldu, Doğu Demirkol'un paylaştığı, mecburen katıldıkları bayi toplantısında kendilerini birbirinden eğlenceli maceraların içinde bulan üç beyaz eşya satıcısının hikayesini konu eden film 21.00'da başlayacak. Filmin sesi radyo frekansı üzerinden arabalardan dinlenebilecek. 2 saat sürecek film gösterimi sırasında izleyicilere patlamış mısır ve gazoz da ikram edilecek.
Kayıtlar 11 Mayıs'ta başlıyor.
Belirli sayıda otomobilin girişine izin verilecek etkinliğe katılmak isteyen İzmirliler 11 Mayıs Pazar akşamı saat 21.00'dan itibaren www.arabalisinema.com.tr adresinden kayıt yaptırması gerekiyor. Kayıt yaptıran ilk 750 araç sahibi etkinliğe katılım hakkı kazanacak.İstenilen bölgedeki arabalı sinema etkinliğinin kontenjanın dolması halinde katılımcılar kendilerine en yakın bölgedeki arabalı sinema etkinliğine yönlendirilecek.Etkinlik günü katılım hakkı kazanmış ve listeye alınmış kişilerin araçlarıyla arabalı sinema etkinliğinin yapıldığı alana girişine izin verilecek. Gösterimin yapıldığı alana girdikten sonra vatandaşların araçlardan çıkılmasına izin verilmeyecek.";

            var text = orjText;

            CleanCharacters(ref text);

            var sortedList = SortByNumberOfUses(text: text);

            var mostUsedWords = GetMostUsedWords(sortedList: sortedList);

            var summaryText = GetSummary(orjText: orjText, mostUsedWords: mostUsedWords);

            Console.WriteLine(summaryText);

            Console.Read();
        }

        private static void CleanCharacters(ref string text)
        {
            if (!String.IsNullOrEmpty(text))
            {
                text = text.ToLower();
                var charsToRemove = new string[] { "@", ",", ".", ";", "'", "!", "<", ">", "#", "$", "%", "&", "/", "(", ")", "?", "-", "_", ":", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                foreach (var c in charsToRemove)
                {
                    text = text.Replace(c, " ");
                }
            }
        }

        private static Dictionary<string, int> SortByNumberOfUses(string text)
        {
            if (!String.IsNullOrEmpty(text))
            {
                string[] splittedWords = text.Split(' ');
                Dictionary<string, int> wordsUsage = new Dictionary<string, int>();
                foreach (var word in splittedWords)
                {
                    if (!String.IsNullOrWhiteSpace(word) && wordsUsage.ContainsKey(word))
                    {
                        wordsUsage[word] = ++wordsUsage[word];
                    }
                    else if (!String.IsNullOrWhiteSpace(word))
                    {
                        wordsUsage.Add(word, default(int));
                    }
                }
                return wordsUsage.OrderByDescending(u => u.Value).ToDictionary(z => z.Key, y => y.Value);
            }
            return null;
        }

        private static List<string> GetMostUsedWords(Dictionary<string, int> sortedList)
        {
            if (sortedList?.Count > 0)
            {
                var maxLength = sortedList.FirstOrDefault().Value;
                var restrictionLength = maxLength / 2;
                return sortedList.Where(c => c.Value > restrictionLength).Select(x => x.Key).ToList();
            }

            return null;
        }

        private static string GetSummary(string orjText, List<string> mostUsedWords)
        {
            string summaryText = string.Empty;
            if (!String.IsNullOrEmpty(orjText))
            {
                string[] sentences = orjText.Split('.');
                foreach (var sentence in sentences)
                {
                    if(mostUsedWords.Take(1).Any(s => sentence.Contains(s)))
                    {
                        summaryText += $"{sentence}. ";
                    }
                }
            }
            return summaryText;
        }
    }
}