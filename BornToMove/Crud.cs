﻿using System;
using MySql.Data.MySqlClient;

namespace BornToMove
{
    public class Crud : ICrud
    {
        public MySqlConnection Connect()
        {
            var connString = "Server=localhost;Database=born2move;Uid=born2move_user;Pwd=IRo4HiMfdl!x2VWN;";
            MySqlConnection conn = new MySqlConnection(connString);
            return conn;
        }

        public bool IsConnected(MySqlConnection? conn)
        {
            if (conn == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void CreateMove(string name, string description, int sweatRate)
        {
            var conn = Connect();
            if (IsConnected(conn))
            {
                var sql = "INSERT INTO move (name, description, sweatRate)" +
                "VALUES (@name, @description, @sweatRate);";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@sweatRate", sweatRate);

                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                Console.WriteLine("Move created successfully.");
            }
        }

        public List<int>? ReadMoveIds()
        {
            List<int> moveIds = new List<int>();
            var conn = Connect();
            if (IsConnected(conn))
            {
                var sql = "SELECT id FROM move;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        int id = rdr.GetInt32(0);
                        moveIds.Add(id);
                    }
                    return moveIds;
                }
                finally
                {
                    conn.Close();
                }
            }
            return null;
        }

        public Move? ReadMoveById(int moveId)
        {
            var conn = Connect();
            if (IsConnected(conn))
            {
                var sql = "SELECT * FROM move" +
                    "WHERE id = @id;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", moveId);

                try
                {
                    conn.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        int id = rdr.GetInt32(0);
                        string name = rdr.GetString(1);
                        string description = rdr.GetString(2);
                        int sweatRate = rdr.GetInt32(3);
                        return new Move(id, name, description, sweatRate);
                    }
                    return null;
                }
                finally
                {
                    conn.Close();
                }
            }
            return null;
        }

        public Dictionary<int, string>? ReadAllMoveNames()
        {
            Dictionary<int, string> moveNames = new Dictionary<int, string>();
            var conn = Connect();
            if (IsConnected(conn))
            {
                var sql = "SELECT id, name FROM move;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        int id = rdr.GetInt32(0);
                        string name = rdr.GetString(1);
                        moveNames.Add(id, name);
                    }
                    return moveNames;
                }
                finally
                {
                    conn.Close();
                }
            }
            return null;
        }

        public Dictionary<int, Move>? ReadAllMoves()
        {
            Dictionary<int, Move> moves = new Dictionary<int, Move>();
            var conn = Connect();
            if (IsConnected(conn))
            {
                var sql = "SELECT * FROM move;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        int id = rdr.GetInt32(0);
                        string name = rdr.GetString(1);
                        string description = rdr.GetString(2);
                        int sweatRate = rdr.GetInt32(3);
                        moves.Add(id, new Move(id, name, description, sweatRate));
                    }
                    return moves;
                }
                finally
                {
                    conn.Close();
                }
            }
            return null;
        }
    }
}
