using System;

public class Person
{
    string name;
    string mobile;
    string address;
    int age;

    public Person()
    {
        Console.Write("Enter the Customer Name : ");
        this.name = Console.ReadLine();

        Console.Write("Enter the Mobile Number : ");
        this.mobile = Console.ReadLine();

        Console.Write("Enter the Address : ");
        this.address = Console.ReadLine();

        Console.Write("Enter the Age : ");
        this.age = Convert.ToInt32(Console.ReadLine());
    }

    public override string ToString()
    {
        return "Name : " + name + "\nMobile : " + mobile + "\nAddress : " + address + "\nAge : " + age;
    }
}