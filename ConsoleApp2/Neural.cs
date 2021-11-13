using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace ConsoleApp2
{
    public class Neural
    {
        private double w11;
        private double w12;
        private double w21, w22, v11, v12, v13, v21, v22, v23, w1, w2, w3, e;

        private readonly List<double> X1 = new();
        private readonly List<double> X2 = new();
        private readonly List<double> testY = new();

        private static readonly Random rng = new();

        private static double f(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        private double GetE()
        {
            double res = 0;
            for (var i = 0; i < X1.Count; i++)
            {
                var yt = g(X1[i], X2[i]);
                res += (yt - testY[i]) * (yt - testY[i]);
            }

            return res;
        }

        private double g(double x1, double x2)
        {
            var h11 = f(x1 * w11 + x2 * w21);
            var h12 = f(x1 * w12 + x2 * w22);
            return f(f(h11 * v11 + h12 * v21) * w1 + f(h11 * v12 + h12 * v22) * w2 + f(h11 * v13 + h12 * v23) *w3);
        }

        public DataModel Start(JToken? jToken)
        {
            using var parser = new TextFieldParser("../../../test_data_100.csv");
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            while (!parser.EndOfData)
            {
                //Process row
                var fields = parser.ReadFields();
                foreach (var field in fields)
                {
                    var splitted = field.Split(';');
                    var x1 = double.Parse(splitted[0].Replace('.', ','));
                    var x2 = double.Parse(splitted[1].Replace('.', ','));
                    var y = double.Parse(splitted[2].Replace('.', ','));

                    X1.Add(x1);
                    X2.Add(x2);
                    testY.Add(y);
                }
            }

            return Calculate();
        }

        private DataModel Calculate()
        {
            while (true)
            {
                w11 = rng.NextDouble();
                w12 = rng.NextDouble();
                w21 = rng.NextDouble();
                w22 = rng.NextDouble();
                v11 = rng.NextDouble();
                v12 = rng.NextDouble();
                v13 = rng.NextDouble();
                v21 = rng.NextDouble();
                v22 = rng.NextDouble();
                v23 = rng.NextDouble();
                w1 = rng.NextDouble();
                w2 = rng.NextDouble();
                w3 = rng.NextDouble();

                e = GetE();
                if (e is < 1 and > 0) break;
            }
            
            var dm = new DataModel(w11, w12, w21, w22, v11, v12, v13, v21, v22, v23, w1, w2, w3)
            {
                e = e,
                publickey = Program.Get("http://188.93.211.195/public")
            };
            return dm;
        }
    }
}