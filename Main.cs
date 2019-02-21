using System;
using System.ComponentModel;
using System.Windows;
using System.Runtime.CompilerServices;

namespace Laba1
{
    class ZodiacChineese
    {
        private string _name;
        private bool _female;
        public ZodiacChineese(string name, bool female)
        {
            _name = name;
            _female = female;
        }
        public string name() { return _name; }
        public bool female() { return _female; }
    }
    class ZodiacWest
    {
        private string _name;
        private int _dayBeg;
        public ZodiacWest(string name, int day)
        {
            _name = name;
            _dayBeg = day;
        }
        public string name() { return _name; }
        public int dayBeg() { return _dayBeg; }
    }
    class Element
    {
        private string _name;
        private string _adjBase;
        private string _nameMale;
        public Element(string name, string adjBase)
        {
            _name = name;
            _adjBase  = adjBase;
        }

        public string noun() { return _name; }
        public string femaleAdj() { return _adjBase + "а"; }
        public string maleAdj() { return _adjBase + "ий"; }
    }
    class Main : INotifyPropertyChanged
    {
        private int _age;
        private DateTime _date = DateTime.Today.AddYears(-20);
        private Element _element;
        private ZodiacWest _zodiac;
        private ZodiacChineese _zodiacChineese;
        ZodiacChineese[] _zodiacChineeses = {
            new ZodiacChineese("Миша", true),
            new ZodiacChineese("Бик", false),
            new ZodiacChineese("Тигр", false),
            new ZodiacChineese("Кіт", false),
            new ZodiacChineese("Дракон", false),
            new ZodiacChineese("Змія", true),
            new ZodiacChineese("Кінь", false),
            new ZodiacChineese("Коза", true),
            new ZodiacChineese("Мавпа", true),
            new ZodiacChineese("Півень", false),
            new ZodiacChineese("Собака", false),
            new ZodiacChineese("Свиня", true) };
        ZodiacWest[] _zodiacWNames = {
            new ZodiacWest("Водолій", 20),
            new ZodiacWest("Риби", 19),
            new ZodiacWest("Овен", 21),
            new ZodiacWest("Тілець", 20),
            new ZodiacWest("Близнюки", 21),
            new ZodiacWest("Рак", 21),
            new ZodiacWest("Лев", 23),
            new ZodiacWest("Діва", 23),
            new ZodiacWest("Терези", 23),
            new ZodiacWest("Скорпіон", 23),
            new ZodiacWest("Стрілець", 22),
            new ZodiacWest("Козеріг", 22) };
        Element[] _elements = {
            new Element("Метал", "Металев"),
            new Element("Вода", "Водян"),
            new Element("Дерево", "Дерев'ян"),
            new Element("Вогонь", "Вогнян"),
            new Element("Земля", "Землян") };
        public string Age
        {
            get
            {
                return $"{_age} років";
            }
        }
        public string ZodiacW
        {
            get
            {
                return _zodiac.name();
            }
        }
        public string ZodiacChineese
        {
            get
            {
                string adjective;
                if (_zodiacChineese.female())
                    adjective = _element.femaleAdj();
                else
                    adjective = _element.maleAdj();
                return adjective + " " + _zodiacChineese.name();
            }
        }
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                try
                {
                    CountAge();
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message, "Error");
                }
                CountZodiacWest();
                CountZodiacChineese();
                CountElement();
                OnPropertyChanged(nameof(Age));
                OnPropertyChanged(nameof(ZodiacW));
                OnPropertyChanged(nameof(ZodiacChineese));
                OnPropertyChanged();

            }
        }
       private int CountAge()
        {
            DateTime today = DateTime.Today;
            _age = today.Year - _date.Year;
            if (today.Month < _date.Month ||
                (today.Month == _date.Month && today.Day < _date.Day))
                _age--;
            if (_age < 0 || _age > 135)
                throw new Exception("Wrong birthday date");
            else if (_date.Day == today.Day && _date.Month == today.Month)
                MessageBox.Show("Happy birthday!!", "Congratulations");
            return _age;
        }
        private ZodiacWest CountZodiacWest()
        {
            int zodiacInd = _date.Month - 1;
            if (_date.Day < 20)
                zodiacInd = (zodiacInd == 0) ? 11 : zodiacInd - 1;
            _zodiac = _zodiacWNames[zodiacInd];
            return _zodiac;
        }
        private ZodiacChineese CountZodiacChineese()
        {
            int zodiacInd = (_date.Year + 8) % 12;
            _zodiacChineese = _zodiacChineeses[zodiacInd];
            return _zodiacChineese;
        }
        private Element CountElement()
        {
            int elementInd = (_date.Year) % 10 / 2;
            _element = _elements[elementInd];
            return _element;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
