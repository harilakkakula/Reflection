using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Reflection
{
    class Program
    {
        private static Dictionary<string, string> _methodDictionary;
        static void Main(string[] args)
        {

            _methodDictionary = new Dictionary<string, string>();
            _methodDictionary = GetMethdoDictionary();

            var type = typeof(StudentFunction);
            var sudentFunctionInstance = Activator.CreateInstance(type);

            int i=42;

            //System.Type type = i.GetType(); 
            Console.WriteLine(sudentFunctionInstance);

            var testString = "Hello [GetName], your university name is [GetUniversity] and roll is [GetRoll] and muliplcation resukt is [multiplication]";

            var match = Regex.Matches(testString, @"\[([A-Za-z]+)]", RegexOptions.IgnoreCase);

            foreach (var v in match)
            {
                var originalString = v.ToString();
                var x = v.ToString();
                x = x.Replace("[", "");
                x = x.Replace("]", "");
                x = _methodDictionary[x];
                var toInvoke = type.GetMethod(x);
                var getParameters = toInvoke.GetParameters();
                var prameters = new Object[getParameters.Length];
                if(getParameters.Length>0)
                {
                    TypeConverter tc = TypeDescriptor.GetConverter(i.GetType());
                    prameters[0]=2;
                }
                var result = toInvoke.Invoke(sudentFunctionInstance, prameters);
                testString = testString.Replace(originalString, result.ToString());
            }

            Console.WriteLine(testString);
            System.Reflection.Assembly info = typeof(System.Int32).Assembly;
            Console.WriteLine(info);

            Console.WriteLine("Hello World!");
        }

        private static Dictionary<String ,String> GetMethdoDictionary()
        {
            var dictionary = new Dictionary<String, String>
            {
            {"multiplication","multiplication"},
            {"GetName", "GetName"},
            {"GetUniversity", "GetUniversity"},
            {"GetRoll","GetRoll"},
           
            };

            return dictionary;
        }
    }

    class Student
    {
        public string Name { get; set; }
        public string University { get; set; }
        public int Roll { get; set; }
        public int mulresult { get; set; }
    }

   public class StudentFunction
    {
        private Student student;

        public StudentFunction()
        {
            student = new Student() 
            {
                Name = "Gopal C. Bala",
                University = "Jahangirnagar University",
                Roll = 1424
            };
        }
        public string GetName()
        {
            return student.Name;
        }

        public string GetUniversity()
        {
            return student.University;
        }

        public int GetRoll()
        {
            return student.Roll;
        }

        public int multiplication(int x)
        {
            return x * x;
        }

    }
}
