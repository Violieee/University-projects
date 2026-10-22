using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7
{
    public class MasterWorker
    {
        public string StopLine()
        {
            return "Майстер: Терміново зупинити лінію!";
        }

        public string RestartLine()
        {
            return "Майстер: Конвеєр відновив роботу.";
        }
    }

    public class Engineer
    {
        public string Diagnose()
        {
            return "Інженер: Діагностувати причину.";
        }

        public string CheckAfterRestart()
        {
            return "Інженер: Перевірити стан обладнання.";
        }
    }

    public class Administration
    {
        public string RecordDowntime()
        {
            return "Адміністрація: Зафіксувати простій.";
        }
    }
}
