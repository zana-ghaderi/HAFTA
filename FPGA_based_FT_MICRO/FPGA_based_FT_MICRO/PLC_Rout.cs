using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FPGA_based_FT_MICRO
{
     public class PLC_Rout
    {
         static public int His_NUM = 0;
         static public int U = 0;
         public void main()
         {
             Stream read;
             Stream read_slice;
             read = File.OpenRead(@"F:\MS\FinalProject\CODE\FCLBRouting.XDL");
             read_slice = File.OpenRead(@"F:\MS\FinalProject\CODE\FCLBRouting.XDL");
             StreamReader XDLmux = new StreamReader(read);
             StreamReader XDLslice = new StreamReader(read_slice);

             Stream write;
             write = File.OpenWrite(@"F:\MS\FinalProject\CODE\TMP.XDL");
             StreamWriter XDLwrite = new StreamWriter(write);
             
             string whole = "";
             whole = XDLmux.ReadToEnd().ToString();

             string Slice = "";
             string Bro = "";
             int Number_of_Mux = 0;
             int Number_of_FF = 0;
             int length_of_file = 0;

             Slice = XDLslice.ReadLine().ToString();
        //     Bro = whole.Substring(whole.IndexOf(Slice) + Slice.Length + 2, whole.IndexOf("CT", whole.IndexOf(Slice) + Slice.Length + 3) - whole.IndexOf(Slice) - Slice.Length - 2);
             length_of_file = whole.Length - 40;
             int index_backslash = 0;
             while (XDLslice.EndOfStream == false )
             {                 
                     if (Slice.IndexOf("A6LUT") == -1 & Slice.IndexOf("A5LUT") == -1 & Slice.IndexOf("AOUTMUX") == -1) Number_of_Mux++;
                     if (Slice.IndexOf("B6LUT") == -1 & Slice.IndexOf("B5LUT") == -1 & Slice.IndexOf("BOUTMUX") == -1) Number_of_Mux++;
                     if (Slice.IndexOf("C6LUT") == -1 & Slice.IndexOf("C5LUT") == -1 & Slice.IndexOf("COUTMUX") == -1) Number_of_Mux++;
                     if (Slice.IndexOf("D6LUT") == -1 & Slice.IndexOf("D5LUT") == -1 & Slice.IndexOf("DOUTMUX") == -1) Number_of_Mux++;
                     if (Slice.IndexOf("*") == -1)
                     {   
                         if (Slice.IndexOf("ROW1 COL72") != -1)
                             Console.Write("awa");
                         for (int i = 0; i < Slice.Length; i++) if (Slice.IndexOf("H", i, 1) != -1) Number_of_FF++;
                         ///////Add number of ff from the * slices
                         Bro = whole.Substring(whole.IndexOf(Slice) + Slice.Length + 2, whole.IndexOf(";", whole.IndexOf(Slice) + Slice.Length ) - whole.IndexOf(Slice) - Slice.Length - 1);
                         index_backslash = Bro.IndexOf("\n");
                         if(Bro.IndexOf("*") != -1)
                         for (int j = 0; j <index_backslash; j++) if (Bro.IndexOf("H", j,1) != -1) Number_of_FF++;
                         if (Number_of_FF > 8)
                             Console.Write("aha\n");
                     }
                     XDLwrite.WriteLine(Slice + " {NuMUX<" + Number_of_Mux + "} " + "{NuFF<" + Number_of_FF + "}");
                     //FFTOt = FFTOt + Number_of_Mux;
                     Number_of_Mux = 0;
                     Number_of_FF = 0;
                     Slice = XDLslice.ReadLine().ToString();                        
                     
             }
             //Console.Write(FFTOt);
             XDLwrite.Close();
             write.Close();


             ///////////////////////////////////////////Search For MUX
             Stream MUX;
             MUX = File.OpenRead(@"F:\MS\FinalProject\CODE\TMP.XDL");
             StreamReader XDL_LK_MUX = new StreamReader(MUX);
             multiplexer mulplx = new multiplexer();

             Stream NewHistory;
             NewHistory = File.OpenWrite(@"F:\MS\FinalProject\CODE\new_Routing.XDL");
             StreamWriter XDL_new_history = new StreamWriter(NewHistory);

             Stream History;
             History = File.OpenRead(@"F:\MS\FinalProject\CODE\Routing.XDL");
             StreamReader RHis = new StreamReader(History);

             Stream Addmux;
             Addmux = File.OpenWrite(@"F:\MS\FinalProject\CODE\Mux_adding.XDL");
             StreamWriter XDL_MUX = new StreamWriter(Addmux);

             string ALL = "";
             long Number_of_lines = 0;
             ALL = RHis.ReadToEnd().ToString();
             while (XDL_LK_MUX.EndOfStream == false)
             {
                 string Aline = XDL_LK_MUX.ReadLine();
                 mulplx.search_for_mux(ref Aline, XDL_new_history,ref ALL,ref His_NUM);
                 Number_of_lines++;
                 XDL_MUX.WriteLine(Aline);
             }
             XDL_MUX.Close();
             Addmux.Close();
             ////////////////////////////Search for MUXes that are not available in our region
             Stream MUX2;
             Stream MUX2_t;
             MUX2 = File.OpenRead(@"F:\MS\FinalProject\CODE\Mux_adding.XDL");
             MUX2_t = File.OpenRead(@"F:\MS\FinalProject\CODE\Mux_adding.XDL");
             StreamReader XDL_MUX_Bro = new StreamReader(MUX2);
             StreamReader XDL_MUX_Bro_tot = new StreamReader(MUX2_t);

             Stream Addmux2;
             Addmux2 = File.OpenWrite(@"F:\MS\FinalProject\CODE\Mux_adding_2.XDL");
             StreamWriter XDL_MUX2 = new StreamWriter(Addmux2);

             addvanced_multiplexer adv_mux = new addvanced_multiplexer();
             
             string[] XDL_mux_tmp = new string[Number_of_lines + 50];
           //  int[] size = new int[Number_of_lines + 50];
             int t = 0;
             for (t = 0; t < Number_of_lines; t++)
             {
                 XDL_mux_tmp[t] = XDL_MUX_Bro_tot.ReadLine();
                 //size[t] = XDL_mux_tmp[t].Length + 2;                
             }

           //  XDL_mux_tmp[t] = "NULL"; size[t] = 6;

             adv_mux.search_mux2(ref XDL_mux_tmp, Number_of_lines ,ref ALL,ref His_NUM,ref U);
             for (t = 0; t < Number_of_lines + 50 ; t++)
             {
                 if(XDL_mux_tmp[t] != null)
                 XDL_MUX2.WriteLine(XDL_mux_tmp[t]);
             }
             XDL_MUX2.Close();
             Addmux2.Close();

             //////Search for B and BB
             Stream Addmux3;
             Stream Addmux4;
             Addmux3 = File.OpenRead(@"F:\MS\FinalProject\CODE\Mux_adding_2.XDL");
             Addmux4 = File.OpenRead(@"F:\MS\FinalProject\CODE\Mux_adding_2.XDL");
             StreamReader XDL_MUX3 = new StreamReader(Addmux3);
             StreamReader XDL_MUX4 = new StreamReader(Addmux4);

             Number_of_lines = 0;
             while (XDL_MUX3.EndOfStream == false)
             {
                 XDL_MUX3.ReadLine();
                 Number_of_lines++;
             }
             XDL_MUX3.Close();
             Addmux3.Close();
             string[] XDL_mux_tmp2 = new string[Number_of_lines + 50];
            // int[] size2 = new int[Number_of_lines];
             for (t = 0; t < Number_of_lines; t++)
             {
                 XDL_mux_tmp2[t] = XDL_MUX4.ReadLine();
               //  size2[t] = XDL_mux_tmp2[t].Length;
             }

             Bmultiplexers bMUX = new Bmultiplexers();
             bMUX.Search_multiplesers(ref XDL_mux_tmp2, Number_of_lines, ref ALL, ref His_NUM, ref U);

             Stream Addmux5;
             Addmux5 = File.OpenWrite(@"F:\MS\FinalProject\CODE\Mux_adding_3.XDL");
             StreamWriter XDL_MUX5 = new StreamWriter(Addmux5);

             for (t = 0; t < Number_of_lines + 50; t++)
             {
                 XDL_MUX5.WriteLine(XDL_mux_tmp2[t]);
             }
             XDL_MUX5.Close();
             Addmux5.Close();

             ////////////////////////finding muxes for bb 

             Stream Addmux6;
             Addmux6 = File.OpenRead(@"F:\MS\FinalProject\CODE\Mux_adding_3.XDL");
             StreamReader XDL_MUX6 = new StreamReader(Addmux6);
             Stream Addmux7;
             Addmux7 = File.OpenRead(@"F:\MS\FinalProject\CODE\Mux_adding_3.XDL");
             StreamReader XDL_MUX7 = new StreamReader(Addmux7);

             Number_of_lines = 0;
             while (XDL_MUX6.EndOfStream == false)
             {
                 XDL_MUX6.ReadLine();
                 Number_of_lines++;
             }
             XDL_MUX6.Close();
             Addmux6.Close();

             string[] XDL_mux_tmp3 = new string[Number_of_lines + 50];
        //     int[] size3 = new int[Number_of_lines];
             for (t = 0; t < Number_of_lines; t++)
             {
                 XDL_mux_tmp3[t] = XDL_MUX7.ReadLine();
              //   size3[t] = XDL_mux_tmp3[t].Length;
             }
             BBmuxes Bbmuxes1 = new BBmuxes();

             Bbmuxes1.search_for_F_muxes(ref His_NUM, ref XDL_mux_tmp3, ref ALL, ref Number_of_lines);

             Stream Final;
             Final = File.OpenWrite(@"F:\MS\FinalProject\CODE\Final.XDL");
             StreamWriter XDL_MUX8 = new StreamWriter(Final);

             for (t = 0; t < Number_of_lines + 50; t++)
             {
                 XDL_MUX8.WriteLine(XDL_mux_tmp3[t]);
             }
                          
             XDL_new_history.Write(ALL);
             XDL_new_history.Close();
             XDL_MUX8.Close();
             Final.Close();

             Console.Write("Finito");
         }
    }
}
