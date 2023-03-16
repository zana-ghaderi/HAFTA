using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPGA_based_FT_MICRO
{
    public class BBmuxes
    {
        internal void search_for_F_muxes(ref int His_NUM, ref string[] XDL_mux_tmp3, ref string ALL, ref long Number_of_lines)
        {
            int ind_ff = 0; int num_ff = 0; int num_mux = 0;
            string ROW = ""; string COL = ""; string Out_H_FF = "";
            string WOR = ""; string TYP = ""; string Styp = "";
            int logic = 0; string lOG = ""; string B = ""; string Bh = "";
            string BYPB = ""; string pin = ""; string His_FF = ""; string Pin1 = "";
            string H_logic = ""; string LOC = "";
            int F11 = 0; int F12 = 0; int F21 = 0; int F22 = 0; int F31 = 0; int F32 = 0; int F41 = 0; int F42 = 0;
            int i = 0; int num_s_ff = 0; int num_s_mux = 0;
            /////////////////////////////////////////////////
            string Brother = ""; int wor_b = 0; string Sbro = ""; string Sbro1 = ""; int IndBro = 0; string bro_tmp = "";
            /////////////////////////////////////////////////

            for (i = 0; i < Number_of_lines; i++)
            {
                IndBro = -1;
                bro_tmp = "";
                Brother = "";
                ind_ff = XDL_mux_tmp3[i].IndexOf("FF<");
                if (XDL_mux_tmp3[i].IndexOf("$") == -1 & XDL_mux_tmp3[i] != "")
                {
                    ////////////////////////////////////////////////////////
                    ROW = XDL_mux_tmp3[i].Substring(XDL_mux_tmp3[i].IndexOf("ROW") + 3, XDL_mux_tmp3[i].IndexOf(" ", XDL_mux_tmp3[i].IndexOf("ROW")) - XDL_mux_tmp3[i].IndexOf("ROW") - 3);
                    COL = XDL_mux_tmp3[i].Substring(XDL_mux_tmp3[i].IndexOf("COL") + 3, XDL_mux_tmp3[i].IndexOf(" ", XDL_mux_tmp3[i].IndexOf("COL")) - XDL_mux_tmp3[i].IndexOf("COL") - 3);
                    WOR = XDL_mux_tmp3[i].Substring(XDL_mux_tmp3[i].IndexOf("WOR") + 3, XDL_mux_tmp3[i].IndexOf(" ", XDL_mux_tmp3[i].IndexOf("WOR")) - XDL_mux_tmp3[i].IndexOf("WOR") - 3);
                    TYP = XDL_mux_tmp3[i].Substring(XDL_mux_tmp3[i].IndexOf("CT") + 2, 2);
                    Styp = XDL_mux_tmp3[i].Substring(XDL_mux_tmp3[i].IndexOf("ST") + 2, 1);
                    ////////////////////////////////////////////////////////
                    num_s_ff = Int16.Parse(XDL_mux_tmp3[i].Substring(ind_ff + 3, 1));
                    num_s_mux = 2 * Int16.Parse(XDL_mux_tmp3[i].Substring(ind_ff - 6, 1));

                    if (num_s_mux != 0)
                    {
                        search_inside_slice(ref XDL_mux_tmp3[i], WOR, logic, lOG, ref num_s_ff, ref num_s_mux, ROW, COL, ref His_NUM, ref ALL,
                            ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32,
                            ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, TYP, Styp);
                        F11 = 0; F12 = 0; F21 = 0; F22 = 0; F31 = 0; F32 = 0; F41 = 0; F42 = 0;
                        His_FF = ""; BYPB = ""; Out_H_FF = ""; H_logic = ""; B = ""; Bh = ""; Pin1 = ""; pin = ""; Styp = "";
                        Brother = ""; Sbro = ""; Sbro1 = ""; bro_tmp = ""; IndBro = 0; wor_b = 0;
                    }
                    if (num_s_ff > 0)
                    {
                        addvanced_multiplexer ADV_MUX = new addvanced_multiplexer();
                        
                        if (Int16.Parse(WOR) % 2 == 0) wor_b = Int16.Parse(WOR) + 1;
                        else wor_b = Int16.Parse(WOR) - 1;                        

                        Sbro = "CT" + TYP + " ROW" + ROW + " COL" + COL + " STL WOR" + wor_b.ToString();
                        Sbro1 = "CT" + TYP + " ROW" + ROW + " COL" + COL + " STM WOR" + wor_b.ToString();

                        //////find brother
                        if (TYP == "LL")
                        {
                            IndBro = ADV_MUX.search_ind(0, Sbro, ref XDL_mux_tmp3, Number_of_lines);
                            Brother = ADV_MUX.search(0, Sbro, ref XDL_mux_tmp3, Number_of_lines);
                        }
                        else if (TYP == "LM")
                        {
                            IndBro = ADV_MUX.search_ind(0, Sbro1, ref XDL_mux_tmp3, Number_of_lines);
                            Brother = ADV_MUX.search(0, Sbro1, ref XDL_mux_tmp3, Number_of_lines);
                            if (Brother == "")
                            {
                                IndBro = ADV_MUX.search_ind(0, Sbro, ref XDL_mux_tmp3, Number_of_lines);
                                Brother = ADV_MUX.search(0, Sbro, ref XDL_mux_tmp3, Number_of_lines);
                            }
                        }
                        bro_tmp = Brother;

                        if (Brother != "" & IndBro != -1)
                        {
                            ind_ff = Brother.IndexOf("FF<");
                            if (Brother.IndexOf("NuFF<0") != -1 & (Brother.IndexOf("$") == -1 || Brother.IndexOf("#") == -1))
                            {
                                num_ff = Int32.Parse(Brother.Substring(ind_ff + 3, 1));
                                num_mux = 2 * Int32.Parse(Brother.Substring(ind_ff - 6, 1));
                            }
                            else num_mux = 0;
                            F11 = 0; F12 = 0; F21 = 0; F22 = 0; F31 = 0; F32 = 0; F41 = 0; F42 = 0;
                            if (Brother.IndexOf("A6LUT") == -1 & Brother.IndexOf("A5LUT") == -1 & Brother.IndexOf("AOUTMUX") == -1) { F11 = 1; F12 = 1; }
                            if (Brother.IndexOf("B6LUT") == -1 & Brother.IndexOf("B5LUT") == -1 & Brother.IndexOf("BOUTMUX") == -1) { F21 = 1; F22 = 1; }
                            if (Brother.IndexOf("C6LUT") == -1 & Brother.IndexOf("C5LUT") == -1 & Brother.IndexOf("COUTMUX") == -1) { F31 = 1; F32 = 1; }
                            if (Brother.IndexOf("D6LUT") == -1 & Brother.IndexOf("D5LUT") == -1 & Brother.IndexOf("DOUTMUX") == -1) { F41 = 1; F42 = 1; }

                            if (num_ff == 0 & num_s_ff < num_mux )
                            {
                                search_in_brother_slice("INB",IndBro,ref bro_tmp,ref Brother,ref XDL_mux_tmp3[i], WOR, logic, lOG, 
                            ref num_s_ff, ref num_mux, ROW, COL, ref His_NUM, ref ALL,ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic,
                            ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin,
                            TYP, Styp, ref XDL_mux_tmp3, Number_of_lines,i);
                                F11 = 0; F12 = 0; F21 = 0; F22 = 0; F31 = 0; F32 = 0; F41 = 0; F42 = 0;
                                His_FF = ""; BYPB = ""; Out_H_FF = ""; H_logic = ""; B = ""; Bh = ""; Pin1 = ""; pin = ""; Styp = "";
                                Brother = ""; Sbro = ""; Sbro1 = ""; bro_tmp = ""; IndBro = 0; wor_b = 0;
                            }
                        }                    
                    }
                    if (num_s_ff > 0)
                    {
                        int ret = 0;
                        addvanced_multiplexer Advancmux = new addvanced_multiplexer();
                        ret = Advancmux.search_in_another_CLBs(ref LOC,ref IndBro, num_s_ff, WOR, ROW, COL, ref Brother, ref num_mux, ref XDL_mux_tmp3, Number_of_lines);
                        bro_tmp = Brother;
                        if (Brother != "")
                        {
                            F11 = 0; F12 = 0; F21 = 0; F22 = 0; F31 = 0; F32 = 0; F41 = 0; F42 = 0;
                            if (Brother.IndexOf("A6LUT") == -1 & Brother.IndexOf("A5LUT") == -1 & Brother.IndexOf("AOUTMUX") == -1) { F11 = 1; F12 = 1; }
                            if (Brother.IndexOf("B6LUT") == -1 & Brother.IndexOf("B5LUT") == -1 & Brother.IndexOf("BOUTMUX") == -1) { F21 = 1; F22 = 1; }
                            if (Brother.IndexOf("C6LUT") == -1 & Brother.IndexOf("C5LUT") == -1 & Brother.IndexOf("COUTMUX") == -1) { F31 = 1; F32 = 1; }
                            if (Brother.IndexOf("D6LUT") == -1 & Brother.IndexOf("D5LUT") == -1 & Brother.IndexOf("DOUTMUX") == -1) { F41 = 1; F42 = 1; }

                            search_in_brother_slice(LOC,IndBro, ref bro_tmp, ref Brother, ref XDL_mux_tmp3[i], WOR, logic, lOG, ref num_s_ff, ref num_mux, ROW, COL, ref His_NUM, ref ALL,
                            ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32,
                            ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, TYP, Styp, ref XDL_mux_tmp3, Number_of_lines,i);
                            F11 = 0; F12 = 0; F21 = 0; F22 = 0; F31 = 0; F32 = 0; F41 = 0; F42 = 0;
                            His_FF = ""; BYPB = ""; Out_H_FF = ""; H_logic = ""; B = ""; Bh = ""; Pin1 = ""; pin = ""; Styp = "";
                            Brother = ""; Sbro = ""; Sbro1 = ""; bro_tmp = ""; IndBro = 0; wor_b = 0;
                        }
                    }
                }
            }     
        }

        private void search_in_brother_slice(string LOC,int IndBro,ref string bro_tmp,ref string Brother,ref string XDL_mux_tmpi,
            string WOR, int logic, string lOG, ref int num_ff, 
            ref int num_mux, string ROW, string COL, ref int His_NUM, ref string ALL, ref string His_FF, ref string BYPB, 
            ref string Out_H_FF, ref string H_logic, ref int F11, ref int F12, ref int F21, ref int F22, ref int F31,
            ref int F32, ref int F41, ref int F42, ref string B, ref string Bh, ref string Pin1, ref string pin, string TYP,
            string Styp,ref string[] XDL_mux_tmp,long num_line,int i)
        {
            if (XDL_mux_tmpi.IndexOf("1bb H") != -1 & XDL_mux_tmpi.IndexOf("1bb H]") == -1 & num_mux > 0)
            {
                if (Int32.Parse(WOR) % 2 == 0) { logic = 0; }
                else { logic = 4; }
                lOG = logic.ToString();
                Easier2(LOC, ref Brother, ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "1bb H",
                       ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                       ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmpi, TYP, Styp, XDL_mux_tmp
                       , num_line, WOR);
            }
            if (XDL_mux_tmpi.IndexOf("2bb H") != -1 & XDL_mux_tmpi.IndexOf("2bb H]") == -1 & num_mux > 0)
            {
                if (Int32.Parse(WOR) % 2 == 0) { logic = 16; }
                else { logic = 20; }
                lOG = logic.ToString();
                Easier2(LOC, ref Brother, ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "2bb H",
                       ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                       ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmpi, TYP, Styp, XDL_mux_tmp
                       , num_line, WOR);
            }
            if (XDL_mux_tmpi.IndexOf("3bb H") != -1 & XDL_mux_tmpi.IndexOf("3bb H]") == -1 & num_mux > 0)
            {
                if (Int32.Parse(WOR) % 2 == 0) { logic = 1; }
                else { logic = 5; }
                lOG = logic.ToString();
                Easier2(LOC, ref Brother, ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "3bb H",
                       ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                       ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmpi, TYP, Styp, XDL_mux_tmp
                       , num_line, WOR);
            }
            if (XDL_mux_tmpi.IndexOf("4bb H") != -1 & XDL_mux_tmpi.IndexOf("4bb H]") == -1 & num_mux > 0)
            {
                if (Int32.Parse(WOR) % 2 == 0) { logic = 17; }
                else { logic = 21; }
                lOG = logic.ToString();
                Easier2(LOC, ref Brother, ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "4bb H",
                       ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                       ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmpi, TYP, Styp, XDL_mux_tmp
                       , num_line, WOR);
            }
            if (XDL_mux_tmpi.IndexOf("5bb H") != -1 & XDL_mux_tmpi.IndexOf("5bb H]") == -1 & num_mux > 0)
            {
                if (Int32.Parse(WOR) % 2 == 0) { logic = 2; }
                else { logic = 6; }
                lOG = logic.ToString();
                Easier2(LOC, ref Brother, ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "5bb H",
                       ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                       ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmpi, TYP, Styp, XDL_mux_tmp
                       , num_line, WOR);
            }
            if (XDL_mux_tmpi.IndexOf("6bb H") != -1 & XDL_mux_tmpi.IndexOf("6bb H]") == -1 & num_mux > 0)
            {
                if (Int32.Parse(WOR) % 2 == 0) { logic = 18; }
                else { logic = 22; }
                lOG = logic.ToString();
                Easier2(LOC, ref Brother, ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "6bb H",
                       ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                       ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmpi, TYP, Styp, XDL_mux_tmp
                       , num_line, WOR);
            }
            if (XDL_mux_tmpi.IndexOf("7bb H") != -1 & XDL_mux_tmpi.IndexOf("7bb H]") == -1 & num_mux > 0)
            {
                if (Int32.Parse(WOR) % 2 == 0) { logic = 3; }
                else { logic = 7; }
                lOG = logic.ToString();
                Easier2(LOC, ref Brother, ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "7bb H",
                       ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                       ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmpi, TYP, Styp, XDL_mux_tmp
                       , num_line, WOR);
            }
            if (XDL_mux_tmpi.IndexOf("8bb H") != -1 & XDL_mux_tmpi.IndexOf("8bb H]") == -1 & num_mux > 0)
            {
                if (Int32.Parse(WOR) % 2 == 0) { logic = 19; }
                else { logic = 23; }
                lOG = logic.ToString();
                Easier2(LOC, ref Brother, ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "8bb H",
                       ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                       ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmpi, TYP, Styp, XDL_mux_tmp
                       , num_line, WOR);
            }
            string tiny = ""; string teeny = "";
            tiny = XDL_mux_tmpi.Substring(XDL_mux_tmpi.IndexOf("NuFF") + 5, 1);
            teeny = Brother.Substring(Brother.IndexOf("NuMUX") + 6, 1);
            XDL_mux_tmpi = XDL_mux_tmpi.Replace("NuFF<" + tiny, "NuFF<" + num_ff);
            if (num_ff < Int16.Parse(tiny))
            {
                Brother = Brother.Replace("NuMUX<" + teeny, "NuMUX<" + num_mux / 2);
                if (IndBro != -100)
                {
                    XDL_mux_tmp[IndBro] = XDL_mux_tmp[IndBro].Replace(bro_tmp, Brother);
                   // size[IndBro] = Brother.Length;
                }
                else
                    XDL_mux_tmp[i] = XDL_mux_tmp[i].Insert(XDL_mux_tmp[i].Length, "\n" + Brother);
            }
            if (num_ff == 0)/////////The brother creating and because of that the $ is added to another one
                XDL_mux_tmpi = XDL_mux_tmpi.Insert(XDL_mux_tmpi.Length, " $");
        }

        private void Easier2(string LOC,ref string Brother, ref int num_ff, ref int num_mux, string ROW, string COL, int logic,
            ref int His_NUM, ref string ALL, string lOG, string p, ref string His_FF, ref string BYPB, ref string Out_H_FF,
            ref string H_logic, ref int F11, ref int F12, ref int F21, ref int F22, ref int F31, ref int F32, ref int F41,
            ref int F42, ref string B, ref string Bh, ref string Pin1, ref string pin, ref string XDL_mux_tmp, string TYP,
            string Styp,string[] XDL_mux_tmp_array,long num_line,string WOR)
        {
            int cache = 0;
            addvanced_multiplexer ad_m = new addvanced_multiplexer();
            string Source = ""; string inst_name = ""; string inst_name_H = ""; string goal = "";
            string brother_ff = "";
            cache = XDL_mux_tmp.IndexOf(p);
            His_FF = XDL_mux_tmp.Substring(cache + 6, 3);
            Source = XDL_mux_tmp.Substring(cache - 5, 4);
            if (Source.IndexOf("L") != -1)
            {
                if (Int16.Parse(WOR) % 2 != 0) WOR = (Int16.Parse(WOR) - 1).ToString();
               
                if (Source == "LSS1") goal = "WOR" + WOR + " " + (Int16.Parse(COL) - 1).ToString();
                else if (Source == "LNN1") goal = "WOR" + WOR + " " + (Int16.Parse(COL) + 1).ToString();
                else if (Source == "LWW1") goal = "WOR" + (Int16.Parse(WOR) - 2).ToString() + " " + COL;
                else if (Source == "LEE1") goal = "WOR" + (Int16.Parse(WOR) + 2).ToString() + " " + COL;
                else if (Source == "LNW2") goal = "WOR" + (Int16.Parse(WOR) - 2).ToString() + " " + (Int16.Parse(COL) + 1).ToString();
                else if (Source == "LSW2") goal = "WOR" + (Int16.Parse(WOR) - 2).ToString() + " " + (Int16.Parse(COL) - 1).ToString();
                else if (Source == "LNE2") goal = "WOR" + (Int16.Parse(WOR) + 2).ToString() + " " + (Int16.Parse(COL) + 1).ToString();
                else if (Source == "LSE2") goal = "WOR" + (Int16.Parse(WOR) + 2).ToString() + " " + (Int16.Parse(COL) - 1).ToString();
                else if (Source == "LNN2") goal = "WOR" + WOR + " " + (Int16.Parse(COL) + 2).ToString();
                else if (Source == "LSS2") goal = "WOR" + WOR + " " + (Int16.Parse(COL) - 2).ToString();
                else if (Source == "LWW2") goal = "WOR" + (Int16.Parse(WOR) - 4).ToString() + " " + COL;
                else if (Source == "LEE2") goal = "WOR" + (Int16.Parse(WOR) + 4).ToString() + " " + COL;
            }
            else
            {
                if (Int16.Parse(WOR) % 2 == 0) WOR = (Int16.Parse(WOR) + 1).ToString();

                if (Source == "RSS1") goal = "WOR" + WOR + " " + (Int16.Parse(COL) - 1).ToString();
                else if (Source == "RNN1") goal = "WOR" + WOR + " " + (Int16.Parse(COL) + 1).ToString();
                else if (Source == "RWW1") goal = "WOR" + (Int16.Parse(WOR) - 2).ToString() + " " + COL;
                else if (Source == "REE1") goal = "WOR" + (Int16.Parse(WOR) + 2).ToString() + " " + COL;
                else if (Source == "RNW2") goal = "WOR" + (Int16.Parse(WOR) - 2).ToString() + " " + (Int16.Parse(COL) + 1).ToString();
                else if (Source == "RSW2") goal = "WOR" + (Int16.Parse(WOR) - 2).ToString() + " " + (Int16.Parse(COL) - 1).ToString();
                else if (Source == "RNE2") goal = "WOR" + (Int16.Parse(WOR) + 2).ToString() + " " + (Int16.Parse(COL) + 1).ToString();
                else if (Source == "RSE2") goal = "WOR" + (Int16.Parse(WOR) + 2).ToString() + " " + (Int16.Parse(COL) - 1).ToString();
                else if (Source == "RNN2") goal = "WOR" + WOR + " " + (Int16.Parse(COL) + 2).ToString();
                else if (Source == "RSS2") goal = "WOR" + WOR + " " + (Int16.Parse(COL) - 2).ToString();
                else if (Source == "RWW2") goal = "WOR" + (Int16.Parse(WOR) - 4).ToString() + " " + COL;
                else if (Source == "REE2") goal = "WOR" + (Int16.Parse(WOR) + 4).ToString() + " " + COL;
            }
            if (Source.IndexOf("L") != -1) Source = Source.Replace("L", "");
            else Source = Source.Replace("R", "");
            brother_ff = ad_m.search(0, goal, ref XDL_mux_tmp_array, num_line);
            inst_name = brother_ff.Substring(brother_ff.IndexOf("(") + 1, brother_ff.IndexOf(")") - brother_ff  .IndexOf("(") - 1);
            inst_name_H = Brother.Substring(Brother.IndexOf("(") + 1, Brother.IndexOf(")") - Brother.IndexOf("(") - 1);
            
            if (logic == 0 || logic == 1 || logic == 2 || logic == 3 || logic == 16 || logic == 17 || logic == 18 || logic == 19)
            {
                set_arguments1B(LOC,p, ref  Brother, ref  His_FF, ref  BYPB, ref  Out_H_FF, ref  H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp);
            }
            else if (logic == 4 || logic == 5 || logic == 6 || logic == 7 || logic == 20 || logic == 21 || logic == 22 || logic == 23)
            {
                set_argumentsB(LOC,p, ref  Brother, ref  His_FF, ref  BYPB, ref  Out_H_FF, ref  H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp);
            }
            His_NUM++;
            int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + Source + "BEG0 ,");
            if (indINT == -1)
                Console.Write("salam\n");
            int indkama = ALL.IndexOf(",", indINT - 115);
            string temporary = ALL.Substring(indkama, indINT - indkama);
            int indkama2 = ALL.IndexOf(",", indINT + 1);
            int indsemi = ALL.IndexOf(";", indkama2 + 1);
            int indStype = ALL.IndexOf("->", indINT - 25);
            string TYP_h = ALL.Substring(indStype + 6, 2);
            string Styp_h = ALL.Substring(indStype + 9, 1);
            string temp2 = ALL.Substring(indkama2 + 2, indsemi - indkama2 - 2);
            Routing_function(inst_name,inst_name_H,TYP_h, Styp_h, temp2, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL, Source);
            num_mux--;
            num_ff--;
        }

        private void search_inside_slice(ref string XDL_mux_tmp, string WOR, int logic, string lOG, ref int num_ff, 
            ref int num_mux, string ROW, string COL, ref int His_NUM, ref string ALL, ref string His_FF, 
            ref string BYPB, ref string Out_H_FF, ref string H_logic, ref int F11, ref int F12, ref int F21,
            ref int F22, ref int F31, ref int F32, ref int F41, ref int F42, ref string B, ref string Bh,
            ref string Pin1, ref string pin, string TYP, string Styp)
        {
            Bmultiplexers BmuxZ = new Bmultiplexers();
            F11 = 0; F12 = 0; F21 = 0; F22 = 0; F31 = 0; F32 = 0; F41 = 0; F42 = 0;
            if (XDL_mux_tmp.IndexOf("A6LUT") == -1 & XDL_mux_tmp.IndexOf("A5LUT") == -1 & XDL_mux_tmp.IndexOf("AOUTMUX") == -1) { F11 = 1; F12 = 1; }
            if (XDL_mux_tmp.IndexOf("B6LUT") == -1 & XDL_mux_tmp.IndexOf("B5LUT") == -1 & XDL_mux_tmp.IndexOf("BOUTMUX") == -1) { F21 = 1; F22 = 1; }
            if (XDL_mux_tmp.IndexOf("C6LUT") == -1 & XDL_mux_tmp.IndexOf("C5LUT") == -1 & XDL_mux_tmp.IndexOf("COUTMUX") == -1) { F31 = 1; F32 = 1; }
            if (XDL_mux_tmp.IndexOf("D6LUT") == -1 & XDL_mux_tmp.IndexOf("D5LUT") == -1 & XDL_mux_tmp.IndexOf("DOUTMUX") == -1) { F41 = 1; F42 = 1; }

            if (XDL_mux_tmp.IndexOf("1bb H") != -1 & num_mux > 0)
            {
                if (Int32.Parse(WOR) % 2 == 0) { logic = 0; }
                else { logic = 4; }
                lOG = logic.ToString();
                Easier("INS",ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "1bb H",
                       ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                       ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp, TYP, Styp);
            }
            if (XDL_mux_tmp.IndexOf("2bb H") != -1 & num_mux > 0)
            {
                if (Int32.Parse(WOR) % 2 == 0) { logic = 16; }
                else { logic = 20; }
                lOG = logic.ToString();
                Easier("INS", ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "2bb H",
                       ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                       ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp, TYP, Styp);
            }
            if (XDL_mux_tmp.IndexOf("3bb H") != -1 & num_mux > 0)
            {
                if (Int32.Parse(WOR) % 2 == 0) { logic = 1; }
                else { logic = 5; }
                lOG = logic.ToString();
                Easier("INS", ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "3bb H",
                       ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                       ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp, TYP, Styp);
            }
            if (XDL_mux_tmp.IndexOf("4bb H") != -1 & num_mux > 0)
            {
                if (Int32.Parse(WOR) % 2 == 0) { logic = 17; }
                else { logic = 21; }
                lOG = logic.ToString();
                Easier("INS", ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "4bb H",
                       ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                       ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp, TYP, Styp);
            }
            if (XDL_mux_tmp.IndexOf("5bb H") != -1 & num_mux > 0)
            {
                if (Int32.Parse(WOR) % 2 == 0) { logic = 2; }
                else { logic = 6; }
                lOG = logic.ToString();
                Easier("INS", ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "5bb H",
                       ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                       ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp, TYP, Styp);
            }
            if (XDL_mux_tmp.IndexOf("6bb H") != -1 & num_mux > 0)
            {
                if (Int32.Parse(WOR) % 2 == 0) { logic = 18; }
                else { logic = 22; }
                lOG = logic.ToString();
                Easier("INS", ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "6bb H",
                       ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                       ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp, TYP, Styp);
            }
            if (XDL_mux_tmp.IndexOf("7bb H") != -1 & num_mux > 0)
            {
                if (Int32.Parse(WOR) % 2 == 0) { logic = 3; }
                else { logic = 7; }
                lOG = logic.ToString();
                Easier("INS", ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "7bb H",
                       ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                       ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp, TYP, Styp);
            }
            if (XDL_mux_tmp.IndexOf("8bb H") != -1 & num_mux > 0)
            {
                if (Int32.Parse(WOR) % 2 == 0) { logic = 19; }
                else { logic = 23; }
                lOG = logic.ToString();
                Easier("INS", ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "8bb H",
                       ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                       ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp, TYP, Styp);
            }
            string tiny = ""; string teeny = "";
            tiny = XDL_mux_tmp.Substring(XDL_mux_tmp.IndexOf("NuFF") + 5, 1);
            teeny = XDL_mux_tmp.Substring(XDL_mux_tmp.IndexOf("NuMUX") + 6, 1);
            XDL_mux_tmp = XDL_mux_tmp.Replace("NuFF<" + tiny, "NuFF<" + num_ff);
            if (num_ff < Int16.Parse(tiny))
                XDL_mux_tmp = XDL_mux_tmp.Replace("NuMUX<" + teeny, "NuMUX<" + num_mux / 2);
            if (num_ff == 0)/////////The brother creating and because of that the $ is added to another one
                XDL_mux_tmp = XDL_mux_tmp.Insert(XDL_mux_tmp.Length, " $");
        }

        private void Easier(string LOC,ref int num_ff, ref int num_mux, string ROW, string COL, int logic,
            ref int His_NUM, ref string ALL, string lOG, string p, ref string His_FF, ref string BYPB,
            ref string Out_H_FF, ref string H_logic, ref int F11, ref int F12, ref int F21, ref int F22,
            ref int F31, ref int F32, ref int F41, ref int F42, ref string B, ref string Bh, ref string Pin1,
            ref string pin, ref string XDL_mux_tmp, string TYP, string Styp)
        {
            int cache = 0;
            string Source = ""; string inst_name = "";
            cache = XDL_mux_tmp.IndexOf(p);
            His_FF = XDL_mux_tmp.Substring(cache + 6, 3);
            Source = XDL_mux_tmp.Substring(cache - 4, 3);
            inst_name = XDL_mux_tmp.Substring(XDL_mux_tmp.IndexOf("(") + 1, XDL_mux_tmp.IndexOf(")") - XDL_mux_tmp.IndexOf("(") - 1);
            if (logic == 0 || logic == 1 || logic == 2 || logic == 3 || logic == 16 || logic == 17 || logic == 18 || logic == 19)
            {
                set_arguments1(LOC, p, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp);
            }
            else if (logic == 4 || logic == 5 || logic == 6 || logic == 7 || logic == 20 || logic == 21 || logic == 22 || logic == 23)
            {
                set_arguments(LOC, p, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp);
            }
            His_NUM++;
            int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + Source + "BEG0 ,");
            if (indINT == -1)
                Console.Write("salam\n");
            int indkama = ALL.IndexOf(",", indINT - 115);
            string temporary = ALL.Substring(indkama, indINT - indkama);
            int indkama2 = ALL.IndexOf(",", indINT + 1);
            int indsemi = ALL.IndexOf(";", indkama2 + 1);           
            int indStype = ALL.IndexOf("->", indINT - 25);
            string TYP_h = ALL.Substring(indStype + 6, 2);
            string Styp_h = ALL.Substring(indStype + 9, 1);
            string temp2 = ALL.Substring(indkama2 + 2, indsemi - indkama2 - 2);
            Routing_function(inst_name,inst_name,TYP_h,Styp_h,temp2,temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL,Source);
            num_mux--;
            num_ff--;
        }

        private void set_argumentsB(string LOC,string MFF, ref string Brother, ref string His_FF, ref string BYPB, ref string Out_H_FF, ref string H_logic, ref int F11, ref int F12, ref int F21, ref int F22, ref int F31, ref int F32, ref int F41, ref int F42, ref string B, ref string Bh, ref string Pin1, ref string pin, ref string p)
        {
            if (His_FF == "AFF") { BYPB = "BYP_B0"; Out_H_FF = "AQ"; H_logic = "0"; }
            else if (His_FF == "BFF") { BYPB = "BYP_B5"; Out_H_FF = "BQ"; H_logic = "1"; }
            else if (His_FF == "CFF") { BYPB = "BYP_B2"; Out_H_FF = "CQ"; H_logic = "2"; }
            else if (His_FF == "DFF") { BYPB = "BYP_B7"; Out_H_FF = "DQ"; H_logic = "3"; }

            if (F11 == 1) { B = "18"; Bh = "5"; Pin1 = "A2"; pin = "A3"; F11 = 0; p = p.Insert(p.Length, " [A6LUT, " + LOC + " " + MFF + "]"); Brother = Brother.Insert(Brother.Length, " !A6LUT"); }
            else if (F12 == 1) { B = "7"; Bh = "0"; Pin1 = "A4"; pin = "A5"; F12 = 0; p = p.Insert(p.Length, " [A5LUT, " + LOC + " " + MFF + "]" + " [AOUTMUX]"); Brother = Brother.Insert(Brother.Length, " !A5LUT"); }
            else if (F22 == 1) { B = "38"; Bh = "1"; Pin1 = "B2"; pin = "B3"; F22 = 0; p = p.Insert(p.Length, " [B6LUT, " + LOC + " " + MFF + "]"); Brother = Brother.Insert(Brother.Length, " !B6LUT"); }
            else if (F21 == 1) { B = "3"; Bh = "4"; Pin1 = "B4"; pin = "B5"; F21 = 0; p = p.Insert(p.Length, " [B5LUT, " + LOC + " " + MFF + "]" + " [BOUTMUX]"); Brother = Brother.Insert(Brother.Length, " !B5LUT"); }
            else if (F32 == 1) { B = "22"; Bh = "9"; Pin1 = "C2"; pin = "C3"; F32 = 0; p = p.Insert(p.Length, " [C6LUT, " + LOC + " " + MFF + "]"); Brother = Brother.Insert(Brother.Length, " !C6LUT"); }
            else if (F31 == 1) { B = "11"; Bh = "12"; Pin1 = "C4"; pin = "C5"; F31 = 0; p = p.Insert(p.Length, " [C5LUT, " + LOC + " " + MFF + "]" + " [COUTMUX]"); Brother = Brother.Insert(Brother.Length, " !C5LUT"); }
            else if (F42 == 1) { B = "34"; Bh = "13"; Pin1 = "D2"; pin = "D3"; F42 = 0; p = p.Insert(p.Length, " [D6LUT, " + LOC + " " + MFF + "]"); Brother = Brother.Insert(Brother.Length, " !D6LUT"); }
            else if (F41 == 1) { B = "15"; Bh = "8"; Pin1 = "D4"; pin = "D5"; F41 = 0; p = p.Insert(p.Length, " [D5LUT, " + LOC + " " + MFF + "]" + " [DOUTMUX]"); Brother = Brother.Insert(Brother.Length, " !D5LUT"); }
        }

        private void set_arguments1B(string LOC,string MFF, ref string Brother, ref string His_FF, ref string BYPB, ref string Out_H_FF, ref string H_logic, ref int F11, ref int F12, ref int F21, ref int F22, ref int F31, ref int F32, ref int F41, ref int F42, ref string B, ref string Bh, ref string Pin1, ref string pin, ref string p)
        {
            if (His_FF == "AFF") { BYPB = "BYP_B1"; Out_H_FF = "AQ"; H_logic = "4"; }
            else if (His_FF == "BFF") { BYPB = "BYP_B4"; Out_H_FF = "BQ"; H_logic = "5"; }
            else if (His_FF == "CFF") { BYPB = "BYP_B3"; Out_H_FF = "CQ"; H_logic = "6"; }
            else if (His_FF == "DFF") { BYPB = "BYP_B6"; Out_H_FF = "DQ"; H_logic = "7"; }

            if (F12 == 1) { B = "19"; Bh = "28"; Pin1 = "A2"; pin = "A3"; F12 = 0; p = p.Insert(p.Length, " [A6LUT, " + LOC + " " + MFF + "]"); Brother = Brother.Insert(Brother.Length, " !A6LUT"); }
            else if (F11 == 1) { B = "30"; Bh = "25"; Pin1 = "A4"; pin = "A5"; F11 = 0; p = p.Insert(p.Length, " [A5LUT, " + LOC + " " + MFF + "]" + " [AOUTMUX]"); Brother = Brother.Insert(Brother.Length, " !A5LUT"); }
            else if (F22 == 1) { B = "39"; Bh = "24"; Pin1 = "B2"; pin = "B3"; F22 = 0; p = p.Insert(p.Length, " [B6LUT, " + LOC + " " + MFF + "]"); Brother = Brother.Insert(Brother.Length, " !B6LUT"); }
            else if (F21 == 1) { B = "26"; Bh = "29"; Pin1 = "B4"; pin = "B5"; F21 = 0; p = p.Insert(p.Length, " [B5LUT, " + LOC + " " + MFF + "]" + " [BOUTMUX]"); Brother = Brother.Insert(Brother.Length, " !B5LUT"); }
            else if (F32 == 1) { B = "23"; Bh = "40"; Pin1 = "C2"; pin = "C3"; F32 = 0; p = p.Insert(p.Length, " [C6LUT, " + LOC + " " + MFF + "]"); Brother = Brother.Insert(Brother.Length, " !C6LUT"); }
            else if (F31 == 1) { B = "42"; Bh = "14"; Pin1 = "C4"; pin = "C5"; F31 = 0; p = p.Insert(p.Length, " [C5LUT, " + LOC + " " + MFF + "]" + " [COUTMUX]"); Brother = Brother.Insert(Brother.Length, " !C5LUT"); }
            else if (F42 == 1) { B = "35"; Bh = "44"; Pin1 = "D2"; pin = "D3"; F42 = 0; p = p.Insert(p.Length, " [D6LUT, " + LOC + " " + MFF + "]"); Brother = Brother.Insert(Brother.Length, " !D6LUT"); }
            else if (F41 == 1) { B = "46"; Bh = "41"; Pin1 = "D4"; pin = "D5"; F41 = 0; p = p.Insert(p.Length, " [D5LUT, " + LOC + " " + MFF + "]" + " [DOUTMUX]"); Brother = Brother.Insert(Brother.Length, " !D5LUT"); }            
        }

        private void Routing_function(string inst_name,string inst_name_H1,string TYP_h,string Styp_h,string temp2,string temporary, string ROW, string COL, string lOG, string BYPB, string pin, string TYP, 
            string Bh, string B, string Styp, string Pin1, int His_NUM, string Out_H_FF, string H_logic, ref string ALL,string Source)
        {
            string ROW_history = "";
            string COL_history = "";
            string reverse = "";
            if (Source == "NN1") { ROW_history = ROW; COL_history = (Int16.Parse(COL) + 1).ToString(); reverse = "SS1"; }
            else if (Source == "NN2") { ROW_history = ROW; COL_history = (Int16.Parse(COL) + 2).ToString(); reverse = "SS2"; }
            else if (Source == "NW2") { ROW_history = (Int16.Parse(ROW) - 1).ToString(); COL_history = (Int16.Parse(COL) + 1).ToString(); reverse = "SE2"; }
            else if (Source == "NE2") { ROW_history = (Int16.Parse(ROW) + 1).ToString(); COL_history = (Int16.Parse(COL) + 1).ToString(); reverse = "SW2"; }
            else if (Source == "SS1") { ROW_history = ROW; COL_history = (Int16.Parse(COL) - 1).ToString(); reverse = "NN1"; }
            else if (Source == "SS2") { ROW_history = ROW; COL_history = (Int16.Parse(COL) - 2).ToString(); reverse = "NN2"; }
            else if (Source == "SW2") { ROW_history = (Int16.Parse(ROW) - 1).ToString(); COL_history = (Int16.Parse(COL) - 1).ToString(); reverse = "NE2"; }
            else if (Source == "SE2") { ROW_history = (Int16.Parse(ROW) + 1).ToString(); COL_history = (Int16.Parse(COL) - 1).ToString(); reverse = "NW2"; }
            else if (Source == "EE1") { ROW_history = (Int16.Parse(ROW) + 1).ToString(); COL_history = COL; reverse = "WW1"; }
            else if (Source == "WW1") { ROW_history = (Int16.Parse(ROW) - 1).ToString(); COL_history = COL; reverse = "EE1"; }
            ALL = ALL.Replace(temporary + "pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + Source + "BEG0 ,\n" + temp2,
                              ",\n  inpin \"" + inst_name_H1 + "\" " + pin + " " + temporary +
                              "pip CLB" + TYP + "_X" + ROW + "Y" + COL + " " + "CLB" + TYP + "_IMUX_B" + Bh + " -> " + "CLB" + TYP + "_" + Styp + "_" + pin + ",\n" +
                              "  pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + Source + "BEG0" + " ,\n" +
                              "  pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> IMUX_B" + Bh + " ,\n"+ temp2 + ";" +
                              "\n net \"History_H_" + His_NUM + "\" ,\n" + "  outpin \"" + inst_name + "\" " + Out_H_FF + " ,\n" + "  inpin \"" + inst_name_H1 + "\" " + Pin1 +
                              " ,\n  pip CLB" + TYP_h + "_X" + ROW_history + "Y" + COL_history + " " +
                              "CLB" + TYP_h + "_" + Styp_h + "_" + Out_H_FF + " -> " + "CLB" +
                              TYP_h + "_" + "LOGIC_OUTS" + H_logic + " ,\n" +
                              "  pip CLB" + TYP + "_X" + ROW + "Y" + COL + " " +
                              "CLB" + TYP + "_IMUX_B" + B + " -> " + "CLB" + TYP + "_" + Styp + "_" + Pin1 + " " + ",\n" +                              
                              "  pip INT" + "_X" + ROW_history + "Y" + COL_history + " " + "LOGIC_OUTS" + H_logic + " -> " + reverse + "BEG0"  + " ,\n" +
                              "  pip INT" + "_X" + ROW + "Y" + COL + " " +reverse +"END0" + " -> IMUX_B" + B + " ,\n" );
        }

        private void set_arguments(string LOC,string MFF, ref string His_FF, ref string BYPB, ref string Out_H_FF, ref string H_logic,
            ref int F11, ref int F12, ref int F21, ref int F22, ref int F31, ref int F32, ref int F41, ref int F42,
            ref string B, ref string Bh, ref string Pin1, ref string pin, ref string p)
        {            
            if (His_FF == "AFF") { BYPB = "BYP_B0"; Out_H_FF = "AQ"; H_logic = "0"; }
            else if (His_FF == "BFF") { BYPB = "BYP_B5"; Out_H_FF = "BQ"; H_logic = "1"; }
            else if (His_FF == "CFF") { BYPB = "BYP_B2"; Out_H_FF = "CQ"; H_logic = "2"; }
            else if (His_FF == "DFF") { BYPB = "BYP_B7"; Out_H_FF = "DQ"; H_logic = "3"; }

            if (F11 == 1) { B = "18"; Bh = "5"; Pin1 = "A2"; pin = "A3"; F11 = 0; p = p.Insert(p.Length, " [A6LUT, " + LOC + " " + MFF + "]"); }
            else if (F12 == 1) { B = "7"; Bh = "0"; Pin1 = "A4"; pin = "A5"; F12 = 0; p = p.Insert(p.Length, " [A5LUT, " + LOC + " " + MFF + "]" + " [AOUTMUX]"); }
            else if (F22 == 1) { B = "38"; Bh = "1"; Pin1 = "B2"; pin = "B3"; F22 = 0; p = p.Insert(p.Length, " [B6LUT, " + LOC + " " + MFF + "]"); }
            else if (F21 == 1) { B = "3"; Bh = "4"; Pin1 = "B4"; pin = "B5"; F21 = 0; p = p.Insert(p.Length, " [B5LUT, " + LOC + " " + MFF + "]" + " [BOUTMUX]"); }
            else if (F32 == 1) { B = "22"; Bh = "9"; Pin1 = "C2"; pin = "C3"; F32 = 0; p = p.Insert(p.Length, " [C6LUT, " + LOC + " " + MFF + "]"); }
            else if (F31 == 1) { B = "11"; Bh = "12"; Pin1 = "C4"; pin = "C5"; F31 = 0; p = p.Insert(p.Length, " [C5LUT, " + LOC + " " + MFF + "]" + " [COUTMUX]"); }
            else if (F42 == 1) { B = "34"; Bh = "13"; Pin1 = "D2"; pin = "D3"; p = p.Insert(p.Length, " [D6LUT, " + LOC + " " + MFF + "]"); }
            else if (F41 == 1) { B = "15"; Bh = "8"; Pin1 = "D4"; pin = "D5"; F42 = 0; F41 = 0; p = p.Insert(p.Length, " [D5LUT, " + LOC + " " + MFF + "]" + " [DOUTMUX]"); }            
        }

        private void set_arguments1(string LOC,string MFF, ref string His_FF, ref string BYPB, ref string Out_H_FF, ref string H_logic,
            ref int F11, ref int F12, ref int F21, ref int F22, ref int F31, ref int F32, ref int F41, ref int F42,
            ref string B, ref string Bh, ref string Pin1, ref string pin, ref string p)
        {            
            if (His_FF == "AFF") { BYPB = "BYP_B1"; Out_H_FF = "AQ"; H_logic = "4"; }
            else if (His_FF == "BFF") { BYPB = "BYP_B4"; Out_H_FF = "BQ"; H_logic = "5"; }
            else if (His_FF == "CFF") { BYPB = "BYP_B3"; Out_H_FF = "CQ"; H_logic = "6"; }
            else if (His_FF == "DFF") { BYPB = "BYP_B6"; Out_H_FF = "DQ"; H_logic = "7"; }

            if (F12 == 1) { B = "19"; Bh = "28"; Pin1 = "A2"; pin = "A3"; F12 = 0; p = p.Insert(p.Length, " [A6LUT, " + LOC + " " + MFF + "]"); }
            else if (F11 == 1) { B = "30"; Bh = "25"; Pin1 = "A4"; pin = "A5"; F11 = 0; p = p.Insert(p.Length, " [A5LUT, " + LOC + " " + MFF + "]" + " [AOUTMUX]"); }
            else if (F22 == 1) { B = "39"; Bh = "24"; Pin1 = "B2"; pin = "B3"; F22 = 0; p = p.Insert(p.Length, " [B6LUT, " + LOC + " " + MFF + "]"); }
            else if (F21 == 1) { B = "26"; Bh = "29"; Pin1 = "B4"; pin = "B5"; F21 = 0; p = p.Insert(p.Length, " [B5LUT, " + LOC + " " + MFF + "]" + " [BOUTMUX]"); }
            else if (F32 == 1) { B = "23"; Bh = "40"; Pin1 = "C2"; pin = "C3"; F32 = 0; p = p.Insert(p.Length, " [C6LUT, " + LOC + " " + MFF + "]"); }
            else if (F31 == 1) { B = "42"; Bh = "14"; Pin1 = "C4"; pin = "C5"; F31 = 0; p = p.Insert(p.Length, " [C5LUT, " + LOC + " " + MFF + "]" + " [COUTMUX]"); }
            else if (F42 == 1) { B = "35"; Bh = "44"; Pin1 = "D2"; pin = "D3"; F42 = 0; p = p.Insert(p.Length, " [D6LUT, " + LOC + " " + MFF + "]"); }
            else if (F41 == 1) { B = "46"; Bh = "41"; Pin1 = "D4"; pin = "D5"; F41 = 0; p = p.Insert(p.Length, " [D5LUT, " + LOC + " " + MFF + "]" + " [DOUTMUX]"); }            
        }
    }
}
