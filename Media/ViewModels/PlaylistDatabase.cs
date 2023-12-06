using Media.Models;
using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using MsBox.Avalonia;
using Media.Views;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace Media.ViewModels
{
    internal class PlaylistDatabase
    {
        static string playlistDatabaseFilePath = "Data Source=" + Environment.CurrentDirectory + "\\Playlist.db";
        static SqliteConnection playlistDatabaseConnection = new SqliteConnection(playlistDatabaseFilePath);  
        public PlaylistDatabase()
        {
            InitializeTables();
        }
        public async Task InitializeTables()
        {
            string createPlaylistTable = "CREATE TABLE IF NOT EXISTS Playlist(" +
                "PlaylistID VARCHAR(4000) PRIMARY KEY NOT NULL," +
                "PlaylistName VARCHAR(4000) NOT NULL," +
                "PlaylistThumbnailPath VARCHAR(4000)," +
                "PlaylistDateOfCreation SMALLDATETIME " +
                ")";
            try
            {
                CreateTable(createPlaylistTable);
            }
            catch (Exception ex)
            {
                await MessageBoxManager.GetMessageBoxStandard("Lỗi", ex.Message).ShowWindowDialogAsync(MainWindow.GetInstance());
            }

            string createPlaylistMediasTable = "CREATE TABLE IF NOT EXISTS PlaylistMedias(" +
                "MediaPath VARCHAR(4000)," +
                "PlaylistID VARCHAR(4000)," +
                "FOREIGN KEY (PlaylistID) REFERENCES Playlist(PlaylistID)" +
                "PRIMARY KEY(MediaPath, PlaylistID)" +
                ")";
            try
            {
                CreateTable(createPlaylistMediasTable);
            }
            catch (Exception ex)
            {
                await MessageBoxManager.GetMessageBoxStandard("Lỗi", ex.Message).ShowWindowDialogAsync (MainWindow.GetInstance());
            }
            DeleteNotExistMedias();
        }
        

        public void CreateTable(string tableCreationString)
        {
            playlistDatabaseConnection.Open();
            using (SqliteCommand sqlCommand = new SqliteCommand(tableCreationString, playlistDatabaseConnection))
            {
                RunSqlCommand(sqlCommand);
            }
            playlistDatabaseConnection.Close();
        }

        private async Task RunSqlCommand(SqliteCommand sqlCommand)
        {
            try
            {
                int result = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                await MessageBoxManager.GetMessageBoxStandard("Lỗi", ex.Message).ShowWindowDialogAsync(MainWindow.GetInstance());
            }
        }

        public void InsertPlaylist(Playlist insertPlaylist)
        {
            playlistDatabaseConnection.Open();

            string insertQuery = "INSERT INTO Playlist "
                + "VALUES(@playlistID, @playlistName, @playlistThumbnailPath, @playlistDateOfCreation)";
            SqliteCommand sqlCommand = new SqliteCommand(insertQuery, playlistDatabaseConnection);
            AddValueIntoPlaylistinSQLCommand(insertPlaylist, sqlCommand);
            RunSqlCommand(sqlCommand);
            playlistDatabaseConnection.Close();
        }

        public void InsertMediaIntoPlaylistMedias(Playlist playlistContainsMedia, MediaItem insertMedia)
        {
            playlistDatabaseConnection.Open();
            string insertQuery = "INSERT INTO PlaylistMedias "
                + "VALUES(@mediaPath, @playlistID)";
            SqliteCommand sqlCommand = new SqliteCommand(insertQuery, playlistDatabaseConnection);
            AddValueIntoPlaylistMediasinSQLCommand(playlistContainsMedia, insertMedia, sqlCommand);
            RunSqlCommand(sqlCommand);

            playlistDatabaseConnection.Close();
        }

        public void UpdatePlaylist(Playlist updatePlaylist)
        {
            playlistDatabaseConnection.Open();

            string updateQuery = "UPDATE Playlist\n"
                + "SET PlaylistName = @playlistName, PlaylistThumbnailPath = @playlistThumbnailPath, "
                + "PlaylistDateOfCreation = @playlistDateOfCreation\n"
                + "WHERE PlaylistID = @playlistID";
            SqliteCommand sqlCommand = new SqliteCommand(updateQuery, playlistDatabaseConnection);
            AddValueIntoPlaylistinSQLCommand(updatePlaylist, sqlCommand);
            RunSqlCommand(sqlCommand);

            playlistDatabaseConnection.Close();
        }
        public void UpdateMedia(Playlist playlistContainsMedia, MediaItem updateMedia)
        {
            playlistDatabaseConnection.Open();

            string updateQuery = "UPDATE PlaylistMedias\n"
                + "SET PlaylistID = @playlistID\n"
                + "WHERE MediaPath = @mediaPath";            
            SqliteCommand sqlCommand = new SqliteCommand(updateQuery, playlistDatabaseConnection);
            AddValueIntoPlaylistMediasinSQLCommand(playlistContainsMedia, updateMedia, sqlCommand);
            RunSqlCommand(sqlCommand);

            playlistDatabaseConnection.Close();
        }

        public async Task<Playlist> QueryPlaylistGivenPlaylistID(string playlistID)
        {
            Playlist result = new Playlist();
            playlistDatabaseConnection.Open();

            string queryPlaylist = "SELECT PlaylistID, PlaylistName, PlaylistThumbnailPath, PlaylistDateOfCreation FROM Playlist " +
                "WHERE PlaylistID = @playlistID";          
            var sqlCommand = new SqliteCommand(queryPlaylist, playlistDatabaseConnection);
            var dataReader = sqlCommand.ExecuteReader();
            try
            {
                while (dataReader.Read())
                {
                    List<MediaItem> mediaInPlaylist = QueryAllMediaInGivenPlaylist(playlistID);
                    result = new Playlist(dataReader["PlaylistID"].ToString(), dataReader["PlaylistName"].ToString(),
                        dataReader["PlaylistThumbnailPath"].ToString(), mediaInPlaylist);
                    break;
                }
            }
            catch (Exception ex)
            {
                await MessageBoxManager.GetMessageBoxStandard("Lỗi", ex.Message).ShowWindowDialogAsync(MainWindow.GetInstance());
            }

            playlistDatabaseConnection.Close();
            return result;
        }
        public List<Playlist> QueryAllPlaylists()
        {
            playlistDatabaseConnection.Open();
            List<Playlist> playlistsInDatabase = new List<Playlist>();
            List<MediaItem> mediaInPlaylist = new List<MediaItem>();

            string queryPlaylists = "SELECT PlaylistID, PlaylistName, PlaylistThumbnailPath, PlaylistDateOfCreation FROM Playlist";           
            var sqlCommand = new SqliteCommand(queryPlaylists, playlistDatabaseConnection);
            var dataReader = sqlCommand.ExecuteReader();
            try
            {
                while (dataReader.Read())
                {
                    Playlist playlist;
                    mediaInPlaylist = QueryAllMediaInGivenPlaylist(dataReader["PlaylistID"].ToString());
                    playlist = new Playlist(dataReader["PlaylistID"].ToString(), dataReader["PlaylistName"].ToString(),
                    dataReader["PlaylistThumbnailPath"].ToString(), mediaInPlaylist);
                    playlistsInDatabase.Add(playlist);
                }
            }
            catch (Exception ex)
            {
                MessageBoxManager.GetMessageBoxStandard("Lỗi", ex.Message);
            }
            playlistDatabaseConnection.Close();
            return playlistsInDatabase;
        }

        public List<MediaItem> QueryAllMediaInGivenPlaylist(string playlistID)
        {
            List<MediaItem> mediasInPlaylist = new List<MediaItem>();

            string queryMedia = "SELECT MediaPath FROM PlaylistMedias WHERE PlaylistID = @playlistID";           
            var sqlCommand = new SqliteCommand(queryMedia, playlistDatabaseConnection);
            sqlCommand.Parameters.AddWithValue("@playlistID", playlistID);
            SqliteDataReader dataReader = sqlCommand.ExecuteReader();         
            while (dataReader.Read())
            {
                string path = dataReader["MediaPath"].ToString();
                if (System.IO.File.Exists(path))
                {
                    MediaItem addMedia = new MediaItem(path);
                    addMedia.PlaylistID = playlistID;
                    mediasInPlaylist.Add(addMedia);
                }
            }
            return mediasInPlaylist;
        }
        public void DeletePlaylist(string playlistID)
        {
            DeleteMediasInGivenPlaylist(playlistID);
            if (playlistDatabaseConnection.State == System.Data.ConnectionState.Closed)
            {
                playlistDatabaseConnection.Open();
            }
            string deleteQuery = "DELETE FROM Playlist " +
                "WHERE PlaylistID = @playlistID";           
            var sqlCommand = new SqliteCommand(deleteQuery, playlistDatabaseConnection);
            sqlCommand.Parameters.AddWithValue("@playlistID", playlistID);
            RunSqlCommand(sqlCommand);
            playlistDatabaseConnection.Close();
        }
        public void DeleteMediasInGivenPlaylist(string playlistID)
        {
            if (playlistDatabaseConnection.State == System.Data.ConnectionState.Closed)
            {
                playlistDatabaseConnection.Open();
            }
            string deleteQuery = "DELETE FROM PlaylistMedias " +
                "WHERE PlaylistID = @playlistID ";            
            var sqlCommand = new SqliteCommand(deleteQuery, playlistDatabaseConnection);
            sqlCommand.Parameters.AddWithValue("@playlistID", playlistID);
            RunSqlCommand(sqlCommand);
            playlistDatabaseConnection.Close();
        }
        public void DeleteMediasGivenPath(string mediaPath)
        {
            if (playlistDatabaseConnection.State == System.Data.ConnectionState.Closed)
            {
                playlistDatabaseConnection.Open();
            }
            string deleteQuery = "DELETE FROM PlaylistMedias " +
                "WHERE MediaPath = @mediaPath";            
            var sqlCommand = new SqliteCommand(deleteQuery, playlistDatabaseConnection);
            sqlCommand.Parameters.AddWithValue("@mediaPath", mediaPath);
            RunSqlCommand(sqlCommand);
            playlistDatabaseConnection.Close();
        }

        public void DeleteMediaInAPlaylist(string mediaPath, string playlistID)
        {
            if (playlistDatabaseConnection.State == System.Data.ConnectionState.Closed)
            {
                playlistDatabaseConnection.Open();
            }
            string deleteQuery = "DELETE FROM PlaylistMedias " +
                "WHERE MediaPath = @mediaPath AND PlaylistID = @playlistID";           
            var sqlCommand = new SqliteCommand(deleteQuery, playlistDatabaseConnection);
            AddValueIntoPlaylistMediasinSQLCommand(new Playlist(playlistID), new MediaItem(mediaPath), sqlCommand);
            RunSqlCommand(sqlCommand);
            playlistDatabaseConnection.Close();
        }

        public void DeleteNotExistMedias()
        {
            playlistDatabaseConnection.Open();
            string query = "SELECT MediaPath FROM PlaylistMedias";
            List<string> deletePaths = new List<string>();           
            var sqlCommand = new SqliteCommand(query, playlistDatabaseConnection);
            var dataReader = sqlCommand.ExecuteReader();

            while (dataReader.Read())
            {
                if (System.IO.File.Exists(dataReader["MediaPath"].ToString()) == false)
                    deletePaths.Add(dataReader["MediaPath"].ToString());
            }
            playlistDatabaseConnection.Close();

            foreach (var path in deletePaths)
            {
                DeleteMediasGivenPath(path);
            }

        }
        private void AddValueIntoPlaylistMediasinSQLCommand(Playlist playlistContainsMedia, MediaItem insertMedia, SqliteCommand sqlCommand)
        {
            sqlCommand.Parameters.AddWithValue("@mediaPath", insertMedia.FilePath);
            sqlCommand.Parameters.AddWithValue("@playlistID", playlistContainsMedia.PlayListID);
        }

        private void AddValueIntoPlaylistinSQLCommand(Playlist insertPlaylist, SqliteCommand sqlCommand)
        {
            sqlCommand.Parameters.AddWithValue("@playlistID", insertPlaylist.PlayListID);
            sqlCommand.Parameters.AddWithValue("@playlistName", insertPlaylist.PlayListName);
            sqlCommand.Parameters.AddWithValue("@playlistThumbnailPath", insertPlaylist.BackroundImageFileName);
            sqlCommand.Parameters.AddWithValue("@playlistDateOfCreation", insertPlaylist.DateCreated);
        }
    }
}
