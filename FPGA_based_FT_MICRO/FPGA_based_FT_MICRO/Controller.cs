using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FPGA_based_FT_MICRO
{    
    class Controller
    {
        static public string slice = "";
        static public int i = 0;
        static public int j = 0;
        static public int OR_pin = 0;
        internal void main()
        {
            int Border_WOR = 0;
            int TmpBorder_WOR = 0;
            int Border_ROW = 0;
            int TmpBorder_ROW = 0;
            int Border_WOR_min = 0;
            int TmpBorder_WOR_min = 0;
            int Border_ROW_min = 0;
            int TmpBorder_ROW_min = 0;
            int COL = 0;
            string line = "";
            Stream Find_POS;
            Find_POS = File.OpenRead(@"F:\MS\FinalProject\CODE\Final.XDL");
            StreamReader XDL_POS = new StreamReader(Find_POS);

            ///////At this section we want to find the controller position and it's between to module that were duplicated
            ///////in one column
            line = XDL_POS.ReadLine();
            TmpBorder_WOR_min = Int16.Parse(line.Substring(line.IndexOf("WOR") + 3, line.IndexOf(" ", line.IndexOf("WOR")) - line.IndexOf("WOR") - 3));
            TmpBorder_ROW_min = Int16.Parse(line.Substring(line.IndexOf("ROW") + 3, line.IndexOf(" ", line.IndexOf("ROW")) - line.IndexOf("ROW") - 3));
            while (XDL_POS.EndOfStream == false)
            {
                if (line.IndexOf("WOR") != -1)
                {
                    TmpBorder_WOR = Int16.Parse(line.Substring(line.IndexOf("WOR") + 3, line.IndexOf(" ", line.IndexOf("WOR")) - line.IndexOf("WOR") - 3));
                    TmpBorder_ROW = Int16.Parse(line.Substring(line.IndexOf("ROW") + 3, line.IndexOf(" ", line.IndexOf("ROW")) - line.IndexOf("ROW") - 3));
                    if (TmpBorder_WOR > Border_WOR) Border_WOR = TmpBorder_WOR;
                    if (TmpBorder_ROW > Border_ROW) Border_ROW = TmpBorder_ROW;
                    if (TmpBorder_WOR <= TmpBorder_WOR_min) Border_WOR_min = TmpBorder_WOR;
                    if (TmpBorder_ROW <= TmpBorder_ROW_min) Border_ROW_min = TmpBorder_ROW;
                    COL = Int16.Parse(line.Substring(line.IndexOf("COL") + 3, line.IndexOf(" ", line.IndexOf("COL")) - line.IndexOf("COL") - 3));
                }
                line = XDL_POS.ReadLine();
            }

            int ROW_CTRL = 0;
            int WOR_L_CTRL = 0;
            
            ///////ROW position
            ROW_CTRL = Border_ROW + 1;
            ///////WOR position
            if (Border_WOR % 2 == 0)  WOR_L_CTRL = Border_WOR + 2;
            else  WOR_L_CTRL = Border_WOR + 1;
            ///////Design a counter
            string COUNTER = "";
            COUNTER = "inst \"" + "COUNTER2bits" + "\"" + " \"SLICEL\" ," + "placed " + "CLBLM_X" + ROW_CTRL + "Y" + COL +
                            " SLICE_X" + WOR_L_CTRL + "Y" + COL + "  ,\n  cfg \" " +
                            " A5FFINIT::#OFF A5FFMUX::#OFF A5FFSR::#OFF A5LUT::#OFF A6LUT:G131:#LUT:O6=(~A3*A1)\n" +
                            "\t\tACY0::#OFF AFF:DFF_0_Cou/Q:#FF AFFINIT::INIT0 AFFMUX::O6 AFFSR::SRHIGH\n" +
                            "\t\tAOUTMUX::#OFF AUSED::#OFF B5FFINIT::#OFF B5FFMUX::#OFF B5FFSR::#OFF\n" +
                            "\t\tB5LUT::#OFF B6LUT:XOR:#LUT:O6=(A2@A4)*A1 BCY0::#OFF\n" +
                            "\t\tBFF:DFF_1_Cou/Q:#FF BFFINIT::#OFF BFFMUX::O6 BFFSR::SRHIGH BOUTMUX::#OFF\n" +
                            "\t\tBUSED::#OFF C5FFINIT::#OFF C5FFMUX::#OFF C5FFSR::#OFF C5LUT::#OFF\n" +
                            "\t\tC6LUT:NOT:#LUT:O6=(A2*A4) CCY0::#OFF CEUSEDMUX::#OFF CFF::#OFF CFFINIT::#OFF CFFMUX::#OFF\n" +
                            "\t\tCFFSR::#OFF CLKINV::CLK COUTMUX::#OFF COUTUSED::#OFF CUSED::0\n" +
                            "\t\tD5FFINIT::#OFF D5FFMUX::#OFF D5FFSR::#OFF D5LUT::#OFF D6LUT::#OFF\n" +
                            "\t\tDCY0::#OFF DFF::#OFF DFFINIT::#OFF DFFMUX::#OFF DFFSR::#OFF DOUTMUX::#OFF\n" +
                            "\t\tDUSED::#OFF PRECYINIT::#OFF SRUSEDMUX::#OFF SYNC_ATTR::SYNC\" \n   ;\n";

            Stream COUNT;
            COUNT = File.OpenRead(@"F:\MS\FinalProject\CODE\Ch_s953.XDL");
            StreamReader XDL_COUNTER = new StreamReader(COUNT);
            
            string Rd_for_Coun = "";
            int COUNTER_POS = 0;
            Rd_for_Coun = XDL_COUNTER.ReadToEnd();
            XDL_COUNTER.Close();
            COUNT.Close();
            COUNTER_POS = Rd_for_Coun.IndexOf("inst \"XDL_");

            //////routing for the counter
            string nets = "";
            nets = "net \"DFF_0_Cou/Q\" ,\n  " +
                   "outpin " + "\"COUNTER2bits\"" + " AQ ,\n  " +
                   "inpin " + "\"COUNTER2bits\"" + " A3 ,\n  " +
                   "inpin " + "\"COUNTER2bits\"" + " B2 ,\n  " +
                   "inpin " + "\"COUNTER2bits\"" + " C2 ,\n  ;\n" +
                   "net \"DFF_1_Cou/Q\" ,\n  " +
                   "outpin " + "\"COUNTER2bits\"" + " BQ ,\n  " +
                   "inpin " + "\"COUNTER2bits\"" + " B4 ,\n  " +
                   "inpin " + "\"COUNTER2bits\"" + " C4 ,\n  ;\n" +
                   "net \"PR\" , cfg \" _BELSIG:PAD,PAD,PR:PR\" ,\n  ;\n" +
                   "net \"PR_OBUF\" ,\n  " +
                   "outpin \"COUNTER2bits\" C ,\n  " +
                   "inpin \"PR\" O ,\n  ;\n";

            int POS_CLK = 0;
            POS_CLK = Rd_for_Coun.IndexOf("net \"CK_BUFGP\"");
            POS_CLK = Rd_for_Coun.IndexOf("pip", POS_CLK);

            Rd_for_Coun = Rd_for_Coun.Insert(POS_CLK, "inpin \"COUNTER2bits\" CLK ,\n  ");

            ///////////////////Design the combinational part of the controller 
            ///////////////////A big or that the number of inputs is equal to the number of FFs in a simple circuit
            ///////////////////this inputs come from the outputs of equivalent FFs XOR in the two module
            ///////////////////the number of XORs are qual to the number of FFs
            float number_of_FFs = 0;
            int number_of_stages = 1;
            string Final_file = "";
            int index_ff = 0; int tmp_NUM_FF = 0;
            
            Stream NUM_FFs;
            NUM_FFs = File.OpenRead(@"F:\MS\FinalProject\CODE\Final.XDL");
            StreamReader XDL_Num_FFs = new StreamReader(NUM_FFs);
            Final_file = XDL_Num_FFs.ReadToEnd();

            while ((index_ff = Final_file.IndexOf("FF ", index_ff)) != -1)
            {
                index_ff += 3;
                number_of_FFs++;
            }
            number_of_FFs /= 2;
            XDL_Num_FFs.Close();
            NUM_FFs.Close();

            tmp_NUM_FF = (int)number_of_FFs;

            while (tmp_NUM_FF > 6)
            {
                tmp_NUM_FF /= 6;
                number_of_stages++;
            }
            WOR_L_CTRL = WOR_L_CTRL + 1;
            //////Call the design OR function
            int LOCAT = 0;
            OR_design(number_of_FFs, number_of_stages,ref WOR_L_CTRL,ref COL, ROW_CTRL,ref nets,LOCAT);
            //////Concatenate the counter slice with new slices that used for OR function
            COUNTER = COUNTER + slice;
            ////////Insert new slices
            Rd_for_Coun = Rd_for_Coun.Insert(COUNTER_POS, COUNTER);
            ///////Insert new nets
            Rd_for_Coun = Rd_for_Coun.Insert(Rd_for_Coun.IndexOf("net \"GLOBAL_LOGIC0_0") - 1 ,nets);
            Stream Wr_Count;
            Wr_Count = File.OpenWrite(@"F:\MS\FinalProject\CODE\Ch_s953.XDL");
            StreamWriter XDL_WR_Count = new StreamWriter(Wr_Count);

           
            ////////////////////////Design comparision of equivalent FFs in to module by XOR//////////////////////////
            //////////////////////////XOR/////////////////////////////////////////XOR/////////////////////////////////
            Comarision_by_XOR(number_of_FFs,number_of_stages,ref WOR_L_CTRL,ref COL,ROW_CTRL,ref Rd_for_Coun);
            Do_input_XOR_Routing(number_of_FFs, ref Rd_for_Coun);
            //////Write new circuit 
            XDL_WR_Count.Write(Rd_for_Coun);
            XDL_WR_Count.Close();
            Wr_Count.Close();
        }
        private void OR_design(float number_of_FFs, int number_of_stages,ref int WOR,ref int COL, int ROW_CTRL,ref string nets,int Location)
        {
            string ALUT = ""; string AUSED = "";
            string BLUT = ""; string BUSED = "";
            string CLUT = ""; string CUSED = "";
            string DLUT = ""; string DUSED = "";
            float num_in_las_or = 0;
            float PIN = 0;
            float num_of_nxt_FF = 0;
            string pinA = ""; string pinB = ""; string pinC = ""; string pinD = "";
            num_in_las_or = number_of_FFs;
            num_of_nxt_FF = (float)Math.Ceiling(number_of_FFs / 6);

            while (number_of_FFs > 0)
            {
                PIN = 1;
                while (number_of_FFs > 0 & PIN < 25)
                {                    
                    if (PIN < 7 & number_of_FFs > 0)
                    {
                        ALUT = ALUT + "A" + PIN.ToString();
                        if (PIN < 6 & number_of_FFs > 1)
                            ALUT = ALUT + "+";
                        number_of_FFs--;
                        PIN++;
                    }
                    if (PIN < 13 & PIN > 6 & number_of_FFs > 0)
                    {
                        BLUT = BLUT + "A" + (PIN - 6).ToString();
                        if (PIN < 12 & number_of_FFs > 1)
                            BLUT = BLUT + "+";
                        number_of_FFs--;
                        PIN++;
                    }
                    if (PIN < 19 & PIN > 12 & number_of_FFs > 0)
                    {
                        CLUT = CLUT + "A" + (PIN - 12).ToString();
                        if (PIN < 18 & number_of_FFs > 1)
                            CLUT = CLUT + "+";
                        number_of_FFs--;
                        PIN++;
                    }
                    if (PIN < 25 & PIN > 18 & number_of_FFs > 0)
                    {
                        DLUT = DLUT + "A" + (PIN - 18).ToString();
                        if (PIN < 24 & number_of_FFs > 1)
                            DLUT = DLUT + "+";
                        number_of_FFs--;
                        PIN++;
                    }
                }
                string OUTPUT = "";
                if (ALUT != "") 
                {
                    if (number_of_stages != 1)
                    {
                        if (j % 3 == 0) pinA = "A1";
                        else if (j % 3 == 1) pinA = "A5";
                        else pinA = "A3";
                        OUTPUT = (number_of_stages - 1).ToString() + Location.ToString();                        
                        nets = nets + "net \"ORpin" + number_of_stages + OR_pin + number_of_FFs + "\" ," + "\n  outpin \"OR" + number_of_stages + number_of_FFs + "\" A ," + "\n  inpin \"OR" + OUTPUT + "\" " + pinA + " ,\n;\n";
                        OR_pin++;
                    }
                    else Do_mux_select_Routing(ref nets, number_of_stages.ToString() + number_of_FFs.ToString());
                    ALUT = "OR1:#LUT:O6=" + "(" + ALUT + ")"; AUSED = "0";
                }
                else { ALUT = ":#OFF"; AUSED = "#OFF"; }
                if (BLUT != "") 
                {
                    if (number_of_stages != 1)
                    {
                        if (j % 3 == 0) pinB = "A2";
                        else if (j % 3 == 1) pinB = "A6";
                        else pinB = "A4";
                    }
                    BLUT = "OR2:#LUT:O6=" + "(" + BLUT + ")"; BUSED = "0";
                    nets = nets + "net \"ORpin" + number_of_stages + OR_pin + number_of_FFs + "\" ," + "\n  outpin \"OR" + number_of_stages + number_of_FFs + "\" B ," + "\n  inpin \"OR" + (number_of_stages - 1).ToString() + Location.ToString() + "\" " + pinB + " ,\n;\n";
                    OR_pin++;
                }
                else 
                { BLUT = ":#OFF"; BUSED = "#OFF"; }
                if (CLUT != "") 
                {
                    if (number_of_stages != 1)
                    {
                        if (j % 3 == 0) pinC = "A3";
                        else if (j % 3 == 1) pinC = "A1";
                        else pinC = "A5";
                    }
                    CLUT = "OR3:#LUT:O6=" + "(" + CLUT + ")"; CUSED = "0";
                    nets = nets + "net \"ORpin" + number_of_stages + OR_pin + number_of_FFs + "\" ," + "\n  outpin \"OR" + number_of_stages + number_of_FFs + "\" C ," + "\n  inpin \"OR" + (number_of_stages - 1).ToString() + Location.ToString() + "\" " + pinC + " ,\n;\n";
                    OR_pin++;
                }
                else { CLUT = ":#OFF"; CUSED = "#OFF"; }
                if (DLUT != "") 
                {
                    if (number_of_stages != 1)
                    {
                        if (j % 3 == 0) pinD = "A4";
                        else if (j % 3 == 1) pinD = "A2";
                        else pinD = "A6";
                    }
                    DLUT = "OR4:#LUT:O6=" + "(" + DLUT + ")"; DUSED = "0";
                    nets = nets + "net \"ORpin" + number_of_stages + OR_pin + number_of_FFs + "\" ," + "\n  outpin \"OR" + number_of_stages + number_of_FFs + "\" D ," + "\n  inpin \"OR" + (number_of_stages - 1).ToString() + Location.ToString() + "\" " + pinD + " ,\n;\n";
                    OR_pin++;
                }
                else { DLUT = ":#OFF"; DUSED = "#OFF"; }
                j++;

                slice = slice + "inst \"" + "OR" + number_of_stages + number_of_FFs + "\"" + " \"SLICEL\" ," + "placed " + "CLBLM_X" + ROW_CTRL + "Y" + COL +
                            " SLICE_X" + WOR + "Y" + COL + "  ,\n  cfg \" " +
                            " A5FFINIT::#OFF A5FFMUX::#OFF A5FFSR::#OFF A5LUT::#OFF A6LUT:" + ALUT + "\n" +
                            "\t\tACY0::#OFF AFF::#OFF AFFINIT::#OFF AFFMUX::#OFF AFFSR::#OFF\n" +
                            "\t\tAOUTMUX::#OFF AUSED::" + AUSED + " B5FFINIT::#OFF B5FFMUX::#OFF B5FFSR::#OFF\n" +
                            "\t\tB5LUT::#OFF B6LUT:" + BLUT + " BCY0::#OFF\n" +
                            "\t\tBFF::#OFF BFFINIT::#OFF BFFMUX::#OFF BFFSR::#OFF BOUTMUX::#OFF\n" +
                            "\t\tBUSED::" + BUSED + " C5FFINIT::#OFF C5FFMUX::#OFF C5FFSR::#OFF C5LUT::#OFF\n" +
                            "\t\tC6LUT:" + CLUT + " CCY0::#OFF CEUSEDMUX::#OFF CFF::#OFF CFFINIT::#OFF CFFMUX::#OFF\n" +
                            "\t\tCFFSR::#OFF CLKINV::#OFF COUTMUX::#OFF COUTUSED::#OFF CUSED::" + CUSED + "\n" +
                            "\t\tD5FFINIT::#OFF D5FFMUX::#OFF D5FFSR::#OFF D5LUT::#OFF D6LUT:" + DLUT + "\n" +
                            "\t\tDCY0::#OFF DFF::#OFF DFFINIT::#OFF DFFMUX::#OFF DFFSR::#OFF DOUTMUX::#OFF\n" +
                            "\t\tDUSED::" + DUSED + " PRECYINIT::#OFF SRUSEDMUX::#OFF SYNC_ATTR::#OFF\" \n   ;\n";
                if (i % 2 == 0)
                { WOR -= 1; COL -= 1; }
                else WOR += 1;
                i++;
                ALUT = BLUT = CLUT = DLUT = AUSED = BUSED = CUSED = DUSED = "";
            }
            number_of_stages = number_of_stages - 1;

            if (number_of_stages > 0)
             OR_design(num_of_nxt_FF, number_of_stages,ref WOR,ref COL, ROW_CTRL,ref nets,0); 
        }

        private void Do_mux_select_Routing(ref string nets, string outpin)
        {
            string routing = "";
            string name = "";
            string pin = "";
            string select_nets = "";
            int index_net = 0;
            int index_name = 0;
            Stream SEL_MUX;
            SEL_MUX = File.OpenRead(@"F:\MS\FinalProject\CODE\Duplicated_Routing.XDL");
            StreamReader XDL_SEL_MUX= new StreamReader(SEL_MUX);
            routing = XDL_SEL_MUX.ReadToEnd();

            select_nets = "net \"select_muxes\"  ,\n  outpin \"OR" + outpin + "\" A ,\n";
            while ((index_net = routing.IndexOf("History_H_", index_net)) != -1)
            {
                index_name = routing.IndexOf("inpin", index_net);
                name = routing.Substring(index_name + 7, routing.IndexOf("\"", index_name + 7) - index_name - 7);
                pin = routing.Substring(routing.IndexOf("\"", index_name + 7) + 2, 1);

                if (pin == "A") pin = "A1";
                else if (pin == "B") pin = "B1";
                else if (pin == "C") pin = "C1";
                else pin = "D1";
                select_nets = select_nets + "  inpin" + " \"" + name + "\" " + pin + " ,\n";
                index_net += 10;                
            }
            select_nets = select_nets + "  inpin \"COUNTER2bits\" A1 ,\n  inpin \"COUNTER2bits\" B1 ,\n";
            select_nets = select_nets + ";\n";
            nets = nets + select_nets;
        }

        private void Comarision_by_XOR(float number_of_FFs,int Num_Stage, ref int WOR, ref int COL, int ROW_CTRL,ref string Circuit)
        {
            string slices = ""; string net = ""; string inpin = ""; string slicename = "";
            string A6LUT = ":#OFF"; string A5LUT = ":#OFF";
            string B6LUT = ":#OFF"; string B5LUT = ":#OFF";
            string C6LUT = ":#OFF"; string C5LUT = ":#OFF";
            string D6LUT = ":#OFF"; string D5LUT = ":#OFF";
            string AUSED = "#OFF"; string AOUTMUX = "#OFF";
            string BUSED = "#OFF"; string BOUTMUX = "#OFF";
            string CUSED = "#OFF"; string COUTMUX = "#OFF";
            string DUSED = "#OFF"; string DOUTMUX = "#OFF";
            int which = 0; int k = 1; int location = 0; int j = 0;
            if (number_of_FFs - 8 > 0)
                slicename = (number_of_FFs - 8).ToString();
            else slicename = "0";
            while (number_of_FFs > 0)
            {
                if (which == 24 * j)
                {
                    location =(int) number_of_FFs - 24;
                    if (location < 0) location = 0;
                    j++;
                }
                if (which % 8 == 0)
                {
                    A6LUT = "XOR" + which + number_of_FFs + ":#LUT:O6=(A1@A3)";
                    AUSED = "0";
                    number_of_FFs--;
                    if (which % 24 == 0) inpin = "A1";
                    else if (which  % 24 == 8) inpin = "B3";
                    else if (which  % 24 == 16) inpin = "C5";
                    net = net + "net \"XOR" + which + number_of_FFs + k + "\" ,\n  " +
                        "outpin \"" + "XOR" + slicename +"\" A ,\n  " +
                        "inpin \"OR" + Num_Stage + location + "\" " + inpin + " ,\n;\n";
                }
                else if (which % 8 == 1)
                {
                    A5LUT = "XOR" + which + number_of_FFs + ":#LUT:O5=(A4@A5)";
                    AOUTMUX = "O5";
                    number_of_FFs--;
                    if (which % 24 == 1) inpin = "A2";
                    else if (which % 24 == 9) inpin = "B4";
                    else if (which % 24 == 17) inpin = "C6";
                    net = net + "net \"XOR" + which + number_of_FFs + k + "\" ,\n  " +
                        "outpin \"" + "XOR" + slicename + "\" AMUX ,\n  " +
                        "inpin \"OR" + Num_Stage + location + "\" " + inpin + " ,\n;\n";
                }
                else if (which % 8 == 2)
                {
                    B6LUT = "XOR" + which + number_of_FFs + ":#LUT:O6=(A1@A3)";
                    BUSED = "0";
                    number_of_FFs--;
                    if (which % 24 == 2) inpin = "A3";
                    else if (which % 24 == 10) inpin = "B5";
                    else if (which % 24 == 18) inpin = "D1";
                    net = net + "net \"XOR" + which + number_of_FFs + k + "\" ,\n  " +
                        "outpin \"" + "XOR" + slicename + "\" B ,\n  " +
                        "inpin \"OR" + Num_Stage + location + "\" " + inpin + " ,\n;\n";
                }
                else if (which % 8 == 3)
                {
                    B5LUT = "XOR" + which + number_of_FFs + ":#LUT:O5=(A4@A5)";
                    BOUTMUX = "O5";
                    number_of_FFs--;
                    if (which % 24 == 3) inpin = "A4";
                    else if (which % 24 == 11) inpin = "B6";
                    else if (which % 24 == 19) inpin = "D2";
                    net = net + "net \"XOR" + which + number_of_FFs + k + "\" ,\n  " +
                        "outpin \"" + "XOR" + slicename + "\" BMUX ,\n  " +
                        "inpin \"OR" + Num_Stage + location + "\" " + inpin + " ,\n;\n";
                }
                else if (which % 8 == 4)
                {
                    C6LUT = "XOR" + which + number_of_FFs + ":#LUT:O6=(A1@A3)";
                    CUSED = "0";
                    number_of_FFs--;
                    if (which % 24 == 4) inpin = "A5";
                    else if (which % 24 == 12) inpin = "C1";
                    else if (which % 24 == 20) inpin = "D3";
                    net = net + "net \"XOR" + which + number_of_FFs + k + "\" ,\n  " +
                        "outpin \"" + "XOR" + slicename + "\" C ,\n  " +
                        "inpin \"OR" + Num_Stage + location + "\" " + inpin + " ,\n;\n";
                }
                else if (which % 8 == 5)
                {
                    C5LUT = "XOR" + which + number_of_FFs + ":#LUT:O5=(A4@A5)";
                    COUTMUX = "O5";
                    number_of_FFs--;
                    if (which % 24 == 5) inpin = "A6";
                    else if (which % 24 == 13) inpin = "C2";
                    else if (which % 24 == 21) inpin = "D4";
                    net = net + "net \"XOR" + which + number_of_FFs + k + "\" ,\n  " +
                        "outpin \"" + "XOR" + slicename + "\" CMUX ,\n  " +
                        "inpin \"OR" + Num_Stage + location + "\" " + inpin + " ,\n;\n";
                }
                else if (which % 8 == 6)
                {
                    D6LUT = "XOR" + which + number_of_FFs + ":#LUT:O6=(A1@A3)";
                    DUSED = "0";
                    number_of_FFs--;
                    if (which % 24 == 6) inpin = "B1";
                    else if (which % 24 == 14) inpin = "C3";
                    else if (which % 24 == 22) inpin = "D5";
                    net = net + "net \"XOR" + which + number_of_FFs + k + "\" ,\n  " +
                        "outpin \"" + "XOR" + slicename + "\" D ,\n  " +
                        "inpin \"OR" + Num_Stage + location + "\" " + inpin + " ,\n;\n";
                }
                else if (which % 8 == 7)
                {
                    D5LUT = "XOR" + which + number_of_FFs + ":#LUT:O5=(A4@A5)";
                    DOUTMUX = "O5";
                    number_of_FFs--;
                    if (which % 24 == 7) inpin = "B2";
                    else if (which % 24 == 15) inpin = "C4";
                    else if (which % 24 == 23) inpin = "D6";
                    net = net + "net \"XOR" + which + number_of_FFs + k + "\" ,\n  " +
                        "outpin \"" + "XOR" + slicename + "\" DMUX ,\n  " +
                        "inpin \"OR" + Num_Stage + location + "\" " + inpin + " ,\n;\n";
                }
                which++;
                if (number_of_FFs == 0 || which == 8 * k)
                {
                    slices = slices + "inst \"" + "XOR" + number_of_FFs + "\"" + " \"SLICEL\" ," + "placed " + "CLBLM_X" + ROW_CTRL + "Y" + COL +
                                " SLICE_X" + WOR + "Y" + COL + "  ,\n  cfg \" " +
                                " A5FFINIT::#OFF A5FFMUX::#OFF A5FFSR::#OFF A5LUT:" + A5LUT + " A6LUT:" + A6LUT + "\n" +
                                "\t\tACY0::#OFF AFF::#OFF AFFINIT::#OFF AFFMUX::#OFF AFFSR::#OFF\n" +
                                "\t\tAOUTMUX::" + AOUTMUX + " AUSED::" + AUSED + " B5FFINIT::#OFF B5FFMUX::#OFF B5FFSR::#OFF\n" +
                                "\t\tB5LUT:" + B5LUT + " B6LUT:" + B6LUT + " BCY0::#OFF\n" +
                                "\t\tBFF::#OFF BFFINIT::#OFF BFFMUX::#OFF BFFSR::#OFF BOUTMUX::" + BOUTMUX + "\n" +
                                "\t\tBUSED::" + BUSED + " C5FFINIT::#OFF C5FFMUX::#OFF C5FFSR::#OFF C5LUT:" + C5LUT + "\n" +
                                "\t\tC6LUT:" + C6LUT + " CCY0::#OFF CEUSEDMUX::#OFF CFF::#OFF CFFINIT::#OFF CFFMUX::#OFF\n" +
                                "\t\tCFFSR::#OFF CLKINV::#OFF COUTMUX::" + COUTMUX + " COUTUSED::#OFF CUSED::" + CUSED + "\n" +
                                "\t\tD5FFINIT::#OFF D5FFMUX::#OFF D5FFSR::#OFF D5LUT:" + D5LUT + " D6LUT:" + D6LUT + "\n" +
                                "\t\tDCY0::#OFF DFF::#OFF DFFINIT::#OFF DFFMUX::#OFF DFFSR::#OFF DOUTMUX::" + DOUTMUX + "\n" +
                                "\t\tDUSED::" + DUSED + " PRECYINIT::#OFF SRUSEDMUX::#OFF SYNC_ATTR::#OFF\" \n   ;\n";
                    k++;
                    if (WOR % 2 == 0) WOR++;
                    else 
                    { 
                        WOR--; COL--;
                        if (COL < 0) COL = 239;
                    }
                    A6LUT = ":#OFF"; A5LUT = ":#OFF";
                    B6LUT = ":#OFF"; B5LUT = ":#OFF";
                    C6LUT = ":#OFF"; C5LUT = ":#OFF";
                    D6LUT = ":#OFF"; D5LUT = ":#OFF";
                    AUSED = "#OFF"; AOUTMUX = "#OFF";
                    BUSED = "#OFF"; BOUTMUX = "#OFF";
                    CUSED = "#OFF"; COUTMUX = "#OFF";
                    DUSED = "#OFF"; DOUTMUX = "#OFF";
                    if (number_of_FFs - 8 > 0)
                        slicename = (number_of_FFs - 8).ToString();
                    else slicename = "0";
                }
                if (number_of_FFs == 0)
                {
                    Circuit = Circuit.Insert(Circuit.IndexOf("inst \"XDL_"), slices);
                    Circuit = Circuit.Insert(Circuit.IndexOf("net \"GLOBAL_LOGIC0_0") - 1, net);
                }
            }
        }
        private void Do_input_XOR_Routing(float number_of_FFs, ref string Rd_for_Coun)
        {
            string Rout = "";
            string inputpin = "";
            string pinname = ""; string pinname_D = "";
            string slicename = "";
            int num = 1;
            int internal_num = 0;
            int index = 0;
            int index_xor = 0;
            int tmp_num_FF = 0;
            Stream Routing;
            Routing = File.OpenRead(@"F:\MS\FinalProject\CODE\Duplicated_Routing.XDL");
            StreamReader XDL_Routing = new StreamReader(Routing);

            tmp_num_FF = (int) number_of_FFs;
            Rout = Rd_for_Coun;
            while (num <= number_of_FFs)
            {
                slicename = Rd_for_Coun.Substring(Rd_for_Coun.IndexOf("\"XOR",index_xor) + 1, Rd_for_Coun.IndexOf("\"", Rd_for_Coun.IndexOf("\"XOR",index_xor) + 1) - Rd_for_Coun.IndexOf("\"XOR",index_xor) - 1);

                while (internal_num < 8 & num <= number_of_FFs)
                {
                    if (internal_num % 8 == 0) { pinname = "A1"; pinname_D = "A3"; }
                    else if (internal_num % 8 == 1) { pinname = "A4"; pinname_D = "A5"; }
                    else if (internal_num % 8 == 2) { pinname = "B1"; pinname_D = "B3"; }
                    else if (internal_num % 8 == 3) { pinname = "B4"; pinname_D = "B5"; }
                    else if (internal_num % 8 == 4) { pinname = "C1"; pinname_D = "C3"; }
                    else if (internal_num % 8 == 5) { pinname = "C4"; pinname_D = "C5"; }
                    else if (internal_num % 8 == 6) { pinname = "D1"; pinname_D = "D3"; }
                    else if (internal_num % 8 == 7) { pinname = "D4"; pinname_D = "D5"; }
                    
                    index = Rd_for_Coun.IndexOf("History_" + num);
                    index = Rd_for_Coun.IndexOf("pip", index);
                    inputpin = "  inpin \"" + slicename + "\" " + pinname + " ,\n";
                    Rd_for_Coun = Rd_for_Coun.Insert(index - 2, inputpin);

                    index = Rd_for_Coun.IndexOf("History_" + num +"_D");
                    index = Rd_for_Coun.IndexOf("pip", index);
                    inputpin = "  inpin \"" + slicename + "\" " + pinname_D + " ,\n";
                    Rd_for_Coun = Rd_for_Coun.Insert(index - 2, inputpin );

                    internal_num++;
                    num++;
                }
                internal_num = 0;
                index_xor = Rd_for_Coun.IndexOf("\"XOR", index_xor) + 4;
            }            
        }
    }
}
