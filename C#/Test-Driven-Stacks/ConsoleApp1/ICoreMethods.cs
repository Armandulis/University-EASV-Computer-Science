using System;
using System.Collections.Generic;
using System.Text;

namespace Stack
{
    public interface ICoreMethods
    {
        int PopItem();
        int PeakItem();
        void GetAllItems();
        void CountItems();
        void ClearItems();
        void AddAndPush(int item);
        void AdditionOf2();
        void SubtractionOf2();
        void MultiplicationOf2();
        int DivisionOf2();
        int MultiplicationOfAllStack();
        int AdditionOfAllStack();
        void Menu();
    }
}
