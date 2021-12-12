using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Linq;
using System.Diagnostics;

namespace IGU_CalorieTracker2
{

    public class Model
    {
        #region Atributos
        private ObservableCollection<Dia> dias;
        private Opciones o;
        private static readonly Model instance = new Model();

        public static Model Instance
        {
            get
            {
                return instance;
            }
        }
        public TipoGrafica tipo;
        public Dia? diaSeleccionado;
        internal Opciones Opciones { get => o; set => o = value; }
        public ObservableCollection<Dia> Dias { get => dias; 
            set { 
                dias = value; 
                //this.Sort();
            } 
        }
        #endregion

        #region Constructores y retornador de instancia
        private Model()
        {
            Dias = new ObservableCollection<Dia>();
            tipo = TipoGrafica.Dia;
            diaSeleccionado = null;
            o = new Opciones();
        }
        #endregion

        #region Metodos
        public void Sort()
        {
            List<Dia> temp = new List<Dia>(Dias.OrderBy(d => d.Fecha));
            // Limpiamos y reañadimos los elementos para que se lance el evento CollectionChanged forzosamente.
            // Es lento, pero he probado otros métodos y no se lanzan los eventos correctamente y por tanto no se reflejan los cambios en la UI.
            Dias.Clear();
            foreach (Dia d in temp)
            {
                Dias.Add(d);
            }
        }
        public bool saveToCsv(String fPath)
        {
            StringBuilder csv = new StringBuilder();
            try
            {
                foreach (Dia d in Dias)
                {
                    string newLine = string.Format("{0},{1},{2},{3},{4}", d.Fecha.DayNumber, d.caloriasDesayuno, d.caloriasComida, d.caloriasCena, d.caloriasSnacks);
                    csv.AppendLine(newLine);
                }
                File.WriteAllText(fPath, csv.ToString());
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("saveToCsv: {0}", e.Message);
                return false;
            }
        }

        public bool importFromCsv(String fPath)
        {
            String[] result;
            int dayNumber;
            float cDes, cCom, cCen, cSna;
            

            foreach (String s in File.ReadAllLines(fPath))
            {
                result = s.Split(',');
                
                if (result.Length != 5) return false;
                if (!int.TryParse(result[0], out dayNumber) || !float.TryParse(result[1], out cDes) || !float.TryParse(result[2], out cCom) || !float.TryParse(result[3], out cCen) || !float.TryParse(result[4], out cSna))
                {
                    return false;
                }
                Dia d = new Dia(cDes, cCom, cCen, cSna, DateOnly.FromDayNumber(dayNumber));
                Debug.WriteLine(String.Format("Created day: '{0}'", d.ToString()));
                dias.Add(d);
            }
            
            return true;
        }

        internal void ChangeDay(int howMuch)
        {
            if (this.diaSeleccionado == null) return;
            int indice = this.Dias.IndexOf(this.diaSeleccionado);
            //Evitamos indices negativos
            indice = (indice + howMuch >= 0) ? indice + howMuch : this.Dias.Count() + howMuch;
            this.diaSeleccionado = this.Dias[indice];
        }
        #endregion
    }
    public class Dia 
    {
        private DateOnly fecha;
        public float caloriasDesayuno;
        public float caloriasComida;
        public float caloriasCena;
        public float caloriasSnacks;
        public float caloriasTotales;

        public DateOnly Fecha { get => fecha; set => fecha = value; }
        public float CaloriasTotales { get => caloriasTotales; set => caloriasTotales = value; }
        #region Constructor
        public Dia()
        {
            fecha = new DateOnly();
            caloriasDesayuno = caloriasComida = caloriasCena = caloriasSnacks = 0.0f;
        }

        public Dia(float caloriasDesayuno, float caloriasComida, float caloriasCena, float caloriasSnacks, DateOnly fecha)
        {
            this.fecha = fecha;
            this.caloriasDesayuno = caloriasDesayuno;
            this.caloriasComida = caloriasComida;
            this.caloriasCena = caloriasCena;
            this.caloriasSnacks = caloriasSnacks;
            this.caloriasTotales = caloriasDesayuno + caloriasComida + caloriasCena + caloriasSnacks;
        }
        #endregion Constructor

        #region Metodos
        public override String ToString()
        {
            return String.Format("{0}|{1}|{2}|{3}|{4}", this.fecha.ToShortDateString(), caloriasDesayuno, caloriasComida, caloriasCena, caloriasSnacks);
        }
        #endregion
    }


    public class Opciones
    {
        #region Atributos
        Color colorFill, colorStrokeLinea, colorEjes;
        double strokeSizeLinea, strokeSizeEjes;
        int limiteX, limiteY;
        bool isFillEnabled;
        String themeName;

        public Color ColorFill { get => colorFill; set => colorFill = value; }
        public Color ColorStroke { get => colorStrokeLinea; set => colorStrokeLinea = value; }
        public double StrokeSizeLinea { get => strokeSizeLinea; set => strokeSizeLinea = value; }
        public Color ColorEjes { get => colorEjes; set => colorEjes = value; }
        public double StrokeSizeEjes { get => strokeSizeEjes; set => strokeSizeEjes = value; }
        public int LimiteX { get => limiteX; set => limiteX = value; }
        public int LimiteY { get => limiteY; set => limiteY = value; }
        public bool IsFillEnabled { get => isFillEnabled; set => isFillEnabled = value; }
        #endregion

        #region Constructor
        public Opciones()
        {
            if (!this.loadFromFile()) { 
                //Si no podemos cargar el fichero de configuración cargamos los defectos.
                colorFill = colorStrokeLinea = colorEjes = Colors.Black;
                strokeSizeLinea = 1.0;
                strokeSizeEjes = 5.0;
                limiteX = 200;
                limiteY = 5000;
                isFillEnabled = false;
            }
        }

        private Boolean loadFromFile()
        {
            //throw new NotImplementedException();
            return true;
        }
        private Boolean saveToFile(String path)
        {
            //throw new NotImplementedException();
            return true;
        }
        #endregion

    }
}
