// This file was auto-generated by ML.NET Model Builder. 

using Microsoft.ML.Data;

namespace ConsoleApp1ML.Model
{
    public class ModelInput
    {
        [ColumnName("review"), LoadColumn(0)]
        public string Review { get; set; }


        [ColumnName("rating"), LoadColumn(1)]
        public string Rating { get; set; }


    }
}