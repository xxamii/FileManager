using System;
using Xunit;
using Collections;

namespace Testing
{
    public class ListTests
    {
        [Fact]
        public void Create_empty_int_list()
        {
            // Arrange
            int expected = 0;

            // Act
            List<int> list = new List<int>();

            // Assert
            Assert.Equal(list.Length, expected);
        }

        [Fact]
        public void Create_non_empty_int_list()
        {
            int expected = 5;

            List<int> list = new List<int>(5);

            Assert.Equal(list.Length, expected);
        }

        [Fact]
        public void Create_int_list_from_array()
        {
            int[] array = { 1, 2, 3, 4, 5 };
            int expectedLength = 5;
            int expectedFirst = 1;

            List<int> list = new List<int>(array);

            Assert.Equal(list.Length, expectedLength);
            Assert.Equal(list[0], expectedFirst);
        }

        [Fact]
        public void ToArray_non_empty_int_list()
        {
            List<int> list = new List<int>();

            Assert.IsType<int[]>(list.ToArray());
        }

        [Fact]
        public void Length_non_empty_int_list()
        {
            int[] array = { 1, 2, 3, 4, 5 };

            List<int> list = new List<int>(array);

            Assert.Equal(5, list.Length);
        }

        [Fact]
        public void Indexing_valid_argument()
        {
            int[] array = { 1, 2, 3, 4, 5 };

            List<int> list = new List<int>(array);

            Assert.Equal(array[0], list[0]);
            Assert.Equal(array[array.Length-1], list[list.Length-1]);
        }

        [Fact]
        public void Indexing_non_valid_argument()
        {
            int[] array = { 1, 2, 3, 4, 5 };
            int test = 0;
            var exceptionType = (new IndexOutOfRangeException()).GetType();

            List<int> list = new List<int>(array);

            Assert.Throws(exceptionType, () => test = list[-1]);
        }

        [Fact]
        public void Push_element()
        {
            int[] array = { 1, 2, 3, 4, 5 };
            List<int> list = new List<int>(array);
            int expected = 6;

            list.Push(expected);

            Assert.Equal(list.Length, expected);
            Assert.Equal(list[list.Length - 1], expected);
        }

        [Fact]
        public void Pop_element_once()
        {
            int[] array = { 1, 2, 3, 4, 5 };
            List<int> list = new List<int>(array);
            int expected = 4;

            list.Pop();

            Assert.Equal(list.Length, expected);
            Assert.Equal(list[list.Length - 1], expected);
        }

        [Fact]
        public void Pop_elements_empty_list()
        {
            var exceptionType = (new OverflowException()).GetType();

            List<int> list = new List<int>();

            Assert.Throws(exceptionType, () => list.Pop());
        }

        [Fact]
        public void Contains_contains_element()
        {
            int[] array = { 1, 2, 3, 4, 5 };
            List<int> list = new List<int>(array);
            int test = 5;

            Assert.True(list.Contains(test));
        }

        [Fact]
        public void Contains_does_not_cointain_element()
        {
            int[] array = { 1, 2, 3, 4, 5 };
            List<int> list = new List<int>(array);
            int test = 420;

            Assert.False(list.Contains(test));
        }

        [Fact]
        public void Concat_another_list()
        {
            int[] array1 = { 1, 2, 3, 4, 5 };
            List<int> list1 = new List<int>(array1);

            int[] array2 = { 6, 7, 8, 9, 10 };
            List<int> list2 = new List<int>(array2);

            int expected = 10;

            list1.Concat(list2);

            Assert.Equal(list1.Length, expected);
            Assert.Equal(list1[list1.Length - 1], expected);
        }

        [Fact]
        public void Concat_self()
        {
            int[] array = { 1, 2, 3, 4, 5 };
            List<int> list = new List<int>(array);

            list.Concat(list);

            int expectedLength = 10;
            int expected = 5;

            Assert.Equal(list.Length, expectedLength);
            Assert.Equal(list[list.Length - 1], expected);
        }

        [Fact]
        public void Sort_int_ascending_method()
        {
            int[] array = { 5, 4, 1, 3, 2 };
            List<int> list = new List<int>(array);
            int expectedFirst = 1;
            int expectedLast = 5;

            list.Sort((int a, int b) => a - b > 0);

            Assert.Equal(list[0], expectedFirst);
            Assert.Equal(list[list.Length - 1], expectedLast);
        }

        [Fact]
        public void Sort_int_descending_method()
        {
            int[] array = { 5, 4, 1, 3, 2 };
            List<int> list = new List<int>(array);
            int expectedFirst = 5;
            int expectedLast = 1;

            list.Sort((int a, int b) => a - b < 0);

            Assert.Equal(list[0], expectedFirst);
            Assert.Equal(list[list.Length - 1], expectedLast);
        }

        [Fact]
        public void Filter_int_odd_numbers_method()
        {
            int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            List<int> list = new List<int>(array);
            int expectedLength = 5;
            int expectedFirst = 2;

            list = list.Filter((int a) => a % 2 != 0);

            Assert.Equal(list.Length, expectedLength);
            Assert.Equal(list[0], expectedFirst);
        }
    }
}
