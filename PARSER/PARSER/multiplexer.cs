using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace PARSER
{
    public class multiplexer
    {
        
        public void search_for_mux(ref string p, StreamWriter XDL_new_history,ref string ALL,ref int His_NUM)
        {
            int ind_ff = 0; int num_ff = 0; int num_mux = 0;
            string ROW = ""; string COL = ""; string Out_H_FF = "";
            string WOR = ""; string TYP = ""; string Styp = "";
            int logic = 0; string lOG = ""; string B = ""; string Bh = "";
            string BYPB = ""; string pin = ""; string His_FF = ""; string Pin1 = "";
            string H_logic = "";
            int F11 = 0; int F12 = 0; int F21 = 0; int F22 = 0; int F31 = 0; int F32 = 0; int F41 = 0; int F42 = 0;
            string inst_name = "";
            

            ind_ff = p.IndexOf("FF<");

            if (p.IndexOf("FF ") != -1 & p.IndexOf("*") == -1)
            {
                ROW = p.Substring(p.IndexOf("ROW") + 3, p.IndexOf(" ", p.IndexOf("ROW")) - p.IndexOf("ROW") - 3);
                COL = p.Substring(p.IndexOf("COL") + 3, p.IndexOf(" ", p.IndexOf("COL")) - p.IndexOf("COL") - 3);
                WOR = p.Substring(p.IndexOf("WOR") + 3, p.IndexOf(" ", p.IndexOf("WOR")) - p.IndexOf("WOR") - 3);
                TYP = p.Substring(p.IndexOf("CT") + 2, 2);
                Styp = p.Substring(p.IndexOf("ST") + 2, 1);
                F11 = 0; F12 = 0; F21 = 0; F22 = 0; F31 = 0; F32 = 0; F41 = 0; F42 = 0;
                if (p.IndexOf("A6LUT") == -1 & p.IndexOf("A5LUT") == -1 & p.IndexOf("AOUTMUX") == -1) { F11 = 1; F12 = 1; }
                if (p.IndexOf("B6LUT") == -1 & p.IndexOf("B5LUT") == -1 & p.IndexOf("BOUTMUX") == -1) { F21 = 1; F22 = 1; }
                if (p.IndexOf("C6LUT") == -1 & p.IndexOf("C5LUT") == -1 & p.IndexOf("COUTMUX") == -1) { F31 = 1; F32 = 1; }
                if (p.IndexOf("D6LUT") == -1 & p.IndexOf("D5LUT") == -1 & p.IndexOf("DOUTMUX") == -1) { F41 = 1; F42 = 1; }

                if (p.IndexOf("NuFF<0") == -1)
                {
                    num_ff = Int32.Parse(p.Substring(ind_ff + 3, 1));
                    num_mux = Int32.Parse(p.Substring(ind_ff - 6, 1));
                    num_mux = num_mux * 2;
                }

                if (num_ff <= num_mux & num_ff != 0)////////search for mux inside the slice
                {
                    int cache = 0;
                    inst_name = p.Substring(p.IndexOf("(") + 1, p.IndexOf(")") - p.IndexOf("(") - 1);
                    //AFF
                    if (p.IndexOf("1 H") != -1)
                    {
                        cache = p.IndexOf("1 H");
                        His_FF = p.Substring(p.IndexOf("1 H") + 4, 3);

                        if (Int32.Parse(WOR) % 2 != 0) logic = 0;
                        else logic = 4;

                        lOG = logic.ToString();

                        if (logic == 0)
                        {
                            set_arguments("INS","1 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        else if (logic == 4)
                        {
                            set_arguments1("INS", "1 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        His_NUM++;
                        int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                        if (indINT == -1)
                            Console.Write("not-found\n");
                        int indkama = ALL.IndexOf(",", indINT - 115);
                        string temporary = ALL.Substring(indkama, indINT - indkama);
                        ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                        Routing_function(inst_name, inst_name, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                        num_mux--;
                        num_ff--;
                    }////////end of the first if for "1 H"

                    if (p.IndexOf("3 H") != -1)
                    {
                        cache = p.IndexOf("3 H");
                        His_FF = p.Substring(p.IndexOf("3 H") + 4, 3);
                        if (Int32.Parse(WOR) % 2 != 0) logic = 1;
                        else logic = 5;
                        lOG = logic.ToString();

                        if (logic == 1)
                        {
                            set_arguments("INS", "3 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        else if (logic == 5)
                        {
                            set_arguments1("INS", "3 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        His_NUM++;
                        int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                        if (indINT == -1)
                            Console.Write("not-found\n");
                        int indkama = ALL.IndexOf(",", indINT - 115);
                        string temporary = ALL.Substring(indkama, indINT - indkama);
                        ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                        Routing_function(inst_name, inst_name, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                        num_mux--;
                        num_ff--;
                    }///////////end of the second if for "3 H"
                    if (p.IndexOf("5 H") != -1)
                    {
                        cache = p.IndexOf("5 H");
                        His_FF = p.Substring(p.IndexOf("5 H") + 4, 3);
                        if (Int32.Parse(WOR) % 2 != 0) logic = 2;
                        else logic = 6;
                        lOG = logic.ToString();
                        if (logic == 2)
                        {
                            set_arguments("INS", "5 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        else if (logic == 6)
                        {
                            set_arguments1("INS", "5 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        His_NUM++;
                        int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                        if (indINT == -1)
                            Console.Write("not-found\n");
                        int indkama = ALL.IndexOf(",", indINT - 115);
                        string temporary = ALL.Substring(indkama, indINT - indkama);
                        //ReRouting for Main & history flip flop
                        Routing_function(inst_name, inst_name, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                        num_mux--;
                        num_ff--;
                    }///////////end of the second if for "5 H"
                    if (p.IndexOf("7 H") != -1)
                    {
                        cache = p.IndexOf("7 H");
                        His_FF = p.Substring(p.IndexOf("7 H") + 4, 3);
                        if (Int32.Parse(WOR) % 2 != 0) logic = 3;
                        else logic = 7;
                        lOG = logic.ToString();
                        if (logic == 3)
                        {
                            set_arguments("INS", "7 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        else if (logic == 7)
                        {
                            set_arguments1("INS", "7 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        His_NUM++;
                        int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                        if (indINT == -1)
                            Console.Write("not-found\n");
                        int indkama = ALL.IndexOf(",", indINT - 115);
                        string temporary = ALL.Substring(indkama, indINT - indkama);
                        //ReRouting for Main & history flip flop
                        Routing_function(inst_name, inst_name, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                        num_mux--;
                        num_ff--;
                    }///////////end of the second if for "7 H"
                    if (p.IndexOf("2 H") != -1)
                    {
                        cache = p.IndexOf("2 H");
                        His_FF = p.Substring(p.IndexOf("2 H") + 4, 3);
                        if (Int32.Parse(WOR) % 2 != 0) logic = 16;
                        else logic = 20;
                        lOG = logic.ToString();

                        if (logic == 16)
                        {
                            set_arguments("INS", "2 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        else if (logic == 20)
                        {
                            set_arguments1("INS", "2 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        His_NUM++;
                        int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                        if (indINT == -1)
                            Console.Write("not-found\n");
                        int indkama = ALL.IndexOf(",", indINT - 115);
                        string temporary = ALL.Substring(indkama, indINT - indkama);
                        //ReRouting for Main & history flip flop
                        Routing_function(inst_name, inst_name, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                        num_mux--;
                        num_ff--;
                    }///////////end of the second if for "2 H"
                    if (p.IndexOf("4 H") != -1)
                    {
                        cache = p.IndexOf("4 H");
                        His_FF = p.Substring(p.IndexOf("4 H") + 4, 3);
                        if (Int32.Parse(WOR) % 2 != 0) logic = 17;
                        else logic = 21;
                        lOG = logic.ToString();

                        if (logic == 17)
                        {
                            set_arguments("INS", "4 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        else if (logic == 21)
                        {
                            set_arguments1("INS", "4 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        His_NUM++;
                        int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                        if (indINT == -1)
                            Console.Write("not-found\n");
                        int indkama = ALL.IndexOf(",", indINT - 115);
                        string temporary = ALL.Substring(indkama, indINT - indkama);
                        //ReRouting for Main & history flip flop
                        Routing_function(inst_name, inst_name, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                        num_mux--;
                        num_ff--;
                    }///////////end of the second if for "4 H"
                    if (p.IndexOf("6 H") != -1)
                    {
                        cache = p.IndexOf("6 H");
                        His_FF = p.Substring(p.IndexOf("6 H") + 4, 3);
                        if (Int32.Parse(WOR) % 2 != 0) logic = 18;
                        else logic = 22;
                        lOG = logic.ToString();

                        if (logic == 18)
                        {
                            set_arguments("INS", "6 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        else if (logic == 22)
                        {
                            set_arguments1("INS", "6 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        His_NUM++;
                        int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                        if (indINT == -1)
                            Console.Write("not-found\n");
                        int indkama = ALL.IndexOf(",", indINT - 115);
                        string temporary = ALL.Substring(indkama, indINT - indkama);
                        //ReRouting for Main & history flip flop
                        Routing_function(inst_name, inst_name, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                        num_mux--;
                        num_ff--;
                    }///////////end of the second if for "6 H"
                    if (p.IndexOf("8 H") != -1)
                    {
                        cache = p.IndexOf("8 H");
                        His_FF = p.Substring(p.IndexOf("8 H") + 4, 3);
                        if (Int32.Parse(WOR) % 2 != 0) logic = 19;
                        else logic = 23;
                        lOG = logic.ToString();

                        if (logic == 19)
                        {
                            set_arguments("INS", "8 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        else if (logic == 23)
                        {
                            set_arguments1("INS", "8 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        His_NUM++;
                        int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                        if (indINT == -1)
                            Console.Write("not-found\n");
                        int indkama = ALL.IndexOf(",", indINT - 115);
                        string temporary = ALL.Substring(indkama, indINT - indkama);
                        //ReRouting for Main & history flip flop
                        Routing_function(inst_name, inst_name, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                        num_mux--;
                        num_ff--;
                    }///////////end of the second if for "8 H"
                    string tiny = ""; string teeny = "";
                    tiny = p.Substring(p.IndexOf("NuFF") + 5, 1);
                    teeny = p.Substring(p.IndexOf("NuMUX") + 6, 1);
                    p = p.Replace("NuFF<" + tiny, "NuFF<" + num_ff);
                    p = p.Replace("NuMUX<" + teeny, "NuMUX<" + num_mux / 2);
                    if (num_ff == 0)
                        p = p.Insert(p.Length, " $");
                    else if (tiny != num_ff.ToString())
                        p = p.Insert(p.Length, "#");
                }

                else if (num_mux != 0 & num_ff != 0)/////search for mux inside the slice but the number of available MUXes is not sufficient
                {
                    int cache = 0;
                    inst_name = p.Substring(p.IndexOf("(") + 1, p.IndexOf(")") - p.IndexOf("(") - 1);
                    //AFF
                    if (p.IndexOf("1 H") != -1 & num_mux > 0)
                    {
                        cache = p.IndexOf("1 H");
                        His_FF = p.Substring(p.IndexOf("1 H") + 4, 3);

                        if (Int32.Parse(WOR) % 2 != 0) logic = 0;
                        else logic = 4;

                        lOG = logic.ToString();

                        if (logic == 0)
                        {
                            set_arguments("INS", "1 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        else if (logic == 4)
                        {
                            set_arguments1("INS", "1 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        His_NUM++;
                        int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                        if (indINT == -1)
                            Console.Write("not-found\n");
                        int indkama = ALL.IndexOf(",", indINT - 115);
                        string temporary = ALL.Substring(indkama, indINT - indkama);
                        //ReRouting for Main & history flip flop
                        Routing_function(inst_name, inst_name, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                        num_mux--;
                        num_ff--;
                    }////////end of the first if for "1 H"

                    if (p.IndexOf("3 H") != -1 & num_mux > 0)
                    {
                        cache = p.IndexOf("3 H");
                        His_FF = p.Substring(p.IndexOf("3 H") + 4, 3);
                        if (Int32.Parse(WOR) % 2 != 0) logic = 1;
                        else logic = 5;
                        lOG = logic.ToString();

                        if (logic == 1)
                        {
                            set_arguments("INS", "3 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        else if (logic == 5)
                        {
                            set_arguments1("INS", "3 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        His_NUM++;
                        int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                        if (indINT == -1)
                            Console.Write("not-found\n");
                        int indkama = ALL.IndexOf(",", indINT - 115);
                        string temporary = ALL.Substring(indkama, indINT - indkama);
                        //ReRouting for Main & history flip flop
                        Routing_function(inst_name, inst_name, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                        num_mux--;
                        num_ff--;
                    }///////////end of the second if for "3 H"
                    if (p.IndexOf("5 H") != -1 & num_mux > 0)
                    {
                        cache = p.IndexOf("5 H");
                        His_FF = p.Substring(p.IndexOf("5 H") + 4, 3);
                        if (Int32.Parse(WOR) % 2 != 0) logic = 2;
                        else logic = 6;
                        lOG = logic.ToString();
                        if (logic == 2)
                        {
                            set_arguments("INS", "5 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        else if (logic == 6)
                        {
                            set_arguments1("INS", "5 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        His_NUM++;
                        int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                        if (indINT == -1)
                            Console.Write("not-found\n");
                        int indkama = ALL.IndexOf(",", indINT - 115);
                        string temporary = ALL.Substring(indkama, indINT - indkama);
                        //ReRouting for Main & history flip flop
                        Routing_function(inst_name, inst_name, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                        num_mux--;
                        num_ff--;
                    }///////////end of the second if for "5 H"
                    if (p.IndexOf("7 H") != -1 & num_mux > 0)
                    {
                        cache = p.IndexOf("7 H");
                        His_FF = p.Substring(p.IndexOf("7 H") + 4, 3);
                        if (Int32.Parse(WOR) % 2 != 0) logic = 3;
                        else logic = 7;
                        lOG = logic.ToString();
                        if (logic == 3)
                        {
                            set_arguments("INS", "7 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        else if (logic == 7)
                        {
                            set_arguments1("INS", "7 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        His_NUM++;
                        int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                        if (indINT == -1)
                            Console.Write("not-found\n");
                        int indkama = ALL.IndexOf(",", indINT - 115);
                        string temporary = ALL.Substring(indkama, indINT - indkama);
                        //ReRouting for Main & history flip flop
                        Routing_function(inst_name, inst_name, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                        num_mux--;
                        num_ff--;
                    }///////////end of the second if for "7 H"
                    if (p.IndexOf("2 H") != -1 & num_mux > 0)
                    {
                        cache = p.IndexOf("2 H");
                        His_FF = p.Substring(p.IndexOf("2 H") + 4, 3);
                        if (Int32.Parse(WOR) % 2 != 0) logic = 16;
                        else logic = 20;
                        lOG = logic.ToString();

                        if (logic == 16)
                        {
                            set_arguments("INS", "2 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        else if (logic == 20)
                        {
                            set_arguments1("INS", "2 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        His_NUM++;
                        int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                        if (indINT == -1)
                            Console.Write("not-found\n");
                        int indkama = ALL.IndexOf(",", indINT - 115);
                        string temporary = ALL.Substring(indkama, indINT - indkama);
                        //ReRouting for Main & history flip flop
                        Routing_function(inst_name, inst_name, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                        num_mux--;
                        num_ff--;
                    }///////////end of the second if for "2 H"
                    if (p.IndexOf("4 H") != -1 & num_mux > 0)
                    {
                        cache = p.IndexOf("4 H");
                        His_FF = p.Substring(p.IndexOf("4 H") + 4, 3);
                        if (Int32.Parse(WOR) % 2 != 0) logic = 17;
                        else logic = 21;
                        lOG = logic.ToString();

                        if (logic == 17)
                        {
                            set_arguments("INS", "4 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        else if (logic == 21)
                        {
                            set_arguments1("INS", "4 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        His_NUM++;
                        int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                        if (indINT == -1)
                            Console.Write("not-found\n");
                        int indkama = ALL.IndexOf(",", indINT - 115);
                        string temporary = ALL.Substring(indkama, indINT - indkama);
                        ////////////////////////////////////////////////////////////ReRouting for Main & history flip flop
                        Routing_function(inst_name, inst_name, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                        num_mux--;
                        num_ff--;
                    }///////////end of the second if for "4 H"
                    if (p.IndexOf("6 H") != -1 & num_mux > 0)
                    {
                        cache = p.IndexOf("6 H");
                        His_FF = p.Substring(p.IndexOf("6 H") + 4, 3);
                        if (Int32.Parse(WOR) % 2 != 0) logic = 18;
                        else logic = 22;
                        lOG = logic.ToString();

                        if (logic == 18)
                        {
                            set_arguments("INS", "6 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        else if (logic == 22)
                        {
                            set_arguments1("INS", "6 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        His_NUM++;
                        int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                        if (indINT == -1)
                            Console.Write("not-found\n");
                        int indkama = ALL.IndexOf(",", indINT - 115);
                        string temporary = ALL.Substring(indkama, indINT - indkama);
                        //ReRouting for Main & history flip flop
                        Routing_function(inst_name, inst_name, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                        num_mux--;
                        num_ff--;
                    }///////////end of the second if for "6 H"
                    if (p.IndexOf("8 H") != -1 & num_mux > 0)
                    {
                        cache = p.IndexOf("8 H");
                        His_FF = p.Substring(p.IndexOf("8 H") + 4, 3);
                        if (Int32.Parse(WOR) % 2 != 0) logic = 19;
                        else logic = 23;
                        lOG = logic.ToString();

                        if (logic == 19)
                        {
                            set_arguments("INS", "8 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        else if (logic == 23)
                        {
                            set_arguments1("INS", "8 H", ref His_FF, ref BYPB, ref Out_H_FF, ref H_logic, ref F11, ref F12, ref F21, ref F22, ref F31, ref F32, ref F41, ref F42, ref B, ref Bh, ref Pin1, ref pin, ref p);
                        }
                        His_NUM++;
                        int indINT = ALL.IndexOf("pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,");
                        if (indINT == -1)
                            Console.Write("not-found\n");
                        int indkama = ALL.IndexOf(",", indINT - 115);
                        string temporary = ALL.Substring(indkama, indINT - indkama);
                        //ReRouting for Main & history flip flop
                        Routing_function(inst_name, inst_name, temporary, ROW, COL, lOG, BYPB, pin, TYP, Bh, B, Styp, Pin1, His_NUM, Out_H_FF, H_logic, ref ALL);
                        num_mux--;
                        num_ff--;
                    }///////////end of the second if for "8 H"
                    string tiny = ""; string teeny = "";
                    tiny = p.Substring(p.IndexOf("NuFF") + 5, 1);
                    teeny = p.Substring(p.IndexOf("NuMUX") + 6, 1);
                    p = p.Replace("NuFF<" + tiny, "NuFF<" + num_ff);
                    p = p.Replace("NuMUX<" + teeny, "NuMUX<" + num_mux / 2);
                    if(tiny != num_ff.ToString())
                    p = p.Insert(p.Length, " #");
                }
            }
            else 
                p = p.Insert(p.Length, " $");
        }

        public void Routing_function(string inst_name,string inst_name_H1,string temporary, string ROW, string COL, string lOG, string BYPB, string pin, string TYP,
            string Bh,string B, string Styp, string Pin1, int His_NUM, string Out_H_FF, string H_logic, ref string ALL)
        {
            ALL = ALL.Replace(temporary + "pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,",
                              ",\n  inpin \"" + inst_name_H1 + "\" " + pin + " " + temporary +
                              "pip CLB" + TYP + "_X" + ROW + "Y" + COL + " " + "CLB" + TYP + "_IMUX_B" + Bh + " -> " + "CLB" + TYP + "_" + Styp + "_" + pin + ",\n" +
                              "  pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> " + BYPB + " ,\n" +
                              "  pip INT_X" + ROW + "Y" + COL + " LOGIC_OUTS" + lOG + " -> IMUX_B" + Bh + " ,\n;" +
                              "\n net \"History_H_" + His_NUM + "\" ,\n" + "  outpin \"" + inst_name + "\" " + Out_H_FF + " ,\n" + "  inpin \"" + inst_name_H1 + "\" " + Pin1 +
                              " ,\n  pip CLB" + TYP + "_X" + ROW + "Y" + COL + " " + "CLB" + TYP + "_" + Styp + "_" + Out_H_FF + " -> " + "CLB" +
                              TYP + "_" + "LOGIC_OUTS" + H_logic + " ,\n" + "  pip CLB" + TYP + "_X" + ROW + "Y" + COL + " " +
                              "CLB" + TYP + "_IMUX_B" + B + " -> " + "CLB" + TYP + "_" + Styp + "_" + Pin1 + " " + ",\n" +
                              "  pip INT" + "_X" + ROW + "Y" + COL + " " + "LOGIC_OUTS" + H_logic + " -> IMUX_B" + B + " ,");
        }
        public void set_arguments1(string LOC,string MFF,ref string His_FF, ref string BYPB, ref string Out_H_FF, ref string H_logic, ref int F11,
            ref int F12, ref int F21, ref int F22, ref int F31, ref int F32, ref int F41, ref int F42, ref string B,
            ref string Bh, ref string Pin1, ref string pin,ref string p)
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

            if (F12 == 1) { B = "19"; Bh = "28"; Pin1 = "A2"; pin = "A3";  F12 = 0; p = p.Insert(p.Length, " [A6LUT, " + LOC + " " + MFF + "]"); }
            else if (F11 == 1) { B = "30"; Bh = "25"; Pin1 = "A4"; pin = "A5"; F11 = 0; p = p.Insert(p.Length, " [A5LUT, " + LOC + " " + MFF + "]" + " [AOUTMUX]"); }
            else if (F22 == 1) { B = "39"; Bh = "24"; Pin1 = "B2"; pin = "B3"; F22 = 0; p = p.Insert(p.Length, " [B6LUT, " + LOC + " " + MFF + "]"); }
            else if (F21 == 1) { B = "26"; Bh = "29"; Pin1 = "B4"; pin = "B5"; F21 = 0; p = p.Insert(p.Length, " [B5LUT, " + LOC + " " + MFF + "]" + " [BOUTMUX]"); }
            else if (F32 == 1) { B = "23"; Bh = "40"; Pin1 = "C2"; pin = "C3"; F32 = 0; p = p.Insert(p.Length, " [C6LUT, " + LOC + " " + MFF + "]"); }
            else if (F31 == 1) { B = "42"; Bh = "14"; Pin1 = "C4"; pin = "C5"; F31 = 0; p = p.Insert(p.Length, " [C5LUT, " + LOC + " " + MFF + "]" + " [COUTMUX]"); }
            else if (F42 == 1) { B = "35"; Bh = "44"; Pin1 = "D2"; pin = "D3"; F42 = 0; p = p.Insert(p.Length, " [D6LUT, " + LOC + " " + MFF + "]"); }
            else if (F41 == 1) { B = "46"; Bh = "41"; Pin1 = "D4"; pin = "D5"; F41 = 0; p = p.Insert(p.Length, " [D5LUT, " + LOC + " " + MFF + "]" + " [DOUTMUX]"); }            
        }
        public void set_arguments(string LOC,string MFF, ref string His_FF, ref string BYPB, ref string Out_H_FF, ref string H_logic, ref int F11,
            ref int F12, ref int F21, ref int F22, ref int F31, ref int F32, ref int F41, ref int F42, ref string B, 
            ref string Bh, ref string Pin1, ref string pin,ref string p)
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

            if (F11 == 1) { B = "18"; Bh = "5"; Pin1 = "A2"; pin = "A3"; F11 = 0; p = p.Insert(p.Length, " [A6LUT, " + LOC + " " + MFF + "]"); }
            else if (F12 == 1) { B = "7"; Bh = "0"; Pin1 = "A4"; pin = "A5"; F12 = 0; p = p.Insert(p.Length, " [A5LUT, " + LOC + " " + MFF + "]" + " [AOUTMUX]"); }
            else if (F22 == 1) { B = "38"; Bh = "1"; Pin1 = "B2"; pin = "B3";  F22 = 0; p = p.Insert(p.Length, " [B6LUT, " + LOC + " " + MFF + "]"); }
            else if (F21 == 1) { B = "3"; Bh = "4"; Pin1 = "B4"; pin = "B5"; F21 = 0; p = p.Insert(p.Length, " [B5LUT, " + LOC + " " + MFF + "]" + " [BOUTMUX]"); }
            else if (F32 == 1) { B = "22"; Bh = "9"; Pin1 = "C2"; pin = "C3"; F32 = 0; p = p.Insert(p.Length, " [C6LUT, " + LOC + " " + MFF + "]"); }
            else if (F31 == 1) { B = "11"; Bh = "12"; Pin1 = "C4"; pin = "C5"; F31 = 0; p = p.Insert(p.Length, " [C5LUT, " + LOC + " " + MFF + "]" + " [COUTMUX]"); }
            else if (F42 == 1) { B = "34"; Bh = "13"; Pin1 = "D2"; pin = "D3"; F42 = 0; p = p.Insert(p.Length, " [D6LUT, " + LOC + " " + MFF + "]"); }
            else if (F41 == 1) { B = "15"; Bh = "8"; Pin1 = "D4"; pin = "D5";   F41 = 0; p = p.Insert(p.Length, " [D5LUT, " + LOC + " " + MFF + "]" + " [DOUTMUX]"); }            
        }
    }
}
