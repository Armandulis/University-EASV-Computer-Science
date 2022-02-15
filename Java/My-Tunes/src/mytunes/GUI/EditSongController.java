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
import javafx.scene.control.ComboBox;
import javafx.scene.control.TextField;
import javafx.stage.Stage;
import javax.swing.JFileChooser;
import mytunes.BE.Songs;
import mytunes.BLL.MyTunesBLL;

/**
 * FXML Controller class
 *
 * @author Arman
 */
public class EditSongController implements Initializable { //Loads the controller

    @FXML
    private TextField textFieldTitle;
    @FXML
    private TextField textFieldArtist;
    @FXML
    private TextField textFieldTime;
    @FXML
    private ComboBox<String> comboBoxCategory;
    @FXML
    private Button buttonClosefx;
    @FXML
    private TextField textFieldFile;
    @FXML
    private Button buttonLocation;
    MainWindowController mwc;
    MyTunesBLL bll = new MyTunesBLL();

   

    /**
     * Initializes the controller class.
     */
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        makeComboBox();
        
    }    
    public void setMainViewCont(MainWindowController mwc){
        this.mwc=mwc;
    }
    
    private void makeComboBox(){ // A Box with categories
        comboBoxCategory.getItems().add("Rap");
        comboBoxCategory.getItems().add("Country");
        comboBoxCategory.getItems().add("Pop");
        comboBoxCategory.getItems().add("Rock");
        comboBoxCategory.getItems().add("Metal");
        comboBoxCategory.getItems().add("Jazz");
        comboBoxCategory.getItems().add("Classic");
    }
    
   
    
    private Songs makeSong() { //Rename the properties of a selected song
        Songs updateSong = mwc.getSelectedSong();
        updateSong.setArtist(textFieldArtist.getText());
        updateSong.setTime(textFieldTime.getText());
        updateSong.setFile(textFieldFile.getText());
        updateSong.setTitle(textFieldTitle.getText());
        updateSong.setCategory(comboBoxCategory.getValue());
        return updateSong;
    
    }

    @FXML
    private void buttonSave(ActionEvent event) { //Saves the changed properties
        
        bll.updateSong(makeSong());
        mwc.loadSongs();
        
        Stage stage = (Stage) buttonClosefx.getScene().getWindow();
        stage.close();
        
        
    }

    @FXML
    private void buttonCancel(ActionEvent event) { //Cancels the current window
            Stage stage = (Stage) buttonClosefx.getScene().getWindow();
            stage.close();
    }

    @FXML
    private void buttonGetLocation(ActionEvent event) { //Returns the location to default for the file selection
        JFileChooser chooser = new JFileChooser();
        int returnVal = chooser.showOpenDialog(null);
        if(returnVal == JFileChooser.APPROVE_OPTION) {
            String songPath = chooser.getSelectedFile().getAbsolutePath();
            textFieldFile.setText(songPath);
        }
    }

    
}
