using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisoners
{
    public class Box
    {
        private BoxStatus _status;
        public BoxStatus Status  { get => _status; }

        private int _order;
        public int Order { get => _order; }

        private int _number;
        public int Number { get => _number; }

        public Box (int order, int number)
        {
            _order = order;
            _number = number;
            _status = BoxStatus.Closed;
        }

        public int Open()
        {
            _status = BoxStatus.Opened;
            return _number;
        }

        public void Close()
        {
            _status = BoxStatus.Closed;
        }
    }

    public enum BoxStatus
    {
        Closed = 0,
        Opened = 1
    }
}
