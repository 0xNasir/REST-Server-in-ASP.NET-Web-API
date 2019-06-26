using SampleRESTServer.Models;
using System;
using MySql.Data.MySqlClient;
using System.Collections;

namespace SampleRESTServer
{
    public class PersonPersistence
    {
        private MySqlConnection con;
        public PersonPersistence()
        {
            string connection;
            connection = "server=127.0.0.1;uid=root;pwd=;database=csharp";
            try
            {
                con = new MySqlConnection();
                con.ConnectionString = connection;
                con.Open();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public ArrayList GetPersons()
        {
            ArrayList alist = new ArrayList();
            String sql = "SELECT * FROM person";
            MySqlCommand command = new MySqlCommand(sql, con);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Person person = new Person();
                person.ID = reader.GetInt32(0);
                person.lastname = reader.GetString(2);
                person.firstname = reader.GetString(1);
                person.payrate = reader.GetFloat(3);
                person.startdate = reader.GetString(4);
                person.enddate = reader.GetString(5);
                alist.Add(person);
            }
            return alist;
        }

        public long savePerson(Person person)
        {
            String sql = "INSERT INTO person(id, firstname, lastname, payrate, startdate, enddate) VALUES('','" + person.firstname + "','" + person.lastname + "'," + person.payrate + ",'" + person.startdate.ToString() + "','" + person.enddate.ToString() + "')";
            MySqlCommand command = new MySqlCommand(sql, con);
            command.ExecuteNonQuery();
            long id = command.LastInsertedId;
            return id;
        }

        public Person GetPerson(int id)
        {
            Person person = new Person();
            String sql = "SELECT * FROM person WHERE id=" + id.ToString();
            MySqlCommand command = new MySqlCommand(sql, con);
            MySqlDataReader reader= command.ExecuteReader();
            if (reader.Read())
            {
                person.ID = reader.GetInt32(0);
                person.lastname = reader.GetString(2);
                person.firstname = reader.GetString(1);
                person.payrate = reader.GetFloat(3);
                person.startdate = reader.GetString(4);
                person.enddate = reader.GetString(5);
                return person;
            }
            else
            {
                return null;
            }
        }

        public Boolean DeletePerson(int id)
        {
            Person person = new Person();
            String sql = "SELECT * FROM person WHERE id=" + id.ToString();
            MySqlCommand command = new MySqlCommand(sql, con);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                String deleteStmt = "DELETE FROM person WHERE id=" + id.ToString();
                command = new MySqlCommand(deleteStmt, con);
                command.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool updatePerson(long ID, Person person)
        {
            String sql = "SELECT * FROM person WHERE id=" + ID.ToString();
            MySqlCommand command = new MySqlCommand(sql, con);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                String updatesql = "UPDATE person SET firstname='" + person.firstname + "', lastname='" + person.lastname + "', payrate='" + person.payrate + "', startdate='" + person.startdate + "', enddate='" + person.enddate + "' WHERE id=" + ID.ToString();
                command = new MySqlCommand(updatesql, con);
                command.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}