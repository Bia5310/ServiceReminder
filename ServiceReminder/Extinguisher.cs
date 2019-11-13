using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ServiceReminder
{
    public enum Types { Extinguisher = 'e', Firelocker = 'f' };
    public enum Extinguishers { OU3, OP4, };

    class Element
    {
        public Point Position = new Point(); //Положение на карте
        public string Name = ""; //Имя точки
        public string Building = ""; //Здание
        public string Corps = ""; //Корпус
        public string Floor = ""; //Этаж
        public Types Type = Types.Extinguisher; //Тип хранилища
        public int NumberOfFirehose = 0; //Количество рукавов
    }

    class Extinguisher : Element
    {
        public static string ExtinguisherToString(Extinguishers extinguisher)
        {
            switch (extinguisher)
            {
                case Extinguishers.OP4:
                    return "ОП-4";
                case Extinguishers.OU3:
                    return "ОУ-3";
            }
            return "Noname";
        }
    }


}
