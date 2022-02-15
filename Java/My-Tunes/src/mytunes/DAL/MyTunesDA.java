/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package mytunes.DAL;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;
import mytunes.BE.Playlists;
import mytunes.BE.Songs;
import mytunes.BLL.MyTunesBLL;
import mytunes.ConnectionManager;

/**
 *
 * @author Arman
 */
public class MyTunesDA { //The Data Access class
    
     private static ConnectionManager cm =  new ConnectionManager();
    
    
    public List<Songs> getFilteredSongs(String searched) { //Loads the filtered songs to the list

        System.out.println("connecting");
        List<Songs> filteredSongs = new ArrayList();

        try (Connection con = cm.getConnection()) {

            String query = "SELECT * FROM SongsInfo WHERE title LIKE ? OR artist LIKE ? OR category LIKE ?  ";

            // Protect against SQL injection
            PreparedStatement pstmt= con.prepareStatement(query);
            pstmt.setString(1, "%" + searched + "%");
            pstmt.setString(2, "%" + searched + "%");
            pstmt.setString(3, "%" + searched + "%");

            ResultSet rs = pstmt.executeQuery();
            while (rs.next()) {
                Songs song = new Songs();
                song.setTitle(rs.getString("title"));
                song.setCategory(rs.getString("category"));
                song.setArtist(rs.getString("artist"));
                song.setTime(rs.getString("time"));
                song.setFile(rs.getString("fiel"));
                song.setId(rs.getInt("id"));

                filteredSongs.add(song);
            }
        }
        catch (SQLException ex) {
            Logger.getLogger(MyTunesBLL.class.getName()).log(Level.SEVERE, null, ex);
        }
        return filteredSongs;

    }
    
     public List<Songs> loadSongs() { //Loads the songs to the list

        List<Songs> allSongs = new ArrayList();

        try (Connection con = cm.getConnection()) {

            String query = "SELECT * FROM SongsInfo";
            PreparedStatement pstmt= con.prepareStatement(query);

            ResultSet rs = pstmt.executeQuery();
            while (rs.next()) {
                Songs song = new Songs();
                song.setId(rs.getInt("id"));
                song.setTitle(rs.getString("title"));
                song.setCategory(rs.getString("category"));
                song.setArtist(rs.getString("artist"));
                song.setTime(rs.getString("time"));
                song.setFile(rs.getString("fiel"));

                allSongs.add(song);
            }
        }
        catch (SQLException ex) {
            Logger.getLogger(MyTunesBLL.class.getName()).log(Level.SEVERE, null, ex);
        }
        return allSongs;

    }
     
      public List<Playlists> loadPlaylists() { //Loads all of the playlists

        List<Playlists> allPlaylists = new ArrayList();

        try (Connection con = cm.getConnection()) {

            String query = "SELECT * FROM PlaylistInfo";
            PreparedStatement pstmt= con.prepareStatement(query);

            ResultSet rs = pstmt.executeQuery();
            while (rs.next()) {
                Playlists playlist = new Playlists();
                playlist.setId(rs.getInt("id")); 
                playlist.setName(rs.getString("name"));

                allPlaylists.add(playlist);
            }
        }
        catch (SQLException ex) {
            Logger.getLogger(MyTunesBLL.class.getName()).log( Level.SEVERE, null, ex);
        }
        return allPlaylists;

    }

  
    public void addPlaylist(Playlists playlist) { //Adds a new playlist from the database
        try (Connection con = cm.getConnection()) {
            String sql  = "INSERT INTO PlaylistInfo (name) VALUES(?)";
            PreparedStatement pstmt = con.prepareStatement( sql, Statement.RETURN_GENERATED_KEYS);
            pstmt.setString(1, playlist.getName());
            

            int affected = pstmt.executeUpdate();
            if (affected<1)
                throw new SQLException("Playlist could not be added");

        }
        catch (SQLException ex) {
            Logger.getLogger(MyTunesDA.class.getName()).log(
                    Level.SEVERE, null, ex);
        }
    }

    public void addSong(Songs song) { //Adds a new song from the database
        try (Connection con = cm.getConnection()) {
            String sql  = "INSERT INTO SongsInfo (title,artist,category,fiel,time) VALUES(?,?,?,?,?)";
            PreparedStatement pstmt = con.prepareStatement( sql, Statement.RETURN_GENERATED_KEYS);
            pstmt.setString(1, song.getTitle());
            pstmt.setString(2, song.getArtist());
            pstmt.setString(3, song.getCategory());
            pstmt.setString(4, song.getFile());
            pstmt.setString(5, song.getTime());
            

            int affected = pstmt.executeUpdate();
            if (affected<1)
                throw new SQLException("Song could not be added");

        }
        catch (SQLException ex) {
            Logger.getLogger(MyTunesDA.class.getName()).log(
                    Level.SEVERE, null, ex);
        }
    }
    
   
    
    public void removePlaylist(Playlists playlist) {      //Removes the selected playlist from the program and the database
        System.err.println(playlist.getId());
        
        try (Connection con = cm.getConnection()) {
            String sql
                    = "DELETE FROM PlaylistInfo WHERE id = ?";
            PreparedStatement pstmt
                    = con.prepareStatement(sql);
            pstmt.setInt(1, playlist.getId());
            pstmt.execute();
        }
        catch (SQLException ex) {
            Logger.getLogger(MyTunesDA.class.getName()).log(
                    Level.SEVERE, null, ex);
        }
    }
    
    public void removeSong(Songs selectedSong) { //Removes the selected song from the song list
        try (Connection con = cm.getConnection()) {
            String sql
                    = "DELETE FROM SongsInfo WHERE id = ?";
            PreparedStatement pstmt
                    = con.prepareStatement(sql);
            pstmt.setInt(1, selectedSong.getId());
            pstmt.execute();
        }
        catch (SQLException ex) {
            Logger.getLogger(MyTunesDA.class.getName()).log(
                    Level.SEVERE, null, ex);
        }
    }
    
        public void updatePlaylist(Playlists updatePlaylist) {   //Loades the playlist from the database
            System.err.println(updatePlaylist.getId());
        try (Connection con = cm.getConnection()) {
            String sql = "UPDATE PlaylistInfo SET name=? WHERE id=?";
            PreparedStatement pstmt = con.prepareStatement(sql);
            pstmt.setString(1, updatePlaylist.getName());
            pstmt.setInt(2, updatePlaylist.getId());

            int affected = pstmt.executeUpdate();
            if (affected<1)
                throw new SQLException("PLaylist could not be updated");

        }
        catch (SQLException ex) {
            Logger.getLogger(MyTunesDA.class.getName()).log(
                    Level.SEVERE, null, ex);
        }
    }
        
    public void updateSong(Songs song) { //Overwrites the changes to the database
        System.err.println(song.getId());
        try (Connection con = cm.getConnection()) {
            String sql = "UPDATE SongsInfo SET title = ?, artist = ?, category =?, time =?, fiel = ? WHERE id=?";
            PreparedStatement pstmt = con.prepareStatement(sql);
            pstmt.setString(1, song.getArtist());
            pstmt.setString(2, song.getTitle());
            pstmt.setString(3, song.getCategory());
            pstmt.setString(4, song.getTime());
            pstmt.setString(5, song.getFile());
            pstmt.setInt(6, song.getId());

            int affected = pstmt.executeUpdate();
            if (affected<1)
                throw new SQLException("PLaylist could not be updated");

        }
        catch (SQLException ex) {
            Logger.getLogger(MyTunesDA.class.getName()).log(
                    Level.SEVERE, null, ex);
        }
    }
    public List<Songs> getSongsInPlaylistList(Playlists selectedPlaylist) { //Loads the songs in the selected playlist

        List<Songs> songsInPlaylistList = new ArrayList();

        try (Connection con = cm.getConnection()) {
            

            String query = "SELECT * FROM songsOnPlaylist WHERE idplaylist = ?";

            // Protect against SQL injection
            PreparedStatement pstmt= con.prepareStatement(query);
            
            pstmt.setInt(1, selectedPlaylist.getId());

            ResultSet rs = pstmt.executeQuery();
            while (rs.next()) {
                
                int songID = rs.getInt("idsong");
                
                String queryList = "SELECT * FROM SongsInfo WHERE id = ?";
                
                PreparedStatement pstmtList= con.prepareStatement(queryList);
                pstmtList.setInt(1,songID);
                
                ResultSet rsList = pstmtList.executeQuery();
                
                Songs song = new Songs();
                rsList.next();
                song.setTitle(rsList.getString("title"));
                song.setCategory(rsList.getString("category"));
                song.setArtist(rsList.getString("artist"));
                song.setTime(rsList.getString("time"));
                song.setFile(rsList.getString("fiel"));
                song.setId(rsList.getInt("id"));
                
                songsInPlaylistList.add(song);

            }
        }
        catch (SQLException ex) {
            Logger.getLogger(MyTunesBLL.class.getName()).log(Level.SEVERE, null, ex);
        }
        return songsInPlaylistList;

    }
    
    public void addSongToPlaylist(Playlists playlist,Songs song) { //Adds the selected song to the playlist
        try (Connection con = cm.getConnection()) {
            String sql  = "INSERT INTO songsOnPlaylist (idplaylist, idsong) VALUES(?,?)";
            PreparedStatement pstmt = con.prepareStatement( sql, Statement.RETURN_GENERATED_KEYS);
            pstmt.setInt(1, playlist.getId());
            pstmt.setInt(2, song.getId());
            
            int affected = pstmt.executeUpdate();
            if (affected<1)
                throw new SQLException("Song could not be added");

        }
        catch (SQLException ex) {
            Logger.getLogger(MyTunesDA.class.getName()).log(
                    Level.SEVERE, null, ex);
        }
    }
    
    public void removeSongFromPlaylist(Songs song, Playlists playlist) { //Removes the selected song from playlist and database
        try (Connection con = cm.getConnection()) {
            String sql
                    = "DELETE FROM songsOnPlaylist WHERE idplaylist = ? AND idsong = ?";
            PreparedStatement pstmt
                    = con.prepareStatement(sql);
            pstmt.setInt(1, playlist.getId());
            pstmt.setInt(2, song.getId());
            pstmt.execute();
        }
        catch (SQLException ex) {
            Logger.getLogger(MyTunesDA.class.getName()).log(
                    Level.SEVERE, null, ex);
        }
    }
    
    
    
    
   
    
}
