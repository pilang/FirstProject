using System;
using System.Collections.Generic;
using System.IO;   // För filoperationer
using System.Linq;
using System.Text.RegularExpressions;

namespace Förberedande_Kurs
{
    
    class Program
    {
        static void Main(string[] args)

            // TODO: Fånga felformaterade svar som inte kan bli tal.
            
        {
            int input=1;
            int tal1, tal2;
            var slump = new Random();

            string filepath = @"testfil.txt";   // Kanske blir mer plattformsoberoende om man skippar absolut sökväg

            if (!File.Exists(filepath))  // Gör filen och fyll med något om den inte finns
            {
                using (StreamWriter sw = File.CreateText(filepath))   
                {
                    sw.WriteLine("Hello File");
                }
            }
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;

            while (input != 0)
            {
                Console.Clear();
                Console.WriteLine(" 0: Exit");
                Console.WriteLine(" 1: Hello World");
                Console.WriteLine(" 2: Namn och ålder");
                Console.WriteLine(" 3: Byt färg");
                Console.WriteLine(" 4: Dagens datum");
                Console.WriteLine(" 5: Jämför tal");
                Console.WriteLine(" 6: Gissa tal");
                Console.WriteLine(" 7: Lagra text på disk");
                Console.WriteLine(" 8: Visa lagrad textfil");
                Console.WriteLine(" 9: Intressant fakta om valfritt decimaltal");
                Console.WriteLine("10: Gångertabell");
                Console.WriteLine("11: Sortera en array");
                Console.WriteLine("12: Palindromtest");
                Console.WriteLine("13: Lista mellanliggande tal");
                Console.WriteLine("14: Lista sorterade tal efter udda/jämn");
                Console.WriteLine("15: Summera tal");
                Console.WriteLine("16: Gör två karaktärer");

                //input = Int32.Parse(Console.ReadLine());
                bool resp = Int32.TryParse(Console.ReadLine(), out input);
                if (!resp)
                {
                    input = 17;
                }
                

                switch (input)
                {
                    case 1:
                        Ex1();
                        break;

                    case 2:
                        Ex2();
                        break;

                    case 3:
                        // Gör ett byte mellan förgrund och bakgrund. Då blir det lite mer flexibelt
                        ConsoleColor temp;
                        temp = Console.ForegroundColor;
                        Console.ForegroundColor = Console.BackgroundColor;
                        Console.BackgroundColor = temp;
                        break;

                    case 4:
                        var nowdate = DateTime.Now;
                        Console.WriteLine(nowdate.ToString("yyyy-MM-dd"));
                        break;

                    case 5:
                        Console.Write("Tal1: ");
                        tal1 = Int32.Parse(Console.ReadLine());
                        Console.Write("Tal2: ");
                        tal2 = Int32.Parse(Console.ReadLine());
                        Console.WriteLine(Math.Max(tal1, tal2) + " är det största talet!");
                        break;

                    case 6:
                        int gissningsant = 0;
                        int tal = slump.Next(1, 100);
                        int gissning = 0;  // tal som inte kan ha slumpats
                        while(gissning != tal)
                        {
                            Console.WriteLine("Gissa vad talet är (1-100):");
                            gissning = Int32.Parse(Console.ReadLine());
                            gissningsant++;
                            if(gissning > tal)
                            {
                                Console.WriteLine("För stort, försök igen!");
                            }
                            else if(gissning < tal){
                                Console.WriteLine("För litet, försök igen!");
                            }
                            else
                            {
                                Console.WriteLine("Grattis, talet var " + tal);
                                Console.WriteLine("Du gissade rätt efter " + gissningsant + " gissningar.");
                            }
                        }
                        break;

                    case 7:
                        Console.WriteLine("Mata in text. Avsluta med tomrad");
                        using (StreamWriter sw = File.CreateText(filepath))
                        {
                            string fileinput="asdf"; // måste initieras med något...
                            while (fileinput != "")
                            {
                                fileinput = Console.ReadLine();
                                sw.WriteLine(fileinput);
                            }
                        }
                        break;

                    case 8:
                        using (StreamReader sr = File.OpenText(filepath))
                        {
                            string rad;
                            while ((rad = sr.ReadLine()) != null)
                            {
                                Console.WriteLine(rad);
                            }
                        }
                        break;

                    case 9:
                        Console.WriteLine("Skriv in ett decimaltal. (Använd '.' som decimalavdelare");
                        float dectal = float.Parse(Console.ReadLine());
                        Console.WriteLine("sqrt( " + dectal + " ) = " + Math.Sqrt(dectal));
                        Console.WriteLine(dectal + " ^ 2 = " + Math.Pow(dectal, 2));
                        Console.WriteLine(dectal + " ^ 10 = " + Math.Pow(dectal, 10));
                        break;

                    case 10:
                        Console.Write("Storlek: ");
                        int storlek = int.Parse(Console.ReadLine());
                        Console.Write("       ");
                        for (int i = 1; i <= storlek ; i++)
                        {
                            Console.Write("{0,5}",i);
                        }
                        Console.WriteLine("");


                        Console.Write("       ");
                        for(int i=1; i <= storlek ; i++)
                        {
                            Console.Write("_____");
                        }
                        Console.WriteLine("");

                        for (int i=1; i <= storlek; i++)
                        {
                            //int j = 1;
                            Console.Write("{0,5} |",i);
                            for (int j=1; j <= storlek; j++)
                            {
                                Console.Write("{0,5}",i * j);
                            }
                            Console.WriteLine("");
                        }
                        break;

                    case 11:
                        Console.Write("Storlek: ");
                        int size = int.Parse(Console.ReadLine());
                        int[] unsorted = new int[size];

                        for(int i=0; i<unsorted.Length; i++)
                        {
                            unsorted[i] = slump.Next(0, 100);
                        }
                        var sorted = (int[])unsorted.Clone();
                        Array.Sort(sorted);

                        Console.WriteLine("Osorterade: ");
                        foreach (int j in unsorted)
                        {
                            Console.Write("{0,4}", j);
                        }

                        Console.WriteLine("\nSorterade:");
                        foreach (int j in sorted)
                        {
                            Console.Write("{0,4}", j);
                        }
                        break;

                    case 12:
                        Console.Write("Uttryck att testa: ");
                        string pal = Console.ReadLine();
                        string clean = Regex.Replace(pal.ToUpper(), @"[^A-Ö]", "");   // Till uppercase och tvätta bort allt annat än bokstäver
                        //Console.WriteLine(clean);
                        // Så nu borde detta funka: "Mus rev inuits öra, sa röst i universum"

                        int l = clean.Length -1;
                        bool ispal = true;    // en tom string är ett palindrom
                        //Console.WriteLine(((l+1)/2)-1);   // För att slippa jämföra mitten vid udda.
                        //Console.WriteLine(l / 2);
                        for (int i = 0; i <= ((l+1)/2)-1; i++)   // Kanske räcker att gå till mitten, om det är udda behöver inte mitten jämföras med sig själv, därför ((l+1)/2)-1
                        {
                            System.Diagnostics.Debug.WriteLine("(" + clean[i] + "," + clean[l - i] + ")"); // för att testa att alla jämförelser görs
                            if (clean[i] != clean[l - i])
                            {
                                ispal = false;
                                break;              // det räcker att hitta en olikhet
                            }
                        }             
                            
                        Console.WriteLine(ispal ? "Uttrycket är ett palindrom": "Uttrycket är INTE ett palindrom");
                        break;

                    case 13:
                        Console.Write("Tal 1: ");
                        int low = int.Parse(Console.ReadLine());
                        Console.Write("Tal 2: ");
                        int high = int.Parse(Console.ReadLine());
                        int riktning = (low < high) ? 1 : -1 ;   // Kanske måste ta hänsyn till om det största talet matades in först
                        for (low+=riktning; low != high; low+=riktning)
                            Console.Write("{0,3}", low);
                        break;

                    case 14:
                        Console.Write("Kommaseparerad list på tal: ");

                        string numbers = Console.ReadLine();

                        string[] nums = numbers.Split(',');
                        List<int> Odd = new List<int>();
                        List<int> Even = new List<int>();


                        int t;
                        foreach (var n in nums)
                        {
                            t = int.Parse(n);
                            if (t % 2 == 0)
                            {
                                Even.Add(t);
                            }
                            else
                            {
                                Odd.Add(t);
                            }
                        }

                        Even.Sort();
                        Odd.Sort();

                        Console.Write("Jämna tal: ");
                        foreach (var n in Even)
                            Console.Write("{0,4}", n);

                        Console.Write("\nUdda tal: ");
                        foreach (var n in Odd)
                            Console.Write("{0,4}", n);

                        break;

                    case 15:
                        Console.Write("Tal att summera: ");
                        string addingstring = Console.ReadLine();
                        var numArray = addingstring.Split(',').Select(int.Parse).ToArray();
                        int sum = 0;
                        foreach (var n in numArray)
                            sum += n;

                        Console.Write("Summan av talen är " + sum);

                        break;

                    case 16:
                        Console.Write("Namn1: ");
                        var p1 = new Person(Console.ReadLine());  // Person p = ... kanske
                        Console.Write("Namn2: ");
                        var p2 = new Person(Console.ReadLine());  // Person p = ... kanske
                        Console.WriteLine(p1);
                        Console.WriteLine(p2);

                    
                       break;



                    case 17:
                    

      

                        Console.WriteLine("Unvalid input");
                        break;

                   // case default:
                     //   Console.WriteLine("Unvalid input");
                       // break;

                    //case default:
                      //  break;


                }
                Console.WriteLine("\nPress any key to continue");
                Console.ReadKey();
            }
            Console.WriteLine("Bye!");
        }

        private static void Ex2()
        {
            String namn;
            String enamn;
            int age;
            Console.Write("Ange förnamn: ");
            namn = Console.ReadLine();
            Console.Write("Ange efternamn:");
            enamn = Console.ReadLine();
            Console.Write("Ange ålder:");
            age = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Namn: " + namn + " " + enamn);
            Console.WriteLine("Ålder: " + age);
        }

        private static void Ex1()
        {
            Console.WriteLine("Hello World!");
        }
    }
}
