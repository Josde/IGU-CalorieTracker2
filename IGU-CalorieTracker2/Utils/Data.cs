using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGU_CalorieTracker2.Utils
{
    class Data
    {
        public static void GenerateRandomData(int number)
        {
            Dia d;
            Random rnd = new Random();
            for (int i = 0; i < number; i++)
            {
                int caloriasDesayuno, caloriasComida, caloriasCena, caloriasSnacks;
                DateOnly fecha = DateOnly.FromDateTime(DateTime.Today);
                fecha = fecha.AddDays(-rnd.Next(0, 100));
                caloriasDesayuno = rnd.Next(0, 1000);
                caloriasComida = rnd.Next(0, 1000);
                caloriasCena = rnd.Next(0, 1000);
                caloriasSnacks = rnd.Next(0, 1000);
                Debug.WriteLine("Generado día " + fecha.ToString() + " con calorias " + caloriasDesayuno + " " + caloriasComida + " " + caloriasCena + " " + caloriasSnacks);  
                d = new Dia(caloriasDesayuno, caloriasComida, caloriasCena, caloriasSnacks, fecha);
                Model.Instance.Dias.Add(d);
            }
        }

        internal static void Reset()
        {
            Model.Instance.Dias.Clear();
        }
    }
}
