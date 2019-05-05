using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace privoda.Models
{
    public class ModelLine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Protection { get; set; } //Степень защиты
        public string OutputFrequency { get; set; } //Выходная частота
        public string TransientMoment { get; set; } //Переходный момент
        public int Functions { get; set; } //Функции
        public int SecurityFeatures { get; set; } //Функции безопасности
        public int PresetSpeeds { get; set; } //Предварительно заданные скорости
        public string Image { get; set; } //Картинка
    }
}
