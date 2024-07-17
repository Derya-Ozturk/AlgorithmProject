public class Program
{
    public static int GetDurationBetweenLocations(string from, string to, List<DurationBetweenLocations> matrix)
    {
        var duration = matrix.FirstOrDefault(d =>
            (d.From == from && d.To == to) || (d.From == to && d.To == from));

        return duration.DurationMinutes;
    }
    public static void Main(string[] args)
    {

        List<Event> events = new List<Event>
        {
            new Event { Id = 1, StartTime = "10:00", EndTime = "12:00", Location = "A", Priority = 50 },
            new Event { Id = 2, StartTime = "10:00", EndTime = "11:00", Location = "B", Priority = 30 },
            new Event { Id = 3, StartTime = "11:30", EndTime = "12:30", Location = "A", Priority = 40 },
            new Event { Id = 4, StartTime = "14:30", EndTime = "16:00", Location = "C", Priority = 70 },
            new Event { Id = 5, StartTime = "14:25", EndTime = "15:30", Location = "B", Priority = 60 },
            new Event { Id = 6, StartTime = "13:00", EndTime = "14:00", Location = "D", Priority = 80 }
        };


        List<DurationBetweenLocations> durationBetweenLocationsMinutesMatrix = new List<DurationBetweenLocations>
        {
            new DurationBetweenLocations { From = "A", To = "B", DurationMinutes = 15 },
            new DurationBetweenLocations { From = "A", To = "C", DurationMinutes = 20 },
            new DurationBetweenLocations { From = "A", To = "D", DurationMinutes = 10 },
            new DurationBetweenLocations { From = "B", To = "C", DurationMinutes = 5 },
            new DurationBetweenLocations { From = "B", To = "D", DurationMinutes = 25 },
            new DurationBetweenLocations { From = "C", To = "D", DurationMinutes = 25 }
        };


        List<Event> myFinalList = new List<Event>();


        var sortedEvents = events.OrderBy(e => DateTime.ParseExact(e.StartTime, "HH:mm", null)).ToList();

        for (int i = 0; i < sortedEvents.Count - 1; i++)
        {
            if (sortedEvents[i].StartTime == sortedEvents[i + 1].StartTime)
            {
                if (sortedEvents[i].Priority > sortedEvents[i + 1].Priority)
                {
                    myFinalList.Add(sortedEvents[i]);

                    sortedEvents.RemoveAt(i + 1);

                }
                else
                {
                    myFinalList.Add(sortedEvents[i + 1]);
                    //zaten bir önceki değerin priority'si daha küçükse o değeri listeden kaldırmaya gerek olmayabilir
                    //sortedEvents.RemoveAt(i);
                }

                i--;
                continue;
            }
            //Start time'ları farklıysa
            else
            {
                //Bir sonraki etkinliğe yetişemez. Hangi priority daha yüksek ona bak
                if (DateTime.ParseExact(sortedEvents[i].EndTime, "HH:mm", null) > DateTime.ParseExact(sortedEvents[i + 1].StartTime, "HH:mm", null))
                {
                    if (sortedEvents[i].Priority > sortedEvents[i+1].Priority)
                    {
                        //eğer zaten varsa eklemesin
                        if(!myFinalList.Contains(sortedEvents[i]))
                            myFinalList.Add(sortedEvents[i]);

                        if (myFinalList.Contains(sortedEvents[i+1]))
                            myFinalList.Remove(sortedEvents[i+1]);

                        sortedEvents.RemoveAt(i + 1);
                        
                    }

                    else
                    {
                        if (!myFinalList.Contains(sortedEvents[i+1]))
                            myFinalList.Add(sortedEvents[i+1]);

                        if (myFinalList.Contains(sortedEvents[i]))
                            myFinalList.Remove(sortedEvents[i]);

                        sortedEvents.RemoveAt(i);                        
                    }

                    i--;
                    continue;
                }

                else
                {
                    var location1 = sortedEvents[i].Location;
                    var location2 = sortedEvents[i + 1].Location;

                    //Aynı mekansa zaten yetişir, ona da katıldı.
                    if (location1 == location2)
                    {
                        if (!myFinalList.Contains(sortedEvents[i + 1]))
                            myFinalList.Add(sortedEvents[i + 1]);
                    }

                    else
                    {
                        var durationMinute = GetDurationBetweenLocations(location1, location2, durationBetweenLocationsMinutesMatrix);
                        TimeSpan timeSpan = new TimeSpan(0, durationMinute, 0);


                        //Sonraki etkinliğe yetişemiyor demektir. Priority karşılaştırması yap
                        if (DateTime.ParseExact(sortedEvents[i].EndTime, "HH:mm", null) + timeSpan > DateTime.ParseExact(sortedEvents[i + 1].StartTime, "HH:mm", null))
                        {
                            if (sortedEvents[i].Priority < sortedEvents[i + 1].Priority)
                            {
                                if (!myFinalList.Contains(sortedEvents[i + 1]))
                                    myFinalList.Add(sortedEvents[i + 1]);

                                if (myFinalList.Contains(sortedEvents[i]))
                                    myFinalList.Remove(sortedEvents[i]);
                               
                                sortedEvents.RemoveAt(i);
                            }
                            else
                            {
                                if (!myFinalList.Contains(sortedEvents[i]))
                                    myFinalList.Add(sortedEvents[i]);

                                if (myFinalList.Contains(sortedEvents[i+1]))
                                    myFinalList.Remove(sortedEvents[i+1]);

                                sortedEvents.RemoveAt(i + 1);
                            }
                        }
                        //Yetişiyorsa ekle
                        else
                        {
                            if (!myFinalList.Contains(sortedEvents[i + 1]))
                                myFinalList.Add(sortedEvents[i + 1]);
                        }
                    }
                }             
            }
        }

        List<int> Ids = new();
        int TotalValue = 0;

        for (int i = 0; i < myFinalList.Count; i++)
        {
            Ids.Add(myFinalList[i].Id);
            TotalValue += myFinalList[i].Priority;
        }


        Console.WriteLine("Katılınabilecek Maksimum Etkinlik Sayısı:" + Ids.Count);
        Console.WriteLine("Katılınabilecek Etkinliklerin ID'leri:" + String.Join(", ", Ids));
        Console.WriteLine("Toplam Değer:" + TotalValue);
    }
}

public class Event
{
    public int Id { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
    public string? Location { get; set; }
    public int Priority { get; set; }
}

public class DurationBetweenLocations
{
    public string? From { get; set; }
    public string? To { get; set; }
    public int DurationMinutes { get; set; }
}



