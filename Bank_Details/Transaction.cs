using System;
using Microsoft.Data.SqlClient;

public class Transactions
{
    int transaction_no;
    int account_no;
    string transaction_type;
    int amount_before;
    int amount_after;

    static string connectionString = "Server=localhost;Database=BankDB;Trusted_Connection=True;TrustServerCertificate=True;";

    public int TransactionNo
    {
        get { return transaction_no; }
    }

    public int AccountNo
    {
        get { return account_no; }
    }

    public string TransactionType
    {
        get { return transaction_type; }
    }

    public int AmountBefore
    {
        get { return amount_before; }
    }

    public int AmountAfter
    {
        get { return amount_after; }
    }


    public Transactions(int account_no, int amount_before, int amount_after)
    {
        this.account_no = account_no;
        this.amount_before = amount_before;
        this.amount_after = amount_after;

        if (amount_after > amount_before)
        {
            this.transaction_type = "Deposit";
        }
        else
        {
            this.transaction_type = "Withdrawal";
        }

        using SqlConnection con = new SqlConnection(connectionString);
        string sql = @"INSERT INTO Transactions (AccountNo, TransactionType, AmountBefore, AmountAfter) VALUES (@AccountNo, @TransactionType, @AmountBefore, @AmountAfter)";

        using SqlCommand cmd = new SqlCommand(sql, con);
        cmd.Parameters.AddWithValue("@AccountNo", this.account_no);
        cmd.Parameters.AddWithValue("@TransactionType", this.transaction_type);
        cmd.Parameters.AddWithValue("@AmountBefore", this.amount_before);
        cmd.Parameters.AddWithValue("@AmountAfter", this.amount_after);

        con.Open();
        cmd.ExecuteNonQuery();
        Console.WriteLine("Transaction Inserted Successfully!");
    }
    public Transactions(int transaction_no)
    {
        Console.WriteLine("Enter the Transaction No: ");
        this.transaction_no = Convert.ToInt32(Console.ReadLine());

        using SqlConnection con = new SqlConnection(connectionString);
        string sql = @"SELECT AccountNo, TransactionType, AmountBefore, AmountAfter FROM Transactions WHERE TransactionNo = @TransactionNo";
        using SqlCommand cmd = new SqlCommand(sql, con);

        cmd.Parameters.AddWithValue("@TransactionNo", this.transaction_no);
        con.Open();

        using SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            this.account_no = Convert.ToInt32(reader["AccountNo"]);
            this.transaction_type = reader["TransactionType"].ToString();
            this.amount_before = Convert.ToInt32(reader["AmountBefore"]);
            this.amount_after = Convert.ToInt32(reader["AmountAfter"]);

            Console.WriteLine("Transaction Show Successfully!");
        }
        else
        {
            Console.WriteLine("Transaction Not Found!");
        }
    }
    public override string ToString()
    {
        return "Transaction No: " + transaction_no + "\nAccount No: " + account_no + "\nTransaction Type: " + transaction_type + "\nAmount Before: " + amount_before + "\nAmount After: " + amount_after;
    }
}