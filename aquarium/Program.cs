using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aquarium
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class Fish
    {
        public int Age { get; private set; }
        public int MaxAge { get; private set; }

        public Fish(int age,int maxAge)
        {
            Age = age;
            MaxAge = maxAge;
        }
    }
}
