using AccessDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessBusinessLayer
{
    public class clsExercises
    {
        enum enMode { Add  , Update}
        enMode _Mode;
        public int ID { get; set; }
        public string Name { get; set; }

        private bool _AddNewExercise()
        {
            this.ID = clsExerciseName.AddNewExerciseName(this.Name);
            return this.ID > -1;
        }
        public clsExercises()
        {
            ID = -1;
            Name = "";
            _Mode = enMode.Add;
        }

        public void Save()
        {
            switch(_Mode)
            {
                case enMode.Add:
                    if (_AddNewExercise())
                    {
                        _Mode = enMode.Update;
                    }
                    break;
            }
        }

    }
}
