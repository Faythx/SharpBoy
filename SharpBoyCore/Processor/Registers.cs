using System;
using SharpBoyCore.Util;

namespace SharpBoyCore.Processor
{
	public class Registers
	{
		//Flags position
		private const byte positionZeroFlag = 7; //z
		private const byte positionSubtractionFlag = 6; //n (BCD)
		private const byte positionHalfCarryFlag = 5; //h (BCD)
		private const byte positionCarryFlag = 4; //c 

		//Other registers
		public ushort PC;
		public ushort SP;

		//8 bits registers
		public byte A;
		public byte B;
		public byte C;
		public byte D;
		public byte E;
		public byte F;
		public byte H;
		public byte L;

		//16 bits
		public ushort AF
		{
			get => Utils.byteToShort(A, F);
			set
			{
				A = Utils.Msb(value);
				F = Utils.Lsb(value);
			}
		}

		public ushort BC
		{
			get => Utils.byteToShort(B, C);
			set
			{
				B = Utils.Msb(value);
				C = Utils.Lsb(value);
			}
		}
		public ushort DE
		{
			get => Utils.byteToShort(D, E);
			set
			{
				D = Utils.Msb(value);
				E = Utils.Lsb(value);
			}
		}
		public ushort HL
		{
			get => Utils.byteToShort(H, L);
			set
			{
				H = Utils.Msb(value);
				L = Utils.Lsb(value);
			}
		}

		//Flags
		public bool flagZ
		{
			get => Utils.GetBit(positionZeroFlag, F);
			set
			{
				F = value ? Utils.SetBit(positionZeroFlag, F) : Utils.ResetBit(positionZeroFlag, F);
			}

		}

		public bool flagN
		{
			get => Utils.GetBit(positionSubtractionFlag, F);
			set
			{
				F = value ? Utils.SetBit(positionSubtractionFlag, F) : Utils.ResetBit(positionSubtractionFlag, F);
			}

		}

		public bool flagH
		{
			get => Utils.GetBit(positionHalfCarryFlag, F);
			set
			{
				F = value ? Utils.SetBit(positionHalfCarryFlag, F) : Utils.ResetBit(positionHalfCarryFlag, F);
			}

		}

		public bool flagC
		{
			get => Utils.GetBit(positionCarryFlag, F);
			set
			{
				F = value ? Utils.SetBit(positionCarryFlag, F) : Utils.ResetBit(positionCarryFlag, F);
			}

		}
		
	}
}