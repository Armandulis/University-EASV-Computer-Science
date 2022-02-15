/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package mytunes.BLL;

import java.util.List;
import mytunes.BE.Playlists;
import mytunes.BE.Songs;
import mytunes.DAL.MyTunesDA;

/**
 *
 * @author Arman
 */
public class MyTunesBLL { //MyTunes business logic layer

  
    
     MyTunesDA da = new MyTunesDA();
    
     public List<Songs> getFilteredSongs(String searched) { //A method for filtering the songs
        return da.getFilteredSongs(searched);
    }
     
     public List<Songs> loadSongs(){ //A method for loading the songs
     return da.loadSongs();
     }
     
     public List<Playlists> loadPlaylists(){ //A method for loading the playlists
     return da.loadPlaylists();
     }
     
     public void addPlaylist(Playlists playlist){ //A method for adding a playlist
           
         da.addPlaylist(playlist);

     }

    public void addSong(Songs song) { //A method for adding a song
       da.addSong(song);
    }
    
    

    public void removePlaylist(Playlists selectedPlaylist) { //A method for deleting a playlist
        da.removePlaylist(selectedPlaylist);
    }
    
    public void removeSong(Songs selectedSong) { //A method for deleting a song
        da.removeSong(selectedSong);
    }
     
    public void updatePlaylist(Playlists updatePlaylist) { //A method for updating the playlist
        da.updatePlaylist(updatePlaylist);
    }
    
    public void updateSong(Songs updateSong) { //A method for updating the song
        da.updateSong(updateSong);
    }

    public List<Songs> loadSongsOnPlaylist(Playlists selectedPlaylist) { //A method for loading the songs on a playlist
         return da.getSongsInPlaylistList(selectedPlaylist);
       
    }
    
    public void addSongToPlaylist( Playlists playlist, Songs song) { //A method for adding a song to the playlist
       da.addSongToPlaylist(playlist, song);
    }
    
    public void removeSongFromPlaylist(Songs song, Playlists playlist) { //A method for deleting a song from the playlist
        da.removeSongFromPlaylist(song, playlist);
    }

    
     
     

    
    
    
   
     
    
    
    
}
