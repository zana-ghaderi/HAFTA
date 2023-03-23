using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace PARSER
{
    public class addvanced_multiplexer
    {
        public static int His_NUM = 0;
        public static int HIS_INST_NUM = 0;
        public void search_mux2(ref string[] XDL_mux_tmp, long Number_of_lines,ref string ALL,ref int His_NUM,ref int U)
        {
            int ind_ff = 0; int num_ff = 0; int num_mux = 0;
            string ROW = ""; string COL = ""; string Out_H_FF = "";
            string WOR = ""; string TYP = ""; string Styp = "";
            int logic = 0; string lOG = ""; string B = ""; string Bh = "";
            string BYPB = ""; string pin = ""; string His_FF = ""; string Pin1 = "";string H_logic = "";
            int F11 = 0; int F12 = 0; int F21 = 0; int F22 = 0; int F31 = 0; int F32 = 0; int F41 = 0; int F42 = 0;
            int num_s_ff = 0; int ind_s_ff = 0; int i = 0;
            /////////////////////////////////////////////////
            string Brother = ""; int wor_b = 0; string Sbro = ""; string Sbro1 = ""; int IndBro = 0; string bro_temp = "";
            string LOC = ""; string inst_name = ""; string inst_name_H1 = "";
            /////////////////////////////////////////////////
            for (i = 0; i < Number_of_lines; i++)
            {
                IndBro = -1;
                bro_temp = "";
                Brother = "";
                if (XDL_mux_tmp[i].IndexOf("FF ") != -1 & XDL_mux_tmp[i].IndexOf("*") == -1)
                {          
                    ROW = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("ROW") + 3, XDL_mux_tmp[i].IndexOf(" ", XDL_mux_tmp[i].IndexOf("ROW")) - XDL_mux_tmp[i].IndexOf("ROW") - 3);
                    COL = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("COL") + 3, XDL_mux_tmp[i].IndexOf(" ", XDL_mux_tmp[i].IndexOf("COL")) - XDL_mux_tmp[i].IndexOf("COL") - 3);
                    WOR = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("WOR") + 3, XDL_mux_tmp[i].IndexOf(" ", XDL_mux_tmp[i].IndexOf("WOR")) - XDL_mux_tmp[i].IndexOf("WOR") - 3);
                    TYP = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("CT") + 2, 2);
                    Styp = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("ST") + 2, 1);

                    if (XDL_mux_tmp[i].IndexOf("$") == -1)
                    {
                        if (Int16.Parse(WOR) % 2 == 0) wor_b = Int16.Parse(WOR) + 1;
                        else wor_b = Int16.Parse(WOR) - 1;

                        ind_s_ff = XDL_mux_tmp[i].IndexOf("FF<");
                        num_s_ff = Int32.Parse(XDL_mux_tmp[i].Substring(ind_s_ff + 3, 1));
                        
                        Sbro = "CT" + TYP + " ROW" + ROW + " COL" + COL + " STL WOR" + wor_b.ToString();
                        Sbro1 = "CT" + TYP + " ROW" + ROW + " COL" + COL + " STM WOR" + wor_b.ToString();

                        //////find brother
                           
                            IndBro = search_ind(0,Sbro, ref XDL_mux_tmp, Number_of_lines + 50);
                            if (IndBro != -1)
                                Brother = search(0, Sbro, ref XDL_mux_tmp, Number_of_lines + 50);
                            else
                            {
                                IndBro = search_ind(0, Sbro1, ref XDL_mux_tmp, Number_of_lines + 50);
                                Brother = search(0, Sbro1, ref XDL_mux_tmp, Number_of_lines + 50);
                                if (Brother == "")
                                {
                                    IndBro = search_ind(0, Sbro, ref XDL_mux_tmp, Number_of_lines + 50);
                                    Brother = search(0, Sbro, ref XDL_mux_tmp, Number_of_lines + 50);
                                }
                            }
                        bro_temp = Brother;
                        ////////////////////////////////////////////////////////////////////////
                        if (Brother != "" & IndBro != -1)
                        {
                            ind_ff = Brother.IndexOf("FF<");
                            if (Brother.IndexOf("NuFF<0") != -1 & (Brother.IndexOf("$") == -1 || Brother.IndexOf("#") == -1))
                            {
                                num_ff = Int32.Parse(Brother.Substring(ind_ff + 3, 1));
                                num_mux = 2 * Int32.Parse(Brother.Substring(ind_ff - 6, 1));                                
                            }
                            else num_mux = 0;
                            F11 = 0; F12 = 0; F21 = 0; F22 = 0; F31 = 0; F32 = 0; F41 = 0; F21 = 0;
                            if (Brother.IndexOf("A6LUT") == -1 & Brother.IndexOf("A5LUT") == -1 & Brother.IndexOf("AOUTMUX") == -1) { F11 = 1; F12 = 1; }
                            if (Brother.IndexOf("B6LUT") == -1 & Brother.IndexOf("B5LUT") == -1 & Brother.IndexOf("BOUTMUX") == -1) { F21 = 1; F22 = 1; }
                            if (Brother.IndexOf("C6LUT") == -1 & Brother.IndexOf("C5LUT") == -1 & Brother.IndexOf("COUTMUX") == -1) { F31 = 1; F32 = 1; }
                            if (Brother.IndexOf("D6LUT") == -1 & Brother.IndexOf("D5LUT") == -1 & Brother.IndexOf("DOUTMUX") == -1) { F41 = 1; F42 = 1; }

                            ////////Instance from multiplser class to use its functions
                            multiplexer multiplexer1 = new multiplexer();
                            inst_name = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("(") + 1, XDL_mux_tmp[i].IndexOf(")") - XDL_mux_tmp[i].IndexOf("(") - 1);
                            inst_name_H1 = Brother.Substring(Brother.IndexOf("(") + 1, Brother.IndexOf(")") - Brother.IndexOf("(") - 1);
                            ///////if the number of flip flops less than multiplexers
                            if (num_s_ff <= num_mux)
                            {
                                int cache = 0;
                                //AFF
                                if (XDL_mux_tmp[i].IndexOf("1 H") != -1 || XDL_mux_tmp[i+1].IndexOf("*")!= -1)
                                {
                                    if (XDL_mux_tmp[i].IndexOf("1 H]") == -1)
                                    {
                                        cache = XDL_mux_tmp[i].IndexOf("1 H");
                                        His_FF = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("1 H") + 4, 3);
                                        if (cache == -1 & XDL_mux_tmp[i + 1].IndexOf("*") != -1)
                                        {
                                            cache = XDL_mux_tmp[i + 1].IndexOf("1 H");
                                            His_FF = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("1 H") + 4, 3);
                                            inst_name = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("(") + 1, XDL_mux_tmp[i + 1].IndexOf(")") - XDL_mux_tmp[i + 1].IndexOf("(") - 1);
                                        }
                                        if (cache != -1)
                                        {
                                            if (Int32.Parse(WOR) % 2 != 0) logic = 0;
                                            else logic = 4;

                                            lOG = logic.ToString();

                                            if (XDL_mux_tmp[i].IndexOf("1 H") != -1)
                                            {
                                                if (logic == 0)
                                                {
                                                    set_Arguments("INB","1 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                                else if (logic == 4)
                                                {
                                                    set_Arguments1("INB","1 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                            }
                                            else
                                            {
                                                if (logic == 0)
                                                {
                                                    set_Arguments1("INB", "1 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                                else if (logic == 4)
                                                {
                                                    set_Arguments("INB", "1 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                            }

                                            His_NUM++;
                                            int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                                            if (indINT == -1)
                                                Console.Write("NOTFOUND\n");
                                            int indkama = ALL.IndexOf(",", indINT - 115);
                                            string temporary = ALL.Substring(indkama, indINT - indkama);
                                            ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                                            multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                                            num_mux--;
                                            num_s_ff--;
                                        }
                                    }
                                    inst_name = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("(") + 1, XDL_mux_tmp[i].IndexOf(")") - XDL_mux_tmp[i].IndexOf("(") - 1);
                                }////////end of the first if for "1 H"
                                if (XDL_mux_tmp[i].IndexOf("3 H") != -1 || XDL_mux_tmp[i+1].IndexOf("*") != -1)
                                {
                                    if (XDL_mux_tmp[i].IndexOf("3 H]") == -1)
                                    {
                                        cache = XDL_mux_tmp[i].IndexOf("3 H");
                                        His_FF = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("3 H") + 4, 3);
                                        if (cache == -1 & XDL_mux_tmp[i + 1].IndexOf("*") != -1)
                                        {
                                            cache = XDL_mux_tmp[i + 1].IndexOf("3 H");
                                            His_FF = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("3 H") + 4, 3);
                                            inst_name = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("(") + 1, XDL_mux_tmp[i + 1].IndexOf(")") - XDL_mux_tmp[i + 1].IndexOf("(") - 1);
                                        }
                                        if (cache != -1)
                                        {
                                            if (Int32.Parse(WOR) % 2 != 0) logic = 1;
                                            else logic = 5;

                                            lOG = logic.ToString();

                                            if (XDL_mux_tmp[i].IndexOf("3 H") != -1)
                                            {
                                                if (logic == 1)
                                                {
                                                    set_Arguments("INB", "3 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                                else if (logic == 5)
                                                {
                                                    set_Arguments1("INB", "3 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                            }
                                            else
                                            {
                                                if (logic == 1)
                                                {
                                                    set_Arguments1("INB", "3 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                                else if (logic == 5)
                                                {
                                                    set_Arguments("INB", "3 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                            }

                                            His_NUM++;
                                            int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                                            if (indINT == -1)
                                                Console.Write("NOTFOUND\n");
                                            int indkama = ALL.IndexOf(",", indINT - 115);
                                            string temporary = ALL.Substring(indkama, indINT - indkama);
                                            ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                                            multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                                            num_mux--;
                                            num_s_ff--;
                                        }
                                    }
                                    inst_name = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("(") + 1, XDL_mux_tmp[i].IndexOf(")") - XDL_mux_tmp[i].IndexOf("(") - 1);
                                }////////end of the first if for "3 H"
                                if (XDL_mux_tmp[i].IndexOf("5 H") != -1 || XDL_mux_tmp[i+1].IndexOf("*") != -1)
                                {
                                    if (XDL_mux_tmp[i].IndexOf("5 H]") == -1)
                                    {
                                        cache = XDL_mux_tmp[i].IndexOf("5 H");
                                        His_FF = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("5 H") + 4, 3);
                                        if (cache == -1 & XDL_mux_tmp[i + 1].IndexOf("*") != -1)
                                        {
                                            cache = XDL_mux_tmp[i + 1].IndexOf("5 H");
                                            His_FF = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("5 H") + 4, 3);
                                            inst_name = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("(") + 1, XDL_mux_tmp[i + 1].IndexOf(")") - XDL_mux_tmp[i + 1].IndexOf("(") - 1);
                                        }
                                        if (cache != -1)
                                        {
                                            if (Int32.Parse(WOR) % 2 != 0) logic = 2;
                                            else logic = 6;

                                            lOG = logic.ToString();

                                            if (XDL_mux_tmp[i].IndexOf("5 H") != -1)
                                            {
                                                if (logic == 2)
                                                {
                                                    set_Arguments("INB", "5 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                                else if (logic == 6)
                                                {
                                                    set_Arguments1("INB", "5 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                            }
                                            else
                                            {
                                                if (logic == 2)
                                                {
                                                    set_Arguments1("INB", "5 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                                else if (logic == 6)
                                                {
                                                    set_Arguments("INB", "5 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                            }

                                            His_NUM++;
                                            int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                                            if (indINT == -1)
                                                Console.Write("NOTFOUND\n");
                                            int indkama = ALL.IndexOf(",", indINT - 115);
                                            string temporary = ALL.Substring(indkama, indINT - indkama);
                                            ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                                            multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                                            num_mux--;
                                            num_s_ff--;
                                        }
                                    }
                                    inst_name = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("(") + 1, XDL_mux_tmp[i].IndexOf(")") - XDL_mux_tmp[i].IndexOf("(") - 1);
                                }////////end of the first if for "5 H"
                                if (XDL_mux_tmp[i].IndexOf("7 H") != -1 || XDL_mux_tmp[i+1].IndexOf("*") != -1)
                                {
                                    if (XDL_mux_tmp[i].IndexOf("7 H]") == -1)
                                    {
                                        cache = XDL_mux_tmp[i].IndexOf("7 H");
                                        His_FF = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("7 H") + 4, 3);
                                        if (cache == -1 & XDL_mux_tmp[i + 1].IndexOf("*") != -1)
                                        {
                                            cache = XDL_mux_tmp[i + 1].IndexOf("7 H");
                                            His_FF = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("7 H") + 4, 3);
                                            inst_name = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("(") + 1, XDL_mux_tmp[i + 1].IndexOf(")") - XDL_mux_tmp[i + 1].IndexOf("(") - 1);
                                        }
                                        if (cache != -1)
                                        {

                                            if (Int32.Parse(WOR) % 2 != 0) logic = 3;
                                            else logic = 7;

                                            lOG = logic.ToString();

                                            if (XDL_mux_tmp[i].IndexOf("7 H") != -1)
                                            {
                                                if (logic == 3)
                                                {
                                                    set_Arguments("INB","7 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                                else if (logic == 7)
                                                {
                                                    set_Arguments1("INB", "7 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                            }
                                            else
                                            {
                                                if (logic == 3)
                                                {
                                                    set_Arguments1("INB", "7 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                                else if (logic == 7)
                                                {
                                                    set_Arguments("INB", "7 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                            }

                                            His_NUM++;
                                            int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                                            if (indINT == -1)
                                                Console.Write("NOTFOUND\n");
                                            int indkama = ALL.IndexOf(",", indINT - 115);
                                            string temporary = ALL.Substring(indkama, indINT - indkama);
                                            ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                                            multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                                            num_mux--;
                                            num_s_ff--;
                                        }
                                    }
                                    inst_name = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("(") + 1, XDL_mux_tmp[i].IndexOf(")") - XDL_mux_tmp[i].IndexOf("(") - 1);
                                }////////end of the first if for "7 H"
                                if (XDL_mux_tmp[i].IndexOf("2 H") != -1 || XDL_mux_tmp[i].IndexOf("*") != -1)
                                {
                                    if (XDL_mux_tmp[i].IndexOf("2 H]") == -1)
                                    {
                                        cache = XDL_mux_tmp[i].IndexOf("2 H");
                                        His_FF = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("2 H") + 4, 3);
                                        if (cache == -1 & XDL_mux_tmp[i + 1].IndexOf("*") != -1)
                                        {
                                            cache = XDL_mux_tmp[i + 1].IndexOf("2 H");
                                            His_FF = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("2 H") + 4, 3);
                                            inst_name = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("(") + 1, XDL_mux_tmp[i + 1].IndexOf(")") - XDL_mux_tmp[i + 1].IndexOf("(") - 1);
                                        }
                                        if (cache != -1)
                                        {
                                            if (Int32.Parse(WOR) % 2 != 0) logic = 16;
                                            else logic = 20;

                                            lOG = logic.ToString();

                                            if (XDL_mux_tmp[i].IndexOf("2 H") != -1)
                                            {
                                                if (logic == 16)
                                                {
                                                    set_Arguments("INB", "2 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                                else if (logic == 20)
                                                {
                                                    set_Arguments1("INB", "2 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                            }
                                            else
                                            {
                                                if (logic == 16)
                                                {
                                                    set_Arguments1("INB", "2 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                                else if (logic == 20)
                                                {
                                                    set_Arguments("INB", "2 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                            }

                                            His_NUM++;
                                            int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                                            if (indINT == -1)
                                                Console.Write("NOTFOUND\n");
                                            int indkama = ALL.IndexOf(",", indINT - 115);
                                            string temporary = ALL.Substring(indkama, indINT - indkama);
                                            ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                                            multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                                            num_mux--;
                                            num_s_ff--;
                                        }
                                    }
                                    inst_name = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("(") + 1, XDL_mux_tmp[i].IndexOf(")") - XDL_mux_tmp[i].IndexOf("(") - 1);
                                }////////end of the first if for "2 H"
                                if (XDL_mux_tmp[i].IndexOf("4 H") != -1 || XDL_mux_tmp[i+1].IndexOf("*") != -1)
                                {
                                    if (XDL_mux_tmp[i].IndexOf("4 H]") == -1)
                                    {
                                        cache = XDL_mux_tmp[i].IndexOf("4 H");
                                        His_FF = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("4 H") + 4, 3);
                                        if (cache == -1 & XDL_mux_tmp[i + 1].IndexOf("*") != -1)
                                        {
                                            cache = XDL_mux_tmp[i + 1].IndexOf("4 H");
                                            His_FF = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("4 H") + 4, 3);
                                            inst_name = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("(") + 1, XDL_mux_tmp[i + 1].IndexOf(")") - XDL_mux_tmp[i + 1].IndexOf("(") - 1);
                                        }
                                        if (cache != -1)
                                        {
                                            if (Int32.Parse(WOR) % 2 != 0) logic = 17;
                                            else logic = 21;

                                            lOG = logic.ToString();

                                            if (XDL_mux_tmp[i].IndexOf("4 H") != -1)
                                            {
                                                if (logic == 17)
                                                {
                                                    set_Arguments("INB", "4 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                                else if (logic == 21)
                                                {
                                                    set_Arguments1("INB", "4 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                            }
                                            else
                                            {
                                                if (logic == 17)
                                                {
                                                    set_Arguments1("INB", "4 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                                else if (logic == 21)
                                                {
                                                    set_Arguments("INB", "4 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                            }

                                            His_NUM++;
                                            int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                                            if (indINT == -1)
                                                Console.Write("NOTFOUND\n");
                                            int indkama = ALL.IndexOf(",", indINT - 115);
                                            string temporary = ALL.Substring(indkama, indINT - indkama);
                                            ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                                            multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                                            num_mux--;
                                            num_s_ff--;
                                        }
                                    }
                                    inst_name = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("(") + 1, XDL_mux_tmp[i].IndexOf(")") - XDL_mux_tmp[i].IndexOf("(") - 1);
                                }////////end of the first if for "4 H"
                                if (XDL_mux_tmp[i].IndexOf("6 H") != -1 || XDL_mux_tmp[i+1].IndexOf("*") != -1)
                                {
                                    if (XDL_mux_tmp[i].IndexOf("6 H]") == -1)
                                    {
                                        cache = XDL_mux_tmp[i].IndexOf("6 H");
                                        His_FF = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("6 H") + 4, 3);
                                        if (cache == -1 & XDL_mux_tmp[i + 1].IndexOf("*") != -1)
                                        {
                                            cache = XDL_mux_tmp[i + 1].IndexOf("6 H");
                                            His_FF = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("6 H") + 4, 3);
                                            inst_name = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("(") + 1, XDL_mux_tmp[i + 1].IndexOf(")") - XDL_mux_tmp[i + 1].IndexOf("(") - 1);
                                        }
                                        if (cache != -1)
                                        {
                                            if (Int32.Parse(WOR) % 2 != 0) logic = 18;
                                            else logic = 22;

                                            lOG = logic.ToString();

                                            if (XDL_mux_tmp[i].IndexOf("6 H") != -1)
                                            {
                                                if (logic == 18)
                                                {
                                                    set_Arguments("INB", "6 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                                else if (logic == 22)
                                                {
                                                    set_Arguments1("INB", "6 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                            }
                                            else
                                            {
                                                if (logic == 18)
                                                {
                                                    set_Arguments1("INB", "6 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                                else if (logic == 22)
                                                {
                                                    set_Arguments("INB", "6 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                            }
                                            His_NUM++;
                                            int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                                            if (indINT == -1)
                                                Console.Write("NOTFOUND\n");
                                            int indkama = ALL.IndexOf(",", indINT - 115);
                                            string temporary = ALL.Substring(indkama, indINT - indkama);
                                            ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                                            multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                                            num_mux--;
                                            num_s_ff--;
                                        }
                                    }
                                    inst_name = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("(") + 1, XDL_mux_tmp[i].IndexOf(")") - XDL_mux_tmp[i].IndexOf("(") - 1);
                                }////////end of the first if for "6 H"
                                if (XDL_mux_tmp[i].IndexOf("8 H") != -1 || XDL_mux_tmp[i+1].IndexOf("*") != -1)
                                {
                                    if (XDL_mux_tmp[i].IndexOf("8 H]") == -1)
                                    {
                                        cache = XDL_mux_tmp[i].IndexOf("8 H");
                                        His_FF = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("8 H") + 4, 3);
                                        if (cache == -1 & XDL_mux_tmp[i + 1].IndexOf("*") != -1)
                                        {
                                            cache = XDL_mux_tmp[i + 1].IndexOf("8 H");
                                            His_FF = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("8 H") + 4, 3);
                                            inst_name = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("(") + 1, XDL_mux_tmp[i + 1].IndexOf(")") - XDL_mux_tmp[i + 1].IndexOf("(") - 1);
                                        }
                                        if (cache != -1)
                                        {
                                            if (Int32.Parse(WOR) % 2 != 0) logic = 19;
                                            else logic = 23;

                                            lOG = logic.ToString();

                                            if (XDL_mux_tmp[i].IndexOf("8 H") != -1)
                                            {
                                                if (logic == 19)
                                                {
                                                    set_Arguments("INB", "8 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                                else if (logic == 23)
                                                {
                                                    set_Arguments1("INB", "8 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                            }
                                            else
                                            {
                                                if (logic == 19)
                                                {
                                                    set_Arguments1("INB", "8 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                                else if (logic == 23)
                                                {
                                                    set_Arguments("INB", "8 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                                }
                                            }

                                            His_NUM++;
                                            int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                                            if (indINT == -1)
                                                Console.Write("NOTFOUND\n");
                                            int indkama = ALL.IndexOf(",", indINT - 115);
                                            string temporary = ALL.Substring(indkama, indINT - indkama);
                                            ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                                            multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                                            num_mux--;
                                            num_s_ff--;
                                        }
                                    }
                                    inst_name = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("(") + 1, XDL_mux_tmp[i].IndexOf(")") - XDL_mux_tmp[i].IndexOf("(") - 1);
                                }////////end of the first if for "8 H"

                                string tiny = ""; string teeny = "";
                                tiny = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("NuFF") + 5, 1);
                                teeny = Brother.Substring(Brother.IndexOf("NuMUX") + 6, 1);
                                XDL_mux_tmp[i] = XDL_mux_tmp[i].Replace("NuFF<" + tiny, "NuFF<" + num_s_ff);
                                if (num_s_ff < Int16.Parse(tiny))
                                {
                                    Brother = Brother.Replace("NuMUX<" + teeny, "NuMUX<" + num_mux / 2);
                                    XDL_mux_tmp[IndBro] = XDL_mux_tmp[IndBro].Replace(bro_temp, Brother);
                                  //  size[IndBro] = Brother.Length;
                                }
                                if( num_s_ff == 0 )
                                XDL_mux_tmp[i] = XDL_mux_tmp[i].Insert(XDL_mux_tmp[i].Length, " $");                                      
                            }
                                ////////if number of multiplexers less tan flipflops
                            else if (num_mux != 0 & num_s_ff != 0)
                            {
                                int cache = 0;
                                //AFF
                                inst_name = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("(") + 1, XDL_mux_tmp[i].IndexOf(")") - XDL_mux_tmp[i].IndexOf("(") - 1);
                                inst_name_H1 = Brother.Substring(Brother.IndexOf("(") + 1, Brother.IndexOf(")") - Brother.IndexOf("(") - 1);
                                if (XDL_mux_tmp[i].IndexOf("1 H") != -1 & XDL_mux_tmp[i].IndexOf("1 H]") == -1 & num_mux > 0)
                                {
                                    cache = XDL_mux_tmp[i].IndexOf("1 H");
                                    His_FF = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("1 H") + 4, 3);

                                    if (Int32.Parse(WOR) % 2 != 0) logic = 0;
                                    else logic = 4; 

                                    lOG = logic.ToString();

                                    if (logic == 0)
                                    {
                                        set_Arguments("INB", "1 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                    }
                                    else if (logic == 4)
                                    {
                                        set_Arguments1("INB", "1 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                    }

                                    His_NUM++;
                                    int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                                    if (indINT == -1)
                                        Console.Write("NOTFOUND\n");
                                    int indkama = ALL.IndexOf(",", indINT - 115);
                                    string temporary = ALL.Substring(indkama, indINT - indkama);
                                    ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                                    multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                                    num_mux--;
                                    num_s_ff--;
                                }////////end of the first if for "1 H"
                                if (XDL_mux_tmp[i].IndexOf("3 H") != -1 & XDL_mux_tmp[i].IndexOf("3 H]") == -1 & num_mux > 0)
                                {
                                    cache = XDL_mux_tmp[i].IndexOf("3 H");
                                    His_FF = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("3 H") + 4, 3);

                                    if (Int32.Parse(WOR) % 2 != 0) logic = 1;
                                    else logic = 5;

                                    lOG = logic.ToString();

                                    if (logic == 1)
                                    {
                                        set_Arguments("INB", "3 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                    }
                                    else if (logic == 5)
                                    {
                                        set_Arguments1("INB", "3 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                    }

                                    His_NUM++;
                                    int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                                    if (indINT == -1)
                                        Console.Write("NOTFOUND\n");
                                    int indkama = ALL.IndexOf(",", indINT - 115);
                                    string temporary = ALL.Substring(indkama, indINT - indkama);
                                    ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                                    multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                                    num_mux--;
                                    num_s_ff--;
                                }////////end of the first if for "3 H"
                                if (XDL_mux_tmp[i].IndexOf("5 H") != -1 & XDL_mux_tmp[i].IndexOf("5 H]") == -1 & num_mux > 0)
                                {
                                    cache = XDL_mux_tmp[i].IndexOf("5 H");
                                    His_FF = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("5 H") + 4, 3);

                                    if (Int32.Parse(WOR) % 2 != 0) logic = 2;
                                    else logic = 6;

                                    lOG = logic.ToString();

                                    if (logic == 2)
                                    {
                                        set_Arguments("INB", "5 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                    }
                                    else if (logic == 6)
                                    {
                                        set_Arguments1("INB", "5 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                    }

                                    His_NUM++;
                                    int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                                    if (indINT == -1)
                                        Console.Write("NOTFOUND\n");
                                    int indkama = ALL.IndexOf(",", indINT - 115);
                                    string temporary = ALL.Substring(indkama, indINT - indkama);
                                    ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                                    multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                                    num_mux--;
                                    num_s_ff--;
                                }////////end of the first if for "5 H"
                                if (XDL_mux_tmp[i].IndexOf("7 H") != -1 & XDL_mux_tmp[i].IndexOf("7 H]") == -1 & num_mux > 0)
                                {
                                    cache = XDL_mux_tmp[i].IndexOf("7 H");
                                    His_FF = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("7 H") + 4, 3);

                                    if (Int32.Parse(WOR) % 2 != 0) logic = 3;
                                    else logic = 7;

                                    lOG = logic.ToString();

                                    if (logic == 3)
                                    {
                                        set_Arguments("INB", "7 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                    }
                                    else if (logic == 7)
                                    {
                                        set_Arguments1("INB", "7 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                    }

                                    His_NUM++;
                                    int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                                    if (indINT == -1)
                                        Console.Write("NOTFOUND\n");
                                    int indkama = ALL.IndexOf(",", indINT - 115);
                                    string temporary = ALL.Substring(indkama, indINT - indkama);
                                    ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                                    multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                                    num_mux--;
                                    num_s_ff--;
                                }////////end of the first if for "7 H"
                                if (XDL_mux_tmp[i].IndexOf("2 H") != -1 & XDL_mux_tmp[i].IndexOf("2 H]") == -1 & num_mux > 0)
                                {
                                    cache = XDL_mux_tmp[i].IndexOf("2 H");
                                    His_FF = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("2 H") + 4, 3);

                                    if (Int32.Parse(WOR) % 2 != 0) logic = 16;
                                    else logic = 20;

                                    lOG = logic.ToString();

                                    if (logic == 16)
                                    {
                                        set_Arguments("INB", "2 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                    }
                                    else if (logic == 20)
                                    {
                                        set_Arguments1("INB", "2 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                    }

                                    His_NUM++;
                                    int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                                    if (indINT == -1)
                                        Console.Write("NOTFOUND\n");
                                    int indkama = ALL.IndexOf(",", indINT - 115);
                                    string temporary = ALL.Substring(indkama, indINT - indkama);
                                    ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                                    multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                                    num_mux--;
                                    num_s_ff--;
                                }////////end of the first if for "2 H"
                                if (XDL_mux_tmp[i].IndexOf("4 H") != -1 & XDL_mux_tmp[i].IndexOf("4 H]") == -1 & num_mux > 0)
                                {
                                    cache = XDL_mux_tmp[i].IndexOf("4 H");
                                    His_FF = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("4 H") + 4, 3);

                                    if (Int32.Parse(WOR) % 2 != 0) logic = 17;
                                    else logic = 21;

                                    lOG = logic.ToString();

                                    if (logic == 17)
                                    {
                                        set_Arguments("INB", "4 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                    }
                                    else if (logic == 21)
                                    {
                                        set_Arguments1("INB", "4 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                    }

                                    His_NUM++;
                                    int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                                    if (indINT == -1)
                                        Console.Write("NOTFOUND\n");
                                    int indkama = ALL.IndexOf(",", indINT - 115);
                                    string temporary = ALL.Substring(indkama, indINT - indkama);
                                    ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                                    multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                                    num_mux--;
                                    num_s_ff--;
                                }////////end of the first if for "4 H"
                                if (XDL_mux_tmp[i].IndexOf("6 H") != -1 & XDL_mux_tmp[i].IndexOf("6 H]") == -1 & num_mux > 0)
                                {
                                    cache = XDL_mux_tmp[i].IndexOf("6 H");
                                    His_FF = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("6 H") + 4, 3);

                                    if (Int32.Parse(WOR) % 2 != 0) logic = 18;
                                    else logic = 22;

                                    lOG = logic.ToString();

                                    if (logic == 18)
                                    {
                                        set_Arguments("INB", "6 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                    }
                                    else if (logic == 22)
                                    {
                                        set_Arguments1("INB", "6 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                    }

                                    His_NUM++;
                                    int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                                    if (indINT == -1)
                                        Console.Write("NOT_FOUND\n");
                                    int indkama = ALL.IndexOf(",", indINT - 115);
                                    string temporary = ALL.Substring(indkama, indINT - indkama);
                                    ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                                    multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                                    num_mux--;
                                    num_s_ff--;
                                }////////end of the first if for "6 H"
                                if (XDL_mux_tmp[i].IndexOf("8 H") != -1 & XDL_mux_tmp[i].IndexOf("8 H]") == -1 & num_mux > 0)
                                {
                                    cache = XDL_mux_tmp[i].IndexOf("8 H");
                                    His_FF = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("8 H") + 4, 3);

                                    if (Int32.Parse(WOR) % 2 != 0) logic = 19;
                                    else logic = 23;

                                    lOG = logic.ToString();

                                    if (logic == 19)
                                    {
                                        set_Arguments("INB", "8 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                    }
                                    else if (logic == 23)
                                    {
                                        set_Arguments1("INB", "8 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                    }

                                    His_NUM++;
                                    int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                                    if (indINT == -1)
                                        Console.Write("NOTFOUND\n");
                                    int indkama = ALL.IndexOf(",", indINT - 115);
                                    string temporary = ALL.Substring(indkama, indINT - indkama);
                                    ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                                    multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                                    num_mux--;
                                    num_s_ff--;
                                }////////end of the first if for "8 H"

                                if (num_s_ff > 0 & (XDL_mux_tmp[i + 1].IndexOf("*") != -1))
                                {
                                    inst_name = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("(") + 1, XDL_mux_tmp[i + 1].IndexOf(")") - XDL_mux_tmp[i + 1].IndexOf("(") - 1);
                                    if (XDL_mux_tmp[i + 1].IndexOf("1 H") != -1 & num_mux > 0 & XDL_mux_tmp[i].IndexOf("1 H]") == -1) 
                                    {
                                        cache = XDL_mux_tmp[i + 1].IndexOf("1 H");
                                        His_FF = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("1 H") + 4, 3);

                                        if (Int32.Parse(WOR) % 2 != 0) logic = 0;
                                        else logic = 4;

                                        lOG = logic.ToString();
                                        if (logic == 0)
                                        {
                                            set_Arguments1("INB", "1 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                        }
                                        else if (logic == 4)
                                        {
                                            set_Arguments("INB", "1 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                        }

                                        His_NUM++;
                                        int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                                        if (indINT == -1)
                                            Console.Write("NOTFOUND\n");
                                        int indkama = ALL.IndexOf(",", indINT - 115);
                                        string temporary = ALL.Substring(indkama, indINT - indkama);
                                        ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                                        multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                                        num_mux--;
                                        num_s_ff--;
                                    }
                                    if (XDL_mux_tmp[i + 1].IndexOf("3 H") != -1 & num_mux > 0 & XDL_mux_tmp[i].IndexOf("3 H]") == -1) 
                                    {
                                        cache = XDL_mux_tmp[i + 1].IndexOf("3 H");
                                        His_FF = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("3 H") + 4, 3);

                                        if (Int32.Parse(WOR) % 2 != 0) logic = 1;
                                        else logic = 5;

                                        lOG = logic.ToString();
                                        if (logic == 1)
                                        {
                                            set_Arguments1("INB", "3 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                        }
                                        else if (logic == 5)
                                        {
                                            set_Arguments("INB", "3 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                        }

                                        His_NUM++;
                                        int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                                        if (indINT == -1)
                                            Console.Write("NOTFOUND\n");
                                        int indkama = ALL.IndexOf(",", indINT - 115);
                                        string temporary = ALL.Substring(indkama, indINT - indkama);
                                        ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                                        multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                                        num_mux--;
                                        num_s_ff--;
                                    }
                                    if (XDL_mux_tmp[i + 1].IndexOf("5 H") != -1 & num_mux > 0 & XDL_mux_tmp[i].IndexOf("5 H]") == -1) 
                                    {
                                        cache = XDL_mux_tmp[i + 1].IndexOf("5 H");
                                        His_FF = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("5 H") + 4, 3);

                                        if (Int32.Parse(WOR) % 2 != 0) logic = 2;
                                        else logic = 6;

                                        lOG = logic.ToString();
                                        if (logic == 2)
                                        {
                                            set_Arguments1("INB", "5 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                        }
                                        else if (logic == 6)
                                        {
                                            set_Arguments("INB", "5 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                        }

                                        His_NUM++;
                                        int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                                        if (indINT == -1)
                                            Console.Write("NOTFOUND\n");
                                        int indkama = ALL.IndexOf(",", indINT - 115);
                                        string temporary = ALL.Substring(indkama, indINT - indkama);
                                        ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                                        multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                                        num_mux--;
                                        num_s_ff--;
                                    }
                                    if (XDL_mux_tmp[i + 1].IndexOf("7 H") != -1 & num_mux > 0 & XDL_mux_tmp[i].IndexOf("7 H]") == -1) 
                                    {
                                        cache = XDL_mux_tmp[i + 1].IndexOf("7 H");
                                        His_FF = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("7 H") + 4, 3);

                                        if (Int32.Parse(WOR) % 2 != 0) logic = 3;
                                        else logic = 7;

                                        lOG = logic.ToString();
                                        if (logic == 3)
                                        {
                                            set_Arguments1("INB", "7 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                        }
                                        else if (logic == 7)
                                        {
                                            set_Arguments("INB", "7 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                        }

                                        His_NUM++;
                                        int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                                        if (indINT == -1)
                                            Console.Write("NOTFOUND\n");
                                        int indkama = ALL.IndexOf(",", indINT - 115);
                                        string temporary = ALL.Substring(indkama, indINT - indkama);
                                        ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                                        multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                                        num_mux--;
                                        num_s_ff--;
                                    }
                                    if (XDL_mux_tmp[i + 1].IndexOf("2 H") != -1 & num_mux > 0 & XDL_mux_tmp[i].IndexOf("2 H]") == -1) 
                                    {
                                        cache = XDL_mux_tmp[i + 1].IndexOf("2 H");
                                        His_FF = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("2 H") + 4, 3);

                                        if (Int32.Parse(WOR) % 2 != 0) logic = 16;
                                        else logic = 20;

                                        lOG = logic.ToString();
                                        if (logic == 16)
                                        {
                                            set_Arguments1("INB", "2 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                        }
                                        else if (logic == 20)
                                        {
                                            set_Arguments("INB", "2 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                        }

                                        His_NUM++;
                                        int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                                        if (indINT == -1)
                                            Console.Write("NOTFOUND\n");
                                        int indkama = ALL.IndexOf(",", indINT - 115);
                                        string temporary = ALL.Substring(indkama, indINT - indkama);
                                        ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                                        multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                                        num_mux--;
                                        num_s_ff--;
                                    }
                                    if (XDL_mux_tmp[i + 1].IndexOf("4 H") != -1 & num_mux > 0 & XDL_mux_tmp[i].IndexOf("4 H]") == -1) 
                                    {
                                        cache = XDL_mux_tmp[i + 1].IndexOf("4 H");
                                        His_FF = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("4 H") + 4, 3);

                                        if (Int32.Parse(WOR) % 2 != 0) logic = 17;
                                        else logic = 21;

                                        lOG = logic.ToString();
                                        if (logic == 17)
                                        {
                                            set_Arguments1("INB", "4 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                        }
                                        else if (logic == 21)
                                        {
                                            set_Arguments("INB", "4 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                        }

                                        His_NUM++;
                                        int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                                        if (indINT == -1)
                                            Console.Write("NOTFOUND\n");
                                        int indkama = ALL.IndexOf(",", indINT - 115);
                                        string temporary = ALL.Substring(indkama, indINT - indkama);
                                        ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                                        multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                                        num_mux--;
                                        num_s_ff--;
                                    }
                                    if (XDL_mux_tmp[i + 1].IndexOf("6 H") != -1 & num_mux > 0 & XDL_mux_tmp[i].IndexOf("6 H]") == -1) 
                                    {
                                        cache = XDL_mux_tmp[i + 1].IndexOf("6 H");
                                        His_FF = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("6 H") + 4, 3);

                                        if (Int32.Parse(WOR) % 2 != 0) logic = 18;
                                        else logic = 22;

                                        lOG = logic.ToString();
                                        if (logic == 18)
                                        {
                                            set_Arguments1("INB", "6 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                        }
                                        else if (logic == 22)
                                        {
                                            set_Arguments("INB", "6 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                        }

                                        His_NUM++;
                                        int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                                        if (indINT == -1)
                                            Console.Write("NOTFOUND\n");
                                        int indkama = ALL.IndexOf(",", indINT - 115);
                                        string temporary = ALL.Substring(indkama, indINT - indkama);
                                        ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                                        multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                                        num_mux--;
                                        num_s_ff--;
                                    }
                                    if (XDL_mux_tmp[i + 1].IndexOf("8 H") != -1 & num_mux > 0 & XDL_mux_tmp[i].IndexOf("8 H]") == -1) 
                                    {
                                        cache = XDL_mux_tmp[i + 1].IndexOf("8 H");
                                        His_FF = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("8 H") + 4, 3);

                                        if (Int32.Parse(WOR) % 2 != 0) logic = 19;
                                        else logic = 23;

                                        lOG = logic.ToString();
                                        if (logic == 19)
                                        {
                                            set_Arguments1("INB", "8 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                        }
                                        else if (logic == 23)
                                        {
                                            set_Arguments("INB", "8 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                                        }

                                        His_NUM++;
                                        int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                                        if (indINT == -1)
                                            Console.Write("NOTFOUND\n");
                                        int indkama = ALL.IndexOf(",", indINT - 115);
                                        string temporary = ALL.Substring(indkama, indINT - indkama);
                                        ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                                        multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                                        num_mux--;
                                        num_s_ff--;
                                    }
                                }
                                string tiny = ""; string teeny = "";
                                tiny = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("NuFF") + 5, 1);
                                teeny = Brother.Substring(Brother.IndexOf("NuMUX") + 6, 1);
                                XDL_mux_tmp[i] = XDL_mux_tmp[i].Replace("NuFF<" + tiny, "NuFF<" + num_s_ff);
                                if (num_s_ff < Int16.Parse(tiny))
                                {
                                    Brother = Brother.Replace("NuMUX<" + teeny, "NuMUX<" + num_mux / 2);
                                    XDL_mux_tmp[IndBro] = XDL_mux_tmp[IndBro].Replace(bro_temp, Brother);
                              //      size[IndBro] = Brother.Length;
                                    if (IndBro == 0)
                                        Console.Write("LALA\n");
                                }
                                if (num_s_ff == 0)
                                    XDL_mux_tmp[i] = XDL_mux_tmp[i].Insert(XDL_mux_tmp[i].Length, " $");                                                           
                            }
                                ////////////////////////////////////////////////////////////Search in another CLBs
                            if(num_s_ff > 0)
                            {
                                /////call the search function
                                int ret = 0;
                                ret =  search_in_another_CLBs(ref LOC,ref IndBro,num_s_ff,WOR,ROW, COL, ref Brother,ref num_mux,ref XDL_mux_tmp,Number_of_lines + 50);
                                bro_temp = Brother;
                                inst_name = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("(") + 1, XDL_mux_tmp[i].IndexOf(")") - XDL_mux_tmp[i].IndexOf("(") - 1);
                                inst_name_H1 = Brother.Substring(Brother.IndexOf("(") + 1, Brother.IndexOf(")") - Brother.IndexOf("(") - 1);
                                if (Brother != "" & IndBro != -1)
                                {
                                    F11 = 0; F12 = 0; F21 = 0; F22 = 0; F31 = 0; F32 = 0; F41 = 0; F42 = 0;
                                    if (Brother.IndexOf("A6LUT") == -1 & Brother.IndexOf("A5LUT") == -1 & Brother.IndexOf("AOUTMUX") == -1) { F11 = 1; F12 = 1; }
                                    if (Brother.IndexOf("B6LUT") == -1 & Brother.IndexOf("B5LUT") == -1 & Brother.IndexOf("BOUTMUX") == -1) { F21 = 1; F22 = 1; }
                                    if (Brother.IndexOf("C6LUT") == -1 & Brother.IndexOf("C5LUT") == -1 & Brother.IndexOf("COUTMUX") == -1) { F31 = 1; F32 = 1; }
                                    if (Brother.IndexOf("D6LUT") == -1 & Brother.IndexOf("D5LUT") == -1 & Brother.IndexOf("DOUTMUX") == -1) { F41 = 1; F42 = 1; }
                                    int cache = 0;

                                    if (num_mux > 0)
                                    {
                                        MUX_finding(inst_name, inst_name_H1, LOC, ref ALL, ref XDL_mux_tmp, i, cache, ref Brother, ref His_FF, 
                                            ref logic, ref lOG, ref BYPB, ref Out_H_FF,ref H_logic, ref F11, ref F12, ref F21, ref F22,
                                            ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref His_NUM, 
                                            ref num_mux, ref num_s_ff, ROW, COL, WOR, TYP, Styp);
                                        
                                        string tiny = ""; string teeny = "";
                                        tiny = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("NuFF") + 5, 1);
                                        teeny = Brother.Substring(Brother.IndexOf("NuMUX") + 6, 1);
                                        XDL_mux_tmp[i] = XDL_mux_tmp[i].Replace("NuFF<" + tiny, "NuFF<" + num_s_ff);                                        
                                        if (num_s_ff < Int16.Parse(tiny))
                                        {
                                            Brother = Brother.Replace("NuMUX<" + teeny, "NuMUX<" + num_mux / 2);
                                            if (ret == 0) 
                                            {                                              
                                                XDL_mux_tmp[IndBro] = XDL_mux_tmp[IndBro].Replace(bro_temp, Brother);                                                
                                               // size[IndBro] = Brother.Length;
                                            }
                                            else if (ret == 1)
                                            {                                               
                                                XDL_mux_tmp[Number_of_lines + U] = Brother;
                                                U++;
                                            }
                                        }
                                        else if (num_s_ff == 0) XDL_mux_tmp[i] = XDL_mux_tmp[i].Insert(XDL_mux_tmp[i].Length, "$");                                                                               
                                    }
                                }                               
                            }                           
                        }
                        else if (Brother == "")
                        {
                            HIS_INST_NUM--;
                            Brother = Sbro;
                            Brother = Brother.Insert(Brother.Length," " +COL +" (REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<4} {NuFF<0} $");

                            inst_name = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("(") + 1, XDL_mux_tmp[i].IndexOf(")") - XDL_mux_tmp[i].IndexOf("(") - 1);
                            inst_name_H1 = Brother.Substring(Brother.IndexOf("(") + 1, Brother.IndexOf(")") - Brother.IndexOf("(") - 1);
                            ///////////////////////////////////////////////////START for using brother
                            ind_ff = Brother.IndexOf("FF<");
                            if (Brother.IndexOf("NuFF<0") != -1 & (Brother.IndexOf("$") == -1 || Brother.IndexOf("#") == -1))
                            {
                                num_ff = Int32.Parse(Brother.Substring(ind_ff + 3, 1));
                                num_mux = 2 * Int32.Parse(Brother.Substring(ind_ff - 6, 1));
                                F11 = 0; F12 = 0; F21 = 0; F22 = 0; F31 = 0; F32 = 0; F41 = 0; F42 = 0;
                                if (Brother.IndexOf("A6LUT") == -1 & Brother.IndexOf("A5LUT") == -1 & Brother.IndexOf("AOUTMUX") == -1) { F11 = 1; F12 = 1; }
                                if (Brother.IndexOf("B6LUT") == -1 & Brother.IndexOf("B5LUT") == -1 & Brother.IndexOf("BOUTMUX") == -1) { F21 = 1; F22 = 1; }
                                if (Brother.IndexOf("C6LUT") == -1 & Brother.IndexOf("C5LUT") == -1 & Brother.IndexOf("COUTMUX") == -1) { F31 = 1; F32 = 1; }
                                if (Brother.IndexOf("D6LUT") == -1 & Brother.IndexOf("D5LUT") == -1 & Brother.IndexOf("DOUTMUX") == -1) { F41 = 1; F42 = 1; }

                                ////////Instance from multiplser class to use its functions
                                multiplexer multiplexer1 = new multiplexer();

                                int cache = 0;
                                MUX_finding(inst_name,inst_name_H1,"INB",ref ALL, ref XDL_mux_tmp, i, cache, ref Brother, ref His_FF, ref logic, ref lOG, ref BYPB, ref Out_H_FF,
                                                ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B,
                                                ref Bh, ref Pin1, ref pin, ref His_NUM, ref num_mux, ref num_s_ff, ROW, COL, WOR, TYP, Styp);
                                string tiny = ""; string teeny = "";
                                tiny = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("NuFF") + 5, 1);
                                teeny = Brother.Substring(Brother.IndexOf("NuMUX") + 6, 1);
                                XDL_mux_tmp[i] = XDL_mux_tmp[i].Replace("NuFF<" + tiny, "NuFF<" + num_s_ff);
                                if (num_s_ff < Int16.Parse(tiny))
                                {
                                    Brother = Brother.Replace("NuMUX<" + teeny, "NuMUX<" + num_mux / 2);                                    
                                }
                                if (num_s_ff == 0)
                                {/////////The brother creating and because of that the $ is added to another one
                                    XDL_mux_tmp[Number_of_lines + U] = Brother;
                                    U++;
                                }
                            }
                        }                       
                    }
                }
            }
        }

        public void MUX_finding(string inst_name,string inst_name_H1,string LOC,ref string ALL,ref string[] XDL_mux_tmp, int i, int cache,ref string Brother,ref string His_FF, ref int logic, ref string lOG, ref string BYPB, ref string Out_H_FF, ref string H_logic, ref int F11, ref int F12, ref int F21, ref int F22, ref int F31, ref int F32, ref int F41, ref int F42, ref string B, ref string Bh, ref string Pin1, ref string pin, ref int His_NUM, ref int num_mux, ref int num_s_ff, string ROW, string COL,string WOR,string TYP, string Styp)
        {
            multiplexer multiplexer1 = new multiplexer();
            //AFF
            if ((XDL_mux_tmp[i].IndexOf("1 H") != -1 & XDL_mux_tmp[i].IndexOf("1 H]") == -1) ||
                (XDL_mux_tmp[i + 1].IndexOf("*") != -1 & XDL_mux_tmp[i + 1].IndexOf("1 H") != -1 & XDL_mux_tmp[i].IndexOf("1 H]") == -1) & num_mux > 0)
            {
                if (XDL_mux_tmp[i].IndexOf("1 H") != -1)
                {
                    cache = XDL_mux_tmp[i].IndexOf("1 H");
                    His_FF = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("1 H") + 4, 3);
                    inst_name = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("(") + 1, XDL_mux_tmp[i].IndexOf(")") - XDL_mux_tmp[i].IndexOf("(") - 1);
                }
                else if (XDL_mux_tmp[i + 1].IndexOf("*") != -1 & XDL_mux_tmp[i + 1].IndexOf("1 H") != -1)
                {
                    cache = XDL_mux_tmp[i + 1].IndexOf("1 H");
                    His_FF = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("1 H") + 4, 3);
                    inst_name = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("(") + 1, XDL_mux_tmp[i + 1].IndexOf(")") - XDL_mux_tmp[i + 1].IndexOf("(") - 1);
                }

                if (Int32.Parse(WOR) % 2 != 0) logic = 0;
                else logic = 4;

                lOG = logic.ToString();

                if (XDL_mux_tmp[i].IndexOf("1 H") != -1)
                {
                    if (logic == 0)
                    {
                        set_Arguments(LOC,"1 H",ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                    else if (logic == 4)
                    {
                       set_Arguments1(LOC,"1 H",ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                }
                else if (XDL_mux_tmp[i + 1].IndexOf("*") != -1 & XDL_mux_tmp[i + 1].IndexOf("1 H") != -1) 
                {
                    if (logic == 0)
                    {
                        set_Arguments1(LOC,"1 H",ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                    else if (logic == 4)
                    {
                        set_Arguments(LOC, "1 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                }

                His_NUM++;
                int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                if (indINT == -1)
                    Console.Write("NOTFOUND\n");
                int indkama = ALL.IndexOf(",", indINT - 115);
                string temporary = ALL.Substring(indkama, indINT - indkama);
                ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                
                num_mux--;
                num_s_ff--;
            }////////end of the first if for "1 H"
            if ((XDL_mux_tmp[i].IndexOf("3 H") != -1 & XDL_mux_tmp[i].IndexOf("3 H]") == -1) ||
                 (XDL_mux_tmp[i + 1].IndexOf("*") != -1 & XDL_mux_tmp[i + 1].IndexOf("3 H") != -1 & XDL_mux_tmp[i].IndexOf("3 H]") == -1) & num_mux > 0)
            {
                if (XDL_mux_tmp[i].IndexOf("3 H") != -1)
                {
                    cache = XDL_mux_tmp[i].IndexOf("3 H");
                    His_FF = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("3 H") + 4, 3);
                    inst_name = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("(") + 1, XDL_mux_tmp[i].IndexOf(")") - XDL_mux_tmp[i].IndexOf("(") - 1);
                }
                else if (XDL_mux_tmp[i + 1].IndexOf("*") != -1 & XDL_mux_tmp[i + 1].IndexOf("3 H") != -1)
                {
                    cache = XDL_mux_tmp[i + 1].IndexOf("3 H");
                    His_FF = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("3 H") + 4, 3);
                    inst_name = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("(") + 1, XDL_mux_tmp[i + 1].IndexOf(")") - XDL_mux_tmp[i + 1].IndexOf("(") - 1);
                }

                if (Int32.Parse(WOR) % 2 != 0) logic = 1;
                else logic = 5;

                lOG = logic.ToString();

                if (XDL_mux_tmp[i].IndexOf("3 H") != -1)
                {
                    if (logic == 1)
                    {
                        set_Arguments(LOC, "3 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                    else if (logic == 5)
                    {
                        set_Arguments1(LOC, "3 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                }
                else if (XDL_mux_tmp[i + 1].IndexOf("*") != -1 & XDL_mux_tmp[i + 1].IndexOf("3 H") != -1)
                {
                    if (logic == 1)
                    {
                        set_Arguments1(LOC, "3 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                    else if (logic == 5)
                    {
                        set_Arguments(LOC, "3 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                }

                His_NUM++;
                int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                if (indINT == -1)
                    Console.Write("NOTFOUND\n");
                int indkama = ALL.IndexOf(",", indINT - 115);
                string temporary = ALL.Substring(indkama, indINT - indkama);
                ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                num_mux--;
                num_s_ff--;
            }////////end of the first if for "3 H"
            if ((XDL_mux_tmp[i].IndexOf("5 H") != -1 & XDL_mux_tmp[i].IndexOf("5 H]") == -1) ||
                (XDL_mux_tmp[i + 1].IndexOf("*") != -1 & XDL_mux_tmp[i + 1].IndexOf("5 H") != -1 & XDL_mux_tmp[i].IndexOf("5 H]") == -1) & num_mux > 0)
            {
                if (XDL_mux_tmp[i].IndexOf("5 H") != -1)
                {
                    cache = XDL_mux_tmp[i].IndexOf("5 H");
                    His_FF = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("5 H") + 4, 3);
                    inst_name = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("(") + 1, XDL_mux_tmp[i].IndexOf(")") - XDL_mux_tmp[i].IndexOf("(") - 1);
                }
                else if (XDL_mux_tmp[i + 1].IndexOf("*") != -1 & XDL_mux_tmp[i + 1].IndexOf("5 H") != -1)
                {
                    cache = XDL_mux_tmp[i + 1].IndexOf("5 H");
                    His_FF = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("5 H") + 4, 3);
                    inst_name = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("(") + 1, XDL_mux_tmp[i + 1].IndexOf(")") - XDL_mux_tmp[i + 1].IndexOf("(") - 1);
                }

                if (Int32.Parse(WOR) % 2 != 0) logic = 2;
                else logic = 6;

                lOG = logic.ToString();

                if (XDL_mux_tmp[i].IndexOf("5 H") != -1)
                {
                    if (logic == 2)
                    {
                        set_Arguments(LOC, "5 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                    else if (logic == 6)
                    {
                        set_Arguments1(LOC, "5 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                }
                else if (XDL_mux_tmp[i + 1].IndexOf("*") != -1 & XDL_mux_tmp[i + 1].IndexOf("5 H") != -1)
                {
                    if (logic == 2)
                    {
                        set_Arguments1(LOC, "5 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                    else if (logic == 6)
                    {
                        set_Arguments(LOC, "5 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                }

                His_NUM++;
                int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                if (indINT == -1)
                    Console.Write("NOTFOUND\n");
                int indkama = ALL.IndexOf(",", indINT - 115);
                string temporary = ALL.Substring(indkama, indINT - indkama);
                ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                num_mux--;
                num_s_ff--;
            }////////end of the first if for "5 H"
            if ((XDL_mux_tmp[i].IndexOf("7 H") != -1 & XDL_mux_tmp[i].IndexOf("7 H]") == -1) ||
                (XDL_mux_tmp[i + 1].IndexOf("*") != -1 & XDL_mux_tmp[i + 1].IndexOf("7 H") != -1 & XDL_mux_tmp[i].IndexOf("7 H]") == -1) & num_mux > 0)
            {
                if (XDL_mux_tmp[i].IndexOf("7 H") != -1)
                {
                    cache = XDL_mux_tmp[i].IndexOf("7 H");
                    His_FF = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("7 H") + 4, 3);
                    inst_name = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("(") + 1, XDL_mux_tmp[i].IndexOf(")") - XDL_mux_tmp[i].IndexOf("(") - 1);
                }
                else if (XDL_mux_tmp[i + 1].IndexOf("*") != -1 & XDL_mux_tmp[i + 1].IndexOf("7 H") != -1)
                {
                    cache = XDL_mux_tmp[i + 1].IndexOf("7 H");
                    His_FF = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("7 H") + 4, 3);
                    inst_name = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("(") + 1, XDL_mux_tmp[i + 1].IndexOf(")") - XDL_mux_tmp[i + 1].IndexOf("(") - 1);
                }

                if (Int32.Parse(WOR) % 2 != 0) logic = 3;
                else logic = 7;

                lOG = logic.ToString();

                if (XDL_mux_tmp[i].IndexOf("7 H") != -1)
                {
                    if (logic == 3)
                    {
                        set_Arguments(LOC, "7 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                    else if (logic == 7)
                    {
                        set_Arguments1(LOC, "7 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                }
                else if (XDL_mux_tmp[i + 1].IndexOf("*") != -1 & XDL_mux_tmp[i + 1].IndexOf("7 H") != -1) 
                {
                    if (logic == 3)
                    {
                        set_Arguments1(LOC, "7 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                    else if (logic == 7)
                    {
                        set_Arguments(LOC, "7 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                }

                His_NUM++;
                int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                if (indINT == -1)
                    Console.Write("NOTFOUND\n");
                int indkama = ALL.IndexOf(",", indINT - 115);
                string temporary = ALL.Substring(indkama, indINT - indkama);
                ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                num_mux--;
                num_s_ff--;
            }////////end of the first if for "7 H"
            if ((XDL_mux_tmp[i].IndexOf("2 H") != -1 & XDL_mux_tmp[i].IndexOf("2 H]") == -1) ||
                (XDL_mux_tmp[i + 1].IndexOf("*") != -1 & XDL_mux_tmp[i + 1].IndexOf("2 H") != -1 & XDL_mux_tmp[i].IndexOf("2 H]") == -1) & num_mux > 0)
            {
                if (XDL_mux_tmp[i].IndexOf("2 H") != -1)
                {
                    cache = XDL_mux_tmp[i].IndexOf("2 H");
                    His_FF = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("2 H") + 4, 3);
                    inst_name = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("(") + 1, XDL_mux_tmp[i].IndexOf(")") - XDL_mux_tmp[i].IndexOf("(") - 1);
                }
                else if (XDL_mux_tmp[i + 1].IndexOf("*") != -1 & XDL_mux_tmp[i + 1].IndexOf("2 H") != -1)
                {
                    cache = XDL_mux_tmp[i + 1].IndexOf("2 H");
                    His_FF = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("2 H") + 4, 3);
                    inst_name = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("(") + 1, XDL_mux_tmp[i + 1].IndexOf(")") - XDL_mux_tmp[i + 1].IndexOf("(") - 1);
                }

                if (Int32.Parse(WOR) % 2 != 0) logic = 16;
                else logic = 20;

                lOG = logic.ToString();

                if (XDL_mux_tmp[i].IndexOf("2 H") != -1)
                {
                    if (logic == 16)
                    {
                        set_Arguments(LOC, "2 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                    else if (logic == 20)
                    {
                        set_Arguments1(LOC, "2 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                }
                else if(XDL_mux_tmp[i + 1].IndexOf("*") != -1 & XDL_mux_tmp[i + 1].IndexOf("2 H") != -1)
                {
                    if (logic == 16)
                    {
                        set_Arguments1(LOC, "2 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                    else if (logic == 20)
                    {
                        set_Arguments(LOC, "2 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                }

                His_NUM++;
                int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                if (indINT == -1)
                    Console.Write("NOTFOUND\n");
                int indkama = ALL.IndexOf(",", indINT - 115);
                string temporary = ALL.Substring(indkama, indINT - indkama);
                ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                num_mux--;
                num_s_ff--;
            }////////end of the first if for "2 H"
            if ((XDL_mux_tmp[i].IndexOf("4 H") != -1 & XDL_mux_tmp[i].IndexOf("4 H]") == -1) ||
                (XDL_mux_tmp[i + 1].IndexOf("*") != -1 & XDL_mux_tmp[i + 1].IndexOf("4 H") != -1 & XDL_mux_tmp[i].IndexOf("4 H]") == -1) & num_mux > 0)
            {
                if (XDL_mux_tmp[i].IndexOf("4 H") != -1)
                {
                    cache = XDL_mux_tmp[i].IndexOf("4 H");
                    His_FF = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("4 H") + 4, 3);
                    inst_name = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("(") + 1, XDL_mux_tmp[i].IndexOf(")") - XDL_mux_tmp[i].IndexOf("(") - 1);
                }
                else if (XDL_mux_tmp[i + 1].IndexOf("*") != -1 & XDL_mux_tmp[i + 1].IndexOf("4 H") != -1)
                {
                    cache = XDL_mux_tmp[i + 1].IndexOf("4 H");
                    His_FF = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("4 H") + 4, 3);
                    inst_name = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("(") + 1, XDL_mux_tmp[i + 1].IndexOf(")") - XDL_mux_tmp[i + 1].IndexOf("(") - 1);
                }

                if (Int32.Parse(WOR) % 2 != 0) logic = 17;
                else logic = 21;

                lOG = logic.ToString();

                if (XDL_mux_tmp[i].IndexOf("4 H") != -1)
                {
                    if (logic == 17)
                    {
                        set_Arguments(LOC, "4 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                    else if (logic == 21)
                    {
                        set_Arguments1(LOC, "4 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                }
                else if (XDL_mux_tmp[i + 1].IndexOf("*") != -1 & XDL_mux_tmp[i + 1].IndexOf("4 H") != -1)
                {
                    if (logic == 17)
                    {
                        set_Arguments1(LOC, "4 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                    else if (logic == 21)
                    {
                        set_Arguments(LOC, "4 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                }

                His_NUM++;
                int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                if (indINT == -1)
                    Console.Write("NOTFOUND\n");
                int indkama = ALL.IndexOf(",", indINT - 115);
                string temporary = ALL.Substring(indkama, indINT - indkama);
                ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                num_mux--;
                num_s_ff--;
            }////////end of the first if for "4 H"
            if ((XDL_mux_tmp[i].IndexOf("6 H") != -1 & XDL_mux_tmp[i].IndexOf("6 H]") == -1) ||
                (XDL_mux_tmp[i + 1].IndexOf("*") != -1 & XDL_mux_tmp[i + 1].IndexOf("6 H") != -1 & XDL_mux_tmp[i].IndexOf("6 H]") == -1) & num_mux > 0)
            {
                if (XDL_mux_tmp[i].IndexOf("6 H") != -1)
                {
                    cache = XDL_mux_tmp[i].IndexOf("6 H");
                    His_FF = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("6 H") + 4, 3);
                    inst_name = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("(") + 1, XDL_mux_tmp[i].IndexOf(")") - XDL_mux_tmp[i].IndexOf("(") - 1);
                }
                else if (XDL_mux_tmp[i + 1].IndexOf("*") != -1 & XDL_mux_tmp[i + 1].IndexOf("6 H") != -1)
                {
                    cache = XDL_mux_tmp[i + 1].IndexOf("6 H");
                    His_FF = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("6 H") + 4, 3);
                    inst_name = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("(") + 1, XDL_mux_tmp[i + 1].IndexOf(")") - XDL_mux_tmp[i + 1].IndexOf("(") - 1);
                }

                if (Int32.Parse(WOR) % 2 != 0) logic = 18;
                else logic = 22;

                lOG = logic.ToString();

                if ( XDL_mux_tmp[i].IndexOf("6 H") != -1)
                {
                    if (logic == 18)
                    {
                        set_Arguments(LOC, "6 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                    else if (logic == 22)
                    {
                        set_Arguments1(LOC, "6 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                }
                else if(XDL_mux_tmp[i + 1].IndexOf("*") != -1 & XDL_mux_tmp[i + 1].IndexOf("6 H") != -1)
                {
                    if (logic == 18)
                    {
                        set_Arguments1(LOC, "6 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                    else if (logic == 22)
                    {
                        set_Arguments(LOC, "6 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                }

                His_NUM++;
                int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                if (indINT == -1)
                    Console.Write("NOTFOUND\n");
                int indkama = ALL.IndexOf(",", indINT - 115);
                string temporary = ALL.Substring(indkama, indINT - indkama);
                ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                num_mux--;
                num_s_ff--;
            }////////end of the first if for "6 H"
            if ((XDL_mux_tmp[i].IndexOf("8 H") != -1 & XDL_mux_tmp[i].IndexOf("8 H]") == -1) ||
                (XDL_mux_tmp[i + 1].IndexOf("*") != -1 & XDL_mux_tmp[i + 1].IndexOf("8 H") != -1 & XDL_mux_tmp[i].IndexOf("8 H]") == -1) & num_mux > 0)
            {
                if ( XDL_mux_tmp[i].IndexOf("8 H") != -1)
                {
                    cache = XDL_mux_tmp[i].IndexOf("8 H");
                    His_FF = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("8 H") + 4, 3);
                    inst_name = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("(") + 1, XDL_mux_tmp[i].IndexOf(")") - XDL_mux_tmp[i].IndexOf("(") - 1);
                }
                else if(XDL_mux_tmp[i + 1].IndexOf("*") != -1 & XDL_mux_tmp[i + 1].IndexOf("8 H") != -1)
                {
                    cache = XDL_mux_tmp[i + 1].IndexOf("8 H");
                    His_FF = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("8 H") + 4, 3);
                    inst_name = XDL_mux_tmp[i + 1].Substring(XDL_mux_tmp[i + 1].IndexOf("(") + 1, XDL_mux_tmp[i + 1].IndexOf(")") - XDL_mux_tmp[i + 1].IndexOf("(") - 1);
                }
                if (Int32.Parse(WOR) % 2 != 0) logic = 19;
                else logic = 23;

                lOG = logic.ToString();

                if ( XDL_mux_tmp[i].IndexOf("8 H") != -1)
                {
                    if (logic == 19)
                    {
                        set_Arguments(LOC, "8 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                    else if (logic == 23)
                    {
                        set_Arguments1(LOC, "8 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                }
                else if(XDL_mux_tmp[i + 1].IndexOf("*") != -1 & XDL_mux_tmp[i + 1].IndexOf("8 H") != -1)
                {
                    if (logic == 19)
                    {
                        set_Arguments1(LOC, "8 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                    else if (logic == 23)
                    {
                        set_Arguments(LOC, "8 H", ref Brother, ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref XDL_mux_tmp[i]);
                    }
                }

                His_NUM++;
                int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                if (indINT == -1)
                    Console.Write("NOTFOUND\n");
                int indkama = ALL.IndexOf(",", indINT - 115);
                string temporary = ALL.Substring(indkama, indINT - indkama);
                ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                multiplexer1.Routing_function(inst_name, inst_name_H1, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                num_mux--;
                num_s_ff--;
            }////////end of the first if for "8 H"

        }

        public int search_in_another_CLBs(ref string LOC,ref int IndBro, int require, string WOR, string ROW, string COL, ref string Brother, ref int num_mux, ref string[] XDL_mux_tmp, long Number_of_lines)
        {
            string Wst = ""; string Nrth = ""; string Est = ""; string Suth = "";
            string NrWs = ""; string NrEs = ""; string StWs = ""; string StEs = "";
            string Wst2 = ""; string Nrth2 = ""; string Est2 = ""; string Suth2 = "";
            string Wst4 = ""; string Nrth4 = ""; string Est4 = ""; string Suth4 = "";
            string WstT = ""; string NrthT = ""; string EstT = ""; string SuthT = "";
            string NrWsT = ""; string NrEsT = ""; string StWsT = ""; string StEsT = "";
            string Wst2T = ""; string Nrth2T = ""; string Est2T = ""; string Suth2T = "";
            string Wst4T = ""; string Nrth4T = ""; string Est4T = ""; string Suth4T = "";

            Wst = (Int16.Parse(WOR) - 1) + " " + COL;
            WstT = (Int16.Parse(WOR) - 2) + " " + COL;
            Nrth = WOR + " " + (Int16.Parse(COL) + 1);
            Est = (Int16.Parse(WOR) + 1) + " " + COL;
            EstT = (Int16.Parse(WOR) + 2) + " " + COL;
            Suth = WOR + " " + (Int16.Parse(COL) - 1);
            NrWs = (Int16.Parse(WOR) - 1) + " " + (Int16.Parse(COL) + 1);
            NrWsT = (Int16.Parse(WOR) - 2) + " " + (Int16.Parse(COL) + 1);
            NrEs = (Int16.Parse(WOR) + 1) + " " + (Int16.Parse(COL) + 1);
            NrEsT = (Int16.Parse(WOR) + 2) + " " + (Int16.Parse(COL) + 1);
            StWs = (Int16.Parse(WOR) - 1) + " " + (Int16.Parse(COL) - 1);
            StWsT = (Int16.Parse(WOR) - 2) + " " + (Int16.Parse(COL) - 1);
            StEs = (Int16.Parse(WOR) + 1) + " " + (Int16.Parse(COL) - 1);
            StEsT = (Int16.Parse(WOR) + 2) + " " + (Int16.Parse(COL) - 1);
            Wst2 = (Int16.Parse(WOR) - 3) + " " + COL;
            Wst2T = (Int16.Parse(WOR) - 4) + " " + COL;
            Nrth2 = WOR + " " + (Int16.Parse(COL) + 2);
            Est2 = (Int16.Parse(WOR) + 3) + " " + COL;
            Est2T = (Int16.Parse(WOR) + 4) + " " + COL;
            Suth2 = WOR + " " + (Int16.Parse(COL) - 2);
            Wst4 = (Int16.Parse(WOR) - 7) + " " + COL;
            Wst4T = (Int16.Parse(WOR) - 8) + " " + COL;
            Nrth4 = WOR + " " + (Int16.Parse(COL) + 4);
            Est4 = (Int16.Parse(WOR) + 7) + " " + COL;
            Est4T = (Int16.Parse(WOR) + 8) + " " + COL;
            Suth4 = WOR + " " + (Int16.Parse(COL) - 4);
            if (Int16.Parse(WOR) % 2 == 0 )
            {
                NrthT = (Int16.Parse(WOR) - 1) + " " + (Int16.Parse(COL) + 1);
                SuthT = (Int16.Parse(WOR) - 1) + " " + (Int16.Parse(COL) - 1);
                Nrth2T = (Int16.Parse(WOR) - 1) + " " + (Int16.Parse(COL) + 2);
                Suth2T = (Int16.Parse(WOR) - 1) + " " + (Int16.Parse(COL) - 2);
                Nrth4T = (Int16.Parse(WOR) - 1) + " " + (Int16.Parse(COL) + 4);
                Suth4T = (Int16.Parse(WOR) - 1) + " " + (Int16.Parse(COL) - 4);
            }
            else
            {
                NrthT = (Int16.Parse(WOR) + 1) + " " + (Int16.Parse(COL) + 1);
                SuthT = (Int16.Parse(WOR) + 1) + " " + (Int16.Parse(COL) - 1);
                Nrth2T = (Int16.Parse(WOR) + 1) + " " + (Int16.Parse(COL) + 2);
                Suth2T = (Int16.Parse(WOR) + 1) + " " + (Int16.Parse(COL) - 2);
                Nrth4T = (Int16.Parse(WOR) + 1) + " " + (Int16.Parse(COL) + 4);
                Suth4T = (Int16.Parse(WOR) + 1) + " " + (Int16.Parse(COL) - 4);
            }

            int num_mux_Wst = 0; int num_mux_Est = 0; int num_mux_Nrth = 0; int num_mux_Suth = 0;
            int num_mux_NrWs = 0; int num_mux_NrEs = 0; int num_mux_StWs = 0; int num_mux_StEs = 0;
            int num_mux_Wst2 = 0; int num_mux_Est2 = 0; int num_mux_Nrth2 = 0; int num_mux_Suth2 = 0;
            int num_mux_Wst4 = 0; int num_mux_Est4 = 0; int num_mux_Nrth4 = 0; int num_mux_Suth4 = 0;
            string brother_wst = ""; string brother_est = ""; string brother_nrth = ""; string brother_suth = "";
            string brother_wstT = ""; string brother_estT = ""; string brother_nrthT = ""; string brother_suthT = "";
            string brother_NrWs = ""; string brother_NrEs = ""; string brother_StWs = ""; string brother_StEs = "";
            string brother_NrWsT = ""; string brother_NrEsT = ""; string brother_StWsT = ""; string brother_StEsT = "";
            string brother_wst2 = ""; string brother_est2 = ""; string brother_nrth2 = ""; string brother_suth2 = "";
            string brother_wst2T = ""; string brother_est2T = ""; string brother_Nrth2T = ""; string brother_Suth2T = "";
            string brother_wst4 = ""; string brother_est4 = ""; string brother_nrth4 = ""; string brother_suth4 = "";
            string brother_wst4T = ""; string brother_est4T = ""; string brother_Nrth4T = ""; string brother_Suth4T = "";
            int IndBro_wst = 0; int IndBro_est = 0; int IndBro_nrth = 0; int IndBro_suth = 0;
            int IndBro_nrws = 0; int IndBro_nres = 0; int IndBro_stws = 0; int IndBro_stes = 0;
            int IndBro_wst2 = 0; int IndBro_est2 = 0; int IndBro_nrth2 = 0; int IndBro_suth2 = 0;
            int IndBro_wst4 = 0; int IndBro_est4 = 0; int IndBro_nrth4 = 0; int IndBro_suth4 = 0;
            int IndBro_wstT = 0; int IndBro_estT = 0; int IndBro_nrthT = 0; int IndBro_suthT = 0;
            int IndBro_nrwsT = 0; int IndBro_nresT = 0; int IndBro_stwsT = 0; int IndBro_stesT = 0;
            int IndBro_wst2T = 0; int IndBro_est2T = 0; int IndBro_nrth2T = 0; int IndBro_suth2T = 0;
            int IndBro_wst4T = 0; int IndBro_est4T = 0; int IndBro_nrth4T = 0; int IndBro_suth4T = 0;
            int ind_ff = 0;
            int num_mux_WstT = 0; int num_mux_EstT = 0; int num_mux_NrthT = 0; int num_mux_SuthT = 0;
            int num_mux_NrWsT = 0; int num_mux_NrEsT = 0; int num_mux_StWsT = 0; int num_mux_StEsT = 0;
            int num_mux_Wst2T = 0; int num_mux_Est2T = 0; int num_mux_Nrth2T = 0; int num_mux_Suth2T = 0;
            int num_mux_Wst4T = 0; int num_mux_Est4T = 0; int num_mux_Nrth4T = 0; int num_mux_Suth4T = 0;
            /////////////////search for CLBs
            ///west
            IndBro_wst = search_ind(0, Wst, ref XDL_mux_tmp, Number_of_lines);
            if (IndBro_wst != -1)
            {
                IndBro = IndBro_wst;
                brother_wst = search(0, Wst, ref XDL_mux_tmp, Number_of_lines);
                ind_ff = brother_wst.IndexOf("FF<");
                if (brother_wst.IndexOf("NuFF<0") != -1 & brother_wst.IndexOf("NuMUX<0") == -1)
                    num_mux_Wst = 2 * Int32.Parse(brother_wst.Substring(ind_ff - 6, 1));
                Brother = brother_wst;                
            }
                IndBro_wstT = search_ind(0, WstT, ref XDL_mux_tmp, Number_of_lines);
                if (IndBro_wstT != -1)
                {
                    brother_wstT = search(0, WstT, ref XDL_mux_tmp, Number_of_lines);
                    ind_ff = brother_wstT.IndexOf("FF<");
                    if (brother_wstT.IndexOf("NuFF<0") != -1 & brother_wstT.IndexOf("NuMUX<0") == -1)
                        num_mux_WstT = 2 * Int32.Parse(brother_wstT.Substring(ind_ff - 6, 1));                                          
                }
                if (num_mux_WstT > num_mux_Wst) { num_mux = num_mux_WstT; Brother = brother_wstT; IndBro = IndBro_wstT; LOC = "WSTP"; }
                else { num_mux = num_mux_Wst; Brother = brother_wst; IndBro = IndBro_wst; LOC = "WST"; }
            //////Est
            IndBro_est = search_ind(0, Est, ref XDL_mux_tmp, Number_of_lines);
            if (IndBro_est != -1)
            {               
                brother_est = search(0, Est, ref XDL_mux_tmp, Number_of_lines);
                ind_ff = brother_est.IndexOf("FF<");
                if (brother_est.IndexOf("NuFF<0") != -1 & brother_est.IndexOf("NuMUX<0") == -1)
                    num_mux_Est = 2 * Int32.Parse(brother_est.Substring(ind_ff - 6, 1));
            }
            if (num_mux_Est > num_mux) { num_mux = num_mux_Est; Brother = brother_est; IndBro = IndBro_est; LOC = "EST"; }
                IndBro_estT = search_ind(0, EstT, ref XDL_mux_tmp, Number_of_lines);
                if (IndBro_estT != -1)
                {
                    brother_estT = search(0, EstT, ref XDL_mux_tmp, Number_of_lines);
                    ind_ff = brother_estT.IndexOf("FF<");
                    if (brother_estT.IndexOf("NuFF<0") != -1 & brother_estT.IndexOf("NuMUX<0") == -1)
                    {
                         num_mux_EstT = 2 * Int32.Parse(brother_estT.Substring(ind_ff - 6, 1));                        
                    }
                }
                if (num_mux_EstT > num_mux) { num_mux = num_mux_EstT; Brother = brother_estT; IndBro = IndBro_estT; LOC = "ESTP"; }
            /////Nrth
            IndBro_nrth = search_ind(0, Nrth, ref XDL_mux_tmp, Number_of_lines);
            if (IndBro_nrth != -1)
            {               
                brother_nrth = search(0, Nrth, ref XDL_mux_tmp, Number_of_lines);
                ind_ff = brother_nrth.IndexOf("FF<");
                if (brother_nrth.IndexOf("NuFF<0") != -1 & brother_nrth.IndexOf("NuMUX<0") == -1)
                    num_mux_Nrth = 2 * Int32.Parse(brother_nrth.Substring(ind_ff - 6, 1));
            }
            if (num_mux_Nrth > num_mux) { num_mux = num_mux_Nrth; Brother = brother_nrth; IndBro = IndBro_nrth; LOC = "NRT"; }
                IndBro_nrthT = search_ind(0, NrthT, ref XDL_mux_tmp, Number_of_lines);
                if (IndBro_nrthT != -1)
                {                   
                    brother_nrthT = search(0, NrthT, ref XDL_mux_tmp, Number_of_lines);
                    ind_ff = brother_nrthT.IndexOf("FF<");
                    if (brother_nrthT.IndexOf("NuFF<0") != -1 & brother_nrthT.IndexOf("NuMUX<0") == -1)
                         num_mux_NrthT = 2 * Int32.Parse(brother_nrthT.Substring(ind_ff - 6, 1));
                }
                if (num_mux_NrthT > num_mux) { num_mux = num_mux_NrthT; Brother = brother_nrthT; IndBro = IndBro_nrthT; LOC = "NRTP"; }            
            //////Suth
            IndBro_suth = search_ind(0, Suth, ref XDL_mux_tmp, Number_of_lines);
            if (IndBro_suth != -1)
            {                
                brother_suth = search(0, Suth, ref XDL_mux_tmp, Number_of_lines);
                ind_ff = brother_suth.IndexOf("FF<");
                if (brother_suth.IndexOf("NuFF<0") != -1 & brother_suth.IndexOf("NuMUX<0") == -1)
                    num_mux_Suth = 2 * Int32.Parse(brother_suth.Substring(ind_ff - 6, 1));
            }
            if (num_mux_Suth > num_mux) { num_mux = num_mux_Suth; Brother = brother_suth; IndBro = IndBro_suth; LOC = "SUT"; }
                IndBro_suthT = search_ind(0, SuthT, ref XDL_mux_tmp, Number_of_lines);
                if (IndBro_suthT != -1)
                {
                    brother_suthT = search(0, SuthT, ref XDL_mux_tmp, Number_of_lines);
                    ind_ff = brother_suthT.IndexOf("FF<");
                    if (brother_suthT.IndexOf("NuFF<0") != -1 & brother_suthT.IndexOf("NuMUX<0") == -1)
                        num_mux_SuthT = 2 * Int32.Parse(brother_suthT.Substring(ind_ff - 6, 1));
                }
                if (num_mux_SuthT > num_mux) { num_mux = num_mux_SuthT; Brother = brother_suthT; IndBro = IndBro_suthT; LOC = "SUTP"; }
            //////NrWs
            IndBro_nrws = search_ind(0, NrWs, ref XDL_mux_tmp, Number_of_lines);
            if (IndBro_nrws != -1)
            {               
                brother_NrWs = search(0, NrWs, ref XDL_mux_tmp, Number_of_lines);
                ind_ff = brother_NrWs.IndexOf("FF<");
                if (brother_NrWs.IndexOf("NuFF<0") != -1 & brother_NrWs.IndexOf("NuMUX<0") == -1)
                    num_mux_NrWs = 2 * Int32.Parse(brother_NrWs.Substring(ind_ff - 6, 1));
            }
            if (num_mux_NrWs > num_mux) { num_mux = num_mux_NrWs; Brother = brother_NrWs; IndBro = IndBro_nrws; LOC = "NWT"; }
                IndBro_nrwsT = search_ind(0, NrWsT, ref XDL_mux_tmp, Number_of_lines);
                if (IndBro_nrwsT != -1)
                {
                    brother_NrWsT = search(0, NrWsT, ref XDL_mux_tmp, Number_of_lines);
                    ind_ff = brother_NrWsT.IndexOf("FF<");
                    if (brother_NrWsT.IndexOf("NuFF<0") != -1 & brother_NrWsT.IndexOf("NuMUX<0") == -1)
                        num_mux_NrWsT = 2 * Int32.Parse(brother_NrWsT.Substring(ind_ff - 6, 1));
                }
                if (num_mux_NrWsT > num_mux) { num_mux = num_mux_NrWsT; Brother = brother_NrWsT; IndBro = IndBro_nrwsT; LOC = "NWTP"; }
            /////NrEs
            IndBro_nres = search_ind(0, NrEs, ref XDL_mux_tmp, Number_of_lines);
            if (IndBro_nres != -1)
            {               
                brother_NrEs = search(0, NrEs, ref XDL_mux_tmp, Number_of_lines);
                ind_ff = brother_NrEs.IndexOf("FF<");
                if (brother_NrEs.IndexOf("NuFF<0") != -1 & brother_NrEs.IndexOf("NuMUX<0") == -1)
                    num_mux_NrEs = 2 * Int32.Parse(brother_NrEs.Substring(ind_ff - 6, 1));
            }
            if (num_mux_NrEs > num_mux) { num_mux = num_mux_NrEs; Brother = brother_NrEs; IndBro = IndBro_nres; LOC = "NET"; }
                IndBro_nresT = search_ind(0, NrEsT, ref XDL_mux_tmp, Number_of_lines);
                if (IndBro_nresT != -1)
                {
                    brother_NrEsT = search(0, NrEsT, ref XDL_mux_tmp, Number_of_lines);
                    ind_ff = brother_NrEsT.IndexOf("FF<");
                    if (brother_NrEsT.IndexOf("NuFF<0") != -1 & brother_NrEsT.IndexOf("NuMUX<0") == -1)
                         num_mux_NrEsT = 2 * Int32.Parse(brother_NrEsT.Substring(ind_ff - 6, 1));
                }
                if (num_mux_NrEsT > num_mux) { num_mux = num_mux_NrEsT; Brother = brother_NrEsT; IndBro = IndBro_nresT; LOC = "NETP"; }
            //////StWs
            IndBro_stws = search_ind(0, StWs, ref XDL_mux_tmp, Number_of_lines);
            if (IndBro_stws != -1)
            {
                brother_StWs = search(0, StWs, ref XDL_mux_tmp, Number_of_lines);
                ind_ff = brother_StWs.IndexOf("FF<");
                if (brother_StWs.IndexOf("NuFF<0") != -1 & brother_StWs.IndexOf("NuMUX<0") == -1)
                    num_mux_StWs = 2 * Int32.Parse(brother_StWs.Substring(ind_ff - 6, 1));
            }
            if (num_mux_StWs > num_mux) { num_mux = num_mux_StWs; Brother = brother_StWs; IndBro = IndBro_stws; LOC = "SWT"; }
                IndBro_stwsT = search_ind(0, StWsT, ref XDL_mux_tmp, Number_of_lines);
                if (IndBro_stwsT != -1)
                {
                    brother_StWsT = search(0, StWsT, ref XDL_mux_tmp, Number_of_lines);
                    ind_ff = brother_StWsT.IndexOf("FF<");
                    if (brother_StWsT.IndexOf("NuFF<0") != -1 & brother_StWsT.IndexOf("NuMUX<0") == -1)
                        num_mux_StWsT = 2 * Int32.Parse(brother_StWsT.Substring(ind_ff - 6, 1));
                }
                if (num_mux_StWsT > num_mux) { num_mux = num_mux_StWsT; Brother = brother_StWsT; IndBro = IndBro_stwsT; LOC = "SWTP"; }
            ///////StEs
            IndBro_stes = search_ind(0, StEs, ref XDL_mux_tmp, Number_of_lines);
            if (IndBro_stes != -1)
            {  
                brother_StEs = search(0, StEs, ref XDL_mux_tmp, Number_of_lines);
                ind_ff = brother_StEs.IndexOf("FF<");
                if (brother_StEs.IndexOf("NuFF<0") != -1 & brother_StEs.IndexOf("NuMUX<0") == -1)
                    num_mux_StEs = 2 * Int32.Parse(brother_StEs.Substring(ind_ff - 6, 1));
            }
            if (num_mux_StEs > num_mux) {num_mux =num_mux_StEs; Brother = brother_StEs; IndBro = IndBro_stes; LOC = "SET"; }
                IndBro_stesT = search_ind(0, StEsT, ref XDL_mux_tmp, Number_of_lines);
                if (IndBro_stesT != -1)
                {
                    brother_StEsT = search(0, StEsT, ref XDL_mux_tmp, Number_of_lines);
                    ind_ff = brother_StEsT.IndexOf("FF<");
                    if (brother_StEsT.IndexOf("NuFF<0") != -1 & brother_StEsT.IndexOf("NuMUX<0") == -1)
                       num_mux_StEsT = 2 * Int32.Parse(brother_StEsT.Substring(ind_ff - 6, 1));
                }
            if (num_mux_StEsT > num_mux) {num_mux =num_mux_StEsT; Brother = brother_StEsT; IndBro = IndBro_stesT; LOC = "SETP"; }
            ///////Wst2
            IndBro_wst2 = search_ind(0, Wst2, ref XDL_mux_tmp, Number_of_lines);
            if (IndBro_wst2 != -1)
            {
                brother_wst2 = search(0, Wst2, ref XDL_mux_tmp, Number_of_lines);
                ind_ff = brother_wst2.IndexOf("FF<");
                if (brother_wst2.IndexOf("NuFF<0") != -1 & brother_wst2.IndexOf("NuMUX<0") == -1)
                    num_mux_Wst2 = 2 * Int32.Parse(brother_wst2.Substring(ind_ff - 6, 1));
            }
            if (num_mux_Wst2 > num_mux) {num_mux = num_mux_Wst2;  Brother = brother_wst2; IndBro = IndBro_wst2; LOC = "WST2"; }
                IndBro_wst2T = search_ind(0, Wst2T, ref XDL_mux_tmp, Number_of_lines);
                if (IndBro_wst2T != -1)
                {
                    brother_wst2T = search(0, Wst2T, ref XDL_mux_tmp, Number_of_lines);
                    ind_ff = brother_wst2T.IndexOf("FF<");
                    if (brother_wst2T.IndexOf("NuFF<0") != -1 & brother_wst2T.IndexOf("NuMUX<0") == -1)
                        num_mux_Wst2T = 2 * Int32.Parse(brother_wst2T.Substring(ind_ff - 6, 1));
                }
            if (num_mux_Wst2T > num_mux) {num_mux = num_mux_Wst2T;  Brother = brother_wst2T; IndBro = IndBro_wst2T; LOC = "WST2P"; }
            //////Est2
            IndBro_est2 = search_ind(0, Est2, ref XDL_mux_tmp, Number_of_lines);
            if (IndBro_est2 != -1)
            {
                brother_est2 = search(0, Est2, ref XDL_mux_tmp, Number_of_lines);
                ind_ff = brother_est2.IndexOf("FF<");
                if (brother_est2.IndexOf("NuFF<0") != -1 & brother_est2.IndexOf("NuMUX<0") == -1)
                    num_mux_Est2 = 2 * Int32.Parse(brother_est2.Substring(ind_ff - 6, 1));
            }
            if (num_mux_Est2 > num_mux) { num_mux = num_mux_Est2; Brother = brother_est2; IndBro = IndBro_est2; LOC = "EST2"; }
                IndBro_est2T = search_ind(0, Est2T, ref XDL_mux_tmp, Number_of_lines);
                if (IndBro_est2T != -1)
                {
                    brother_est2T = search(0, Est2T, ref XDL_mux_tmp, Number_of_lines);
                    ind_ff = brother_est2T.IndexOf("FF<");
                    if (brother_est2T.IndexOf("NuFF<0") != -1 & brother_est2T.IndexOf("NuMUX<0") == -1)
                        num_mux_Est2T = 2 * Int32.Parse(brother_est2T.Substring(ind_ff - 6, 1));
                }
                if (num_mux_Est2T > num_mux) { num_mux = num_mux_Est2T; Brother = brother_est2T; IndBro = IndBro_est2T; LOC = "EST2P"; }
            //////Nrth2
            IndBro_nrth2 = search_ind(0, Nrth2, ref XDL_mux_tmp, Number_of_lines);
            if (IndBro_nrth2 != -1)
            { 
                brother_nrth2 = search(0, Nrth2, ref XDL_mux_tmp, Number_of_lines);
                ind_ff = brother_nrth2.IndexOf("FF<");
                if (brother_nrth2.IndexOf("NuFF<0") != -1 & brother_nrth2.IndexOf("NuMUX<0") == -1)
                    num_mux_Nrth2 = 2 * Int32.Parse(brother_nrth2.Substring(ind_ff - 6, 1));
            }
            if (num_mux_Nrth2 > num_mux) { num_mux = num_mux_Nrth2; Brother = brother_nrth2; IndBro = IndBro_nrth2; LOC = "NRT2"; }
                IndBro_nrth2T = search_ind(0, Nrth2T, ref XDL_mux_tmp, Number_of_lines);
                if (IndBro_nrth2T != -1)
                {
                    brother_Nrth2T = search(0, Nrth2T, ref XDL_mux_tmp, Number_of_lines);
                    ind_ff = brother_Nrth2T.IndexOf("FF<");
                    if (brother_Nrth2T.IndexOf("NuFF<0") != -1 & brother_Nrth2T.IndexOf("NuMUX<0") == -1)
                        num_mux_Nrth2T = 2 * Int32.Parse(brother_Nrth2T.Substring(ind_ff - 6, 1));
                }
                if (num_mux_Nrth2T > num_mux) { num_mux = num_mux_Nrth2T; Brother = brother_Nrth2T; IndBro = IndBro_nrth2T; LOC = "NRT2P"; }
            /////Suth2
            IndBro_suth2 = search_ind(0, Suth2, ref XDL_mux_tmp, Number_of_lines);
            if (IndBro_suth2 != -1)
            { 
                brother_suth2 = search(0, Suth2, ref XDL_mux_tmp, Number_of_lines);
                ind_ff = brother_suth2.IndexOf("FF<");
                if (brother_suth2.IndexOf("NuFF<0") != -1 & brother_suth2.IndexOf("NuMUX<0") == -1)
                    num_mux_Suth2 = 2 * Int32.Parse(brother_suth2.Substring(ind_ff - 6, 1));
            }
            if (num_mux_Suth2 > num_mux) { num_mux = num_mux_Suth2; Brother = brother_suth2; IndBro = IndBro_suth2; LOC = "SUT2"; }
                IndBro_suth2T = search_ind(0, Suth2T, ref XDL_mux_tmp, Number_of_lines);
                if (IndBro_suth2T != -1)
                {
                    brother_Suth2T = search(0, Suth2T, ref XDL_mux_tmp, Number_of_lines);
                    ind_ff = brother_Suth2T.IndexOf("FF<");
                    if (brother_Suth2T.IndexOf("NuFF<0") != -1 & brother_Suth2T.IndexOf("NuMUX<0") == -1)
                         num_mux_Suth2T = 2 * Int32.Parse(brother_Suth2T.Substring(ind_ff - 6, 1));
                }
                if (num_mux_Suth2T > num_mux) { num_mux = num_mux_Suth2T; Brother = brother_Suth2T; IndBro = IndBro_suth2T; LOC = "SUT2P"; }
            /////Wst4
            IndBro_wst4 = search_ind(0, Wst4, ref XDL_mux_tmp, Number_of_lines);
            if (IndBro_wst4 != -1)
            { 
                brother_wst4 = search(0, Wst4, ref XDL_mux_tmp, Number_of_lines);
                ind_ff = brother_wst4.IndexOf("FF<");
                if (brother_wst4.IndexOf("NuFF<0") != -1 & brother_wst4.IndexOf("NuMUX<0") == -1)
                    num_mux_Wst4 = 2 * Int32.Parse(brother_wst4.Substring(ind_ff - 6, 1));
            }
            if (num_mux_Wst4 > num_mux) { num_mux = num_mux_Wst4; Brother = brother_wst4; IndBro = IndBro_wst4; LOC = "WST4"; }
                IndBro_wst4T = search_ind(0, Wst4T, ref XDL_mux_tmp, Number_of_lines);
                if (IndBro_wst4T != -1)
                {
                    brother_wst4T = search(0, Wst4T, ref XDL_mux_tmp, Number_of_lines);
                    ind_ff = brother_wst4T.IndexOf("FF<");
                    if (brother_wst4T.IndexOf("NuFF<0") != -1 & brother_wst4T.IndexOf("NuMUX<0") == -1)
                        num_mux_Wst4T = 2 * Int32.Parse(brother_wst4T.Substring(ind_ff - 6, 1));
                }
                if (num_mux_Wst4T > num_mux) { num_mux = num_mux_Wst4T; Brother = brother_wst4T; IndBro = IndBro_wst4T; LOC = "WST4P"; }
            /////Est4
            IndBro_est4 = search_ind(0, Est4, ref XDL_mux_tmp, Number_of_lines);
            if (IndBro_est4 != -1)
            { 
                brother_est4 = search(0, Est4, ref XDL_mux_tmp, Number_of_lines);
                ind_ff = brother_est4.IndexOf("FF<");
                if (brother_est4.IndexOf("NuFF<0") != -1 & brother_est4.IndexOf("NuMUX<0") == -1)
                    num_mux_Est4 = 2 * Int32.Parse(brother_est4.Substring(ind_ff - 6, 1));
            }
            if (num_mux_Est4T > num_mux) { num_mux = num_mux_Est4T; Brother = brother_est4T; IndBro = IndBro_est4T; LOC = "EST4"; }           
                IndBro_est4T = search_ind(0, Est4T, ref XDL_mux_tmp, Number_of_lines);
                if (IndBro_est4T != -1)
                {
                    brother_est4T = search(0, Est4T, ref XDL_mux_tmp, Number_of_lines);
                    ind_ff = brother_est4T.IndexOf("FF<");
                    if (brother_est4T.IndexOf("NuFF<0") != -1 & brother_est4T.IndexOf("NuMUX<0") == -1)
                         num_mux_Est4T = 2 * Int32.Parse(brother_est4T.Substring(ind_ff - 6, 1));
                }
                if (num_mux_Est4T > num_mux) { num_mux = num_mux_Est4T; Brother = brother_est4T; IndBro = IndBro_est4T; LOC = "EST4P"; }           
            /////Nrth4
            IndBro_nrth4 = search_ind(0, Nrth4, ref XDL_mux_tmp, Number_of_lines);
            if (IndBro_nrth4 != -1)
            { 
                brother_nrth4 = search(0, Nrth4, ref XDL_mux_tmp, Number_of_lines);
                ind_ff = brother_nrth4.IndexOf("FF<");
                if (brother_nrth4.IndexOf("NuFF<0") != -1 & brother_nrth4.IndexOf("NuMUX<0") == -1)
                    num_mux_Nrth4 = 2 * Int32.Parse(brother_nrth4.Substring(ind_ff - 6, 1));
            }
            if (num_mux_Nrth4 > num_mux) { num_mux = num_mux_Nrth4; Brother = brother_nrth4; IndBro = IndBro_nrth4; LOC = "NRT4"; }
                IndBro_nrth4T = search_ind(0, Nrth4T, ref XDL_mux_tmp, Number_of_lines);
                if (IndBro_nrth4 != -1)
                {
                    brother_Nrth4T = search(0, Nrth4T, ref XDL_mux_tmp, Number_of_lines);
                    ind_ff = brother_Nrth4T.IndexOf("FF<");
                    if (brother_Nrth4T.IndexOf("NuFF<0") != -1 & brother_Nrth4T.IndexOf("NuMUX<0") == -1)
                        num_mux_Nrth4T = 2 * Int32.Parse(brother_Nrth4T.Substring(ind_ff - 6, 1));
                }
                if (num_mux_Nrth4T > num_mux) { num_mux = num_mux_Nrth4T; Brother = brother_Nrth4T; IndBro = IndBro_nrth4T; LOC = "NRT4P"; }
            /////Suth4
            IndBro_suth4 = search_ind(0, Suth4, ref XDL_mux_tmp, Number_of_lines);
            if (IndBro_suth4 != -1)
            {
                brother_suth4 = search(0, Suth4, ref XDL_mux_tmp, Number_of_lines);
                ind_ff = brother_suth4.IndexOf("FF<");
                if (brother_suth4.IndexOf("NuFF<0") != -1 & brother_suth4.IndexOf("NuMUX<0") == -1)
                    num_mux_Suth4 = 2 * Int32.Parse(brother_suth4.Substring(ind_ff - 6, 1));
            }
            if (num_mux_Suth4 > num_mux) { num_mux = num_mux_Suth4; Brother = brother_suth4; IndBro = IndBro_suth4; LOC = "SUT4"; }
                IndBro_suth4T = search_ind(0, Suth4T, ref XDL_mux_tmp, Number_of_lines);
                if (IndBro_suth4 != -1)
                {
                    brother_Suth4T = search(0, Suth4T, ref XDL_mux_tmp, Number_of_lines);
                    ind_ff = brother_Suth4T.IndexOf("FF<");
                    if (brother_Suth4T.IndexOf("NuFF<0") != -1 & brother_Suth4T.IndexOf("NuMUX<0") == -1)
                      num_mux_Suth4T = 2 * Int32.Parse(brother_Suth4T.Substring(ind_ff - 6, 1));
                }
                if (num_mux_Suth4T > num_mux) { num_mux = num_mux_Suth4T; Brother = brother_Suth4T; IndBro = IndBro_suth4T; LOC = "SUT4P"; }

            if (require > num_mux)
            {
                HIS_INST_NUM--;
                string TMPWOR = "";

                if (brother_wst == "" & WOR != "0") { LOC = "WST"; Brother = "CTLL ROW" + (Int16.Parse(ROW) - 1) + " COL" + COL + " STL WOR" + (Int16.Parse(WOR) - 1) + " " + COL + " (REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_wstT == "" & Int16.Parse(WOR) >= 2) { LOC = "WSTP"; Brother = "CTLL ROW" + (Int16.Parse(ROW) - 1) + " COL" + COL + " STL WOR" + (Int16.Parse(WOR) - 2) + " " + COL + " (REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_nrth == "") { LOC = "NRT"; Brother = "CTLL ROW" + ROW + " COL" + (Int16.Parse(COL) + 1) + " STL WOR" + WOR + " " + (Int16.Parse(COL) + 1) + " (REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_nrthT == "" & WOR != "0") 
                {
                    if (Int16.Parse(WOR) % 2 == 0) TMPWOR = (Int16.Parse(WOR) - 1).ToString();
                    else TMPWOR = (Int16.Parse(WOR) + 1).ToString();
                    LOC = "NRTP"; Brother = "CTLL ROW" + ROW + " COL" + (Int16.Parse(COL) + 1) + " STL WOR" + TMPWOR + " " + (Int16.Parse(COL) + 1) + " (REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; 
                }
                else if (brother_est == "") { LOC = "EST"; Brother = "CTLL ROW" + (Int16.Parse(ROW) + 1) + " COL" + COL + " STL WOR" + (Int16.Parse(WOR) + 1) + " " + COL + " (REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_estT == "") { LOC = "ESTP"; Brother = "CTLL ROW" + (Int16.Parse(ROW) + 1) + " COL" + COL + " STL WOR" + (Int16.Parse(WOR) + 2) + " " + COL + " (REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_suth == "") { LOC = "SUT"; Brother = "CTLL ROW" + ROW + " COL" + (Int16.Parse(COL) - 1) + " STL WOR" + WOR + " " + (Int16.Parse(COL) - 1) + " (REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_suthT == "")
                {
                    if (Int16.Parse(WOR) % 2 == 0) TMPWOR = (Int16.Parse(WOR) - 1).ToString();
                    else TMPWOR = (Int16.Parse(WOR) + 1).ToString();
                    LOC = "SUTP"; Brother = "CTLL ROW" + ROW + " COL" + (Int16.Parse(COL) - 1) + " STL WOR" + TMPWOR + " " + (Int16.Parse(COL) - 1) + " (REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100;
                }
                else if (brother_wst2 == "" & Int16.Parse(WOR) >= 3) { LOC = "WST2"; Brother = "CTLL ROW" + (Int16.Parse(ROW) - 2) + " COL" + COL + " STL WOR" + (Int16.Parse(WOR) - 3) + " " + COL + " (REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_wst2T == "" & Int16.Parse(WOR) >= 4) { LOC = "WST2P"; Brother = "CTLL ROW" + (Int16.Parse(ROW) - 2) + " COL" + COL + " STL WOR" + (Int16.Parse(WOR) - 4) + " " + COL + " (REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_nrth2 == "") { LOC = "NRT2"; Brother = "CTLL ROW" + ROW + " COL" + (Int16.Parse(COL) + 2) + " STL WOR" + WOR + " " + (Int16.Parse(COL) + 2) + " (REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_Nrth2T == "")
                {
                    if (Int16.Parse(WOR) % 2 == 0) TMPWOR = (Int16.Parse(WOR) - 1).ToString();
                    else TMPWOR = (Int16.Parse(WOR) + 1).ToString();
                    LOC = "NRT2P"; Brother = "CTLL ROW" + ROW + " COL" + (Int16.Parse(COL) + 2) + " STL WOR" + TMPWOR + " " + (Int16.Parse(COL) + 2) + " (REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100;
                }
                else if (brother_est2 == "") { LOC = "EST2"; Brother = "CTLL ROW" + (Int16.Parse(ROW) + 2) + " COL" + COL + " STL WOR" + (Int16.Parse(WOR) + 3) + " " + COL + " (REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_est2T == "") { LOC = "EST2P"; Brother = "CTLL ROW" + (Int16.Parse(ROW) + 2) + " COL" + COL + " STL WOR" + (Int16.Parse(WOR) + 4) + " " + COL + " (REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_suth2 == "") { LOC = "SUT2"; Brother = "CTLL ROW" + ROW + " COL" + (Int16.Parse(COL) - 2) + " STL WOR" + WOR + " " + (Int16.Parse(COL) - 2) + " (REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_Suth2T == "")
                {
                    if (Int16.Parse(WOR) % 2 == 0) TMPWOR = (Int16.Parse(WOR) - 1).ToString();
                    else TMPWOR = (Int16.Parse(WOR) + 1).ToString();
                    LOC = "SUT2P"; Brother = "CTLL ROW" + ROW + " COL" + (Int16.Parse(COL) - 2) + " STL WOR" + TMPWOR + " " + (Int16.Parse(COL) - 2) + " (REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100;
                }
                else if (brother_NrWs == "" & Int16.Parse(WOR) >= 1) { LOC = "NWT"; Brother = "CTLL ROW" + (Int16.Parse(ROW) - 1) + " COL" + (Int16.Parse(COL) + 1) + " STL WOR" + (Int16.Parse(WOR) - 1) + " " + (Int16.Parse(COL) + 1) + "(REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_NrWsT == "" & Int16.Parse(WOR) >= 2) { LOC = "NWTP"; Brother = "CTLL ROW" + (Int16.Parse(ROW) - 1) + " COL" + (Int16.Parse(COL) + 1) + " STL WOR" + (Int16.Parse(WOR) - 2) + " " + (Int16.Parse(COL) + 1) + "(REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_NrEs == "") { LOC = "NET"; Brother = "CTLL ROW" + (Int16.Parse(ROW) + 1) + " COL" + (Int16.Parse(COL) + 1) + " STL WOR" + (Int16.Parse(WOR) + 1) + " " + (Int16.Parse(COL) + 1) + "(REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_NrEsT == "") { LOC = "NETP"; Brother = "CTLL ROW" + (Int16.Parse(ROW) + 1) + " COL" + (Int16.Parse(COL) + 1) + " STL WOR" + (Int16.Parse(WOR) + 2) + " " + (Int16.Parse(COL) + 1) + "(REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_StWs == "" & Int16.Parse(WOR) >= 1) { LOC = "SWT"; Brother = "CTLL ROW" + (Int16.Parse(ROW) - 1) + " COL" + (Int16.Parse(COL) - 1) + " STL WOR" + (Int16.Parse(WOR) - 1) + " " + (Int16.Parse(COL) - 1) + "(REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_StWsT == "" & Int16.Parse(WOR) >= 1) { LOC = "SWTP"; Brother = "CTLL ROW" + (Int16.Parse(ROW) - 1) + " COL" + (Int16.Parse(COL) - 1) + " STL WOR" + (Int16.Parse(WOR) - 2) + " " + (Int16.Parse(COL) - 1) + "(REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_StEs == "" & Int16.Parse(WOR) >= 1) { LOC = "SET"; Brother = "CTLL ROW" + (Int16.Parse(ROW) + 1) + " COL" + (Int16.Parse(COL) - 1) + " STL WOR" + (Int16.Parse(WOR) + 1) + " " + (Int16.Parse(COL) - 1) + "(REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_StEsT == "" & Int16.Parse(WOR) >= 1) { LOC = "SETP"; Brother = "CTLL ROW" + (Int16.Parse(ROW) + 1) + " COL" + (Int16.Parse(COL) - 1) + " STL WOR" + (Int16.Parse(WOR) + 2) + " " + (Int16.Parse(COL) - 1) + "(REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_wst4 == "" & Int16.Parse(WOR) >= 7) { LOC = "WST4"; Brother = "CTLL ROW" + (Int16.Parse(ROW) - 4) + " COL" + COL + " STL WOR" + (Int16.Parse(WOR) - 7) + " " + COL + " (REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_wst4T == "" & Int16.Parse(WOR) >= 8) { LOC = "WST4P"; Brother = "CTLL ROW" + (Int16.Parse(ROW) - 4) + " COL" + COL + " STL WOR" + (Int16.Parse(WOR) - 8) + " " + COL + " (REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_nrth4 == "") { LOC = "NRT4"; Brother = "CTLL ROW" + ROW + " COL" + (Int16.Parse(COL) + 4) + " STL WOR" + WOR + " " + (Int16.Parse(COL) + 4) + " (REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_Nrth4T == "")
                {
                    if (Int16.Parse(WOR) % 2 == 0) TMPWOR = (Int16.Parse(WOR) - 1).ToString();
                    else TMPWOR = (Int16.Parse(WOR) + 1).ToString();
                    LOC = "NRT4P"; Brother = "CTLL ROW" + ROW + " COL" + (Int16.Parse(COL) + 4) + " STL WOR" + TMPWOR + " " + (Int16.Parse(COL) + 4) + " (REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100;
                }
                else if (brother_est4 == "") { LOC = "EST4"; Brother = "CTLL ROW" + (Int16.Parse(ROW) + 4) + " COL" + COL + " STL WOR" + (Int16.Parse(WOR) + 7) + " " + COL + " (REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_est4T == "") { LOC = "EST4P"; Brother = "CTLL ROW" + (Int16.Parse(ROW) + 4) + " COL" + COL + " STL WOR" + (Int16.Parse(WOR) + 8) + " " + COL + " (REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_suth4 == "") { LOC = "SUT4"; Brother = "CTLL ROW" + ROW + " COL" + (Int16.Parse(COL) - 4) + " STL WOR" + WOR + " " + (Int16.Parse(COL) - 4) + " (REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100; }
                else if (brother_Suth4T == "")
                {
                    if (Int16.Parse(WOR) % 2 == 0) TMPWOR = (Int16.Parse(WOR) - 1).ToString();
                    else TMPWOR = (Int16.Parse(WOR) + 1).ToString();
                    LOC = "SUT4P"; Brother = "CTLL ROW" + ROW + " COL" + (Int16.Parse(COL) - 4) + " STL WOR" + TMPWOR + " " + (Int16.Parse(COL) - 4) + " (REDUNANT" + HIS_INST_NUM + ")" + " {NuMUX<8} " + "{NuFF<0}"; num_mux = 8; IndBro = -100;
                }
                else return 2;
                return 1;
            }
            else return 0;
        }

        public void set_Arguments1(string LOC,string MFF,ref string Brother, ref string His_FF, ref string BYPB, ref string Out_H_FF, ref string H_logic, ref int F11, ref int F12, ref int F21, ref int F22, ref int F31, ref int F32, ref int F41, ref int F42, ref string B, ref string Bh, ref string Pin1, ref string pin, ref string p)
        {
            if (His_FF == "A5F") { BYPB = "BYP_B1"; Out_H_FF = "AMUX"; H_logic = "20"; }
            else if (His_FF == "B5F") { BYPB = "BYP_B4"; Out_H_FF = "BMUX"; H_logic = "21"; }
            else if (His_FF == "C5F") { BYPB = "BYP_B3"; Out_H_FF = "CMUX"; H_logic = "22"; }
            else if (His_FF == "D5F") { BYPB = "BYP_B6"; Out_H_FF = "DMUX"; H_logic = "23"; }
            else if (His_FF == "AFF") { BYPB = "BYP_B1"; Out_H_FF = "AQ"; H_logic = "4"; }
            else if (His_FF == "BFF") { BYPB = "BYP_B4"; Out_H_FF = "BQ"; H_logic = "5"; }
            else if (His_FF == "CFF") { BYPB = "BYP_B3"; Out_H_FF = "CQ"; H_logic = "6"; }
            else if (His_FF == "DFF") { BYPB = "BYP_B6"; Out_H_FF = "DQ"; H_logic = "7"; }
            else if (His_FF == "A5L") { BYPB = "IMUX_B21"; Out_H_FF = "AMUX"; H_logic = "20"; }
            else if (His_FF == "B5L") { BYPB = "IMUX_B33"; Out_H_FF = "BMUX"; H_logic = "21"; }
            else if (His_FF == "C5L") { BYPB = "IMUX_B17"; Out_H_FF = "CMUX"; H_logic = "22"; }
            else if (His_FF == "D5L") { BYPB = "IMUX_B37"; Out_H_FF = "DMUX"; H_logic = "23"; }

            if (F12 == 1) { B = "19"; Bh = "28"; Pin1 = "A2"; pin = "A3"; F12 = 0; p = p.Insert(p.Length, " [A6LUT, " + LOC + " " + MFF + "]"); Brother = Brother.Insert(Brother.Length, " !A6LUT"); }
            else if (F11 == 1) { B = "30"; Bh = "25"; Pin1 = "A4"; pin = "A5";  F11 = 0; p = p.Insert(p.Length, " [A5LUT, " + LOC + " " + MFF + "]" + " [AOUTMUX]"); Brother = Brother.Insert(Brother.Length, " !A5LUT"); }
            else if (F22 == 1) { B = "39"; Bh = "24"; Pin1 = "B2"; pin = "B3"; F22 = 0; p = p.Insert(p.Length, " [B6LUT, " + LOC + " " + MFF + "]"); Brother = Brother.Insert(Brother.Length, " !B6LUT"); }
            else if (F21 == 1) { B = "26"; Bh = "29"; Pin1 = "B4"; pin = "B5";  F21 = 0; p = p.Insert(p.Length, " [B5LUT, " + LOC + " " + MFF + "]" + " [BOUTMUX]"); Brother = Brother.Insert(Brother.Length, " !B5LUT"); }
            else if (F32 == 1) { B = "23"; Bh = "40"; Pin1 = "C2"; pin = "C3"; F32 = 0; p = p.Insert(p.Length, " [C6LUT, " + LOC + " " + MFF + "]"); Brother = Brother.Insert(Brother.Length, " !C6LUT"); }
            else if (F31 == 1) { B = "42"; Bh = "14"; Pin1 = "C4"; pin = "C5"; F31 = 0; p = p.Insert(p.Length, " [C5LUT, " + LOC + " " + MFF + "]" + " [COUTMUX]"); Brother = Brother.Insert(Brother.Length, " !C5LUT"); }
            else if (F42 == 1) { B = "35"; Bh = "44"; Pin1 = "D2"; pin = "D3"; F42 = 0; p = p.Insert(p.Length, " [D6LUT, " + LOC + " " + MFF + "]"); Brother = Brother.Insert(Brother.Length, " !D6LUT"); }
            else if (F41 == 1) { B = "46"; Bh = "41"; Pin1 = "D4"; pin = "D5";  F41 = 0; p = p.Insert(p.Length, " [D5LUT, " + LOC + " " + MFF + "]" + " [DOUTMUX]"); Brother = Brother.Insert(Brother.Length, " !D5LUT"); }            
        }

        public void set_Arguments(string LOC,string MFF,ref string Brother, ref string His_FF, ref string BYPB, ref string Out_H_FF,
            ref string H_logic, ref int F11, ref int F12, ref int F21, ref int F22, ref int F31, ref int F32, ref int F41, ref int F42,
            ref string B, ref string Bh, ref string Pin1, ref string pin, ref string p)
        {
            if (His_FF == "A5F") { BYPB = "BYP_B0"; Out_H_FF = "AMUX"; H_logic = "16"; }
            else if (His_FF == "B5F") { BYPB = "BYP_B5"; Out_H_FF = "BMUX"; H_logic = "17"; }
            else if (His_FF == "C5F") { BYPB = "BYP_B2"; Out_H_FF = "CMUX"; H_logic = "18"; }
            else if (His_FF == "D5F") { BYPB = "BYP_B7"; Out_H_FF = "DMUX"; H_logic = "19"; }
            else if (His_FF == "AFF") { BYPB = "BYP_B0"; Out_H_FF = "AQ"; H_logic = "0"; }
            else if (His_FF == "BFF") { BYPB = "BYP_B5"; Out_H_FF = "BQ"; H_logic = "1"; }
            else if (His_FF == "CFF") { BYPB = "BYP_B2"; Out_H_FF = "CQ"; H_logic = "2"; }
            else if (His_FF == "DFF") { BYPB = "BYP_B7"; Out_H_FF = "DQ"; H_logic = "3"; }
            else if (His_FF == "A5L") { BYPB = "IMUX_B20"; Out_H_FF = "AMUX"; H_logic = "16"; }
            else if (His_FF == "B5L") { BYPB = "IMUX_B32"; Out_H_FF = "BMUX"; H_logic = "17"; }
            else if (His_FF == "C5L") { BYPB = "IMUX_B16"; Out_H_FF = "CMUX"; H_logic = "18"; }
            else if (His_FF == "D5L") { BYPB = "IMUX_B36"; Out_H_FF = "DMUX"; H_logic = "19"; }

            if (F12 == 1) { B = "18"; Bh = "5"; Pin1 = "A2"; pin = "A3"; F12 = 0; p = p.Insert(p.Length, " [A6LUT, " + LOC + " " + MFF + "]"); Brother = Brother.Insert(Brother.Length, " !A6LUT"); }
            else if (F11 == 1) { B = "7"; Bh = "0"; Pin1 = "A4"; pin = "A5"; F11 = 0; p = p.Insert(p.Length, " [A5LUT, " + LOC + " " + MFF + "]" + " [AOUTMUX]"); Brother = Brother.Insert(Brother.Length, " !A5LUT"); }
            else if (F22 == 1) { B = "38"; Bh = "1"; Pin1 = "B2"; pin = "B3"; F22 = 0; p = p.Insert(p.Length, " [B6LUT, " + LOC + " " + MFF + "]"); Brother = Brother.Insert(Brother.Length, " !B6LUT"); }
            else if (F21 == 1) { B = "3"; Bh = "4"; Pin1 = "B4"; pin = "B5";  F21 = 0; p = p.Insert(p.Length, " [B5LUT, " + LOC + " " + MFF + "]" + " [BOUTMUX]"); Brother = Brother.Insert(Brother.Length, " !B5LUT"); }
            else if (F32 == 1) { B = "22"; Bh = "9"; Pin1 = "C2"; pin = "C3"; F32 = 0; p = p.Insert(p.Length, " [C6LUT, " + LOC + " " + MFF + "]"); Brother = Brother.Insert(Brother.Length, " !C6LUT"); }
            else if (F31 == 1) { B = "11"; Bh = "12"; Pin1 = "C4"; pin = "C5";  F31 = 0; p = p.Insert(p.Length, " [C5LUT, " + LOC + " " + MFF + "]" + " [COUTMUX]"); Brother = Brother.Insert(Brother.Length, " !C5LUT"); }
            else if (F42 == 1) { B = "34"; Bh = "13"; Pin1 = "D2"; pin = "D3"; F42 = 0; p = p.Insert(p.Length, " [D6LUT, " + LOC + " " + MFF + "]"); Brother = Brother.Insert(Brother.Length, " !D6LUT"); }
            else if (F41 == 1) { B = "15"; Bh = "8"; Pin1 = "D4"; pin = "D5";  F41 = 0; p = p.Insert(p.Length, " [D5LUT, " + LOC + " " + MFF + "]" + " [DOUTMUX]"); Brother = Brother.Insert(Brother.Length, " !D5LUT"); }
        }

        public int search_ind(int j,string Goal, ref string[] XDL_mux_tmp, long Number_of_lines)
        {
            int i = 0;
            for (i = j; i < Number_of_lines; i++)
            {
                if(XDL_mux_tmp[i] != null)
                if (XDL_mux_tmp[i].IndexOf(Goal) != -1) return i;
            }
            return -1;
        }

        public string search(int j, string Goal, ref string[] XDL_mux_tmp,long number_of_lines)
        {
            int i = 0;
            for (i = j; i < number_of_lines; i++) 
            {
             /*   if (XDL_mux_tmp[i].IndexOf("\n") == -1)
                {
                    if (XDL_mux_tmp[i].IndexOf(Goal) != -1)
                        return XDL_mux_tmp[i];
                }
                else
                {
                    int oo = 0;
                    if (XDL_mux_tmp[i].IndexOf(Goal) != -1)
                    {
                       string one = ""; string two = "";
                       oo = XDL_mux_tmp[i].IndexOf("\n");
                      one = XDL_mux_tmp[i].Substring(XDL_mux_tmp[i].IndexOf("\n"), XDL_mux_tmp[i].Length - oo);
                      two = XDL_mux_tmp[i].Substring(0,XDL_mux_tmp[i].IndexOf("\n"));
                      if (one.IndexOf(Goal) != -1) return one;
                      else if (two.IndexOf(Goal) != -1) return two;
                    }
                }*/
                for (i = j; i < number_of_lines; i++)
                {
                    if (XDL_mux_tmp[i] != null)
                        if (XDL_mux_tmp[i].IndexOf(Goal) != -1) return XDL_mux_tmp[i];
                }
            }
            return "";
        }

    }
}
