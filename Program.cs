
using System;
using System.Collections.Generic;
using System.Linq;


// Build a collection of customers who are millionaires


// Define a bank
public class Bank
{
    public string Symbol { get; set; }
    public string Name { get; set; }
}

// Define a customer
public class Customer
{
    public string Name { get; set; }
    public double Balance { get; set; }
    public string Bank { get; set; }
}

public class GroupedMillionaires
{
    public string Bank { get; set; }
    public IEnumerable<string> Millionaires { get; set; }
}

namespace linq
{

    public class Program
    {
        public static void Main()
        {

            List<Customer> customers = new List<Customer>() {
            new Customer(){ Name="Bob Lesman", Balance=80345.66, Bank="FTB"},
            new Customer(){ Name="Joe Landy", Balance=9284756.21, Bank="WF"},
            new Customer(){ Name="Meg Ford", Balance=487233.01, Bank="BOA"},
            new Customer(){ Name="Peg Vale", Balance=7001449.92, Bank="BOA"},
            new Customer(){ Name="Mike Johnson", Balance=790872.12, Bank="WF"},
            new Customer(){ Name="Les Paul", Balance=8374892.54, Bank="WF"},
            new Customer(){ Name="Sid Crosby", Balance=957436.39, Bank="FTB"},
            new Customer(){ Name="Sarah Ng", Balance=56562389.85, Bank="FTB"},
            new Customer(){ Name="Tina Fey", Balance=1000000.00, Bank="CITI"},
            new Customer(){ Name="Sid Brown", Balance=49582.68, Bank="CITI"}
        };

/*
Given the same customer set, display how many millionaires per bank.
Ref: https://stackoverflow.com/questions/7325278/group-by-in-linq
*/
        var millionairesPerBank = (from taco in customers
                where taco.Balance >= 1000000
                group taco by taco.Bank into burrito
                select new { BankName = burrito.Key, MillionaireCount = burrito.Count() }).ToList();
        Console.WriteLine("Millionaires per bank");
        foreach (var item in millionairesPerBank)
        {
            Console.WriteLine($"{item.BankName}: {item.MillionaireCount}");
        }
        Console.WriteLine();

Console.WriteLine("Millionaires and their banks");
        var groupedByBank = customers.Where(c => c.Balance >= 1000000).GroupBy(
            p => p.Bank,  // Group banks
            p => p.Name,  // by millionaire names
            (bank, millionaires) => new GroupedMillionaires()
            {
                Bank = bank,
                Millionaires = millionaires
            }
        ).ToList();

            foreach (var item in groupedByBank)
            {
                Console.WriteLine($"{item.Bank}: {string.Join(" and ", item.Millionaires)}");
            }
            Console.WriteLine();

/*
    TASK:
    As in the previous exercise, you're going to output the millionaires,
    but you will also display the full name of the bank. You also need
    to sort the millionaires' names, ascending by their LAST name.

    Example output:
        Tina Fey at Citibank
        Joe Landy at Wells Fargo
        Sarah Ng at First Tennessee
        Les Paul at Wells Fargo
        Peg Vale at Bank of America
*/

    // Create some banks and store in a List
    List<Bank> banks = new List<Bank>() {
                new Bank(){ Name="First Tennessee", Symbol="FTB"},
                new Bank(){ Name="Wells Fargo", Symbol="WF"},
                new Bank(){ Name="Bank of America", Symbol="BOA"},
                new Bank(){ Name="Citibank", Symbol="CITI"},
            };

    List<Customer> millionaireReport = customers.Where(c => c.Balance >= 1000000)
        .Select(c => new Customer()
        {
            Name = c.Name,
            Bank = banks.Find(b => b.Symbol == c.Bank).Name,
            Balance = c.Balance
        })
        .ToList();

            foreach (Customer customer in millionaireReport)
            {
                Console.WriteLine($"{customer.Name} at {customer.Bank}");
            }

        }
    }
}


