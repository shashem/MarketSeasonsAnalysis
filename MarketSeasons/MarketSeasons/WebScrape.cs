using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using System.Data;
using System.Net;
using System.IO;

namespace MarketSeasons
{
    public class WebScrape
    {
        public DataTable ReadWeb(string Symbol)
        {
            var Results = InitDataTableResults(); //Create DataTable for easier processing.
            var url = "https://www.google.com/finance/historical?q=|symbol|&startdate=Jan+01%2C+1900&output=csv"; //Saved this url string Will move to web.Config at end of project
            using (var Client = new WebClient())
            {
                using (var Stream = Client.OpenRead(url.Replace("|symbol|", Symbol)))
                {
                    using (var sr = new StreamReader(Stream))
                    {
                        var Response = sr.ReadToEnd().Split('\n'); //Separating each row from csv format string
                        AddRowsToResults(Results, Response);
                    }
                }
            }
            return Results;
        }

        private static void AddRowsToResults(DataTable Results, string[] Response)
        {
            foreach (string Row in Response)
            {
                var t = Row.Split(','); //Output is a csv format
                if (t.Length == 6 && t[0] != "Date") //Make Sure not to add the header
                {
                    Results.Rows.Add(t[0], t[1], t[2], t[3], t[4], t[5]); //Add each row of the result to the DataTable for easier processing
                }
            }
        }

        private static DataTable InitDataTableResults()
        {
            var Results = new DataTable();
            Results.Columns.Add("Date");
            Results.Columns.Add("Open");
            Results.Columns.Add("High");
            Results.Columns.Add("Low");
            Results.Columns.Add("Close");
            Results.Columns.Add("Volume");
            return Results;
        }
    }
}