using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Prisoners
{
    public class Playground
    {
        private int _order;
        public int Order { get => _order; }

        private List<Box> _boxes = new List<Box>();
        public List<Box> Boxes { get => _boxes; }

        private List<Prisoner> _prisoners = new List<Prisoner>();
        public List<Prisoner> Prisoners { get => _prisoners;}

        private Result _result;
        public Result Result { get => _result; set => _result = value; }

        private int _countPassed = 0;
        public int CountPassed { get => _countPassed; set => _countPassed = value; }

        private int _countFailed = 0;
        public int CountFailed { get => _countFailed; set => _countFailed = value; }

        List<int> randomOrders = new List<int>();
        List<int> randomNumbers = new List<int>();

        Random random = new Random();

        private int _dimension = 0;

        public Playground(int dimesion)
        {
            _dimension = dimesion;
        }

        public Playground(int dimesion, int order)
        {
            _dimension = dimesion;
            _order = order;
        }

        public void CreatePrisoners()
        {
            for (int max = 1; max <= _dimension; max++)
                Prisoners.Add(new Prisoner(max));
        }
        
        public void CreateBoxes()
        {
            for(int max = 1; max <= _dimension; max++)
            {
                randomOrders.Add(max);
                randomNumbers.Add(max);
            }

            //randomOrders = randomOrders.OrderBy(a => random.Next()).ToList();
            randomNumbers = randomNumbers.OrderBy(a => random.Next()).ToList();

            for (int i = 0; i < _dimension; i++)
                _boxes.Add(new Box(randomOrders[i], randomNumbers[i]));
        }

        public void CloseAllBoxes()
        {
            foreach (Box box in _boxes)
                box.Close();
        }

        public void DoJourney(Prisoner prisoner)
        {
            CloseAllBoxes();
            
            Box theBox = new Box(0,0);

            theBox = _boxes.Find(a => a.Order == prisoner.Number);

            for (int i = 1; i <= _dimension / 2; i++)
            {
                if (theBox.Status == BoxStatus.Opened)
                    theBox = _boxes.Find(a => a.Status == BoxStatus.Closed);
                
                int numberInTheBox = theBox.Open();
                prisoner.OpenedBoxes.Add(theBox);

                if (numberInTheBox == prisoner.Number)
                {
                    prisoner.Result = Result.Passed;
                    break;
                }
                else
                {
                    prisoner.Result = Result.Failed;
                    theBox = _boxes.Find(a => a.Order == numberInTheBox);
                }
            }
        }





    }
}
