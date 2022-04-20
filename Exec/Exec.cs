using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using System.IO;
using ConsoleApp1ML.Model;
using ConsoleApp1ML.ConsoleApp;
namespace WebApplication.Exec
{
    public class Exec
    {
        public static string FilmName { get; set; }
        static List<string> Norm(List<string> comments)
        {
            List<string> all_str = new List<string>();
            foreach (string str in comments)
            {
                string new_str = Regex.Replace(str, "[^а-яА-Я;0-9 a-zA-Z.]", "");
                all_str.Add(new_str.ToLower());
            }
            return all_str;
        }
        public static List<string> GetScore(string FilmName)
        {
            List<string> comments = KinopoiskParser.Program.ParserExec(FilmName, false);
            List<string> scores = new List<string>();
            List<string> comments_new = Norm(comments);
            List<string> positive = new List<string>();
            List<string> negative = new List<string>();
            List<string> alldata = new List<string>();
            foreach (string comment in comments_new)
            {
                ModelInput input = new ModelInput()
                {
                    Review = comment
                };
                ModelOutput result = ConsumeModel.Predict(input);
                scores.Add(result.Prediction);
                if (result.Prediction == "0")
                    negative.Add(result.Prediction);
                else
                    positive.Add(result.Prediction);
            }

            double last_score = 0;
            foreach (string score in scores)
                last_score += Convert.ToInt32(score);

            last_score /= scores.Count;
            last_score *= 10;
/*            Console.WriteLine("Полученная оценка фильма: " + Math.Round(last_score, 1));
            Console.WriteLine("Всего отзывов: " + scores.Count);
            Console.WriteLine("Количество положительно определенных отзывов: " + positive.Count);
            Console.WriteLine("Количество негативно определенных отзывов: " + negative.Count);*/
            alldata.Add(Math.Round(last_score, 1).ToString());
            alldata.Add(scores.Count.ToString());
            alldata.Add(positive.Count.ToString());
            alldata.Add(negative.Count.ToString());
            return alldata;
        }
    }
}
