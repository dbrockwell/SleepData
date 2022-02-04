using System;
using System.IO;

namespace SleepData
{
    class Program
    {
        static void Main(string[] args)
        {
            // ask for input
            Console.WriteLine("Enter 1 to create data file.");
            Console.WriteLine("Enter 2 to parse data.");
            Console.WriteLine("Enter anything else to quit.");
            // input response
            string resp = Console.ReadLine();

            if (resp == "1")
            {
                // TODO: create data file
                // create data file

                 // ask a question
                Console.WriteLine("How many weeks of data is needed?");
                // input the response (convert to int)
                int weeks = int.Parse(Console.ReadLine());
                // determine start and end date
                DateTime today = DateTime.Now;
                // we want full weeks sunday - saturday
                DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
                // subtract # of weeks from endDate to get startDate
                DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
                
                // random number generator
                Random rnd = new Random();

                                // create file
                StreamWriter sw = new StreamWriter("data.txt");
                // loop for the desired # of weeks
                while (dataDate < dataEndDate)
                {
                    // 7 days in a week
                    int[] hours = new int[7];
                    for (int i = 0; i < hours.Length; i++)
                    {
                        // generate random number of hours slept between 4-12 (inclusive)
                        hours[i] = rnd.Next(4, 13);
                    }
                    // M/d/yyyy,#|#|#|#|#|#|#
                    //Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
                    sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
                    // add 1 week to date
                    dataDate = dataDate.AddDays(7);
                }
                sw.Close();
            }
            else if (resp == "2")
            {
                // TODO: parse data file
                StreamReader readData = new StreamReader("data.txt");
                while (!readData.EndOfStream) {
                    string data = readData.ReadLine();
                    string[] split = data.Split(',');
                    string[] datasplit = split[1].Split('|');
                    var parseDate = DateTime.Parse(split[0]);
                    int[] intData = new int[datasplit.Length];
                    for (int i = 0; i < datasplit.Length; i++) {
                        intData[i] = Int32.Parse(datasplit[i]);
                    }
                    int total = intData[0] + intData[1] + intData[2] + intData[3] + intData[4] + intData[5] + intData[6];
                    double totalDouble = Convert.ToDouble(total);
                    double average = totalDouble / 7;
                    Console.WriteLine("Week of {0:MMM}, {0:dd}, {0:yyyy}", parseDate);
                    Console.WriteLine("Mo Tu We Th Fr Sa Su Tot Avg");
                    Console.WriteLine("-- -- -- -- -- -- -- --- ---");
                    Console.WriteLine("{0,2} {1,2} {2,2} {3,2} {4,2} {5,2} {6,2} {7,3} {8,3:n1}", datasplit[0], datasplit[1], datasplit[2], datasplit[3], datasplit[4], datasplit[5], datasplit[6], total, average);
                }
                readData.Close();
            }
        }
    }
}
