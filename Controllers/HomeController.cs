using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestPrimesMVC.Models;

namespace TestPrimesMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private List<int> primeNumbers = new List<int>();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public List<int> GetCircularPrimeNumbers(int number)
        {
            if (number < 0)
                return null;
            if (number > 1000000)
                return null;
            return GetCPN(number);
        }

        [HttpGet]
        public bool IsCircular(int number)
        {
            if (number < 0)
                return false;
            if (number > 1000000)
                return false;
            return IsCPN(number);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private bool IsPrimeNumber(int number)
        {
            if (primeNumbers.Contains(number))
                return true;
            int div = 0;
            for (int i = 1; i <= number; i++)
            {
                if (number % i == 0)
                    div++;
            }
            if (div == 2)
                primeNumbers.Add(number);

            return div == 2;
        }

        private bool IsCPN(int number)
        {
            int c = 0;
            int temp = number;
            int num, r, d;
            while (temp > 0)
            {
                c++;
                temp /= 10;
            }

            num = number;

            while (IsPrimeNumber(num))
            {
                r = num % 10;
                d = num / 10;
                num = (int)((Math.Pow(10, c - 1)) * r + d);
                if (num == number)
                    return true;
            }

            return false;
        }

        private List<int> GetCPN(int number)
        {
            List<int> numbers = new List<int>();
            int multipleTwo;
            for (int i = 1; i < number; i++)
            {
                multipleTwo = i % 10;
                if (multipleTwo == 5 || (multipleTwo % 2 == 0))
                    continue;
                if (IsCPN(i))
                    numbers.Add(i);
            }

            return numbers;
        }


    }
}
