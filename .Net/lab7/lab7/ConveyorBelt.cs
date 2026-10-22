using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7
{
    public abstract class Machine
    {
        public string Name { get; set; } = "Конвеєр";
    }

    public interface IWeightCheck
    {
        bool CheckWeight(double weight);
    }

    public interface IProcess
    {
        void ProcessItem(double weight);
    }

    public interface IReset
    {
        void Reset();
    }

    public class ConveyorBelt : Machine,
        IWeightCheck, IProcess, IReset
    {
        public double MaxWeight { get; }
        public bool IsStopped { get; private set; }

        public event Action? CriticalStop;
        public event Action? ItemProcessed;
        public event Action? Restarted;
        public event Action? WeightChecked;

        public Action<string>? MessageGenerated;

        public ConveyorBelt(double maxWeight)
        {
            if (maxWeight <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(maxWeight));

            MaxWeight = maxWeight;
        }

        public bool CheckWeight(double weight)
        {
            if (weight < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(weight));

            WeightChecked?.Invoke();

            if (IsStopped)
            {
                MessageGenerated?.Invoke(
                    "Конвеєр зупинено. Спочатку скиньте аварію.");
                return false;
            }

            if (weight > MaxWeight)
            {
                IsStopped = true;
                CriticalStop?.Invoke();
                return false;
            }

            return true;
        }

        public void ProcessItem(double weight)
        {
            if (CheckWeight(weight))
            {
                ItemProcessed?.Invoke();
            }
        }

        public void Reset()
        {
            if (IsStopped)
            {
                IsStopped = false;
                Restarted?.Invoke();
            }
        }
    }
}

