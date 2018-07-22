using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordStore.Model
{
    public class Credential
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public byte[] PasswordEncrypted { get; set; }

        public override string ToString() => $"{Name} - {Login}";
    }
}
