using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Mysqlx.Crud;
using static System.Net.Mime.MediaTypeNames;

class Aluno
{
    public string Nome { get; set; }
    public int Idade { get; set; }
    public float Nota { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        List<Aluno> alunos = new List<Aluno>();
        string connectionString = "Server=localhost;Port=3306;Database=banco_escola;Uid=root;Pwd=;";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            // Adicionar alunos na lista
            alunos.Add(new Aluno { Nome = "João", Idade = 18, Nota = 9.5f });
            alunos.Add(new Aluno { Nome = "Maria", Idade = 19, Nota = 8.0f });
            alunos.Add(new Aluno { Nome = "Pedro", Idade = 20, Nota = 7.5f });

            // Inserir dados na tabela
            foreach (Aluno aluno in alunos)
            {
                string query = "INSERT INTO Alunos (Nome, Idade, Nota) VALUES (@nome, @idade, @nota)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", aluno.Nome);
                    command.Parameters.AddWithValue("@idade", aluno.Idade);
                    command.Parameters.AddWithValue("@nota", aluno.Nota);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Exibir dados
        Console.WriteLine("Nome - Idade - Nota");
        foreach (Aluno aluno in alunos)
        {
            Console.WriteLine("{0} - {1} - {2} ", aluno.Nome, aluno.Idade, aluno.Nota);
        }
    }
}