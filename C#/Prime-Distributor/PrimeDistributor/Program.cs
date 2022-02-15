using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace PrimeDistributor
{
    class Program
    {

        static void Main(string[] args)
        {

            // List of out APIs
            string[] primesQueue = {
                  "https://localhost:44314/api/prime",
                  "https://localhost:44344/api/prime"
                   };
            // Queue number
            int iPrimesQue = 0;
            string input = "Booting primes";

            while (!input.Equals("quit"))
            {
                Console.Write("Enter a number: ");
                input = Console.ReadLine();
                input = input.Trim();

                // Validate user's input
                if (CheckInputForNumbers(input))
                {

                    int userNumber = int.Parse(input);

                    Console.WriteLine("Enter 1 if you want to find out if your number is a prime");
                    Console.WriteLine("Enter 2 if you want to add all primes that your number contains");

                    input = Console.ReadLine();

                    if (input.Equals("1"))
                    {
                        try
                        {
                            // Get spesific API from que and reset if it reached limit
                            if (iPrimesQue >= 2) iPrimesQue = 0;
                            LogData("User placed number: " + input + ", trying to connect to primesQueue: " + iPrimesQue + " at " + primesQueue[iPrimesQue]);
                            IsNumberAPrime(primesQueue[iPrimesQue], userNumber);
                        }
                        catch
                        {
                            Console.WriteLine("The server is down, requesting the next server...");

                            // Add one to que and reset que if it reache limit
                            iPrimesQue += 1;
                            if (iPrimesQue >= 2) iPrimesQue = 0;
                            LogData("User placed number: " + input + ", trying to connect to primesQueue: " + iPrimesQue + " at " + primesQueue[iPrimesQue]);
                            IsNumberAPrime(primesQueue[iPrimesQue], userNumber);
                        }
                        Console.WriteLine("APiUrl: " + primesQueue[iPrimesQue] + "APiNumber: " + (iPrimesQue + 1));
                        iPrimesQue += 1;
                    }
                    if (input.Equals("2"))
                    {
                        try
                        {
                            // Get spesific API from que and reset if it reached limit
                            if (iPrimesQue >= 2) iPrimesQue = 0;
                            LogData("User placed number: " + input + ", trying to connect to primesQueue: " + iPrimesQue + " at " + primesQueue[iPrimesQue]);

                            AddPrimes(primesQueue[iPrimesQue], userNumber);
                        }
                        catch
                        {
                            Console.WriteLine("The server is down, requesting the next server...");

                            // Add one to que and reset que if it reache limit
                            iPrimesQue += 1;
                            if (iPrimesQue >= 2) iPrimesQue = 0;
                            LogData("User placed number: " + input + ", trying to connect to primesQueue: " + iPrimesQue + " at " + primesQueue[iPrimesQue]);

                            AddPrimes(primesQueue[iPrimesQue], userNumber);
                        }
                        Console.WriteLine("APiUrl: " + primesQueue[iPrimesQue] + "APiNumber: " + (iPrimesQue + 1));
                        iPrimesQue += 1;
                    }
                }
            }
        }

        public static bool CheckInputForNumbers(string input)
        {
            // Simply checks if user placed a valid number
            return input.All(c => c >= '0' && c <= '9') && !string.IsNullOrEmpty(input);
        }


        public static void IsNumberAPrime(string apiUrl, int number)
        {
            if (!string.IsNullOrEmpty(apiUrl))
            {

                // Set up out API request
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;
                var json = client.DownloadData(apiUrl + "/" + number);

                var result = json;

                if (json != null)
                {
                    // Extract data from response
                    string download = Encoding.ASCII.GetString(json);
                    bool isPrime = (new JavaScriptSerializer()).Deserialize<bool>(download);
                    if (isPrime)
                    {
                        Console.WriteLine("Your number is indeed a prime! :)");
                        LogData("Successfully responded to user with answer that the a number is a prime");

                    }
                    else
                    {
                        Console.WriteLine("Your number is not a prime :(");
                        LogData("Successfully responded to user with answer that the a number is not a prime");
                    }
                }
                else
                {

                    Console.WriteLine("Something went wrong, please try again later");
                    LogData("Something went wrong while checking if a number is a prime");
                }


            }
        }

        public static void AddPrimes(string apiUrl, int number)
        {
            if (!string.IsNullOrEmpty(apiUrl))
            {
                // Set up out API request
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var json = client.UploadString(apiUrl + "/" + number, WebRequestMethods.Http.Post, JsonConvert.SerializeObject(number));

                if (json != null)
                {
                    // Extract data from response
                    int primeSum = (new JavaScriptSerializer()).Deserialize<int>(json);

                    LogData("User got a response that user's prime sum is: " + primeSum);
                    Console.WriteLine("Your number primes sum is: " + primeSum);

                }
                else
                {
                    Console.WriteLine("Something went wrong, please try again later");
                    LogData("Something went wrong while calculating primes sum");
                }

            }
        }

        public static void LogData(string data)
        {
            DateTime date = new DateTime();
            string currentDate = date.Date.ToShortDateString();

            string path = @"monitordata.txt";
            using (StreamWriter writer = File.AppendText(path))
            {
                writer.WriteLine(currentDate + " Action: ");
                writer.WriteLine(data);
                writer.Flush();
            }
        }
    }
}
