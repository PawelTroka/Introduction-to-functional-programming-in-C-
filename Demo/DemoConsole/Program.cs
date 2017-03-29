using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DemoConsole
{

    public class UserWithFriends
    {
        public IReadOnlyCollection<int> FriendsIds { get; }
        public string Name { get; }
        public int Age { get; }
        public int Id { get; }

        public UserWithFriends(string name, int age, int id, IReadOnlyCollection<int> friendsIds)
        {
            Name = name;
            Age = age;
            Id = id;
            FriendsIds = friendsIds;
        }

        public UserWithFriends Rename(string name) => new UserWithFriends(name, Age,Id,FriendsIds);
        public UserWithFriends Birthday() => new UserWithFriends(Name, Age + 1, Id, FriendsIds);
    }

    public class User
    {
        public string Name { get; }
        public int Age { get; }

        public User(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public User Rename(string name) => new User(name,Age);

        public User Birthday() => new User(Name,Age+1);
    }

    public static class EnumerableExtensions
    {
        public static T Find<T>(IEnumerable<T> enumerable, Predicate<T> predicate)
        {
            foreach (var value in enumerable)
            {
                if(predicate(value))
                    return value;
            }
            return default(T);
        }
    }

    class Program
    {

        [Pure]
        public static int Add(int a, int b)
        {
            return a + b;
        }
        
        static void UnpureMethod(int a, int b)
        {
            Add(a, b);
        }

        private static int _res = 0;



        static double Method(double x)
        {
            return x + 1;
        }

        static void Example1()
        {
            var f = new Func<double, double>(x => x + 2);
            var g = (Delegate)f;

            var masterDelegate = f +
                (Func<double, double>)g +
                new Func<double, double>(Method) +
                (Func<double, double>)Method;

            Console.WriteLine(masterDelegate.Invoke(1));
        }

        static void Example2()
        {
            var f = new Func<double, double>(x => x + 2);

            Console.WriteLine(f.Method);
            Console.WriteLine(f.Method.ReturnParameter);
            Console.WriteLine(f.Method.MemberType);

            Console.WriteLine(f.Target);
            Console.WriteLine(f.GetType());
            Console.ReadLine();
        }

        static void Example3()
        {
            var f = new Func<double, double>(x =>
            {
                var c = 2;
                var res = x + c;
                return res;
            });
            var methodBody = f.Method.GetMethodBody();
            Console.WriteLine(string.Join(",", methodBody.LocalVariables));
            Console.WriteLine(string.Join(",", methodBody.GetILAsByteArray()));
            Console.ReadLine();
        }


        static void Example4()
        {
            var range = Enumerable.Range(1, 100).ToArray();
            var sum = range.Sum();
            Console.WriteLine(sum*sum - range.Select(v => v*v).Sum());
        }

        static void Example5()
        {
            var sqr = new Func<int,int>(x => x*x);

            var sumOfSquares = new Func<IEnumerable<int>,int>(en => en.Select(sqr).Sum());
            var squareOfSum = new Func<IEnumerable<int>,int>(en => sqr(en.Sum()));

            var sumSquareDifference = new Func<IEnumerable<int>, int>(en =>
            squareOfSum(en) - sumOfSquares(en));

            Console.WriteLine(sumSquareDifference(Enumerable.Range(1, 100)));
        }

        static void Example6()
        {
            Func<int, int, int> add = (x, y) => x + y;
            int a = add(2, 3);  // a = 5


            Func<int, Func<int, int>> curriedAdd = x => y => x + y;
            int b = curriedAdd(2)(3); // b = 5


            var add5 = curriedAdd(5);

            int c = add5(3); // c = 8
            int d = add5(5); // d = 10
        }

        void Example7()
        {
            Func<int, int, int, int, int> addFourThings = (a, b, c, d) =>
            a + b + c + d;

            var curriedAddFourThings = Curry(addFourThings);

            int result = curriedAddFourThings(1)(2)(3)(4);  // result = 10

            var addOne = curriedAddFourThings(1);
            var addOneAndTwo = addOne(2);
            var addOneAndTwoAndThree = addOneAndTwo(3);

            int result2 = addOneAndTwoAndThree(4); // result2 = 10
        }

        public static Func<T1, Func<T2, T3>> Curry<T1, T2, T3>(Func<T1, T2, T3> function)
        {
            return a => b => function(a, b);
        }

        public static Func<T1, Func<T2, Func<T3, T4>>> Curry<T1, T2, T3, T4>(Func<T1, T2, T3, T4> function)
        {
            return a => b => c => function(a, b, c);
        }

        public static Func<T1, Func<T2, Func<T3, Func<T4, T5>>>> Curry<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5> function)
        {
            return a => b => c => d => function(a, b, c, d);
        }


        static void Example8()
        {
            var str = "Alamakota";
            var str2 = "xyz";
            str = str + str2;
        }

        static void Main(string[] args)
        {
            var str = "Alamakota";
            str.Replace("a", "b");
            Console.WriteLine(Math.Sqrt(4));
        }
    }
}
