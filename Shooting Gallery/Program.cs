using System.Text;
class Program
{
    public static int a, i, j, instructor1 = 0, instructor2 = 3, ttotal = 0, tshot = 0, t;
    public static int[] Lanes = new int[6];
    public static int[] Attempt = new int[13];
    public static Queue<int> participant = new Queue<int>();
    public static Random random = new Random();
    public static bool occupied1 = false, occupied2 = false;

    public static void Main()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var enc1251 = Encoding.GetEncoding(1251);
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = enc1251;

        //формирование очереди
        for (i = 1; i < 14; i++)
        { participant.Enqueue(i); }

        //стрельбы
        for (i = 1; i < 13; i++) {
            while (Attempt[i] < 5)
            { shot(); } }

        //вывод времени
        TimeSpan TST = new TimeSpan(0, 0, ttotal);
        TimeSpan TSS = new TimeSpan(0, 0, tshot);
        Console.WriteLine("Сумма затраченного времени: {0} минут {1} секунд", TST.Minutes, TST.Seconds);
        Console.WriteLine("Длительность стрельб:  {0} минут {1} секунд", TSS.Minutes, TSS.Seconds);

        Console.ReadKey();
    }

    //последовательность действий стрельбы
    public static void shot()
    {
        //заполнение направлений
        foreach (int number in participant.ToList())
        {
            for (i = 0; i < 6; ++i)
            {
                j = i + 1;
                if (Lanes[i] == 0 && participant.Count != 0)
                {
                    Lanes[i] = participant.Dequeue();
                    Console.WriteLine("Стрелок " + Lanes[i] + " занял направление " + j);
                }
            }
        }
        ttotal = ttotal + random.Next(3, 10);

        Parallel.For(0, 6, i =>
       {
           //направления 1-го инструктора
           if (Lanes[i] != 0 && i == instructor1)
           {
               occupied1 = true;
               j = i + 1;
               a = Attempt[Lanes[i] - 1] + 1;

               Console.WriteLine("Инструтктор 1, направление " + j + ", подготовиться к стрельбе");
               Console.WriteLine("Направление " + j + ", Инструктор 1, Стрелок " + Lanes[i] + " к стрельбе готов");
               Console.WriteLine("Инструтктор 1, направление " + j + " произвести стрельбу");
               Console.WriteLine("Направление " + j + ", Инструктор 1, Стрелок " + Lanes[i] + " стрельбу окончил! (попытка номер " + a + ")");

               //стрелок возвращается в очереь при наличии попыток
               Attempt[Lanes[i] - 1] = Attempt[Lanes[i] - 1] + 1;
               if (Attempt[Lanes[i] - 1] < 5)
               { participant.Enqueue(Lanes[i]); }

               //направвление освобождается
               Lanes[i] = 0;
           }

           //направления 2-го инструктора

           if (Lanes[i] != 0 && i == instructor2)
           {
               occupied2 = true;
               j = i + 1;
               a = Attempt[Lanes[i] - 1] + 1;

               Console.WriteLine("Инструтктор 2, направление " + j + ", подготовиться к стрельбе");
               Console.WriteLine("Направление " + j + ", Инструктор 2, Стрелок " + Lanes[i] + " к стрельбе готов");
               Console.WriteLine("Инструтктор 2, направление " + j + " произвести стрельбу");
               Console.WriteLine("Направление " + j + ", Инструктор 2, Стрелок " + Lanes[i] + " стрельбу окончил! (попытка номер " + a + ")");

               //стрелок возвращается в очереь при наличии попыток
               Attempt[Lanes[i] - 1] = Attempt[Lanes[i] - 1] + 1;
               if (Attempt[Lanes[i] - 1] < 5)
               { participant.Enqueue(Lanes[i]); }

               //направвление освобождается
               Lanes[i] = 0;
           }

       });

        if (occupied1 == true || occupied2 == true)
        {
            //время инструктор подготовится к стрельбе
            ttotal = ttotal + random.Next(2, 6);
            //время стрелок к стельбе готов
            ttotal = ttotal + random.Next(1, 4);
            //время инструктор командует произвести стрельбу
            ttotal = ttotal + random.Next(1, 2);
            //время стрелок стреляет
            t = random.Next(5, 15);
            ttotal = ttotal + t;
            tshot = tshot + t;

            occupied1 = false;
            occupied2 = false;
        }


        //инструктор переходит на следующее направление
        if (instructor1 < 2)
        { instructor1 = instructor1 + 1; }
        else { instructor1 = 0; }
        if (instructor2 < 5)
        { instructor2 = instructor2 + 1; }
        else { instructor2 = 3; }
    }
}