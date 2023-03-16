using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FPGA_based_FT_MICRO
{
    class Second_change
    {
        internal void main()
        {
            string line = "";
            // string DUP_line = "";
            string ROUTING = "";

            /*      /////Then we reread the final file and duplicate the "INST"(instances) based on the border and new name with postfix of "_D"
                  Stream Duplication_2;
                  Duplication_2 = File.OpenRead(@"F:\MS\FinalProject\ICCAD12\SELECTEDBENCHMARKs\s953\FT\Syn_A\Final.XDL");
                  StreamReader XDL_DUP_2 = new StreamReader(Duplication_2);

                  Stream Duplication_3;
                  Duplication_3 = File.OpenWrite(@"F:\MS\FinalProject\ICCAD12\SELECTEDBENCHMARKs\s953\FT\Syn_A\Final_DUP.XDL");
                  StreamWriter XDL_DUP_3 = new StreamWriter(Duplication_3);

                  while (XDL_DUP_2.EndOfStream == false)
                  {
                      line = XDL_DUP_2.ReadLine();
                      if (line.IndexOf("WOR") != -1)
                      {
                          XDL_DUP_3.WriteLine(line);
                          line = line.Replace("WOR" + line.Substring(line.IndexOf("WOR") + 3, line.IndexOf(" ", line.IndexOf("WOR")) - line.IndexOf("WOR") - 3),
                                     "WOR" + (Int16.Parse(line.Substring(line.IndexOf("WOR") + 3, line.IndexOf(" ", line.IndexOf("WOR")) - line.IndexOf("WOR") - 3)) + DELTA_WOR).ToString());
                          line = line.Replace("ROW" + line.Substring(line.IndexOf("ROW") + 3, line.IndexOf(" ", line.IndexOf("ROW")) - line.IndexOf("ROW") - 3),
                                     "ROW" + (Int16.Parse(line.Substring(line.IndexOf("ROW") + 3, line.IndexOf(" ", line.IndexOf("ROW")) - line.IndexOf("ROW") - 3)) + DELTA_ROW).ToString());
                          line = line.Replace(")", "_D)");
                          DUP_line = line;
                          XDL_DUP_3.WriteLine(DUP_line);
                      }
                  }
                  XDL_DUP_2.Close();
                  Duplication_2.Close();
                  XDL_DUP_3.Close();
                  Duplication_3.Close();*/

            ///////In this part the Duplication of the "NET"s will be done
            Stream Duplication_4;
            Duplication_4 = File.OpenRead(@"F:\MS\FinalProject\ICCAD12\SELECTEDBENCHMARKs\s953\FT\Syn_A\new_Routing.XDL");
            StreamReader XDL_DUP_4 = new StreamReader(Duplication_4);

            Stream Duplication_5;
            Duplication_5 = File.OpenWrite(@"F:\MS\FinalProject\ICCAD12\SELECTEDBENCHMARKs\s953\FT\Syn_A\Duplicated_Routing.XDL");
            StreamWriter XDL_DUP_5 = new StreamWriter(Duplication_5);

            ROUTING = XDL_DUP_4.ReadToEnd();
            XDL_DUP_5.Write(ROUTING);
            ROUTING = ROUTING.Replace("\" ", "_D\" ");
            XDL_DUP_5.Write(ROUTING);

            XDL_DUP_4.Close();
            Duplication_4.Close();
            //////Duplicate output pins
            string output_nets = "";
            Stream Duplication_7;
            Duplication_7 = File.OpenRead(@"F:\MS\FinalProject\ICCAD12\SELECTEDBENCHMARKs\s953\FT\Syn_A\Tmps953NETO.XDL");
            StreamReader XDL_DUP_7 = new StreamReader(Duplication_7);
            output_nets = XDL_DUP_7.ReadToEnd().ToString();
            output_nets = output_nets.Replace("\" ", "_D\" ");

            /////////Duplicate inside the input pins
            Stream Duplication_8;
            Duplication_8 = File.OpenRead(@"F:\MS\FinalProject\ICCAD12\SELECTEDBENCHMARKs\s953\FT\Syn_A\Tmps953NETI.XDL");
            StreamReader XDL_DUP_8 = new StreamReader(Duplication_8);

            Stream Duplication_9;
            Duplication_9 = File.OpenWrite(@"F:\MS\FinalProject\ICCAD12\SELECTEDBENCHMARKs\s953\FT\Syn_A\Tmps953NETID.XDL");
            StreamWriter XDL_DUP_9 = new StreamWriter(Duplication_9);

            string tmp_line = "";
            string TOTLines = "";
            string TOTLines_DUP = "";

            while (XDL_DUP_8.EndOfStream == false)
            {
                line = XDL_DUP_8.ReadLine().ToString();
                if (line.IndexOf(";") == -1)
                    TOTLines = TOTLines + "\n" + line;
                if (line.IndexOf("inpin") != -1)
                {
                    tmp_line = line.Replace("\" ", "_D\" ");
                    TOTLines_DUP = TOTLines_DUP + "\n" + tmp_line;
                }
                if (line.IndexOf(";") != -1)
                {
                    XDL_DUP_9.Write(TOTLines);
                    XDL_DUP_9.Write(TOTLines_DUP);
                    XDL_DUP_9.Write("\n  ;");
                    TOTLines = "";
                    TOTLines_DUP = "";
                }
            }

            XDL_DUP_5.Write(output_nets);
            //      XDL_DUP_5.Write(Regular_nets);

            XDL_DUP_9.Close();
            Duplication_9.Close();
            XDL_DUP_5.Close();
            Duplication_5.Close();
        }
    }
}
