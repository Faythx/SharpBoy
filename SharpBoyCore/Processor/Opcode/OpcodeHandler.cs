using System;
using SharpBoyCore.Util;
using SharpBoyCore.Memory;

namespace SharpBoyCore.Processor
{
	public class OpcodeHandler
	{
		private Registers regs;
		private MemoryMap mMap;
		//Interrupt, GPU, etc.

		public OpcodeHandler(Registers regs, MemoryMap mMap)
		{
			this.regs = regs;
			this.mMap = mMap;
			//....
		}

		public void Decode(byte opc)
		{
			//Bits 7 and 6 are always used as command.
			switch (opc & 0xC0)
			{

			}			
		}
	}
}
