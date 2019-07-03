using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace locationserver
{
    class LocationServer
    {
        static void Main(string[] args)
        {
            runServer();

        }

        static void runServer()
        {
            TcpListener listener;
            Socket connection;
            Handler handler;


            try
            {
                listener = new TcpListener(IPAddress.Any, 43);
                listener.Start();
                Console.WriteLine("Server started Listening");
                while (true)
                {
                    connection = listener.AcceptSocket();
                    handler = new Handler();
                    Thread thread = new Thread(() => handler.doRequest(connection));
                    thread.Start();
                    Console.WriteLine("new thread started");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
    class Handler
    {
        static Dictionary<string, string> userDictionary = new Dictionary<string, string>();
        NetworkStream socketStream;
        public void doRequest(Socket connection)
        {
            socketStream = new NetworkStream(connection);
            Console.WriteLine("Connection Received");
            try
            {
               socketStream.ReadTimeout = 1000;
                socketStream.WriteTimeout = 1000;

                StreamWriter sw = new StreamWriter(socketStream);
                StreamReader sr = new StreamReader(socketStream);

                string clientInput = ReadInput(sr);
                Console.WriteLine("Input read input is: ");
                Console.WriteLine(clientInput);
                clientInput = clientInput.TrimEnd(' ');
                string[] clientInputArray = clientInput.Split(' ');
                if (clientInputArray[0] == "GET")
                {
                    if(clientInputArray.Length == 1)
                    {
                        Console.WriteLine("ran whois");
                        WhoIs(clientInput, sw);
                    }
                    if(clientInputArray.Length == 2 && !clientInputArray[1].StartsWith("/"))
                    {
                        Console.WriteLine("ran whois");
                        WhoIs(clientInput, sw);
                    }
                    else if(clientInputArray.Length == 2 && clientInputArray[1].StartsWith("/"))
                    {
                        Console.WriteLine("ran HTTP 0.9");
                        HTTPZeroPointNine(clientInput, sw);
                    }
                    else if (clientInputArray[2] == "HTTP/1.0")
                    {
                        Console.WriteLine("ran HTTP 1.0");
                        HTTPOnePointZero(clientInput, sw);
                    }
                    else if (clientInputArray[2] == "HTTP/1.1")
                    {
                        Console.WriteLine("ran HTTP 1.1");
                        HTTPOnePointOne(clientInput, sw);
                    }
                    else
                    {
                        Console.WriteLine("ran whois");
                        WhoIs(clientInput, sw);
                    }
                }
                else if (clientInputArray[0] == "POST")
                {
                    if(clientInputArray.Length == 1)
                    {
                        Console.WriteLine("ran whois");
                        WhoIs(clientInput, sw);
                    }
                    else if(clientInputArray.Length == 2)
                    {
                        Console.WriteLine("ran whois");
                        WhoIs(clientInput, sw);
                    }
                    else if (clientInputArray[2] == "HTTP/1.0")
                    {
                        Console.WriteLine("ran HTTP 1.0");
                        HTTPOnePointZero(clientInput, sw);
                    }
                    else if (clientInputArray[2] == "HTTP/1.1")
                    {
                        Console.WriteLine("ran HTTP 1.1");
                        HTTPOnePointOne(clientInput, sw);
                    }
                    else
                    {
                        Console.WriteLine("ran whois");
                        WhoIs(clientInput, sw);
                    }
                }
                else if (clientInputArray[0] == "PUT")
                {
                    if (clientInputArray.Length == 1)
                    {
                        Console.WriteLine("ran whois");
                        WhoIs(clientInput, sw);
                    }
                    else if (clientInputArray.Length == 2 && !clientInputArray[1].StartsWith("/"))
                    {
                        Console.WriteLine("ran whois");
                        WhoIs(clientInput, sw);
                    }
                    else if (clientInputArray[1].StartsWith("/"))
                    {
                        Console.WriteLine("ran HTTP 0.9");
                        HTTPZeroPointNine(clientInput, sw);                        
                    }
                    else
                    {
                        Console.WriteLine("ran whois");
                        WhoIs(clientInput, sw);
                    }
                }
                else
                {
                        Console.WriteLine("ran whois");
                        WhoIs(clientInput, sw);                        
                    
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                socketStream.Close();
                connection.Close();
            }
            
        }

        static string ReadInput(StreamReader sr)
        {
            string clientInput = "";
            int lineCount = 0;
            string temp = null;
            while (true)
            {
                lineCount++;
                try
                {
                   if (!sr.EndOfStream)
                   {
                        if (lineCount <= 3)
                        {
                            temp = sr.ReadLine();
                            clientInput += temp;
                            if (lineCount == 3)
                            {

                            }
                            else
                           {
                                clientInput += " ";
                            }
                       }
                        
                        else
                        {
                            char h = (char)sr.Read();
                            if (h == '\r')
                            {
                                clientInput += ' ';
                            }
                            else if (h == '\n')
                            {

                            }
                            else
                            {
                                clientInput += h;
                            }
                        }
                    }
                }
                catch
                {
                    break;
                }
            }
            return clientInput;
        }
        static void WhoIs(string clientInput, StreamWriter sw)
        {
            String[] clientInputArray = clientInput.Split(new char[] { ' ' }, 2);
            if (clientInputArray.Length == 2)
            {
                if (userDictionary.ContainsKey(clientInputArray[0]))
                {
                    userDictionary[clientInputArray[0]] = clientInputArray[1];
                    sw.Write("OK\r\n");
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("updated location " + clientInputArray[0] + " whoIs");
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }
                }
                else
                {
                    userDictionary.Add(clientInputArray[0], clientInputArray[1]);
                    sw.Write("OK\r\n");
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("added " + clientInputArray[0] + " to dictionary");
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }
                }
            }
            else
            {
                if (userDictionary.ContainsKey(clientInputArray[0]))
                {
                    sw.WriteLine(userDictionary[clientInputArray[0]]);
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("sent location");
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }
                }
                else
                {
                    sw.WriteLine("ERROR: no entries found");
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("no entries");
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }
                }
            }
        }
        static void HTTPZeroPointNine(string clientInput, StreamWriter sw)
        {
            String[] clientInputArray = clientInput.Split(new char[] { ' ' }, 4);
            clientInputArray[1] = clientInputArray[1].TrimStart('/');

            if (clientInputArray[0] == "PUT")
            {
                clientInputArray[3] = clientInputArray[3].TrimEnd(' ');
                if (userDictionary.ContainsKey(clientInputArray[1]))
                {
                    userDictionary[clientInputArray[1]] = clientInputArray[3];
                    sw.WriteLine("HTTP/0.9 200 OK");
                    sw.WriteLine("Content-Type: text/plain");
                    sw.WriteLine();
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("updated " + clientInputArray[1] + " 0.9");
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }
                }
                else
                {
                    userDictionary.Add(clientInputArray[1], clientInputArray[3]);
                    sw.WriteLine("HTTP/0.9 200 OK");
                    sw.WriteLine("Content-Type: text/plain");
                    sw.WriteLine();
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("updated " + clientInputArray[1] + " 0.9");
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }
                }
            }
            if (clientInputArray[0] == "GET")
            {
                if (userDictionary.ContainsKey(clientInputArray[1]))
                {
                    sw.WriteLine("HTTP/0.9 200 OK");
                    sw.WriteLine("Content-Type: text/plain");
                    sw.WriteLine();
                    sw.Write(userDictionary[clientInputArray[1]]);
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("sent location 0.9");
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }
                }
                else
                {
                    sw.Write("HTTP/0.9 404 Not Found\r\nContent-Type: text/plain\r\n\r\n");
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("no entries 0.9");
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }
                }
            }
        }
        static void HTTPOnePointZero(string clientInput, StreamWriter sw)
        {
            String[] clientInputArray = clientInput.Split(new char[] { ' ' }, 6);
            clientInputArray[1] = clientInputArray[1].TrimStart('/');
            clientInputArray[1] = clientInputArray[1].TrimStart('?');
            if (clientInputArray[0] == "POST")
            {
                clientInputArray[5] = clientInputArray[5].TrimEnd(' ');
                if (userDictionary.ContainsKey(clientInputArray[1]))
                {
                    userDictionary[clientInputArray[1]] = clientInputArray[5];
                    sw.WriteLine("HTTP/1.0 200 OK");
                    sw.WriteLine("Content-Type: text/plain");
                    sw.WriteLine();
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("updated " + clientInputArray[1] + " 1.0: " + clientInputArray[5]);
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }

                }
                else
                {
                    userDictionary.Add(clientInputArray[1], clientInputArray[5]);
                    sw.WriteLine("HTTP/1.0 200 OK");
                    sw.WriteLine("Content-Type: text/plain");
                    sw.WriteLine();
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("added " + clientInputArray[1] + " 1.0 at " + clientInputArray[5]);
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }
                }
            }
            if (clientInputArray[0] == "GET")
            {
                if (userDictionary.ContainsKey(clientInputArray[1]))
                {
                    sw.Write("HTTP/1.0 200 OK\r\nContent-Type: text/plain\r\n\r\n");
                    string test = userDictionary[clientInputArray[1]];
                    sw.Write(test + "\r\n");
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("sent location 1.0 to " + userDictionary[clientInputArray[1]]);
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }
                }
                else
                {
                    sw.Write("HTTP/1.0 404 Not Found\r\nContent-Type: text/plain\r\n\r\n");
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("no entries 1.0");
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }
                }
            }
        }
        static void HTTPOnePointOne(string clientInput, StreamWriter sw)
        {
            String[] clientInputArray = clientInput.Split(new char[] { ' ' }, 8);
            if (clientInputArray[0] == "POST")
            {
                string[] inputSplit = clientInputArray[7].Split('&');
                string[] nameSplit = inputSplit[0].Split('=');
                string[] locationSplit = inputSplit[1].Split('=');
                locationSplit[1] = locationSplit[1].TrimEnd(' ');
                if (userDictionary.ContainsKey(nameSplit[1]))
                {
                    userDictionary[nameSplit[1]]= locationSplit[1];
                    sw.WriteLine("HTTP/1.1 200 OK");
                    sw.WriteLine("Content-Type: text/plain");
                    sw.WriteLine();
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("updated " + clientInputArray[1] + " 1.1");
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }
                }
                else
                {
                    userDictionary.Add(nameSplit[1], locationSplit[1]);
                    sw.WriteLine("HTTP/1.1 200 OK");
                    sw.WriteLine("Content-Type: text/plain");
                    sw.WriteLine();
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("updated " + nameSplit[1] + " 1.1");
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }

                }
            }
            if (clientInputArray[0] == "GET")
            {
                string[] name = clientInputArray[1].Split('=');
                if (userDictionary.ContainsKey(name[1]))
                {
                    sw.Write("HTTP/1.1 200 OK \r\nContent-Type: text/plain\r\n\r\n");
                    string test = userDictionary[name[1]];
                    sw.Write(test + "\r\n");
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("sent location 1.1 " + userDictionary[name[1]]);
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }
                }
                else
                {
                    sw.Write("HTTP/1.1 404 Not Found\r\nContent-Type: text/plain\r\n\r\n");
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("no entries 1.1");
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }
                }
            }
        }

    }
}
