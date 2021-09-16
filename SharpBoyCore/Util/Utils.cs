using System;

namespace SharpBoyCore.Util
{
	public class Utils
	{
		private const ushort highByte = 0xFF00;
		private const ushort lowByte = 0x00FF;

		public static ushort byteToShort(byte hi, byte lo)
		{
			return (ushort)((hi << 8) | lo); ;
		}

		public static byte Msb(ushort value)
		{
			return (byte)((value & highByte) >> 8);
		}

		public static byte Lsb(ushort value)
		{
			return (byte)(value & lowByte);
		}

		public static ushort SetBit(byte n, ushort r)
		{
			return (ushort)(r | (1 << n));
		}

		public static byte SetBit(byte n, byte r)
		{
			return (byte)(r | (1 << n));
		}

		public static ushort ResetBit(byte n, ushort r)
		{
			return (ushort)(r & ~(1 << n));
		}

		public static byte ResetBit(byte n, byte r)
		{
			return (byte)(r & ~(1 << n));
		}

		public static bool GetBit(byte n, byte r)
		{
			return ((r & (1 << n)) != 0);
		}

		public static sbyte byteToSbyte(byte n)
		{
			return unchecked((sbyte)n);
		}
	}
}