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

		public void SetPC(ushort value)
		{
			//SetPC is a method because it does add +4 MCycle
			this.PC = value;
		}
		
		//Ease of Access
		public byte GetR8(byte n)
		{
			switch (n)
			{
				case 0:
					return this.B;
				case 1:
					return this.C;
				case 2:
					return this.D;
				case 3:
					return this.E;
				case 4:
					return this.H;
				case 5:
					return this.L;
				case 6:
					//Since (HL) is a pointer it needs to read the memory at that point
					return 0x0A; //TODO
				case 7:
					return this.A;
				default:
					return 0xFF;    //Throw exception
			}			
		}

		public void SetR8(byte n, byte value)
		{
			switch (n)
			{
				case 0:
					this.B = value;
					break;
				case 1:
					this.C = value;
					break;
				case 2:
					this.D = value;
					break;
				case 3:
					this.E = value;
					break;
				case 4:
					this.H = value;
					break;
				case 5:
					this.L = value;
					break;
				case 6:
					//Since (HL) is a pointer it needs to write the memory at that point
					break;
				case 7:
					this.A = value;
					break;
			}
		}

		public ushort GetR16(byte n, byte g)
		{
			switch (n)
			{
				case 0x00:
					return this.BC;
				case 0x01:
					return this.DE;
				case 0x02:
					//Group 2 returns HL+, otherwise HL
					if (g == 2)
						return this.HL++;
					else
						return this.HL;
				case 0x03:
					switch (g)
					{
						case 1:
							return this.SP;
						case 2:
							//HL -
							return this.HL--;
						case 3:
							return this.AF;
						default:
							//InvalidOpcode(n);
							return 0xFFFF;
					}
				default:
					//InvalidOpcode(n);
					return 0xFFFF;
			}
		}

		public void SetR16(byte n, ushort value, byte g)
		{
			//There's no set for group 2, group 3 is for PUSH/POP
			switch (n)
			{
				case 0x00:
					this.BC = value;
					break;
				case 0x01:
					this.DE = value;
					break;
				case 0x02:
					//Group 2 returns HL+, otherwise HL
					this.HL = value;
					break;
				case 0x03:
					if (g == 1)
						this.SP = value;
					else
						this.AF = value;
					break;
			}
		}
	}
}