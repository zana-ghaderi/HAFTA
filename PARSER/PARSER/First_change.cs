using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PARSER
{
    public class First_change
    {
        public int Inst_num = 0;
        internal void main()
        {
            /////Read the final that contains the changes
            Stream R_Final;
            R_Final = File.OpenRead(@"F:\MS\FinalProject\CODE\Final.XDL");
            StreamReader XDL_final = new StreamReader(R_Final);
            ////Read whole s953
            Stream W_Final;
            W_Final = File.OpenRead(@"F:\MS\FinalProject\CODE\s953.XDL");
            StreamReader XDL_Ch_s953 = new StreamReader(W_Final);
            ////Read an instance from s953
            Stream WI_Final;
            WI_Final = File.OpenRead(@"F:\MS\FinalProject\CODE\s953.XDL");
            StreamReader XDL_Ch_s953_I = new StreamReader(W_Final);
            ////Write changed s953
            Stream Changed_s953;
            Changed_s953 = File.OpenWrite(@"F:\MS\FinalProject\CODE\Ch_s953.XDL");
            StreamWriter XDL_F_s953 = new StreamWriter(Changed_s953);

            string Inst = ""; string tmp_inst = ""; string WOR_B="";
            string Small_ins = ""; string Small_insB = ""; string Small_insBB = "";
            string Coms953 = "";
            string Line = "";
            string TYP = ""; string STYP = ""; string ROW = ""; string COL = ""; string WOR = "";
            int indSPAC = 0; int indendINS = 0; int indfirstINS = 0;
            int count = 0; int ind_BB = 0; string wor_bb = "";

            Coms953 = XDL_Ch_s953.ReadToEnd().ToString();
            while (XDL_final.EndOfStream == false)
            {
                Line = XDL_final.ReadLine();
                if (Line != "")
                {
                    TYP = Line.Substring(Line.IndexOf("CT") + 2, 2);
                    STYP = Line.Substring(Line.IndexOf("ST") + 2, 1);
                    indSPAC = Line.IndexOf(" ", Line.IndexOf("ROW"));
                    ROW = Line.Substring(Line.IndexOf("ROW") + 3, indSPAC - Line.IndexOf("ROW") - 3);
                    indSPAC = Line.IndexOf(" ", Line.IndexOf("COL"));
                    COL = Line.Substring(Line.IndexOf("COL") + 3, indSPAC - Line.IndexOf("COL") - 3);
                    if (Line.IndexOf("WOR") != -1)
                    {
                        indSPAC = Line.IndexOf(" ", Line.IndexOf("WOR"));
                        WOR = Line.Substring(Line.IndexOf("WOR") + 3, indSPAC - Line.IndexOf("WOR") - 3);
                    }

                    Small_ins = "CLB" + TYP + "_X" + ROW + "Y" + COL + " " + "SLICE_X" + WOR + "Y" + COL;
                    if (Int16.Parse(WOR) % 2 == 0) WOR_B = (Int16.Parse(WOR) + 1).ToString();
                    else WOR_B = (Int16.Parse(WOR) - 1).ToString();
                    Small_insB = "SLICE_X" + WOR_B + "Y" + COL;
                    if (Coms953.IndexOf(Small_ins) != -1)
                    {
                        Inst_num++;

                        indendINS = Coms953.IndexOf(";", Coms953.IndexOf(Small_ins));
                        indfirstINS = Coms953.IndexOf("inst \"", Coms953.IndexOf(Small_ins) - 200);
                        Inst = Coms953.Substring(indfirstINS, indendINS - indfirstINS + 1);
                        tmp_inst = Inst;
                        change(ref tmp_inst, Line);
                        Coms953 = Coms953.Replace(Inst, tmp_inst);
                        /////////////////////////////////////////change the brothers
                        if (Line.IndexOf("1b H ") != -1 || Line.IndexOf("2b H ") != -1 || Line.IndexOf("3b H ") != -1 || Line.IndexOf("4b H ") != -1
                            || Line.IndexOf("5b H ") != -1 || Line.IndexOf("6b H ") != -1 || Line.IndexOf("7b H ") != -1 || Line.IndexOf("8b H ") != -1)
                        {
                            if (Coms953.IndexOf(Small_insB) != -1)
                            {
                                Inst_num++;
                                indendINS = Coms953.IndexOf(";", Coms953.IndexOf(Small_insB));
                                indfirstINS = Coms953.IndexOf("inst \"", Coms953.IndexOf(Small_insB) - 500);
                                Inst = Coms953.Substring(indfirstINS, indendINS - indfirstINS + 1);
                                tmp_inst = Inst;
                                changeB(ref tmp_inst, Line);
                                Coms953 = Coms953.Replace(Inst, tmp_inst);
                            }
                        }
                        ind_BB = 0;
                        while (Line.IndexOf("bb H ", ind_BB) != -1)
                        {
                            Small_insBB = Line.Substring(Line.IndexOf("bb H ") - 6, 4);
                            if (Small_insBB.IndexOf("WW1") == -1 & Small_insBB.IndexOf("EE1") == -1 &
                                Small_insBB.IndexOf("WW2") == -1 & Small_insBB.IndexOf("EE2") == -1 &
                                Small_insBB.IndexOf("NW2") == -1 & Small_insBB.IndexOf("NE2") == -1 &
                                Small_insBB.IndexOf("SW2") == -1 & Small_insBB.IndexOf("SE2") == -1)
                            {
                                if (Small_insBB.IndexOf("L") != -1)
                                {
                                    if (Int16.Parse(WOR) % 2 != 0) wor_bb = (Int16.Parse(WOR) - 1).ToString();
                                    else wor_bb = WOR;
                                    Small_insBB = Small_insBB.Replace("L", "");
                                }
                                else
                                {
                                    if (Int16.Parse(WOR) % 2 != 0) wor_bb = WOR;
                                    else wor_bb = (Int16.Parse(WOR) + 1).ToString();
                                    Small_insBB = Small_insBB.Replace("R", "");
                                }
                            }

                            if (Small_insBB == "SS1") { Small_insBB = "SLICE_X" + wor_bb + "Y" + (Int16.Parse(COL) - 1).ToString(); }
                            else if (Small_insBB == "NN1") { Small_insBB = "SLICE_X" + wor_bb + "Y" + (Int16.Parse(COL) + 1).ToString(); }
                            else if (Small_insBB == "LWW1" || Small_insBB == "RWW1")
                            {
                                if (Small_insBB.IndexOf("L") != -1)
                                {
                                    if (Int16.Parse(WOR) % 2 != 0) wor_bb = (Int16.Parse(WOR) - 3).ToString();
                                    else wor_bb = (Int16.Parse(WOR) - 2).ToString();
                                }
                                else
                                {
                                    if (Int16.Parse(WOR) % 2 != 0) wor_bb = (Int16.Parse(WOR) - 2).ToString();
                                    else wor_bb = (Int16.Parse(WOR) - 1).ToString();
                                }
                                Small_insBB = "SLICE_X" + wor_bb + "Y" + COL;
                            }
                            else if (Small_insBB == "LEE1" || Small_insBB == "REE1")
                            {
                                if (Small_insBB.IndexOf("L") != -1)
                                {
                                    if (Int16.Parse(WOR) % 2 != 0) wor_bb = (Int16.Parse(WOR) + 1).ToString();
                                    else wor_bb = (Int16.Parse(WOR) + 2).ToString();
                                }
                                else
                                {
                                    if (Int16.Parse(WOR) % 2 != 0) wor_bb = (Int16.Parse(WOR) + 2).ToString();
                                    else wor_bb = (Int16.Parse(WOR) + 3).ToString();
                                }
                                Small_insBB = "SLICE_X" + wor_bb + "Y" + COL;
                            }
                            else if (Small_insBB == "SS2") { Small_insBB = "SLICE_X" + wor_bb + "Y" + (Int16.Parse(COL) - 2).ToString(); }
                            else if (Small_insBB == "NN2") { Small_insBB = "SLICE_X" + wor_bb + "Y" + (Int16.Parse(COL) - 2).ToString(); }
                            else if (Small_insBB == "LWW2" || Small_insBB == "RWW2")
                            {
                                if (Small_insBB.IndexOf("L") != -1)
                                {
                                    if (Int16.Parse(WOR) % 2 != 0) wor_bb = (Int16.Parse(WOR) - 5).ToString();
                                    else wor_bb = (Int16.Parse(WOR) - 4).ToString();
                                }
                                else
                                {
                                    if (Int16.Parse(WOR) % 2 != 0) wor_bb = (Int16.Parse(WOR) - 4).ToString();
                                    else wor_bb = (Int16.Parse(WOR) - 3).ToString();
                                }
                                Small_insBB = "SLICE_X" + WOR + "Y" + COL;
                            }
                            else if (Small_insBB == "LEE2" || Small_insBB == "REE2")
                            {
                                if (Small_insBB.IndexOf("L") != -1)
                                {
                                    if (Int16.Parse(WOR) % 2 != 0) wor_bb = (Int16.Parse(WOR) + 3).ToString();
                                    else wor_bb = (Int16.Parse(WOR) + 4).ToString();

                                }
                                else
                                {
                                    if (Int16.Parse(WOR) % 2 != 0) wor_bb = (Int16.Parse(WOR) + 4).ToString();
                                    else wor_bb = (Int16.Parse(WOR) + 5).ToString();
                                }
                                Small_insBB = "SLICE_X" + wor_bb + "Y" + COL;
                            }
                            else if (Small_insBB == "LNW2" || Small_insBB == "RNW2") 
                            {
                                if (Small_insBB.IndexOf("L") != -1)
                                {
                                    if (Int16.Parse(WOR) % 2 != 0) wor_bb = (Int16.Parse(WOR) - 3).ToString();
                                    else wor_bb = (Int16.Parse(WOR) - 2).ToString();
                                }
                                else
                                {
                                    if (Int16.Parse(WOR) % 2 != 0) wor_bb = (Int16.Parse(WOR) - 2).ToString();
                                    else wor_bb = (Int16.Parse(WOR) - 1).ToString();
                                }
                                Small_insBB = "SLICE_X" + wor_bb + "Y" + (Int16.Parse(COL) + 1).ToString(); 
                            }
                            else if (Small_insBB == "LNE2" || Small_insBB == "RNE2") 
                            {
                                if (Small_insBB.IndexOf("L") != -1)
                                {
                                    if (Int16.Parse(WOR) % 2 != 0) wor_bb = (Int16.Parse(WOR) + 1).ToString();
                                    else wor_bb = (Int16.Parse(WOR) + 2).ToString();
                                }
                                else
                                {
                                    if (Int16.Parse(WOR) % 2 != 0) wor_bb = (Int16.Parse(WOR) + 2).ToString();
                                    else wor_bb = (Int16.Parse(WOR) + 3).ToString();
                                }
                                Small_insBB = "SLICE_X" + wor_bb + "Y" + (Int16.Parse(COL) + 1).ToString(); 
                            }
                            else if (Small_insBB == "RSE2" || Small_insBB == "LSE2") 
                            {
                                if (Small_insBB.IndexOf("L") != -1)
                                {
                                    if (Int16.Parse(WOR) % 2 != 0) wor_bb = (Int16.Parse(WOR) + 1).ToString();
                                    else wor_bb = (Int16.Parse(WOR) + 2).ToString();
                                }
                                else
                                {
                                    if (Int16.Parse(WOR) % 2 != 0) wor_bb = (Int16.Parse(WOR) + 2).ToString();
                                    else wor_bb = (Int16.Parse(WOR) + 3).ToString();
                                }
                                Small_insBB = "SLICE_X" + wor_bb + "Y" + (Int16.Parse(COL) - 1).ToString(); 
                            }
                            else if (Small_insBB == "RSW2" || Small_insBB == "LSW2") 
                            {
                                if (Small_insBB.IndexOf("L") != -1)
                                {
                                    if (Int16.Parse(WOR) % 2 != 0) wor_bb = (Int16.Parse(WOR) + 1).ToString();
                                    else wor_bb = (Int16.Parse(WOR) + 2).ToString();
                                }
                                else
                                {
                                    if (Int16.Parse(WOR) % 2 != 0) wor_bb = (Int16.Parse(WOR) + 2).ToString();
                                    else wor_bb = (Int16.Parse(WOR) + 3).ToString();
                                }
                                Small_insBB = "SLICE_X" + wor_bb + "Y" + (Int16.Parse(COL) - 1).ToString(); 
                            }

                            Inst_num++;
                        
                            indendINS = Coms953.IndexOf(";", Coms953.IndexOf(Small_insBB));
                            indfirstINS = Coms953.IndexOf("inst \"", Coms953.IndexOf(Small_insBB) - 500);
                            Inst = Coms953.Substring(indfirstINS, indendINS - indfirstINS + 1);
                            tmp_inst = Inst;
                            changeBB(ref tmp_inst, Line);
                            Coms953 = Coms953.Replace(Inst, tmp_inst);
                            ind_BB = Line.IndexOf("bb H ", ind_BB) + 5;
                            wor_bb = "";
                        }
                        Coms953 = Coms953.Replace(Inst, tmp_inst);
                    }
                    else
                    {
                        if (Line.IndexOf("STL") != -1)
                        {
                            string name = Line.Substring(Line.IndexOf("(") + 1, Line.IndexOf(")") - Line.IndexOf("(") - 1);
                            tmp_inst = "inst \"" + name + "\"" + " \"SLICEL\" ," + "placed " + Small_ins + "  ,\n  cfg \" " +
                                "A5FFINIT::#OFF A5FFMUX::#OFF A5FFSR::#OFF A5LUT::#OFF A6LUT::#OFF\n" +
                                "\t   ACY0::#OFF AFF::#OFF AFFINIT::#OFF AFFMUX::#OFF AFFSR::#OFF AOUTMUX::#OFF\n" +
                                "\t   AUSED::#OFF B5FFINIT::#OFF B5FFMUX::#OFF B5FFSR::#OFF B5LUT::#OFF\n" +
                                "\t   B6LUT::#OFF BCY0::#OFF BFF::#OFF BFFINIT::#OFF BFFMUX::#OFF BFFSR::#OFF\n" +
                                "\t   BOUTMUX::#OFF BUSED::#OFF C5FFINIT::#OFF C5FFMUX::#OFF C5FFSR::#OFF\n" +
                                "\t   C5LUT::#OFF C6LUT::#OFF CCY0::#OFF CEUSEDMUX::#OFF CFF::#OFF\n" +
                                "\t   CFFINIT::#OFF CFFMUX::#OFF CFFSR::#OFF CLKINV::CLK COUTMUX::#OFF\n" +
                                "\t   COUTUSED::#OFF CUSED::#OFF D5FFINIT::#OFF D5FFMUX::#OFF D5FFSR::#OFF\n" +
                                "\t   D5LUT::#OFF D6LUT::#OFF DCY0::#OFF DFF::#OFF DFFINIT::#OFF\n" +
                                "\t   DFFMUX::#OFF DFFSR::#OFF DOUTMUX::#OFF DUSED::#OFF PRECYINIT::#OFF\n" +
                                "\t   SRUSEDMUX::#OFF SYNC_ATTR::SYNC \"\n  ;\n";
                            change(ref tmp_inst, Line);
                            int POS = 0;
                            POS = Coms953.IndexOf("inst \"XDL_");
                            Coms953 = Coms953.Insert(POS, tmp_inst);

                            int POS_CLK = 0;
                            POS_CLK = Coms953.IndexOf("net \"CK_BUFGP\"");
                            POS_CLK = Coms953.IndexOf("pip", POS_CLK);
                            if (name.IndexOf("-") == -1)
                            {
                                Coms953 = Coms953.Insert(POS_CLK, "inpin \"" + name + "\" CLK ,\n  ");
                                Coms953 = Coms953.Insert(POS_CLK, "inpin \"" + name + "_D" + "\" CLK ,\n  ");
                            }
                        }
                    }
                    count++;
                    Console.Write("alla" + count + "\n");
                }
            }
            //Coms953 = Coms953.Insert(Coms953.IndexOf("#  The syntax for nets is:") - 1

            Stream READ_ROUT;
            Stream READ_ROUT1;
            READ_ROUT = File.OpenRead(@"F:\MS\FinalProject\CODE\Duplicated_Routing.XDL");
            READ_ROUT1 = File.OpenRead(@"F:\MS\FinalProject\CODE\Duplicated_Routing.XDL");
            StreamReader XDL_ROUTING_FINAL = new StreamReader(READ_ROUT);
            StreamReader XDL_ROUT_FINAL = new StreamReader(READ_ROUT1);

            string ROUT = "";
            ROUT = XDL_ROUTING_FINAL.ReadToEnd().ToString();
             
            /////////here we want to rout the output of multiplexers based on main flipflop routing
            change_routing(XDL_ROUT_FINAL,ref Coms953,ROUT);
            //////////////////////////////////////////////// here we duplicate the regular net that was 
            ///////////////////////////////////////////////available but the outputpin might be changed
            Stream Compuls;
            Compuls = File.OpenWrite(@"F:\MS\FinalProject\CODE\COMPLULS.XDL");
            StreamWriter xdl_COMPLS = new StreamWriter(Compuls);
            xdl_COMPLS.Write(Coms953);
            xdl_COMPLS.Close();
            Compuls.Close();
            read_new_routing();

            ////Duplicate regular nets
            string Regular_nets = "";
            Stream Duplication_6;
            Duplication_6 = File.OpenRead(@"F:\MS\FinalProject\CODE\Tmps953NETCOMPLULS.XDL");
            StreamReader XDL_DUP_6 = new StreamReader(Duplication_6);
            Regular_nets = XDL_DUP_6.ReadToEnd().ToString();
            Regular_nets = Regular_nets.Replace("\" ", "_D\" ");

            Coms953 = Coms953.Insert(Coms953.IndexOf("net \"GLOBAL_LOGIC0_0") - 1, Regular_nets);

            ////////////////////////////////////////////////
            Stream Duplication_ROUT;
            Duplication_ROUT = File.OpenRead(@"F:\MS\FinalProject\CODE\Tmps953NETID.XDL");
            StreamReader XDL_DUP_ROUT = new StreamReader(Duplication_ROUT);

            int index_SEMI = 0;
            int index_first = 0;
            string DUP_ROUT = "";
            string temp = "";
            int index_D = 0;
            int index_first_D = 0;
            string temp_D = "";
            DUP_ROUT = XDL_DUP_ROUT.ReadToEnd().ToString();
            index_SEMI = DUP_ROUT.IndexOf(";");
            index_D = DUP_ROUT.IndexOf("_D\"");
            while (index_SEMI < DUP_ROUT.Length & index_SEMI != -1)
            {
                temp = DUP_ROUT.Substring(index_first + 1, index_SEMI - index_first - 1);
                temp_D = DUP_ROUT.Substring(index_first_D + 1, index_D - index_first_D);
                temp = temp.Substring(temp_D.LastIndexOf("inpin"));
                temp_D = temp_D.Substring(0, temp_D.LastIndexOf(","));
                temp_D = temp_D.Substring(temp_D.LastIndexOf("inpin"));
                index_first = index_SEMI;
                index_first_D = index_SEMI;                
                index_D = DUP_ROUT.IndexOf("_D\"", index_SEMI + 1);
                index_SEMI = DUP_ROUT.IndexOf(";", index_SEMI + 1);
                Coms953 = Coms953.Insert(Coms953.IndexOf(temp_D) + temp_D.Length + 5, temp);
            }

            Coms953 = Coms953.Insert(Coms953.IndexOf("net \"GLOBAL_LOGIC0_0") - 1, ROUT);
            //Coms953 = Coms953.Insert(Coms953.IndexOf("# SUMMARY") - 1, 

            XDL_F_s953.Write(Coms953);
            XDL_F_s953.Close();
            Changed_s953.Close();
            
            /////Read the Final file for start duplication, here we want to make a copy of used instanses
            
            int Border_WOR = 0;
            int TmpBorder_WOR = 0;
            int Border_ROW = 0;
            int TmpBorder_ROW = 0;
            int Border_WOR_min = 0;
            int Border_ROW_min = 0;
            int DELTA_WOR = 0;
            int DELTA_ROW = 0;
            string line = "";
            Stream Duplication;
            Duplication = File.OpenRead(@"F:\MS\FinalProject\CODE\Final.XDL");
            StreamReader XDL_DUP = new StreamReader(Duplication);

            ///////For duplication we shod first find the appropriate positions 
            ///////for this goal we shod find the rectangular borders 
            /////// and for this reason we want to duplicate the circuit at the right side (WEST) of the original circuit
            ///////So we are going to find the right border
            line = XDL_DUP.ReadLine();
            Border_WOR_min = Int16.Parse(line.Substring(line.IndexOf("WOR") + 3, line.IndexOf(" ", line.IndexOf("WOR")) - line.IndexOf("WOR") - 3));
            Border_ROW_min = Int16.Parse(line.Substring(line.IndexOf("ROW") + 3, line.IndexOf(" ", line.IndexOf("ROW")) - line.IndexOf("ROW") - 3));
            while (XDL_DUP.EndOfStream == false)
            {
                if (line.IndexOf("WOR") != -1)
                {
                    TmpBorder_WOR = Int16.Parse(line.Substring(line.IndexOf("WOR") + 3, line.IndexOf(" ", line.IndexOf("WOR")) - line.IndexOf("WOR") - 3));
                    TmpBorder_ROW = Int16.Parse(line.Substring(line.IndexOf("ROW") + 3, line.IndexOf(" ", line.IndexOf("ROW")) - line.IndexOf("ROW") - 3));
                    if (TmpBorder_WOR > Border_WOR) Border_WOR = TmpBorder_WOR;
                    if (TmpBorder_ROW > Border_ROW) Border_ROW = TmpBorder_ROW;
                    if (TmpBorder_WOR <= Border_WOR_min) Border_WOR_min = TmpBorder_WOR;
                    if (TmpBorder_ROW <= Border_ROW_min) Border_ROW_min = TmpBorder_ROW;
                }
                line = XDL_DUP.ReadLine();
            }
            if (Border_WOR % 2 == 0) DELTA_WOR = Border_WOR + 2;
            else DELTA_WOR = Border_WOR + 1;
            DELTA_ROW = Border_ROW + 1;

            Stream DUP_LAST;
            DUP_LAST = File.OpenRead(@"F:\MS\FinalProject\CODE\Ch_s953.XDL");
            StreamReader XDL_DUP_LAST = new StreamReader(DUP_LAST);

            Stream DUP_LAST_2;
            DUP_LAST_2 = File.OpenRead(@"F:\MS\FinalProject\CODE\Ch_s953.XDL");
            StreamReader XDL_DUP_LAST_2 = new StreamReader(DUP_LAST_2);

            string instances = ""; string INSTANCE = ""; string NET = "";
            string Circuit = ""; string nets = "";
            int Ind_Fir_inst = 0; int Ind_Fir_net = 0;
            int Ind_Las_inst = 0; int Ind_Las_net = 0;
            string Line_ch = "";
            string inst_tmp = ""; string net_tmp = "";
            string OL_ROW = "";
            string Nu_ROW = "";
            string OL_WOR = "";
            string Nu_WOR = "";
            int Ind_CLB = 0;
            int Ind_SLice = 0;
            string CLBSLICE = "";

            Circuit = XDL_DUP_LAST_2.ReadToEnd().ToString();

            XDL_DUP_LAST_2.Close();
            DUP_LAST_2.Close();

            Ind_Fir_inst = Circuit.IndexOf("placed CLB");
            Ind_Fir_inst = Circuit.IndexOf("inst ", Ind_Fir_inst - 150);
            Ind_Las_inst = Circuit.IndexOf("inst \"XDL_DUMMY");

           // instances = Circuit.Substring(Ind_Fir_inst, Ind_Las_inst - Ind_Fir_inst);
            instances = Circuit.Substring(0, Ind_Las_inst);

            Ind_Fir_net = Circuit.IndexOf("net \"");
            Ind_Las_net = Circuit.Length - 242;
            nets = Circuit.Substring(Ind_Fir_net, Ind_Las_net - Ind_Fir_net);

            Line_ch = XDL_DUP_LAST.ReadLine();

            string COLCOL = "";

            while (XDL_DUP_LAST.EndOfStream == false)
            {
                if (Line_ch.IndexOf("placed CLBL") != -1 & Line_ch.IndexOf("XDL_DUMMY") == -1)
                {
                    inst_tmp = instances.Substring(instances.IndexOf(Line_ch), instances.IndexOf(";", instances.IndexOf(Line_ch)) - instances.IndexOf(Line_ch) + 2);
                    inst_tmp = inst_tmp.Replace("\" \"", "_D\" \"");

                    if (Line_ch.IndexOf("CLBLM") != -1) Ind_CLB = Line_ch.IndexOf("CLBLM");
                    else if (Line_ch.IndexOf("CLBLL") != -1) Ind_CLB = Line_ch.IndexOf("CLBLL");

                    OL_ROW = Line_ch.Substring(Ind_CLB + 7, Line_ch.IndexOf("Y", Ind_CLB) - Ind_CLB - 7);
                    COLCOL = Line_ch.Substring(Line_ch.IndexOf("Y", Ind_CLB) + 1, Line_ch.IndexOf(" ", Ind_CLB) - Line_ch.IndexOf("Y", Ind_CLB) - 1);
                    if (2 * DELTA_ROW - Int16.Parse(OL_ROW) < 48 || 2 * DELTA_ROW - Int16.Parse(OL_ROW) > 53)
                        Nu_ROW = (2 * DELTA_ROW - Int16.Parse(OL_ROW)).ToString();
                    else if (Int16.Parse(COLCOL) > 79 & Int16.Parse(COLCOL) < 160)
                        Nu_ROW = (2 * DELTA_ROW - Int16.Parse(OL_ROW) + 6).ToString();
                    else
                        Nu_ROW = (2 * DELTA_ROW - Int16.Parse(OL_ROW)).ToString();

                    Ind_SLice = Line_ch.IndexOf("SLICE_X");

                    OL_WOR = Line_ch.Substring(Ind_SLice + 7, Line_ch.IndexOf("Y", Ind_SLice) - Ind_SLice - 7);

                    if (2 * DELTA_WOR - Int16.Parse(OL_WOR) < 72 || 2 * DELTA_WOR - Int16.Parse(OL_WOR) > 83)
                        Nu_WOR = (2 * DELTA_WOR - Int16.Parse(OL_WOR)).ToString();
                    else if (Int16.Parse(COLCOL) > 79 & Int16.Parse(COLCOL) < 160)
                        Nu_WOR = (2 * DELTA_WOR - Int16.Parse(OL_WOR) + 12).ToString();
                    else
                        Nu_WOR = (2 * DELTA_WOR - Int16.Parse(OL_WOR)).ToString();

                    CLBSLICE = Line_ch.Substring(Line_ch.IndexOf("Y"), Line_ch.IndexOf(" ", Line_ch.IndexOf("Y")) - Line_ch.IndexOf("Y"));

                    if (Line_ch.IndexOf("CLBLM") != -1)
                        inst_tmp = inst_tmp.Replace("M_X" + OL_ROW + CLBSLICE + " " + "SLICE_X" + OL_WOR,
                        "M_X" + Nu_ROW + CLBSLICE + " " + "SLICE_X" + Nu_WOR);
                    else
                        inst_tmp = inst_tmp.Replace("L_X" + OL_ROW + CLBSLICE + " " + "SLICE_X" + OL_WOR,
                    "L_X" + Nu_ROW + CLBSLICE + " " + "SLICE_X" + Nu_WOR);
                    INSTANCE = INSTANCE + inst_tmp;
                    Nu_WOR = ""; OL_WOR = ""; Nu_ROW = ""; OL_ROW = "";
                }
                else if (Line_ch.IndexOf("placed LIOB") != -1)
                {
                    inst_tmp = instances.Substring(instances.IndexOf(Line_ch),
                        instances.IndexOf(";", instances.IndexOf(Line_ch)) - instances.IndexOf(Line_ch) + 2);
                    if (inst_tmp.IndexOf("OUTBUF:") != -1)
                    {
                        inst_tmp = inst_tmp.Replace(": ", "_D: ");
                        inst_tmp = inst_tmp.Replace(":\r", "_D:\r");
                        inst_tmp = inst_tmp.Replace("\" \"", "_D\" \"");
                        INSTANCE = INSTANCE + inst_tmp;
                    }
                }
                else if (Line_ch.IndexOf("_BELSIG:PAD,PAD") != -1)
                {
                    net_tmp = nets.Substring(nets.IndexOf(Line_ch), nets.IndexOf(";", nets.IndexOf(Line_ch)) - nets.IndexOf(Line_ch) + 2);
                    net_tmp = net_tmp.Replace("\" ,", "_D\" ,");
                    net_tmp = net_tmp.Replace("\",", "_D\",");
                    net_tmp = net_tmp.Replace(":", "_D:");
                    NET = NET + net_tmp;
                }
                Line_ch = XDL_DUP_LAST.ReadLine();
              //  if (Line_ch.IndexOf("inst \"XDL_DUMMY") != -1) break;
            }
            XDL_DUP_LAST.Close();
            DUP_LAST.Close();

            /*          Circuit = XDL_DUP_LAST.ReadToEnd().ToString();


            

                      XDL_DUP_LAST.Close();
                      DUP_LAST.Close();

                      Ind_Fir_inst = Circuit.IndexOf("placed CLB");
                      Ind_Fir_inst = Circuit.IndexOf("inst ", Ind_Fir_inst - 150);
                      Ind_Las_inst = Circuit.IndexOf("inst \"XDL_DUMMY");
            
                      instances = Circuit.Substring(Ind_Fir_inst, Ind_Las_inst - Ind_Fir_inst);
                      ////duplicate the instances names
                      instances = instances.Replace("\" \"", "_D\" \"");
                      /////copy in new position(change the row and the wor)
                      int Ind_CLB = 0;
                      int Ind_SLice = 0;
                      string OL_ROW = "";
                      string Nu_ROW = "";
                      string OL_WOR = "";
                      string Nu_WOR = "";
                      string CLBSLICE = "";
                      while (Ind_CLB < instances.Length & Ind_CLB != -1)
                      {
                          if (instances.IndexOf("CLBLM",Ind_CLB) != -1 || instances.IndexOf("CLBLL",Ind_CLB) != -1)
                          {
                              if (instances.IndexOf("CLBLM", Ind_CLB, 350) != -1) Ind_CLB = instances.IndexOf("CLBLM", Ind_CLB, 350);
                              else if (instances.IndexOf("CLBLL", Ind_CLB, 350) != -1) Ind_CLB = instances.IndexOf("CLBLL", Ind_CLB, 350);
                              OL_ROW = instances.Substring(Ind_CLB + 7, instances.IndexOf("Y", Ind_CLB) - Ind_CLB - 7);

                              if (Int16.Parse(OL_ROW) + DELTA_ROW < 48 || Int16.Parse(OL_ROW) + DELTA_ROW > 35)
                                  Nu_ROW = (Int16.Parse(OL_ROW) + DELTA_ROW).ToString(); 
                              else
                                  Nu_ROW = (Int16.Parse(OL_ROW) + DELTA_ROW + 6).ToString();

                              Ind_SLice = instances.IndexOf("SLICE_X", Ind_CLB, 350);

                              OL_WOR = instances.Substring(Ind_SLice + 7, instances.IndexOf("Y", Ind_SLice, 350) - Ind_SLice - 7);

                              if (Int16.Parse(OL_ROW) + DELTA_ROW < 72 || Int16.Parse(OL_ROW) + DELTA_ROW > 83)
                                  Nu_WOR = (Int16.Parse(OL_WOR) + DELTA_WOR).ToString();
                              else
                                  Nu_WOR = (Int16.Parse(OL_WOR) + DELTA_WOR + 12).ToString();

                              CLBSLICE = instances.Substring(instances.IndexOf("Y", Ind_CLB, 350), instances.IndexOf(" ", instances.IndexOf("Y", Ind_CLB, 500)) - instances.IndexOf("Y", Ind_CLB, 500));

                              if (instances.IndexOf("CLBLM", Ind_CLB, 350) != -1)
                                  instances = instances.Replace("M_X" + OL_ROW + CLBSLICE + " " + "SLICE_X" + OL_WOR,
                                  "M_X" + Nu_ROW + CLBSLICE + " " + "SLICE_X" + Nu_WOR);
                              else
                                  instances = instances.Replace("L_X" + OL_ROW + CLBSLICE + " " + "SLICE_X" + OL_WOR,
                              "L_X" + Nu_ROW + CLBSLICE + " " + "SLICE_X" + Nu_WOR);
                          }
                          Ind_CLB = instances.IndexOf("inst ", Ind_CLB);
                      }
          */
            ////save new changes 
            Stream SAVE;
            SAVE = File.OpenWrite(@"F:\MS\FinalProject\CODE\Instances.XDL");
            StreamWriter XDL_SAVE = new StreamWriter(SAVE);

            XDL_SAVE.Write(INSTANCE);
            XDL_SAVE.Close();
            SAVE.Close();

            Stream LAST;
            LAST = File.OpenWrite(@"F:\MS\FinalProject\CODE\Ch_s953.XDL");
            StreamWriter XDL_LAST = new StreamWriter(LAST);

            Circuit = Circuit.Insert(Ind_Las_inst, INSTANCE);
            Circuit = Circuit.Insert(Circuit.Length - 247, NET);
            XDL_LAST.Write(Circuit);
            XDL_LAST.Close();
            LAST.Close();

            ////Close the file and its stream
            XDL_DUP.Close();
            Duplication.Close();
        }

        private void changeBB(ref string tmp_inst, string Line)
        {
            /////CONSTRUCION OF FFs
            if (tmp_inst.IndexOf("AFF::#OFF") != -1 & (Line.IndexOf("1bb H AFF") != -1 || Line.IndexOf("2bb H AFF") != -1
                                                      || Line.IndexOf("3bb H AFF") != -1 || Line.IndexOf("4bb H AFF") != -1
                                                      || Line.IndexOf("5bb H AFF") != -1 || Line.IndexOf("6bb H AFF") != -1
                                                      || Line.IndexOf("7bb H AFF") != -1 || Line.IndexOf("8bb H AFF") != -1))
            {
                tmp_inst = tmp_inst.Replace("AFF::#OFF", "AFF:" + "HisTaf" + Inst_num + ":#FF");
                tmp_inst = tmp_inst.Replace("AFFINIT::#OFF", "AFFINIT::INIT0");
                tmp_inst = tmp_inst.Replace("AFFSR::#OFF", "AFFSR::SRLOW");
                tmp_inst = tmp_inst.Replace("AFFMUX::#OFF", "AFFMUX::AX");
            }
            if (tmp_inst.IndexOf("BFF::#OFF") != -1 & (Line.IndexOf("1bb H BFF") != -1 || Line.IndexOf("2bb H BFF") != -1
                                                      || Line.IndexOf("3bb H BFF") != -1 || Line.IndexOf("4bb H BFF") != -1
                                                      || Line.IndexOf("5bb H BFF") != -1 || Line.IndexOf("6bb H BFF") != -1
                                                      || Line.IndexOf("7bb H BFF") != -1 || Line.IndexOf("8bb H BFF") != -1))
            {
                tmp_inst = tmp_inst.Replace("BFF::#OFF", "BFF:" + "HisTbf" + Inst_num + ":#FF");
                tmp_inst = tmp_inst.Replace("BFFINIT::#OFF", "BFFINIT::INIT0");
                tmp_inst = tmp_inst.Replace("BFFSR::#OFF", "BFFSR::SRLOW");
                tmp_inst = tmp_inst.Replace("BFFMUX::#OFF", "BFFMUX::BX");
            }
            if (tmp_inst.IndexOf("CFF::#OFF") != -1 & (Line.IndexOf("1bb H CFF") != -1 || Line.IndexOf("2bb H CFF") != -1
                                                      || Line.IndexOf("3bb H CFF") != -1 || Line.IndexOf("4bb H CFF") != -1
                                                      || Line.IndexOf("5bb H CFF") != -1 || Line.IndexOf("6bb H CFF") != -1
                                                      || Line.IndexOf("7bb H CFF") != -1 || Line.IndexOf("8bb H CFF") != -1))
            {
                tmp_inst = tmp_inst.Replace("CFF::#OFF", "CFF:" + "HisTcf" + Inst_num + ":#FF");
                tmp_inst = tmp_inst.Replace("CFFINIT::#OFF", "CFFINIT::INIT0");
                tmp_inst = tmp_inst.Replace("CFFSR::#OFF", "CFFSR::SRLOW");
                tmp_inst = tmp_inst.Replace("CFFMUX::#OFF", "CFFMUX::CX");
            }
            if (tmp_inst.IndexOf("DFF::#OFF") != -1 & (Line.IndexOf("1bb H DFF") != -1 || Line.IndexOf("2bb H DFF") != -1
                                                      || Line.IndexOf("3bb H DFF") != -1 || Line.IndexOf("4bb H DFF") != -1
                                                      || Line.IndexOf("5bb H DFF") != -1 || Line.IndexOf("6bb H DFF") != -1
                                                      || Line.IndexOf("7bb H DFF") != -1 || Line.IndexOf("8bb H DFF") != -1))
            {
                tmp_inst = tmp_inst.Replace("DFF::#OFF", "DFF:" + "HisTdf" + Inst_num + ":#FF");
                tmp_inst = tmp_inst.Replace("DFFINIT::#OFF", "DFFINIT::INIT0");
                tmp_inst = tmp_inst.Replace("DFFSR::#OFF", "DFFSR::SRLOW");
                tmp_inst = tmp_inst.Replace("DFFMUX::#OFF", "DFFMUX::DX");
            }
            /////CONSTRUCION OF 5FFs
            if (tmp_inst.IndexOf("A5FFINIT::#OFF") != -1 & (Line.IndexOf("1bb H A5FF") != -1 || Line.IndexOf("2bb H A5FF") != -1
                                                      || Line.IndexOf("3bb H A5FF") != -1 || Line.IndexOf("4bb H A5FF") != -1
                                                      || Line.IndexOf("5bb H A5FF") != -1 || Line.IndexOf("6bb H A5FF") != -1
                                                      || Line.IndexOf("7bb H A5FF") != -1 || Line.IndexOf("8bb H AFF") != -1))
            {
                tmp_inst = tmp_inst.Replace("A5FFINIT::#OFF", "A5FFINIT::INIT0");
                tmp_inst = tmp_inst.Replace("A5FFSR::#OFF", "A5FFSR::SRLOW");
                tmp_inst = tmp_inst.Replace("AOUTMUX::#OFF", "AOUTMUX::A5Q");
                tmp_inst = tmp_inst.Replace("A5FFMUX::#OFF", "A5FFMUX::IN_B");
            }
            if (tmp_inst.IndexOf("B5FFINIT::#OFF") != -1 & (Line.IndexOf("1bb H B5FF") != -1 || Line.IndexOf("2bb H B5FF") != -1
                                                      || Line.IndexOf("3bb H B5FF") != -1 || Line.IndexOf("4bb H B5FF") != -1
                                                      || Line.IndexOf("5bb H B5FF") != -1 || Line.IndexOf("6bb H B5FF") != -1
                                                      || Line.IndexOf("7bb H B5FF") != -1 || Line.IndexOf("8bb H B5FF") != -1))
            {
                tmp_inst = tmp_inst.Replace("B5FFINIT::#OFF", "B5FFINIT::INIT0");
                tmp_inst = tmp_inst.Replace("B5FFSR::#OFF", "B5FFSR::SRLOW");
                tmp_inst = tmp_inst.Replace("BOUTMUX::#OFF", "BOUTMUX::B5Q");
                tmp_inst = tmp_inst.Replace("B5FFMUX::#OFF", "B5FFMUX::IN_B");
            }
            if (tmp_inst.IndexOf("C5FFINIT::#OFF") != -1 & (Line.IndexOf("1bb H C5FF") != -1 || Line.IndexOf("2bb H C5FF") != -1
                                                      || Line.IndexOf("3bb H C5FF") != -1 || Line.IndexOf("4bb H C5FF") != -1
                                                      || Line.IndexOf("5bb H C5FF") != -1 || Line.IndexOf("6bb H C5FF") != -1
                                                      || Line.IndexOf("7bb H C5FF") != -1 || Line.IndexOf("8bb H C5FF") != -1))
            {
                tmp_inst = tmp_inst.Replace("C5FFINIT::#OFF", "C5FFINIT::INIT0");
                tmp_inst = tmp_inst.Replace("C5FFSR::#OFF", "C5FFSR::SRLOW");
                tmp_inst = tmp_inst.Replace("COUTMUX::#OFF", "COUTMUX::C5Q");
                tmp_inst = tmp_inst.Replace("C5FFMUX::#OFF", "C5FFMUX::IN_B");
            }
            if (tmp_inst.IndexOf("D5FFINIT::#OFF") != -1 & (Line.IndexOf("1bb H D5FF") != -1 || Line.IndexOf("2bb H D5FF") != -1
                                                      || Line.IndexOf("3bb H D5FF") != -1 || Line.IndexOf("4bb H D5FF") != -1
                                                      || Line.IndexOf("5bb H D5FF") != -1 || Line.IndexOf("6bb H D5FF") != -1
                                                      || Line.IndexOf("7bb H D5FF") != -1 || Line.IndexOf("8bb H D5FF") != -1))
            {
                tmp_inst = tmp_inst.Replace("D5FFINIT::#OFF", "D5FFINIT::INIT0");
                tmp_inst = tmp_inst.Replace("D5FFSR::#OFF", "D5FFSR::SRLOW");
                tmp_inst = tmp_inst.Replace("DOUTMUX::#OFF", "DOUTMUX::D5Q");
                tmp_inst = tmp_inst.Replace("D5FFMUX::#OFF", "D5FFMUX::IN_B");
            }
        }

        private void changeB(ref string tmp_inst, string Line)
        {       
            /////CONSTRUCION OF FFs
            if (tmp_inst.IndexOf("AFF::#OFF") != -1 & (Line.IndexOf("1b H AFF") != -1 || Line.IndexOf("2b H AFF") != -1
                                                      || Line.IndexOf("3b H AFF") != -1 || Line.IndexOf("4b H AFF") != -1
                                                      || Line.IndexOf("5b H AFF") != -1 || Line.IndexOf("6b H AFF") != -1
                                                      || Line.IndexOf("7b H AFF") != -1 || Line.IndexOf("8b H AFF") != -1))
            {
                tmp_inst = tmp_inst.Replace("AFF::#OFF", "AFF:" + "HisTaf" + Inst_num + ":#FF");
                tmp_inst = tmp_inst.Replace("AFFINIT::#OFF", "AFFINIT::INIT0");
                tmp_inst = tmp_inst.Replace("AFFSR::#OFF", "AFFSR::SRLOW");
                tmp_inst = tmp_inst.Replace("AFFMUX::#OFF", "AFFMUX::AX");
            }
            if (tmp_inst.IndexOf("BFF::#OFF") != -1 & (Line.IndexOf("1b H BFF") != -1 || Line.IndexOf("2b H BFF") != -1
                                                      || Line.IndexOf("3b H BFF") != -1 || Line.IndexOf("4b H BFF") != -1
                                                      || Line.IndexOf("5b H BFF") != -1 || Line.IndexOf("6b H BFF") != -1
                                                      || Line.IndexOf("7b H BFF") != -1 || Line.IndexOf("8b H BFF") != -1))
            {
                tmp_inst = tmp_inst.Replace("BFF::#OFF", "BFF:" + "HisTbf" + Inst_num + ":#FF");
                tmp_inst = tmp_inst.Replace("BFFINIT::#OFF", "BFFINIT::INIT0");
                tmp_inst = tmp_inst.Replace("BFFSR::#OFF", "BFFSR::SRLOW");
                tmp_inst = tmp_inst.Replace("BFFMUX::#OFF", "BFFMUX::BX");
            }
            if (tmp_inst.IndexOf("CFF::#OFF") != -1 & (Line.IndexOf("1b H CFF") != -1 || Line.IndexOf("2b H CFF") != -1
                                                      || Line.IndexOf("3b H CFF") != -1 || Line.IndexOf("4b H CFF") != -1
                                                      || Line.IndexOf("5b H CFF") != -1 || Line.IndexOf("6b H CFF") != -1
                                                      || Line.IndexOf("7b H CFF") != -1 || Line.IndexOf("8b H CFF") != -1))
            {
                tmp_inst = tmp_inst.Replace("CFF::#OFF", "CFF:" + "HisTcf" + Inst_num + ":#FF");
                tmp_inst = tmp_inst.Replace("CFFINIT::#OFF", "CFFINIT::INIT0");
                tmp_inst = tmp_inst.Replace("CFFSR::#OFF", "CFFSR::SRLOW");
                tmp_inst = tmp_inst.Replace("CFFMUX::#OFF", "CFFMUX::CX");
            }
            if (tmp_inst.IndexOf("DFF::#OFF") != -1 & (Line.IndexOf("1b H DFF") != -1 || Line.IndexOf("2b H DFF") != -1
                                                      || Line.IndexOf("3b H DFF") != -1 || Line.IndexOf("4b H DFF") != -1
                                                      || Line.IndexOf("5b H DFF") != -1 || Line.IndexOf("6b H DFF") != -1
                                                      || Line.IndexOf("7b H DFF") != -1 || Line.IndexOf("8b H DFF") != -1))
            {
                tmp_inst = tmp_inst.Replace("DFF::#OFF", "DFF:" + "HisTdf" + Inst_num + ":#FF");
                tmp_inst = tmp_inst.Replace("DFFINIT::#OFF", "DFFINIT::INIT0");
                tmp_inst = tmp_inst.Replace("DFFSR::#OFF", "DFFSR::SRLOW");
                tmp_inst = tmp_inst.Replace("DFFMUX::#OFF", "DFFMUX::DX");
            }
            /////CONSTRUCION OF 5FFs
            if (tmp_inst.IndexOf("A5FFINIT::#OFF") != -1 & (Line.IndexOf("1b H A5FF") != -1 || Line.IndexOf("2b H A5FF") != -1
                                                      || Line.IndexOf("3b H A5FF") != -1 || Line.IndexOf("4b H A5FF") != -1
                                                      || Line.IndexOf("5b H A5FF") != -1 || Line.IndexOf("6b H A5FF") != -1
                                                      || Line.IndexOf("7b H A5FF") != -1 || Line.IndexOf("8b H AFF") != -1))
            {
                tmp_inst = tmp_inst.Replace("A5FFINIT::#OFF", "A5FFINIT::INIT0");
                tmp_inst = tmp_inst.Replace("A5FFSR::#OFF", "A5FFSR::SRLOW");
                tmp_inst = tmp_inst.Replace("AOUTMUX::#OFF", "AOUTMUX::A5Q");
                tmp_inst = tmp_inst.Replace("A5FFMUX::#OFF", "A5FFMUX::IN_B");
            }
            if (tmp_inst.IndexOf("B5FFINIT::#OFF") != -1 & (Line.IndexOf("1b H B5FF") != -1 || Line.IndexOf("2b H B5FF") != -1
                                                      || Line.IndexOf("3b H B5FF") != -1 || Line.IndexOf("4b H B5FF") != -1
                                                      || Line.IndexOf("5b H B5FF") != -1 || Line.IndexOf("6b H B5FF") != -1
                                                      || Line.IndexOf("7b H B5FF") != -1 || Line.IndexOf("8b H B5FF") != -1))
            {
                tmp_inst = tmp_inst.Replace("B5FFINIT::#OFF", "B5FFINIT::INIT0");
                tmp_inst = tmp_inst.Replace("B5FFSR::#OFF", "B5FFSR::SRLOW");
                tmp_inst = tmp_inst.Replace("BOUTMUX::#OFF", "BOUTMUX::B5Q");
                tmp_inst = tmp_inst.Replace("B5FFMUX::#OFF", "B5FFMUX::IN_B");
            }
            if (tmp_inst.IndexOf("C5FFINIT::#OFF") != -1 & (Line.IndexOf("1b H C5FF") != -1 || Line.IndexOf("2b H C5FF") != -1
                                                      || Line.IndexOf("3b H C5FF") != -1 || Line.IndexOf("4b H C5FF") != -1
                                                      || Line.IndexOf("5b H C5FF") != -1 || Line.IndexOf("6b H C5FF") != -1
                                                      || Line.IndexOf("7b H C5FF") != -1 || Line.IndexOf("8b H C5FF") != -1))
            {
                tmp_inst = tmp_inst.Replace("C5FFINIT::#OFF", "C5FFINIT::INIT0");
                tmp_inst = tmp_inst.Replace("C5FFSR::#OFF", "C5FFSR::SRLOW");
                tmp_inst = tmp_inst.Replace("COUTMUX::#OFF", "COUTMUX::C5Q");
                tmp_inst = tmp_inst.Replace("C5FFMUX::#OFF", "C5FFMUX::IN_B");
            }
            if (tmp_inst.IndexOf("D5FFINIT::#OFF") != -1 & (Line.IndexOf("1b H D5FF") != -1 || Line.IndexOf("2b H D5FF") != -1
                                                      || Line.IndexOf("3b H D5FF") != -1 || Line.IndexOf("4b H D5FF") != -1
                                                      || Line.IndexOf("5b H D5FF") != -1 || Line.IndexOf("6b H D5FF") != -1
                                                      || Line.IndexOf("7b H D5FF") != -1 || Line.IndexOf("8b H D5FF") != -1))
            {
                tmp_inst = tmp_inst.Replace("D5FFINIT::#OFF", "D5FFINIT::INIT0");
                tmp_inst = tmp_inst.Replace("D5FFSR::#OFF", "D5FFSR::SRLOW");
                tmp_inst = tmp_inst.Replace("DOUTMUX::#OFF", "DOUTMUX::D5Q");
                tmp_inst = tmp_inst.Replace("D5FFMUX::#OFF", "D5FFMUX::IN_B");
            }                
        }

        private void change(ref string tmp_inst, string Line)
        {
                //////CONSTRUTION OF Upper Muxes
                if (tmp_inst.IndexOf("A6LUT::#OFF") != -1 & Line.IndexOf("A6LUT") != -1)
                    if (Line.IndexOf("A6LUT, INB") == -1 & Line.IndexOf("A6LUT, N") == -1
                        & Line.IndexOf("A6LUT, S") == -1 & Line.IndexOf("A6LUT, W") == -1 & Line.IndexOf("A6LUT, E") == -1)
                {                    
                    tmp_inst = tmp_inst.Replace("A6LUT::#OFF", "A6LUT:" + "HisTa" + Inst_num + ":#LUT:O6=((~A1*A3)+(A1*A2))");
                    tmp_inst = tmp_inst.Replace("AUSED::#OFF", "AUSED::0");
                }
                if (tmp_inst.IndexOf("B6LUT::#OFF") != -1 & Line.IndexOf("B6LUT") != -1)
                    if (Line.IndexOf("B6LUT, INB") == -1 & Line.IndexOf("B6LUT, N") == -1
                        & Line.IndexOf("B6LUT, S") == -1 & Line.IndexOf("B6LUT, W") == -1 & Line.IndexOf("B6LUT, E") == -1)
                {
                    tmp_inst = tmp_inst.Replace("B6LUT::#OFF", "B6LUT:" + "HisTb" + Inst_num + ":#LUT:O6=((~A1*A3)+(A1*A2))");
                    tmp_inst = tmp_inst.Replace("BUSED::#OFF", "BUSED::0");
                }
                if (tmp_inst.IndexOf("C6LUT::#OFF") != -1 & Line.IndexOf("C6LUT") != -1)
                    if (Line.IndexOf("C6LUT, INB") == -1 & Line.IndexOf("C6LUT, N") == -1
                        & Line.IndexOf("C6LUT, S") == -1 & Line.IndexOf("C6LUT, W") == -1 & Line.IndexOf("C6LUT, E") == -1)
                {
                    tmp_inst = tmp_inst.Replace("C6LUT::#OFF", "C6LUT:" + "HisTc" + Inst_num + ":#LUT:O6=((~A1*A3)+(A1*A2))");
                    tmp_inst = tmp_inst.Replace("CUSED::#OFF", "CUSED::0");
                }
                if (tmp_inst.IndexOf("D6LUT::#OFF") != -1 & Line.IndexOf("D6LUT") != -1)
                    if (Line.IndexOf("D6LUT, INB") == -1 & Line.IndexOf("D6LUT, N") == -1
                        & Line.IndexOf("D6LUT, S") == -1 & Line.IndexOf("D6LUT, W") == -1 & Line.IndexOf("D6LUT, E") == -1)
                {
                    tmp_inst = tmp_inst.Replace("D6LUT::#OFF", "D6LUT:" + "HisTd" + Inst_num + ":#LUT:O6=((~A1*A3)+(A1*A2))");
                    tmp_inst = tmp_inst.Replace("DUSED::#OFF", "DUSED::0");
                }
                ////CONSTRUCTION OF underneath Muxes
                if (tmp_inst.IndexOf("A5LUT::#OFF") != -1 & Line.IndexOf("A5LUT") != -1 & Line.IndexOf("H A5LUT") == -1)
                    if (Line.IndexOf("A5LUT, INB") == -1 & Line.IndexOf("A5LUT, N") == -1
                        & Line.IndexOf("A5LUT, S") == -1 & Line.IndexOf("A5LUT, W") == -1 & Line.IndexOf("A5LUT, E") == -1)
                {
                    tmp_inst = tmp_inst.Replace("A5LUT::#OFF", "A5LUT:" + "HisTa5" + Inst_num + ":#LUT:O5=((~A1*A5)+(A1*A4))");
                    tmp_inst = tmp_inst.Replace("AOUTMUX::#OFF", "AOUTMUX::O5");
                }
                if (tmp_inst.IndexOf("B5LUT::#OFF") != -1 & Line.IndexOf("B5LUT") != -1 & Line.IndexOf("H B5LUT") == -1)
                    if (Line.IndexOf("B5LUT, INB") == -1 & Line.IndexOf("B5LUT, N") == -1
                        & Line.IndexOf("B5LUT, S") == -1 & Line.IndexOf("B5LUT, W") == -1 & Line.IndexOf("B5LUT, E") == -1)
                {
                    tmp_inst = tmp_inst.Replace("B5LUT::#OFF", "B5LUT:" + "HisTb5" + Inst_num + ":#LUT:O5=((~A1*A5)+(A1*A4))");
                    tmp_inst = tmp_inst.Replace("BOUTMUX::#OFF", "BOUTMUX::O5");
                }
                if (tmp_inst.IndexOf("C5LUT::#OFF") != -1 & Line.IndexOf("C5LUT") != -1 & Line.IndexOf("H C5LUT") == -1)
                    if (Line.IndexOf("C5LUT, INB") == -1 & Line.IndexOf("C5LUT, N") == -1
                        & Line.IndexOf("C5LUT, S") == -1 & Line.IndexOf("C5LUT, W") == -1 & Line.IndexOf("C5LUT, E") == -1)
                {
                    tmp_inst = tmp_inst.Replace("C5LUT::#OFF", "C5LUT:" + "HisTc5" + Inst_num + ":#LUT:O5=((~A1*A5)+(A1*A4))");
                    tmp_inst = tmp_inst.Replace("COUTMUX::#OFF", "COUTMUX::O5");
                }
                if (tmp_inst.IndexOf("D5LUT::#OFF") != -1 & Line.IndexOf("D5LUT") != -1 & Line.IndexOf("H D5LUT") == -1)
                    if (Line.IndexOf("D5LUT, INB") == -1 & Line.IndexOf("D5LUT, N") == -1
                        & Line.IndexOf("D5LUT, S") == -1 & Line.IndexOf("D5LUT, W") == -1 & Line.IndexOf("D5LUT, E") == -1)
                {
                    tmp_inst = tmp_inst.Replace("D5LUT::#OFF", "D5LUT:" + "HisTd5" + Inst_num + ":#LUT:O5=((~A1*A5)+(A1*A4))");
                    tmp_inst = tmp_inst.Replace("DOUTMUX::#OFF", "DOUTMUX::O5");
                }
            ////////luts that are used as a buffer for ff
                if (tmp_inst.IndexOf("A5LUT::#OFF") != -1 & Line.IndexOf("H A5LUT") != -1)
                    if (Line.IndexOf("A5LUT, INB") == -1 & Line.IndexOf("A5LUT, N") == -1
                        & Line.IndexOf("A5LUT, S") == -1 & Line.IndexOf("A5LUT, W") == -1 & Line.IndexOf("A5LUT, E") == -1)
                    {
                        tmp_inst = tmp_inst.Replace("A5LUT::#OFF", "A5LUT:" + "BUFA5" + Inst_num + ":#LUT:O5=(A1)");
                        tmp_inst = tmp_inst.Replace("A5FFINIT::#OFF", "A5FFINIT::INIT0");
                        tmp_inst = tmp_inst.Replace("A5FFSR::#OFF", "A5FFSR::SRLOW");
                        tmp_inst = tmp_inst.Replace("AOUTMUX::#OFF", "AOUTMUX::A5Q");
                        tmp_inst = tmp_inst.Replace("A5FFMUX::#OFF", "A5FFMUX::IN_A");
                    }
                if (tmp_inst.IndexOf("B5LUT::#OFF") != -1 & Line.IndexOf("H B5LUT") != -1)
                    if (Line.IndexOf("B5LUT, INB") == -1 & Line.IndexOf("B5LUT, N") == -1
                        & Line.IndexOf("B5LUT, S") == -1 & Line.IndexOf("B5LUT, W") == -1 & Line.IndexOf("B5LUT, E") == -1)
                    {
                        tmp_inst = tmp_inst.Replace("B5LUT::#OFF", "B5LUT:" + "BUFB5" + Inst_num + ":#LUT:O5=(A1)");
                        tmp_inst = tmp_inst.Replace("B5FFINIT::#OFF", "B5FFINIT::INIT0");
                        tmp_inst = tmp_inst.Replace("B5FFSR::#OFF", "B5FFSR::SRLOW");
                        tmp_inst = tmp_inst.Replace("BOUTMUX::#OFF", "BOUTMUX::B5Q");
                        tmp_inst = tmp_inst.Replace("B5FFMUX::#OFF", "B5FFMUX::IN_A");
                    }
                if (tmp_inst.IndexOf("C5LUT::#OFF") != -1 & Line.IndexOf("H C5LUT") != -1)
                    if (Line.IndexOf("C5LUT, INB") == -1 & Line.IndexOf("C5LUT, N") == -1
                        & Line.IndexOf("C5LUT, S") == -1 & Line.IndexOf("C5LUT, W") == -1 & Line.IndexOf("C5LUT, E") == -1)
                    {
                        tmp_inst = tmp_inst.Replace("C5LUT::#OFF", "C5LUT:" + "BUFC5" + Inst_num + ":#LUT:O5=(A1)");
                        tmp_inst = tmp_inst.Replace("C5FFINIT::#OFF", "C5FFINIT::INIT0");
                        tmp_inst = tmp_inst.Replace("C5FFSR::#OFF", "C5FFSR::SRLOW");
                        tmp_inst = tmp_inst.Replace("COUTMUX::#OFF", "COUTMUX::C5Q");
                        tmp_inst = tmp_inst.Replace("C5FFMUX::#OFF", "C5FFMUX::IN_A");
                    }
                if (tmp_inst.IndexOf("D5LUT::#OFF") != -1 & Line.IndexOf("H D5LUT") != -1)
                    if (Line.IndexOf("D5LUT, INB") == -1 & Line.IndexOf("D5LUT, N") == -1
                        & Line.IndexOf("D5LUT, S") == -1 & Line.IndexOf("D5LUT, W") == -1 & Line.IndexOf("D5LUT, E") == -1)
                    {
                        tmp_inst = tmp_inst.Replace("D5LUT::#OFF", "D5LUT:" + "BUFD5" + Inst_num + ":#LUT:O5=(A1)");
                        tmp_inst = tmp_inst.Replace("D5FFINIT::#OFF", "D5FFINIT::INIT0");
                        tmp_inst = tmp_inst.Replace("D5FFSR::#OFF", "D5FFSR::SRLOW");
                        tmp_inst = tmp_inst.Replace("DOUTMUX::#OFF", "DOUTMUX::D5Q");
                        tmp_inst = tmp_inst.Replace("D5FFMUX::#OFF", "D5FFMUX::IN_A");
                    }
                /////CONSTRUCION OF FFs
                if (tmp_inst.IndexOf("AFF::#OFF") != -1 & Line.IndexOf("H AFF") != -1 & Line.IndexOf("b H AFF") == -1)
                {
                    tmp_inst = tmp_inst.Replace("AFF::#OFF", "AFF:" + "HisTaf" + Inst_num + ":#FF");
                    tmp_inst = tmp_inst.Replace("AFFINIT::#OFF" ,"AFFINIT::INIT0");
                    tmp_inst = tmp_inst.Replace("AFFSR::#OFF", "AFFSR::SRLOW");
                    tmp_inst = tmp_inst.Replace("AFFMUX::#OFF", "AFFMUX::AX");
                }
                if (tmp_inst.IndexOf("BFF::#OFF") != -1 & Line.IndexOf("H BFF") != -1 & Line.IndexOf("b H BFF") == -1)
                {
                    tmp_inst = tmp_inst.Replace("BFF::#OFF", "BFF:" + "HisTbf" + Inst_num + ":#FF");
                    tmp_inst = tmp_inst.Replace("BFFINIT::#OFF", "BFFINIT::INIT0");
                    tmp_inst = tmp_inst.Replace("BFFSR::#OFF", "BFFSR::SRLOW");
                    tmp_inst = tmp_inst.Replace("BFFMUX::#OFF", "BFFMUX::BX");
                }
                if (tmp_inst.IndexOf("CFF::#OFF") != -1 & Line.IndexOf("H CFF") != -1 & Line.IndexOf("b H CFF") == -1)
                {
                    tmp_inst = tmp_inst.Replace("CFF::#OFF", "CFF:" + "HisTcf" + Inst_num + ":#FF");
                    tmp_inst = tmp_inst.Replace("CFFINIT::#OFF", "CFFINIT::INIT0");
                    tmp_inst = tmp_inst.Replace("CFFSR::#OFF", "CFFSR::SRLOW");
                    tmp_inst = tmp_inst.Replace("CFFMUX::#OFF", "CFFMUX::CX");
                }
                if (tmp_inst.IndexOf("DFF::#OFF") != -1 & Line.IndexOf("H DFF") != -1 & Line.IndexOf("b H DFF") == -1)
                {
                    tmp_inst = tmp_inst.Replace("DFF::#OFF", "DFF:" + "HisTdf" + Inst_num + ":#FF");
                    tmp_inst = tmp_inst.Replace("DFFINIT::#OFF", "DFFINIT::INIT0");
                    tmp_inst = tmp_inst.Replace("DFFSR::#OFF", "DFFSR::SRLOW");
                    tmp_inst = tmp_inst.Replace("DFFMUX::#OFF", "DFFMUX::DX");
                }
                /////CONSTRUCION OF 5FFs
                if (tmp_inst.IndexOf("A5FFINIT::#OFF") != -1 & Line.IndexOf("H A5FF") != -1 & Line.IndexOf("b H A5FF") == -1)
                {
                    tmp_inst = tmp_inst.Replace("A5FFINIT::#OFF", "A5FFINIT::INIT0");
                    tmp_inst = tmp_inst.Replace("A5FFSR::#OFF", "A5FFSR::SRLOW");
                    tmp_inst = tmp_inst.Replace("AOUTMUX::#OFF", "AOUTMUX::A5Q");
                    tmp_inst = tmp_inst.Replace("A5FFMUX::#OFF", "A5FFMUX::IN_B");
                }
                if (tmp_inst.IndexOf("B5FFINIT::#OFF") != -1 & Line.IndexOf("H B5FF") != -1 & Line.IndexOf("b H B5FF") == -1)
                {
                    tmp_inst = tmp_inst.Replace("B5FFINIT::#OFF", "B5FFINIT::INIT0");
                    tmp_inst = tmp_inst.Replace("B5FFSR::#OFF", "B5FFSR::SRLOW");
                    tmp_inst = tmp_inst.Replace("BOUTMUX::#OFF", "BOUTMUX::B5Q");
                    tmp_inst = tmp_inst.Replace("B5FFMUX::#OFF", "B5FFMUX::IN_B");
                }
                if (tmp_inst.IndexOf("C5FFINIT::#OFF") != -1 & Line.IndexOf("H C5FF") != -1 & Line.IndexOf("b H C5FF") == -1)
                {
                    tmp_inst = tmp_inst.Replace("C5FFINIT::#OFF", "C5FFINIT::INIT0");
                    tmp_inst = tmp_inst.Replace("C5FFSR::#OFF", "C5FFSR::SRLOW");
                    tmp_inst = tmp_inst.Replace("COUTMUX::#OFF", "COUTMUX::C5Q");
                    tmp_inst = tmp_inst.Replace("C5FFMUX::#OFF", "C5FFMUX::IN_B");
                }
                if (tmp_inst.IndexOf("D5FFINIT::#OFF") != -1 & Line.IndexOf("H D5FF") != -1 & Line.IndexOf("b H D5FF") == -1)
                {
                    tmp_inst = tmp_inst.Replace("D5FFINIT::#OFF", "D5FFINIT::INIT0");
                    tmp_inst = tmp_inst.Replace("D5FFSR::#OFF", "D5FFSR::SRLOW");
                    tmp_inst = tmp_inst.Replace("DOUTMUX::#OFF", "DOUTMUX::D5Q");
                    tmp_inst = tmp_inst.Replace("D5FFMUX::#OFF", "D5FFMUX::IN_B");
                }
                
            }

        private void change_routing(StreamReader XDL_ROUT_FINAL, ref string Coms953,string ROUT)
        {
            string line = "";            
            while (XDL_ROUT_FINAL.EndOfStream == false)
            {
                line = XDL_ROUT_FINAL.ReadLine();
                if (line.IndexOf("outpin") != -1)
                {
                    int index = 0;
                    int end = 0;
                    string temp = "";
                    int index_pin = 0;
                    string PIN = "";
                    string FF_POS = "";
                    int index_pip = 0;
                    int index_kama = 0;
                    int index_mux_pip = 0;
                    int index_mux_end = 0;
                    string mux_CLB = "";
                    string outpin_name = "";
                    int inpin = 0;
                    int inpin2 = 0;                    
                    int index_end_name = 0;
                    index = ROUT.IndexOf(line);
                    end = ROUT.IndexOf(";", index);
                    temp = ROUT.Substring(index, end - index);
                    index_pip = temp.IndexOf("pip");
                    if (index_pip != -1)
                    {
                        index_kama = temp.IndexOf(",", index_pip);
                        FF_POS = temp.Substring(index_pip, index_kama - index_pip);
                    }
                    inpin = temp.IndexOf("inpin");
                    inpin2 = temp.IndexOf("inpin", inpin + 5);
                    index_end_name = temp.IndexOf("\"", inpin2 + 8);
                    outpin_name = temp.Substring(inpin2 + 6, index_end_name - inpin2 - 5);

                    if (temp.IndexOf("_L_A3") != -1 || temp.IndexOf("_M_A3") != -1)
                    {
                        if (temp.IndexOf("_L_A3") != -1) index_pin = temp.IndexOf("_L_A3");                            
                        else  index_pin = temp.IndexOf("_M_A3");
                        index_mux_pip = temp.IndexOf("pip", index_pin - 50);
                        index_mux_end = temp.IndexOf("CLB", index_mux_pip + 7);
                        mux_CLB = temp.Substring(index_mux_pip + 4, index_mux_end - index_mux_pip - 5);                        
                        Coms953 = Coms953.Replace(line, "  outpin "+ outpin_name + " A ,");
                    }
                    else if (temp.IndexOf("_L_B3") != -1 || temp.IndexOf("_M_B3") != -1)
                    {
                        if (temp.IndexOf("_L_B3") != -1) index_pin = temp.IndexOf("_L_B3");                            
                        else index_pin = temp.IndexOf("_M_B3");
                        index_mux_pip = temp.IndexOf("pip", index_pin - 50);
                        index_mux_end = temp.IndexOf("CLB", index_mux_pip + 7);
                        mux_CLB = temp.Substring(index_mux_pip + 4, index_mux_end - index_mux_pip - 5);
                        Coms953 = Coms953.Replace(line, "  outpin " + outpin_name + " B ,");
                    }
                    else if (temp.IndexOf("_L_C3") != -1 || temp.IndexOf("_M_C3") != -1)
                    {
                        if (temp.IndexOf("_L_C3") != -1) index_pin = temp.IndexOf("_L_C3");                           
                        else index_pin = temp.IndexOf("_M_C3");
                        index_mux_pip = temp.IndexOf("pip", index_pin - 50);
                        index_mux_end = temp.IndexOf("CLB", index_mux_pip + 7);
                        mux_CLB = temp.Substring(index_mux_pip + 4, index_mux_end - index_mux_pip - 5);
                        Coms953 = Coms953.Replace(line, "  outpin " + outpin_name + " C ,");
                    }
                    else if (temp.IndexOf("_L_D3") != -1 || temp.IndexOf("_M_D3") != -1)
                    {
                        if (temp.IndexOf("_L_D3") != -1) index_pin = temp.IndexOf("_L_D3");
                        else index_pin = temp.IndexOf("_M_D3");
                        index_mux_pip = temp.IndexOf("pip", index_pin - 50);
                        index_mux_end = temp.IndexOf("CLB", index_mux_pip + 7);
                        mux_CLB = temp.Substring(index_mux_pip + 4, index_mux_end - index_mux_pip - 5);
                        Coms953 = Coms953.Replace(line, "  outpin " + outpin_name + " D ,");
                    }
                    else if (temp.IndexOf("_L_A5") != -1 || temp.IndexOf("_M_A5") != -1)
                    {
                        if (temp.IndexOf("_L_A5") != -1) index_pin = temp.IndexOf("_L_A5");
                        else index_pin = temp.IndexOf("_M_A5");
                        index_mux_pip = temp.IndexOf("pip", index_pin - 50);
                        index_mux_end = temp.IndexOf("CLB", index_mux_pip + 7);
                        mux_CLB = temp.Substring(index_mux_pip + 4, index_mux_end - index_mux_pip - 5);
                        Coms953 = Coms953.Replace(line, "  outpin " + outpin_name + " AMUX ,");                        
                    }
                    else if (temp.IndexOf("_L_B5") != -1 || temp.IndexOf("_M_B5") != -1)
                    {
                        if (temp.IndexOf("_L_B5") != -1) index_pin = temp.IndexOf("_L_B5");                       
                        else index_pin = temp.IndexOf("_M_B5");
                        index_mux_pip = temp.IndexOf("pip", index_pin - 50);
                        index_mux_end = temp.IndexOf("CLB", index_mux_pip + 7);
                        mux_CLB = temp.Substring(index_mux_pip + 4, index_mux_end - index_mux_pip - 5);
                        Coms953 = Coms953.Replace(line, "  outpin " + outpin_name + " BMUX ,");                        
                    }
                    else if (temp.IndexOf("_L_C5") != -1 || temp.IndexOf("_M_C5") != -1)
                    {
                        if (temp.IndexOf("_L_C5") != -1) index_pin = temp.IndexOf("_L_C5");
                        else index_pin = temp.IndexOf("_M_C5");
                        index_mux_pip = temp.IndexOf("pip", index_pin - 50);
                        index_mux_end = temp.IndexOf("CLB", index_mux_pip + 7);
                        mux_CLB = temp.Substring(index_mux_pip + 4, index_mux_end - index_mux_pip - 5);
                        Coms953 = Coms953.Replace(line, "  outpin " + outpin_name + " CMUX ,");                        
                    }
                    else if (temp.IndexOf("_L_D5") != -1 || temp.IndexOf("_M_D5") != -1)
                    {
                        if (temp.IndexOf("_L_D5") != -1) index_pin = temp.IndexOf("_L_D5");
                        else index_pin = temp.IndexOf("_M_D5");
                        index_mux_pip = temp.IndexOf("pip", index_pin - 50);
                        index_mux_end = temp.IndexOf("CLB", index_mux_pip + 7);
                        mux_CLB = temp.Substring(index_mux_pip + 4, index_mux_end - index_mux_pip - 5);
                        Coms953 = Coms953.Replace(line, "  outpin " + outpin_name + " DMUX ,");
                    }
                    if (index_pin != 0)
                    {
                        PIN = temp.Substring(index_pin - 12, 3);
                        string PIN_num = "";
                        if (PIN == "_B5" ) { PIN = "A"; PIN_num = "8"; }
                        else if (PIN == "B28" ) { PIN = "A"; PIN_num = "12"; }
                        else if (PIN == "_B1" ) { PIN = "B"; PIN_num = "9"; }
                        else if (PIN == "B24" ) { PIN = "B"; PIN_num = "13"; }
                        else if (PIN == "_B9" ) { PIN = "C"; PIN_num = "10"; }
                        else if (PIN == "B40" ) { PIN = "C"; PIN_num = "14"; }
                        else if (PIN == "B13" ) { PIN = "D"; PIN_num = "11"; }
                        else if (PIN == "B44" ) { PIN = "D"; PIN_num = "15"; }
                        else if (PIN == "_B0") { PIN = "AMUX"; PIN_num = "16"; }
                        else if (PIN == "_B25") { PIN = "AMUX"; PIN_num = "20"; }
                        else if (PIN == "_B4") { PIN = "BMUX"; PIN_num = "17"; }
                        else if (PIN == "_B29") { PIN = "BMUX"; PIN_num = "21"; }
                        else if (PIN == "B12") { PIN = "CMUX"; PIN_num = "18"; }
                        else if (PIN == "B14") { PIN = "CMUX"; PIN_num = "22"; }
                        else if (PIN == "_B8") { PIN = "DMUX"; PIN_num = "19"; }
                        else if (PIN == "B41") { PIN = "DMUX"; PIN_num = "23"; }


                        int index_OUTS = 0;
                        string tmp_FF_POS = "";
                        int index_CLB = 0;
                        int index_2CLB = 0;
                        string FF_CLB = "";
                        int index_narow = 0;
                        string FF_pin = "";
                        string OUTS = "";
                        string ROWCOL = "";
                        string ROWCOL_mux = "";
                        tmp_FF_POS = FF_POS;
                        index_OUTS = FF_POS.IndexOf("OUTS");
                        index_CLB = FF_POS.IndexOf("CLB");
                        index_2CLB = FF_POS.IndexOf("CLB", index_CLB + 3);
                        FF_CLB = FF_POS.Substring(index_CLB, index_2CLB - index_CLB - 1);
                        index_narow = FF_POS.IndexOf("->");
                        FF_pin = FF_POS.Substring(index_narow - 3, 2);
                        OUTS = FF_POS.Substring(index_OUTS, FF_POS.Length - index_OUTS - 1);
                        tmp_FF_POS = tmp_FF_POS.Replace(OUTS, "OUTS" + PIN_num);
                        tmp_FF_POS = tmp_FF_POS.Replace(FF_CLB, mux_CLB);
                        tmp_FF_POS = tmp_FF_POS.Replace(FF_pin, PIN);
                        ROWCOL = FF_CLB.Substring(FF_CLB.IndexOf("_") + 1, FF_CLB.Length - FF_CLB.IndexOf("_") - 1);
                        ROWCOL_mux = mux_CLB.Substring(mux_CLB.IndexOf("_") + 1, mux_CLB.Length - mux_CLB.IndexOf("_") - 1);
                        if (Coms953.IndexOf(FF_POS) != -1) Coms953 = Coms953.Replace(FF_POS, tmp_FF_POS);
                        else
                        {
                            FF_POS = FF_POS.Replace("_L_", "_M_");
                            tmp_FF_POS = tmp_FF_POS.Replace("_L_", "_M_");
                            Coms953 = Coms953.Replace(FF_POS, tmp_FF_POS);
                        }
                        string LOGIC_OUTS = "";
                        string LOGIC_OUTS_mux = "";
                        LOGIC_OUTS = ROWCOL + " LOGIC_" + OUTS + " -> ";
                        LOGIC_OUTS_mux = ROWCOL_mux + " LOGIC_OUTS" + PIN_num + " -> ";
                        Coms953 = Coms953.Replace(LOGIC_OUTS, LOGIC_OUTS_mux);
                    }
                }
            }
        }

        public void read_new_routing()
        {
            Stream s953;
            Stream Tmps953NET;
            s953 = File.OpenRead(@"F:\MS\FinalProject\CODE\COMPLULS.XDL");
            Tmps953NET = File.OpenWrite(@"F:\MS\FinalProject\CODE\Tmps953NETCOMPLULS.XDL");
            StreamReader XDL = new StreamReader(s953);
            StreamWriter XDLwNET = new StreamWriter(Tmps953NET);
            string XDLline = "";
            string argument = "";
            int state = 0;
            string net_name = "";
            string outpin = "";
            string inpin = "";
            int index_inpin = 0;
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
                            //   func_inst(argument, XDLw);
                            break;
                        case 2:
                            //   func_design(argument);
                            break;
                        case 3:
                            if (argument.IndexOf("cfg") == -1 & argument.IndexOf("GTXE1") == -1 & argument.IndexOf("_ML_") == -1 & argument.IndexOf("CK") == -1 & argument.IndexOf("GLOBAL_LOGIC") == -1 & argument.IndexOf("IBUF") == -1
                                & argument.IndexOf("History") == -1)
                            {
                                if (argument.IndexOf("net") != -1)
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
                                XDLwNET.WriteLine("  ;");
                            }
                            //  func_net(argument, XDLwNET);
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
                    argument = XDLline;
                }

            }

            // Close StreamReader
            XDL.Close();
            XDLwNET.Close();

            // Close file
            s953.Close();
            Tmps953NET.Close();

        }
     }
}
