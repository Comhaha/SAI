using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp.DevTools.Storage;

namespace SAI.App.Models
{
    public class LogCsvModel
    {
        private static LogCsvModel _instance;
        public string[] _titles;
        public double[][] _values;
        public double[][] _smoothes;

        private LogCsvModel() { }

        public static LogCsvModel instance
        {
            get { 
                if(_instance == null)
                {
                   _instance = new LogCsvModel();
                }

                return _instance;
            }
        }

        public string[] titles
        { get { return _titles; } set { _titles = value; } }

        public double[][] values
        { get { return _values; } set { _values = value; } }

        public double[][] smoothes { get { return _smoothes; } set { _smoothes = value; } }

        public void clear()
        {
            _titles = null;
            _values = null;
            _smoothes = null;
        }
    }
}
