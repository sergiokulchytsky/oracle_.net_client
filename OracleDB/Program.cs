﻿using System;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace OracleDB
{
    class Program
    {
        static void Main(string[] args)
        {
            OracleConnection myConnection = new OracleConnection("Data Source=XE;User Id=Sergio;Password=123456;");

            //OPEN CONNECTION
            myConnection.Open();

            //CREATE TABLE STATEMENT
            string create = " CREATE TABLE Persons " +
                                    " ( " +
                                        " ID  NUMBER NOT NULL PRIMARY KEY, " +
                                        " Name VARCHAR2(50) NOT NULL, " +
                                        " City VARCHAR2(50) NOT NULL" +
                                    " ) ";
            OracleCommand myCommand = new OracleCommand();
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.Text;
            myCommand.CommandText = create;

            myCommand.ExecuteNonQuery();
            
            //-------------------------------------------------------
            //INSERT STATEMENT
            string insert = "INSERT INTO Persons (ID, Name, City) VALUES (2, :Name, :City)";
            myCommand = new OracleCommand();
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.Text;
            myCommand.Parameters.Add(new OracleParameter("Name", OracleDbType.Varchar2)).Value = "SERGIO";
            myCommand.Parameters.Add(new OracleParameter("City", OracleDbType.Varchar2)).Value = "Ternopil";
            myCommand.CommandText = insert;
            myCommand.ExecuteNonQuery();
            myCommand.Parameters.Clear();
            
            //------------------------------------------------------
            //UPDATE STATEMENT
            string update = "UPDATE Persons SET Persons.Name = :NewName WHERE Persons.Name =  :Name";
            myCommand = new OracleCommand();
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.Text;

            myCommand.Parameters.Add(new OracleParameter("NewName", OracleDbType.Varchar2)).Value = "SERHIY";
            myCommand.Parameters.Add(new OracleParameter("Name", OracleDbType.Varchar2)).Value = "SERGIO";
            myCommand.CommandText = update;
            myCommand.ExecuteNonQuery();
            myCommand.Parameters.Clear();
            
            //------------------------------------------------------
            //SELECT STATEMENT
            string select = "SELECT Persons.Name FROM Persons WHERE Persons.City = :City";
            myCommand = new OracleCommand();
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.Text;
            myCommand.Parameters.Add(new OracleParameter("City", OracleDbType.Varchar2)).Value = "Ternopil";
            myCommand.CommandText = select;
            OracleDataReader reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["Name"]);
            }
            reader.Close();
            myCommand.Parameters.Clear();

            //-----------------------------------------------------
            //DELETE STATEMENT
            string delete = "DELETE FROM Persons WHERE City = :City";
            myCommand = new OracleCommand();
            myCommand.Connection = myConnection;
            myCommand.Parameters.Add(new OracleParameter("City", OracleDbType.Varchar2)).Value = "Ternopil";
            myCommand.CommandText = delete;
            myCommand.ExecuteNonQuery();
   
            myCommand.CommandText = "DROP TABLE Sergio.Persons";
            myCommand.ExecuteNonQuery();
            myCommand.Parameters.Clear();

            //CLOSE CONNECTION
            myConnection.Close();
            Console.ReadLine();

        }
    }
}