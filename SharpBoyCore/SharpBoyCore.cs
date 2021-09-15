using System;
using SharpBoyCore.Processor;

namespace SharpBoyCore
{
    class SharpBoyCore
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SharpBoy!");
            
            Registers Reg = new Registers();
            Reg.F = 0xF0;
            Reg.A = 0xF0;
            Reg.B = 0xF0;
            Reg.C = 0xF0;
            Reg.D = 0xF0;
            Reg.E = 0xF0;
            Reg.H = 0xF0;
            Reg.L = 0xF0;
            
            PrintRegisters(Reg);

            //Console.WriteLine($"A {Reg.GetType().GetProperties()}");

            //System.Reflection.PropertyInfo[] myPropertyInfo;
            System.Reflection.FieldInfo[] myPropertyInfo;
            // Get the properties of 'Type' class object.
            myPropertyInfo = Reg.GetType().GetFields();
            Console.WriteLine("Properties of Reg are:");
            for (int i = 0; i < myPropertyInfo.Length; i++)
            {
                Console.WriteLine(myPropertyInfo[i].ToString());
            }
            Console.WriteLine($"A {Reg.GetType().GetField("A").GetValue(Reg)}");

        }
        
        static void PrintRegisters(Registers regs)
        {
            Console.WriteLine($"A {regs.F,0:X}");
            Console.WriteLine($"F {regs.F,0:X}");
            Console.WriteLine($"B {regs.F,0:X}");
            Console.WriteLine($"C {regs.F,0:X}");
            Console.WriteLine($"D {regs.F,0:X}");
            Console.WriteLine($"E {regs.F,0:X}");
            Console.WriteLine($"H {regs.F,0:X}");
            Console.WriteLine($"L {regs.F,0:X}");
        }
    }
}
