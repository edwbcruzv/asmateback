using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRsa
    {
        public byte[] Encript(string data);
        public string Dencript(byte[] data);

    }
}
