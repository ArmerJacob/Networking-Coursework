using System;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Location
{
    public class LocationClass
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                location.locationUI ui = new location.locationUI();
                Application.Run(ui);
            }
            else
            {
                Location.LocationClass client = new LocationClass();
                client.RunClient(args);
            }
            //Checks if there are any arguments, if there are 0 it runs the UI, otherwise it runs the client with the arguments as the input.
        }
        public string RunClient(string[] args)
        {
            string clientOutput = null;
            location.locationUI ui = new location.locationUI();
            //Creates a string "clientOutput" to return to the ui for the output text box.
            //Creates a instance of the UI.
            try
            {
                TcpClient client = new TcpClient();
                string IP = "whois.net.dcs.hull.ac.uk";
                string protocol = "whois";
                int port = 43;
                //Creates the TCP client and initializes the IP, port and protocol defaults.
                string locationMessageTemp = null;
                //Creates a string used to hold the args temporally so it can be organized for sending to the server.
                List<string> locationMessage = new List<string>();                
                for (int i = 0; i < args.Length; i++)
                {
                    switch (args[i])
                    {
                        case "-h":
                            if (args[i + 1] != null && args[i + 1] != "")
                            {
                                IP = args[i + 1];
                            }
                            args[i] = null;
                            args[i + 1] = null;
                            break;
                            // when, or if, it finds -h it stores the IP and then replaces -h and the IP in the array with null.
                        case "-p":
                            if (args[i + 1] != null && args[i + 1] != "")
                            {
                                port = int.Parse(args[i + 1]);
                            }
                            args[i] = null;
                            args[i + 1] = null;
                            break;
                            //when, or if, it finds -p it stores the port and then replaces -p and the port in the array with null.
                        case "-h0":
                            protocol = args[i];
                            args[i] = null;
                            break;
                            //when, or if, it finds -h0 it stores the protocol and replaces it in the array with null.
                        case "-h1":
                            protocol = args[i];
                            args[i] = null;
                            break;
                            //when, or if, it finds -h1 it stores the protocol and replaces it in the array with null.
                        case "-h9":
                            protocol = args[i];
                            args[i] = null;
                            break;
                            //when, or if, it finds -h9 it stores the protocol and replaces it in the array with null.

                    }
                    //if it didnt find any of these it will use the defaults specified above.
                    if (args[i] != null && args[i] != "")
                    {
                        locationMessageTemp += args[i] + " ";
                        //here it stores all of the things in the array that are not the protocol, port or IP
                    }
                }

                string[] locationMessageArray = locationMessageTemp.Split(new char[] {' '}, 2);
                locationMessage.Add(locationMessageArray[0]);
                locationMessage.Add(locationMessageArray[1]);
                //Stores the Temporary variable into a list, with a length of 2, this allows the location to be multiple words without speachmarks.
                client.Connect(IP, port);
                client.ReceiveTimeout = 1000;
                client.SendTimeout = 1000;
                StreamWriter sw = new StreamWriter(client.GetStream());
                StreamReader sr = new StreamReader(client.GetStream());
                //sets up the connection to the server and the timeout.
                switch (protocol)
                {
                    case "whois":
                        clientOutput = WhoIs(locationMessage, sw, sr, clientOutput);
                        break;
                    case "-h0":
                        clientOutput = HTTPOne(locationMessage, sw, sr, port, clientOutput);
                        break;
                    case "-h1":
                        clientOutput = HTTPOnepOne(locationMessage, sw, sr, IP, port, clientOutput);
                        break;
                    case "-h9":
                        clientOutput = HTTPZeropNine(locationMessage, sw, sr, port, clientOutput);
                        break;
                }     
                //runs the appropriate protocol.
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                clientOutput += e + "\r\n";
            }
            return clientOutput;
            //returns the writelines to the UI so it can print it to the text box.
        }

        private static string WhoIs(List<string> locationMessage, StreamWriter sw, StreamReader sr, string clientOutput)
        {
            location.locationUI ui = new location.locationUI();
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
                        clientOutput += "ERROR: no entries found\r\n";
                    }
                    else
                    {
                        response = response.TrimEnd(' ');
                        Console.WriteLine(locationMessage[0] + " is " + response);
                        clientOutput += locationMessage[0] + " is " + response + "\r\n";
                    }
                    //This is the whois for when the client asks the server for the location of a user.

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
                        clientOutput += locationMessage[0] + " location changed to be " + locationMessage[1] + "\r\n";   
                    }
                    //This is the whois for updating a users location.
                }
            }
            else
            {
                Console.WriteLine("No arguments");
                clientOutput += "No arguments \r\n";
            }
            //This is to catch when there are no arguments provided
            ui.clientOutput.Text += "\r\n";
            return clientOutput;
            //This returns the clientOutput so the ui can print the log.
        }
        private static string HTTPOnepOne(List<string> locationMessage, StreamWriter sw, StreamReader sr, string hostName, int port, string clientOutput)
        {
            location.locationUI ui = new location.locationUI();
            List<string> serverResponce = new List<string>();
            locationMessage[1] = locationMessage[1].TrimEnd(' ');
            if (locationMessage.Count >= 1)
            {
                if (locationMessage[1] == "")
                {
                    sw.WriteLine("GET /?name=" + locationMessage[0] + " HTTP/1.1");
                    sw.WriteLine("Host: " + hostName + "\r\n");
                    sw.Flush();
                    //This sends the HTTP 1.1 request to the server
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
                                    clientOutput += locationMessage[0] + " is " + temp + "\r\n";
                                    while (true)
                                    {
                                        string tempBeta = sr.ReadLine();
                                        if (tempBeta != null)
                                        {
                                            Console.WriteLine(tempBeta);
                                            clientOutput += tempBeta + "\r\n";
                                        }
                                        else
                                        {
                                            break;
                                        }
                                        //This is the read in for web servers.
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
                            clientOutput += locationMessage[0] + " is " + resonceAlpha[3] + "\r\n";

                        }
                        if (resonceAlpha[0] == "HTTP/1.1 404 Not Found")
                        {
                            Console.WriteLine(resonceAlpha[0]);
                            Console.WriteLine(resonceAlpha[1]);
                            clientOutput += resonceAlpha[0] + "\r\n";
                            clientOutput += resonceAlpha[1] + "\r\n";
                        }
                        //This is the HTTP 1.1 for when the client asks the server for the location of a user.
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
                    //This sends the appropriate HTTP 1.1 request to the server. 
                    System.Threading.Thread.Sleep(10);
                    serverResponce = getResponce(serverResponce, sr);
                    if (serverResponce[0] == "HTTP/1.1 200 OK")
                    {
                        locationMessage[1] = locationMessage[1].TrimEnd(' ');
                        Console.WriteLine(locationMessage[0] + " location changed to be " + locationMessage[1]);
                        clientOutput += locationMessage[0] + " location changed to be " + locationMessage[1] + "\r\n";

                    }
                    else
                    {
                        Console.WriteLine("Failed to update");
                        clientOutput += "Failed to update\r\n";
                    }
                    //This is the HTTP 1.1 for when the client updates a user's location.
                }

            }
            else
            {
                Console.WriteLine("No arguments");
                clientOutput += "No arguments\r\n";
            }
            //this is to catch when there are no arguments.
            ui.clientOutput.Text += "\r\n";
            return clientOutput;
            //This returns the clientOutput so the ui can print the log.
        }
        private static string HTTPOne(List<string> locationMessage, StreamWriter sw, StreamReader sr, int port, string clientOutput)
        {
            location.locationUI ui = new location.locationUI();
            List<string> serverResponce = new List<string>();
            locationMessage[1] = locationMessage[1].TrimEnd(' ');
            if (locationMessage.Count >= 1)
            {
                if (locationMessage[1] == "")
                {
                    sw.WriteLine("GET /?" + locationMessage[0] + " HTTP/1.0" + "\r\n");
                    sw.Flush();
                    //This sends the HTTP 1.0 request to the server for a users location.
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
                                    clientOutput += locationMessage[0] + " is " + temp + "\r\n";
                                    while (true)
                                    {
                                        string tempBeta = sr.ReadLine();
                                        if (tempBeta != null)
                                        {
                                            Console.WriteLine(tempBeta);
                                            clientOutput += tempBeta + "\r\n";
                                        }
                                        else
                                        {
                                            break;
                                        }
                                        //This is the read in for web servers.
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
                        System.Threading.Thread.Sleep(10);
                        responceAlpha = getResponce(responceAlpha, sr);
                        if (responceAlpha[0] == "HTTP/1.0 200 OK")
                        {
                            locationMessage[1] = locationMessage[1].TrimEnd(' ');
                            Console.WriteLine(locationMessage[0] + " is " + responceAlpha[3]);
                            clientOutput += locationMessage[0] + " is " + responceAlpha[3] + "\r\n";
                        }
                        if (responceAlpha[0] == "HTTP/1.0 404 Not Found")
                        {
                            Console.WriteLine(responceAlpha[0]);
                            Console.WriteLine(responceAlpha[1]);
                            clientOutput += responceAlpha[0];
                            clientOutput += responceAlpha[1];
                        }
                        //This is dealing with the responce from the server for the location of a user.
                    }
                }
                if (locationMessage[1] != "")
                {
                    int asd = locationMessage[1].Length;
                    sw.WriteLine("POST /" + locationMessage[0] + " HTTP/1.0");
                    sw.WriteLine("Content-Length: " + asd + "\r\n");
                    sw.Write(locationMessage[1]);
                    sw.Flush();
                    System.Threading.Thread.Sleep(10);
                    serverResponce = getResponce(serverResponce, sr);
                    if (serverResponce[0] == "HTTP/1.0 200 OK")
                    {
                        locationMessage[1] = locationMessage[1].TrimEnd(' ');
                        Console.WriteLine(locationMessage[0] + " location changed to be " + locationMessage[1]);
                        clientOutput += locationMessage[0] + " location changed to be " + locationMessage[1] + "\r\n";
                    }
                    else
                    {
                        Console.WriteLine("Failed to update");
                        clientOutput += "Failed to update";
                    }
                    //This is the client updating a users location to the server.

                }
            }
            else
            {
                Console.WriteLine("No arguments");
                clientOutput += "No arguments";
            }
            clientOutput += "\r\n";
            return clientOutput;
            //This prints the clientOutput to the UI.
        }
        private static string HTTPZeropNine(List<string> locationMessage, StreamWriter sw, StreamReader sr, int port, string clientOutput)
        {
            location.locationUI ui = new location.locationUI();
            List<string> serverResponce = new List<string>();
            locationMessage[1] = locationMessage[1].TrimEnd(' ');
            if (locationMessage.Count >= 1)
            {
                if (locationMessage[1] == "")
                {

                    sw.WriteLine("GET /" + locationMessage[0]);
                    sw.Flush();
                    //This sends the HTTP 0.9 request for a users location.
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
                                    clientOutput += locationMessage[0] + " is " + temp + "\r\n";
                                    while (true)
                                    {
                                        string tempBeta = sr.ReadLine();
                                        if (tempBeta != null)
                                        {
                                            Console.WriteLine(tempBeta);
                                            clientOutput += tempBeta + "\r\n";
                                        }
                                        else
                                        {
                                            break;
                                        }
                                        //This prints the responce from a webserver.
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
                            clientOutput += locationMessage[0] + " is " + responceAlpha[3] + "\r\n";
                        }
                        if (responceAlpha[0] == "HTTP/0.9 404 Not Found")
                        {
                            Console.WriteLine(responceAlpha[0]);
                            Console.WriteLine(responceAlpha[1]);
                            clientOutput += responceAlpha[0] + "\r\n";
                            clientOutput += responceAlpha[1] + "\r\n";
                        }
                        //This deals with the servers responce for a users location.
                    }

                }
                if (locationMessage[1] != "")
                {
                    sw.WriteLine("PUT /" + locationMessage[0]);
                    sw.WriteLine();
                    sw.WriteLine(locationMessage[1]);
                    sw.Flush();
                    //This sends the HTTP 0.9 request for the server to update a users location.
                    System.Threading.Thread.Sleep(10);
                    serverResponce = getResponce(serverResponce, sr);
                    if (serverResponce[0] == "HTTP/0.9 200 OK")
                    {
                        locationMessage[1] = locationMessage[1].TrimEnd(' ');
                        Console.WriteLine(locationMessage[0] + " location changed to be " + locationMessage[1]);
                        clientOutput += locationMessage[0] + " location changed to be " + locationMessage[1] + "\r\n";
                    }
                    else
                    {
                        Console.WriteLine("Failed to update");
                        clientOutput += "Failed to update";
                    }
                    //This is the server dealing with the servers responce to the request.
                }
            }
            else
            {
                Console.WriteLine("No arguments");
                clientOutput += "No arguments";
            }

            clientOutput += "\r\n";
            return clientOutput;
            //This returns the clientOutput to the UI so it can print it to the text box.
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
            //This is the getResponce method that reads in a responce from the server.
        }
    }
}

