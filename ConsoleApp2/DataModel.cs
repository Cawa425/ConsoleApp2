using static System.Double;

namespace ConsoleApp2
{
    public class DataModel
    {
        public double w11;
        public double w12;
        public double w21;
        public double w22;
        public double v11;
        public double v12;
        public double v13;
        public double v21;
        public double v22;
        public double v23;
        public double w1 ;
        public double w2 ;
        public double w3 ;
        public double e;
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