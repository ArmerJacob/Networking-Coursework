using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace locationserver
{
    class LocationServer
    {
        static void Main(string[] args)
        {
            locationserver.locationServerUI ui = new locationserver.locationServerUI();
            int port = 43;
            int timeout = 1000;
            bool runInterface = false;
            //Sets the defaults incase they arnt specified in the arguments.
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-t":
                        if (args[i + 1] != null && args[i + 1] != "")
                        {
                            timeout = int.Parse(args[i + 1]);
                        }
                        args[i] = null;
                        args[i + 1] = null;
                        break;

                    case "-p":
                        if (args[i + 1] != null && args[i + 1] != "")
                        {
                            port = int.Parse(args[i + 1]);
                        }
                        args[i] = null;
                        args[i + 1] = null;
                        break;
                    case "-w":
                        runInterface = true;
                        break;
                }
            }
            //checks the arguments for the port, timeout and if they want to run the server UI
            if(runInterface == true)
            {
                ui.SetValues(port, timeout);
                Application.Run(ui);
                //Runs the UI if specified
            }
            else
            {
                runServer(port, timeout);
            }


        }
        //this checks if there are arguments and runs the UI if there is, otherwise it runs normally.
        public static void runServer(int port, int timeout)
        {
            TcpListener listener;
            Socket connection;
            Handler handler;
            try
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                Console.WriteLine("Server started Listening");
                while (true)
                {
                    connection = listener.AcceptSocket();
                    handler = new Handler();
                    Thread thread = new Thread(() => handler.doRequest(connection, timeout));
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
    //This method runs the server, it starts a thread and does the request.
    class Handler
    {
        static Dictionary<string, string> userDictionary = new Dictionary<string, string>();
        NetworkStream socketStream;
        public void doRequest(Socket connection, int timeout)
        {
            socketStream = new NetworkStream(connection);
            Console.WriteLine("Connection Received");
            try
            {
               socketStream.ReadTimeout = timeout;
                socketStream.WriteTimeout = timeout;

                StreamWriter sw = new StreamWriter(socketStream);
                StreamReader sr = new StreamReader(socketStream);
                
                string clientInput = ReadInput(sr);
                Console.WriteLine("Input read input is: ");
                Console.WriteLine(clientInput);
                clientInput = clientInput.TrimEnd(' ');
                string[] clientInputArray = clientInput.Split(' ');
                //This creates the connection to the client and reads the input.
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
                //This checks what protocol the client has requested.
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
            //This closes the connection.
            
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
            //This reads in the input from the Client.
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
                    //this is the response saying that they updated the location of the requested user.
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("updated location " + clientInputArray[0] + " whoIs");
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }
                    //this is updating the location of an existing user.
                }
                else
                {
                    userDictionary.Add(clientInputArray[0], clientInputArray[1]);
                    sw.Write("OK\r\n");
                    //this is the response saying they they added the location of the requested user.
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("added " + clientInputArray[0] + " to dictionary");
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }
                    //this is adding a new user and adding the location of the user to the dictionary.
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
                    //this is sending the location of a user requested by the client.
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
                    //this is sent when the server cannot find any existing users in the system.
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
                    //this is a response to the client saying they updated the location of the requested user.
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("updated " + clientInputArray[1] + " 0.9");
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }
                    //this is updating an existing user's location in the dictionary.
                }
                else
                {
                    userDictionary.Add(clientInputArray[1], clientInputArray[3]);
                    sw.WriteLine("HTTP/0.9 200 OK");
                    sw.WriteLine("Content-Type: text/plain");
                    sw.WriteLine();
                    //this is a response to the user that they updated the location of the requested user.
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("updated " + clientInputArray[1] + " 0.9");
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }
                    //This is adding a new user and their location to the dictionary.
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
                    //this is sending the location of a user requested by the client.
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
                    //This is sent to the client when the server cannot find the user sent by the client.
                }
            }
        }
        static void HTTPOnePointZero(string clientInput, StreamWriter sw)
        {
            String[] clientInputArray = clientInput.Split(new char[] { ' ' }, 6);
            clientInputArray[1] = clientInputArray[1].TrimStart('/');
            clientInputArray[1] = clientInputArray[1].TrimStart('?');
            //this is putting the input from the client into a string.
            if (clientInputArray[0] == "POST")
            {
                clientInputArray[5] = clientInputArray[5].TrimEnd(' ');
                if (userDictionary.ContainsKey(clientInputArray[1]))
                {
                    userDictionary[clientInputArray[1]] = clientInputArray[5];
                    sw.WriteLine("HTTP/1.0 200 OK");
                    sw.WriteLine("Content-Type: text/plain");
                    sw.WriteLine();
                    //this is sending off a message to the client, saying they updated the location.
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("updated " + clientInputArray[1] + " 1.0: " + clientInputArray[5]);
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }
                    //this is updating an existing user's location to the dictionary.
                }
                else
                {
                    userDictionary.Add(clientInputArray[1], clientInputArray[5]);
                    sw.WriteLine("HTTP/1.0 200 OK");
                    sw.WriteLine("Content-Type: text/plain");
                    sw.WriteLine();
                    //this is sending off a message to the client, saying they updated the location.
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("added " + clientInputArray[1] + " 1.0 at " + clientInputArray[5]);
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }
                    //This is adding a new user to the dictionary and updating their location.
                }
            }
            if (clientInputArray[0] == "GET")
            {
                if (userDictionary.ContainsKey(clientInputArray[1]))
                {
                    sw.Write("HTTP/1.0 200 OK\r\nContent-Type: text/plain\r\n\r\n");
                    string location = userDictionary[clientInputArray[1]];
                    sw.Write(location + "\r\n");
                    //this is a response to the client that they found the location of the requested user.
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("sent location 1.0 to " + userDictionary[clientInputArray[1]]);
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }
                    //this is sending the location of a user requested by the client.
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
                    //This is sent when the server cannot find an entry requested by the client.
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
                //this is saving and spliting the client input for use.
                if (userDictionary.ContainsKey(nameSplit[1]))
                {
                    userDictionary[nameSplit[1]]= locationSplit[1];
                    sw.WriteLine("HTTP/1.1 200 OK");
                    sw.WriteLine("Content-Type: text/plain");
                    sw.WriteLine();
                    //this is sending a response to the client saying they updated the user.
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("updated " + clientInputArray[1] + " 1.1");
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }
                    //this is updating the location of an existing user's location.
                }
                else
                {
                    userDictionary.Add(nameSplit[1], locationSplit[1]);
                    sw.WriteLine("HTTP/1.1 200 OK");
                    sw.WriteLine("Content-Type: text/plain");
                    sw.WriteLine();
                    //this is sending off a message to the client, saying they updated the location.
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("updated " + nameSplit[1] + " 1.1");
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }
                    //this is adding a new user and their location.
                }
            }
            if (clientInputArray[0] == "GET")
            {
                string[] name = clientInputArray[1].Split('=');
                if (userDictionary.ContainsKey(name[1]))
                {
                    sw.Write("HTTP/1.1 200 OK \r\nContent-Type: text/plain\r\n\r\n");
                    string location = userDictionary[name[1]];
                    sw.Write(location + "\r\n");
                    //this is sending a response to the server saying they found the location of the requested user.
                    try
                    {
                        sw.Flush();
                        Console.WriteLine("sent location 1.1 " + userDictionary[name[1]]);
                    }
                    catch
                    {
                        Console.WriteLine("cannot write to datastream");
                    }
                    //this is the server finding the location of an existing user in the dictionary.
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
                    //this is the server responding that they couldn't find any entires of the requested user.
                }
            }
        }

    }
}
