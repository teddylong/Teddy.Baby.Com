using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.Coverage.Analysis;

namespace TestingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            CoverageInfo ci = CoverageInfo.CreateFromFile(@"d:\Users\yqlong\Desktop\output.coverage");
            CoverageDS data = ci.BuildDataSet(null);
            string outputName = DateTime.Now.ToString("yyyyMMddHHmm") + ".xml";
            data.ExportXml(outputName);
        }
    }
}
