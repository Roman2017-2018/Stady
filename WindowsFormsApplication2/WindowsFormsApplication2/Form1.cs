using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO.Ports;
namespace WindowsFormsApplication2
{
    using System.Globalization;
    using System.Runtime.Serialization.Formatters;

    public struct ComandsAndValues
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public partial class Form1 : Form
{
public Form1()
{
    InitializeComponent();
}  public int i,s = 0;
int z = 0; int z1 = 1; int z2 = 2; private static string RxString = "";int ppc2 = 0;int ppc1 = 0;int ppc = 0;
private void button1_Click(object sender, EventArgs e)
{

    OpenFileDialog op = new OpenFileDialog();
    op.InitialDirectory = "С:\\";
    op.Filter = "Txt files (*.txt)|*.txt|All Fiels(*.*)|*.*";
    op.FilterIndex = 2;
    op.RestoreDirectory = true;
    if (op.ShowDialog() == DialogResult.OK)
        richTextBox1.Text = File.ReadAllText(op.FileName);
    List<string> data = new List<string>();
    foreach (var lineOfData in richTextBox1.Text.Split('\n'))
    {
        data.Add(lineOfData);
    }
 
    for (int i = 0; i < data.Count; ++i) {

        string result = "";
        string[] value = ParseNumber(data[i]).Split('\n');
        if (value[0] == "") {
        }
        else {
               if (value.Length >= 3)
            {
                result += "Command \"" + value[0][0] + "\" Value = " + value[0].Substring(1, value[0].Length - 1) + " ";
                result += "Command\"" + value[1][0] + "\" Value = " + value[1].Substring(1, value[1].Length -1) + "\n";
            }
            else
            {
                result += "Command \"" + value[0][0] + "\" Value = " + value[0].Substring(1, value[0].Length -1) + "\n";
            }
            richTextBox3.AppendText(result); 
            }
        }        
}

public string ParseNumber( string source)
{
    string temp = "";
    int ii = 0;
    for (int i = 0; i < source.Length; ++i) {
    switch (source[i])
    {
        case 'X':
            int countOfNumber = i+1;
            bool flag = false;
            for (int count = 0; count < 100; count++) {
                if (countOfNumber == source.Length) {
                    countOfNumber++;
                    flag = true;
                    break;
                }
                if (((source[countOfNumber] >= 48 && source[countOfNumber] <= 57) || (source[countOfNumber] == '-')
                                                                                  || (source[countOfNumber] == '.'))) {
                    countOfNumber++;
                }
            }          
            
            countOfNumber = countOfNumber - 1;
            char[] array;
            if (flag)
            {
                array = new char[(countOfNumber - i) + 1];
            }
            else
            {
                array = new char[(countOfNumber - i) + 2];
            }            
            for (int e = 0; e < (countOfNumber - i) + 1; e++) {
                if(flag&& i + e == source.Length)
                    break;
                array[e] = source[i+e];
            }

            if (flag) {
                array[(countOfNumber - i)] = '\n';
            }
            else {
                array[(countOfNumber - i) + 1] = '\n';
            }
            temp += new string(array);
            i = countOfNumber;
            break;
        case 'Y':
            int countOfNumberY = i + 1;
            bool flagY = false;
            for (int count = 0; count < 100; count++)
            {
                if (countOfNumberY == source.Length)
                {
                    countOfNumberY++;
                        flagY = true;
                    break;
                }
                if (((source[countOfNumberY] >= 48 && source[countOfNumberY] <= 57) || (source[countOfNumberY] == '-')
                                                                                  || (source[countOfNumberY] == '.')))
                {
                    countOfNumberY++;
                }
            }

            countOfNumber = countOfNumberY - 1;
            char[] arrayY;
            if (flagY)
            {
                arrayY = new char[(countOfNumberY - i) + 1];
            }
            else
            {
                arrayY = new char[(countOfNumberY - i) + 2];
            }
            for (int e = 0; e < (countOfNumberY - i) + 1; e++)
            {
                if (flagY && i + e == source.Length) 
                    break;
                
                arrayY[e] = source[i + e];
            }

            if (flagY)
            {
                arrayY[(countOfNumberY - i)] = '\n';
            }
            else
            {
                arrayY[(countOfNumberY - i) ] = '\n';
            }
            temp += new string(arrayY);
            i = countOfNumberY;
                        break;
            }
    }

    if (temp.Length < 10)
        return temp;
    else {
        return temp.Remove(temp.Length - 1);
    }
}

        public void button2_Click(object sender, EventArgs e)
        { double @string;
            Regex regex = new Regex("^[xyzfg](?<Value1>[+-]?\\d+.\\d+)[xyzfg]?(?<Value2>[+-]?\\d+.\\d+)?", 
                RegexOptions.Multiline | RegexOptions.IgnoreCase);
           List<ComandsAndValues> single = new List<ComandsAndValues>();
           List<ComandsAndValues> @double = new List<ComandsAndValues>();
           
            Regex checkRegex = new Regex("^[xy](?<V1>[+-]?\\d+.\\d+)[xy](?<Va>[+-]?\\d+.\\d+)", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            Regex checkRegexsingl = new Regex("^[xy](?<V1>[+-]?\\d+.\\d+$)", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            int i3 = 0;
            for (Match m1 = regex.Match(richTextBox1.Text); m1.Success; m1 = m1.NextMatch())
            {
                
                if (checkRegex.IsMatch(m1.Value)) {
                    double Xdouble = double.Parse(
                        m1.Groups["Value1"].ToString(),
                        NumberStyles.Float,
                        CultureInfo.CreateSpecificCulture("en-US"));
                    //double Ydouble = double.Parse(
                    //m1.Groups["Value2"].ToString(),
                    //NumberStyles.Float,
                    //CultureInfo.CreateSpecificCulture("en-US"));
                    if (m1.Value.ToUpper()[0] == 'X')
                        @double.Add(
                            new ComandsAndValues()
                                {
                                    X = double.Parse(
                                        m1.Groups["Value1"].ToString(),
                                        NumberStyles.Float,
                                        CultureInfo.CreateSpecificCulture("en-US")),
                                    Y = double.Parse(
                                        m1.Groups["Value2"].ToString(),
                                        NumberStyles.Float,
                                        CultureInfo.CreateSpecificCulture("en-US"))
                                });
                    i3++;

                    //m1.Groups["Value1"].ToString() + m1.Groups["Value2"].ToString() +"\n")};
                    //    else {
                    //        @double.Add(m1.Groups["Value2"].ToString() + m1.Groups["Value1"].ToString() + "\n");
                    //    }
                }
                if (checkRegexsingl.IsMatch(m1.Value)) {
                    //double Xsingl = double.Parse(
                    //m1.Groups["Value1"].ToString(),
                    //NumberStyles.Float,
                    //CultureInfo.CreateSpecificCulture("en-US"));
                    
                    @double.Add( 
                    
                        new ComandsAndValues()
                            {
                                X = double.Parse(
                                    m1.Groups["Value1"].ToString(),
                                    NumberStyles.Float,
                                    CultureInfo.CreateSpecificCulture("en-US")),
                               // Y =0
                                Y = @double[i3-1].Y
                        });
                    i3++;
                }

                
            }

            Match m = regex.Match(richTextBox1.Text);
            string temp = m.Groups["Value1"].ToString();
            temp = Regex.Replace(temp, @"\.", ",");
            @string = double.Parse(m.Groups["Value1"].ToString(), NumberStyles.Float, CultureInfo.CreateSpecificCulture("en-US"));
            string X = ""; string Y = ""; string Z = ""; string F = "";
            while (m.Success)
            //for (int i = 0; i<5;i++)  
            {


                if (m.Value.StartsWith("X"))
                {int i=0;; char decimalSepparator;
                    Char gavno = 'X';double testNum = 0.5;decimalSepparator = testNum.ToString()[1];
                    
                    string [] Xxs = m.Value.Split(gavno);
                    foreach (var X1 in Xxs)
                       
                       
                     
           
            
            testNum = double.Parse(Xxs[1].Replace('.', decimalSepparator).Replace(',', decimalSepparator));
                    testNum = testNum / 0.005;
                    testNum = Math.Round(testNum, 0);
            X = Convert.ToString(testNum);
                      
                    //x = Math.Round(x, 1);
                    
                    //= Convert.ToString(x);
                    m = m.NextMatch();
                }


                if (m.Value.StartsWith("Y"))
                {
                    int i = 0; char decimalSepparator1;
                     double testNum1 = 0.5; decimalSepparator1 = testNum1.ToString()[1];
                    Char gavno = 'Y';
                    
                    string[] Xxs = m.Value.Split(gavno);
                    foreach (var X1 in Xxs)
                        testNum1 = double.Parse(Xxs[1].Replace('.', decimalSepparator1).Replace(',', decimalSepparator1));
                    testNum1 = testNum1 / 0.005;
                    testNum1 = Math.Round(testNum1, 0);
                    Y = Convert.ToString(testNum1);
                    
                      
                   
                    m = m.NextMatch();
                    
                }

                if (m.Value.StartsWith("Z"))
                {
                    int i = 0; char decimalSepparator2;
                    double testNum2 = 0.5; decimalSepparator2 = testNum2.ToString()[1];
                    Char gavno = 'Z';
                   
                    string[] Xxs = m.Value.Split(gavno);
                    foreach (var X1 in Xxs)
                        testNum2 = double.Parse(Xxs[1].Replace('.', decimalSepparator2).Replace(',', decimalSepparator2));
                    testNum2 = testNum2 / 0.005;
                    testNum2 = Math.Round(testNum2, 0);
                    Z = Convert.ToString(testNum2);
                  
                    m = m.NextMatch();
                    
                }


                if (m.Value.StartsWith("F"))
                {
                    F = m.Value;
                    m = m.NextMatch();
                    
                }


                richTextBox2.AppendText(X + "\r\n" + Y + "\r\n" + Z + "\r\n" + F + "\r\n");
                string str = richTextBox2.Text;
              
                
    //StringBuilder sb = new StringBuilder(str.Length);
    //for (int i = str.Length; i-- != 0; )
    //    sb.Append(str[i]);
   
    //   richTextBox3.Text = sb.ToString();





                StreamWriter sw = new StreamWriter(@"D:\G-code.txt");
                //StreamWriter sw = new StreamWriter("C:\\Users\\Роман\\Desktop\\G-code.txt");

                sw.Close(); richTextBox2.SaveFile(@"D:\G-code.txt", RichTextBoxStreamType.PlainText);
                StreamWriter se = new StreamWriter(new FileStream(@"D:\G-code.txt", FileMode.Open, FileAccess.Write));
 
//    se.BaseStream.Seek(0, SeekOrigin.Begin);
//se.WriteLine("0"); 

//    se.BaseStream.Seek(0, SeekOrigin.Begin);
//se.WriteLine("0");
    

//    se.BaseStream.Seek(0, SeekOrigin.Begin);
// se.WriteLine("0");
   
//    se.BaseStream.Seek(0, SeekOrigin.Begin);
// se.WriteLine("0"); 
//    se.BaseStream.Seek(0, SeekOrigin.Begin);

//se.WriteLine("0");
//se.BaseStream.Seek(0, SeekOrigin.End);
//se.WriteLine("0");

//se.BaseStream.Seek(0, SeekOrigin.End);
//se.WriteLine("0");


//se.BaseStream.Seek(0, SeekOrigin.End);
//se.WriteLine("0");

//se.BaseStream.Seek(0, SeekOrigin.End);
//se.WriteLine("0");
//se.BaseStream.Seek(0, SeekOrigin.End);

//se.WriteLine("0");
                  se.Close();
                  StreamWriter s = new StreamWriter(new FileStream(@"D:\G-code.txt", mode: FileMode.Open, access: FileAccess.Write));

   //s.BaseStream.Seek(0, SeekOrigin.Begin);
   //s.WriteLine("0");

   //s.BaseStream.Seek(0, SeekOrigin.Begin);
   //s.WriteLine("0");


   //s.BaseStream.Seek(0, SeekOrigin.Begin);
   //s.WriteLine("0");

   //s.BaseStream.Seek(0, SeekOrigin.Begin);
   //s.WriteLine("0");
   s.BaseStream.Seek(0, SeekOrigin.Begin);
   s.Close();
  
   
            }
      
    }

       

        private void button3_Click(object sender, EventArgs e)
        {
            serialPort1.Open();
            byte[] ibn3 = BitConverter.GetBytes(0);
            serialPort1.Write(ibn3, 0, ibn3.Length);
            byte[] ibn4 = BitConverter.GetBytes(0);
            serialPort1.Write(ibn4, 0, ibn4.Length);
            byte[] ibn5 = BitConverter.GetBytes(0);
            serialPort1.Write(ibn5, 0, ibn5.Length);
            
                

        }

      

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            
             byte[] data = new byte[serialPort1.BytesToRead];
            serialPort1.Read(data, 0, data.Length);
            System.Threading.Thread.Sleep(10);
                // Send the one character buffer.

                //i = serialPort1.ReadByte();byte[] ibn = BitConverter.GetBytes(5);

            this.Invoke(new EventHandler(DisplayText)); 
          

        }


        private void DisplayText(object s, EventArgs e)
        {
           

          
            //timer1.Enabled = true;
            string[] xyi = File.ReadAllLines(@"D:\G-code.txt");

xyi[0] = "0";
xyi[1] = "0";
            xyi[2] = "0";
            if (z < xyi.Length)
            {
                                           
                 //Array.Reverse(xyi); 
                
                //xyi[xyi.Length-1]=String.Empty;
                //    xyi[xyi.Length-2]=String.Empty;
                //        xyi[xyi.Length-3]=String.Empty;
                //            xyi[xyi.Length-4]=String.Empty;
                //                xyi[xyi.Length-5]=String.Empty;
                //                xyi[xyi.Length - 6] = String.Empty;
                //                xyi[xyi.Length - 7] = String.Empty;
               

                //if (!(z == 0)  && Convert.ToInt32(xyi[z]) > Convert.ToInt32(xyi[z + 4]))
                //{
                //    xyi[z] = Convert.ToString(Convert.ToInt32(xyi[z]) + Convert.ToInt32(xyi[z + 4]));
                //}
                //if (!(z == 0)  && Convert.ToInt32(xyi[z]) < Convert.ToInt32(xyi[z + 4]))
                //{
                              
                                
                                    //richTextBox3.AppendText(xyi[4]);
                                    if (z + 4< xyi.Length)
                                    {
                                        if ((xyi[z] != "") && (xyi[z + 4] != ""))
                                        {
                                            
                                                ppc = (Convert.ToInt32(xyi[z + 4]) - Convert.ToInt32(xyi[z]));
                                                string x = Convert.ToString(ppc);  
                                                //richTextBox3.AppendText(xyi[z]);xyi[z] = Convert.ToString(ppc);
                                                richTextBox3.AppendText(x); z = (z + 4); byte[] ibn = BitConverter.GetBytes(ppc);

                                                serialPort1.Write(ibn, 0, ibn.Length); 
                                            } 
                                    }
                                    
  } if (z1 < xyi.Length)
            {
                 //Array.Reverse(xyi); 

                //xyi[xyi.Length-1]=String.Empty;
                //    xyi[xyi.Length-2]=String.Empty;
                //        xyi[xyi.Length-3]=String.Empty;
                //            xyi[xyi.Length-4]=String.Empty;
                //                xyi[xyi.Length-5]=String.Empty;
                //                xyi[xyi.Length - 6] = String.Empty;
                //                xyi[xyi.Length - 7] = String.Empty;


                //if (!(z == 0)  && Convert.ToInt32(xyi[z]) > Convert.ToInt32(xyi[z + 4]))
                //{
                //    xyi[z] = Convert.ToString(Convert.ToInt32(xyi[z]) + Convert.ToInt32(xyi[z + 4]));
                //}
                //if (!(z == 0)  && Convert.ToInt32(xyi[z]) < Convert.ToInt32(xyi[z + 4]))
                //{


                //richTextBox3.AppendText(xyi[4]);
                if (z1 + 4 < xyi.Length)
                {
                    if (xyi[z1] != "" && xyi[z1 + 4] != "")
                    {
                       
                            ppc1 = Convert.ToInt32(xyi[z1 + 4]) - Convert.ToInt32(xyi[z1]);
                            xyi[z1] = Convert.ToString(ppc1); string y = Convert.ToString(ppc1); z1 = (z1 + 4);

                            richTextBox3.AppendText(y);
                            byte[] ibn1 = BitConverter.GetBytes(ppc1);

                            serialPort1.Write(ibn1, 0, ibn1.Length); 
                        //System.Threading.Thread.Sleep(50);
                    }
                }

            }
            if (z2 < xyi.Length)
            {
                 //Array.Reverse(xyi); 

                //xyi[xyi.Length-1]=String.Empty;
                //    xyi[xyi.Length-2]=String.Empty;
                //        xyi[xyi.Length-3]=String.Empty;
                //            xyi[xyi.Length-4]=String.Empty;
                //                xyi[xyi.Length-5]=String.Empty;
                //                xyi[xyi.Length - 6] = String.Empty;
                //                xyi[xyi.Length - 7] = String.Empty;


                //if (!(z == 0)  && Convert.ToInt32(xyi[z]) > Convert.ToInt32(xyi[z + 4]))
                //{
                //    xyi[z] = Convert.ToString(Convert.ToInt32(xyi[z]) + Convert.ToInt32(xyi[z + 4]));
                //}
                //if (!(z == 0)  && Convert.ToInt32(xyi[z]) < Convert.ToInt32(xyi[z + 4]))
                //{


                //richTextBox3.AppendText(xyi[4]);
                if (z2 + 4 < xyi.Length)
                {
                    if (xyi[z2] != "" && xyi[z2 + 4] != "")
                    {
                        ppc2 = Convert.ToInt32(xyi[z2 + 4]) - Convert.ToInt32(xyi[z2]);
                        xyi[z2] = Convert.ToString(ppc2);  string Zz = Convert.ToString(ppc2); z2 = (z2 + 4);
                        richTextBox3.AppendText(Zz);
                        byte[] ibn2 = BitConverter.GetBytes(ppc2);

                        serialPort1.Write(ibn2, 0, ibn2.Length); 
                        //serialPort1.Write(Zz); 
            //richTextBox3.AppendText(xyi[z2]);
                    }
                }

            }
              
   

            
            
            
            
            
            richTextBox1.AppendText(Convert.ToString(i));
        
        }

        
    }
}
