using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;
using System.Text.RegularExpressions;
using System.Windows.Input;
using IGU_CalorieTracker2.Utils;

namespace IGU_CalorieTracker2.MVVM.ViewModel
{
    class AddDataViewModel
    {
        public int caloriasDesayuno, caloriasComida, caloriasCena, caloriasSnacks;
        public bool Accept(String textoDesayuno, String textoComida, String textoCena, String textoSnacks, DateTime? date)
        {
            // Validamos que todos los campos de calorias sean numeros validos, y que la fecha no este vacía.
            // En vez de regex podríamos usar TryParse.
            Regex regex = new Regex("^\\d+");
            if (!regex.IsMatch(textoDesayuno)) {
                return false;
            }
            if (!regex.IsMatch(textoComida)) {
                return false;
            }
            if (!regex.IsMatch(textoCena)) {
                return false;
            }
            if (!regex.IsMatch(textoSnacks)) {
                return false;
            }
            if (date == null)
            {
                return false;
            }
            DateOnly soloDate = DateOnly.FromDateTime((DateTime)date);
            float desayuno, comida, cena, snacks;
            desayuno = float.Parse(textoDesayuno);
            comida = float.Parse(textoComida);
            cena = float.Parse(textoCena);
            snacks = float.Parse(textoSnacks);
            // Guardamos los datos en el modelo.
            if (this.SaveData(desayuno, comida, cena, snacks, soloDate))
            {
                return true;
            } else
            {
                return false;
            }
            
        }

        private bool SaveData(float desayuno, float comida, float cena, float snacks, DateOnly date)
        {
            Dia d = new Dia(desayuno, comida, cena, snacks, date);
            Model.Instance.Dias.Add(d);
            return true;
        }
    }


}
