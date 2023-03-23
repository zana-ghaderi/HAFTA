using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PARSER
{
    public class Bmultiplexers
    {
        public void Search_multiplesers(ref string[] XDL_mux_tmp, long Number_of_lines, ref string ALL, ref int His_NUM,ref int U)
        {
            int ind_ff = 0; int num_ff = 0; int num_mux = 0;
            string ROW = ""; string COL = ""; string Out_H_FF = "";
            string WOR = ""; string TYP = ""; string Styp = "";
            int logic = 0; string lOG = ""; string B = ""; string Bh = "";
            string BYPB = ""; string pin = ""; string His_FF = ""; string Pin1 = "";
            string H_logic = ""; 
            int F11 = 0; int F12 = 0; int F21 = 0; int F22 = 0; int F31 = 0; int F32 = 0; int F41 = 0; int F42 = 0;
            int i = 0; string LOC = "";
            /////////////////////////////////////////////////
            string Brother = ""; int wor_b = 0; string Sbro = ""; string Sbro1 = ""; int IndBro = 0; string bro_tmp = "";
            /////////////////////////////////////////////////
            
            for (i = 0; i < Number_of_lines; i++)
            {
                IndBro = -1;
                bro_tmp = "";
                Brother = "";
                ind_ff = XDL_mux_tmp[i].IndexOf("FF<");
                if (XDL_mux_tmp[i].IndexOf("$") == -1 & XDL_mux_tmp[i] != "")////If the flipflops don't have mux
                {
                    ////////////////////////////////////////////////////////
                    ROW = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("ROW") + 3, XDL_mux_tmp[i].IndexOf(" ", XDL_mux_tmp[i].IndexOf("ROW")) - XDL_mux_tmp[i].IndexOf("ROW") - 3);
                    COL = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("COL") + 3, XDL_mux_tmp[i].IndexOf(" ", XDL_mux_tmp[i].IndexOf("COL")) - XDL_mux_tmp[i].IndexOf("COL") - 3);
                    WOR = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("WOR") + 3, XDL_mux_tmp[i].IndexOf(" ", XDL_mux_tmp[i].IndexOf("WOR")) - XDL_mux_tmp[i].IndexOf("WOR") - 3);
                    TYP = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("CT") + 2, 2);
                    Styp = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("ST") + 2, 1);
                    ////////////////////////////////////////////////////////

                    num_ff = Int16.Parse(XDL_mux_tmp[i].Substring(ind_ff + 3, 1));
                    num_mux = 2 * Int16.Parse(XDL_mux_tmp[i].Substring(ind_ff - 6, 1));
                    ///////////////////////////////Now we have 3 states depend on the Number of flipflop 
                    ///////////////////////////////in the slice and number af free LUT in the same slice
                    if (num_mux != 0)////////////// 
                    {
                        F11 = 0; F12 = 0; F21 = 0; F22 = 0; F31 = 0; F32 = 0; F41 = 0; F42 = 0;                      
                        if (XDL_mux_tmp[i].IndexOf("A6LUT") == -1 & XDL_mux_tmp[i].IndexOf("A5LUT") == -1 & XDL_mux_tmp[i].IndexOf("AOUTMUX") == -1) { F11 = 1; F12 = 1; }
                        if (XDL_mux_tmp[i].IndexOf("B6LUT") == -1 & XDL_mux_tmp[i].IndexOf("B5LUT") == -1 & XDL_mux_tmp[i].IndexOf("BOUTMUX") == -1) { F21 = 1; F22 = 1; }
                        if (XDL_mux_tmp[i].IndexOf("C6LUT") == -1 & XDL_mux_tmp[i].IndexOf("C5LUT") == -1 & XDL_mux_tmp[i].IndexOf("COUTMUX") == -1) { F31 = 1; F32 = 1; }
                        if (XDL_mux_tmp[i].IndexOf("D6LUT") == -1 & XDL_mux_tmp[i].IndexOf("D5LUT") == -1 & XDL_mux_tmp[i].IndexOf("DOUTMUX") == -1) { F41 = 1; F42 = 1; }

                        if (XDL_mux_tmp[i].IndexOf("1b H") != -1 & num_mux > 0) 
                        {
                            if (Int32.Parse(WOR) % 2 != 0) { logic = 0; }
                            else {  logic = 4; }
                            lOG = logic.ToString();
                            Easier("INS",ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "1b H",
                                   ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                                   ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i], TYP, Styp);
                        }
                        if (XDL_mux_tmp[i].IndexOf("2b H") != -1 & num_mux > 0) 
                        {
                            if (Int32.Parse(WOR) % 2 != 0) { logic = 16; }
                            else {  logic = 20; }
                            lOG = logic.ToString();
                            Easier("INS", ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "2b H",
                                   ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                                   ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i], TYP, Styp);
                        }
                        if (XDL_mux_tmp[i].IndexOf("3b H") != -1 & num_mux > 0) 
                        {
                            if (Int32.Parse(WOR) % 2 != 0) {  logic = 1; }
                            else {  logic = 5; }                            
                            lOG = logic.ToString();
                            Easier("INS", ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "3b H", 
                                   ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                                   ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i], TYP, Styp);
                        }
                        if (XDL_mux_tmp[i].IndexOf("4b H") != -1 & num_mux > 0) 
                        {
                            if (Int32.Parse(WOR) % 2 != 0) {  logic = 17; }
                            else {  logic = 21; }
                            lOG = logic.ToString();
                            Easier("INS", ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "4b H",
                                   ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                                   ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i], TYP, Styp);
                        }
                        if (XDL_mux_tmp[i].IndexOf("5b H") != -1 & num_mux > 0) 
                        {
                            if (Int32.Parse(WOR) % 2 != 0) {  logic = 2; }
                            else { logic = 6; }
                            lOG = logic.ToString();
                            Easier("INS", ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "5b H",
                                   ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                                   ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i], TYP, Styp);
                        }
                        if (XDL_mux_tmp[i].IndexOf("6b H") != -1 & num_mux > 0) 
                        {
                            if (Int32.Parse(WOR) % 2 != 0) { logic = 18; }
                            else {  logic = 22; }
                            lOG = logic.ToString();
                            Easier("INS", ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "6b H",
                                   ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                                   ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i], TYP, Styp);
                        }
                        if (XDL_mux_tmp[i].IndexOf("7b H") != -1 & num_mux > 0) 
                        {
                            if (Int32.Parse(WOR) % 2 != 0) { logic = 3; }
                            else { logic = 7; }
                            lOG = logic.ToString();
                            Easier("INS", ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "7b H",
                                   ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                                   ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i], TYP, Styp);
                        }
                        if (XDL_mux_tmp[i].IndexOf("8b H") != -1 & num_mux > 0) 
                        {
                            if (Int32.Parse(WOR) % 2 != 0) { logic = 19; }
                            else { logic = 23; }
                            lOG = logic.ToString();
                            Easier("INS", ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "8b H",
                                   ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                                   ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i], TYP, Styp);
                        }
                        string tiny = ""; string teeny = "";
                        tiny = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("NuFF") + 5, 1);
                        teeny = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("NuMUX") + 6, 1);
                        XDL_mux_tmp[i] = XDL_mux_tmp[i].Replace("NuFF<" + tiny, "NuFF<" + num_ff);
                        if (num_ff < Int16.Parse(tiny))
                            XDL_mux_tmp[i] = XDL_mux_tmp[i].Replace("NuMUX<" + teeny, "NuMUX<" + num_mux / 2);
                        if (num_ff == 0)/////////The brother creating and because of that the $ is added to another one
                            XDL_mux_tmp[i] = XDL_mux_tmp[i].Insert(XDL_mux_tmp[i].Length, " $");
                    }
                    if (num_ff != 0)// In this situation we could route some of the flipflops but not all of them
                    {
                        addvanced_multiplexer NewAdv = new addvanced_multiplexer();
                        if (Int16.Parse(WOR) % 2 == 0) wor_b = Int16.Parse(WOR) + 1;
                        else wor_b = Int16.Parse(WOR) - 1;
                        Sbro = "CT" + TYP + " ROW" + ROW + " COL" + COL + " STL WOR" + wor_b.ToString();
                        Sbro1 = "CT" + TYP + " ROW" + ROW + " COL" + COL + " STM WOR" + wor_b.ToString();
                        //////find brother
                        if (TYP == "LL")
                        {
                            Brother = NewAdv.search(0, Sbro, ref XDL_mux_tmp, Number_of_lines + 50);
                            IndBro = NewAdv.search_ind(0, Sbro, ref XDL_mux_tmp, Number_of_lines + 50);
                        }
                        else if (TYP == "LM")
                        {
                            Brother = NewAdv.search(0, Sbro1, ref XDL_mux_tmp, Number_of_lines + 50);
                            IndBro = NewAdv.search_ind(0, Sbro1, ref XDL_mux_tmp, Number_of_lines + 50);
                            if (Brother == "")
                            {
                                Brother = NewAdv.search(0, Sbro, ref XDL_mux_tmp, Number_of_lines + 50);
                                IndBro = NewAdv.search_ind(0, Sbro, ref XDL_mux_tmp, Number_of_lines + 50);
                            }
                        }
                        bro_tmp = Brother;
                        if (Brother != "")
                        {
                            ind_ff = Brother.IndexOf("FF<");
                            if (Brother.IndexOf("NuFF<0") != -1 & (Brother.IndexOf("$") == -1 || Brother.IndexOf("#") == -1))
                            {
                                num_mux = 2 * Int32.Parse(Brother.Substring(ind_ff - 6, 1));
                            }
                            else num_mux = 0;
                        }
                        ////////////////////////////////////////////////////////////////////////
                        if (Brother != "")
                        {
                            F11 = 0; F12 = 0; F21 = 0; F22 = 0; F31 = 0; F32 = 0; F41 = 0; F42 = 0;
                            if (Brother.IndexOf("A6LUT") == -1 & Brother.IndexOf("A5LUT") == -1 & Brother.IndexOf("AOUTMUX") == -1) { F11 = 1; F12 = 1; }
                            if (Brother.IndexOf("B6LUT") == -1 & Brother.IndexOf("B5LUT") == -1 & Brother.IndexOf("BOUTMUX") == -1) { F21 = 1; F22 = 1; }
                            if (Brother.IndexOf("C6LUT") == -1 & Brother.IndexOf("C5LUT") == -1 & Brother.IndexOf("COUTMUX") == -1) { F31 = 1; F32 = 1; }
                            if (Brother.IndexOf("D6LUT") == -1 & Brother.IndexOf("D5LUT") == -1 & Brother.IndexOf("DOUTMUX") == -1) { F41 = 1; F42 = 1; }

                            if (XDL_mux_tmp[i].IndexOf("1b H") != -1 & XDL_mux_tmp[i].IndexOf("1b H]") == -1 & num_mux > 0)
                            {
                                if (Int32.Parse(WOR) % 2 != 0) { logic = 0; }
                                else { logic = 4; }
                                lOG = logic.ToString();
                                Easier2("INB", ref Brother, ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "1b H",
                                       ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                                       ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i], TYP, Styp, XDL_mux_tmp,
                                        Number_of_lines + 50, WOR);
                            }
                            if (XDL_mux_tmp[i].IndexOf("2b H") != -1 & XDL_mux_tmp[i].IndexOf("2b H]") == -1 & num_mux > 0)
                            {
                                if (Int32.Parse(WOR) % 2 != 0) { logic = 16; }
                                else { logic = 20; }
                                lOG = logic.ToString();
                                Easier2("INB", ref Brother, ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "2b H",
                                       ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                                       ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i], TYP, Styp, XDL_mux_tmp,
                                        Number_of_lines + 50, WOR);
                            }
                            if (XDL_mux_tmp[i].IndexOf("3b H") != -1 & XDL_mux_tmp[i].IndexOf("3b H]") == -1 & num_mux > 0)
                            {
                                if (Int32.Parse(WOR) % 2 != 0) { logic = 1; }
                                else { logic = 5; }
                                lOG = logic.ToString();
                                Easier2("INB", ref Brother, ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "3b H",
                                       ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                                       ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i], TYP, Styp, XDL_mux_tmp,
                                        Number_of_lines + 50, WOR);
                            }
                            if (XDL_mux_tmp[i].IndexOf("4b H") != -1 & XDL_mux_tmp[i].IndexOf("4b H]") == -1 & num_mux > 0)
                            {
                                if (Int32.Parse(WOR) % 2 != 0) { logic = 17; }
                                else { logic = 21; }
                                lOG = logic.ToString();
                                Easier2("INB", ref Brother, ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "4b H",
                                       ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                                       ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i], TYP, Styp, XDL_mux_tmp,
                                        Number_of_lines + 50, WOR);
                            }
                            if (XDL_mux_tmp[i].IndexOf("5b H") != -1 & XDL_mux_tmp[i].IndexOf("5b H]") == -1 & num_mux > 0)
                            {
                                if (Int32.Parse(WOR) % 2 != 0) { logic = 2; }
                                else { logic = 6; }
                                lOG = logic.ToString();
                                Easier2("INB", ref Brother, ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "5b H",
                                       ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                                       ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i], TYP, Styp, XDL_mux_tmp,
                                        Number_of_lines + 50, WOR);
                            }
                            if (XDL_mux_tmp[i].IndexOf("6b H") != -1 & XDL_mux_tmp[i].IndexOf("6b H]") == -1 & num_mux > 0)
                            {
                                if (Int32.Parse(WOR) % 2 != 0) { logic = 18; }
                                else { logic = 22; }
                                lOG = logic.ToString();
                                Easier2("INB", ref Brother, ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "6b H",
                                       ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                                       ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i], TYP, Styp, XDL_mux_tmp,
                                        Number_of_lines + 50, WOR);
                            }
                            if (XDL_mux_tmp[i].IndexOf("7b H") != -1 & XDL_mux_tmp[i].IndexOf("7b H]") == -1 & num_mux > 0)
                            {
                                if (Int32.Parse(WOR) % 2 != 0) { logic = 3; }
                                else { logic = 7; }
                                lOG = logic.ToString();
                                Easier2("INB", ref Brother, ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "7b H",
                                       ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                                       ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i], TYP, Styp, XDL_mux_tmp,
                                        Number_of_lines + 50, WOR);
                            }
                            if (XDL_mux_tmp[i].IndexOf("8b H") != -1 & XDL_mux_tmp[i].IndexOf("8b H]") == -1 & num_mux > 0)
                            {
                                if (Int32.Parse(WOR) % 2 != 0) { logic = 19; }
                                else { logic = 23; }
                                lOG = logic.ToString();
                                Easier2("INB", ref Brother, ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "8b H",
                                       ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                                       ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i], TYP, Styp, XDL_mux_tmp,
                                        Number_of_lines + 50, WOR);
                            }
                            string tiny = ""; string teeny = "";
                            tiny = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("NuFF") + 5, 1);
                            teeny = Brother.Substring(Brother.IndexOf("NuMUX") + 6, 1);
                            XDL_mux_tmp[i] = XDL_mux_tmp[i].Replace("NuFF<" + tiny, "NuFF<" + num_ff);
                            if (num_ff < Int16.Parse(tiny))
                            {
                                Brother = Brother.Replace("NuMUX<" + teeny, "NuMUX<" + num_mux / 2);
                                XDL_mux_tmp[IndBro] = XDL_mux_tmp[IndBro].Replace(bro_tmp, Brother);
                               // size[IndBro] = Brother.Length; 
                            }
                            if (num_ff == 0)/////////The brother creating and because of that the $ is added to another one
                                XDL_mux_tmp[i] = XDL_mux_tmp[i].Insert(XDL_mux_tmp[i].Length, " $");
                                
                        }
                        if (num_ff > 0) 
                        {
                            int ret = 0;
                            addvanced_multiplexer Advancmux = new addvanced_multiplexer();
                            ret = Advancmux.search_in_another_CLBs(ref LOC, ref IndBro, num_ff, WOR, ROW, COL, ref Brother, ref num_mux, ref XDL_mux_tmp, Number_of_lines + 50);
                            bro_tmp = Brother;
                             if (Brother != "")
                                    {
                                        F11 = 0; F12 = 0; F21 = 0; F22 = 0; F31 = 0; F32 = 0; F41 = 0; F42 = 0;
                                        if (Brother.IndexOf("A6LUT") == -1 & Brother.IndexOf("A5LUT") == -1 & Brother.IndexOf("AOUTMUX") == -1) { F11 = 1; F12 = 1; }
                                        if (Brother.IndexOf("B6LUT") == -1 & Brother.IndexOf("B5LUT") == -1 & Brother.IndexOf("BOUTMUX") == -1) { F21 = 1; F22 = 1; }
                                        if (Brother.IndexOf("C6LUT") == -1 & Brother.IndexOf("C5LUT") == -1 & Brother.IndexOf("COUTMUX") == -1) { F31 = 1; F32 = 1; }
                                        if (Brother.IndexOf("D6LUT") == -1 & Brother.IndexOf("D5LUT") == -1 & Brother.IndexOf("DOUTMUX") == -1) { F41 = 1; F42 = 1; }

                                        if (XDL_mux_tmp[i].IndexOf("1b H") != -1 & XDL_mux_tmp[i].IndexOf("1b H]") == -1 & num_mux > 0)
                                        {
                                            if (Int32.Parse(WOR) % 2 != 0) { logic = 0; }
                                            else { logic = 4; }
                                            lOG = logic.ToString();
                                            Easier2(LOC, ref Brother, ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "1b H",
                                                   ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                                                   ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i], TYP, Styp, XDL_mux_tmp,
                                                    Number_of_lines + 50, WOR);
                                        }
                                        if (XDL_mux_tmp[i].IndexOf("2b H") != -1 & XDL_mux_tmp[i].IndexOf("2b H]") == -1 & num_mux > 0)
                                        {
                                            if (Int32.Parse(WOR) % 2 != 0) { logic = 16; }
                                            else { logic = 20; }
                                            lOG = logic.ToString();
                                            Easier2(LOC, ref Brother, ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "2b H",
                                                   ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                                                   ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i], TYP, Styp, XDL_mux_tmp,
                                                    Number_of_lines + 50, WOR);
                                        }
                                        if (XDL_mux_tmp[i].IndexOf("3b H") != -1 & XDL_mux_tmp[i].IndexOf("3b H]") == -1 & num_mux > 0)
                                        {
                                            if (Int32.Parse(WOR) % 2 != 0) { logic = 1; }
                                            else { logic = 5; }
                                            lOG = logic.ToString();
                                            Easier2(LOC, ref Brother, ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "3b H",
                                                   ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                                                   ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i], TYP, Styp, XDL_mux_tmp,
                                                    Number_of_lines + 50, WOR);
                                        }
                                        if (XDL_mux_tmp[i].IndexOf("4b H") != -1 & XDL_mux_tmp[i].IndexOf("4b H]") == -1 & num_mux > 0)
                                        {
                                            if (Int32.Parse(WOR) % 2 != 0) { logic = 17; }
                                            else { logic = 21; }
                                            lOG = logic.ToString();
                                            Easier2(LOC, ref Brother, ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "4b H",
                                                   ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                                                   ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i], TYP, Styp, XDL_mux_tmp,
                                                    Number_of_lines + 50, WOR);
                                        }
                                        if (XDL_mux_tmp[i].IndexOf("5b H") != -1 & XDL_mux_tmp[i].IndexOf("5b H]") == -1 & num_mux > 0)
                                        {
                                            if (Int32.Parse(WOR) % 2 != 0) { logic = 2; }
                                            else { logic = 6; }
                                            lOG = logic.ToString();
                                            Easier2(LOC, ref Brother, ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "5b H",
                                                   ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                                                   ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i], TYP, Styp, XDL_mux_tmp,
                                                    Number_of_lines + 50, WOR);
                                        }
                                        if (XDL_mux_tmp[i].IndexOf("6b H") != -1 & XDL_mux_tmp[i].IndexOf("6b H]") == -1 & num_mux > 0)
                                        {
                                            if (Int32.Parse(WOR) % 2 != 0) { logic = 18; }
                                            else { logic = 22; }
                                            lOG = logic.ToString();
                                            Easier2(LOC, ref Brother, ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "6b H",
                                                   ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                                                   ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i], TYP, Styp, XDL_mux_tmp,
                                                    Number_of_lines + 50, WOR);
                                        }
                                        if (XDL_mux_tmp[i].IndexOf("7b H") != -1 & XDL_mux_tmp[i].IndexOf("7b H]") == -1 & num_mux > 0)
                                        {
                                            if (Int32.Parse(WOR) % 2 != 0) { logic = 3; }
                                            else { logic = 7; }
                                            lOG = logic.ToString();
                                            Easier2(LOC, ref Brother, ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "7b H",
                                                   ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                                                   ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i], TYP, Styp, XDL_mux_tmp,
                                                    Number_of_lines + 50, WOR);
                                        }
                                        if (XDL_mux_tmp[i].IndexOf("8b H") != -1 & XDL_mux_tmp[i].IndexOf("8b H]") == -1 & num_mux > 0)
                                        {
                                            if (Int32.Parse(WOR) % 2 != 0) { logic = 19; }
                                            else { logic = 23; }
                                            lOG = logic.ToString();
                                            Easier2(LOC, ref Brother, ref num_ff, ref num_mux, ROW, COL, logic, ref His_NUM, ref ALL, lOG, "8b H",
                                                    ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31,
                                                    ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i], TYP, Styp, XDL_mux_tmp,
                                                     Number_of_lines + 50, WOR); 
                                        }
                                    }
                             string tiny = ""; string teeny = "";
                             tiny = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("NuFF") + 5, 1);
                             teeny = Brother.Substring(Brother.IndexOf("NuMUX") + 6, 1);
                             XDL_mux_tmp[i] = XDL_mux_tmp[i].Replace("NuFF<" + tiny, "NuFF<" + num_ff);
                             if (num_ff < Int16.Parse(tiny))
                             {
                                 Brother = Brother.Replace("NuMUX<" + teeny, "NuMUX<" + num_mux / 2);
                                 if (ret == 0)
                                 {                                     
                                     XDL_mux_tmp[IndBro] = XDL_mux_tmp[IndBro].Replace(bro_tmp, Brother);
                                 //    size[IndBro] = Brother.Length;                                                                          
                                 }
                                 else if (ret == 1)
                                 {
                                     XDL_mux_tmp[Number_of_lines + U] = Brother;
                                     U++;
                                 }
                             }
                             if (num_ff == 0)/////////The brother creating and because of that the $ is added to another one
                                 XDL_mux_tmp[i] = XDL_mux_tmp[i].Insert(XDL_mux_tmp[i].Length, " $");
                        }

                    }
                }
            }
        }       

        public void Easier2(string LOC,ref string Brother,ref int num_ff, ref int num_mux, string ROW, string COL, int logic, ref int His_NUM,
                             ref string ALL, string lOG, string p, ref string His_FF, ref string BYPB, ref string Out_H_FF,
                             ref string H_logic, ref int F11, ref int F12, ref int F21, ref int F22, ref int F31, ref int F32,
                             ref int F41, ref int F42, ref string B, ref string Bh, ref string Pin1, ref string pin,
                             ref string p_26, string TYP, string Styp, string[] XDL_mux_tmp,long num_line,string WOR)
        {
            addvanced_multiplexer MultiPLX = new addvanced_multiplexer();
            multiplexer Multiplexer2 = new multiplexer();
            string inst_name = ""; string inst_name_H1 = ""; string brother_ff = ""; string goal = "";
            int cache = 0;
            cache = p_26.IndexOf(p);
            His_FF = p_26.Substring(cache + 5, 3);
            inst_name_H1 = Brother.Substring(Brother.IndexOf("(") + 1, Brother.IndexOf(")") - Brother.IndexOf("(") - 1);
            inst_name = p_26.Substring(p_26.IndexOf("(") + 1, p_26.IndexOf(")") - p_26.IndexOf("(") - 1);

            addvanced_multiplexer ad_m = new addvanced_multiplexer();
       //     if (Int16.Parse(WOR) % 2 == 0) WOR = (Int16.Parse(WOR) + 1).ToString();
      //      else WOR = (Int16.Parse(WOR) - 1).ToString();
            if (LOC != "INB")
            {
                if (LOC == "WST") goal = (Int16.Parse(WOR) - 1) + " " + COL;
                else if (LOC == "WSTP") goal = (Int16.Parse(WOR) - 2) + " " + COL;
                else if (LOC == "NRT") goal = WOR + " " + (Int16.Parse(COL) + 1);
                else if (LOC == "EST") goal = (Int16.Parse(WOR) + 1) + " " + COL;
                else if (LOC == "ESTP") goal = (Int16.Parse(WOR) + 2) + " " + COL;
                else if (LOC == "SUT") goal = WOR + " " + (Int16.Parse(COL) - 1);
                else if (LOC == "NWT") goal = (Int16.Parse(WOR) - 1) + " " + (Int16.Parse(COL) + 1);
                else if (LOC == "NWTP") goal = (Int16.Parse(WOR) - 2) + " " + (Int16.Parse(COL) + 1);
                else if (LOC == "NET") goal = (Int16.Parse(WOR) + 1) + " " + (Int16.Parse(COL) + 1);
                else if (LOC == "NETP") goal = (Int16.Parse(WOR) + 2) + " " + (Int16.Parse(COL) + 1);
                else if (LOC == "SWT") goal = (Int16.Parse(WOR) - 1) + " " + (Int16.Parse(COL) - 1);
                else if (LOC == "SWTP") goal = (Int16.Parse(WOR) - 2) + " " + (Int16.Parse(COL) - 1);
                else if (LOC == "SET") goal = (Int16.Parse(WOR) + 1) + " " + (Int16.Parse(COL) - 1);
                else if (LOC == "SETP") goal = (Int16.Parse(WOR) + 2) + " " + (Int16.Parse(COL) - 1);
                else if (LOC == "WST2") goal = (Int16.Parse(WOR) - 3) + " " + COL;
                else if (LOC == "WST2P") goal = (Int16.Parse(WOR) - 4) + " " + COL;
                else if (LOC == "NRT2") goal = WOR + " " + (Int16.Parse(COL) + 2);
                else if (LOC == "EST2") goal = (Int16.Parse(WOR) + 3) + " " + COL;
                else if (LOC == "EST2P") goal = (Int16.Parse(WOR) + 4) + " " + COL;
                else if (LOC == "SUT2") goal = WOR + " " + (Int16.Parse(COL) - 2);
                else if (LOC == "WST4") goal = (Int16.Parse(WOR) - 7) + " " + COL;
                else if (LOC == "WST4P") goal = (Int16.Parse(WOR) - 8) + " " + COL;
                else if (LOC == "NRT4") goal = WOR + " " + (Int16.Parse(COL) + 4);
                else if (LOC == "EST4") goal = (Int16.Parse(WOR) + 7) + " " + COL;
                else if (LOC == "EST4P") goal = (Int16.Parse(WOR) + 8) + " " + COL;
                else if (LOC == "SUT4") goal = WOR + " " + (Int16.Parse(COL) - 4);
                else if (LOC == "NRTP" & Int16.Parse(WOR) % 2 == 0) goal = (Int16.Parse(WOR) - 1) + " " + (Int16.Parse(COL) + 1);
                else if (LOC == "SUTP" & Int16.Parse(WOR) % 2 == 0) goal = (Int16.Parse(WOR) - 1) + " " + (Int16.Parse(COL) - 1);
                else if (LOC == "NRT2P" & Int16.Parse(WOR) % 2 == 0) goal = (Int16.Parse(WOR) - 1) + " " + (Int16.Parse(COL) + 2);
                else if (LOC == "SUT2P" & Int16.Parse(WOR) % 2 == 0) goal = (Int16.Parse(WOR) - 1) + " " + (Int16.Parse(COL) - 2);
                else if (LOC == "NRT4P" & Int16.Parse(WOR) % 2 == 0) goal = (Int16.Parse(WOR) - 1) + " " + (Int16.Parse(COL) + 4);
                else if (LOC == "SUT4P" & Int16.Parse(WOR) % 2 == 0) goal = (Int16.Parse(WOR) - 1) + " " + (Int16.Parse(COL) - 4);
                else if (LOC == "NRTP" & Int16.Parse(WOR) % 2 == 1) goal = (Int16.Parse(WOR) + 1) + " " + (Int16.Parse(COL) + 1);
                else if (LOC == "SUTP" & Int16.Parse(WOR) % 2 == 1) goal = (Int16.Parse(WOR) + 1) + " " + (Int16.Parse(COL) - 1);
                else if (LOC == "NRT2P" & Int16.Parse(WOR) % 2 == 1) goal = (Int16.Parse(WOR) + 1) + " " + (Int16.Parse(COL) + 2);
                else if (LOC == "SUT2P" & Int16.Parse(WOR) % 2 == 1) goal = (Int16.Parse(WOR) + 1) + " " + (Int16.Parse(COL) - 2);
                else if (LOC == "NRT4P" & Int16.Parse(WOR) % 2 == 1) goal = (Int16.Parse(WOR) + 1) + " " + (Int16.Parse(COL) + 4);
                else if (LOC == "SUT4P" & Int16.Parse(WOR) % 2 == 1) goal = (Int16.Parse(WOR) + 1) + " " + (Int16.Parse(COL) - 4);
                brother_ff = ad_m.search(0, goal, ref XDL_mux_tmp, num_line);
            }

            if (LOC != "INB") inst_name = Brother.Substring(Brother.IndexOf("(") + 1, Brother.IndexOf(")") - Brother.IndexOf("(") - 1);
                //brother_ff.Substring(brother_ff.IndexOf("(") + 1, brother_ff.IndexOf(")") - brother_ff.IndexOf("(") - 1);
            else inst_name = inst_name_H1;
            if (logic == 0 || logic == 1 || logic == 2 || logic == 3 || logic == 16 || logic == 17 || logic == 18 || logic == 19)
            {
                MultiPLX.set_Arguments1(LOC,p,ref  Brother, ref  His_FF, ref  BYPB, ref  Out_H_FF, ref  H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1,ref pin, ref p_26);
            }
            else if (logic == 4 || logic == 5 || logic == 6 || logic == 7 || logic == 20 || logic == 21 || logic == 22 || logic == 23)
            {
                MultiPLX.set_Arguments(LOC, p, ref  Brother, ref  His_FF, ref  BYPB, ref  Out_H_FF, ref  H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p_26);
            }
            His_NUM++;
            int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
            if (indINT == -1)
                Console.Write("NotFound\n");
            int indkama = ALL.IndexOf(",", indINT - 115);
            string temporary = ALL.Substring(indkama, indINT - indkama);
            Multiplexer2.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
            num_mux--;
            num_ff--;
        }

        public void Easier(string LOC,ref int num_ff, ref int num_mux, string ROW,string COL,int logic, ref int His_NUM, 
                            ref string ALL, string lOG, string p, ref string His_FF, ref string BYPB, ref string Out_H_FF,
                            ref string H_logic, ref int F11, ref int F12, ref int F21, ref int F22, ref int F31, ref int F32,
                            ref int F41, ref int F42, ref string B, ref string Bh, ref string Pin1, ref string pin, ref string p_23,
                            string TYP, string Styp)
        {
            multiplexer MultiPLX = new multiplexer();
            string inst_name = ""; 
            int cache = 0;
            cache = p_23.IndexOf(p);
            His_FF = p_23.Substring(cache + 5, 3);
            inst_name = p_23.Substring(p_23.IndexOf("(") + 1, p_23.IndexOf(")") - p_23.IndexOf("(") - 1);
            if (logic == 0 || logic== 1 || logic == 2 || logic == 3 || logic == 16 || logic == 17 || logic == 18 || logic == 19)
            {
                MultiPLX.set_arguments1(LOC, p, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p_23);
            }
            else if (logic == 4 || logic == 5 || logic == 6 || logic == 7 || logic == 20 || logic == 21 || logic == 22 || logic == 23)
            {
                MultiPLX.set_arguments(LOC, p, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p_23);
            }
            His_NUM++;
            int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
            if (indINT == -1)
                Console.Write("salam\n");
            int indkama = ALL.IndexOf(",", indINT - 115);
            string temporary = ALL.Substring(indkama, indINT - indkama);
            MultiPLX.Routing_function(inst_name, inst_name, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
            num_mux--;
            num_ff--;
        }
    }
}
