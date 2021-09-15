using System;

namespace SharpBoyCore.Memory
{
	public class MemoryMap
	{
		private byte[] data;
		private ushort start;
		private ushort finish;
		public MemoryMap(ushort start, ushort finish)
		{
			this.start = start;
			this.finish = finish;
			data = new byte[finish - start + 1];
		}

		public byte this[ushort address]
		{
			get => data[address - start];
			set
			{
				data[address - start] = value;
			}
		}
	}
}