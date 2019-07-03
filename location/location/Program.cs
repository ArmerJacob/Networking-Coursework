using System;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;

namespace Location
{
    public class Location
    {
        static void Main(string[] args)
        {
            try
            {
                TcpClient client = new TcpClient();
                string IP = "whois.net.dcs.hull.ac.uk";
                string protocol = "whois";
                string locationMessageTemp = null;
                int port = 43;
                List<string> locationMessage = new List<string>();                
                for (int i = 0; i < args.Length; i++)
                {
                    switch (args[i])
                    {
                        case "-h":
                            IP = args[i + 1];
                            args[i] = null;
                            args[i + 1] = null;
                            break;

                        case "-p":
                            port = int.Parse(args[i + 1]);
                            args[i] = null;
                            args[i + 1] = null;
                            break;
                        case "-h0":
                            protocol = args[i];
                            //IP = args[i + 1];
                            args[i] = null;
                            //args[i + 1] = null;
                            break;
                        case "-h1":
                            protocol = args[i];
                            //IP = args[i + 1];
                            args[i] = null;
                            //args[i + 1] = null;
                            break;
                        case "-h9":
                            protocol = args[i];
                            //IP = args[i + 1];
                            args[i] = null;
                            //args[i + 1] = null;
                            break;

                    }
                    if (args[i] != null)
                    {
                        locationMessageTemp += args[i] + " ";
                    }
                }

                string[] locationMessageArray = locationMessageTemp.Split(new char[] {' '}, 2);
                locationMessage.Add(locationMessageArray[0]);
                locationMessage.Add(locationMessageArray[1]);
                client.Connect(IP, port);
                client.ReceiveTimeout = 1000;
                client.SendTimeout = 1000;
                StreamWriter sw = new StreamWriter(client.GetStream());
                StreamReader sr = new StreamReader(client.GetStream());
                switch (protocol)
                {
                    case "whois":
                        WhoIs(locationMessage, sw, sr);
                        break;
                    case "-h0":
                        HTTPOne(locationMessage, sw, sr, port);
                        break;
                    case "-h1":
                        HTTPOnepOne(locationMessage, sw, sr, IP, port);
                        break;
                    case "-h9":
                        HTTPZeropNine(locationMessage, sw, sr, port);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        private static void WhoIs(List<string> locationMessage, StreamWriter sw, StreamReader sr)
        {

            if (locationMessage.Count >= 1)
            {
                if (locationMessage[1] == "")
                {
                    sw.WriteLine(locationMessage[0]);
                    sw.Flush();
                    System.Threading.Thread.Sleep(10);
                    string response = sr.ReadLine();
                    if (response == "ERROR: no entries found")
                    {
                        Console.WriteLine("ERROR: no entries found");
                    }
                    else
                    {
                        response = response.TrimEnd(' ');
                        Console.WriteLine(locationMessage[0] + " is " + response);
                    }

                }
                else
                {
                    sw.WriteLine(locationMessage[0] + " " + locationMessage[1]);
                    sw.Flush();
                    System.Threading.Thread.Sleep(10);
                    string response = sr.ReadLine();
                    if (response == "OK")
                    {
                        locationMessage[1] = locationMessage[1].TrimEnd(' ');
                        Console.WriteLine(locationMessage[0] + " location changed to be " + locationMessage[1]);
                    }
                }
            }
            else
            {
                Console.WriteLine("No arguments");
            }



        }
        private static void HTTPOnepOne(List<string> locationMessage, StreamWriter sw, StreamReader sr, string hostName, int port)
        {
            List<string> serverResponce = new List<string>();
            if (locationMessage.Count >= 1)
            {
                if (locationMessage[1] == "")
                {
                    sw.WriteLine("GET /?name=" + locationMessage[0] + " HTTP/1.1");
                    sw.WriteLine("Host: " + hostName + "\r\n");
                    sw.Flush();
                    
                    if (port == 80)
                    {
                        while (true)
                        {
                            try
                            {
                                string temp;
                                temp = sr.ReadLine();
                                if (temp == "<!DOCTYPE html>")
                                {
                                    Console.WriteLine(locationMessage[0] + " is " + temp);
                                    while (true)
                                    {
                                        string tempBeta = sr.ReadLine();
                                        if (tempBeta != null)
                                        {
                                            Console.WriteLine(tempBeta);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                    }
                                }
                            }
                            catch
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        List<string> resonceAlpha = new List<string>();
                        System.Threading.Thread.Sleep(10);
                        resonceAlpha = getResponce(resonceAlpha, sr);
                        resonceAlpha[0] = resonceAlpha[0].TrimEnd(' ');
                        if (resonceAlpha[0] == "HTTP/1.1 200 OK")
                        {
                            locationMessage[1] = locationMessage[1].TrimEnd(' ');
                            Console.WriteLine(locationMessage[0] + " is " + resonceAlpha[3]);

                        }
                        if (resonceAlpha[0] == "HTTP/1.1 404 Not Found")
                        {
                            Console.WriteLine(resonceAlpha[0]);
                            Console.WriteLine(resonceAlpha[1]);
                        }

                    }
                }
                else if (locationMessage[1] != "")
                {
                    string message = "name=" + locationMessage[0] + "&location=" + locationMessage[1];
                    sw.WriteLine("POST / HTTP/1.1");
                    sw.WriteLine("Host: " + hostName);
                    sw.WriteLine("Content-Length: " + message.Length + "\r\n");
                    sw.Write(message);
                    sw.Flush();
                    System.Threading.Thread.Sleep(10);
                    serverResponce = getResponce(serverResponce, sr);
                    if (serverResponce[0] == "HTTP/1.1 200 OK")
                    {
                        locationMessage[1] = locationMessage[1].TrimEnd(' ');
                        Console.WriteLine(locationMessage[0] + " location changed to be " + locationMessage[1]);

                    }
                    else
                    {
                        Console.WriteLine("Failed to update");
                    }

                }

            }
            else
            {
                Console.WriteLine("No arguments");
            }




        }
        private static void HTTPOne(List<string> locationMessage, StreamWriter sw, StreamReader sr, int port)
        {
            List<string> serverResponce = new List<string>();
            if (locationMessage.Count >= 1)
            {
                if (locationMessage[1] == "")
                {
                    sw.WriteLine("GET /?" + locationMessage[0] + " HTTP/1.0" + "\r\n");
                    sw.Flush();
                    List<string> responceAlpha = new List<string>();
                    if (port == 80)
                    {
                        while (true)
                        {
                            try
                            {
                                string temp;
                                System.Threading.Thread.Sleep(10);
                                temp = sr.ReadLine();
                                if (temp == "<!DOCTYPE html>")
                                {
                                    Console.WriteLine(locationMessage[0] + " is " + temp);
                                    while (true)
                                    {
                                        string tempBeta = sr.ReadLine();
                                        if (tempBeta != null)
                                        {
                                            Console.WriteLine(tempBeta);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                    }
                                }
                            }
                            catch
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        responceAlpha = getResponce(responceAlpha, sr);
                        responceAlpha[0] = responceAlpha[0].TrimEnd(' '); 
                        if (responceAlpha[0] == "HTTP/1.0 200 OK")
                        {
                            locationMessage[1] = locationMessage[1].TrimEnd(' ');
                            Console.WriteLine(locationMessage[0] + " is " + responceAlpha[3]);
                        }
                        if (responceAlpha[0] == "HTTP/1.0 404 Not Found/r/n")
                        {
                            Console.WriteLine(responceAlpha[0]);
                            Console.WriteLine(responceAlpha[1]);
                        }
                    }
                }
                if (locationMessage[1] != "")
                {
                    sw.WriteLine("POST /" + locationMessage[0] + " HTTP/1.0");
                    sw.WriteLine("Content-Length: " + locationMessage[1].Length + "\r\n");
                    sw.Write(locationMessage[1]);
                    sw.Flush();
                    System.Threading.Thread.Sleep(10);
                    serverResponce = getResponce(serverResponce, sr);
                    if (serverResponce[0] == "HTTP/1.0 200 OK")
                    {
                        locationMessage[1] = locationMessage[1].TrimEnd(' ');
                        Console.WriteLine(locationMessage[0] + " location changed to be " + locationMessage[1]);
                    }
                    else
                    {
                        Console.WriteLine("Failed to update");
                    }


                }
            }
            else
            {
                Console.WriteLine("No arguments");
            }

        }
        private static void HTTPZeropNine(List<string> locationMessage, StreamWriter sw, StreamReader sr, int port)
        {
            List<string> serverResponce = new List<string>();
            if (locationMessage.Count >= 1)
            {
                if (locationMessage[1] == "")
                {

                    sw.WriteLine("GET /" + locationMessage[0]);
                    sw.Flush();
                    List<string> responceAlpha = new List<string>();
                    System.Threading.Thread.Sleep(10);
                    responceAlpha = getResponce(responceAlpha, sr);
                    if (port == 80)
                    {
                        while (true)
                        {
                            try
                            {
                                string temp;
                                System.Threading.Thread.Sleep(10);
                                temp = sr.ReadLine();
                                if (temp == "<!DOCTYPE html>")
                                {
                                    Console.WriteLine(locationMessage[0] + " is " + temp);
                                    while (true)
                                    {
                                        string tempBeta = sr.ReadLine();
                                        if (tempBeta != null)
                                        {
                                            Console.WriteLine(tempBeta);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                    }
                                }
                            }
                            catch
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (responceAlpha[0] == "HTTP/0.9 200 OK")
                        {
                            locationMessage[1] = locationMessage[1].TrimEnd(' ');
                            Console.WriteLine(locationMessage[0] + " is " + responceAlpha[3]);
                        }
                        if (responceAlpha[0] == "HTTP/0.9 404 Not Found")
                        {
                            Console.WriteLine(responceAlpha[0]);
                            Console.WriteLine(responceAlpha[1]);
                        }
                    }

                }
                if (locationMessage[1] != "")
                {
                    sw.WriteLine("PUT /" + locationMessage[0]);
                    sw.WriteLine();
                    sw.WriteLine(locationMessage[1]);
                    sw.Flush();
                    System.Threading.Thread.Sleep(10);
                    serverResponce = getResponce(serverResponce, sr);
                    if (serverResponce[0] == "HTTP/0.9 200 OK")
                    {
                        locationMessage[1] = locationMessage[1].TrimEnd(' ');
                        Console.WriteLine(locationMessage[0] + " location changed to be " + locationMessage[1]);
                    }
                    else
                    {
                        Console.WriteLine("Failed to update");
                    }

                }
            }
            else
            {
                Console.WriteLine("No arguments");
            }



        }
        private static List<string> getResponce(List<string> pResponceList, StreamReader sr)
        {
            string tempResponce = null;
            while (true)
            {
                try
                {
                    System.Threading.Thread.Sleep(10);
                    tempResponce = sr.ReadLine();
                    if (tempResponce != null)
                    {
                        pResponceList.Add(tempResponce);
                    }
                    else
                    {
                        break;
                    }
               }
               catch
                {
                    break;
               }
            }
            return pResponceList;

        }
    }
}

