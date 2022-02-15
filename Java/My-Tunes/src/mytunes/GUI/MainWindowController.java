    /*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package mytunes.GUI;

import java.io.File;
import java.io.IOException;
import java.net.URL;
import java.util.ResourceBundle;
import javafx.beans.InvalidationListener;
import javafx.beans.Observable;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.Slider;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.TextField;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.media.Media;
import javafx.scene.media.MediaPlayer;
import javafx.stage.Modality;
import javafx.stage.Stage;
import mytunes.BE.Playlists;
import mytunes.BE.Songs;
import mytunes.BLL.MyTunesBLL;

/**
 *
 * @author Arman
 */
public class MainWindowController implements Initializable { //Starts the lists and buttons
    
    @FXML
    private TextField textFieldSearch;
    MyTunesBLL playsongs = new MyTunesBLL();
    
    private MediaPlayer mp;
     private Media me;
    @FXML
    private Button buttonClosefx;
    @FXML
    private Button buttonSearchfx;
    
    MyTunesBLL  bll = new MyTunesBLL();
    
    int switchSearchButton = 0;
    int switchPlayToPauseButton = 0;
    @FXML
    private TableColumn<Songs, String> titleColumn;
    @FXML
    private TableColumn<Songs, String> ArtistColumn;
    @FXML
    private TableColumn<Songs, String> categoryColumn;
    @FXML
    private TableColumn<Songs, String> timeColumn;
    
    
    private ObservableList<Songs> songList = FXCollections.observableArrayList();
    @FXML
    private TableColumn<Playlists, String> columnName;
    
    private ObservableList<Playlists> playlistList = FXCollections.observableArrayList();
    
    @FXML
    private TableView<Songs> tableListSongs;
    @FXML
    private TableView<Playlists> tablePlaylistslist;
    
    @FXML
    private Button buttonPlayToPause;
    String path;
    @FXML
    private Label labelSongTitle;
    @FXML
    private TableView<Songs> tableListSongsInPlaylist;
    
    private ObservableList<Songs> listSongsInPlaylist = FXCollections.observableArrayList();
    @FXML
    private TableColumn<Songs, String> columnMiddleTitle;
    @FXML
    private TableColumn<Songs, String> columnMiddleArtist;
    @FXML
    private Slider dragVolume;
    
    
    
    @Override
    public void initialize(URL url, ResourceBundle rb) { //Loads everything to the lists
        
        loadPlaylists();
        loadSongs();
        
        titleColumn.setCellValueFactory( new PropertyValueFactory("title"));
        ArtistColumn.setCellValueFactory( new PropertyValueFactory("artist"));
        categoryColumn.setCellValueFactory(new PropertyValueFactory("category"));
        timeColumn.setCellValueFactory( new PropertyValueFactory("time"));
        columnName.setCellValueFactory(new PropertyValueFactory("name"));
        columnMiddleTitle.setCellValueFactory(new PropertyValueFactory("title"));
        columnMiddleArtist.setCellValueFactory(new PropertyValueFactory("artist"));
        
        tableListSongsInPlaylist.getSelectionModel().select(0);
        mediaPlayer("cinema.mp3");
        volume();
      
    }  
    
    public void mediaPlayer(String path){ //Plays the next song after the current one ends
        
        me = new Media(new File(path).toURI().toString());
        mp = new MediaPlayer(me);
        mp.setOnEndOfMedia(new Runnable() {
            @Override
            public void run() {
            tableListSongsInPlaylist.getSelectionModel().selectNext();
            Songs nextSong = tableListSongsInPlaylist.getSelectionModel().getSelectedItem();
            labelSongTitle.setText("" + nextSong.getArtist() +"-" + nextSong.getTitle());
                mediaPlayer(nextSong.getFile());
            mp.play();
            }
        });
    }
    
    private void volume(){
    
    dragVolume.setValue(mp.getVolume()*100);
        dragVolume.valueProperty().addListener(new InvalidationListener() {
            @Override
            public void invalidated(Observable observable) {
                mp.setVolume(dragVolume.getValue()/100);
            }
        });
    }
    

    @FXML
    private void buttonNewPlaylist(ActionEvent event) throws IOException  { //Adds a new playlist
        FXMLLoader loader = new FXMLLoader(getClass().getResource("NewPlaylist.fxml"));
        Parent root1 = loader.load();
        NewPlaylistController contrl = loader.getController();
        contrl.setMainViewCont(this);
        Stage stage = new Stage();
        stage.initModality(Modality.APPLICATION_MODAL);
        stage.setScene(new Scene(root1));
        stage.show();
    }

    @FXML
    private void buttonEditPlaylist(ActionEvent event) throws IOException { //Edits the selected playlist
        if(getSelectedPlaylist()!=null){
        FXMLLoader loader = new FXMLLoader(getClass().getResource("EditPlaylist.fxml"));
        Parent root1 = loader.load();
        EditPlaylistController contrl = loader.getController();
        contrl.setMainViewCont(this);
        Stage stage = new Stage();
        stage.initModality(Modality.APPLICATION_MODAL);
        stage.setScene(new Scene(root1));
        stage.show();}
    }

    @FXML
    private void buttonDeletePlaylist(ActionEvent event) throws IOException {  //Deletes the selected playlist
        
        if(getSelectedPlaylist()!=null){
        FXMLLoader loader = new FXMLLoader(getClass().getResource("DeletingPlaylist.fxml"));
        Parent root1 = loader.load();
        DeletingPlaylistController contrl = loader.getController();
        contrl.setMainViewCont(this);
        Stage stage = new Stage();
        stage.initModality(Modality.APPLICATION_MODAL);
        stage.setScene(new Scene(root1));
        stage.show();}
       
        
    }
    
    public void deletePlaylist(){
    
         if (getSelectedPlaylist() != null){
        bll.removePlaylist(getSelectedPlaylist());
        loadPlaylists();}
    
    }

    @FXML
    private void buttonNewSong(ActionEvent event) throws IOException { //Adds a new song to the song list
        FXMLLoader loader = new FXMLLoader(getClass().getResource("NewSong.fxml"));
        Parent root1 = loader.load();
        NewSongController contrl = loader.getController();
        contrl.setMainViewCont(this);
        Stage stage = new Stage();
        stage.initModality(Modality.APPLICATION_MODAL);
        stage.setScene(new Scene(root1));
        stage.show();
        
    }

    @FXML
    private void buttonEditSong(ActionEvent event) throws IOException { //Button to edit the selected song in the song list
        
        if(getSelectedSong()!=null){
        FXMLLoader loader = new FXMLLoader(getClass().getResource("EditSong.fxml"));
        Parent root1 = loader.load();
        EditSongController contrl = loader.getController();
        contrl.setMainViewCont(this);
        Stage stage = new Stage();
        stage.initModality(Modality.APPLICATION_MODAL);
        stage.setScene(new Scene(root1));
        stage.show();
       }
    }

    @FXML
    private void buttonDeleteSong(ActionEvent event) throws IOException { //Deletes the selected song from the song list
        
        if(getSelectedPlaylist()!=null){
        FXMLLoader loader = new FXMLLoader(getClass().getResource("DeletingSong.fxml"));
        Parent root1 = loader.load();
        DeletingSongController contrl = loader.getController();
        contrl.setMainViewCont(this);
        Stage stage = new Stage();
        stage.initModality(Modality.APPLICATION_MODAL);
        stage.setScene(new Scene(root1));
        stage.show();}
        
        
    }
    
    public void deleteSong(){
    
        if (getSelectedSong() != null){
            
        bll.removeSong(getSelectedSong());
        loadSongs();}
    
    }

    @FXML
    private void buttonClose(ActionEvent event) { //Closes the current window
            Stage stage = (Stage) buttonClosefx.getScene().getWindow();
            stage.close();
            
    }

    @FXML
    private void buttonDeleteSongFromList(ActionEvent event) throws IOException { //Deletes the selected song from the playlist
        if(tableListSongsInPlaylist.getSelectionModel().getSelectedItem()!=null){
        FXMLLoader loader = new FXMLLoader(getClass().getResource("DeletingSongOnPlaylist.fxml"));
        Parent root1 = loader.load();
        DeletingSongOnPlaylistController contrl = loader.getController();
        contrl.setMainViewCont(this);
        Stage stage = new Stage();
        stage.initModality(Modality.APPLICATION_MODAL);
        stage.setScene(new Scene(root1));
        stage.show();}
    }
    
    public void deleteSongFromPlaylist(){
        if (getSelectedPlaylist() != null && getSelectedSong() != null){
            bll.removeSongFromPlaylist(getSelectedSong(),getSelectedPlaylist());
            loadSongsOnPlaylist();
        }
    
    }
    
     @FXML
    private void buttonAddSong(ActionEvent event) { //Adds the selected song to the playlist
        if (getSelectedPlaylist() != null && getSelectedSong() != null){
        bll.addSongToPlaylist(getSelectedPlaylist(),getSelectedSong()); 
        loadSongsOnPlaylist();
        }
    }

    @FXML
    private void buttonMoveSongUp(ActionEvent event) {
    
    }

    @FXML
    private void buttonMoveSongDown(ActionEvent event) {
        
        
    }

   

    @FXML
    private void buttonPreviuos(ActionEvent event) { //Plays the previous song on the playlist 
        mp.stop();
        tableListSongsInPlaylist.getSelectionModel().selectPrevious();
        Songs selectedSong = tableListSongsInPlaylist.getSelectionModel().getSelectedItem();
        labelSongTitle.setText("" + selectedSong.getArtist() +"-" + selectedSong.getTitle());
         mediaPlayer(selectedSong.getFile());
        mp.play();
        buttonPlayToPause.setText("Pause");
        
    }
    @FXML
    private void buttonNext(ActionEvent event) { //Plays the next song on the playlist
        mp.stop();
        tableListSongsInPlaylist.getSelectionModel().selectNext();
        Songs selectedSong = tableListSongsInPlaylist.getSelectionModel().getSelectedItem();
        labelSongTitle.setText("" + selectedSong.getArtist() +"-" + selectedSong.getTitle());
       
        mediaPlayer(selectedSong.getFile());
        mp.play();
        buttonPlayToPause.setText("Pause");
       
    }
    
    @FXML
    private void buttonSearch(ActionEvent event) { //Searches for the typed song in the song list
        if (switchSearchButton ==0){
        String searched = textFieldSearch.getText();
        songList.addAll(bll.getFilteredSongs(searched));
        tableListSongs.setItems(songList);
        buttonSearchfx.setText("Clear");
        switchSearchButton ++;}
        else {
            loadSongs();
            textFieldSearch.clear();
            switchSearchButton = 0; 
            buttonSearchfx.setText("Search");
        }
    }

    @FXML
    private void buttonPlay(ActionEvent event) { //Button that plays the song
      // PLAY BUTTON
        
     if(buttonPlayToPause.getText().equals("Play")){ 
         
               
        buttonPlayToPause.setText("Pause");
        
        Songs selectedSong = tableListSongsInPlaylist.getSelectionModel().getSelectedItem();
        labelSongTitle.setText("" + selectedSong.getArtist() +"-" + selectedSong.getTitle());
        mp.play();
        }
        
     
     // PAUSE BUTTON
       else{
        mp.pause();
        buttonPlayToPause.setText("Play");
        }
        
    }
    
    public void loadSongs(){ //Loads all of the songs
     songList.clear();
     songList.addAll(bll.loadSongs());
     tableListSongs.setItems(songList);
    }
    
    public void loadPlaylists(){ //Loads all of the playlists
        playlistList.clear();
        playlistList.addAll(bll.loadPlaylists());
        tablePlaylistslist.setItems(playlistList);
        
    
    }
    
    public void addPlaylist(Playlists playlist){ //Adds a new playlist
        bll.addPlaylist(playlist);
        loadPlaylists();
    }
    
    public void addSong(Songs song){ //Adds a new song
        bll.addSong(song);
        loadSongs();
    
    }
    
    public Playlists getSelectedPlaylist(){ //Selects the clicked playlist
        Playlists selectedPlayist = tablePlaylistslist.getSelectionModel().getSelectedItem();
        return selectedPlayist;
    }
    
    public Songs getSelectedSong(){ //Returns a selected song
        Songs selectedSong = tableListSongs.getSelectionModel().getSelectedItem();
        return selectedSong;
    }
    
    @FXML
    private void butttonLoadList(ActionEvent event) { //Button for loading a playlist
        
        if(getSelectedPlaylist()!=null ){
        loadSongsOnPlaylist();
        }
    }
    
   private void loadSongsOnPlaylist(){ //Loads all of the songs on the playlist
    listSongsInPlaylist.clear();
    listSongsInPlaylist.addAll(bll.loadSongsOnPlaylist(getSelectedPlaylist()));
    tableListSongsInPlaylist.getItems().addAll(listSongsInPlaylist);
   }

 

    
    
    
    
    
     
     
        
}
