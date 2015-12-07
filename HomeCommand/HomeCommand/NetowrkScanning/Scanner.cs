using System.Net.NetworkInformation;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using System.Net;
using System.Collections;
using HomeCommand;

public class Scanner
{

    private int startIP = 0;
    private int endIP = 0;
    private string ipPrefix = "";


    public Scanner(string ipPrefix, int startIP, int endIP)
    {
        this.startIP = startIP;
        this.endIP = endIP;
        this.ipPrefix = ipPrefix;

    }

    public Boolean IsConnected(string address)
    {
        Ping pingSender = new Ping();

        IPAddress addr = IPAddress.Parse(address);
        PingReply reply = pingSender.Send(addr);
        if (reply.Status == IPStatus.Success)
        {
            return true;
        }
        else
        {
            return false;
        }

    }


    public List<Device> ScanNetwork()
    {
        List<Device> devices = new List<Device>();
        // Ping's the local machine.
        Ping pingSender = new Ping();
        for (int i = startIP; i <= endIP; i++)
        {
            String ip = ipPrefix + "." + i;

            IPAddress address = IPAddress.Parse(ip);
            PingReply reply = pingSender.Send(address);

            if (reply.Status == IPStatus.Success)
            {
                IPHostEntry myScanHost = null;
                string[] arr = new string[2];
                try
                {
                    myScanHost = Dns.GetHostEntry(address);
                }
                catch
                {
                    //Console.WriteLine(ip + " is reachable but failed to read hostname");
                }
                if (myScanHost != null)
                {

                    Device d = new Device(myScanHost.HostName.ToString(), address.ToString());
                    devices.Add(d);
                    // Console.Write(myScanHost.HostName.ToString() + "\t");
                    //Console.WriteLine(address.ToString());
                }
            }
            else
            {
                //Console.WriteLine(ip + " " + reply.Status.ToString());
            }
        }
        return devices;
    }
}