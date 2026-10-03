using System;
using Microsoft.Data.SqlClient;

public class Bank_Account
{
    string account_no;
    string customer_name;
    int balance;

    static string connectionString = "Server=localhost;Database=BankDB;Trusted_Connection=True;TrustServerCertificate=True;";
    public string AccountNo
    {
        get { return account_no; }
    }

    public string Customer
    {
        get { return customer_name; }
    }

    public int Balance
    {
        get { return balance; }
    }


    public Bank_Account()
    {
        using SqlConnection con = new SqlConnection(connectionString);
        con.Open();

        string accountSql = "SELECT ISNULL(MAX(AccountNo), 0) + 1 FROM Bank_Accounts";
        using SqlCommand accountCmd = new SqlCommand(accountSql, con);

        int nextAccountNo = Convert.ToInt32(accountCmd.ExecuteScalar());
        this.account_no = nextAccountNo.ToString();

        Console.WriteLine("Enter customer name:");
        this.customer_name = Console.ReadLine();

        Console.WriteLine("Enter balance:");
        this.balance = Convert.ToInt32(Console.ReadLine());


        string sql = @"INSERT INTO Bank_Accounts (AccountNo, CustomerName, Balance) VALUES (@AccountNo, @CustomerName, @Balance)";
        using SqlCommand cmd = new SqlCommand(sql, con);

        cmd.Parameters.AddWithValue("@AccountNo", this.account_no);
        cmd.Parameters.AddWithValue("@CustomerName", this.customer_name);
        cmd.Parameters.AddWithValue("@Balance", this.balance);

        cmd.ExecuteNonQuery();

        Console.WriteLine("Account Created Successfully!");
        Console.WriteLine("Your Account Number: " + this.account_no);
    }


    public Bank_Account(string account_no, string customer_name, int balance)
    {
        this.account_no = account_no;
        this.customer_name = customer_name;
        this.balance = balance;
    }


    public void deposit(int amount)
    {
        Console.WriteLine("Enter the Deposit Amount:");
        amount = Convert.ToInt32(Console.ReadLine());

        if (amount > 0)
        {
            int amount_before = balance;
            string sql = @"UPDATE Bank_Accounts SET Balance = Balance + @Amount WHERE AccountNo = @AccountNo";

            using SqlConnection con = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@AccountNo", account_no);

            con.Open();

            int rows = cmd.ExecuteNonQuery();

            if (rows > 0)
            {
                balance = balance + amount;
                int amount_after = balance;

                Transactions t = new Transactions(Convert.ToInt32(account_no), amount_before , amount_after);

                Console.WriteLine("Deposit Successful!");
                Console.WriteLine("New Balance: " + balance);
            }
            else
            {
                Console.WriteLine("Account not found!");
            }
        }
        else
        {
            Console.WriteLine("Enter valid amount!");
        }
    }

    public void withdrawal(int amount)
    {
        Console.WriteLine("Enter the Withdrawal Amount:");
        amount = Convert.ToInt32(Console.ReadLine());

        if (amount > 0 && balance >= amount)
        {
            int amount_before = balance;

            string sql = @"UPDATE Bank_Accounts SET Balance = Balance - @Amount WHERE AccountNo = @AccountNo";

            using SqlConnection con = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@AccountNo", account_no);

            con.Open();

            int rows = cmd.ExecuteNonQuery();

            if (rows > 0)
            {
                balance = balance - amount;
                int amount_after = balance;

                Transactions t = new Transactions(Convert.ToInt32(account_no), amount_before, amount_after);

                Console.WriteLine("Withdrawal Successful!");
                Console.WriteLine("New Balance: " + balance);
            }
            else
            {
                Console.WriteLine("Account not found!");
            }
        }
        else
        {
            Console.WriteLine("Insufficient Balance or Invalid Amount!");
        }
    }


    public Bank_Account(string account_no)
    {
        using SqlConnection con = new SqlConnection(connectionString);

        string sql = @"SELECT AccountNo, CustomerName, Balance FROM Bank_Accounts WHERE AccountNo = @AccountNo";
        using SqlCommand cmd = new SqlCommand(sql, con);

        cmd.Parameters.AddWithValue("@AccountNo", account_no);

        con.Open();

        using SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {

            this.account_no = reader["AccountNo"].ToString();
            this.customer_name = reader["CustomerName"].ToString();
            this.balance = Convert.ToInt32(reader["Balance"]);

        }
        else
            throw new Exception("1");

    }
    public override string ToString()
    {
        return "Account No: " + account_no + "\nCustomer Name: " + customer_name + "\nBalance: " + balance;
    }
}
