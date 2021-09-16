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

            //sbyte test = Convert.ToSByte(0x35);
            sbyte test = unchecked((sbyte)Reg.L);

            Console.WriteLine($"A {test}");



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
