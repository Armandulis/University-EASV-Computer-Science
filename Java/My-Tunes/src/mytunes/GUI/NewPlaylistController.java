/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package mytunes.GUI;

import java.net.URL;
import java.util.ResourceBundle;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Button;
import javafx.scene.control.TextField;
import javafx.stage.Stage;
import mytunes.BE.Playlists;

/**
 * FXML Controller class
 *
 * @author Arman
 */
public class NewPlaylistController implements Initializable { //Starts the new list function

    @FXML
    private TextField textFieldName;
    @FXML
    private Button buttonClosefx;
    
    
        
        Playlists playlist = new Playlists();

        MainWindowController mwc;
    /**
     * Initializes the controller class.
     */
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        // TODO
    }    

    public void setMainViewCont(MainWindowController mwc){
        this.mwc=mwc;
    }
    @FXML
    private void buttonSavePlaylist(ActionEvent event) { //Saves the changes to the playlist
        playlist.setName(textFieldName.getText());
        mwc.addPlaylist(playlist);
        mwc.loadPlaylists();
        Stage stage = (Stage) buttonClosefx.getScene().getWindow();
        stage.close();
    }

    @FXML
    private void buttonCancelPaylist(ActionEvent event) { //Cancels the current window
            Stage stage = (Stage) buttonClosefx.getScene().getWindow();
            stage.close();
    }
    
    
    
}
