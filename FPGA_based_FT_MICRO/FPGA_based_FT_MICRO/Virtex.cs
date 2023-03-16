using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FPGA_based_FT_MICRO
{
    class Virtex
    {
        /// <summary>
        /// Find all instances in the XDL file and analyse it
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="XDLw"></param>
        /// 
        static public void func_inst(string argument,StreamWriter  XDLw)
        {
            int Index = 0;
            int Yindex = 0;
            int Spcindex = 0;
            int UndLinindex = 0;
            string inst_name = "";
            if (argument.IndexOf("inst") != -1)
            {
                inst_name = argument.Substring(argument.IndexOf("inst") + 6, argument.IndexOf("\"", argument.IndexOf("inst") + 6) - argument.IndexOf("inst") - 6);
            }

            //RAM
            if (argument.IndexOf(" BRAM") != -1)
            {
                XDLw.Write("BRAM\n");                
                Index = argument.IndexOf(" BRAM");
                Yindex = argument.IndexOf("Y", Index);
                Spcindex = argument.IndexOf(" ", Yindex);
                XDLw.Write(argument.Substring(Index + 7, Yindex - Index - 7));
                XDLw.Write("\n");
                XDLw.Write(argument.Substring(Yindex + 1, Spcindex - Yindex - 1));
                XDLw.Write("\n");
                XDLw.Write(" (" + inst_name + ") \n");
            }

            if (argument.IndexOf(" RAMB") != -1)
            {
                XDLw.Write("RAMB\n");
                Index = argument.IndexOf(" RAMB");                
                Yindex = argument.IndexOf("Y", Index);
                UndLinindex = argument.IndexOf("_", Index);
                Spcindex = argument.IndexOf(" ", Yindex);
                XDLw.Write(argument.Substring(Index + 5, UndLinindex - Index - 5));
                XDLw.Write(" \n");
                XDLw.Write(argument.Substring(Index + 9, Yindex - Index - 9));
                XDLw.Write(" \n");
                XDLw.Write(argument.Substring(Yindex + 1, Spcindex - Yindex - 1));
                XDLw.Write(" \n");
                XDLw.Write(" ("+inst_name + ") \n");
            }

            //CLB type
            if (argument.IndexOf("CLBLL") != -1) 
            { 
                XDLw.Write("CTLL \n");                
                Index = argument.IndexOf(" CLBLL");
                Yindex = argument.IndexOf("Y",Index);
                Spcindex = argument.IndexOf(" ", Yindex);
                XDLw.Write("ROW");
                XDLw.Write(argument.Substring(Index +8,Yindex -Index -8));
                XDLw.Write(" \n");
                XDLw.Write("COL");
                XDLw.Write(argument.Substring(Yindex +1, Spcindex -Yindex -1));
                XDLw.Write(" \n");
            }
            else if (argument.IndexOf("CLBLM") != -1)
            {
                XDLw.Write("CTLM \n");                
                Index = argument.IndexOf(" CLBLM");
                Yindex = argument.IndexOf("Y", Index);
                Spcindex = argument.IndexOf(" ", Yindex);
                XDLw.Write("ROW");
                XDLw.Write(argument.Substring(Index + 8, Yindex - Index - 8));
                XDLw.Write(" \n");
                XDLw.Write("COL");
                XDLw.Write(argument.Substring(Yindex + 1, Spcindex - Yindex - 1));
                XDLw.Write(" \n");
            }
            
            //SLICE type           
            if (argument.IndexOf("SLICEL") != -1 & argument.IndexOf("DUMMY") == -1)
            {
                XDLw.Write("STL \n");
                Index = argument.IndexOf(" SLICE_");
                Yindex = argument.IndexOf("Y", Index);
                Spcindex = argument.IndexOf(" ", Yindex);
                XDLw.Write("WOR");
                XDLw.Write(argument.Substring(Index + 8, Yindex - Index - 8));
                XDLw.Write(" \n");
                XDLw.Write(argument.Substring(Yindex + 1, Spcindex - Yindex - 1));
                XDLw.Write(" \n");

                //A Region
                if (argument.IndexOf("A5FFINIT::#OFF") == -1) XDLw.Write(" A5FF\n");
                if (argument.IndexOf("AFF::#OFF") == -1) XDLw.Write(" AFF\n");
                if (argument.IndexOf("A5LUT::#OFF") == -1) XDLw.Write(" A5LUT\n");
                if (argument.IndexOf("A6LUT::#OFF") == -1) XDLw.Write(" A6LUT\n");
                if (argument.IndexOf("ACY0::#OFF") == -1) XDLw.Write(" ACY0\n");
                if (argument.IndexOf("AOUTMUX::#OFF") == -1) XDLw.Write(" AOUTMUX\n");
                //B Region
                if (argument.IndexOf("B5FFINIT::#OFF") == -1) XDLw.Write(" B5FF\n");
                if (argument.IndexOf("BFF::#OFF") == -1) XDLw.Write(" BFF\n");
                if (argument.IndexOf("B5LUT::#OFF") == -1) XDLw.Write(" B5LUT\n");
                if (argument.IndexOf("B6LUT::#OFF") == -1) XDLw.Write(" B6LUT\n");
                if (argument.IndexOf("BCY0::#OFF") == -1) XDLw.Write(" BCY0\n");
                if (argument.IndexOf("BOUTMUX::#OFF") == -1) XDLw.Write(" BOUTMUX\n");
                //C Region
                if (argument.IndexOf("C5FFINIT::#OFF") == -1) XDLw.Write(" C5FF\n");
                if (argument.IndexOf("CFF::#OFF") == -1) XDLw.Write(" CFF\n");
                if (argument.IndexOf("C5LUT::#OFF") == -1) XDLw.Write(" C5LUT\n");
                if (argument.IndexOf("C6LUT::#OFF") == -1) XDLw.Write(" C6LUT\n");
                if (argument.IndexOf("CCY0::#OFF") == -1) XDLw.Write(" CCY0\n");
                if (argument.IndexOf("COUTMUX::#OFF") == -1) XDLw.Write(" COUTMUX\n");
                //D Region
                if (argument.IndexOf("D5FFINIT::#OFF") == -1) XDLw.Write(" D5FF\n");
                if (argument.IndexOf("DFF::#OFF") == -1) XDLw.Write(" DFF\n");
                if (argument.IndexOf("D5LUT::#OFF") == -1) XDLw.Write(" D5LUT\n");
                if (argument.IndexOf("D6LUT::#OFF") == -1) XDLw.Write(" D6LUT\n");
                if (argument.IndexOf("DCY0::#OFF") == -1) XDLw.Write(" DCY0\n");
                if (argument.IndexOf("DOUTMUX::#OFF") == -1) XDLw.Write(" DOUTMUX\n");
                //X inputs
                if (argument.IndexOf("F7BMUX") != -1) XDLw.Write("CX\n");
                if (argument.IndexOf("F7AMUX") != -1) XDLw.Write("AX\n");
                if (argument.IndexOf("F8MUX") != -1) XDLw.Write("BX\n");
                if (argument.IndexOf("AX") != -1) XDLw.Write(" AX\n");
                if (argument.IndexOf("BX") != -1) XDLw.Write(" BX\n");
                if (argument.IndexOf("CX") != -1) XDLw.Write(" CX\n");
                if (argument.IndexOf("DX") != -1) XDLw.Write(" DX\n");
                if (argument.IndexOf("CARRY4") != -1) XDLw.Write(" CRY\n");
                XDLw.Write(" (" + inst_name + ") \n");
            }
            else if (argument.IndexOf("SLICEM") != -1 & argument.IndexOf("DUMMY") == -1)
            {
                XDLw.Write("STM \n");
                Index = argument.IndexOf(" SLICE_");
                Yindex = argument.IndexOf("Y", Index);
                Spcindex = argument.IndexOf(" ", Yindex);
                XDLw.Write("WOR");
                XDLw.Write(argument.Substring(Index + 8, Yindex - Index - 8));
                XDLw.Write(" \n");
                XDLw.Write(argument.Substring(Yindex + 1, Spcindex - Yindex - 1));
                XDLw.Write(" \n");

                //A Region
                if (argument.IndexOf("A5FFINIT::#OFF") == -1) XDLw.Write(" A5FF\n");
                if (argument.IndexOf("AFF::#OFF") == -1) XDLw.Write(" AFF\n");
                if (argument.IndexOf("A5LUT::#OFF") == -1) XDLw.Write(" A5LUT\n");
                if (argument.IndexOf("A6LUT::#OFF") == -1) XDLw.Write(" A6LUT\n");
                if (argument.IndexOf("ACY0::#OFF") == -1) XDLw.Write(" ACY0\n");
                if (argument.IndexOf("AOUTMUX::#OFF") == -1) XDLw.Write(" AOUTMUX\n");
                //B Region
                if (argument.IndexOf("B5FFINIT::#OFF") == -1) XDLw.Write(" B5FF\n");
                if (argument.IndexOf("BFF::#OFF") == -1) XDLw.Write(" BFF\n");
                if (argument.IndexOf("B5LUT::#OFF") == -1) XDLw.Write(" B5LUT\n");
                if (argument.IndexOf("B6LUT::#OFF") == -1) XDLw.Write(" B6LUT\n");
                if (argument.IndexOf("BCY0::#OFF") == -1) XDLw.Write(" BCY0\n");
                if (argument.IndexOf("BOUTMUX::#OFF") == -1) XDLw.Write(" BOUTMUX\n");
                //C Region
                if (argument.IndexOf("C5FFINIT::#OFF") == -1) XDLw.Write(" C5FF\n");
                if (argument.IndexOf("CFF::#OFF") == -1) XDLw.Write(" CFF\n");
                if (argument.IndexOf("C5LUT::#OFF") == -1) XDLw.Write(" C5LUT\n");
                if (argument.IndexOf("C6LUT::#OFF") == -1) XDLw.Write(" C6LUT\n");
                if (argument.IndexOf("CCY0::#OFF") == -1) XDLw.Write(" CCY0\n");
                if (argument.IndexOf("COUTMUX::#OFF") == -1) XDLw.Write(" COUTMUX\n");
                //D Region
                if (argument.IndexOf("D5FFINIT::#OFF") == -1) XDLw.Write(" D5FF\n");
                if (argument.IndexOf("DFF::#OFF") == -1) XDLw.Write(" DFF\n");
                if (argument.IndexOf("D5LUT::#OFF") == -1) XDLw.Write(" D5LUT\n");
                if (argument.IndexOf("D6LUT::#OFF") == -1) XDLw.Write(" D6LUT\n");
                if (argument.IndexOf("DCY0::#OFF") == -1) XDLw.Write(" DCY0\n");
                if (argument.IndexOf("DOUTMUX::#OFF") == -1) XDLw.Write(" DOUTMUX\n");
                //X inputs
                if (argument.IndexOf("F7BMUX") != -1) XDLw.Write("CX\n");
                if (argument.IndexOf("F7BMUX") != -1) XDLw.Write("AX\n");
                if (argument.IndexOf("F8MUX") != -1) XDLw.Write("BX\n");
                if (argument.IndexOf("AX") != -1) XDLw.Write(" AX\n");
                if (argument.IndexOf("BX") != -1) XDLw.Write(" BX\n");
                if (argument.IndexOf("CX") != -1) XDLw.Write(" CX\n");
                if (argument.IndexOf("DX") != -1) XDLw.Write(" DX\n");
                if (argument.IndexOf("CARRY4") != -1) XDLw.Write(" CRY\n");
                XDLw.Write(" (" + inst_name + ") \n");
            }     

            if (argument.IndexOf("IOB") != -1)
            { 
                XDLw.Write("IOB\n");
                XDLw.Write(inst_name + "\n");
            }

            if (argument.IndexOf("OLOGIC") != -1)
            {
                XDLw.Write("OLOGIC\n");
                XDLw.Write(inst_name + "\n");
            }

            if (argument.IndexOf("ILOGICE") != -1)
            {
                XDLw.Write("ILOGICE\n");
                XDLw.Write(inst_name + "\n");
            }

            if (argument.IndexOf("CMT_BUFG") != -1)
            {
                XDLw.Write("BUFG\n");
                XDLw.Write(inst_name + "\n");
            }

            if (argument.IndexOf("CFG_CENTER") != -1)
            {
                XDLw.Write("STARTUP\n");
                XDLw.Write(inst_name + "\n");
            }

            if (argument.IndexOf("GTX_") != -1)
            {
                XDLw.Write("GTXE\n");
                XDLw.Write(inst_name + "\n");
            }

            if (argument.IndexOf("TIEOFF") != -1)
            {
                XDLw.Write("TIEOFF\n");
                XDLw.Write(inst_name + "\n");
            }

            if (argument.IndexOf("BUFHCE") != -1)
            {
                XDLw.Write("BUFHCE\n");
                XDLw.Write(inst_name + "\n");
            }

            XDLw.Write(";\n");
        }
        //End of func_inst
        /// <summary>
        /// Find all nets in the XDL and analyse it
        /// </summary>
        /// <param name="argument"></param>

        static public  void func_net_rout(string inst_name,string inst_name_H,int History_NUM,string outpin,string X,string CLB_Type,string CLB_ROW,string COL,string SLICE_Type,string LOGIC_OUTS,string BYP_B,StreamWriter XDLrout)
        {
            string His = "";
            His = "\"History_" + History_NUM.ToString() + "\"";
            XDLrout.Write("net ");
            XDLrout.Write(His);
            XDLrout.Write(" ,\n  outpin \"" + inst_name + "\" " + outpin + " ,\n  inpin \"" + inst_name_H + "\" " + X + ",\n  pip CLB");
            XDLrout.Write(CLB_Type + "_X" + CLB_ROW);
            XDLrout.Write("Y" + COL + " ");
            XDLrout.Write("CLB" + CLB_Type + "_" + SLICE_Type + "_" + outpin + " -> " + "CLB" + CLB_Type + "_" + LOGIC_OUTS + " ,\n");
            XDLrout.Write("  pip CLB" + CLB_Type + "_X" + CLB_ROW);
            XDLrout.Write("Y" + COL + " ");
            XDLrout.Write("CLB" + CLB_Type + "_" + BYP_B + " -> " + "CLB" + CLB_Type + "_" + SLICE_Type + "_" + X + " " + ",\n");
            XDLrout.Write("  pip INT" + "_X" + CLB_ROW);
            XDLrout.Write("Y" + COL + " ");
            XDLrout.Write(LOGIC_OUTS + " -> " + BYP_B + " ,\n");

            XDLrout.Write("  ;\n");
        }
        private static void Func_net_2(string inst_name,string inst_name_H,string Line, string z, string outpin, string X, string CLB_ROW, string COL, int Logic, StreamWriter XDLrout,string Dir)
        {
            string CLB_Type = ""; string CLB_Type_B = "";
            string SLICE_Type = ""; string SLICE_Type_B = "";
            string His = "";
            string BYP_B = ""; string BYP = "";
            string CLB_ROW_B = ""; string COL_B = "";  int WOR_z = 0;

            CLB_Type = Line.Substring(Line.IndexOf("CT") + 2, 2);
            SLICE_Type = Line.Substring(Line.IndexOf("ST") + 2, 1);           
            CLB_Type_B = z.Substring(z.IndexOf("CT") + 2, 2);
            SLICE_Type_B = z.Substring(z.IndexOf("ST") + 2, 1);
            CLB_ROW_B = z.Substring(z.IndexOf("ROW") + 3, z.IndexOf(" ", z.IndexOf("ROW")) - z.IndexOf("ROW") - 3);
            COL_B = z.Substring(z.IndexOf("COL") + 3, z.IndexOf(" ", z.IndexOf("COL")) - z.IndexOf("COL") - 3);


            WOR_z = Int32.Parse(z.Substring(z.IndexOf("WOR") + 3, z.IndexOf(" ", z.IndexOf("WOR")) - z.IndexOf("WOR") - 3));
          
             if (WOR_z % 2 == 0)
            {
                if (X == "AX") { BYP_B = "BYP_B0"; BYP = "BYP0"; }
                else if (X == "BX") { BYP_B = "BYP_B5"; BYP = "BYP5"; }
                else if (X == "CX") { BYP_B = "BYP_B2"; BYP = "BYP2"; }
                else if (X == "DX") { BYP_B = "BYP_B7"; BYP = "BYP7"; }
            }
            else
            {
                if (X == "AX") { BYP_B = "BYP_B1";BYP = "BYP1"; }
                else if (X == "BX") { BYP_B = "BYP_B4";BYP = "BYP4"; }
                else if (X == "CX") { BYP_B = "BYP_B3";BYP = "BYP3"; }
                else if (X == "DX") { BYP_B = "BYP_B6"; BYP = "BYP6"; }
            }
           //  inst_name_H = inst_name_H.Substring(inst_name_H.IndexOf("(") + 1, inst_name_H.IndexOf(")") - inst_name_H.IndexOf("(") - 1);
            His = "\"History_" + History_NUM.ToString() + "\"";
            XDLrout.Write("net ");
            XDLrout.Write(His);
            XDLrout.Write(" ,\n  outpin \"" + inst_name + "\" " + outpin + " ,\n  inpin \"" + inst_name_H + "\" " + X + ",\n  pip CLB");
            
            XDLrout.Write(CLB_Type + "_X" + CLB_ROW);
            XDLrout.Write("Y" + COL + " ");
            XDLrout.Write("CLB" + CLB_Type + "_" + SLICE_Type + "_" + outpin + " -> " + "CLB" + CLB_Type + "_" + "LOGIC_OUTS"+ Logic + " ,\n");
            
            XDLrout.Write("  pip CLB" + CLB_Type_B + "_X" + CLB_ROW_B);
            XDLrout.Write("Y" + COL_B + " ");
            XDLrout.Write("CLB" + CLB_Type_B + "_" + BYP_B + " -> " + "CLB" + CLB_Type_B + "_" + SLICE_Type_B + "_" + X + " " + ",\n");

            XDLrout.Write("  pip INT" + "_X" + CLB_ROW);
            XDLrout.Write("Y" + COL + " ");
            XDLrout.Write("LOGIC_OUTS" + Logic + " -> " + Dir + "BEG0 ,\n");
                        
            XDLrout.Write("  pip INT" + "_X" + CLB_ROW_B);
            XDLrout.Write("Y" + COL_B + " ");
            XDLrout.Write(BYP + " -> " + BYP_B + " ,\n");

            XDLrout.Write("  pip INT" + "_X" + CLB_ROW_B);
            XDLrout.Write("Y" + COL_B + " ");
            XDLrout.Write(Dir + "END0" + " -> " + BYP + " ,\n");
       
            XDLrout.Write("  ;\n");
        }
        private static void Func_temp(ref string Line,ref string z, string outpin, string ROW, string COL,ref int Logic, StreamWriter XDLrout, string Dir)
        {
            string BB = "";
            string inst_name = "";
            string inst_name_H = "";
            string WOR = ""; string bit_dir = "";
            if (outpin == "AQ") BB = "1bb";
            else if (outpin == "BQ") BB = "3bb";
            else if (outpin == "CQ") BB = "5bb";
            else if (outpin == "DQ") BB = "7bb";
            else if (outpin == "AMUX") BB = "2bb";
            else if (outpin == "BMUX") BB = "4bb";
            else if (outpin == "CMUX") BB = "6bb";
            else if (outpin == "DMUX") BB = "8bb";

            inst_name = Line.Substring(Line.IndexOf("(") + 1, Line.IndexOf(")") - Line.IndexOf("(") - 1);
            inst_name_H = z.Substring(z.IndexOf("(") + 1, z.IndexOf(")") - z.IndexOf("(") - 1);
            WOR = z.Substring(z.IndexOf("WOR") + 3, z.IndexOf(" ", z.IndexOf("WOR")) - z.IndexOf("WOR") - 3);
            if (Int16.Parse(WOR) % 2 == 0) bit_dir = "L";
            else bit_dir = "R";
            if (z.IndexOf(" AFF") == -1 & z.IndexOf("AX") == -1)
            {
                History_NUM++;
                Func_net_2(inst_name,inst_name_H,Line, z, outpin, "AX", ROW, COL, Logic, XDLrout, Dir);
                z = z.Insert(z.Length, " AFF AX");
                Line = Line.Insert(Line.Length, bit_dir + Dir + " " + BB + " H AFF AX ");               
            }
            else if (z.IndexOf(" BFF") == -1 & z.IndexOf("BX") == -1)
            {
                History_NUM++;
                Func_net_2(inst_name, inst_name_H, Line, z, outpin, "BX", ROW, COL, Logic, XDLrout, Dir);
                z = z.Insert(z.Length, " BFF BX");
                Line = Line.Insert(Line.Length, bit_dir + Dir + " " + BB + " H BFF BX ");
            }
            else if (z.IndexOf(" CFF") == -1 & z.IndexOf("CX") == -1)
            {
                History_NUM++;
                Func_net_2(inst_name, inst_name_H, Line, z, outpin, "CX", ROW, COL, Logic, XDLrout, Dir);
                z = z.Insert(z.Length, " CFF CX");
                Line = Line.Insert(Line.Length, bit_dir + Dir + " " + BB + " H CFF CX ");
            }
            else if (z.IndexOf(" DFF") == -1 & z.IndexOf("DX") == -1)
            {
                History_NUM++;
                Func_net_2(inst_name, inst_name_H, Line, z, outpin, "DX", ROW, COL, Logic, XDLrout, Dir);
                z = z.Insert(z.Length, " DFF DX");
                Line = Line.Insert(Line.Length, bit_dir + Dir + " " + BB + " H DFF DX ");
            }

        }
        private static void func_net(string argument, StreamWriter XDLwNET,StreamWriter XDLwNETO,StreamWriter XDLwNETI)
        {
            string net_name = "";
            string outpin = "";
            string inpin = "";
            int index_inpin = 0;
            if (argument.IndexOf("cfg") == -1 & argument.IndexOf("GTXE1") == -1 & argument.IndexOf("_ML_") == -1 & argument.IndexOf("CK") == -1 & argument.IndexOf("GLOBAL_LOGIC") == -1 & argument.IndexOf("IBUF") == -1)
            {
            /*    if (argument.IndexOf("net") != -1)
                {
                    net_name = argument.Substring(argument.IndexOf("net") + 5, argument.IndexOf("\"", argument.IndexOf("net") + 5) - argument.IndexOf("net") - 5);

                    if (argument.IndexOf("cfg") != -1)
                    {
                        net_name = net_name + "\" , " + argument.Substring(argument.IndexOf("cfg"), argument.IndexOf("\",", argument.IndexOf("cfg")) - argument.IndexOf("cfg"));
                    }
                    XDLwNET.WriteLine("net \"" + net_name + "\" , ");
                }
                if (argument.IndexOf("outpin") != -1)
                {
                    outpin = argument.Substring(argument.IndexOf("outpin"), argument.IndexOf(",", argument.IndexOf("outpin")) - argument.IndexOf("outpin"));
                    XDLwNET.WriteLine("  " + outpin + ",");
                }
                index_inpin = argument.IndexOf("inpin");
                if (index_inpin != -1)
                {
                    while (index_inpin != -1)
                    {
                        inpin = argument.Substring(argument.IndexOf("inpin", index_inpin), argument.IndexOf(",", argument.IndexOf("inpin", index_inpin)) - argument.IndexOf("inpin", index_inpin));
                        XDLwNET.WriteLine("  " + inpin + ",");
                        index_inpin = argument.IndexOf("inpin", index_inpin + 3);
                    }
                }
                XDLwNET.WriteLine("  ;");*/
            }
            else if (argument.IndexOf("IBUF\"") != -1 || argument.IndexOf("\"CK_BUFGP\"") != -1 || argument.IndexOf("GLOBAL_LOGIC") != -1)
            {
                if (argument.IndexOf("CLBL") != -1 & argument.IndexOf("CFG_CENTER_") == -1 & argument.IndexOf("GTX_") == -1)
                {
                    if (argument.IndexOf("net") != -1)
                    {
                        net_name = argument.Substring(argument.IndexOf("net") + 5, argument.IndexOf("\"", argument.IndexOf("net") + 5) - argument.IndexOf("net") - 5);

                        if (argument.IndexOf("cfg \"") != -1)
                        {
                            net_name = net_name + "\" , " + argument.Substring(argument.IndexOf("cfg \""), argument.IndexOf("\",", argument.IndexOf("cfg \"")) - argument.IndexOf("cfg \""));
                        }
                        XDLwNETI.WriteLine("net \"" + net_name + "\" , ");
                    }
                    if (argument.IndexOf("outpin") != -1)
                    {
                        outpin = argument.Substring(argument.IndexOf("outpin"), argument.IndexOf(",", argument.IndexOf("outpin")) - argument.IndexOf("outpin"));
                        XDLwNETI.WriteLine("  " + outpin + ",");
                    }
                    index_inpin = argument.IndexOf("inpin");
                    if (index_inpin != -1)
                    {
                        while (index_inpin != -1)
                        {
                            inpin = argument.Substring(argument.IndexOf("inpin", index_inpin), argument.IndexOf(",", argument.IndexOf("inpin", index_inpin)) - argument.IndexOf("inpin", index_inpin));
                            XDLwNETI.WriteLine("  " + inpin + ",");
                            index_inpin = argument.IndexOf("inpin", index_inpin + 3);
                        }
                    }
                    XDLwNETI.WriteLine("  ;");
                }
            }
            else if (argument.IndexOf("OBUF\"") != -1)
            {
                if (argument.IndexOf("net") != -1)
                {
                    net_name = argument.Substring(argument.IndexOf("net") + 5, argument.IndexOf("\"", argument.IndexOf("net") + 5) - argument.IndexOf("net") - 5);

                    if (argument.IndexOf("cfg") != -1)
                    {
                        net_name = net_name + "\" , " + argument.Substring(argument.IndexOf("cfg"), argument.IndexOf("\",", argument.IndexOf("cfg")) - argument.IndexOf("cfg"));
                    }
                    XDLwNETO.WriteLine("net \"" + net_name + "\" , ");
                }
                if (argument.IndexOf("outpin") != -1)
                {
                    outpin = argument.Substring(argument.IndexOf("outpin"), argument.IndexOf(",", argument.IndexOf("outpin")) - argument.IndexOf("outpin"));
                    XDLwNETO.WriteLine("  " + outpin + ",");
                }
                index_inpin = argument.IndexOf("inpin");
                if (index_inpin != -1)
                {
                    while (index_inpin != -1)
                    {
                        inpin = argument.Substring(argument.IndexOf("inpin", index_inpin), argument.IndexOf(",", argument.IndexOf("inpin", index_inpin)) - argument.IndexOf("inpin", index_inpin));
                        XDLwNETO.WriteLine("  " + inpin + ",");
                        index_inpin = argument.IndexOf("inpin", index_inpin + 3);
                    }
                }
                XDLwNETO.WriteLine("  ;");
            }
        }
       
        static void Main(string[] args)
        {
           // int NumberOfSlices = 0;
           // int NumberOfLUTperSlice = 0;
    
            Stream s953;
            Stream Tmps953;
            Stream Tmps953NET;
            Stream Tmps953NETO;
            Stream Tmps953NETI;
            string XDLline = "";
            s953 = File.OpenRead(@"F:\MS\FinalProject\CODE\s953.XDL");
            Tmps953  = File.OpenWrite(@"F:\MS\FinalProject\CODE\Tmps953.XDL");
            Tmps953NET = File.OpenWrite(@"F:\MS\FinalProject\CODE\Tmps953NET.XDL");
            Tmps953NETO = File.OpenWrite(@"F:\MS\FinalProject\CODE\Tmps953NETO.XDL");
            Tmps953NETI = File.OpenWrite(@"F:\MS\FinalProject\CODE\Tmps953NETI.XDL");
            // Create a new stream to read from a file
             StreamReader XDL = new StreamReader(s953);
             StreamWriter XDLw = new StreamWriter(Tmps953);
             StreamWriter XDLwNET = new StreamWriter(Tmps953NET);
             StreamWriter XDLwNETO = new StreamWriter(Tmps953NETO);
             StreamWriter XDLwNETI = new StreamWriter(Tmps953NETI); 
            
             string argument = "";
             int state = 0;
            //Read contents of file into a string
             while (XDL.EndOfStream == false)
             {
                 XDLline = XDL.ReadLine();

                 if (XDLline.Equals(""))
                     continue;

                 if (XDLline[0] == '#' || XDLline[0] == '\n')
                     continue;

                 if (state != 0)
                     argument = argument + XDLline;

                 if (XDLline.IndexOf(';') != -1)
                 {

                     switch (state)
                     {
                         case 1:
                             func_inst(argument,XDLw);
                             break;
                         case 2:
                          //   func_design(argument);
                             break;
                         case 3:
                             func_net(argument,XDLwNET,XDLwNETO,XDLwNETI);
                             break; 
                     }

                     state = 0;
                 }

                 if (XDLline[0] == 'i')
                 {
                     state = 1;
                     argument = XDLline;
                 }
                 else if (XDLline[0] == 'd')
                 {
                     state = 2;
                     argument = XDLline;
                 }
                 else if (XDLline[0] == 'n')
                 {
                     state = 3;
                     argument  = XDLline;
                 }

             }

             // Close StreamReader
             XDL.Close();
             XDLw.Close();
             XDLwNET.Close();
             XDLwNETI.Close();
             XDLwNETO.Close();
                
             // Close file
             s953.Close();
             Tmps953.Close();
             Tmps953NET.Close();
             Tmps953NETI.Close();
             Tmps953NETO.Close();

             Tmps953 = File.OpenRead(@"F:\MS\FinalProject\CODE\Tmps953.XDL");
             StreamReader XDLr = new StreamReader (Tmps953);
             
             Stream Routing;
             Routing = File.OpenWrite(@"F:\MS\FinalProject\CODE\Routing.XDL");
             StreamWriter XDLrout = new StreamWriter(Routing);
             
             Find_histrory(XDLr,XDLrout);

             XDLr.Close();            

             Stream Dif_Routing;
             Dif_Routing = File.OpenRead(@"F:\MS\FinalProject\CODE\Tmprouting.XDL");
             StreamReader XDL_Dif_Routing = new StreamReader(Dif_Routing);
             Find_Final_History(XDL_Dif_Routing, XDLrout);         
            
             Dif_Routing.Close();
             XDL_Dif_Routing.Close();
             
            ///////////////////////Analyze
             Stream HCLB;
             Stream HCLB_Tmp;
             HCLB = File.OpenRead(@"F:\MS\FinalProject\CODE\FinalRouting.XDL");
             HCLB_Tmp = File.OpenRead(@"F:\MS\FinalProject\CODE\FinalRouting.XDL");
             StreamReader XDL_HCLB = new StreamReader(HCLB);
             StreamReader XDL_HCLB_Tmp = new StreamReader(HCLB_Tmp);

             Stream FHCLB;
             FHCLB = File.OpenWrite(@"F:\MS\FinalProject\CODE\FCLBRouting.XDL");
             StreamWriter XDL_FHCLB = new StreamWriter(FHCLB);

             int kolFF = 0;
             int kolH = 0;
             string Tot = XDL_HCLB_Tmp.ReadToEnd();
             while (XDL_HCLB.EndOfStream == false)
             {
                 string Line = XDL_HCLB.ReadLine();
                 
                 int FF=0;
                 int H=0;
                 
                 for (int i = 0; i < Line.Length - 2; i++)
                 {
                     if (Line.IndexOf("FF ", i, 3) != -1) FF++;
                     if (Line.IndexOf("H ", i, 2) != -1) H++;  
                 }

                 if (FF == 2 * H || FF == H || Tot.IndexOf("*", Tot.IndexOf(Line) + Line.Length, 50) != -1)
                     XDL_FHCLB.WriteLine(Line);
                 else
                 {
                     Find_History_N_CLB(Tot, ref Line, (FF - 2 * H),XDLrout);
                     XDL_FHCLB.WriteLine(Line);
                 }
                //  XDLrout.WriteLine("FF=" + FF + " H=" + H );
                //   Console.Write("FF=" + FF + " H=" + H + "\n");
                 kolFF = kolFF + FF;
                 kolH = kolH + H;
             }
                //XDLrout.WriteLine("kolFF=" + kolFF + " kolH=" + kolH+ " AllFFs= " +(kolFF-kolH));
             XDLrout.Close();
             XDL_HCLB.Close();
             XDL_HCLB_Tmp.Close();
             HCLB.Close();
             HCLB_Tmp.Close();
             XDL_FHCLB.Close();
             FHCLB.Close();
            //////////////////////////////////////////////////////////////Finish the finding of History flipflops
             PLC_Rout plc_rout = new PLC_Rout();
             plc_rout.main();
            //////////////////////////////////////////////////////////////Finish the finding of Multiplexers
         //    First_change F_change = new First_change();
         //    F_change.main();
            /////////////////////////////////////////////////////////////Finish the changing placement and some routing
             Second_change S_change = new Second_change();
             S_change.main();
            ///////////////////////////////////////////////////////////////change the original file based on changes and duplication
             First_change F_change = new First_change();
             F_change.main();
            ///////////////////////////////////////////////////////////////Design the controller 
             Controller CTRL = new Controller();
             CTRL.main();
            //////////////////////////////////////////////////////////////
            /////////////////////////////delete pips
             Stream del_pips;
             del_pips = File.OpenRead(@"F:\MS\FinalProject\CODE\Ch_s953.XDL");
             StreamReader XDL_del_PIPs = new StreamReader(del_pips);
             string LINE = "";

             Stream FT_CIRCUIT;
             FT_CIRCUIT = File.OpenWrite(@"F:\MS\FinalProject\CODE\FT_CIRCUIT_s953.XDL");
             StreamWriter XDL_FT_CIRCUIT = new StreamWriter(FT_CIRCUIT);

             LINE = XDL_del_PIPs.ReadLine();
             while (XDL_del_PIPs.EndOfStream == false)
             {
                 if (LINE.IndexOf("pip ") != -1) LINE = "";
                 else  XDL_FT_CIRCUIT.WriteLine(LINE);
                 LINE = XDL_del_PIPs.ReadLine();
             }

             XDL_FT_CIRCUIT.Close();
             FT_CIRCUIT.Close();

             Console.Write("FINITO");

             Console.ReadLine();    
        }

        private static void Count_history(string Self, ref int Bro)
        {
            if (Self.IndexOf("1b") != -1) Bro--;
            if (Self.IndexOf("2b") != -1) Bro--;
            if (Self.IndexOf("3b") != -1) Bro--;
            if (Self.IndexOf("4b") != -1) Bro--;

            if (Self.IndexOf("5b") != -1) Bro--;
            if (Self.IndexOf("6b") != -1) Bro--;
            if (Self.IndexOf("7b") != -1) Bro--;
            if (Self.IndexOf("8b") != -1) Bro--;
        }

        private static void Find_History_N_CLB(string Tot, ref string Line, int Req, StreamWriter XDLrout)
        {
            int Indx_Row = 0;   int Indx_Col = 0;   int Indx_Wor = 0;
            string ROW = ""; string COL = ""; string WOR = ""; 
            string Wst = "";string Nrth = ""; string Est = ""; string Suth = "";
            int W = 0; int N = 0; int E = 0; int S = 0;           
            string W_z = ""; string W_o = "";
            string N_z = ""; string N_o = "";
            string E_z = ""; string E_o = "";
            string S_z = ""; string S_o = "";
            int R_W_z = 0; int R_W_o = 0; int R_N_z = 0; int R_N_o = 0;
            int R_E_z = 0; int R_E_o = 0; int R_S_z = 0; int R_S_o = 0;           
            int FW = 0; int FN = 0; int FE = 0; int FS = 0;
            int tFW = 0; int tFN = 0; int tFE = 0; int tFS = 0; 

            Indx_Row = Line.IndexOf("ROW");
            Indx_Col = Line.IndexOf("COL");
            Indx_Wor = Line.IndexOf("WOR");
            ROW = Line.Substring(Indx_Row + 3, Line.IndexOf(" ", Indx_Row) - Indx_Row - 3);
            COL = Line.Substring(Indx_Col + 3, Line.IndexOf(" ", Indx_Col) - Indx_Col - 3);
            WOR = Line.Substring(Indx_Wor + 3, Line.IndexOf(" ", Indx_Wor) - Indx_Wor - 3);

            string inst_name_H = "";
            HiS_INST_NUM++;

            if (ROW != "6" & ROW != "9" & ROW != "14" & ROW != "17" & ROW != "26" & ROW != "29" & ROW != "34" & ROW != "37" & ROW != "93" &
                ROW != "42" & ROW != "58" & ROW != "62" & ROW != "66" & ROW != "71" & ROW != "74" & ROW != "83" & ROW != "86" & ROW != "91")
            {
                if (ROW == "53")
                {
                    if (Int16.Parse(COL) < 160 & Int16.Parse(COL) > 79)
                        Wst = "ROW" + (Int16.Parse(ROW) - 8).ToString() + " COL" + COL.ToString();
                }
                else Wst = "ROW" + (Int16.Parse(ROW) - 1).ToString() + " COL" + COL.ToString();
                 
            }
            else Wst = "ROW" + (Int16.Parse(ROW) - 2).ToString() + " COL" + COL.ToString();


            if (ROW != "4" & ROW != "7" & ROW != "12" & ROW != "15" & ROW != "24" & ROW != "27" & ROW != "32" & ROW != "35" & ROW != "91" &
                ROW != "40" & ROW != "56" & ROW != "60" & ROW != "64" & ROW != "69" & ROW != "72" & ROW != "81" & ROW != "84" & ROW != "89")
            {
                if (Line.IndexOf("DFF_349/Q") != -1)
                    Console.Write("DD");
                if (ROW == "45")
                {
                    if (Int16.Parse(COL) < 160 & Int16.Parse(COL) > 79)
                        Est = "ROW" + (Int16.Parse(ROW) + 8).ToString() + " COL" + COL.ToString();
                }
                else Est = "ROW" + (Int16.Parse(ROW) + 1).ToString() + " COL" + COL.ToString();
            }
            else Est = "ROW" + (Int16.Parse(ROW) + 2).ToString() + " COL" + COL.ToString();

            Nrth = "ROW" + ROW.ToString() + " COL" + (Int16.Parse(COL) + 1).ToString();            
            Suth = "ROW" + ROW.ToString() + " COL" + (Int16.Parse(COL) - 1).ToString();
            
            W = Tot.IndexOf(Wst);  N = Tot.IndexOf(Nrth);  E = Tot.IndexOf(Est); S = Tot.IndexOf(Suth);
            inst_name_H = " (REDUNDANT" + HiS_INST_NUM + ")";
            Wst = Wst + inst_name_H;
            Nrth = Nrth + inst_name_H;
            Est = Est + inst_name_H;
            Suth = Suth + inst_name_H;
            /////Call the Count resources function
            Count_resources(N, S, E, W, Tot, Nrth, Suth, Est, Wst, ref R_N_z, ref R_N_o,
                ref R_S_z, ref R_S_o, ref R_E_z, ref R_E_o, ref R_W_z, ref R_W_o, ref FN, ref FS, 
                ref FE, ref FW, ref N_z,ref N_o,ref S_z,ref S_o,ref E_z,ref E_o,ref W_z,ref W_o);
            //////Search in close CLBs and if the require FFs is less than the unused resources assign thesse resources
            search_for_History_CLB(ref Req, ref R_N_z, ref R_N_o, ref R_S_z, ref R_S_o, ref R_E_z,
                ref R_E_o, ref R_W_z, ref R_W_o, N_z, N_o, S_z, S_o, E_z, E_o, W_z, W_o, ref Line, ROW, COL, WOR, XDLrout, "D");
            //tFW = FW; tFN = FN; tFE = FE; tFS = FS; 
            inst_name_H = "";
            if (Req > 0)
            {////We should looking for CLBs that the distance from them are 1
                //FN = 0; FS = 0; FE = 0; FW = 0;
                string NW_z = ""; string NW_o = "";
                string NE_z = ""; string NE_o = "";
                string SW_z = ""; string SW_o = "";
                string SE_z = ""; string SE_o = "";
                int R_NW_z = 0; int R_NW_o = 0; int R_NE_z = 0; int R_NE_o = 0;
                int R_SE_z = 0; int R_SE_o = 0; int R_SW_z = 0; int R_SW_o = 0;
                string NrWs = ""; string NrEs = ""; string StWs = ""; string StEs = "";
                int NW = 0; int NE = 0; int SW = 0; int SE = 0;

                HiS_INST_NUM++;

                if (ROW != "6" & ROW != "9" & ROW != "14" & ROW != "17" & ROW != "26" & ROW != "29" & ROW != "34" & ROW != "37" & ROW != "93" &
                ROW != "42" & ROW != "58" & ROW != "62" & ROW != "66" & ROW != "71" & ROW != "74" & ROW != "83" & ROW != "86" & ROW != "91")
                {
                    if (ROW == "53")
                    {
                        if (Int16.Parse(COL) < 160 & Int16.Parse(COL) > 79)
                        {
                            NrWs = "ROW" + (Int16.Parse(ROW) - 8).ToString() + " COL" + (Int16.Parse(COL) + 1).ToString();
                            StWs = "ROW" + (Int16.Parse(ROW) - 8).ToString() + " COL" + (Int16.Parse(COL) - 1).ToString();
                        }
                    }
                    else
                    {
                        NrWs = "ROW" + (Int16.Parse(ROW) - 1).ToString() + " COL" + (Int16.Parse(COL) + 1).ToString();
                        StWs = "ROW" + (Int16.Parse(ROW) - 1).ToString() + " COL" + (Int16.Parse(COL) - 1).ToString();
                    }

                }
                else
                {
                    NrWs = "ROW" + (Int16.Parse(ROW) - 2).ToString() + " COL" + (Int16.Parse(COL) + 1).ToString();
                    StWs = "ROW" + (Int16.Parse(ROW) - 2).ToString() + " COL" + (Int16.Parse(COL) - 1).ToString();
                }

                if (ROW != "4" & ROW != "7" & ROW != "12" & ROW != "15" & ROW != "24" & ROW != "27" & ROW != "32" & ROW != "35" & ROW != "91" &
               ROW != "40" & ROW != "56" & ROW != "60" & ROW != "64" & ROW != "69" & ROW != "72" & ROW != "81" & ROW != "84" & ROW != "89")
                {
                    if (Line.IndexOf("DFF_349/Q") != -1)
                        Console.Write("DD");
                    if (ROW == "45")
                    {
                        if (Int16.Parse(COL) < 160 & Int16.Parse(COL) > 79)
                        {
                            NrEs = "ROW" + (Int16.Parse(ROW) + 8).ToString() + " COL" + (Int16.Parse(COL) + 1).ToString();
                            StEs = "ROW" + (Int16.Parse(ROW) + 8).ToString() + " COL" + (Int16.Parse(COL) - 1).ToString();
                        }
                    }
                    else
                    {
                        NrEs = "ROW" + (Int16.Parse(ROW) + 1).ToString() + " COL" + (Int16.Parse(COL) + 1).ToString();
                        StEs = "ROW" + (Int16.Parse(ROW) + 1).ToString() + " COL" + (Int16.Parse(COL) - 1).ToString();
                    }
                }
                else
                {
                    NrEs = "ROW" + (Int16.Parse(ROW) + 2).ToString() + " COL" + (Int16.Parse(COL) + 1).ToString();
                    StEs = "ROW" + (Int16.Parse(ROW) + 2).ToString() + " COL" + (Int16.Parse(COL) - 1).ToString();
                }
                NW = Tot.IndexOf(NrWs); NE = Tot.IndexOf(NrEs); SE = Tot.IndexOf(StEs); SW = Tot.IndexOf(StWs);
                inst_name_H = " (REDUNDANT" + HiS_INST_NUM + ")";
                NrWs = NrWs + inst_name_H;
                NrEs = NrEs + inst_name_H;
                StWs = StWs + inst_name_H;
                StEs = StEs + inst_name_H;
                Count_resources(NW, SW, NE, SE, Tot, NrWs, StWs, NrEs, StEs, ref R_NW_z, ref R_NW_o,
                    ref R_SW_z, ref R_SW_o, ref R_NE_z, ref R_NE_o, ref R_SE_z, ref R_SE_o, ref tFN, ref tFS,
                    ref tFE, ref tFW, ref NW_z, ref NW_o, ref SW_z, ref SW_o, ref NE_z, ref NE_o, ref SE_z, ref SE_o);
                //////Search in close CLBs and if the require FFs is less than the unused resources assign thesse resources
                search_for_History_CLB(ref Req, ref R_NW_z, ref R_NW_o, ref R_SW_z, ref R_SW_o, ref R_NE_z,
                    ref R_NE_o, ref R_SE_z, ref R_SE_o, NW_z, NW_o, SW_z, SW_o, NE_z, NE_o, SE_z, SE_o, ref Line,
                    ROW, COL, WOR, XDLrout, "UD");
                inst_name_H = "";
            }
            if (Req > 0)
            {////We should looking for CLBs that the distance from them are 2
                string W2_z = ""; string W2_o = "";
                string N2_z = ""; string N2_o = "";
                string E2_z = ""; string E2_o = "";
                string S2_z = ""; string S2_o = "";
                int R_W2_z = 0; int R_W2_o = 0; int R_N2_z = 0; int R_N2_o = 0;
                int R_E2_z = 0; int R_E2_o = 0; int R_S2_z = 0; int R_S2_o = 0;
                string Wst2 = ""; string Nrth2 = ""; string Est2 = ""; string Suth2 = "";
                int W2 = 0; int N2 = 0; int E2 = 0; int S2 = 0;
                HiS_INST_NUM++;

                if (ROW != "7" & ROW != "10" & ROW != "15" & ROW != "18" & ROW != "27" & ROW != "30" & ROW != "35" & ROW != "38" & ROW != "94" &
                ROW != "43" & ROW != "59" & ROW != "63" & ROW != "67" & ROW != "72" & ROW != "75" & ROW != "84" & ROW != "87" & ROW != "92")
                {
                    if (ROW == "53")
                    {
                        if (Int16.Parse(COL) < 160 & Int16.Parse(COL) > 79)
                            Wst2 = "ROW" + (Int16.Parse(ROW) - 10).ToString() + " COL" + COL.ToString();
                    }
                    else Wst2 = "ROW" + (Int16.Parse(ROW) - 2).ToString() + " COL" + COL.ToString();
                }
                else Wst2 = "ROW" + (Int16.Parse(ROW) - 3).ToString() + " COL" + COL.ToString();

                if (ROW != "3" & ROW != "6" & ROW != "11" & ROW != "14" & ROW != "23" & ROW != "26" & ROW != "31" & ROW != "34" & ROW != "90" &
                ROW != "39" & ROW != "55" & ROW != "59" & ROW != "63" & ROW != "68" & ROW != "71" & ROW != "80" & ROW != "83" & ROW != "88")
                {
                    if (ROW == "45")
                    {
                        if (Int16.Parse(COL) < 160 & Int16.Parse(COL) > 79)
                            Est2 = "ROW" + (Int16.Parse(ROW) + 10).ToString() + " COL" + COL.ToString();
                    }
                    else Est2 = "ROW" + (Int16.Parse(ROW) + 2).ToString() + " COL" + COL.ToString();
                }
                else Est2 = "ROW" + (Int16.Parse(ROW) + 3).ToString() + " COL" + COL.ToString();

                Nrth2 = "ROW" + ROW.ToString() + " COL" + (Int16.Parse(COL) + 2).ToString();                
                Suth2 = "ROW" + ROW.ToString() + " COL" + (Int16.Parse(COL) - 2).ToString();
                W2 = Tot.IndexOf(Wst2); N2 = Tot.IndexOf(Nrth2); E2 = Tot.IndexOf(Est2); S2 = Tot.IndexOf(Suth2);
                inst_name_H = " (REDUNDANT" + HiS_INST_NUM + ")";
                Wst2 = Wst2 + inst_name_H;
                Nrth2 = Nrth2 + inst_name_H;
                Est2 = Est2 + inst_name_H;
                Suth2 = Suth2 + inst_name_H;
                /////Call the Count resources function
                Count_resources(N2, S2, E2, W2, Tot, Nrth2, Suth2, Est2, Wst2, ref R_N2_z, ref R_N2_o,
                    ref R_S2_z, ref R_S2_o, ref R_E2_z, ref R_E2_o, ref R_W2_z, ref R_W2_o, ref tFN, ref tFS,
                    ref tFE, ref tFW, ref N2_z, ref N2_o, ref S2_z, ref S2_o, ref E2_z, ref E2_o, ref W2_z, ref W2_o);
                //////Search in close CLBs and if the require FFs is less than the unused resources assign thesse resources
                search_for_History_CLB(ref Req, ref R_N2_z, ref R_N2_o, ref R_S2_z, ref R_S2_o, ref R_E2_z,
                    ref R_E2_o, ref R_W2_z, ref R_W2_o, N2_z, N2_o, S2_z, S2_o, E2_z, E2_o, W2_z, W2_o, ref Line,
                    ROW, COL, WOR, XDLrout, "D2");
                inst_name_H = "";
            }
            if(Req > 0)
            {
               string WOR_z_e = "";
               string WOR_o_e = "";
               string WOR_z_w = "";
               string WOR_o_w = "";
               string WOR_z_ns = "";
               string WOR_o_ns = "";

               if (Int32.Parse(WOR) % 2 == 0)
               {
                   WOR_z_e = (Int32.Parse(WOR) + 2).ToString();
                   WOR_o_e = (Int32.Parse(WOR) + 3).ToString();

                   WOR_z_w = (Int32.Parse(WOR) - 2).ToString();
                   WOR_o_w = (Int32.Parse(WOR) - 1).ToString();

                   WOR_z_ns = WOR;
                   WOR_o_ns = (Int32.Parse(WOR) + 1).ToString();
               }
               else
               {
                   WOR_z_e = (Int32.Parse(WOR) + 1).ToString();
                   WOR_o_e = (Int32.Parse(WOR) + 2).ToString();

                   WOR_z_w = (Int32.Parse(WOR) - 3).ToString();
                   WOR_o_w = (Int32.Parse(WOR) - 2).ToString();

                   WOR_z_ns = (Int32.Parse(WOR) - 1).ToString();
                   WOR_o_ns = WOR;
               }
               int tmpreq = Req;               
               HiS_INST_NUM++;
               
               if (FN == 1)
               {                   
                   R_N_o = R_N_z = 4;
                   inst_name_H = " (REDUNDANT" + HiS_INST_NUM + ")";
                   N_z = "CTLM ROW" + ROW.ToString() + " COL" + (Int32.Parse(COL) + 1) + " STL " + "WOR" + WOR_z_ns + " " + (Int32.Parse(COL) + 1) + inst_name_H + " ;";
                   N_o = "CTLM ROW" + ROW.ToString() + " COL" + (Int32.Parse(COL) + 1) + " STL " + "WOR" + WOR_o_ns + " " + (Int32.Parse(COL) + 1) + inst_name_H + " ;";
                   Search_for_History(ref R_N_z, N_z, ref R_N_o, N_o, ref Req, ref Line, ROW, COL, Int32.Parse(WOR) + 2, XDLrout, "NN1");
                   if (tmpreq <= 4)
                       Line = Line.Insert(Line.Length - 1, "\n" + N_z);
                   else
                       Line = Line.Insert(Line.Length - 1, "\n" + N_z + "\n" + N_o);

               }
               else if (FS == 1)
               {                   
                   R_S_o = R_S_z = 4;
                   inst_name_H = " (REDUNDANT" + HiS_INST_NUM + ")";
                   S_z = "CTLM ROW" + ROW.ToString() + " COL" + (Int32.Parse(COL) - 1) + " STL " + "WOR" + WOR_z_ns + " " + (Int32.Parse(COL) - 1) + inst_name_H + " ;";
                   S_o = "CTLM ROW" + ROW.ToString() + " COL" + (Int32.Parse(COL) - 1) + " STL " + "WOR" + WOR_o_ns + " " + (Int32.Parse(COL) - 1) + inst_name_H + " ;";
                   Search_for_History(ref R_S_z, S_z, ref R_S_o, S_o, ref Req, ref Line, ROW, COL, Int32.Parse(WOR) - 2, XDLrout, "SS1");
                   if (tmpreq <= 4)
                       Line = Line.Insert(Line.Length - 1, "\n" + S_z);
                   else
                       Line = Line.Insert(Line.Length - 1, "\n" + S_z + "\n" + S_o);
               }
               else if (FW == 1)
               {
                   R_W_o = R_W_z = 4;
                   inst_name_H = " (REDUNDANT" + HiS_INST_NUM + ")";
                   W_z = "CTLM ROW" + (Int32.Parse(ROW) - 1).ToString() + " COL" + COL + " STL " + "WOR" + WOR_z_w + " " + COL + inst_name_H + " ;";
                   W_o = "CTLM ROW" + (Int32.Parse(ROW) - 1).ToString() + " COL" + COL + " STL " + "WOR" + WOR_o_w + " " + COL + inst_name_H + " ;";
                   Search_for_History(ref R_W_z, W_z, ref R_W_o, W_o, ref Req, ref Line, ROW, COL, Int32.Parse(WOR), XDLrout, "WW1");
                   if (tmpreq <= 4)
                       Line = Line.Insert(Line.Length - 1, "\n" + W_z);
                   else
                       Line = Line.Insert(Line.Length - 1, "\n" + W_z + "\n" + W_o);
               }
               else if (FE == 1)
               {
                   R_E_o = R_E_z = 4;
                   inst_name_H = " (REDUNDANT" + HiS_INST_NUM + ")";
                   E_z = "CTLM ROW" + (Int32.Parse(ROW) + 1).ToString() + " COL" + COL + " STL " + "WOR" + WOR_z_e + " " + COL + inst_name_H + " ;";
                   E_o = "CTLM ROW" + (Int32.Parse(ROW) + 1).ToString() + " COL" + COL + " STL " + "WOR" + WOR_o_e + " " + COL + inst_name_H + " ;";
                   Search_for_History(ref R_E_z, E_z, ref R_E_o, E_o, ref Req, ref Line, ROW, COL, Int32.Parse(WOR), XDLrout, "EE1");
                   if (tmpreq <= 4)
                       Line = Line.Insert(Line.Length - 1, "\n" + E_z);
                   else
                       Line = Line.Insert(Line.Length -1, "\n" + E_z + "\n" + E_o);
               }
               Console.Write("\nIT'S NOT COMPELETED! :D\n" + Req + "\n");
            }             
        }

        private static void search_for_History_CLB(ref int Req, ref int R_N_z, ref int R_N_o, ref int R_S_z, ref int R_S_o, ref int R_E_z, ref int R_E_o, ref int R_W_z, ref int R_W_o, string N_z, string N_o, string S_z, string S_o, string E_z, string E_o, string W_z, string W_o, ref string Line, string ROW, string COL, string WOR, StreamWriter XDLrout, string Si)
        {
            string N = ""; string S = ""; string W = ""; string E = "";

            if (Si == "D") { N = "NN1"; S = "SS1"; W = "WW1"; E = "EE1"; }
            else if (Si == "UD") { N = "NW2"; S = "SW2"; E = "NE2"; W = "SE2"; }
            else if (Si == "D2") { N = "NN2"; S = "SS2"; W = "WW2"; E = "EE2"; }

            if (Req <= R_W_z + R_W_o || Req <= R_N_z + R_N_o || Req <= R_S_z + R_S_o || Req <= R_E_z + R_E_o)
            {
                int Div_W = 0; int Div_N = 0; int Div_S = 0; int Div_E = 0;
                Div_W = R_W_z + R_W_o - Req; Div_N = R_N_z + R_N_o - Req;
                Div_E = R_E_z + R_E_o - Req; Div_S = R_S_z + R_S_o - Req;

                if (Div_N >= Div_W & Div_N >= Div_E & Div_N >= Div_S) Search_for_History(ref R_N_z, N_z, ref R_N_o, N_o, ref Req, ref Line, ROW, COL, Int32.Parse(WOR), XDLrout, N);
                else if (Div_S >= Div_W & Div_S >= Div_E & Div_S >= Div_N) Search_for_History(ref R_S_z, S_z, ref R_S_o, S_o, ref Req, ref Line, ROW, COL, Int32.Parse(WOR), XDLrout, S);
                else if (Div_W >= Div_N & Div_W >= Div_S & Div_W >= Div_E) Search_for_History(ref R_W_z, W_z, ref R_W_o, W_o, ref Req, ref Line, ROW, COL, Int32.Parse(WOR), XDLrout, W);
                else if (Div_E >= Div_N & Div_E >= Div_S & Div_E >= Div_W) Search_for_History(ref R_E_z, E_z, ref R_E_o, E_o, ref Req, ref Line, ROW, COL, Int32.Parse(WOR), XDLrout, E);
            }
            /////Else if the resources in close CLBs are not sufficient the select from all of them
            else if (Req <= R_W_z + R_W_o + R_N_z + R_N_o + R_S_z + R_S_o + R_E_o + R_E_z)
            {
                while (Req > 0)
                {
                    if (R_N_o != 0 || R_N_z != 0) Search_for_History(ref R_N_z, N_z, ref R_N_o, N_o, ref Req, ref Line, ROW, COL, Int32.Parse(WOR), XDLrout, N);
                    if (R_S_o != 0 || R_S_z != 0) Search_for_History(ref R_S_z, S_z, ref R_S_o, S_o, ref Req, ref Line, ROW, COL, Int32.Parse(WOR), XDLrout, S);
                    if (R_W_o != 0 || R_W_z != 0) Search_for_History(ref R_W_z, W_z, ref R_W_o, W_o, ref Req, ref Line, ROW, COL, Int32.Parse(WOR), XDLrout, W);
                    if (R_E_o != 0 || R_E_z != 0) Search_for_History(ref R_E_z, E_z, ref R_E_o, E_o, ref Req, ref Line, ROW, COL, Int32.Parse(WOR), XDLrout, E);
                }
            }
            
            else
            {
                while (Req > 0 & (R_N_o > 0 || R_N_z > 0 || R_S_o > 0 || R_S_z > 0 || R_W_o > 0 || R_W_z > 0 || R_E_o > 0 || R_E_z>0))
                {
                    if (R_N_o != 0 || R_N_z != 0) Search_for_History(ref R_N_z, N_z, ref R_N_o, N_o, ref Req, ref Line, ROW, COL, Int32.Parse(WOR), XDLrout, N);
                    if (R_S_o != 0 || R_S_z != 0) Search_for_History(ref R_S_z, S_z, ref R_S_o, S_o, ref Req, ref Line, ROW, COL, Int32.Parse(WOR), XDLrout, S);
                    if (R_W_o != 0 || R_W_z != 0) Search_for_History(ref R_W_z, W_z, ref R_W_o, W_o, ref Req, ref Line, ROW, COL, Int32.Parse(WOR), XDLrout, W);
                    if (R_E_o != 0 || R_E_z != 0) Search_for_History(ref R_E_z, E_z, ref R_E_o, E_o, ref Req, ref Line, ROW, COL, Int32.Parse(WOR), XDLrout, E);
                }
            }
        }

        private static void Count_resources(int N, int S, int E, int W, string Tot, string Nrth, string Suth, string Est, string Wst, ref int R_N_z, ref int R_N_o, ref int R_S_z, ref int R_S_o, ref int R_E_z, ref int R_E_o, ref int R_W_z, ref int R_W_o, ref int FN, ref int FS, ref int FE, ref int FW, ref string N_z, ref string N_o, ref string S_z, ref string S_o, ref string E_z, ref string E_o, ref string W_z, ref string W_o)
        {
            if (W != -1)
            {
                W_z = Tot.Substring(W - 5, Tot.IndexOf("CTL", W + 1) - W + 5);

                if (W_z.IndexOf(" AFF") == -1 & W_z.IndexOf("AX") == -1) R_W_z++;
                if (W_z.IndexOf(" BFF") == -1 & W_z.IndexOf("BX") == -1) R_W_z++;
                if (W_z.IndexOf(" CFF") == -1 & W_z.IndexOf("CX") == -1) R_W_z++;
                if (W_z.IndexOf(" DFF") == -1 & W_z.IndexOf("DX") == -1) R_W_z++;
                Count_history(W_z, ref R_W_o);
                if (Tot.IndexOf(Wst, W + 1) != -1)
                {
                    W_o = Tot.Substring(Tot.IndexOf(Wst, W + 1) - 5, Tot.IndexOf("CTL", Tot.IndexOf(Wst, W + 1) + 1) - Tot.IndexOf(Wst, W + 1) + 5);
                    if (W_o.IndexOf(" AFF") == -1 & W_o.IndexOf("AX") == -1) R_W_o++;
                    if (W_o.IndexOf(" BFF") == -1 & W_o.IndexOf("BX") == -1) R_W_o++;
                    if (W_o.IndexOf(" CFF") == -1 & W_o.IndexOf("CX") == -1) R_W_o++;
                    if (W_o.IndexOf(" DFF") == -1 & W_o.IndexOf("DX") == -1) R_W_o++;
                    Count_history(W_o, ref R_W_z);
                }
            }
            else { FW = 1; }////Both of z & o are not available
            if (N != -1)
            {
                N_z = Tot.Substring(N - 5, Tot.IndexOf("CTL", N + 1) - N + 5);

                if (N_z.IndexOf(" AFF") == -1 & N_z.IndexOf("AX") == -1) R_N_z++;
                if (N_z.IndexOf(" BFF") == -1 & N_z.IndexOf("BX") == -1) R_N_z++;
                if (N_z.IndexOf(" CFF") == -1 & N_z.IndexOf("CX") == -1) R_N_z++;
                if (N_z.IndexOf(" DFF") == -1 & N_z.IndexOf("DX") == -1) R_N_z++;

                Count_history(N_z, ref R_N_o);
                if (Tot.IndexOf(Nrth, N + 1) != -1)
                {
                    N_o = Tot.Substring(Tot.IndexOf(Nrth, N + 1) - 5, Tot.IndexOf("CTL", Tot.IndexOf(Nrth, N + 1) + 1) - Tot.IndexOf(Nrth, N + 1) + 5);
                    if (N_o.IndexOf(" AFF") == -1 & N_o.IndexOf("AX") == -1) R_N_o++;
                    if (N_o.IndexOf(" BFF") == -1 & N_o.IndexOf("BX") == -1) R_N_o++;
                    if (N_o.IndexOf(" CFF") == -1 & N_o.IndexOf("CX") == -1) R_N_o++;
                    if (N_o.IndexOf(" DFF") == -1 & N_o.IndexOf("DX") == -1) R_N_o++;
                    Count_history(N_o, ref R_N_z);
                }
            }
            else { FN = 1; }////Both of z & o are not available
            if (E != -1)
            {
                E_z = Tot.Substring(E - 5, Tot.IndexOf("CTL", E + 1) - E + 5);

                if (E_z.IndexOf(" AFF") == -1 & E_z.IndexOf("AX") == -1) R_E_z++;

                if (E_z.IndexOf(" BFF") == -1 & E_z.IndexOf("BX") == -1) R_E_z++;

                if (E_z.IndexOf(" CFF") == -1 & E_z.IndexOf("CX") == -1) R_E_z++;

                if (E_z.IndexOf(" DFF") == -1 & E_z.IndexOf("DX") == -1) R_E_z++;

                Count_history(E_z, ref R_E_o);
                if (Tot.IndexOf(Est, E + 2) != -1)
                {
                    E_o = Tot.Substring(Tot.IndexOf(Est, E + 1) - 5, Tot.IndexOf("CTL", Tot.IndexOf(Est, E + 1) + 1) - Tot.IndexOf(Est, E + 1) + 5);
                    if (E_o.IndexOf(" AFF") == -1 & E_o.IndexOf("AX") == -1) R_E_o++;

                    if (E_o.IndexOf(" BFF") == -1 & E_o.IndexOf("BX") == -1) R_E_o++;

                    if (E_o.IndexOf(" CFF") == -1 & E_o.IndexOf("CX") == -1) R_E_o++;

                    if (E_o.IndexOf(" DFF") == -1 & E_o.IndexOf("DX") == -1) R_E_o++;
                    Count_history(E_o, ref R_E_z);
                }
            }
            else { FE = 1; }////Both of z & o are not available
            if (S != -1)
            {
                S_z = Tot.Substring(S - 5, Tot.IndexOf("CTL", S + 1) - S + 5);

                if (S_z.IndexOf(" AFF") == -1 & S_z.IndexOf("AX") == -1) R_S_z++;

                if (S_z.IndexOf(" BFF") == -1 & S_z.IndexOf("BX") == -1) R_S_z++;

                if (S_z.IndexOf(" CFF") == -1 & S_z.IndexOf("CX") == -1) R_S_z++;

                if (S_z.IndexOf(" DFF") == -1 & S_z.IndexOf("DX") == -1) R_S_z++;
                Count_history(S_z, ref R_S_o);
                if (Tot.IndexOf(Suth, S + 1) != -1)
                {
                    S_o = Tot.Substring(Tot.IndexOf(Suth, S + 1) - 5, Tot.IndexOf("CTL", Tot.IndexOf(Suth, S + 1) + 1) - Tot.IndexOf(Suth, S + 1) + 5);
                    if (S_o.IndexOf(" AFF") == -1 & S_o.IndexOf("AX") == -1) R_S_o++;

                    if (S_o.IndexOf(" BFF") == -1 & S_o.IndexOf("BX") == -1) R_S_o++;

                    if (S_o.IndexOf(" CFF") == -1 & S_o.IndexOf("CX") == -1) R_S_o++;

                    if (S_o.IndexOf(" DFF") == -1 & S_o.IndexOf("DX") == -1) R_S_o++;
                    Count_history(S_o, ref R_S_z);
                }
            }
            else { FS = 1; }////Both of z & o are not available
        }

        private static void Search_for_History(ref int R_z, string z,ref int R_o, string o,ref int Req, ref string Line, string ROW, string COL, int WOR, StreamWriter XDLrout,string Dir)
        {
            int Logic = 0;
            while (Req > 0 & (R_o > 0 || R_z > 0))
            { 
                if(Line.IndexOf(" AFF")!=-1)
                    if (Line.IndexOf("H", Line.IndexOf(" AFF") - 1,1) == -1)
                    {
                        if (Line.IndexOf("1 H") == -1 & Line.IndexOf("1b H") == -1 & Line.IndexOf("1bb H") == -1)
                        {
                            if (WOR % 2 == 0) Logic = 0;
                            else Logic = 4;

                            if (R_z > 0) 
                            {
                                Func_temp(ref Line,ref z, "AQ", ROW, COL,ref Logic, XDLrout, Dir);
                                R_z--;
                                Req--; 
                            }
                            else if (R_o > 0)
                            {
                                Func_temp(ref Line,ref o, "AQ", ROW, COL,ref Logic, XDLrout, Dir);
                                R_o--;
                                Req--; 
                            }
                            
                        }
                    }
                if (Line.IndexOf(" BFF") != -1)
                    if (Line.IndexOf("H", Line.IndexOf(" BFF") - 1, 1) == -1)
                    {
                        if (Line.IndexOf("3 H") == -1 & Line.IndexOf("3b H") == -1 & Line.IndexOf("3bb H") == -1)
                        {
                            if (WOR % 2 == 0) Logic = 1;
                            else Logic = 5;

                            if (R_z > 0)
                            {
                                Func_temp(ref Line,ref z, "BQ", ROW, COL,ref Logic, XDLrout, Dir);
                                R_z--;
                                Req--;
                            }
                            else if (R_o > 0)
                            {
                                Func_temp(ref Line,ref o, "BQ", ROW, COL,ref Logic, XDLrout, Dir);
                                R_o--;
                                Req--;
                            }                            
                        }
                    }
                if (Line.IndexOf(" CFF") != -1)
                    if (Line.IndexOf("H", Line.IndexOf(" CFF") - 1, 1) == -1)
                    {
                        if (Line.IndexOf("5 H") == -1 & Line.IndexOf("5b H") == -1 & Line.IndexOf("5bb H") == -1)
                        {
                            if (WOR % 2 == 0) Logic = 2;
                            else Logic = 6;

                            if (R_z > 0)
                            {
                                Func_temp(ref Line,ref z, "CQ", ROW, COL,ref Logic, XDLrout, Dir);
                                R_z--;
                                Req--;                          
                            }
                            else if (R_o > 0)
                            {
                                Func_temp(ref Line,ref o, "CQ", ROW, COL,ref Logic, XDLrout, Dir);
                                R_o--;
                                Req--;                          
                            }                            
                        }
                    }
                if (Line.IndexOf(" DFF") != -1)
                    if (Line.IndexOf("H", Line.IndexOf(" DFF") - 1, 1) == -1)
                    {
                        if (Line.IndexOf("7 H") == -1 & Line.IndexOf("7b H") == -1 & Line.IndexOf("7bb H") == -1)
                        {
                            if (WOR % 2 == 0) Logic = 3;
                            else Logic = 7;

                            if (R_z > 0)
                            {
                                Func_temp(ref Line,ref z, "DQ", ROW, COL,ref Logic, XDLrout, Dir);
                                R_z--;
                                Req--;
                            }
                            else if (R_o > 0)
                            {
                                Func_temp(ref Line,ref o, "DQ", ROW, COL,ref Logic, XDLrout, Dir);
                                R_o--;
                                Req--;
                            }                            
                        }
                    }
                if (Line.IndexOf(" A5FF") != -1)
                    if (Line.IndexOf("H", Line.IndexOf(" A5FF") - 1, 1) == -1)
                    {
                        if (Line.IndexOf("2 H") == -1 & Line.IndexOf("2b H") == -1 & Line.IndexOf("2bb H") == -1)
                        {
                            if (WOR % 2 == 0) Logic = 16;
                            else Logic = 20;

                            if (R_z > 0)
                            {
                                Func_temp(ref Line,ref z, "AMUX", ROW, COL,ref Logic, XDLrout, Dir);
                                R_z--;
                                Req--;
                            }
                            else if (R_o > 0)
                            {
                                Func_temp(ref Line,ref o, "AMUX", ROW, COL,ref Logic, XDLrout, Dir);
                                R_o--;
                                Req--;
                            }                            
                        }
                    }
                if (Line.IndexOf(" B5FF") != -1)
                    if (Line.IndexOf("H", Line.IndexOf(" B5FF") - 1, 1) == -1)
                    {
                        if (Line.IndexOf("4 H") == -1 & Line.IndexOf("4b H") == -1 & Line.IndexOf("4bb H") == -1)
                        {
                            if (WOR % 2 == 0) Logic = 17;
                            else Logic = 21;

                            if (R_z > 0)
                            {
                                Func_temp(ref Line,ref z, "BMUX", ROW, COL,ref Logic, XDLrout, Dir);
                                R_z--;
                                Req--;
                            }
                            else if (R_o > 0)
                            {
                                Func_temp(ref Line,ref o, "BMUX", ROW, COL,ref Logic, XDLrout, Dir);
                                R_o--;
                                Req--;
                            }                            
                        }
                    }
                if (Line.IndexOf(" C5FF") != -1)
                    if (Line.IndexOf("H", Line.IndexOf(" C5FF") - 1, 1) == -1)
                    {
                        if (Line.IndexOf("6 H") == -1 & Line.IndexOf("6b H") == -1 & Line.IndexOf("6bb H") == -1)
                        {
                            if (WOR % 2 == 0) Logic = 18;
                            else Logic = 22;

                            if (R_z > 0)
                            {
                                Func_temp(ref Line,ref z, "CMUX", ROW, COL,ref Logic, XDLrout, Dir);
                                R_z--;
                                Req--;
                            }
                            else if (R_o > 0)
                            {
                                Func_temp(ref Line,ref o, "CMUX", ROW, COL,ref Logic, XDLrout, Dir);
                                R_o--;
                                Req--;
                            }                           
                        }
                    }
                if (Line.IndexOf(" D5FF") != -1)
                    if (Line.IndexOf("H", Line.IndexOf(" D5FF") - 1, 1) == -1)
                    {
                        if (Line.IndexOf("8 H") == -1 & Line.IndexOf("8b H") == -1 & Line.IndexOf("8bb H") == -1)
                        {
                            if (WOR % 2 == 0) Logic = 19;
                            else Logic = 23;

                            if (R_z > 0)
                            {
                                Func_temp(ref Line,ref z, "DMUX", ROW, COL,ref Logic, XDLrout, Dir);
                                R_z--;
                                Req--;
                            }
                            else if (R_o > 0)
                            {
                                Func_temp(ref Line,ref o, "DMUX", ROW, COL,ref Logic, XDLrout, Dir);
                                R_o--;
                                Req--;
                            }                            
                        }
                    }
            }
        }      

        private static void Find_Final_History(StreamReader XDL_Dif_Routing, StreamWriter XDLrout)
        {
            Stream FinalRouting;
            FinalRouting = File.OpenWrite(@"F:\MS\FinalProject\CODE\FinalRouting.XDL");
            StreamWriter XDl_Final_Routing = new StreamWriter(FinalRouting);

            Stream Dif_Routing_temp;
            Dif_Routing_temp = File.OpenRead(@"F:\MS\FinalProject\CODE\Tmprouting.XDL");
            StreamReader XDL_Dif_Routing_temp = new StreamReader(Dif_Routing_temp);

            string inst_name = "";
            string inst_name_H = "";
            string FLine = ""; string FNLine = ""; string Total = "";
            int SA = 0; int SB = 0; int SC = 0; int SD = 0; int S5A = 0; int S5B = 0;  int S5C = 0; int S5D = 0; int indFL = 0;
            string LOGIC_OUTS = ""; string BYP_B = ""; string SLICE_Type = ""; string CLB_Type = "";
            Total = XDL_Dif_Routing_temp.ReadToEnd();          
            while (XDL_Dif_Routing.EndOfStream == false)
            {
                SA = 0; SB = 0; SC = 0;SD = 0; S5A = 0; S5B = 0;S5C = 0; S5D = 0;
                int Tindex = 0; int len = 0; int Ind_Tot = 0;
                FLine = XDL_Dif_Routing.ReadLine(); 
                indFL = Total.IndexOf(FNLine);
                Tindex = Total.IndexOf(FLine);
                len = FLine.Length;
                Ind_Tot = Total.IndexOf("CTL", Tindex + len);
                /////////////////////
                if (Ind_Tot != -1)
                    FNLine = Total.Substring(Tindex + len + 1, Ind_Tot - Tindex - len - 1);
            //    else break;

                if (FLine.IndexOf("FF ") == -1 || FLine.IndexOf("*") != -1) XDl_Final_Routing.WriteLine(FLine);
                    ////////Check the availability of FFs without history               
                else if (FLine.IndexOf("*") == -1)
                {     
                    if (FLine.IndexOf(" AFF") != -1)
                        if (FLine.IndexOf("H", FLine.IndexOf(" AFF") - 1, 1) == -1)
                        {
                            SA = 1;/////FF is available
                            if (FLine.IndexOf("1 H") != -1)
                                SA = 9;//The history is available in its slice
                            else if (FNLine.IndexOf("*") != -1)
                                if (FNLine.IndexOf("1 H") != -1)////The history is available in his brother slice
                                    SA = 24;} ///////AFF
                    if (FLine.IndexOf(" BFF") != -1)
                        if (FLine.IndexOf("H", FLine.IndexOf(" BFF") - 1, 1) == -1)
                        {
                            SB = 2;
                            if (FLine.IndexOf("3 H") != -1)
                                SB = 11;
                            else if (FNLine.IndexOf("*") != -1)
                                if (FNLine.IndexOf("3 H") != -1)
                                    SB = 26;}///////BFF
                    if (FLine.IndexOf(" CFF") != -1)
                        if (FLine.IndexOf("H", FLine.IndexOf(" CFF") - 1, 1) == -1)
                        {
                            SC = 3;
                            if (FLine.IndexOf("5 H") != -1)
                                SC = 13;
                            else if (FNLine.IndexOf("*") != -1)
                                if (FNLine.IndexOf("5 H") != -1)
                                    SC = 28;}///////CFF
                    if (FLine.IndexOf(" DFF") != -1)
                        if (FLine.IndexOf("H", FLine.IndexOf(" DFF") - 1, 1) == -1)
                        {
                            SD = 4;
                            if (FLine.IndexOf("7 H") != -1)
                                SD = 15;
                            else if (FNLine.IndexOf("*") != -1)
                                if (FNLine.IndexOf("7 H") != -1)
                                    SD = 30;}///////DFF
                    if (FLine.IndexOf(" A5FF") != -1)
                        if (FLine.IndexOf("H", FLine.IndexOf(" A5FF") - 1, 1) == -1)
                        {
                            S5A = 5;
                            if (FLine.IndexOf("2 H") != -1)
                                S5A = 12;
                            else if (FNLine.IndexOf("*") != -1)
                                if (FNLine.IndexOf("2 H") != -1)
                                    S5A = 32;}///////A5FF
                    if (FLine.IndexOf(" B5FF") != -1)
                        if (FLine.IndexOf("H", FLine.IndexOf(" B5FF") - 1, 1) == -1)
                        {
                            S5B = 6;
                            if (FLine.IndexOf("4 H") != -1)
                                S5B = 13;
                            else if (FNLine.IndexOf("*") != -1)
                                if (FNLine.IndexOf("4 H") != -1)
                                    S5B = 34;}///////B5FF
                    if (FLine.IndexOf(" C5FF") != -1)
                        if (FLine.IndexOf("H", FLine.IndexOf(" C5FF") - 1, 1) == -1)
                        {
                            S5C = 7;
                            if (FLine.IndexOf("6 H") != -1)
                                S5C = 14;
                            else if (FNLine.IndexOf("*") != -1)
                                if (FNLine.IndexOf("6 H") != -1)
                                    S5C = 36;}///////C5FF
                    if (FLine.IndexOf(" D5FF") != -1)
                        if (FLine.IndexOf("H", FLine.IndexOf(" D5FF") - 1, 1) == -1)
                        {
                            S5D = 8;
                            if (FLine.IndexOf("8 H") != -1)
                                S5D = 15;
                            else if (FNLine.IndexOf("*") != -1)
                                if (FNLine.IndexOf("8 H") != -1)
                                    S5D = 38; }////////D5FF

                    if (SA == 1 || SB == 2 || SC == 3 || SD == 4 || S5A == 5 || S5B == 6 || S5C == 7 || S5D == 8)
                    {
                        string ROW = ""; string COL = ""; int index = 0; int indS = 0; int row = 0; int Brother = 0;
                        int End_Brother = 0; int first_Brother = 0; int FA = 0; int FB = 0; int FC = 0; int FD = 0; int F5A = 0;
                        int F5B = 0; int F5C = 0; int F5D = 0; string Broth_Slice = ""; string CLB_ROW = ""; int Ind_clb = 0;

                        index = FLine.IndexOf("WOR");
                        indS = FLine.IndexOf(" ", index);
                        Ind_clb = FLine.IndexOf("ROW");

                        ROW = FLine.Substring(index + 3, FLine.IndexOf(" ", index) - index - 3);
                        COL = FLine.Substring(indS + 1, FLine.IndexOf(" ", indS + 1) - indS - 1);
                        CLB_ROW = FLine.Substring(Ind_clb + 3, FLine.IndexOf(" ", Ind_clb) - Ind_clb - 3);

                        row = Int32.Parse(ROW);
                        if (row % 2 == 0) row++;
                        else row--;

                        Brother = Total.IndexOf("WOR" + row.ToString() + " " + COL);///////Availability of brother

                        if (Brother != -1)//////If the brother slice is available then......
                        {

                            End_Brother = Total.IndexOf(";", Brother);
                            first_Brother = Total.IndexOf("CTL", Brother - 45);
                            Broth_Slice = Total.Substring(first_Brother, End_Brother - first_Brother + 2);

                            CLB_Type = "LL"; SLICE_Type = "L";

                            ///////////////////////////////
                            if (Broth_Slice.IndexOf("AX") == -1)
                            {
                                if (Broth_Slice.IndexOf(" AFF") == -1)
                                    FA = 1;
                                else if (Broth_Slice.IndexOf(" A5FF") == -1 & Broth_Slice.IndexOf("AOUTMUX") == -1)
                                    F5A = 1;
                            }
                            if (Broth_Slice.IndexOf("A5LUT") == -1)
                            {
                                if (Broth_Slice.IndexOf(" AFF") == -1 & FA == 0)
                                    FA = 2;
                                else if (Broth_Slice.IndexOf(" A5FF") == -1 & Broth_Slice.IndexOf("AOUTMUX") == -1 & F5A == 0)
                                    F5A = 2;
                            }
                            ////////////////Checking the freedom of AX & .....in the brother's region///////////////////
                            if (Broth_Slice.IndexOf("BX") == -1)
                            {
                                if (Broth_Slice.IndexOf(" BFF") == -1)
                                    FB = 1;
                                else if (Broth_Slice.IndexOf(" B5FF") == -1 & Broth_Slice.IndexOf("BOUTMUX") == -1)
                                    F5B = 1;
                            }
                            if (Broth_Slice.IndexOf("B5LUT") == -1)
                            {
                                if (Broth_Slice.IndexOf(" BFF") == -1 & FB == 0)
                                    FB = 2;
                                else if (Broth_Slice.IndexOf(" B5FF") == -1 & Broth_Slice.IndexOf("BOUTMUX") == -1 & F5B == 0)
                                    F5B = 2;
                            }
                            ////////////////Checking the freedom of Bx & .... in brother's region/////////////////////
                            if (Broth_Slice.IndexOf("CX") == -1)
                            {
                                if (Broth_Slice.IndexOf(" CFF") == -1)
                                    FC = 1;
                                else if (Broth_Slice.IndexOf(" C5FF") == -1 & Broth_Slice.IndexOf("COUTMUX") == -1)
                                    F5C = 1;
                            }
                            if (Broth_Slice.IndexOf("C5LUT") == -1)
                            {
                                if (Broth_Slice.IndexOf(" CFF") == -1 & FC == 0)
                                    FC = 2;
                                else if (Broth_Slice.IndexOf(" C5FF") == -1 & Broth_Slice.IndexOf("COUTMUX") == -1 & F5C == 0)
                                    F5C = 2;
                            }
                            ////////////////Checking the freedom of CX & ......in brother's region////////////////////////
                            if (Broth_Slice.IndexOf("DX") == -1)
                            {
                                if (Broth_Slice.IndexOf(" DFF") == -1)
                                    FD = 1; 
                                else if (Broth_Slice.IndexOf(" D5FF") == -1 & Broth_Slice.IndexOf("DOUTMUX") == -1)
                                    F5D = 1;
                            }
                            if (Broth_Slice.IndexOf("D5LUT") == -1)
                            {
                                if (Broth_Slice.IndexOf(" DFF") == -1 & FD == 0)
                                    FD = 2;
                                else if (Broth_Slice.IndexOf(" D5FF") == -1 & Broth_Slice.IndexOf("DOUTMUX") == -1 & F5D == 0)
                                    F5D = 2;
                            }
                            ////////////////Checking the freedom of DX & .... in brother's region////////////////////////////////////
                            ///**********************************

                            string X = "";
                            if (SA == 1 & (FA == 1 || FB == 1 || FC == 1 || FD == 1))////FFA Dos not have history
                            {
                                SA = 24;
                                if (Int32.Parse(ROW) % 2 == 1)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS0";
                                    if (FA == 1)
                                    {
                                        BYP_B = "BYP_B1"; X = "AX"; FA = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 1b H " + "AFF " + "AX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 1Nb H " + "NAFF " + "NAX ");
                                    }
                                    else if (FB == 1)
                                    {
                                        BYP_B = "BYP_B4"; X = "BX"; FB = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 1b H " + "BFF " + "BX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 1Nb H " + "NBFF " + "NBX ");
                                    }
                                    else if (FC == 1)
                                    {
                                        BYP_B = "BYP_B3"; X = "CX"; FC = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 1b H " + "CFF " + "CX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 1Nb H " + "NCFF " + "NCX ");
                                    }
                                    else if (FD == 1)
                                    {
                                        BYP_B = "BYP_B6"; X = "DX"; FD = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 1b H " + "DFF " + "DX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 1Nb H " + "NDFF " + "NDX ");
                                    }
                                }
                                else if (Int32.Parse(ROW) % 2 == 0)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS4";
                                    if (FA == 1)
                                    {
                                        BYP_B = "BYP_B0"; X = "AX"; FA = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 1b H " + "AFF " + "AX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 1Nb H " + "NAFF " + "NAX ");
                                    }
                                    else if (FB == 1)
                                    {
                                        BYP_B = "BYP_B5"; X = "BX"; FB = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 1b H " + "BFF " + "BX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 1Nb H " + "NBFF " + "NBX ");
                                    }
                                    else if (FC == 1)
                                    {
                                        BYP_B = "BYP_B2"; X = "CX"; FC = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 1b H " + "CFF " + "CX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 1Nb H " + "NBFF " + "NBX ");
                                    }
                                    else if (FD == 1)
                                    {
                                        BYP_B = "BYP_B7"; X = "DX"; FD = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 1b H " + "DFF " + "DX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 1Nb H " + "NBFF " + "NBX ");
                                    }
                                }
                                History_NUM++;
                                inst_name = FLine.Substring(FLine.IndexOf("(") + 1, FLine.IndexOf(")") - FLine.IndexOf("(") - 1);
                                inst_name_H = Broth_Slice.Substring(Broth_Slice.IndexOf("(") + 1, Broth_Slice.IndexOf(")") - Broth_Slice.IndexOf("(") - 1);
                                func_net_rout(inst_name,inst_name_H,History_NUM, "AQ", X, CLB_Type, CLB_ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);         
                            }///// End of SA
                            if (SB == 2 & (FA == 1 || FB == 1 || FC == 1 || FD == 1))////FFB Dos not have history
                            {
                                SB = 26;
                                if (Int32.Parse(ROW) % 2 == 1)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS1";
                                    if (FA == 1)
                                    {
                                        BYP_B = "BYP_B1"; X = "AX"; FA = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 3b H " + "AFF " + "AX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 3Nb H " + "NAFF " + "NAX ");
                                    }
                                    else if (FB == 1)
                                    {
                                        BYP_B = "BYP_B4"; X = "BX"; FB = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 3b H " + "BFF " + "BX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 3Nb H " + "NBFF " + "NBX ");
                                    }
                                    else if (FC == 1)
                                    {
                                        BYP_B = "BYP_B3"; X = "CX"; FC = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 3b H " + "CFF " + "CX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 3Nb H " + "NAFF " + "NCX ");
                                    }
                                    else if (FD == 1)
                                    {
                                        BYP_B = "BYP_B6"; X = "DX"; FD = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 3b H " + "DFF " + "DX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 3Nb H " + "NDFF " + "NDX ");
                                    }
                                }
                                else if (Int32.Parse(ROW) % 2 == 0)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS5";
                                    if (FA == 1)
                                    {
                                        BYP_B = "BYP_B0"; X = "AX"; FA = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 3b H " + "AFF " + "AX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 3Nb H " + "NAFF " + "NAX ");
                                    }
                                    else if (FB == 1)
                                    {
                                        BYP_B = "BYP_B5"; X = "BX"; FB = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 3b H " + "BFF " + "BX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 3Nb H " + "NBFF " + "NBX ");
                                    }
                                    else if (FC == 1)
                                    {
                                        BYP_B = "BYP_B2"; X = "CX"; FC = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 3b H " + "CFF " + "CX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 3Nb H " + "NCFF " + "NCX ");
                                    }
                                    else if (FD == 1)
                                    {
                                        BYP_B = "BYP_B7"; X = "DX"; FD = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 3b H " + "DFF " + "DX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 3Nb H " + "NDFF " + "NDX ");
                                    }
                                }

                                History_NUM++;
                                inst_name = FLine.Substring(FLine.IndexOf("(") + 1, FLine.IndexOf(")") - FLine.IndexOf("(") - 1);
                                inst_name_H = Broth_Slice.Substring(Broth_Slice.IndexOf("(") + 1, Broth_Slice.IndexOf(")") - Broth_Slice.IndexOf("(") - 1);
                                func_net_rout(inst_name,inst_name_H,History_NUM, "BQ", X, CLB_Type, CLB_ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                            }///end of SB
                            if (SC == 3 & (FA == 1 || FB == 1 || FC == 1 || FD == 1))////FFC Dos not have history
                            {
                                SC = 28;
                                if (Int32.Parse(ROW) % 2 == 1)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS2";
                                    if (FA == 1)
                                    {
                                        BYP_B = "BYP_B1"; X = "AX"; FA = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 5b H " + "AFF " + "AX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 5Nb H " + "NAFF " + "NAX ");
                                    }
                                    else if (FB == 1)
                                    {
                                        BYP_B = "BYP_B4"; X = "BX"; FB = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 5b H " + "BFF " + "BX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 5Nb H " + "NBFF " + "NBX ");
                                    }
                                    else if (FC == 1)
                                    {
                                        BYP_B = "BYP_B3"; X = "CX"; FC = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 5b H " + "CFF " + "CX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 5Nb H " + "NCFF " + "NCX ");
                                    }
                                    else if (FD == 1)
                                    {
                                        BYP_B = "BYP_B6"; X = "DX"; FD = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 5b H " + "DFF " + "DX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 5Nb H " + "NDFF " + "NDX ");
                                    }
                                }
                                else if (Int32.Parse(ROW) % 2 == 0)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS6";
                                    if (FA == 1)
                                    {
                                        BYP_B = "BYP_B0"; X = "AX"; FA = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 5b H " + "AFF " + "AX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 5Nb H " + "NAFF " + "NAX ");
                                    }
                                    else if (FB == 1)
                                    {
                                        BYP_B = "BYP_B5"; X = "BX"; FB = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 5b H " + "BFF " + "BX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 5Nb H " + "NBFF " + "NBX ");
                                    }
                                    else if (FC == 1)
                                    {
                                        BYP_B = "BYP_B2"; X = "CX"; FC = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 5b H " + "CFF " + "CX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 5Nb H " + "NCFF " + "NCX ");
                                    }
                                    else if (FD == 1)
                                    {
                                        BYP_B = "BYP_B7"; X = "DX"; FD = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 5b H " + "DFF " + "DX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 5Nb H " + "NDFF " + "NDX ");
                                    }
                                }
                                History_NUM++;
                                inst_name = FLine.Substring(FLine.IndexOf("(") + 1, FLine.IndexOf(")") - FLine.IndexOf("(") - 1);
                                inst_name_H = Broth_Slice.Substring(Broth_Slice.IndexOf("(") + 1, Broth_Slice.IndexOf(")") - Broth_Slice.IndexOf("(") - 1);
                                func_net_rout(inst_name,inst_name_H,History_NUM, "CQ", X, CLB_Type, CLB_ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                            }//////end of SC
                            if (SD == 4 & (FA == 1 || FB == 1 || FC == 1 || FD == 1))////FFD Dos not have history
                            {
                                SD = 30;
                                if (Int32.Parse(ROW) % 2 == 1)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS3";
                                    if (FA == 1)
                                    {
                                        BYP_B = "BYP_B1"; X = "AX"; FA = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 7b H " + "AFF " + "AX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 7Nb H " + "NAFF " + "NAX ");
                                    }
                                    else if (FB == 1)
                                    {
                                        BYP_B = "BYP_B4"; X = "BX"; FB = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 7b H " + "BFF " + "BX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 7Nb H " + "NBFF " + "NBX ");
                                    }
                                    else if (FC == 1)
                                    {
                                        BYP_B = "BYP_B3"; X = "CX"; FC = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 7b H " + "CFF " + "CX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 7Nb H " + "NCFF " + "NCX ");
                                    }
                                    else if (FD == 1)
                                    {
                                        BYP_B = "BYP_B6"; X = "DX"; FD = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 7b H " + "DFF " + "DX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 7Nb H " + "NDFF " + "NDX ");
                                    }
                                }
                                else if (Int32.Parse(ROW) % 2 == 0)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS7";
                                    if (FA == 1)
                                    {
                                        BYP_B = "BYP_B0"; X = "AX"; FA = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 7b H " + "AFF " + "AX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 7Nb H " + "NAFF " + "NAX ");

                                    }
                                    else if (FB == 1)
                                    {
                                        BYP_B = "BYP_B5"; X = "BX"; FB = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 7b H " + "BFF " + "BX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 7Nb H " + "NBFF " + "NBX ");
                                    }
                                    else if (FC == 1)
                                    {
                                        BYP_B = "BYP_B2"; X = "CX"; FC = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 7b H " + "CFF " + "CX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 7Nb H " + "NCFF " + "NCX ");
                                    }
                                    else if (FD == 1)
                                    {
                                        BYP_B = "BYP_B7"; X = "DX"; FD = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 7b H " + "DFF " + "DX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 7Nb H " + "NDFF " + "NDX ");
                                    }
                                }
                                History_NUM++;
                                inst_name = FLine.Substring(FLine.IndexOf("(") + 1, FLine.IndexOf(")") - FLine.IndexOf("(") - 1);
                                inst_name_H = Broth_Slice.Substring(Broth_Slice.IndexOf("(") + 1, Broth_Slice.IndexOf(")") - Broth_Slice.IndexOf("(") - 1);
                                func_net_rout(inst_name,inst_name_H,History_NUM, "DQ", X, CLB_Type, CLB_ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                            }////end of SD
                            if (S5A == 5 & (FA == 1 || FB == 1 || FC == 1 || FD == 1))////FF5A Dos not have history
                            {
                                S5A = 32;
                                if (Int32.Parse(ROW) % 2 == 1)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS16";
                                    if (FA == 1)
                                    {
                                        BYP_B = "BYP_B1"; X = "AX"; FA = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 2b H " + "AFF " + "AX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 2Nb H " + "NAFF " + "NAX ");
                                    }
                                    else if (FB == 1)
                                    {
                                        BYP_B = "BYP_B4"; X = "BX"; FB = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 2b H " + "BFF " + "BX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 2Nb H " + "NBFF " + "NBX ");
                                    }
                                    else if (FC == 1)
                                    {
                                        BYP_B = "BYP_B3"; X = "CX"; FC = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 2b H " + "CFF " + "CX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 2Nb H " + "NCFF " + "NCX ");
                                    }
                                    else if (FD == 1)
                                    {
                                        BYP_B = "BYP_B6"; X = "DX"; FD = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 2b H " + "DFF " + "DX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 2Nb H " + "NDFF " + "NDX ");
                                    }
                                }
                                else if (Int32.Parse(ROW) % 2 == 0)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS20";
                                    if (FA == 1)
                                    {
                                        BYP_B = "BYP_B0"; X = "AX"; FA = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 2b H " + "AFF " + "AX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 2Nb H " + "NAFF " + "NAX ");
                                    }
                                    else if (FB == 1)
                                    {
                                        BYP_B = "BYP_B5"; X = "BX"; FB = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 2b H " + "BFF " + "BX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 2Nb H " + "NBFF " + "NBX ");
                                    }
                                    else if (FC == 1)
                                    {
                                        BYP_B = "BYP_B2"; X = "CX"; FC = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 2b H " + "CFF " + "CX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 2Nb H " + "NCFF " + "NCX ");
                                    }
                                    else if (FD == 1)
                                    {
                                        BYP_B = "BYP_B7"; X = "DX"; FD = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 2b H " + "DFF " + "DX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 2Nb H " + "NDFF " + "NDX ");
                                    }
                                }

                                History_NUM++;
                                inst_name = FLine.Substring(FLine.IndexOf("(") + 1, FLine.IndexOf(")") - FLine.IndexOf("(") - 1);
                                inst_name_H = Broth_Slice.Substring(Broth_Slice.IndexOf("(") + 1, Broth_Slice.IndexOf(")") - Broth_Slice.IndexOf("(") - 1);
                                func_net_rout(inst_name,inst_name_H,History_NUM, "AMUX", X, CLB_Type, CLB_ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                            }/// end of S5A
                            if (S5B == 6 & (FA == 1 || FB == 1 || FC == 1 || FD == 1))////FF5B Dos not have history
                            {
                                S5B = 34;
                                if (Int32.Parse(ROW) % 2 == 1)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS17";
                                    if (FA == 1)
                                    {
                                        BYP_B = "BYP_B1"; X = "AX"; FA = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 4b H " + "AFF " + "AX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 4Nb H " + "NAFF " + "NAX ");
                                    }
                                    else if (FB == 1)
                                    {
                                        BYP_B = "BYP_B4"; X = "BX"; FB = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 4b H " + "BFF " + "BX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 4Nb H " + "NBFF " + "NBX ");
                                    }
                                    else if (FC == 1)
                                    {
                                        BYP_B = "BYP_B3"; X = "CX"; FC = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 4b H " + "CFF " + "CX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 4Nb H " + "NCFF " + "NCX ");
                                    }
                                    else if (FD == 1)
                                    {
                                        BYP_B = "BYP_B6"; X = "DX"; FD = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 4b H " + "DFF " + "DX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 4Nb H " + "NDFF " + "NDX ");
                                    }
                                }
                                else if (Int32.Parse(ROW) % 2 == 0)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS21";
                                    if (FA == 1)
                                    {
                                        BYP_B = "BYP_B0"; X = "AX"; FA = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 4b H " + "AFF " + "AX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 4Nb H " + "NAFF " + "NAX ");

                                    }
                                    else if (FB == 1)
                                    {
                                        BYP_B = "BYP_B5"; X = "BX"; FB = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 4b H " + "BFF " + "BX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 4Nb H " + "NBFF " + "NBX ");
                                    }
                                    else if (FC == 1)
                                    {
                                        BYP_B = "BYP_B2"; X = "CX"; FC = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 4b H " + "CFF " + "CX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 4Nb H " + "NCFF " + "NCX ");
                                    }
                                    else if (FD == 1)
                                    {
                                        BYP_B = "BYP_B7"; X = "DX"; FD = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 4b H " + "DFF " + "DX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 4Nb H " + "NDFF " + "NDX ");
                                    }
                                }

                                History_NUM++;
                                inst_name = FLine.Substring(FLine.IndexOf("(") + 1, FLine.IndexOf(")") - FLine.IndexOf("(") - 1);
                                inst_name_H = Broth_Slice.Substring(Broth_Slice.IndexOf("(") + 1, Broth_Slice.IndexOf(")") - Broth_Slice.IndexOf("(") - 1);
                                func_net_rout(inst_name,inst_name_H,History_NUM, "BMUX", X, CLB_Type, CLB_ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                            }//////End of S5B
                            if (S5C == 7 & (FA == 1 || FB == 1 || FC == 1 || FD == 1))////FF5C Dos not have history
                            {
                                S5C = 36;
                                if (Int32.Parse(ROW) % 2 == 1)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS18";
                                    if (FA == 1)
                                    {
                                        BYP_B = "BYP_B1"; X = "AX"; FA = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 6b H " + "AFF " + "AX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 6Nb H " + "NAFF " + "NAX ");
                                    }
                                    else if (FB == 1)
                                    {
                                        BYP_B = "BYP_B4"; X = "BX"; FB = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 6b H " + "BFF " + "BX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 6Nb H " + "NBFF " + "NBX ");
                                    }
                                    else if (FC == 1)
                                    {
                                        BYP_B = "BYP_B3"; X = "CX"; FC = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 6b H " + "CFF " + "CX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 6Nb H " + "NCFF " + "NCX ");
                                    }
                                    else if (FD == 1)
                                    {
                                        BYP_B = "BYP_B6"; X = "DX"; FD = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 6b H " + "DFF " + "DX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 6Nb H " + "NDFF " + "NDX ");
                                    }
                                }
                                else if (Int32.Parse(ROW) % 2 == 0)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS22";
                                    if (FA == 1)
                                    {
                                        BYP_B = "BYP_B0"; X = "AX"; FA = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 6b H " + "AFF " + "AX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 6Nb H " + "NaFF " + "NaX ");
                                    }
                                    else if (FB == 1)
                                    {
                                        BYP_B = "BYP_B5"; X = "BX"; FB = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 6b H " + "BFF " + "BX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 6Nb H " + "NBFF " + "NBX ");
                                    }
                                    else if (FC == 1)
                                    {
                                        BYP_B = "BYP_B2"; X = "CX"; FC = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 6b H " + "CFF " + "CX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 6Nb H " + "NCFF " + "NCX ");
                                    }
                                    else if (FD == 1)
                                    {
                                        BYP_B = "BYP_B7"; X = "DX"; FD = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 6b H " + "DFF " + "DX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 6Nb H " + "NDFF " + "NDX ");
                                    }
                                }
                                History_NUM++;
                                inst_name = FLine.Substring(FLine.IndexOf("(") + 1, FLine.IndexOf(")") - FLine.IndexOf("(") - 1);
                                inst_name_H = Broth_Slice.Substring(Broth_Slice.IndexOf("(") + 1, Broth_Slice.IndexOf(")") - Broth_Slice.IndexOf("(") - 1);
                                func_net_rout(inst_name,inst_name_H,History_NUM, "CMUX", X, CLB_Type, CLB_ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                            }///////End of S5C
                            if (S5D == 8 & (FA == 1 || FB == 1 || FC == 1 || FD == 1))////FF5D Dos not have history
                            {
                                S5D = 38;
                                if (Int32.Parse(ROW) % 2 == 1)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS19";
                                    if (FA == 1)
                                    {
                                        BYP_B = "BYP_B1"; X = "AX"; FA = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 8b H " + "AFF " + "AX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 8Nb H " + "NAFF " + "NAX ");
                                    }
                                    else if (FB == 1)
                                    {
                                        BYP_B = "BYP_B4"; X = "BX"; FB = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 8b H " + "BFF " + "BX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 8Nb H " + "NBFF " + "NBX ");
                                    }
                                    else if (FC == 1)
                                    {
                                        BYP_B = "BYP_B3"; X = "CX"; FC = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 8b H " + "CFF " + "CX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 8Nb H " + "NCFF " + "NCX ");
                                    }
                                    else if (FD == 1)
                                    {
                                        BYP_B = "BYP_B6"; X = "DX"; FD = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 8b H " + "DFF " + "DX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 8Nb H " + "NDFF " + "NDX ");
                                    }
                                }
                                else if (Int32.Parse(ROW) % 2 == 0)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS23";
                                    if (FA == 1)
                                    {
                                        BYP_B = "BYP_B0"; X = "AX"; FA = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 8b H " + "AFF " + "AX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 8Nb H " + "NAFF " + "NAX ");
                                    }
                                    else if (FB == 1)
                                    {
                                        BYP_B = "BYP_B5"; X = "BX"; FB = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 8b H " + "BFF " + "BX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 8Nb H " + "NBFF " + "NBX ");
                                    }
                                    else if (FC == 1)
                                    {
                                        BYP_B = "BYP_B2"; X = "CX"; FC = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 8b H " + "CFF " + "CX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 8Nb H " + "NCFF " + "NCX ");
                                    }
                                    else if (FD == 1)
                                    {
                                        BYP_B = "BYP_B7"; X = "DX"; FD = 3;
                                        FLine = FLine.Insert(FLine.Length - 1, " 8b H " + "DFF " + "DX ");
                                        Broth_Slice = Broth_Slice.Insert(Broth_Slice.Length - 1, " 8Nb H " + "NDFF " + "NDX ");                                       
                                    }
                                }
                                History_NUM++;
                                inst_name = FLine.Substring(FLine.IndexOf("(") + 1, FLine.IndexOf(")") - FLine.IndexOf("(") - 1);
                                inst_name_H = Broth_Slice.Substring(Broth_Slice.IndexOf("(") + 1, Broth_Slice.IndexOf(")") - Broth_Slice.IndexOf("(") - 1);
                                func_net_rout(inst_name,inst_name_H,History_NUM, "DMUX", X, CLB_Type, CLB_ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                            }//////end of S5D
                            XDl_Final_Routing.WriteLine(FLine);
                        }//////end of cheking the availability of brother
                        ////////////if (brother != -1)
                        else
                        {
                            SLICE_Type = "L";
                            CLB_Type = "LL";
                            //jojo++;
                           // Console.Write(SA + " " + SB + " " + SC + " " + SD + " " + S5A + " " + S5B + " " + S5C + " " +S5D + " " + "\n");
                            HiS_INST_NUM++;
                            inst_name_H = " (REDUNANT" + HiS_INST_NUM + ")";
                            XDl_Final_Routing.WriteLine(FLine);
                            if (FLine.IndexOf("STL") != -1)
                                XDl_Final_Routing.Write(FLine.Substring(0, FLine.IndexOf("STL")) + "STL WOR" + row.ToString() + " " + COL + inst_name_H + "*");
                            else if (FLine.IndexOf("STM") != -1)
                                XDl_Final_Routing.Write(FLine.Substring(0, FLine.IndexOf("STM")) + "STL WOR" + row.ToString() + " " + COL + inst_name_H + "*");
                            inst_name_H = inst_name_H.Substring(inst_name_H.IndexOf("(") + 1, inst_name_H.IndexOf(")") - inst_name_H.IndexOf("(") - 1);
                            if (SA == 1)
                            {
                                if (Int32.Parse(ROW) % 2 == 1)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS0";
                                    BYP_B = "BYP_B1";
                                }
                                else if (Int32.Parse(ROW) % 2 == 0)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS4";
                                    BYP_B = "BYP_B0";
                                }

                                History_NUM++;
                                inst_name = FLine.Substring(FLine.IndexOf("(") + 1, FLine.IndexOf(")") - FLine.IndexOf("(") - 1);
                                func_net_rout(inst_name, inst_name_H, History_NUM, "AQ", "AX", CLB_Type, CLB_ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                                XDl_Final_Routing.Write(" 1 H " + "AFF " + "AX ");
                                SA = 24;
                            }///////end of if (SA == 1)
                            if (SB == 2)
                            {

                                if (Int32.Parse(ROW) % 2 == 1)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS1";
                                    BYP_B = "BYP_B4";
                                }
                                else if (Int32.Parse(ROW) % 2 == 0)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS5";
                                    BYP_B = "BYP_B5";
                                }

                                History_NUM++;
                                inst_name = FLine.Substring(FLine.IndexOf("(") + 1, FLine.IndexOf(")") - FLine.IndexOf("(") - 1);
                                func_net_rout(inst_name, inst_name_H,History_NUM, "BQ", "BX", CLB_Type, CLB_ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                                XDl_Final_Routing.Write(" 3 H " + "BFF " + "BX ");
                                SB = 26;
                            }//////end of if (SB == 2)
                            if (SC == 3)
                            {
                                if (Int32.Parse(ROW) % 2 == 1)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS2";
                                    BYP_B = "BYP_B3";
                                }
                                else if (Int32.Parse(ROW) % 2 == 0)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS6";
                                    BYP_B = "BYP_B2";
                                }

                                History_NUM++;
                                inst_name = FLine.Substring(FLine.IndexOf("(") + 1, FLine.IndexOf(")") - FLine.IndexOf("(") - 1);
                                func_net_rout(inst_name, inst_name_H,History_NUM, "CQ", "CX", CLB_Type, CLB_ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                                XDl_Final_Routing.Write(" 5 H " + "CFF " + "CX ");
                                SC = 28;
                            }///////end of if (SC == 3)
                            if (SD == 4)
                            {
                                if (Int32.Parse(ROW) % 2 == 1)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS3";
                                    BYP_B = "BYP_B6";
                                }
                                else if (Int32.Parse(ROW) % 2 == 0)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS7";
                                    BYP_B = "BYP_B7";
                                }

                                History_NUM++;
                                inst_name = FLine.Substring(FLine.IndexOf("(") + 1, FLine.IndexOf(")") - FLine.IndexOf("(") - 1);
                                func_net_rout(inst_name, inst_name_H,History_NUM, "DQ", "DX", CLB_Type, CLB_ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                                XDl_Final_Routing.Write(" 7 H " + "DFF " + "DX ");
                                SD = 30;
                            }//////end of if (SD == 4)
                            if (S5A == 5)
                            {
                                if (Int32.Parse(ROW) % 2 == 1)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS16";
                                    BYP_B = "IMUX_B21";
                                }
                                else if (Int32.Parse(ROW) % 2 == 0)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS20";
                                    BYP_B = "IMUX_B20";
                                }

                                History_NUM++;
                                inst_name = FLine.Substring(FLine.IndexOf("(") + 1, FLine.IndexOf(")") - FLine.IndexOf("(") - 1);
                                func_net_rout(inst_name, inst_name_H,History_NUM, "AMUX", "A1", CLB_Type, CLB_ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                                XDl_Final_Routing.Write(" 2 H " + "A5LUT " + "A5FF ");
                                S5A = 32;
                            }///// end of if (S5A == 5)
                            if (S5B == 6)
                            {
                                if (Int32.Parse(ROW) % 2 == 1)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS17";
                                    BYP_B = "IMUX_B33";
                                }
                                else if (Int32.Parse(ROW) % 2 == 0)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS21";
                                    BYP_B = "IMUX_B32";
                                }

                                History_NUM++;
                                inst_name = FLine.Substring(FLine.IndexOf("(") + 1, FLine.IndexOf(")") - FLine.IndexOf("(") - 1);
                                func_net_rout(inst_name, inst_name_H,History_NUM, "BMUX", "B1", CLB_Type, CLB_ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                                XDl_Final_Routing.Write(" 4 H " + "B5LUT " + "B5FF ");
                                S5B = 34;
                            }//////end of if (S5B == 6)
                            if (S5C == 7)
                            {
                                if (Int32.Parse(ROW) % 2 == 1)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS18";
                                    BYP_B = "IMUX_B17";
                                }
                                else if (Int32.Parse(ROW) % 2 == 0)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS22";
                                    BYP_B = "IMUX_B16";
                                }

                                History_NUM++;
                                inst_name = FLine.Substring(FLine.IndexOf("(") + 1, FLine.IndexOf(")") - FLine.IndexOf("(") - 1);
                                func_net_rout(inst_name, inst_name_H,History_NUM, "CMUX", "C1", CLB_Type, CLB_ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                                XDl_Final_Routing.Write(" 6 H " + "C5LUT " + "C5FF ");
                                S5C = 36;
                            }//////end of if (S5C == 7)
                            if (S5D == 8)
                            {
                                if (Int32.Parse(ROW) % 2 == 1)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS19";
                                    BYP_B = "IMUX_B37";
                                }
                                else if (Int32.Parse(ROW) % 2 == 0)
                                {
                                    LOGIC_OUTS = "LOGIC_OUTS23";
                                    BYP_B = "IMUX_B36";
                                }

                                History_NUM++;
                                inst_name = FLine.Substring(FLine.IndexOf("(") + 1, FLine.IndexOf(")") - FLine.IndexOf("(") - 1);
                                func_net_rout(inst_name, inst_name_H,History_NUM, "DMUX", "D1", CLB_Type, CLB_ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                                XDl_Final_Routing.Write(" 8 H " + "D5LUT " + "D5FF ");
                                S5D = 38;
                            }////end of if (S5D == 8)
                            XDl_Final_Routing.Write("\n");
                        }///////End of else (brother != -1)
                    }//////End of checking the availability of free FFs 
                    ///////if (SA == 1 || SB == 2 || SC == 3 || SD == 4 || S5A == 5 || S5B == 6 || S5C == 7 || S5D == 8)                          
                    else { XDl_Final_Routing.WriteLine(FLine); }
                }//////End of the main else if whom check the availability of * in the FLine
            }//////End of The While
            XDl_Final_Routing.Close();
            FinalRouting.Close();
        }      
        /// <summary>
        /// Read from temp s953 file and manage the decision
        /// </summary>
        /// <param name="XDLr"></param>
        public static void Find_histrory(StreamReader XDLr,StreamWriter XDLrout)
        {
            string argument = "";
            int state = 0;
            string XDLrline = "";

            //Read contents of file into a string
            Stream Tmprouting;
            Tmprouting = File.OpenWrite(@"F:\MS\FinalProject\CODE\Tmprouting.XDL");
            StreamWriter XDLtr = new StreamWriter(Tmprouting);

            while (XDLr.EndOfStream == false)
            {
                XDLrline = XDLr.ReadLine();

                if (XDLrline.Equals("") || XDLrline[0] == '\n')
                    continue;

                if (state != 0)
                    argument = argument + XDLrline;

                if (XDLrline.IndexOf(';') != -1)
                {

                    switch (state)
                    {
                        case 1:
                            Find(ref argument,XDLrout);
                            XDLtr.Write(argument+"\n");//the new routing is written
                            break;
                        case 2:
                            Find(ref argument,XDLrout);
                            XDLtr.Write(argument+"\n");// the new routing is written
                            break;
                        case 3:
                           // func_net(argument);
                            break;
                    }

                    state = 0;
                }

                if (XDLrline.IndexOf("LM") != -1)
                {
                    state = 1;
                    argument = XDLrline;
                }
                else if (XDLrline.IndexOf("LL") != -1)
                {
                    state = 2;
                    argument = XDLrline;
                }
                else if (XDLrline.IndexOf("BRAM") != -1)
                {
                    state = 3;
                    argument = XDLrline;
                }

            }
            XDLtr.Close();
            Tmprouting.Close();
        }

        /// <summary>
        /// Find the history
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="XDLr"></param>
        private static void Find(ref string argument,StreamWriter XDLrout)
        {
            int reA = 0;
            int reB = 0;
            int reC = 0;
            //Region A
            if (argument.IndexOf(" AFF") != -1 & argument.IndexOf(" A5FF") == -1 & argument.IndexOf("AOUTMUX") == -1 & argument.IndexOf("AX") == -1)
            {
                func_routing1("A",ref argument, XDLrout);
                argument = argument.Insert(argument.IndexOf(";"), " 1 H " + "A5FF " + "AOUTMUX " + "AX ");  
            }
                //This situtation is so rare and in s9532 for virtex 6 not happend but we manage it
            else if (argument.IndexOf(" AFF") == -1 & argument.IndexOf(" A5FF") != -1 & argument.IndexOf("AX") == -1)
            {
                func_routing2("A",ref argument, XDLrout);
                argument = argument.Insert(argument.IndexOf(";"), " 2 H " + "AFF " + "AX ");
            }
                //Both FF and 5FF are used in the region or we could not use 5FF or FF for history for another
            else if (argument.IndexOf(" AFF") != -1 & argument.IndexOf(" A5FF") != -1)
            {
                
               if(argument.IndexOf("2 H") == -1) func_routing2Ap(ref argument, XDLrout); 
                reA = func_routing1Ap(ref argument, XDLrout, 0);
                 
            }
            
            //Region B
            if (argument.IndexOf(" BFF") != -1 & argument.IndexOf(" B5FF") == -1 & argument.IndexOf("BOUTMUX") == -1 & argument.IndexOf("BX") == -1)
            {
                func_routing1("B",ref  argument, XDLrout);
                argument = argument.Insert(argument.IndexOf(";"), " 3 H " + "B5FF " + "BOUTMUX " + "BX ");
            }
            //This situtation is so rare and in s9532 for virtex 6 not happend but we manage it
            else if (argument.IndexOf(" BFF") == -1 & argument.IndexOf(" B5FF") != -1 & argument.IndexOf("BX") == -1)
            {
                func_routing2("B",ref argument, XDLrout);
                argument = argument.Insert(argument.IndexOf(";"), " 4 H " + "BFF " + "BX ");
            }
            else if (argument.IndexOf(" BFF") != -1 & argument.IndexOf(" B5FF") != -1)
            {
                if (argument.IndexOf("4 H") == -1) func_routing2Bp(ref argument, XDLrout);  
                if(reA == 0) reB = func_routing1Bp(ref argument, XDLrout, 0);   
            }
            
            //Region C
            if (argument.IndexOf(" CFF") != -1 & argument.IndexOf(" C5FF") == -1 & argument.IndexOf("COUTMUX") == -1 & argument.IndexOf("CX") == -1)
            {
                func_routing1("C",ref  argument, XDLrout);
                argument = argument.Insert(argument.IndexOf(";"), " 5 H " + "C5FF " + "COUTMUX " + "CX ");  
            }
            //This situtation is so rare and in s9532 for virtex 6 not happend but we manage it
            else if (argument.IndexOf(" CFF") == -1 & argument.IndexOf(" C5FF") != -1 & argument.IndexOf("CX") == -1)
            {
                func_routing2("C",ref argument, XDLrout);
                argument = argument.Insert(argument.IndexOf(";"), " 6 H " + "CFF " + "CX ");
            }
            else if (argument.IndexOf(" CFF") != -1 & argument.IndexOf(" C5FF") != -1)
            {
                if (argument.IndexOf("6 H") == -1) func_routing2Cp(ref argument, XDLrout); 
                if(reB == 0 & reA == 0)  reC = func_routing1Cp(ref argument, XDLrout, 0);  
            }
            
            //Region D
            if (argument.IndexOf(" DFF") != -1 & argument.IndexOf(" D5FF") == -1 & argument.IndexOf("DOUTMUX") == -1 & argument.IndexOf("DX") == -1)
            {
                func_routing1("D",ref  argument, XDLrout);
                argument = argument.Insert(argument.IndexOf(";"), " 7 H " + "D5FF " + "DOUTMUX " + "DX ");
            }
            //This situtation is so rare and in s9532 for virtex 6 not happend but we manage it
            else if (argument.IndexOf(" DFF") == -1 & argument.IndexOf(" D5FF") != -1 & argument.IndexOf("DX") == -1)
            {
                func_routing2("D",ref argument, XDLrout);
                argument = argument.Insert(argument.IndexOf(";"), " 8 H " + "DFF " + "DX ");
            }
            else if (argument.IndexOf(" DFF") != -1 & argument.IndexOf(" D5FF") != -1)
            {
                if (argument.IndexOf("8 H") == -1) func_routing2Dp(ref argument, XDLrout);
                if(reA == 0 & reB == 0 & reC == 0) func_routing1Dp(ref argument, XDLrout, 0);
            }
            ///////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////
            if (argument.IndexOf(" AFF") != -1 & argument.IndexOf(" A5FF") == -1 & (argument.IndexOf("AOUTMUX") != -1 || argument.IndexOf("AX") != -1))
            {
                if ( argument.IndexOf("H",argument.IndexOf(" AFF") - 1,1) == -1)// If AFF isn't history itself
                func_routing1Ap(ref argument, XDLrout, 1);
            }
            else if (argument.IndexOf(" AFF") == -1 & argument.IndexOf(" A5FF") != -1 & argument.IndexOf("AX") != -1)
            {
                if (argument.IndexOf("H", argument.IndexOf(" A5FF") - 1,1) == -1)// If A5FF isn't history itself
                func_routing2Ap(ref argument, XDLrout);
            }
            //
            if (argument.IndexOf(" BFF") != -1 & argument.IndexOf(" B5FF") == -1 & (argument.IndexOf("BOUTMUX") != -1 || argument.IndexOf("BX") != -1))
            {                
                if (argument.IndexOf("H", argument.IndexOf(" BFF") - 1,1) == -1)
                func_routing1Bp(ref argument, XDLrout,1);
            }
            else if (argument.IndexOf(" BFF") == -1 & argument.IndexOf(" B5FF") != -1 & argument.IndexOf("BX") != -1)
            {
                if (argument.IndexOf("H", argument.IndexOf(" B5FF") - 1,1) == -1)
                func_routing2Bp(ref argument, XDLrout);
            }
            //
            if (argument.IndexOf(" CFF") != -1 & argument.IndexOf(" C5FF") == -1 & (argument.IndexOf("COUTMUX") != -1 || argument.IndexOf("CX") != -1))
            {
                if (argument.IndexOf("H", argument.IndexOf(" CFF") - 1,1) == -1)
                func_routing1Cp(ref argument, XDLrout,1);
            }
            else if (argument.IndexOf(" CFF") == -1 & argument.IndexOf(" C5FF") != -1 & argument.IndexOf("CX") != -1)
            {
                if (argument.IndexOf("H", argument.IndexOf(" C5FF") - 1,1) == -1)
                func_routing2Cp(ref argument, XDLrout);
            }
            //
            if (argument.IndexOf(" DFF") != -1 & argument.IndexOf(" D5FF") == -1 & (argument.IndexOf("DOUTMUX") != -1 || argument.IndexOf("DX") != -1))
            {
                if (argument.IndexOf("H", argument.IndexOf(" DFF") - 1,1) == -1)
                func_routing1Dp(ref argument, XDLrout,1);
            }
            else if (argument.IndexOf(" DFF") == -1 & argument.IndexOf(" D5FF") != -1 & argument.IndexOf("DX") != -1)
            {
                if (argument.IndexOf("H", argument.IndexOf(" D5FF") - 1,1) == -1)
                func_routing2Dp(ref argument, XDLrout);
            }
        }

        private static void func_routing2Dp(ref string argument, StreamWriter XDLrout)
        {
            int CLB_type_index = 0;
            int SLICE_Type_index = 0;
            int Row_index = 0;
            int Col_index = 0;
            int NxtEnt_index = 0;
            int ROWslice_index = 0;
            
            string LOGIC_OUTS = "";
            string BYP_B = "";
            string SLICE_Type = "";
            string CLB_Type = "";
            string ROW = "";
            string COL = "";
            string ROW_SLICE = "";
            string inst_name = "";
            string inst_name_H = "";

            CLB_type_index = argument.IndexOf("CT");
            SLICE_Type_index = argument.IndexOf("ST");
            Row_index = argument.IndexOf("ROW");
            Col_index = argument.IndexOf("COL");
            NxtEnt_index = argument.IndexOf("\n");
            ROWslice_index = argument.IndexOf("WOR");

            inst_name = argument.Substring(argument.IndexOf("(") + 1, argument.IndexOf(")") - argument.IndexOf("(") - 1);
            inst_name_H = inst_name;
            SLICE_Type = argument.Substring(SLICE_Type_index + 2, 1);
            CLB_Type = argument.Substring(CLB_type_index + 2, 2);
            ROW = argument.Substring(Row_index + 3, argument.IndexOf(" ", Row_index) - Row_index - 3);
            COL = argument.Substring(Col_index + 3, argument.IndexOf(" ", Col_index) - Col_index - 3);
            ROW_SLICE = argument.Substring(ROWslice_index + 3, argument.IndexOf(" ", ROWslice_index) - ROWslice_index - 3);



            if (argument.IndexOf("AX") == -1 & argument.IndexOf(" AFF") == -1 & argument.IndexOf(" A5FF") == -1)
            {
                   if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS19";
                        BYP_B = "BYP_B0";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS23";
                        BYP_B = "BYP_B1";
                    }

                    History_NUM++;

                    func_net_rout(inst_name,inst_name_H,History_NUM, "DMUX", "AX", CLB_Type, ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                    argument = argument.Insert(argument.IndexOf(";"), " 8 H " + "AFF " + "AX ");
            }
            //End of region A

            else if (argument.IndexOf("BX") == -1 & argument.IndexOf(" BFF") == -1 & argument.IndexOf(" B5FF") == -1 )
            {
                   if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS19";
                        BYP_B = "BYP_B5";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS23";
                        BYP_B = "BYP_B4";
                    }

                    History_NUM++;

                    func_net_rout(inst_name,inst_name_H,History_NUM, "DMUX", "BX", CLB_Type, ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                    argument = argument.Insert(argument.IndexOf(";"), " 8 H " + "BFF " + "BX ");                
            }
            //End of Region B
            else if (argument.IndexOf("CX") == -1 & argument.IndexOf(" CFF") == -1 & argument.IndexOf(" C5FF") == -1)
            {
                    if ( Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS19";
                        BYP_B = "BYP_B2";
                    }
                    else if ( Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS23";
                        BYP_B = "BYP_B3";
                    }

                    History_NUM++;

                    func_net_rout(inst_name,inst_name_H,History_NUM, "DMUX", "CX", CLB_Type, ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                    argument = argument.Insert(argument.IndexOf(";"), " 8 H " + "CFF " + "CX ");
                }
            else
            {
                ;
            }   
        }

        private static void func_routing1Dp(ref string argument, StreamWriter XDLrout,int Def)
        {
            int CLB_type_index = 0;
            int SLICE_Type_index = 0;
            int Row_index = 0;
            int Col_index = 0;
            int NxtEnt_index = 0;
            int ROWslice_index = 0;

            string LOGIC_OUTS = "";
            string BYP_B = "";
            string SLICE_Type = "";
            string CLB_Type = "";
            string ROW = "";
            string COL = "";
            string ROW_SLICE = "";
            string inst_name = "";
            string inst_name_H = "";

            CLB_type_index = argument.IndexOf("CT");
            SLICE_Type_index = argument.IndexOf("ST");
            Row_index = argument.IndexOf("ROW");
            Col_index = argument.IndexOf("COL");
            NxtEnt_index = argument.IndexOf("\n");
            ROWslice_index = argument.IndexOf("WOR");

            inst_name = argument.Substring(argument.IndexOf("(") + 1, argument.IndexOf(")") - argument.IndexOf("(") - 1);
            inst_name_H = inst_name;
            SLICE_Type = argument.Substring(SLICE_Type_index + 2, 1);
            CLB_Type = argument.Substring(CLB_type_index + 2, 2);
            ROW = argument.Substring(Row_index + 3, argument.IndexOf(" ", Row_index) - Row_index - 3);
            COL = argument.Substring(Col_index + 3, argument.IndexOf(" ", Col_index) - Col_index - 3);
            ROW_SLICE = argument.Substring(ROWslice_index + 3, argument.IndexOf(" ", ROWslice_index) - ROWslice_index - 3);



            if (argument.IndexOf("AX") == -1 & argument.IndexOf(" AFF") == -1 & argument.IndexOf(" A5FF") == -1 & argument.IndexOf("AOUTMUX") == -1)
            {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS3";
                        BYP_B = "BYP_B0";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS7";
                        BYP_B = "BYP_B1";
                    }
                    History_NUM++;
                    func_net_rout(inst_name, inst_name_H, History_NUM, "DQ", "AX", CLB_Type, ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                    argument = argument.Insert(argument.IndexOf(";"), " 7 H " + "A5FF " + "AOUTMUX " + "AX ");               
            }
            //End of region A

            else if (argument.IndexOf("BX") == -1 & argument.IndexOf(" BFF") == -1 & argument.IndexOf(" B5FF") == -1 & argument.IndexOf("BOUTMUX") == -1)
            {
                    if ( Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS3";
                        BYP_B = "BYP_B5";
                    }
                    else if ( Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS7";
                        BYP_B = "BYP_B4";
                    }

                    History_NUM++;
                    func_net_rout(inst_name, inst_name_H, History_NUM, "DQ", "BX", CLB_Type, ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                    argument = argument.Insert(argument.IndexOf(";"), " 7 H " + "B5FF " + "BOUTMUX " + "BX ");                
            }
            //End of Region B
            else if (argument.IndexOf("CX") == -1 & argument.IndexOf(" CFF") == -1 & argument.IndexOf(" C5FF") == -1 & argument.IndexOf("COUTMUX") == -1)
            {
                    if ( Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS3";
                        BYP_B = "BYP_B2";
                    }
                    else if ( Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS7";
                        BYP_B = "BYP_B3";
                    }

                    History_NUM++;
                    func_net_rout(inst_name, inst_name_H, History_NUM, "DQ", "CX", CLB_Type, ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout); 
                    argument = argument.Insert(argument.IndexOf(";"), " 7 H " + "C5FF " + "COUTMUX " + "CX ");
                }
            else
            {
                if (Def == 0) func_Dif_rout4(ref argument, XDLrout, ROW_SLICE, COL);
            }      
        }

        private static void func_routing2Cp(ref string argument, StreamWriter XDLrout)
        {
            int CLB_type_index = 0;
            int SLICE_Type_index = 0;
            int Row_index = 0;
            int Col_index = 0;
            int NxtEnt_index = 0;
            int ROWslice_index = 0;

            string LOGIC_OUTS = "";
            string BYP_B = "";
            string SLICE_Type = "";
            string CLB_Type = "";
            string ROW = "";
            string COL = "";
            string ROW_SLICE = "";
            string inst_name = "";
            string inst_name_H = "";

            CLB_type_index = argument.IndexOf("CT");
            SLICE_Type_index = argument.IndexOf("ST");
            Row_index = argument.IndexOf("ROW");
            Col_index = argument.IndexOf("COL");
            NxtEnt_index = argument.IndexOf("\n");
            ROWslice_index = argument.IndexOf("WOR");

            SLICE_Type = argument.Substring(SLICE_Type_index + 2, 1);
            CLB_Type = argument.Substring(CLB_type_index + 2, 2);
            ROW = argument.Substring(Row_index + 3, argument.IndexOf(" ", Row_index) - Row_index - 3);
            COL = argument.Substring(Col_index + 3, argument.IndexOf(" ", Col_index) - Col_index - 3);
            ROW_SLICE = argument.Substring(ROWslice_index + 3, argument.IndexOf(" ", ROWslice_index) - ROWslice_index - 3);
            inst_name = argument.Substring(argument.IndexOf("(") + 1, argument.IndexOf(")") - argument.IndexOf("(") - 1);
            inst_name_H = inst_name;


            if (argument.IndexOf("AX") == -1 & argument.IndexOf(" AFF") == -1 & argument.IndexOf(" A5FF") == -1)
            {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS18";
                        BYP_B = "BYP_B0";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS22";
                        BYP_B = "BYP_B1";
                    }

                    History_NUM++;

                    func_net_rout(inst_name,inst_name_H,History_NUM, "CMUX", "AX", CLB_Type, ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                    argument = argument.Insert(argument.IndexOf(";"), " 6 H " + "AFF " + "AX ");                
            }
            //End of region A

            else if (argument.IndexOf("BX") == -1 & argument.IndexOf(" BFF") == -1 & argument.IndexOf(" B5FF") == -1)
            {
                    if ( Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS18";
                        BYP_B = "BYP_B5";
                    }
                    else if ( Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS22";
                        BYP_B = "BYP_B4";
                    }

                    History_NUM++;
                    func_net_rout(inst_name, inst_name_H, History_NUM, "CMUX", "BX", CLB_Type, ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                    argument = argument.Insert(argument.IndexOf(";"), " 6 H " + "BFF " + "BX ");               
            }
            //End of Region B
            else if (argument.IndexOf("DX") == -1 & argument.IndexOf(" DFF") == -1 & argument.IndexOf(" D5FF") == -1 )
            {
                    if ( Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS18";
                        BYP_B = "BYP_B7";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS22";
                        BYP_B = "BYP_B6";
                    }

                    History_NUM++;
                    func_net_rout(inst_name, inst_name_H, History_NUM, "CMUX", "DX", CLB_Type, ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                    argument = argument.Insert(argument.IndexOf(";"), " 6 H " + "DFF " + "DX ");           
            }
            else
            {
                ;
            }
        }

        private static int func_routing1Cp(ref string argument, StreamWriter XDLrout,int Def)
        {
            int CLB_type_index = 0;
            int SLICE_Type_index = 0;
            int Row_index = 0;
            int Col_index = 0;
            int NxtEnt_index = 0;
            int ROWslice_index = 0;

            string LOGIC_OUTS = "";
            string BYP_B = "";
            string SLICE_Type = "";
            string CLB_Type = "";
            string ROW = "";
            string COL = "";
            string ROW_SLICE = "";
            string inst_name = "";
            string inst_name_H = "";

            CLB_type_index = argument.IndexOf("CT");
            SLICE_Type_index = argument.IndexOf("ST");
            Row_index = argument.IndexOf("ROW");
            Col_index = argument.IndexOf("COL");
            NxtEnt_index = argument.IndexOf("\n");
            ROWslice_index = argument.IndexOf("WOR");

            SLICE_Type = argument.Substring(SLICE_Type_index + 2, 1);
            CLB_Type = argument.Substring(CLB_type_index + 2, 2);
            ROW = argument.Substring(Row_index + 3, argument.IndexOf(" ", Row_index) - Row_index - 3);
            COL = argument.Substring(Col_index + 3, argument.IndexOf(" ", Col_index) - Col_index - 3);
            ROW_SLICE = argument.Substring(ROWslice_index + 3, argument.IndexOf(" ", ROWslice_index) - ROWslice_index - 3);
            inst_name = argument.Substring(argument.IndexOf("(") + 1, argument.IndexOf(")") - argument.IndexOf("(") - 1);
            inst_name_H = inst_name;


            if (argument.IndexOf("AX") == -1 & argument.IndexOf(" AFF") == -1 & argument.IndexOf(" A5FF") == -1 & argument.IndexOf("AOUTMUX") == -1)
            {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS2";
                        BYP_B = "BYP_B0";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS6";
                        BYP_B = "BYP_B1";
                    }

                    History_NUM++;

                    func_net_rout(inst_name, inst_name_H, History_NUM, "CQ", "AX", CLB_Type, ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                    argument = argument.Insert(argument.IndexOf(";"), " 5 H " + "A5FF " + "AOUTMUX " + "AX ");
                    return 0;
            }
            //End of region A

            else if (argument.IndexOf("BX") == -1 & argument.IndexOf(" BFF") == -1 & argument.IndexOf(" B5FF") == -1 & argument.IndexOf("BOUTMUX") == -1)
            {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS2";
                        BYP_B = "BYP_B5";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS6";
                        BYP_B = "BYP_B4";
                    }

                    History_NUM++;

                    func_net_rout(inst_name, inst_name_H, History_NUM, "CQ", "BX", CLB_Type, ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                    argument = argument.Insert(argument.IndexOf(";"), " 5 H " + "B5FF " + "BOUTMUX " + "BX ");
                    return 0;
            }
            //End of Region B
            else if (argument.IndexOf("DX") == -1 & argument.IndexOf(" DFF") == -1 & argument.IndexOf(" D5FF") == -1 & argument.IndexOf("DOUTMUX") == -1)
            {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS2";
                        BYP_B = "BYP_B7";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS6";
                        BYP_B = "BYP_B6";
                    }

                    History_NUM++;

                    func_net_rout(inst_name, inst_name_H, History_NUM, "CQ", "DX", CLB_Type, ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                    argument = argument.Insert(argument.IndexOf(";"), " 5 H " + "D5FF " + "DOUTMUX " + "DX ");
                    return 0;
            }
            else
            {
                if(Def == 0) func_Dif_rout3(ref argument, XDLrout, ROW_SLICE, COL);
                return 1;
            }
        }

        private static void func_routing2Bp(ref string argument, StreamWriter XDLrout)
        {
            int CLB_type_index = 0;
            int SLICE_Type_index = 0;
            int Row_index = 0;
            int Col_index = 0;
            int NxtEnt_index = 0;
            int ROWslice_index = 0;

            string LOGIC_OUTS = "";
            string BYP_B = "";
            string SLICE_Type = "";
            string CLB_Type = "";
            string ROW = "";
            string COL = "";
            string ROW_SLICE = "";
            string inst_name = "";
            string inst_name_H = "";

            CLB_type_index = argument.IndexOf("CT");
            SLICE_Type_index = argument.IndexOf("ST");
            Row_index = argument.IndexOf("ROW");
            Col_index = argument.IndexOf("COL");
            NxtEnt_index = argument.IndexOf("\n");
            ROWslice_index = argument.IndexOf("WOR");

            SLICE_Type = argument.Substring(SLICE_Type_index + 2, 1);
            CLB_Type = argument.Substring(CLB_type_index + 2, 2);
            ROW = argument.Substring(Row_index + 3, argument.IndexOf(" ", Row_index) - Row_index - 3);
            COL = argument.Substring(Col_index + 3, argument.IndexOf(" ", Col_index) - Col_index - 3);
            ROW_SLICE = argument.Substring(ROWslice_index + 3, argument.IndexOf(" ", ROWslice_index) - ROWslice_index - 3);
            inst_name = argument.Substring(argument.IndexOf("(") + 1, argument.IndexOf(")") - argument.IndexOf("(") - 1);
            inst_name_H = inst_name;

            if (argument.IndexOf("AX") == -1 & argument.IndexOf(" AFF") == -1 & argument.IndexOf(" A5FF") == -1 )
            {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS17";
                        BYP_B = "BYP_B0";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS21";
                        BYP_B = "BYP_B1";
                    }

                    History_NUM++;

                    func_net_rout(inst_name,inst_name_H,History_NUM, "BMUX", "AX", CLB_Type, ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                    argument = argument.Insert(argument.IndexOf(";"), " 4 H " + "AFF " + "AX ");                
            }
            //End of region A

            else if (argument.IndexOf("CX") == -1 & argument.IndexOf(" CFF") == -1 & argument.IndexOf(" C5FF") == -1)
            {
                    if ( Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS17";
                        BYP_B = "BYP_B2";
                    }
                    else if ( Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS21";
                        BYP_B = "BYP_B3";
                    }

                    History_NUM++;
                    func_net_rout(inst_name, inst_name_H, History_NUM, "BMUX", "CX", CLB_Type, ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                    argument = argument.Insert(argument.IndexOf(";"), " 4 H " + "CFF " + "CX ");
            }
            //End of Region C
            else if (argument.IndexOf("DX") == -1 & argument.IndexOf(" DFF") == -1 & argument.IndexOf(" D5FF") == -1)
            {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS17";
                        BYP_B = "BYP_B7";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS21";
                        BYP_B = "BYP_B6";
                    }

                    History_NUM++;
                    func_net_rout(inst_name, inst_name_H, History_NUM, "BMUX", "DX", CLB_Type, ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                    argument = argument.Insert(argument.IndexOf(";"), " 4 H " + "DFF " + "DX ");               
            }
            else
            {
                ;
            }
        }

        private static int func_routing1Bp(ref string argument, StreamWriter XDLrout,int Def)
        {
            int CLB_type_index = 0;
            int SLICE_Type_index = 0;
            int Row_index = 0;
            int Col_index = 0;
            int NxtEnt_index = 0;
            int ROWslice_index = 0;

            string LOGIC_OUTS = "";
            string BYP_B = "";
            string SLICE_Type = "";
            string CLB_Type = "";
            string ROW = "";
            string COL = "";
            string ROW_SLICE = "";
            string inst_name = "";
            string inst_name_H = "";

            CLB_type_index = argument.IndexOf("CT");
            SLICE_Type_index = argument.IndexOf("ST");
            Row_index = argument.IndexOf("ROW");
            Col_index = argument.IndexOf("COL");
            NxtEnt_index = argument.IndexOf("\n");
            ROWslice_index = argument.IndexOf("WOR");

            SLICE_Type = argument.Substring(SLICE_Type_index + 2, 1);
            CLB_Type = argument.Substring(CLB_type_index + 2, 2);
            ROW = argument.Substring(Row_index + 3, argument.IndexOf(" ", Row_index) - Row_index - 3);
            COL = argument.Substring(Col_index + 3, argument.IndexOf(" ", Col_index) - Col_index - 3);
            ROW_SLICE = argument.Substring(ROWslice_index + 3, argument.IndexOf(" ", ROWslice_index) - ROWslice_index - 3);
            inst_name = argument.Substring(argument.IndexOf("(") + 1, argument.IndexOf(")") - argument.IndexOf("(") - 1);
            inst_name_H = inst_name;


            if (argument.IndexOf("AX") == -1 & argument.IndexOf(" AFF") == -1 & argument.IndexOf(" A5FF") == -1 & argument.IndexOf("AOUTMUX") == -1)
            {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS1";
                        BYP_B = "BYP_B0";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS5";
                        BYP_B = "BYP_B1";
                    }

                    History_NUM++;
                    func_net_rout(inst_name, inst_name_H, History_NUM, "BQ", "AX", CLB_Type, ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                    argument = argument.Insert(argument.IndexOf(";"), " 3 H " + "A5FF " + "AOUTMUX " + "AX ");
                    return 0;
            }
            //End of region A

            else if (argument.IndexOf("CX") == -1 & argument.IndexOf(" CFF") == -1 & argument.IndexOf(" C5FF") == -1 & argument.IndexOf("COUTMUX") == -1)
            {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS1";
                        BYP_B = "BYP_B2";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS5";
                        BYP_B = "BYP_B3";
                    }

                    History_NUM++;

                    func_net_rout(inst_name,inst_name_H, History_NUM, "BQ", "CX", CLB_Type, ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                    argument = argument.Insert(argument.IndexOf(";"), " 3 H " + "C5FF " + "COUTMUX " + "CX ");
                    return 0;
            }
            //End of Region C
            else if (argument.IndexOf("DX") == -1 & argument.IndexOf(" DFF") == -1 & argument.IndexOf(" D5FF") == -1 & argument.IndexOf("DOUTMUX") == -1)
            {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS1";
                        BYP_B = "BYP_B7";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS5";
                        BYP_B = "BYP_B6";
                    }

                    History_NUM++;
                    func_net_rout(inst_name, inst_name_H, History_NUM, "BQ", "DX", CLB_Type, ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                    argument = argument.Insert(argument.IndexOf(";"), " 3 H " + "D5FF " + "DOUTMUX " + "DX ");
                    return 0;
            }
            else
            {
                if(Def == 0) func_Dif_rout2(ref argument, XDLrout, ROW_SLICE, COL);
                return 1;
            }
        }
        private static void func_routing2Ap(ref string argument, StreamWriter XDLrout)
        {
            int CLB_type_index = 0;
            int SLICE_Type_index = 0;
            int Row_index = 0;
            int Col_index = 0;
            int NxtEnt_index = 0;
            int ROWslice_index = 0;

            string LOGIC_OUTS = "";
            string BYP_B = "";
            string SLICE_Type = "";
            string CLB_Type = "";
            string ROW = "";
            string COL = "";
            string ROW_SLICE = "";
            string inst_name = "";

            CLB_type_index = argument.IndexOf("CT");
            SLICE_Type_index = argument.IndexOf("ST");
            Row_index = argument.IndexOf("ROW");
            Col_index = argument.IndexOf("COL");
            NxtEnt_index = argument.IndexOf("\n");
            ROWslice_index = argument.IndexOf("WOR");
                        
            SLICE_Type = argument.Substring(SLICE_Type_index + 2, 1);
            CLB_Type = argument.Substring(CLB_type_index + 2, 2);
            ROW = argument.Substring(Row_index + 3, argument.IndexOf(" ", Row_index) - Row_index - 3);
            COL = argument.Substring(Col_index + 3, argument.IndexOf(" ", Col_index) - Col_index - 3);
            ROW_SLICE = argument.Substring(ROWslice_index + 3, argument.IndexOf(" ", ROWslice_index) - ROWslice_index - 3);
            inst_name = argument.Substring(argument.IndexOf("(") + 1, argument.IndexOf(")") - argument.IndexOf("(") - 1);


            if (argument.IndexOf("BX") == -1 & argument.IndexOf(" BFF") == -1 & argument.IndexOf(" B5FF") == -1 )
            {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS16";
                        BYP_B = "BYP_B5";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS20";
                        BYP_B = "BYP_B4";
                    }

                    History_NUM++;
                    func_net_rout(inst_name, inst_name, History_NUM, "AMUX", "BX", CLB_Type, ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                    argument = argument.Insert(argument.IndexOf(";"), " 2 H " + "BFF " + "BX ");                
            }
            //End of region B

            else if (argument.IndexOf("CX") == -1 & argument.IndexOf(" CFF") == -1 & argument.IndexOf(" C5FF") == -1)
            {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS16";
                        BYP_B = "BYP_B2";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS20";
                        BYP_B = "BYP_B3";
                    }

                    History_NUM++;
                    func_net_rout(inst_name, inst_name, History_NUM, "AMUX", "CX", CLB_Type, ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                    argument = argument.Insert(argument.IndexOf(";"), " 2 H " + "CFF " + "CX ");               
            }
            //End of Region C
            else if (argument.IndexOf("DX") == -1 & argument.IndexOf(" DFF") == -1 & argument.IndexOf(" D5FF") == -1)
            {
                  if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS16";
                        BYP_B = "BYP_B7";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS20";
                        BYP_B = "BYP_B6";
                    }

                    History_NUM++;
                    func_net_rout(inst_name, inst_name, History_NUM, "AMUX", "DX", CLB_Type, ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                    argument = argument.Insert(argument.IndexOf(";"), " 2 H " + "DFF " + "DX ");                
            }
            else
            {
                ;
            }
        }
        /// <summary>
        /// Routing in region A When AX is used before
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="XDLrout"></param>
        private static int func_routing1Ap(ref string argument, StreamWriter XDLrout,int Def)
        {
            int CLB_type_index = 0;
            int SLICE_Type_index = 0;
            int Row_index = 0;
            int Col_index = 0;
            int NxtEnt_index = 0;
            int ROWslice_index = 0;
           
            string LOGIC_OUTS = "";
            string BYP_B = "";
            string SLICE_Type = "";
            string CLB_Type = "";
            string ROW = "";
            string COL = "";
            string ROW_SLICE = "";
            string inst_name = "";

            CLB_type_index = argument.IndexOf("CT");
            SLICE_Type_index = argument.IndexOf("ST");
            Row_index = argument.IndexOf("ROW");
            Col_index = argument.IndexOf("COL");
            NxtEnt_index = argument.IndexOf("\n");
            ROWslice_index = argument.IndexOf("WOR");

            SLICE_Type = argument.Substring(SLICE_Type_index + 2, 1);
            CLB_Type = argument.Substring(CLB_type_index + 2, 2);
            ROW = argument.Substring(Row_index + 3, argument.IndexOf(" ", Row_index) - Row_index - 3);
            COL = argument.Substring(Col_index + 3, argument.IndexOf(" ", Col_index) - Col_index - 3);
            ROW_SLICE = argument.Substring(ROWslice_index + 3, argument.IndexOf(" ", ROWslice_index) - ROWslice_index - 3);
            inst_name = argument.Substring(argument.IndexOf("(") + 1, argument.IndexOf(")") - argument.IndexOf("(") - 1);



            if (argument.IndexOf("BX") == -1 & argument.IndexOf(" BFF") == -1 & argument.IndexOf( " B5FF") == -1 & argument.IndexOf("BOUTMUX") == -1)
            {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                            LOGIC_OUTS = "LOGIC_OUTS0";
                            BYP_B = "BYP_B5";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                            LOGIC_OUTS = "LOGIC_OUTS4";
                            BYP_B = "BYP_B4";
                    }

                    History_NUM++;

                    func_net_rout(inst_name, inst_name, History_NUM, "AQ", "BX", CLB_Type, ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                    argument = argument.Insert(argument.IndexOf(";"), " 1 H " + "B5FF " + "BOUTMUX " + "BX ");
                    return 0;
            }
            //End of region B

            else if (argument.IndexOf("CX") == -1 & argument.IndexOf(" CFF") == -1 & argument.IndexOf(" C5FF") == -1 & argument.IndexOf("COUTMUX") == -1)
            {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS0";
                        BYP_B = "BYP_B2";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS4";
                        BYP_B = "BYP_B3";
                    }

                    History_NUM++;
                    func_net_rout(inst_name, inst_name, History_NUM, "AQ", "CX", CLB_Type, ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                    argument = argument.Insert(argument.IndexOf(";"), " 1 H " + "C5FF " + "COUTMUX " + "CX ");
                    return 0;
            }
            //End of Region C
            else if (argument.IndexOf("DX") == -1 & argument.IndexOf(" DFF") == -1 & argument.IndexOf(" D5FF") == -1 & argument.IndexOf("DOUTMUX") == -1)
            {
                if (Int32.Parse(ROW_SLICE) % 2 == 1)
                {
                    LOGIC_OUTS = "LOGIC_OUTS0";
                    BYP_B = "BYP_B7";
                }
                else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                {
                    LOGIC_OUTS = "LOGIC_OUTS4";
                    BYP_B = "BYP_B6";
                }

                History_NUM++;
                func_net_rout(inst_name, inst_name, History_NUM, "AQ", "DX", CLB_Type, ROW, COL, SLICE_Type, LOGIC_OUTS, BYP_B, XDLrout);
                argument = argument.Insert(argument.IndexOf(";"), " 1 H " + "D5FF " + "DOUTMUX " + "DX ");
                return 0;
            }
            else
            {
                if (Def == 0)
                func_Dif_rout(ref argument, XDLrout, ROW_SLICE, COL);
                return 1;
            }
        }

        private static void func_Dif_rout(ref string argument, StreamWriter XDLrout,string ROW,string COL)
        {
            Stream Difrout;
            Difrout = File.OpenRead(@"F:\MS\FinalProject\CODE\Tmps953.XDL");
            StreamReader XDL_dif_rout = new StreamReader(Difrout);

            int row = 0;
            row = Int32.Parse(ROW);

            if (row % 2 == 0) row++;
            else row--;

            int Brother = 0;
            int End_Brother = 0;
            int first_Brother = 0;
            int Myself = 0;
            string XDL_dif = "";
            string Broth_Slice = "";

            ///from Up
            int CLB_type_index = 0;
            int SLICE_Type_index = 0;
            int Row_index = 0;
            int Col_index = 0;
            int NxtEnt_index = 0;
            int ROWslice_index = 0;

            string His = "";
            string LOGIC_OUTS = "";
            string BYP_B = "";
            string SLICE_Type = "";
            string CLB_Type = "";
            string ROW_SLICE = "";
            string inst_name = "";

            CLB_type_index = argument.IndexOf("CT");
            SLICE_Type_index = argument.IndexOf("ST");
            Row_index = argument.IndexOf("ROW");
            Col_index = argument.IndexOf("COL");
            NxtEnt_index = argument.IndexOf("\n");
            ROWslice_index = argument.IndexOf("WOR");
                        
            SLICE_Type = argument.Substring(SLICE_Type_index + 2, 1);
            CLB_Type = argument.Substring(CLB_type_index + 2, 2);
            ROW = argument.Substring(Row_index + 3, argument.IndexOf(" ", Row_index) - Row_index - 3);
            COL = argument.Substring(Col_index + 3, argument.IndexOf(" ", Col_index) - Col_index - 3);
            ROW_SLICE = argument.Substring(ROWslice_index + 3, argument.IndexOf(" ", ROWslice_index) - ROWslice_index - 3);
            inst_name = argument.Substring(argument.IndexOf("(") + 1, argument.IndexOf(")") - argument.IndexOf("(") - 1);

           ///

            XDL_dif = XDL_dif_rout.ReadToEnd();

            Brother = XDL_dif.IndexOf("WOR" + row.ToString() + " \n" + COL);

            Myself = argument.IndexOf("WOR");
            

            if (Brother != -1)
            {
                End_Brother = XDL_dif.IndexOf(";", Brother);
                first_Brother = XDL_dif.IndexOf(";", Brother - 30);
                Broth_Slice = XDL_dif.Substring(first_Brother + 2, End_Brother - first_Brother - 1); 
            }
              else
            {
                string inst_name_H = "";
                HiS_INST_NUM++;
                inst_name_H = " (REDUNANT" + HiS_INST_NUM + ")";
                argument = argument.Insert(argument.Length, "\n" + argument.Substring(0, Myself - 2) + "L WOR" + row.ToString() + " " + COL + inst_name_H + "*");
                inst_name_H = inst_name_H.Substring(inst_name_H.IndexOf("(") + 1, inst_name_H.IndexOf(")") - inst_name_H.IndexOf("(") - 1);
                ///////////For AFF
                if (argument.IndexOf(" AFF") != -1 & argument.IndexOf("1 H") == -1)
                    if (argument.IndexOf("H", argument.IndexOf(" AFF") - 1, 1) == -1)
                {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS0";
                        BYP_B = "BYP_B1";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS4";
                        BYP_B = "BYP_B0";
                    }

                    History_NUM++;

                    His = "\"History_" + History_NUM.ToString() + "\"";
                    XDLrout.Write("net ");
                    XDLrout.Write(His);
                    XDLrout.Write(" ,\n  outpin \"" + inst_name + "\" AQ ,\n  inpin \"" + inst_name_H + "\" AX ,\n  pip CLB");
                    XDLrout.Write(CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + SLICE_Type + "_AQ -> " + "CLB" + CLB_Type + "_" + LOGIC_OUTS + " ,\n");

                    XDLrout.Write("  pip CLB" + CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + BYP_B + " -> " + "CLB" + CLB_Type + "_" + SLICE_Type + "_AX ,\n");

                    XDLrout.Write("  pip INT" + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write(LOGIC_OUTS + " -> " + BYP_B + " ,\n");

                    XDLrout.Write(";\n");
                    argument = argument.Insert(argument.Length, " 1 H " + "AFF " + "AX ");
                }
                //////////For BFF
                if (argument.IndexOf(" BFF") != -1 & argument.IndexOf("3 H") == -1)
                    if (argument.IndexOf("H", argument.IndexOf(" BFF") - 1, 1) == -1)
                {

                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS1";
                        BYP_B = "BYP_B4";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS5";
                        BYP_B = "BYP_B5";
                    }

                    History_NUM++;

                    His = "\"History_" + History_NUM.ToString() + "\"";
                    XDLrout.Write("net ");
                    XDLrout.Write(His);
                    XDLrout.Write(" ,\n  outpin \"" + inst_name + "\" BQ ,\n  inpin \"" + inst_name_H + "\" BX ,\n  pip CLB");
                    XDLrout.Write(CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + SLICE_Type + "_BQ -> " + "CLB" + CLB_Type + "_" + LOGIC_OUTS + " ,\n");

                    XDLrout.Write("  pip CLB" + CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + BYP_B + " -> " + "CLB" + CLB_Type + "_" + SLICE_Type + "_BX ,\n");

                    XDLrout.Write("  pip INT" + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write(LOGIC_OUTS + " -> " + BYP_B + " ,\n");

                    XDLrout.Write(";\n");
                    argument = argument.Insert(argument.Length, " 3 H " + "BFF " + "BX ");
                }
                ///////////FOR CFF
                if (argument.IndexOf(" CFF") != -1 & argument.IndexOf("5 H") == -1)
                    if (argument.IndexOf("H", argument.IndexOf(" CFF") - 1, 1) == -1)
                {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS2";
                        BYP_B = "BYP_B3";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS6";
                        BYP_B = "BYP_B2";
                    }

                    History_NUM++;

                    His = "\"History_" + History_NUM.ToString() + "\"";
                    XDLrout.Write("net ");
                    XDLrout.Write(His);
                    XDLrout.Write(" ,\n  outpin \"" + inst_name + "\" CQ ,\n  inpin \"" + inst_name_H + "\" CX ,\n  pip CLB");
                    XDLrout.Write(CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + SLICE_Type + "_CQ -> " + "CLB" + CLB_Type + "_" + LOGIC_OUTS + " ,\n");

                    XDLrout.Write("  pip CLB" + CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + BYP_B + " -> " + "CLB" + CLB_Type + "_" + SLICE_Type + "_CX ,\n");

                    XDLrout.Write("  pip INT" + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write(LOGIC_OUTS + " -> " + BYP_B + " ,\n");

                    XDLrout.Write(";\n");
                    argument = argument.Insert(argument.Length, " 5 H " + "CFF " + "CX ");
                        
                }
                ////////For DFF

                if (argument.IndexOf(" DFF") != -1 & argument.IndexOf("7 H") == -1)
                    if (argument.IndexOf("H", argument.IndexOf(" DFF") - 1, 1) == -1)
                {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS3";
                        BYP_B = "BYP_B6";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS7";
                        BYP_B = "BYP_B7";
                    }

                    History_NUM++;

                    His = "\"History_" + History_NUM.ToString() + "\"";
                    XDLrout.Write("net ");
                    XDLrout.Write(His);
                    XDLrout.Write(" ,\n  outpin \"" + inst_name + "\" DQ ,\n  inpin \"" + inst_name_H + "\" DX ,\n  pip CLB");
                    XDLrout.Write(CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + SLICE_Type + "_DQ -> " + "CLB" + CLB_Type + "_" + LOGIC_OUTS + " ,\n");

                    XDLrout.Write("  pip CLB" + CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + BYP_B + " -> " + "CLB" + CLB_Type + "_" + SLICE_Type + "_DX ,\n");

                    XDLrout.Write("  pip INT" + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write(LOGIC_OUTS + " -> " + BYP_B + " ,\n");

                    XDLrout.Write(";\n");
                    argument = argument.Insert(argument.Length, " 7 H " + "DFF " + "DX ");
                }
                ////////For A5FF
                if (argument.IndexOf(" A5FF") != -1 & argument.IndexOf("2 H") == -1)
                    if (argument.IndexOf("H", argument.IndexOf(" A5FF") - 1, 1) == -1)
                {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS16";
                        BYP_B = "IMUX_B21";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS20";
                        BYP_B = "IMUX_B20";
                    }

                    History_NUM++;

                    His = "\"History_" + History_NUM.ToString() + "\"";
                    XDLrout.Write("net ");
                    XDLrout.Write(His);
                    XDLrout.Write(" ,\n  outpin \"" + inst_name + "\" AMUX ,\n  inpin \"" + inst_name_H + "\" A1 ,\n  pip CLB");
                    XDLrout.Write(CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + SLICE_Type + "_AMUX -> " + "CLB" + CLB_Type + "_" + LOGIC_OUTS + " ,\n");

                    XDLrout.Write("  pip CLB" + CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + BYP_B + " -> " + "CLB" + CLB_Type + "_" + SLICE_Type + "_A1 ,\n");

                    XDLrout.Write("  pip INT" + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write(LOGIC_OUTS + " -> " + BYP_B + " ,\n");

                    XDLrout.Write(";\n");
                    argument = argument.Insert(argument.Length, " 2 H " + "A5LUT " + "A5FF ");
                }
                ////////For B5FF
                if (argument.IndexOf(" B5FF") != -1 & argument.IndexOf("4 H") == -1)
                    if (argument.IndexOf("H", argument.IndexOf(" B5FF") - 1, 1) == -1)
                {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS17";
                        BYP_B = "IMUX_B33";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS21";
                        BYP_B = "IMUX_B32";
                    }

                    History_NUM++;

                    His = "\"History_" + History_NUM.ToString() + "\"";
                    XDLrout.Write("net ");
                    XDLrout.Write(His);
                    XDLrout.Write(" ,\n  outpin \"" + inst_name + "\" BMUX ,\n  inpin \"" + inst_name_H + "\" B1 ,\n  pip CLB");
                    XDLrout.Write(CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + SLICE_Type + "_BMUX -> " + "CLB" + CLB_Type + "_" + LOGIC_OUTS + " ,\n");

                    XDLrout.Write("  pip CLB" + CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + BYP_B + " -> " + "CLB" + CLB_Type + "_" + SLICE_Type + "_B1 ,\n");

                    XDLrout.Write("  pip INT" + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write(LOGIC_OUTS + " -> " + BYP_B + " ,\n");

                    XDLrout.Write(";\n");
                    argument = argument.Insert(argument.Length, " 4 H " + "B5LUT " + "B5FF ");
                }
                ///////For C5FF
                if (argument.IndexOf(" C5FF") != -1 & argument.IndexOf("6 H") == -1)
                    if (argument.IndexOf("H", argument.IndexOf(" C5FF") - 1, 1) == -1)
                {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS18";
                        BYP_B = "IMUX_B17";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS22";
                        BYP_B = "IMUX_B16";
                    }

                    History_NUM++;

                    His = "\"History_" + History_NUM.ToString() + "\"";
                    XDLrout.Write("net ");
                    XDLrout.Write(His);
                    XDLrout.Write(" ,\n  outpin \"" + inst_name + "\" CMUX ,\n  inpin \"" + inst_name_H + "\" C1 ,\n  pip CLB");
                    XDLrout.Write(CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + SLICE_Type + "_CMUX -> " + "CLB" + CLB_Type + "_" + LOGIC_OUTS + " ,\n");

                    XDLrout.Write("  pip CLB" + CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + BYP_B + " -> " + "CLB" + CLB_Type + "_" + SLICE_Type + "_C1 ,\n");

                    XDLrout.Write("  pip INT" + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write(LOGIC_OUTS + " -> " + BYP_B + " ,\n");

                    XDLrout.Write(";\n");
                    argument = argument.Insert(argument.Length, " 6 H " + "C5LUT " + "C5FF ");
                }
                ////////For D5FF
                if (argument.IndexOf(" D5FF") != -1 & argument.IndexOf("8 H") == -1)
                    if (argument.IndexOf("H", argument.IndexOf(" D5FF") - 1, 1) == -1)
                {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS19";
                        BYP_B = "IMUX_B37";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS23";
                        BYP_B = "IMUX_B36";
                    }

                    History_NUM++;

                    His = "\"History_" + History_NUM.ToString() + "\"";
                    XDLrout.Write("net ");
                    XDLrout.Write(His);
                    XDLrout.Write(" ,\n  outpin \"" + inst_name + "\" DMUX ,\n  inpin \"" + inst_name_H + "\" D1 ,\n  pip CLB");
                    XDLrout.Write(CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + SLICE_Type + "_DMUX -> " + "CLB" + CLB_Type + "_" + LOGIC_OUTS + " ,\n");

                    XDLrout.Write("  pip CLB" + CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + BYP_B + " -> " + "CLB" + CLB_Type + "_" + SLICE_Type + "_D1 ,\n");

                    XDLrout.Write("  pip INT" + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write(LOGIC_OUTS + " -> " + BYP_B + " ,\n");

                    XDLrout.Write(";\n");
                    argument = argument.Insert(argument.Length, " 8 H " + "D5LUT " + "D5FF ");
                }
            }

            Difrout.Close();
            XDL_dif_rout.Close();
        }
        private static void func_Dif_rout2(ref string argument, StreamWriter XDLrout, string ROW, string COL)
        {
            Stream Difrout;
            Difrout = File.OpenRead(@"F:\MS\FinalProject\CODE\Tmps953.XDL");
            StreamReader XDL_dif_rout = new StreamReader(Difrout);

            int row = 0;
            row = Int32.Parse(ROW);

            if (row % 2 == 0) row++;
            else row--;

            int Brother = 0;
            int End_Brother = 0;
            int first_Brother = 0;
            int Myself = 0;
            string XDL_dif = "";
            string Broth_Slice = "";

            ///from Up
            int CLB_type_index = 0;
            int SLICE_Type_index = 0;
            int Row_index = 0;
            int Col_index = 0;
            int NxtEnt_index = 0;
            int ROWslice_index = 0;

            string His = "";
            string LOGIC_OUTS = "";
            string BYP_B = "";
            string SLICE_Type = "";
            string CLB_Type = "";
            string ROW_SLICE = "";
            string inst_name = "";

            CLB_type_index = argument.IndexOf("CT");
            SLICE_Type_index = argument.IndexOf("ST");
            Row_index = argument.IndexOf("ROW");
            Col_index = argument.IndexOf("COL");
            NxtEnt_index = argument.IndexOf("\n");
            ROWslice_index = argument.IndexOf("WOR");

            SLICE_Type = argument.Substring(SLICE_Type_index + 2, 1);
            CLB_Type = argument.Substring(CLB_type_index + 2, 2);
            ROW = argument.Substring(Row_index + 3, argument.IndexOf(" ", Row_index) - Row_index - 3);
            COL = argument.Substring(Col_index + 3, argument.IndexOf(" ", Col_index) - Col_index - 3);
            ROW_SLICE = argument.Substring(ROWslice_index + 3, argument.IndexOf(" ", ROWslice_index) - ROWslice_index - 3);
            inst_name = argument.Substring(argument.IndexOf("(") + 1, argument.IndexOf(")") - argument.IndexOf("(") - 1);
            ///

            XDL_dif = XDL_dif_rout.ReadToEnd();

            Brother = XDL_dif.IndexOf("WOR" + row.ToString() + " \n" + COL);

            Myself = argument.IndexOf("WOR");


            if (Brother != -1)
            {
                End_Brother = XDL_dif.IndexOf(";", Brother);
                first_Brother = XDL_dif.IndexOf(";", Brother - 30);// Brother is in the middle
                Broth_Slice = XDL_dif.Substring(first_Brother + 2, End_Brother - first_Brother - 1);
            }
            else
            {
                //Console.Write("\n");
                string inst_name_H = "";
                HiS_INST_NUM++;
                inst_name_H = " (REDUNANT" + HiS_INST_NUM + ")";
                argument = argument.Insert(argument.Length, "\n" + argument.Substring(0, Myself - 2) + "L WOR" + row.ToString() + " " + COL + inst_name_H + "* ");
                inst_name_H = inst_name_H.Substring(inst_name_H.IndexOf("(") + 1, inst_name_H.IndexOf(")") - inst_name_H.IndexOf("(") - 1);
               
                //////////For BFF

                if (argument.IndexOf(" BFF") != -1 & argument.IndexOf("3 H") == -1)
                    if (argument.IndexOf("H",argument.IndexOf(" BFF")-1,1) == -1)
                {

                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS1";
                        BYP_B = "BYP_B4";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS5";
                        BYP_B = "BYP_B5";
                    }

                    History_NUM++;

                    His = "\"History_" + History_NUM.ToString() + "\"";
                    XDLrout.Write("net ");
                    XDLrout.Write(His);
                    XDLrout.Write(" ,\n  outpin \"" + inst_name + "\" BQ ,\n  inpin \"" + inst_name_H + "\" BX ,\n  pip CLB");
                    XDLrout.Write(CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + SLICE_Type + "_BQ -> " + "CLB" + CLB_Type + "_" + LOGIC_OUTS + " ,\n");

                    XDLrout.Write("  pip CLB" + CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + BYP_B + " -> " + "CLB" + CLB_Type + "_" + SLICE_Type + "_BX ,\n");

                    XDLrout.Write("  pip INT" + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write(LOGIC_OUTS + " -> " + BYP_B + " ,\n");

                    XDLrout.Write(";\n");
                    argument = argument.Insert(argument.Length, " 3 H " + "BFF " + "BX ");
                }
                ///////////FOR CFF
                if (argument.IndexOf(" CFF") != -1 & argument.IndexOf("5 H") == -1)
                    if (argument.IndexOf("H", argument.IndexOf(" CFF") - 1, 1) == -1)
                {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS2";
                        BYP_B = "BYP_B3";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS6";
                        BYP_B = "BYP_B2";
                    }

                    History_NUM++;

                    His = "\"History_" + History_NUM.ToString() + "\"";
                    XDLrout.Write("net ");
                    XDLrout.Write(His);
                    XDLrout.Write(" ,\n  outpin \"" + inst_name + "\" CQ ,\n  inpin \"" + inst_name_H + "\" CX ,\n  pip CLB");
                    XDLrout.Write(CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + SLICE_Type + "_CQ -> " + "CLB" + CLB_Type + "_" + LOGIC_OUTS + " ,\n");

                    XDLrout.Write("  pip CLB" + CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + BYP_B + " -> " + "CLB" + CLB_Type + "_" + SLICE_Type + "_CX ,\n");

                    XDLrout.Write("  pip INT" + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write(LOGIC_OUTS + " -> " + BYP_B + " ,\n");

                    XDLrout.Write(";\n");
                    argument = argument.Insert(argument.Length, " 5 H " + "CFF " + "CX ");
                }
                ////////For DFF

                if (argument.IndexOf(" DFF") != -1 & argument.IndexOf("7 H") == -1)
                     if(argument.IndexOf("H", argument.IndexOf(" DFF") - 1,1) == -1)
                {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS3";
                        BYP_B = "BYP_B6";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS7";
                        BYP_B = "BYP_B7";
                    }

                    History_NUM++;

                    His = "\"History_" + History_NUM.ToString() + "\"";
                    XDLrout.Write("net ");
                    XDLrout.Write(His);
                    XDLrout.Write(" ,\n  outpin \"" + inst_name + "\" DQ ,\n  inpin \"" + inst_name_H + "\" DX ,\n  pip CLB");
                    XDLrout.Write(CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + SLICE_Type + "_DQ -> " + "CLB" + CLB_Type + "_" + LOGIC_OUTS + " ,\n");

                    XDLrout.Write("  pip CLB" + CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + BYP_B + " -> " + "CLB" + CLB_Type + "_" + SLICE_Type + "_DX ,\n");

                    XDLrout.Write("  pip INT" + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write(LOGIC_OUTS + " -> " + BYP_B + " ,\n");

                    XDLrout.Write(";\n");
                    argument = argument.Insert(argument.Length, " 7 H " + "DFF " + "DX ");
                }
                ////////For B5FF
                if (argument.IndexOf(" B5FF") != -1 & argument.IndexOf("4 H") == -1)
                    if (argument.IndexOf("H", argument.IndexOf(" B5FF") - 1, 1) == -1)
                {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS17";
                        BYP_B = "IMUX_B33";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS21";
                        BYP_B = "IMUX_B32";
                    }

                    History_NUM++;

                    His = "\"History_" + History_NUM.ToString() + "\"";
                    XDLrout.Write("net ");
                    XDLrout.Write(His);
                    XDLrout.Write(" ,\n  outpin \"" + inst_name + "\" BMUX ,\n  inpin \"" + inst_name_H + "\" B1 ,\n  pip CLB");
                    XDLrout.Write(CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + SLICE_Type + "_BMUX -> " + "CLB" + CLB_Type + "_" + LOGIC_OUTS + " ,\n");

                    XDLrout.Write("  pip CLB" + CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + BYP_B + " -> " + "CLB" + CLB_Type + "_" + SLICE_Type + "_B1 ,\n");

                    XDLrout.Write("  pip INT" + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write(LOGIC_OUTS + " -> " + BYP_B + " ,\n");


                    XDLrout.Write(";\n");
                    argument = argument.Insert(argument.Length, " 4 H " + "B5LUT " + "B5FF ");
                }

                ///////For C5FF
                if (argument.IndexOf(" C5FF") != -1 & argument.IndexOf("6 H") == -1)
                    if (argument.IndexOf("H", argument.IndexOf(" C5FF") - 1, 1) == -1)
                {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS18";
                        BYP_B = "IMUX_B17";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS22";
                        BYP_B = "IMUX_B16";
                    }

                    History_NUM++;

                    His = "\"History_" + History_NUM.ToString() + "\"";
                    XDLrout.Write("net ");
                    XDLrout.Write(His);
                    XDLrout.Write(" ,\n  outpin \"" + inst_name + "\" CMUX ,\n  inpin \"" + inst_name_H + "\" C1 ,\n  pip CLB");
                    XDLrout.Write(CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + SLICE_Type + "_CMUX -> " + "CLB" + CLB_Type + "_" + LOGIC_OUTS + " ,\n");

                    XDLrout.Write("  pip CLB" + CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + BYP_B + " -> " + "CLB" + CLB_Type + "_" + SLICE_Type + "_C1 ,\n");

                    XDLrout.Write("  pip INT" + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write(LOGIC_OUTS + " -> " + BYP_B + " ,\n");


                    XDLrout.Write(";\n");
                    argument = argument.Insert(argument.Length, " 6 H " + "C5LUT " + "C5FF ");
                }
                ////////For D5FF
                if (argument.IndexOf(" D5FF") != -1 & argument.IndexOf("8 H") == -1)
                    if(argument.IndexOf("H", argument.IndexOf(" D5FF") - 1,1) == -1)
                {
                    if (Int32.Parse(ROW_SLICE) % 2 == 1)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS19";
                        BYP_B = "IMUX_B37";
                    }
                    else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                    {
                        LOGIC_OUTS = "LOGIC_OUTS23";
                        BYP_B = "IMUX_B36";
                    }

                    History_NUM++;

                    His = "\"History_" + History_NUM.ToString() + "\"";
                    XDLrout.Write("net ");
                    XDLrout.Write(His);
                    XDLrout.Write(" ,\n  outpin \"" + inst_name + "\" DMUX ,\n  inpin \"" + inst_name_H + "\" D1 ,\n  pip CLB");
                    XDLrout.Write(CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + SLICE_Type + "_DMUX -> " + "CLB" + CLB_Type + "_" + LOGIC_OUTS + " ,\n");

                    XDLrout.Write("  pip CLB" + CLB_Type + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write("CLB" + CLB_Type + "_" + BYP_B + " -> " + "CLB" + CLB_Type + "_" + SLICE_Type + "_D1 ,\n");

                    XDLrout.Write("  pip INT" + "_X" + ROW);
                    XDLrout.Write("Y" + COL + " ");
                    XDLrout.Write(LOGIC_OUTS + " -> " + BYP_B + " ,\n");


                    XDLrout.Write(";\n");
                    argument = argument.Insert(argument.Length, " 8 H " + "D5LUT " + "D5FF ");
                }
            }

            Difrout.Close();
            XDL_dif_rout.Close();
        }
        private static void func_Dif_rout3(ref string argument, StreamWriter XDLrout, string ROW, string COL)
        {
            Stream Difrout;
            Difrout = File.OpenRead(@"F:\MS\FinalProject\CODE\Tmps953.XDL");
            StreamReader XDL_dif_rout = new StreamReader(Difrout);

            int row = 0;
            row = Int32.Parse(ROW);

            if (row % 2 == 0) row++;
            else row--;

            int Brother = 0;
            int End_Brother = 0;
            int first_Brother = 0;
            int Myself = 0;
            string XDL_dif = "";
            string Broth_Slice = "";

            ///from Up
            int CLB_type_index = 0;
            int SLICE_Type_index = 0;
            int Row_index = 0;
            int Col_index = 0;
            int NxtEnt_index = 0;
            int ROWslice_index = 0;

            string His = "";
            string LOGIC_OUTS = "";
            string BYP_B = "";
            string SLICE_Type = "";
            string CLB_Type = "";
            string ROW_SLICE = "";
            string inst_name = "";

            CLB_type_index = argument.IndexOf("CT");
            SLICE_Type_index = argument.IndexOf("ST");
            Row_index = argument.IndexOf("ROW");
            Col_index = argument.IndexOf("COL");
            NxtEnt_index = argument.IndexOf("\n");
            ROWslice_index = argument.IndexOf("WOR");
                        
            SLICE_Type = argument.Substring(SLICE_Type_index + 2, 1);
            CLB_Type = argument.Substring(CLB_type_index + 2, 2);
            ROW = argument.Substring(Row_index + 3, argument.IndexOf(" ", Row_index) - Row_index - 3);
            COL = argument.Substring(Col_index + 3, argument.IndexOf(" ", Col_index) - Col_index - 3);
            ROW_SLICE = argument.Substring(ROWslice_index + 3, argument.IndexOf(" ", ROWslice_index) - ROWslice_index - 3);
            inst_name = argument.Substring(argument.IndexOf("(") + 1, argument.IndexOf(")") - argument.IndexOf("(") - 1);
            ///

            XDL_dif = XDL_dif_rout.ReadToEnd();

            Brother = XDL_dif.IndexOf("WOR" + row.ToString() + " \n" + COL);

            Myself = argument.IndexOf("WOR");


            if (Brother != -1)
            {
                End_Brother = XDL_dif.IndexOf(";", Brother);
                first_Brother = XDL_dif.IndexOf(";", Brother - 70);// Brother is in the middle
                Broth_Slice = XDL_dif.Substring(first_Brother + 2, End_Brother - first_Brother - 1);
            }
            else
            {
               // Console.Write("\n");
                string inst_name_H = "";
                HiS_INST_NUM++;
                inst_name_H = " (REDUNANT" + HiS_INST_NUM + ")";
                argument = argument.Insert(argument.Length, "\n" + argument.Substring(0, Myself - 2) + "L WOR" + row.ToString() + " " + COL + inst_name_H + "* ");
                inst_name_H = inst_name_H.Substring(inst_name_H.IndexOf("(") + 1, inst_name_H.IndexOf(")") - inst_name_H.IndexOf("(") - 1);
                ///////////FOR CFF
                if (argument.IndexOf(" CFF") != -1 & argument.IndexOf("5 H") == -1)
                    if (argument.IndexOf("H", argument.IndexOf(" CFF") - 1, 1) == -1)
                    {
                        if (Int32.Parse(ROW_SLICE) % 2 == 1)
                        {
                            LOGIC_OUTS = "LOGIC_OUTS2";
                            BYP_B = "BYP_B3";
                        }
                        else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                        {
                            LOGIC_OUTS = "LOGIC_OUTS6";
                            BYP_B = "BYP_B2";
                        }

                        History_NUM++;

                        His = "\"History_" + History_NUM.ToString() + "\"";
                        XDLrout.Write("net ");
                        XDLrout.Write(His);
                        XDLrout.Write(" ,\n  outpin \"" + inst_name + "\" CQ ,\n  inpin \"" + inst_name_H + "\" CX ,\n  pip CLB");
                        XDLrout.Write(CLB_Type + "_X" + ROW);
                        XDLrout.Write("Y" + COL + " ");
                        XDLrout.Write("CLB" + CLB_Type + "_" + SLICE_Type + "_CQ -> " + "CLB" + CLB_Type + "_" + LOGIC_OUTS + " ,\n");

                        XDLrout.Write("  pip CLB" + CLB_Type + "_X" + ROW);
                        XDLrout.Write("Y" + COL + " ");
                        XDLrout.Write("CLB" + CLB_Type + "_" + BYP_B + " -> " + "CLB" + CLB_Type + "_" + SLICE_Type + "_CX ,\n");

                        XDLrout.Write("  pip INT" + "_X" + ROW);
                        XDLrout.Write("Y" + COL + " ");
                        XDLrout.Write(LOGIC_OUTS + " -> " + BYP_B + " ,\n");

                        XDLrout.Write(";\n");
                        argument = argument.Insert(argument.Length, " 5 H " + "CFF " + "CX ");
                    }
                ////////For DFF

                if (argument.IndexOf(" DFF") != -1 & argument.IndexOf("7 H") == -1)
                    if (argument.IndexOf("H", argument.IndexOf(" DFF") - 1, 1) == -1)
                    {
                        if (Int32.Parse(ROW_SLICE) % 2 == 1)
                        {
                            LOGIC_OUTS = "LOGIC_OUTS3";
                            BYP_B = "BYP_B6";
                        }
                        else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                        {
                            LOGIC_OUTS = "LOGIC_OUTS7";
                            BYP_B = "BYP_B7";
                        }

                        History_NUM++;

                        His = "\"History_" + History_NUM.ToString() + "\"";
                        XDLrout.Write("net ");
                        XDLrout.Write(His);
                        XDLrout.Write(" ,\n  outpin \"" + inst_name + "\" DQ ,\n  inpin \"" + inst_name_H + "\" DX ,\n  pip CLB");
                        XDLrout.Write(CLB_Type + "_X" + ROW);
                        XDLrout.Write("Y" + COL + " ");
                        XDLrout.Write("CLB" + CLB_Type + "_" + SLICE_Type + "_DQ -> " + "CLB" + CLB_Type + "_" + LOGIC_OUTS + " ,\n");

                        XDLrout.Write("  pip CLB" + CLB_Type + "_X" + ROW);
                        XDLrout.Write("Y" + COL + " ");
                        XDLrout.Write("CLB" + CLB_Type + "_" + BYP_B + " -> " + "CLB" + CLB_Type + "_" + SLICE_Type + "_DX ,\n");

                        XDLrout.Write("  pip INT" + "_X" + ROW);
                        XDLrout.Write("Y" + COL + " ");
                        XDLrout.Write(LOGIC_OUTS + " -> " + BYP_B + " ,\n");

                        XDLrout.Write(";\n");
                        argument = argument.Insert(argument.Length, " 7 H " + "DFF " + "DX ");
                    }

                ///////For C5FF
                if (argument.IndexOf(" C5FF") != -1 & argument.IndexOf("6 H") == -1)
                    if (argument.IndexOf("H", argument.IndexOf(" C5FF") - 1, 1) == -1)
                    {
                        if (Int32.Parse(ROW_SLICE) % 2 == 1)
                        {
                            LOGIC_OUTS = "LOGIC_OUTS18";
                            BYP_B = "IMUX_B17";
                        }
                        else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                        {
                            LOGIC_OUTS = "LOGIC_OUTS22";
                            BYP_B = "IMUX_B16";
                        }

                        History_NUM++;

                        His = "\"History_" + History_NUM.ToString() + "\"";
                        XDLrout.Write("net ");
                        XDLrout.Write(His);
                        XDLrout.Write(" ,\n  outpin \"" + inst_name + "\" CMUX ,\n  inpin \"" + inst_name_H + "\" C1 ,\n  pip CLB");
                        XDLrout.Write(CLB_Type + "_X" + ROW);
                        XDLrout.Write("Y" + COL + " ");
                        XDLrout.Write("CLB" + CLB_Type + "_" + SLICE_Type + "_CMUX -> " + "CLB" + CLB_Type + "_" + LOGIC_OUTS + " ,\n");

                        XDLrout.Write("  pip CLB" + CLB_Type + "_X" + ROW);
                        XDLrout.Write("Y" + COL + " ");
                        XDLrout.Write("CLB" + CLB_Type + "_" + BYP_B + " -> " + "CLB" + CLB_Type + "_" + SLICE_Type + "_C1 ,\n");

                        XDLrout.Write("  pip INT" + "_X" + ROW);
                        XDLrout.Write("Y" + COL + " ");
                        XDLrout.Write(LOGIC_OUTS + " -> " + BYP_B + " ,\n");


                        XDLrout.Write(";\n");
                        argument = argument.Insert(argument.Length, " 6 H " + "C5LUT " + "C5FF ");
                    }
                ////////For D5FF
                if (argument.IndexOf(" D5FF") != -1 & argument.IndexOf("8 H") == -1)
                    if (argument.IndexOf("H", argument.IndexOf(" D5FF") - 1, 1) == -1)
                    {
                        if (Int32.Parse(ROW_SLICE) % 2 == 1)
                        {
                            LOGIC_OUTS = "LOGIC_OUTS19";
                            BYP_B = "IMUX_B37";
                        }
                        else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                        {
                            LOGIC_OUTS = "LOGIC_OUTS23";
                            BYP_B = "IMUX_B36";
                        }

                        History_NUM++;

                        His = "\"History_" + History_NUM.ToString() + "\"";
                        XDLrout.Write("net ");
                        XDLrout.Write(His);
                        XDLrout.Write(" ,\n  outpin \"" + inst_name + "\" DMUX ,\n  inpin \"" + inst_name_H + "\" D1 ,\n  pip CLB");
                        XDLrout.Write(CLB_Type + "_X" + ROW);
                        XDLrout.Write("Y" + COL + " ");
                        XDLrout.Write("CLB" + CLB_Type + "_" + SLICE_Type + "_DMUX -> " + "CLB" + CLB_Type + "_" + LOGIC_OUTS + " ,\n");

                        XDLrout.Write("  pip CLB" + CLB_Type + "_X" + ROW);
                        XDLrout.Write("Y" + COL + " ");
                        XDLrout.Write("CLB" + CLB_Type + "_" + BYP_B + " -> " + "CLB" + CLB_Type + "_" + SLICE_Type + "_D1 ,\n");

                        XDLrout.Write("  pip INT" + "_X" + ROW);
                        XDLrout.Write("Y" + COL + " ");
                        XDLrout.Write(LOGIC_OUTS + " -> " + BYP_B + " ,\n");


                        XDLrout.Write(";\n");
                        argument = argument.Insert(argument.Length, " 8 H " + "D5LUT " + "D5FF ");
                    }
            }
            Difrout.Close();
            XDL_dif_rout.Close();
        }
        private static void func_Dif_rout4(ref string argument, StreamWriter XDLrout, string ROW, string COL)
        {
            Stream Difrout;
            Difrout = File.OpenRead(@"F:\MS\FinalProject\CODE\Tmps953.XDL");
            StreamReader XDL_dif_rout = new StreamReader(Difrout);

            int row = 0;
            row = Int32.Parse(ROW);

            if (row % 2 == 0) row++;
            else row--;

            int Brother = 0;
            int End_Brother = 0;
            int first_Brother = 0;
            int Myself = 0;
            string XDL_dif = "";
            string Broth_Slice = "";

            ///from Up
            int CLB_type_index = 0;
            int SLICE_Type_index = 0;
            int Row_index = 0;
            int Col_index = 0;
            int NxtEnt_index = 0;
            int ROWslice_index = 0;

            string His = "";
            string LOGIC_OUTS = "";
            string BYP_B = "";
            string SLICE_Type = "";
            string CLB_Type = "";
            string ROW_SLICE = "";
            string inst_name = "";

            CLB_type_index = argument.IndexOf("CT");
            SLICE_Type_index = argument.IndexOf("ST");
            Row_index = argument.IndexOf("ROW");
            Col_index = argument.IndexOf("COL");
            NxtEnt_index = argument.IndexOf("\n");
            ROWslice_index = argument.IndexOf("WOR");
                        
            SLICE_Type = argument.Substring(SLICE_Type_index + 2, 1);
            CLB_Type = argument.Substring(CLB_type_index + 2, 2);
            ROW = argument.Substring(Row_index + 3, argument.IndexOf(" ", Row_index) - Row_index - 3);
            COL = argument.Substring(Col_index + 3, argument.IndexOf(" ", Col_index) - Col_index - 3);
            ROW_SLICE = argument.Substring(ROWslice_index + 3, argument.IndexOf(" ", ROWslice_index) - ROWslice_index - 3);
            inst_name = argument.Substring(argument.IndexOf("(") + 1, argument.IndexOf(")") - argument.IndexOf("(") - 1);
            ///

            XDL_dif = XDL_dif_rout.ReadToEnd();

            Brother = XDL_dif.IndexOf("WOR" + row.ToString() + " \n" + COL);

            Myself = argument.IndexOf("WOR");


            if (Brother != -1)
            {
                End_Brother = XDL_dif.IndexOf(";", Brother);
                first_Brother = XDL_dif.IndexOf(";", Brother - 30);// Brother is in the middle
                Broth_Slice = XDL_dif.Substring(first_Brother + 2, End_Brother - first_Brother - 1);
            }
            else
            {
               // Console.Write("\n");
                string inst_name_H = "";
                HiS_INST_NUM++;
                inst_name_H = " (REDUNANT" + HiS_INST_NUM + ")";
                argument = argument.Insert(argument.Length, "\n" + argument.Substring(0, Myself - 2) + "L WOR" + row.ToString() + " " + COL + inst_name_H +"* ");
                inst_name_H = inst_name_H.Substring(inst_name_H.IndexOf("(") + 1, inst_name_H.IndexOf(")") - inst_name_H.IndexOf("(") - 1);
                ////////For DFF

                if (argument.IndexOf(" DFF") != -1 & argument.IndexOf("7 H") == -1)
                    if (argument.IndexOf("H", argument.IndexOf(" DFF") - 1, 1) == -1)
                    {
                        if (Int32.Parse(ROW_SLICE) % 2 == 1)
                        {
                            LOGIC_OUTS = "LOGIC_OUTS3";
                            BYP_B = "BYP_B6";
                        }
                        else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                        {
                            LOGIC_OUTS = "LOGIC_OUTS7";
                            BYP_B = "BYP_B7";
                        }

                        History_NUM++;

                        His = "\"History_" + History_NUM.ToString() + "\"";
                        XDLrout.Write("net ");
                        XDLrout.Write(His);
                        XDLrout.Write(" ,\n  outpin \"" + inst_name + "\" DQ ,\n  inpin \"" + inst_name_H + "\" DX ,\n  pip CLB");
                        XDLrout.Write(CLB_Type + "_X" + ROW);
                        XDLrout.Write("Y" + COL + " ");
                        XDLrout.Write("CLB" + CLB_Type + "_" + SLICE_Type + "_DQ -> " + "CLB" + CLB_Type + "_" + LOGIC_OUTS + " ,\n");

                        XDLrout.Write("  pip CLB" + CLB_Type + "_X" + ROW);
                        XDLrout.Write("Y" + COL + " ");
                        XDLrout.Write("CLB" + CLB_Type + "_" + BYP_B + " -> " + "CLB" + CLB_Type + "_" + SLICE_Type + "_DX ,\n");

                        XDLrout.Write("  pip INT" + "_X" + ROW);
                        XDLrout.Write("Y" + COL + " ");
                        XDLrout.Write(LOGIC_OUTS + " -> " + BYP_B + " ,\n");

                        XDLrout.Write(";\n");
                        argument = argument.Insert(argument.Length, " 7 H " + "DFF " + "DX ");
                    }
                ////////For D5FF
                if (argument.IndexOf(" D5FF") != -1 & argument.IndexOf("8 H") == -1)
                    if (argument.IndexOf("H", argument.IndexOf("D5FF") - 1, 1) == -1)
                    {
                        if (Int32.Parse(ROW_SLICE) % 2 == 1)
                        {
                            LOGIC_OUTS = "LOGIC_OUTS19";
                            BYP_B = "IMUX_B37";
                        }
                        else if (Int32.Parse(ROW_SLICE) % 2 == 0)
                        {
                            LOGIC_OUTS = "LOGIC_OUTS23";
                            BYP_B = "IMUX_B36";
                        }

                        History_NUM++;

                        His = "\"History_" + History_NUM.ToString() + "\"";
                        XDLrout.Write("net ");
                        XDLrout.Write(His);
                        XDLrout.Write(" ,\n  outpin \"" + inst_name + "\" DMUX ,\n  inpin \"" + inst_name_H + "\" D1 ,\n  pip CLB");
                        XDLrout.Write(CLB_Type + "_X" + ROW);
                        XDLrout.Write("Y" + COL + " ");
                        XDLrout.Write("CLB" + CLB_Type + "_" + SLICE_Type + "_DMUX -> " + "CLB" + CLB_Type + "_" + LOGIC_OUTS + " ,\n");

                        XDLrout.Write("  pip CLB" + CLB_Type + "_X" + ROW);
                        XDLrout.Write("Y" + COL + " ");
                        XDLrout.Write("CLB" + CLB_Type + "_" + BYP_B + " -> " + "CLB" + CLB_Type + "_" + SLICE_Type + "_D1 ,\n");

                        XDLrout.Write("  pip INT" + "_X" + ROW);
                        XDLrout.Write("Y" + COL + " ");
                        XDLrout.Write(LOGIC_OUTS + " -> " + BYP_B + " ,\n");


                        XDLrout.Write(";\n");
                        argument = argument.Insert(argument.Length, " 8 H " + "D5LUT " + "D5FF ");
                    }
            }
            Difrout.Close();
            XDL_dif_rout.Close();
        }
        ////If 5FF is active and FF doesn't active --> This state is not availabale
        private static void func_routing2(string X,ref string argument, StreamWriter XDLrout)
        {
            int CLB_type_index = 0;
            int SLICE_Type_index = 0;
            int Row_index = 0;
            int Col_index = 0;
            int NxtEnt_index = 0;
            int ROWslice_index = 0;
            
            string His = "";
            string LOGIC_OUTS = "";
            string BYP_B = "";
            string SLICE_Type = "";
            string CLB_Type = "";
            string ROW = "";
            string COL = "";
            string ROW_SLICE = "";
            string inst_name = "";

            CLB_type_index = argument.IndexOf("CT");
            SLICE_Type_index = argument.IndexOf("ST");
            Row_index = argument.IndexOf("ROW");
            Col_index = argument.IndexOf("COL");
            NxtEnt_index = argument.IndexOf("\n");
            ROWslice_index = argument.IndexOf("WOR");
                        
            SLICE_Type = argument.Substring(SLICE_Type_index + 2, 1);
            CLB_Type = argument.Substring(CLB_type_index + 2, 2);
            ROW = argument.Substring(Row_index + 3, argument.IndexOf(" ", Row_index) - Row_index - 3);
            COL = argument.Substring(Col_index + 3, argument.IndexOf(" ", Col_index) - Col_index - 3);
            ROW_SLICE = argument.Substring(ROWslice_index + 3, argument.IndexOf(" ", ROWslice_index) - ROWslice_index - 3);
            inst_name = argument.Substring(argument.IndexOf("(") + 1, argument.IndexOf(")") - argument.IndexOf("(") - 1);
            if (Int32.Parse(ROW_SLICE) % 2 == 1)
            {
                if (X == "A")
                {
                    LOGIC_OUTS = "LOGIC_OUTS16";
                    BYP_B = "BYP_B0";
                }
                else if (X == "B")
                {
                    LOGIC_OUTS = "LOGIC_OUTS17";
                    BYP_B = "BYP_B5";
                }
                else if (X == "C")
                {
                    LOGIC_OUTS = "LOGIC_OUTS18";
                    BYP_B = "BYP_B2";
                }
                else if (X == "D")
                {
                    LOGIC_OUTS = "LOGIC_OUTS19";
                    BYP_B = "BYP_B7";
                }
            }
            else if (Int32.Parse(ROW_SLICE) % 2 == 0)
            {
                if (X == "A")
                {
                    LOGIC_OUTS = "LOGIC_OUTS20";
                    BYP_B = "BYP_B1";
                }
                else if (X == "B")
                {
                    LOGIC_OUTS = "LOGIC_OUTS21";
                    BYP_B = "BYP_B4";
                }
                else if (X == "C")
                {
                    LOGIC_OUTS = "LOGIC_OUTS22";
                    BYP_B = "BYP_B3";
                }
                else if (X == "D")
                {
                    LOGIC_OUTS = "LOGIC_OUTS23";
                    BYP_B = "BYP_B6";
                }
            }
            
            History_NUM++;

            His = "\"History_" + History_NUM.ToString() + "\"";
            XDLrout.Write("net ");
            XDLrout.Write(His);
            XDLrout.Write(" ,\n  outpin \"" + inst_name + "\" " + X + "MUX ,\n  inpin \"" + inst_name + "\" " + X + "X ,\n  pip CLB");
            XDLrout.Write(CLB_Type+"_X"+ ROW);
            XDLrout.Write("Y" + COL + " ");
            XDLrout.Write("CLB" + CLB_Type + "_" + SLICE_Type+ "_"+ X +"MUX -> " + "CLB" + CLB_Type +"_"+ LOGIC_OUTS+" ,\n");
            
            XDLrout.Write("  pip CLB"+CLB_Type + "_X" + ROW);
            XDLrout.Write("Y" + COL + " ");
            XDLrout.Write("CLB" + CLB_Type + "_" + BYP_B + " -> " + "CLB" + CLB_Type + "_" + SLICE_Type + "_" + X + "X ,\n");

            XDLrout.Write("  pip INT"+ "_X" + ROW);
            XDLrout.Write("Y" + COL + " ");
            XDLrout.Write(LOGIC_OUTS+ " -> " + BYP_B+" ,\n");

            XDLrout.Write(";\n");
                       
        }
        //If FF be active and 5FF doesn't be active
        private static void func_routing1(string X,ref string argument, StreamWriter XDLrout)
        {
            int CLB_type_index = 0;
            int SLICE_Type_index = 0;
            int Row_index = 0;
            int Col_index = 0;
            int NxtEnt_index = 0;
            int ROWslice_index = 0;

            string His = "";
            string LOGIC_OUTS = "";
            string BYP_B = "";
            string SLICE_Type = "";
            string CLB_Type = "";
            string ROW = "";
            string COL = "";
            string ROW_SLICE = "";
            string inst_name = "";

            CLB_type_index = argument.IndexOf("CT");
            SLICE_Type_index = argument.IndexOf("ST");
            Row_index = argument.IndexOf("ROW");
            Col_index = argument.IndexOf("COL");
            NxtEnt_index = argument.IndexOf("\n");
            ROWslice_index = argument.IndexOf("WOR");
                        
            SLICE_Type = argument.Substring(SLICE_Type_index + 2, 1);
            CLB_Type = argument.Substring(CLB_type_index + 2, 2);
            ROW = argument.Substring(Row_index + 3, argument.IndexOf(" ", Row_index) - Row_index - 3);
            COL = argument.Substring(Col_index + 3, argument.IndexOf(" ", Col_index) - Col_index - 3);
            ROW_SLICE = argument.Substring(ROWslice_index + 3, argument.IndexOf(" ", ROWslice_index) - ROWslice_index - 3);
            inst_name = argument.Substring(argument.IndexOf("(") + 1, argument.IndexOf(")") - argument.IndexOf("(") - 1);
            if (Int32.Parse(ROW_SLICE) % 2 == 1)
            {
                if (X == "A")
                {
                    LOGIC_OUTS = "LOGIC_OUTS0";
                    BYP_B = "BYP_B0";
                }
                else if (X == "B")
                {
                    LOGIC_OUTS = "LOGIC_OUTS1";
                    BYP_B = "BYP_B5";
                }
                else if (X == "C")
                {
                    LOGIC_OUTS = "LOGIC_OUTS2";
                    BYP_B = "BYP_B2";
                }
                else if (X == "D")
                {
                    LOGIC_OUTS = "LOGIC_OUTS3";
                    BYP_B = "BYP_B7";
                }
            }
            else if (Int32.Parse(ROW_SLICE) % 2 == 0)
            {
                if (X == "A")
                {
                    LOGIC_OUTS = "LOGIC_OUTS4";
                    BYP_B = "BYP_B1";
                }
                else if (X == "B")
                {
                    LOGIC_OUTS = "LOGIC_OUTS5";
                    BYP_B = "BYP_B4";
                }
                else if (X == "C")
                {
                    LOGIC_OUTS = "LOGIC_OUTS6";
                    BYP_B = "BYP_B3";
                }
                else if (X == "D")
                {
                    LOGIC_OUTS = "LOGIC_OUTS7";
                    BYP_B = "BYP_B6";
                }
            }
            
            History_NUM++;

            His = "\"History_" + History_NUM.ToString() + "\"";
            XDLrout.Write("net ");
            XDLrout.Write(His);
            XDLrout.Write(" ,\n  outpin \"" + inst_name + "\" " + X + "Q ,\n  inpin \"" + inst_name + "\" " + X + "X ,\n  pip CLB");
            XDLrout.Write(CLB_Type+"_X"+ ROW);
            XDLrout.Write("Y" + COL + " ");
            XDLrout.Write("CLB" + CLB_Type + "_" + SLICE_Type + "_"+ X +"Q -> " + "CLB" + CLB_Type +"_"+ LOGIC_OUTS+" ,\n");
            
            XDLrout.Write("  pip CLB"+CLB_Type + "_X" + ROW);
            XDLrout.Write("Y" + COL + " ");
            XDLrout.Write("CLB" + CLB_Type + "_" + BYP_B + " -> " + "CLB" + CLB_Type + "_" + SLICE_Type + "_" + X + "X ,\n");

            XDLrout.Write("  pip INT"+ "_X" + ROW);
            XDLrout.Write("Y" + COL + " ");
            XDLrout.Write(LOGIC_OUTS+ " -> " + BYP_B+" ,\n");

            XDLrout.Write(";\n");
                       
        }
    }
}

