using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ReaderTesting
{
    class Program
    {
        COE424Entities3 entities = new COE424Entities3();
        
        static StreamWriter sw;
        static StreamReader sr;
        
        static void Main(string[] args)
        {
            
            

            string hostName = "172.16.10.99";
            int hostPort = 50007;

            IPAddress host = IPAddress.Parse(hostName);
            IPEndPoint hostep = new IPEndPoint(host, hostPort);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            while (!socket.Connected)
            {
                socket.Connect(hostep);

            }

            while (true)
            {
                try
                {
         
                        
                        NetworkStream nw = new NetworkStream(socket);
                        sw = new StreamWriter(nw);
                        sr = new StreamReader(nw);
                        Tag[] tags = readTags();
                    

                }
                catch (Exception)
                {

                    Console.WriteLine("something went wrong");
                }
            }
            
            
            

            //sock.Close();
        }
        

        static Tag[] readTags()
        {
            
            string command = "tag.db.scan_tags(100)";
            writeCommand(command);
            
            Tag[] tags = new Tag[15];

            if (sr.ReadLine().Equals("ok"))
            {
                char[] delimiterChars = { '=', ',' };
                int i = 0;
                string[] words;
                while ((words = sr.ReadLine().Split(delimiterChars)).Length != 1)
                {
                    //words = sr.ReadLine().Split(delimiterChars);

                    tags[i] = new Tag();
                    tags[i].id = words[1];
                    tags[i].readTime = DateTime.Parse(words[5]).TimeOfDay;
                    tags[i].antenna = Int32.Parse(words[9]);
                    var x = tags[i].id;
                    int antenna = tags[i].antenna;

                    using (COE424Entities3 entities = new COE424Entities3())
                    {
                        // Get the required bus from the Schedule Table
                        int busID = (from e2 in entities.Buses where e2.TagId.Equals(x) select e2).First().Id;
                        int lineID = (from e2 in entities.Buses where e2.Id.Equals(busID) select e2).First().LineId;

                        Schedule bus = (from e1 in entities.Schedules
                                        where e1.BusId.Equals(busID)
                                        select e1).FirstOrDefault();
                        bus.CheckIn = tags[i].readTime;
                        
                        bus.CurrentStation = (from e1 in entities.Stations
                                              where e1.ReaderId.Equals(antenna)
                                              select e1).First().Name;
                        string current = (from e1 in entities.Stations
                                          where e1.ReaderId.Equals(antenna)
                                          select e1).First().Name;

                        var listt = (from e1 in entities.Paths
                                     where e1.LineId.Equals(lineID)
                                     where e1.CurrentStation.Equals(current)
                                     select e1).ToList();

                        bus.NextStation = (from e1 in entities.Paths
                                           where (e1.LineId.Equals(lineID) && e1.CurrentStation.Equals(current))
                                           select e1).FirstOrDefault().NextStation;
                        
                        bus.ETA = (from e1 in entities.Paths
                                   where e1.LineId.Equals(lineID)
                                   select e1).First().ETA;

                        entities.SaveChanges();
                    }

                    i++;
                }

                return tags;
            }

            else
            {
                return null;
            }

        }



        static void writeCommand(String command)
        {
            sw.WriteLine(command);
            sw.Flush();
        }
    }
}
