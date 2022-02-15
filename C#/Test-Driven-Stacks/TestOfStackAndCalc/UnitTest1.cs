using Moq;
using Stack;
using System;
using System.IO;
using Xunit;

namespace TestOfStackAndCalc
{
    public class UnitTest1
    {

        [Fact]
        public void TestSubstract()
        {
            var coreMethods = new CoreMethods();


            coreMethods.AddAndPush(1);
            coreMethods.AddAndPush(-3);
            coreMethods.AddAndPush(0);
            coreMethods.SubtractionOf2();
            Assert.Equal(3, coreMethods.PeakItem());
        }

       

        [Fact]
        public void TestAdd()
        {

            var coreMethods = new CoreMethods();


            coreMethods.AddAndPush(1);
            coreMethods.AddAndPush(-3);
            coreMethods.AddAndPush(0);
            coreMethods.AdditionOf2();
            Assert.Equal(-3, coreMethods.PeakItem());
        }

        [Fact]
        public void TestAddAll()
        {

            var coreMethods = new CoreMethods();


            coreMethods.AddAndPush(1);
            coreMethods.AddAndPush(-3);
            coreMethods.AddAndPush(0);
            
            Assert.Equal(-2, coreMethods.AdditionOfAllStack());
        }

        [Fact]
        public void TestMultiplyWith0()
        {
            var coreMethods = new CoreMethods();


            coreMethods.AddAndPush(1);
            coreMethods.AddAndPush(-3);
            coreMethods.AddAndPush(0);
            coreMethods.MultiplicationOf2();
            Assert.Equal(0, coreMethods.PeakItem());
        }

        [Fact]
        public void TestMultiply()
        {
            var coreMethods = new CoreMethods();


            coreMethods.AddAndPush(1);
            coreMethods.AddAndPush(-3);
            coreMethods.AddAndPush(2);
            coreMethods.MultiplicationOf2();
            Assert.Equal(-6, coreMethods.PeakItem());
        }

        [Fact]
        public void TestMultiplyAll()
        {
            var coreMethods = new CoreMethods();


            coreMethods.AddAndPush(2);
            coreMethods.AddAndPush(-3);
            coreMethods.AddAndPush(2);
            
            Assert.Equal(-12, coreMethods.MultiplicationOfAllStack());
        }

        [Fact]

        public void TestDivide()
        {
            var coreMethods = new CoreMethods();


            coreMethods.AddAndPush(1);
            coreMethods.AddAndPush(-3);
            coreMethods.AddAndPush(0);
            coreMethods.DivisionOf2();
            Assert.Equal(0, coreMethods.PeakItem());
        }

        [Fact]
        public void TestDivideWith0()
        {
            var coreMethods = new CoreMethods();


            coreMethods.AddAndPush(1);
            coreMethods.AddAndPush(0);
            coreMethods.AddAndPush(0);

            Exception ex = Assert.Throws<InvalidDataException>(() => coreMethods.DivisionOf2());
   
            Assert.Equal("You can't devide from zero!", ex.Message);
            
        }

        [Theory]
        [InlineData(5, 1, 6)]
        [InlineData(7, 1, 8)]
        [InlineData(5, -11, -6)]
        [InlineData(7, -13, -6)]
        public void AddTest3(int x, int y, int res)
        {
            var coreMethods = new CoreMethods();


            coreMethods.AddAndPush(y);
            coreMethods.AddAndPush(x);

            coreMethods.AdditionOf2();

            Assert.Equal(res, coreMethods.PeakItem());
        }

        [Theory]
        [InlineData(5, 1, 5)]
        [InlineData(8, 4, 2)]
        [InlineData(10, 5, 2)]
        public void DivideTest3(int x, int y, int res)
        {
            var coreMethods = new CoreMethods();


            coreMethods.AddAndPush(y);
            coreMethods.AddAndPush(x);

            coreMethods.DivisionOf2();

            Assert.Equal(res, coreMethods.PeakItem());
        }

        [Theory]
        [InlineData(7, 3, 21)]
        [InlineData(3, 2, 6)]
        [InlineData(10, 5, 50)]
        public void MultiplyTest3(int x, int y, int res)
        {
            var coreMethods = new CoreMethods();


            coreMethods.AddAndPush(y);
            coreMethods.AddAndPush(x);

            coreMethods.MultiplicationOf2();

            Assert.Equal(res, coreMethods.PeakItem());
        }

        [Theory]
        [InlineData(5, 1, 4)]
        [InlineData(42, 3, 39)]
        [InlineData(10, 5, 5)]
        public void SubstractTest3(int x, int y, int res)
        {
            var coreMethods = new CoreMethods();


            coreMethods.AddAndPush(y);
            coreMethods.AddAndPush(x);

            coreMethods.SubtractionOf2();

            Assert.Equal(res, coreMethods.PeakItem());
        }


    }
}

