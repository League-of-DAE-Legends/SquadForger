using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using SquadForger.Model;

namespace SquadForger.Repository
{
    public class CsvChallongeParser : ITeamNamesRepository
    {
        public List<Team> GetTeams()
        {
            List<Team> teamNames = new List<Team>();

            try
            {
                //Show the file picker
                using (var openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                    openFileDialog.Title = "Select a CSV file";

                    //If the user clicked OK
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Get the selected file path
                        string csvFilePath = openFileDialog.FileName;
                        
                        //Read the CSV file
                        using (TextFieldParser parser = new TextFieldParser(csvFilePath))
                        {
                            //delimiter for csv is comma
                            parser.Delimiters = new string[] { "," };

                            if (!parser.EndOfData)
                            {
                                parser.ReadLine();
                            }

                            while (!parser.EndOfData)
                            {
                                string[] fields = parser.ReadFields();

                                if (fields.Length >=2)
                                {
                                    //second column is team name
                                    var team = new Team
                                    {
                                        TeamName = fields[1]
                                    };
                                    teamNames.Add(team);
                                }
                            }
                        }
                    }
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return teamNames;
        }
    }
}