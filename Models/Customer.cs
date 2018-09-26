using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormBot.Models
{
    [Serializable]
    //public class Customer
    //{
    //    public Customer(string name)
    //    {
    //        Name = name;
    //        AccountNumber = "123";
    //    }

    //    public string Name { get; set; }
    //    public string AccountNumber { get; set; }
    //}

    public class Customer
    {
        public string AccountNumber { get; set; }
        public string Name { get; set; }

        public Customer(string name)
        {
            Name = name;
            AccountNumber = "123";
        }

        public override bool Equals(object obj)
        {
            var c = obj as Customer;

            if (c == null)
            {
                return false;
            }

            return this.AccountNumber.Equals(c.AccountNumber) && this.Name.Equals(c.Name);
        }
        public override int GetHashCode()
        {
            return AccountNumber.GetHashCode() ^ Name.GetHashCode();
        }
        public override string ToString()
        {
            return $"{AccountNumber} {Name}";
        }
    }

}