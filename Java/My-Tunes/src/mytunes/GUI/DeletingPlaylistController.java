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
import javafx.stage.Stage;

/**
 * FXML Controller class
 *
 * @author Arman
 */
public class DeletingPlaylistController implements Initializable {

    private MainWindowController mwc;
    @FXML
    private Button buttonClosefx;

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
    private void buttonDelete(ActionEvent event) {
        mwc.deletePlaylist();
        Stage stage = (Stage) buttonClosefx.getScene().getWindow();
            stage.close();
    }

    @FXML
    private void buttonCancel(ActionEvent event) {
        Stage stage = (Stage) buttonClosefx.getScene().getWindow();
            stage.close();
    }
    
}
