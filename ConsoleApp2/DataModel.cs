using static System.Double;

namespace ConsoleApp2
{
    public class DataModel
    {
        public double w11, w12, w21, w22, v11, v12, v13, v21, v22, v23, w1, w2, w3, e;
        public string publickey;
        
        public DataModel(double d, double d1, double d2, double d3, double d4, double d5, double d6, double d7, double d8, double d9, double d10, double d11, double d12)
        {
            w11 = d;
            w12 =d1;
            w21 =d2;
            w22 =d3;
            v11 =d4;
            v12 =d5;
            v13 =d6;
            v21 =d7;
            v22 =d8;
            v23 =d9;
            w1 = d10;
            w2 = d11;
            w3 = d12;
        }
    }
}