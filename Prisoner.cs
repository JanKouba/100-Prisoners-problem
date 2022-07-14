using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisoners
{
    public class Prisoner
    {
        private int _number;
        public int Number{ get=>_number; }

        private Result _result;
        public Result Result { get => _result; set { _result = value; Try(); } }

        private List<Box> _openedBoxes = new List<Box>();
        public List<Box> OpenedBoxes { get => _openedBoxes; }

        private int _tries;
        public int Tries { get => _tries; }

        public Prisoner(int number)
        {
            _number = number;
            _result = Result.None;
        }

        public void Try()
        {
            _tries++;
        }
    }

    public enum Result
    { 
        None = -1,
        Failed = 0,
        Passed = 1
    }
}
