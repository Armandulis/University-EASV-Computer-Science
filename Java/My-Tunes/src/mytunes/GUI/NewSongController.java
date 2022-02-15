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
import javafx.scene.control.Label;
import javafx.scene.control.TextField;
import javafx.stage.Stage;
import javax.swing.JFileChooser;
import javax.swing.filechooser.FileNameExtensionFilter;
import mytunes.BE.Songs;

/**
 * FXML Controller class
 *
 * @author Arman
 */
public class NewSongController implements Initializable { //Initializes the controller

    @FXML
    private TextField textFieldTitle;
    @FXML
    private TextField textFieldArtist;
    @FXML
    private TextField textFieldTime;
    @FXML
    private ComboBox<String> ComboBoxCategory;
    @FXML
    private TextField textFieldFile;
    @FXML
    private Button buttonClosefx;
    
    MainWindowController mwc;
        
        Songs song = new Songs();
    @FXML
    private Label labelError;
    

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
    
    private void makeComboBox(){ //A combo box of song's categories
        ComboBoxCategory.getItems().add("Rap");
        ComboBoxCategory.getItems().add("Country");
        ComboBoxCategory.getItems().add("Pop");
        ComboBoxCategory.getItems().add("Rock");
        ComboBoxCategory.getItems().add("Metal");
        ComboBoxCategory.getItems().add("Jazz");
        ComboBoxCategory.getItems().add("Classic");
    }
   
    
    private void makeSongsList(){ //Properties of a song
       
        song.setArtist(textFieldArtist.getText());
        song.setCategory(ComboBoxCategory.getValue());
        song.setTime(textFieldTime.getText());
        song.setTitle(textFieldTitle.getText());
        song.setFile(textFieldFile.getText());
        mwc.addSong(song);
        
    }
     

    @FXML
    private void buttonSave(ActionEvent event) { //Saves the made song to the list
        if(textFieldArtist.equals("") ||  ComboBoxCategory.equals("") || textFieldTime.equals("") || textFieldTitle.equals("") || textFieldFile.equals("")){
         labelError.setText("Please fill in all the fields!");}
        else {
        makeSongsList();
        mwc.loadSongs();
        Stage stage = (Stage) buttonClosefx.getScene().getWindow();
        stage.close();}
        
    }

    @FXML
    private void buttonCancel(ActionEvent event) { //Cancels the current window
            Stage stage = (Stage) buttonClosefx.getScene().getWindow();
            stage.close();
    }

    @FXML
    private void buttonChoose(ActionEvent event) { //A method for choosing the song's file
        JFileChooser chooser = new JFileChooser();
        FileNameExtensionFilter filter = new FileNameExtensionFilter( "MP3", "wav", "mp3");
        chooser.setFileFilter(filter);
        int returnVal = chooser.showOpenDialog(null);
        if(returnVal == JFileChooser.APPROVE_OPTION) {
            String songPath = chooser.getSelectedFile().getAbsolutePath();
            textFieldFile.setText(songPath);
        }
    }
    
}
