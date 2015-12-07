using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCommand
{
    public class Device
    {
        public string name;
        public string address;

        /// <summary>
        /// Device object to creaete a representation of a device on the network
        /// </summary>
        /// <param name="n"></param>
        /// <param name="a"></param>
        public Device(string n, string a)
        {
            name = n;
            address = a;
        }
    }
}
