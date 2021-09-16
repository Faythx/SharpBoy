using System;
using SharpBoyCore.Util;
using SharpBoyCore.Memory;

namespace SharpBoyCore.Processor
{
    public class OpcodeHandler
    {
        private const byte r8Dest = 0x38;
        private const byte r8Src = 0x07;
        private const byte r16 = 0x30;
        private const byte condition = 0x18;

        private Registers regs;
        private MemoryManager memM;
        //Interrupt, GPU, etc.

        public OpcodeHandler(Registers regs, MemoryManager memM)
        {
            this.regs = regs;
            this.memM = memM;
            //....
        }

        public void Decode()
        {
            byte opc = memM.Read8(regs.PC++);
            //Bits 7 and 6 are always used as command.
            //First check for opcodes that can't change
            if (opc == 0x00)
            {
                //NOP
            }
            else if (opc == 0x08)
            {
                //LD (u16), SP
            }
            else if (opc == 0x10)
            {
                //STOP
            }
            else if (opc == 0x18)
            {
                //JR (unconditional)
                regs.SetPC((ushort)(regs.PC + Utils.byteToSbyte(memM.Read8(regs.PC++))));
            }
            else if (opc == 0x76)
            {
                //HALT
            }
            else if (opc == 0xE0)
            {
                //LD (FF00 + u8),A
            }
            else if (opc == 0xE8)
            {
                //ADD SP,i8
            }
            else if (opc == 0xF0)
            {
                //LD A,(FF00 + u8)
            }
            else if (opc == 0xF8)
            {
                //LD HL,SP + i8
            }
            else if (opc == 0xE2)
            {
                //LD (FF00+C), A
            }
            else if (opc == 0xEA)
            {
                //LD (u16),A
            }
            else if (opc == 0xF2)
            {
                //LD A,(0xFF00+C)
            }
            else if (opc == 0xFA)
            {
                //LD A,(u16)
            }
            else if (opc == 0xCD)
            {
                //CALL u16
            }
            else
            {
                switch (opc & 0xC0)
                {
                    case 0x00:
                        if ((opc & 0x07) == 0)
                        {
                            //JR (Conditional)
                        }
                        else if ((opc & 0x04) == 1)
                        {
                            switch (opc & 0x03)
                            {
                                case 0x00:
                                    // INC r8
                                    break;
                                case 0x01:
                                    // DEC r8
                                    break;
                                case 0x02:
                                    // LD r8, u8
                                    regs.SetR8(R8GetDestination(opc), memM.Read8(regs.PC++));
                                    break;
                                case 0x03:
                                    // opcode(group 1)
                                    break;
                            }
                        }
                        else
                        {
                            switch (opc & 0x0F)
                            {
                                case 0x01:
                                    //LD r16, u16
                                    break;
                                case 0x09:
                                    //ADD HL,r16
                                    break;
                                case 0x02:
                                    //LD (r16),A
                                    break;
                                case 0x0A:
                                    //LD A,(r16)
                                    break;
                                case 0x03:
                                    {
                                        //INC r16 (group 1)
                                        //2 MCycles (Should do 2 SetR8 instead?)
                                        byte i = R16Get(opc);
                                        byte group = 1;
                                        regs.SetR16(i, (ushort)(regs.GetR16(i, group) + 1), group);
                                        break;
                                    }
                                case 0x0B:
                                    {
                                        //DEC r16 (group 1)
                                        //2 MCycles (Should do 2 SetR8 instead?)
                                        byte i = R16Get(opc);
                                        byte group = 1;
                                        regs.SetR16(i, (ushort)(regs.GetR16(i, group) - 1), group);
                                        break;
                                    }
                            }
                        }
                        break;
                    case 0x40:
                        //LD R8, R8 or HALT( (HL), (HL))
                        regs.SetR8(R8GetDestination(opc), regs.GetR8(R8GetSource(opc)));
                        break;
                    case 0x80:
                        /*ALU A,r8
                         * Bit 3, 4 and 5 are the opcode
                         * 0 : ADD
                         * 1 : ADC
                         * 2 : SUB
                         * 3 : SBC
                         * 4 : AND
                         * 5 : XOR
                         * 6 : OR
                         * 7 : CP
                         */
                        switch (R8GetDestination(opc))
                        {
                            case 0x00:
                                
                                break;
                            case 0x01:

                                break;
                            case 0x02:

                                break;
                            case 0x03:

                                break;
                            case 0x04:
                                //AND - Z010
                                regs.A = (byte)(regs.A & regs.GetR8(R8GetSource(opc)));
                                regs.flagZ = (regs.A == 0);
                                regs.flagN = false;
                                regs.flagH = true;
                                regs.flagC = false;
                                break;
                            case 0x05:
                                //XOR - Z000
                                regs.A = (byte)(regs.A ^ regs.GetR8(R8GetSource(opc)));
                                regs.flagZ = (regs.A == 0);
                                regs.flagN = false;
                                regs.flagH = false;
                                regs.flagC = false;
                                break;
                            case 0x06:
                                //OR - Z000
                                regs.A = (byte)(regs.A | regs.GetR8(R8GetSource(opc)));
                                regs.flagZ = (regs.A == 0);
                                regs.flagN = false;
                                regs.flagH = false;
                                regs.flagC = false;
                                break;
                            case 0x07:

                                break;
                        }
                        break;
                    case 0xC0:
                        {
                            //Bit 3 and 4 are condition
                            if ((opc & 0x23) == 0)
                            {
                                //RET Condition
                            }
                            else if ((opc & 0x23) == 2)
                            {
                                //JP Condition
                            }
                            else if ((opc & 0x23) == 4)
                            {
                                //CALL Condition
                            }
                            else
                            {
                                //Bit 0, 1 and 2 are never args
                                switch (opc & 0x03)
                                {
                                    case 0x01:
                                        //Bit 4 and 5 are args
                                        if ((opc & 0x04) == 0)
                                        {
                                            //POP r16 (r16 group 3)
                                        }
                                        else
                                        {
                                            /* Args:
                                             * 0 : RET
                                             * 1 : RETI
                                             * 2 : JP HL
                                             * 3 : LD SP,HL
                                            */
                                        }
                                        break;
                                    case 0x03:
                                        /* Bits 3, 4 and 5 are args
                                         * 0 : JP u16
                                         * 1 : (CB Prefix)
                                         * 6 : DI
                                         * 7 : EI
                                         * All other opcodes are illegal
                                         */
                                        break;
                                    case 0x05:
                                        //PUSH r16
                                        break;
                                    case 0x06:
                                        //ALU a,u8
                                        break;
                                    case 0x07:
                                        //RST (CALL TO 00EXP00)
                                        break;
                                }
                            }
                            break;
                        }
                }
            }
        }


        private void CBPrefix()
        {
            byte opc = memM.Read8(regs.PC++);

            switch ((opc & 0xC0) >> 6)
            {
                case 0x00:
                    /*Shift/rotates
                     * 0 : RLC
                     * 1 : RRC
                     * 2 : RL
                     * 3 : RR
                     * 4 : SLA
                     * 5 : SRA
                     * 6 : SWAP
                     * 7 : SRL
                     */
                    break;
                case 0x01:
                    /* BIT bit,r8
                     * This instruction always reset n and sets h.
                    */
                    regs.flagZ = Utils.GetBit(R8GetDestination(opc), regs.GetR8(R8GetSource(opc)));
                    break;
                case 0x02:
                    //RES bit,r8
                    regs.SetR8(R8GetSource(opc), Utils.ResetBit(R8GetDestination(opc), regs.GetR8(R8GetSource(opc))));
                    break;
                case 0x03:
                    //SET bit,r8
                    regs.SetR8(R8GetSource(opc), Utils.SetBit(R8GetDestination(opc), regs.GetR8(R8GetSource(opc))));
                    break;
            }
        }

        //Hope the compiler inline these
        private byte R8GetSource(byte n)
        {
            return (byte)(n & r8Src);
        }

        private byte R8GetDestination(byte n)
        {
            return (byte)((n & r8Dest) >> 3);
        }

        private byte R16Get(byte n)
        {
            return (byte)((n & r16) >> 4);
        }

        private bool CheckCondition(byte n)
        {
            switch ((n & condition) >> 3)
            {
                case 0x00:
                    //NZ
                    return regs.flagZ != false;
                case 0x01:
                    //Z
                    return regs.flagZ == false;
                case 0x02:
                    //NC
                    return regs.flagZ != false;
                case 0x03:
                    //C
                    return regs.flagZ == false;
                default:
                    InvalidOpcode(n);
                    return false;
            }
        }

        private void InvalidOpcode(byte n)
        {
            throw new ArgumentException($"Invalid OpCode! Opc: {n,0:X}");
        }
    }
}
