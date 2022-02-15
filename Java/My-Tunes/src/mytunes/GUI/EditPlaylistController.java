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
import mytunes.BLL.MyTunesBLL;
/**
 * FXML Controller class
 *
 * @author Arman
 */
public class EditPlaylistController implements Initializable { //Loads the EditPlaylist Window

    @FXML
    private TextField textFieldName;
    @FXML
    private Button closebutton;
    
    MainWindowController mwc;
    
    MyTunesBLL bll = new MyTunesBLL();
    
    
    

    /**
     * Initializes the controller class.
     */
    @Override
    public void initialize(URL url, ResourceBundle rb) {
    }    
    
    public void setMainViewCont(MainWindowController mwc){
        this.mwc=mwc;
    }

    @FXML
    private void buttonSave(ActionEvent event) { //Saves the renamed playlist
        Playlists updatePlaylist = mwc.getSelectedPlaylist();
        updatePlaylist.setName(textFieldName.getText());
        bll.updatePlaylist(updatePlaylist);
        
        mwc.loadPlaylists();
        
        Stage stage = (Stage) closebutton.getScene().getWindow();
        stage.close();
        
    }

    @FXML
    private void buttonCancel(ActionEvent event) { //Cancels the current window
          Stage stage = (Stage) closebutton.getScene().getWindow();
            stage.close();
    }
    
    
    
    
    
    
}
