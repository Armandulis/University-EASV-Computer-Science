using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Stack
{
    public class CoreMethods : ICoreMethods
    {
        
        Stack<int> MyStack = new Stack<int>();

        //Pop - Removes and returns items from the top of the stack.
        public int PopItem() {
            return MyStack.Pop(); ;
        }


        //Peak - Returns the top item from the stack.
        public int PeakItem()
        { 
            return MyStack.Peek();
        }


        // Get and Print
        public void GetAllItems() {
            Console.WriteLine("");
            foreach (int i in MyStack)
            {
                Console.WriteLine(i);
            }
        }

        // Count
        public void CountItems() {
            Console.WriteLine("");
            MyStack.Count();
            Console.WriteLine(MyStack.Count());
            
        }

        // Clear - Removes all items from the stack.
        public void ClearItems()
        {
            MyStack.Clear();
            Console.WriteLine("Zero Items in the stack");
            Console.WriteLine("");
        
        }

        // Push - Inserts an item at the top of the stack.
        public void AddAndPush(int item)
        {
            Console.WriteLine("");
            MyStack.Push(item);
            Console.WriteLine("added: " + item);
            
        }
        public void AdditionOf2() {
            Console.WriteLine("");
            var item1 = PopItem(); 
            var item2 = PopItem();
            int sum = item1 + item2;
            AddAndPush(sum);
            Console.WriteLine("Added Result: " + sum);
        }
        public void SubtractionOf2()
        {
            Console.WriteLine("");
            var item1 = PopItem();
            var item2 = PopItem();

                var substract = item1 - item2;
                AddAndPush(substract);

                Console.WriteLine("Substract Result: " + substract); 
            
        
        }
        public void MultiplicationOf2()
        {
            Console.WriteLine("");
            var item1 = PopItem(); 
            var item2 = PopItem(); 
            var multiply = item1 * item2;
            AddAndPush(multiply);
            Console.WriteLine("Multiply Result: " + multiply);
            
        }
        public int DivisionOf2()
        {
            Console.WriteLine("");
            var item1 = PopItem();
            if (PeakItem() == 0)
            {
                AddAndPush(item1);
                throw new InvalidDataException("You can't devide from zero!");
                
            }
            else
            {

                var item2 = PopItem();
                var divide = item1 / item2;
                AddAndPush(divide);
                Console.WriteLine("Divide Result: " + divide);
                return divide;
            }
        }

        public int MultiplicationOfAllStack()
        {
            Console.WriteLine("");
            int multofAll =1;
            foreach (int i in MyStack)
            {

                multofAll =multofAll * i;
                
            }
            Console.WriteLine("Equals to " + multofAll);
            return multofAll;
        }
        public int AdditionOfAllStack()
        {
            Console.WriteLine("");
            int sumOfAll = 0;
            foreach (int i in MyStack)
            {

                sumOfAll = sumOfAll + i;

            }
            Console.WriteLine("Equals to " + sumOfAll);
            return sumOfAll;
        }


        public void Menu()
        {
            

            Console.WriteLine("                                      ");
            Console.WriteLine("-------------------------------------|");
            Console.WriteLine("Select what you want to do with a Stack:");
            Console.WriteLine("1: Push                              |");
            Console.WriteLine("2: Peek                              |");
            Console.WriteLine("3: Pop                               |");    
            Console.WriteLine("4: Get and Print                     |");
            Console.WriteLine("5: Count                             |");
            Console.WriteLine("6: Clear Stack                       |");
            Console.WriteLine("7: addition of two topMost integers  |");
            Console.WriteLine("8: subtraction of two topMost integers |");
            Console.WriteLine("9: multiplication of two topMost integers |");
            Console.WriteLine("10: division of two topMost integers |");
            Console.WriteLine("11: multiplication of all integers   |");
            Console.WriteLine("12: addition of all integers         |");
            Console.WriteLine("-------------------------------------|");
            Console.WriteLine("                                      ");

            int selecteditem = int.Parse(Console.ReadLine());

            switch (selecteditem)
            {
                case 1:
                    {
                        Console.WriteLine("Type an integer you want to push:");
                        var item = int.Parse(Console.ReadLine());
                        AddAndPush(item);

                        Menu();
                    }
                    break;
                case 2:
                    { PeakItem();

                        Menu();
                    }
                    break;
                case 3:
                    { PopItem();

                        Menu();
                    }
                    break;
                case 4:
                    { GetAllItems();

                        Menu();
                    }
                    break;
                case 5:
                    { CountItems();
                        Menu();
                    }
                    break;
                case 6:
                    { ClearItems();
                        Menu();
                    }
                    break;
                case 7:
                    { AdditionOf2();
                        Menu();
                    }
                    break;
                case 8:
                    { SubtractionOf2();
                        Menu();
                    }
                    break;
                case 9:
                    { MultiplicationOf2();
                        Menu();
                    }
                    break;
                case 10:
                    { DivisionOf2();
                        Menu();
                    }
                    break;
                case 11:
                    { MultiplicationOfAllStack();
                        Menu();
                    }
                    break;
                case 12:
                    { AdditionOfAllStack();
                        Menu();
                    }
                    break;


                default:
                    { Console.Write("Option is not valid, try again");
                        Menu();
                    }
                    break;
            
            }
        }
    }
}
