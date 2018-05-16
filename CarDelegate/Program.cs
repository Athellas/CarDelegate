using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDelegate
{
    class Program
    {
        static void Main(string[] args)
        {
            Car c1 = new Car("SlugBug", 100, 10);

            c1.RegisterWithCarEngine(new Car.CarEngineHandler(OnCarEngineEvent));
            c1.RegisterWithCarEngine(new Car.CarEngineHandler(OnCarEngineEvent));

            Console.WriteLine("Speeding up");
            for (int i = 0; i < 6; i++)
            {
                c1.Accelerate(20);
            }
            Console.ReadLine();
            }

        public static void OnCarEngineEvent(string msg)
        {
            Console.WriteLine("Message From Car Object");
            Console.WriteLine($"=>{msg}\n");
        }
    }
    
    public class Car
    {
        // Internal state data.
        public int CurrentSpeed { get; set; }
        public int MaxSpeed { get; set; }
        public string PetName { get; set; }

        // Is the car alive or dead?
        private bool carIsDead;

        // Class constructors.
        public Car() { MaxSpeed = 100; }
        public Car(string name, int maxSp, int currSp)
        {
            CurrentSpeed = currSp;
            MaxSpeed = maxSp;
            PetName = name;
        }

        // delegate
        public delegate void CarEngineHandler(string msgForCaller);

        private CarEngineHandler listOfHandlers;

        public void RegisterWithCarEngine(CarEngineHandler methodToCall)
        {
            listOfHandlers += methodToCall;
        }

        public void Accelerate(int delta)
        {
            //if the car is dead, send dead message
            if (carIsDead)
            {
                //if (listOfHandlers != null)
                listOfHandlers?.Invoke("Sorry, this car is dead");
            }
            else
            {
                CurrentSpeed += delta;

                //is this car almost dead?
                if (10 >= MaxSpeed - CurrentSpeed && listOfHandlers != null)
                {
                    listOfHandlers("Careful buddy! Gonna blow!");
                }

                if (CurrentSpeed >= MaxSpeed)
                    carIsDead = true;
                else
                    Console.WriteLine("CurrentSpeed = {0}", CurrentSpeed);

            }
        }
    }
}
