using System;

namespace SharpBoyCore.Memory
{
	public class MemoryManager
	{
		public MemoryMap internalRAM;   //Banks are for GBC 
		public MemoryMap ioReg;
		public MemoryMap highRAM;



		public MemoryManager()
		{
			internalRAM = new MemoryMap(0xC000, 0xCFFF);
			ioReg = new MemoryMap(0xFF00, 0xFF7F);
			highRAM = new MemoryMap(0xFF80, 0xFFFE);


		}
	}
}